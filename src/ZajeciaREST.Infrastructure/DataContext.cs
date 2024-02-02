using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZajeciaREST.Infrastructure.Entity;

namespace ZajeciaREST.Infrastructure;

public class DataContext : IdentityDbContext<AppUser, AppRole, long>
{
    internal DbSet<ProductEntity> Products { get; set; }
    internal DbSet<CartEntity> Carts { get; set; }
 
    public DataContext()
    {
        
    }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
      
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<AppUser>()
            .Property(p => p.UserGuid)
            .ValueGeneratedOnAdd();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("User ID=fl0user;Password=OTES35mItnFb;Host=ep-round-hall-a285y2fy.eu-central-1.aws.neon.fl0.io;Port=5432;Database=databaseHttp;Connection Lifetime=0;");
}
