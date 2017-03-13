/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:StudentApp.Movile"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Autofac;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using StudentApp.Movile.Interfase;
using StudentAppHelper.Services.HTTP;
using StudentAppHelper.Services.Contract;
using Xamarin.Forms;
using Autofac.Extras.CommonServiceLocator;

namespace StudentApp.Movile.ViewModel
{
  /// <summary>
  /// This class contains static references to all the view models in the
  /// application and provides an entry point for the bindings.
  /// </summary>
  public class ViewModelLocator
  {
    /// <summary>
    /// Initializes a new instance of the ViewModelLocator class.
    /// </summary>
    public ViewModelLocator()
    {
      //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
      //SimpleIoc.Default.Register<MainViewModel>();
      //SimpleIoc.Default.Register<LoginViewModel>();
      var builder = new ContainerBuilder();

      builder.RegisterType<MainViewModel>().SingleInstance();
      builder.RegisterType<LoginViewModel>().SingleInstance();
      
      
      builder.RegisterType<HttpClientService>().As<IHttpClientService>();

      var container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);
      ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

    }

    public MainViewModel Main
    {
      get
      {
        return ServiceLocator.Current.GetInstance<MainViewModel>();
      }
    }
    public LoginViewModel Login
    {
      get
      {
        return ServiceLocator.Current.GetInstance<LoginViewModel>();
      }
    }

    public static void Cleanup()
    {
      // TODO Clear the ViewModels
    }
  }
}