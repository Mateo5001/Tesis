using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace StudentAppHelper.Library.Auth
{
  public class LoginUserManager : UserManager<LoginUser,string>
  {
    LoginUserStore Store;
    public LoginUserManager(LoginUserStore store) : base(store)
    {
      Store = store;
    }
    public override Task<bool> CheckPasswordAsync(LoginUser user, string password)
    {
      string passwordHash =Task.Run(() => Store.GetPasswordHashAsync(user)).Result;
      return Task.FromResult(password.Equals(password));
    }

    public override Task<bool> GetTwoFactorEnabledAsync(string userId)
    {
      var twoFactor = false;//base.GetTwoFactorEnabledAsync(userId);
      return Task.FromResult(twoFactor);
    }

    public static LoginUserManager Create(IdentityFactoryOptions<LoginUserManager> options, IOwinContext context)
    {
      var manager = new LoginUserManager(new LoginUserStore());
      // Configure validation logic for usernames
      manager.UserValidator = new UserValidator<LoginUser>(manager)
      {
        AllowOnlyAlphanumericUserNames = false,
        RequireUniqueEmail = true
      };
      // Configure validation logic for passwords
      manager.PasswordValidator = new PasswordValidator
      {
        RequiredLength = 6,
        RequireNonLetterOrDigit = true,
        RequireDigit = true,
        RequireLowercase = true,
        RequireUppercase = true,
      };
      var dataProtectionProvider = options.DataProtectionProvider;
      if (dataProtectionProvider != null)
      {
        manager.UserTokenProvider = new DataProtectorTokenProvider<LoginUser>(dataProtectionProvider.Create("ASP.NET Identity"));
      }

      return manager;
    }




  }
}