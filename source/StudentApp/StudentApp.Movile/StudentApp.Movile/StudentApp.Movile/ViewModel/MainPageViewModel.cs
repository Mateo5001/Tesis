using StudenAppHelper.ResousrcesStrings;
using StudentApp.Movile.Util.CustomViewModel;
using StudentAppHelper.ModelBindings.Models;
using StudentAppHelper.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudentApp.Movile.ViewModel
{
  public class MainPageViewModel : CustomAppViewModel
  {

    #region Atributos privados
    string _message;
    #endregion

    #region Consturctores
    public MainPageViewModel() : base()
    {
      btnHola = btnholadef;
    }
    #endregion

    #region Propiedades 
    public string btnholatext { get { return MainResource.btnhola; } }
    #endregion

    #region Encapsulamiento de atributos

    public string Message
    {
      get { return _message; }
      private set
      {
        _message = value;
        OnPropertyChanged();
      }
    }
    #endregion

    #region Defeinicion de comandos
    public ICommand btnHola { get; protected set; }
    #endregion

    #region Definicion de IComand
    public Command btnholadef
    {
      get
      {
        return new Command(async () =>
        {
          await holaMundo();
        });
      }
    }
    #endregion

        #region eventos
    private async Task  holaMundo()
    {
      _client.IsAuthenticated = true;
      var result =  await _client.CallAsync<ObjectNull,string>("api/Account/holamundo",new ObjectNull());
      if (result == null)
        _navigate.NavigateTo("Login");
      Message = result;//result;

    }
    #endregion
  }
}
