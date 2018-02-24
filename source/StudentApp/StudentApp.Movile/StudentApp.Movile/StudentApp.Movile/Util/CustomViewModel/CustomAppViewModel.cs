using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using StudentAppHelper.Services.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudentApp.Movile.Util.CustomViewModel
{
  public class CustomAppViewModel :ViewModelBase, INotifyPropertyChanged
  {

    protected IHttpClientService _client;
    protected INavigationService _navigate;
    protected IStorageFilesService _files;
    private bool _recargar = false;

    public CustomAppViewModel()
    {
      _client = GetInstance<IHttpClientService>();
      _navigate = GetInstance<INavigationService>();
      _files = GetInstance<IStorageFilesService>();
      goMain = cmdback;
    }

    protected TType GetInstance<TType>()
    {
      return ServiceLocator.Current.GetInstance<TType>();
    }


    public ICommand goMain { get; protected set; }

    public Command cmdback
    {
      get
      {
        return new Command(async () =>
        {
          await goBackMain();
        });
      }
    }

    public bool Recargar { get => _recargar; set {
        recargarActions();
        _recargar = value; } }
    
    public virtual void recargarActions()
    {

    }

    private async Task goBackMain()
    {
      _navigate.NavigateTo("Main");
    }

    #region propertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(params string[] arg)
    {
      foreach (string propertyname in arg)
      {
        OnPropertyChanged(arg);
      }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
        handler(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
  }
}
