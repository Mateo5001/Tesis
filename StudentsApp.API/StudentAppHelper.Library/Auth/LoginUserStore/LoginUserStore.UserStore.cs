using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StudentAppHelper.Library.AppLogic;

namespace StudentAppHelper.Library.Auth
{
  public partial class LoginUserStore : IUserStore<LoginUser,string> 
  {
    AccountLogic AccountManagerClass = new AccountLogic();
    public Task CreateAsync(LoginUser user)
    {
      if (user == null)
      {
        throw new ArgumentNullException("user");
      }
      AccountManagerClass.Registrar(user.UserModel);
      return Task.FromResult<object>(null);
    }

    public Task DeleteAsync(LoginUser user)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
     
    }

    public Task<LoginUser> FindByIdAsync(string userId)
    {
      if (string.IsNullOrEmpty(userId))
      {
        return Task.FromResult<LoginUser>(null);
      }

      var result = AccountManagerClass.GetUserById(userId);
      if (result != null && result.Count == 1)
      {
        return Task.FromResult<LoginUser>(result[0]);
      }

      return Task.FromResult<LoginUser>(null);
    }

    public Task<LoginUser> FindByNameAsync(string userName)
    {
      if (string.IsNullOrEmpty(userName))
      {
        throw new ArgumentException("Null or empty argument: userName");
      }
      var result = AccountManagerClass.FindByName(userName);
      // Should I throw if > 1 user?
      if (result != null && result.Count == 1)
      {
        return Task.FromResult(result[0]);
      }
      return Task.FromResult<LoginUser>(null);
    }


    public Task UpdateAsync(LoginUser user)
    {
      return Task.Run(() => { CreateAsync(user); });
    }



  }
}