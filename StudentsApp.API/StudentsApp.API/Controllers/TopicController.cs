using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentAppHelper.Library.AppLogic;
using StudentAppHelper.Library.Auth;
using StudentAppHelper.ModelBindings.Models;
using StudentsApp.API.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace StudentsApp.API.Controllers
{
  [LoginAuthorize]
  [RoutePrefix("api/Topic")]
  public class TopicController : CustomAppAPI
  {
    public TopicController(LoginUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat) :base ()
    {
      UserManager = userManager;
      AccessTokenFormat = accessTokenFormat;
      _signInManager = new SignInManager<LoginUser, string>(userManager, HttpContext.Current.GetOwinContext().Authentication);
    }

    public TopicController() : this(Startup.UserManagerFactory(), Startup.OAuthOptions.AccessTokenFormat)
    {

    }
    
    public SignInManager<LoginUser, string> _signInManager { get; private set; }
    public LoginUserManager UserManager { get; private set; }
    public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
    TopicLogic logic = new TopicLogic();

    [HttpPost]
    [Route("topiList")]
    public List<string> topicList(int indexMatter)
    {
      List<string> listMater = new List<string>();
      string matterName = "Matematicas";//listMater[indexMatter];
      int userId = UserApp.IdUser;
      List<string> listTopic = logic.lisTopic(matterName, userId);
      return listTopic;
    }
    [AllowAnonymous]
    [HttpPost]
    [Route("createTopic")]
    public bool createTopic(TopicModel topic)
    {
      //return false;
      List<string> listMater = new List<string>();
      string matterName = string.Empty;
      if (topic.MatterIndex !=0)
      {
        matterName = listMater[topic.MatterIndex];
      }
      int userId = UserApp.IdUser;
      bool createTopic = logic.createTopic(matterName, userId, topic);
      return createTopic;
    }

  }
}