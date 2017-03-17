using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace StudentAppHelper.Library.Auth
{
  public partial class LoginUserStore : IUserPasswordStore<LoginUser>
  {
    public Task<string> GetPasswordHashAsync(LoginUser user)
    {
      string passwordHash = AccountManagerClass.getPasswordHash(user.Id);
      return Task.FromResult(passwordHash);
    }

    public Task<bool> HasPasswordAsync(LoginUser user)
    {
      return Task.FromResult(true);
    }

    public Task SetPasswordHashAsync(LoginUser user, string passwordHash)
    {
      throw new NotImplementedException();
    }
  }
}