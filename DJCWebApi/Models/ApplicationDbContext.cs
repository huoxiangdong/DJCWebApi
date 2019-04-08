namespace DJCWebApi.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", false)
        {
        }

        public static ApplicationDbContext Create() => 
            new ApplicationDbContext();
    }
}

