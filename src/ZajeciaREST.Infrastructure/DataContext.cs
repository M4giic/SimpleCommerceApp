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

}
