using GalaSoft.MvvmLight;
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

namespace StudentApp.Movile.ViewModel
{
  public class LoginViewModel : ViewModelBase, ILoginViewModel
  {
    #region atributos privados
    private logInModel logingIn;

    HttpClient client = new HttpClient();
    #endregion

    #region constructor
    public LoginViewModel()
    {
      LogingIn = new logInModel();
      CmdLogin = CmdLogin_Clicked;
    }
    #endregion

    #region propiedades estaticas
    public string TextWelcome { get { return LoginResource.WelcomeString; } }
    public string phUser { get { return LoginResource.phUser; } }
    public string phPass { get { return LoginResource.phPass; } }
    public string nmBtnLogin { get { return LoginResource.nmBtnLogin; } }
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

          HttpClientService _client = new HttpClientService();
          _client.WithAtetntication = false;
          var app = await callExample(@"http://studentapphelper-api-test.azurewebsites.net/api/Account/Login", LogingIn);




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

    public async Task<string> callExample(string pathCall, logInModel obToSend)
    {
      try
      {
        //new MediaTypeFormatterCollection().FirstOrDefault();
        var uri = new Uri(pathCall);
        var json = JsonConvert.SerializeObject(obToSend);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
          return await response.Content.ReadAsAsync<string>();
        }
        else
        {
          return null;
        }
      }
      catch (Exception e)
      {
        throw e;
        return null;
      }
    }
  }
}
