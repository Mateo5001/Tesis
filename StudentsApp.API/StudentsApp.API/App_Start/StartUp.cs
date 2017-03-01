using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using StudentAppHelper.Library.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace StudentsApp.API
{
  public partial class Startup
  {
    static Startup()
    {
      PublicClientId = "self";

      UserManagerFactory = () => new LoginUserManager(new LoginUserStore());
      
      //SiginLoginUser =  new SignInManager<LoginUser,string>(null , new LoginAuthenticationManager());
      OAuthOptions = new OAuthAuthorizationServerOptions
      {
        TokenEndpointPath = new PathString("/Token"),
        Provider = new AplicationAuthProvider(PublicClientId, UserManagerFactory),
        //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
        AccessTokenExpireTimeSpan = TimeSpan.FromDays(14)

      };
    }

    public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

    public static LoginUserManager SiginLoginUser { get; set; }
    public static Func<LoginUserManager> UserManagerFactory { get; set; }


    public static string PublicClientId { get; private set; }



    public void ConfigureAuth(IAppBuilder app)
    {
      // Enable the application to use a cookie to store information for the signed in user
      // and to use a cookie to temporarily store information about a user logging in with a third party login provider
      app.CreatePerOwinContext<LoginUserManager>(LoginUserManager.Create);
      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
      });
      app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

      // Enable the application to use bearer tokens to authenticate users
      app.UseOAuthBearerTokens(OAuthOptions);

      // Uncomment the following lines to enable logging in with third party login providers
      //app.UseMicrosoftAccountAuthentication(
      //    clientId: "",
      //    clientSecret: "");

      //app.UseTwitterAuthentication(
      //    consumerKey: "",
      //    consumerSecret: "");

      //app.UseFacebookAuthentication(
      //    appId: "",
      //    appSecret: "");

      //app.UseGoogleAuthentication();
    }

  }
}