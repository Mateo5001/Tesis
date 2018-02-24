
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
using StudentsApp.API.Custom;
using StudentAppHelper.Library.AppLogic;
using StudentAppHelper.ModelBindings.Models;

namespace StudentsApp.API.Controllers
{
  [LoginAuthorize]
  [RoutePrefix("api/Account")]
  public class AccountController : CustomAppAPI
  {
    public AccountController(LoginUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat) :base ()
    {
      UserManager = userManager;
      AccessTokenFormat = accessTokenFormat;
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
        var user = new LoginUser(model);
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
      return "";
    }
    public bool logOut()
    {
      UserManager.Users.FirstOrDefault();
      throw new NotImplementedException();
    }

    TopicLogic logic = new TopicLogic();
    QueryLogic materia = new QueryLogic();
    [AllowAnonymous]
    [HttpPost]
    [Route("topiList")]
    public List<string> topicList(intBinding indexMatter)
    {
      int userId = UserApp.IdUser;
      List<string> listMater = materia.Consultar_Materia(userId);
      string matterName = listMater[indexMatter.entero];
      List<string> listTopic = logic.lisTopic(matterName, userId);
      return listTopic;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("createTopic")]
    public bool createTopic(TopicModel topic)
    {
      List<string> listMater = new List<string>();
      string matterName = string.Empty;
      if (topic.MatterIndex != 0)
      {
        matterName = listMater[topic.MatterIndex];
      }
      int userId = UserApp.IdUser;
      bool createTopic = logic.createTopic(matterName, userId, topic);
      return createTopic;
    }



    [HttpPost]
    [Route("holamundo")]
    public string holamundo()
    {
      var re =Request.Content.Headers;
      return "hola mundo";
    }
    
  }
}