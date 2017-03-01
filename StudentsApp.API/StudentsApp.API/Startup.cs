using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(StudentsApp.API.Startup))]

namespace StudentsApp.API
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      HttpConfiguration config = new HttpConfiguration();
      WebApiConfig.Register(config);
      app.UseWebApi(config);
      ConfigureAuth(app);
    }
  }
}
