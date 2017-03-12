using StudentAppHelper.Library.AppLogic.CustomGenericLogic;
using StudentAppHelper.ModelBindings.Models.CustomUserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace StudentsApp.API.Custom
{
  public class CustomAppAPI : ApiController
  {
    private CustomLogic _customLogic = new CustomLogic();
    private UserApp _userApp;
    public UserApp UserApp
    {
      get
      {
        return _userApp;
      }
      internal set
      {
        _userApp = value;
      }
    }

    internal void cargarUser()
    {
      string UserKey = string.Empty;
      var requies = Request;
      var requcontex = RequestContext;
      var owin = HttpContext.Current.GetOwinContext().Request.Headers["loginKey"];//Where(x => x.Key == "loginKey").FirstOrDefault().Value;
      if (owin != null)
      {
        UserKey = owin;
        var user = _customLogic.getUserByLoginKey(UserKey);
        if (user == null)
        {
          Unauthorized();
        }
        UserApp = user;
      }
    }
    
  }
}