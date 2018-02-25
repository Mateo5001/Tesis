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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using StudentAppHelper.ModelBindings.Models.General;

namespace StudentsApp.API.Controllers
{
  [LoginAuthorize]
  [RoutePrefix("api/Content")]
  public class ContentController : CustomAppAPI
  {
    public ContentController(LoginUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat) :base ()
    {
      UserManager = userManager;
      AccessTokenFormat = accessTokenFormat;
      _signInManager = new SignInManager<LoginUser, string>(userManager, HttpContext.Current.GetOwinContext().Authentication);
    }
    public ContentController() : this(Startup.UserManagerFactory(), Startup.OAuthOptions.AccessTokenFormat)
    {

    }

    public SignInManager<LoginUser, string> _signInManager { get; private set; }
    public LoginUserManager UserManager { get; private set; }
    public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
    ContentLogic contentlogic = new ContentLogic();

    [HttpPost]
    [Route("crearContenido")]
    public bool crearContenido(ContentModel content)
    {
      return contentlogic.crearContenido(content, UserApp.IdUser);
    }

    [HttpPost]
    [Route("busqueda")]
    public List<SearchResult> busqueda(SearchTextModel filtro)
    {
      return contentlogic.buscar(filtro.filtro);
    }

  }
}