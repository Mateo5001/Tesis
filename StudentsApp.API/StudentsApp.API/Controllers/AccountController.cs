
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http.Headers;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using StudentAppHelper.Library.Auth;
using StudentAppHelper.Library.Models;
using StudentsApp.API.Custom;
using StudentAppHelper.Library.AppLogic;

namespace StudentsApp.API.Controllers
{
  [LoginAuthorize]
  [RoutePrefix("api/Account")]
  public class AccountController : CustomAppAPI
  {
    public AccountController(LoginUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
    {
      UserManager = userManager;
      AccessTokenFormat = accessTokenFormat;
      ; 
      _signInManager = new SignInManager<LoginUser,string>(userManager, HttpContext.Current.GetOwinContext().Authentication);
      
    }


    public AccountController() : this(Startup.UserManagerFactory(), Startup.OAuthOptions.AccessTokenFormat)
    {

    }
    public SignInManager<LoginUser,string> _signInManager { get; private set; }
    public LoginUserManager UserManager { get; private set; }
    public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
    AccountLogic Account = new AccountLogic();
    [HttpPost]
    [AllowAnonymous]
    [Route("RegisterUser/{id}")]
    [Route("RegisterUser")]
    public async Task<string> RegisterUser(logInModel model)
    {
      try
      {
        var user = new LoginUser { UserName = model.User };
        var result = await UserManager.CreateAsync(user);
        if (result.Succeeded)
        {
          return "creado";
        }

      }
      catch (Exception e)
      {
        return "fallo";
      }
      finally
      {
      }
      return "fallo";
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("RegisterAdmin")]
    public async Task<bool> RegisterAdmin(UserRegistrationModel model)
    {
      try
      {
        var user = Account.Registrar(model);
        var result = await UserManager.CreateAsync(user);
        return result.Succeeded;
      }
      catch (Exception e)
      {
        return false;
      }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public async Task<string> Login(logInModel model)
    {
      try
      {
        var result = await _signInManager.PasswordSignInAsync(model.User,model.Password ,isPersistent: true, shouldLockout: false);
        //var listUsers = _signInManager.UserManager.Users.ToList();
        if (result == SignInStatus.Success)
        {

          var user = Account.FindByName(model.User);
          return user.FirstOrDefault().Id;
        }
      }
      catch ( Exception e)
      {
        
      }
      return "failure";
    }
    public bool logOut()
    {
      UserManager.Users.FirstOrDefault();
      throw new NotImplementedException();
    }


    [HttpGet]
    public string holamundo()
    {
      var re =Request.Content.Headers;
      return "hola mundo";
    }
    
  }
}