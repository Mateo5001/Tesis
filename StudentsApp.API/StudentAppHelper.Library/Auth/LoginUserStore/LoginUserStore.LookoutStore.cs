using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentAppHelper.Library.Auth
{
  public partial class LoginUserStore : IUserLockoutStore<LoginUser, string>
  {
    public Task<int> GetAccessFailedCountAsync(LoginUser user)
    {
      return Task.FromResult(0);
    }
    public Task<bool> GetLockoutEnabledAsync(LoginUser user)
    {
      return Task.FromResult(false);
    }

    public Task<DateTimeOffset> GetLockoutEndDateAsync(LoginUser user)
    {

      return Task.FromResult(DateTimeOffset.Now.AddDays(-2));

    }

    public Task<int> IncrementAccessFailedCountAsync(LoginUser user)
    {
      return Task.FromResult(1);
    }

    public Task ResetAccessFailedCountAsync(LoginUser user)
    {
      return Task.Run(() => {; });
    }

    public Task SetLockoutEnabledAsync(LoginUser user, bool enabled)
    {
      throw new NotImplementedException();
    }

    public Task SetLockoutEndDateAsync(LoginUser user, DateTimeOffset lockoutEnd)
    {
      return Task.Run(()=> {; });
    }

   
  }
}