using GalaSoft.MvvmLight;
using StudenAppHelper.ResousrcesStrings;
using StudentApp.Movile.Interfase;
using StudentAppHelper.ModelBindings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudentApp.Movile.ViewModel
{
  public class LoginViewModel : ViewModelBase, ILoginViewModel
  {
    #region atributos privados
    private logInModel logingIn;
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
        return new Command(() => {
          var appLoginInSent = logingIn;
          string Stop = string.Empty;
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
