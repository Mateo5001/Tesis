using DBEntityModel.DBModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentAppHelper.Library.AppLogic;
using StudentAppHelper.Library.Auth;
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
  public class MatterController : CustomAppAPI
  {

    public MatterController(LoginUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat) : base()
    {
      UserManager = userManager;
      AccessTokenFormat = accessTokenFormat;
      _signInManager = new SignInManager<LoginUser, string>(userManager, HttpContext.Current.GetOwinContext().Authentication);
    }
    public MatterController() : this(Startup.UserManagerFactory(), Startup.OAuthOptions.AccessTokenFormat)
    {

    }

    public SignInManager<LoginUser, string> _signInManager { get; private set; }
    public LoginUserManager UserManager { get; private set; }
    public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }


    public List<string> ListarMateria()
    {

      List<String> Lmaterias = new List<String>();

      QueryLogic materia = new QueryLogic();

      Lmaterias = materia.Consultar_Materia(UserApp.IdUser);
            
      return Lmaterias;
    }




  }
}








