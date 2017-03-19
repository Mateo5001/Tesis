using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using StudentApp.Movile.ViewModel;
using StudentApp.Movile.Views;
using StudentApp.Movile.Views.Login;
using StudentAppHelper.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace StudentApp.Movile
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
      Resources.Add("Locator", new ViewModelLocator());
      MainPage = new NavigationPage();
      var navigation = ServiceLocator.Current.GetInstance<INavigationService>();
      string inicio = ServiceLocator.Current.GetInstance<IStorageCookiesService>().existsCookie("loginKey") ? "Main" : "Login";
      navigation.NavigateTo(inicio);
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
