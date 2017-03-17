using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace StudentsApp.API.Custom
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class LoginAuthorize : AuthorizeAttribute
  {

    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
      var loginKey = actionContext.Request.Headers.Where(x => x.Key == "loginKey").FirstOrDefault().Value;
      return loginKey != null;
    }
    public override void OnAuthorization(HttpActionContext actionContext)
    {

      base.OnAuthorization(actionContext);
    }
  }
}