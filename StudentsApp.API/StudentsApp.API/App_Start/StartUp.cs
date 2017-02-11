using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace StudentsApp.API.App_Start
{
  public class StartUp
  {
    public void Configuration(IAppBuilder app)
    {
      HttpConfiguration config = new HttpConfiguration();
      WebApiConfig.Register(config);
      app.UseWebApi(config);
    }
  }
}