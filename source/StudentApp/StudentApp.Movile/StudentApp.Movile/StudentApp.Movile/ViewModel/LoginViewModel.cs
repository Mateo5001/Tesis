﻿using GalaSoft.MvvmLight;
using StudenAppHelper.ResousrcesStrings;
using StudentApp.Movile.Interfase;
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

namespace StudentApp.Movile.ViewModel
{
  public class LoginViewModel : ViewModelBase,  INotifyPropertyChanged
  {
    #region atributos privados
    private logInModel logingIn;
    private IHttpClientService _client;
    #endregion

    #region constructor
    public LoginViewModel()
    {
      LogingIn = new logInModel();
      _client = ServiceLocator.Current.GetInstance<IHttpClientService>();
      CmdLogin = CmdLogin_Clicked;
    }
    #endregion

    #region propiedades estaticas
    public string TextWelcome { get { return LoginResource.WelcomeString; } }
    public string phUser { get { return LoginResource.phUser; } }
    public string phPass { get { return LoginResource.phPass; } }
    public string nmBtnLogin { get { return LoginResource.nmBtnLogin; } }
    public string LK { get; set; }
    #endregion

    #region Encapsulamiento de atributos
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
    #endregion

    #region def de comandos
    public Command CmdLogin_Clicked
    {
      get
      {
        return new Command(async() => {
          string loginKey=await _client.CallAsync<logInModel, string>("api/Account/login", LogingIn);
          LK = loginKey;
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
