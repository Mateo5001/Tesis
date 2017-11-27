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

namespace StudentApp.Movile.Util.CustomViewModel
{
  public class CustomAppViewModel :ViewModelBase, INotifyPropertyChanged
  {

    protected IHttpClientService _client;
    protected INavigationService _navigate;
    public CustomAppViewModel()
    {
      _client = GetInstance<IHttpClientService>();
      _navigate = GetInstance<INavigationService>();
    }

    protected TType GetInstance<TType>()
    {
      return ServiceLocator.Current.GetInstance<TType>();
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
