using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using ZajeciaREST.Application.Extensions;
using ZajeciaREST.Domain.Mapper;
using ZajeciaREST.Infrastructure;
using ZajeciaREST.Infrastructure.Entity;
using ZajeciaREST.Infrastructure.Extension;
using ZajeciaREST.Infrastructure.Mapper;
using ZajeciaREST.Infrastructure.Settings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new DomainProfile());
    mc.AddProfile(new InfrastructureProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var infrastructureSettings = builder.Configuration.GetSection("Infrastructure").Get<InfrastructureSettings>();

builder.Services.AddAuthorization();
builder.Services.AddInfrastructure(infrastructureSettings);

if (string.IsNullOrEmpty(infrastructureSettings.ConnectionString))
{
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseInMemoryDatabase("Database");
    });
}
else
{
    builder.Services.AddEntityFrameworkNpgsql()
        .AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(infrastructureSettings.ConnectionString);
        });
            
    builder.Services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<AppRole>()
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();
    
    
    builder.Services.AddIdentityApiEndpoints<AppUser>()
        .AddEntityFrameworkStores<DataContext>();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;

        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedAccount = false;
        
        options.User.RequireUniqueEmail = true;
    });
    
}

builder.Services.AddApplication();
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5001;
  
});

var app = builder.Build();
app.MapGroup("/account").MapIdentityApi<AppUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
