using Microsoft.AspNetCore.Identity;

namespace ZajeciaREST.Infrastructure.Entity;

public class AppUser : IdentityUser<long>
{
    public Guid UserGuid { get; set; }
}

public class AppRole : IdentityRole<long>
{
    public AppRole() : base()
    {

    }

    public AppRole(string roleName)
    {
        Name = roleName;
    }
}