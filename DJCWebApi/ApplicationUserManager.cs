namespace DJCWebApi
{
    using DJCWebApi.Models;
    using DJCWebApiBO.User;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.DataProtection;
    using PI.Core.BL;
    using PI.Core.vo;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            UserValidator<ApplicationUser> validator = new UserValidator<ApplicationUser>(manager) {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.UserValidator = validator;
            PasswordValidator validator2 = new PasswordValidator {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };
            manager.PasswordValidator = validator2;
            IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider > null)
            {
                string[] purposes = new string[] { "ASP.NET Identity" };
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create(purposes));
            }
            return manager;
        }

        public override Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            List<Claim> claims = new List<Claim> {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.UserName)
            };
            ClaimsIdentity result = new ClaimsIdentity(claims, authenticationType);
            return Task.FromResult<ClaimsIdentity>(result);
        }

        public override Task<ApplicationUser> FindAsync(string userName, string password)
        {
            UserVO rvo = UserBO.GetUser(userName);
            if (rvo == null)
            {
                return Task.FromResult<ApplicationUser>(null);
            }
            if ((UserBO.Validating(rvo.Pk_user, password) != UserValidatingResult.Passed) && !UserBO.FlyErpPWDCheck(rvo.Code, password))
            {
                return Task.FromResult<ApplicationUser>(null);
            }
            ApplicationUser result = new ApplicationUser {
                UserName = rvo.Pk_user
            };
            result.Discription = rvo.Name;
            result.UserCode = rvo.Code;
            return Task.FromResult<ApplicationUser>(result);
        }
    }
}

