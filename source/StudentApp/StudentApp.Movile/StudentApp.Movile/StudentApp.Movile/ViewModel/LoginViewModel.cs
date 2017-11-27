using GalaSoft.MvvmLight;
using StudenAppHelper.ResousrcesStrings;
using StudentAppHelper.ModelBindings.Models;
using StudentAppHelper.Services.HTTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Flurl.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using StudentAppHelper.Services.Contract;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudentApp.Movile.Util.CustomViewModel;

namespace StudentApp.Movile.ViewModel
{
  public class LoginViewModel : CustomAppViewModel
  {
    #region atributos privados
    private string _lk= string.Empty;
    private logInModel logingIn;
    private IStorageCookiesService _CookieService;
    #endregion

    #region constructor
    public LoginViewModel() : base ()
    {
      LogingIn = new logInModel();
      _CookieService = GetInstance<IStorageCookiesService>();
      CmdLogin = CmdLogin_Clicked;
      var storedcookie = _CookieService.GetCookieValue("loginKey");
      Lk = storedcookie;
    }
    #endregion

    #region propiedades 
    public string TextWelcome { get { return LoginResource.WelcomeString; } }
    public string phUser { get { return LoginResource.phUser; } }
    public string phPass { get { return LoginResource.phPass; } }
    public string nmBtnLogin { get { return LoginResource.nmBtnLogin; } }
    public bool isEnableLoginButton { get { return !(string.IsNullOrEmpty(logingIn.User)) && !(string.IsNullOrEmpty(logingIn.Password)); } }
    #endregion

    #region Encapsulamiento de atributos
    public string PassSTR
    {
      get { return logingIn.Password; }
      set
      {
        logingIn.Password = value;
        OnPropertyChanged("isEnableLoginButton");
      }
    }
    public string UserSTR
    {
      get { return logingIn.User; }
      set
      {
        logingIn.User = value;
        OnPropertyChanged("isEnableLoginButton");
      }
    }

    public logInModel LogingIn
    {
      get
      {
        return logingIn;
      }

      set
      {
        logingIn = value;
      }
    }
    public string Lk
    {
      get
      {
        return _lk;
      }

      set
      {
        _lk = value;
        OnPropertyChanged();
      }
    }
    #endregion

    #region def de comandos
    public Command CmdLogin_Clicked
    {
      get
      {
        return new Command(async () =>
        {
          _client.IsAuthenticated = false;
          var loginKey = await _client.CallAsync<logInModel, string>("api/Account/login", LogingIn);
          _CookieService.AddCookie("loginKey", loginKey);
          var storedcookie = _CookieService.GetCookieValue("loginKey");
          Lk = loginKey;
          _navigate.NavigateTo("Main");
        });
      }
    }
    #endregion

    #region def Interfaz de comandos
    public ICommand CmdLogin
    {
      protected set;
      get;
    }


    #endregion
  }

}
