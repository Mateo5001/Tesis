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
using StudentAppHelper.Services.HTTP;
using StudentAppHelper.Services.Contract;
using Xamarin.Forms;
using Autofac.Extras.CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using StudentApp.Movile.Services.Navigation;

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
      var builder = new ContainerBuilder();
      builder.RegisterType<TopicViewModel>().SingleInstance();
      builder.RegisterType<MainPageViewModel>().SingleInstance();
      builder.RegisterType<MenuAccountViewModel>().SingleInstance();
      builder.RegisterType<LoginViewModel>().SingleInstance();
      builder.RegisterType<MatterViewModel>().SingleInstance();
      builder.RegisterType<SearchContentViewModel>().SingleInstance();
      

      var oauth = DependencyService.Get<IStorageCookiesService>();
      builder.RegisterInstance(oauth).As<IStorageCookiesService>();

      builder.RegisterType<NavigationService>().As<INavigationService>();
      builder.RegisterType<HttpClientService>().As<IHttpClientService>();

      var container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);
      ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
    }
    public TopicViewModel Topic
    {
      get
      {
        return ServiceLocator.Current.GetInstance<TopicViewModel>();
      }
    }
    public MatterViewModel Matter
    {
      get
      {
        return ServiceLocator.Current.GetInstance<MatterViewModel>();
      }
    }

    public MenuAccountViewModel MenuAccount
    {
      get
      {
        return ServiceLocator.Current.GetInstance<MenuAccountViewModel>();
      }
    }
    public MainPageViewModel Main
    {
      get
      {
        return ServiceLocator.Current.GetInstance<MainPageViewModel>();
      }
    }
    public LoginViewModel Login
    {
      get
      {
        return ServiceLocator.Current.GetInstance<LoginViewModel>();
      }
    }
    public SearchContentViewModel Search
    {
      get
      {
        return ServiceLocator.Current.GetInstance<SearchContentViewModel>();
      }
    }
        public AboutViewModel About
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutViewModel>();
            }
        }

    public static void Cleanup()
    {
      // TODO Clear the ViewModels
    }
  }
}