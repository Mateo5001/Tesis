using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using StudentApp.Movile.Util.CustomViewModel;
using StudentApp.Movile.ViewModel;
using StudentApp.Movile.Views;
using StudentApp.Movile.Views.About;
using StudentApp.Movile.Views.Content;
using StudentApp.Movile.Views.General;
using StudentApp.Movile.Views.Login;
using StudentApp.Movile.Views.MasterPage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentApp.Movile.Services.Navigation
{
  public class NavigationService : INavigationService
  {
    Dictionary<string, Type> Pages { get; set; }
    Dictionary<string, Type> viewModels { get; set; }

    string _currentPageKey;

    public Master MainPage
    {
      get
      {
        return (((Master)Application.Current.MainPage));
      }
    }

    public NavigationService()
    {
      Pages = new Dictionary<string, Type>();
      Pages.Add("Login", typeof(Login));
      Pages.Add("Main", typeof(MainPage));
      Pages.Add("Matter", typeof(Matter));
      Pages.Add("Topic", typeof(Topic));
      Pages.Add("Search", typeof(SearchContent));
      Pages.Add("About", typeof(About));
      Pages.Add("Audio", typeof(AudioContent));
      Pages.Add("Write", typeof(WriteContent));

      viewModels =new Dictionary<string, Type>();
      viewModels.Add("Login", typeof(LoginViewModel));
      viewModels.Add("Main", typeof(MainPageViewModel));
      viewModels.Add("Matter", typeof(MatterViewModel));
      viewModels.Add("Topic", typeof(TopicViewModel));
      viewModels.Add("Search", typeof(SearchContentViewModel));
      viewModels.Add("About", typeof(AboutViewModel));
      viewModels.Add("Audio", typeof(AudioContentViewModel));
      viewModels.Add("Write", typeof(WriteContentViewModel));
    } 

    #region INavigationService implementation

    public void GoBack()
    {
      //if (MainPage.Detail.Navigation.ModalStack.Count > 0)
      //{
      //  MainPage.Detail.Navigation.PopModalAsync();
      //}
      //else
      //{
      //MainPage.Detail.Navigation.PopAsync();
      //this.NavigateTo("MainPage");
      //}
    }

    public void NavigateTo(string pageKey)
    {
        NavigateTo(pageKey, null);
    }

    public void NavigateTo(string pageKey, object parameter)
    {
      try
      {
        object[] parameters = null;
        if (parameter != null)
        {
          parameters = new object[] { parameter };
        }
        Page displayPage = (Page)Activator.CreateInstance(Pages[pageKey], parameters);
        _currentPageKey = pageKey;
        //MainPage.Detail = displayPage; //Navigation.PushAsync(displayPage);
        //MainPage.Detail.Navigation.PushAsync(displayPage);
        Type type = viewModels[pageKey];
        CustomAppViewModel view = (CustomAppViewModel) ServiceLocator.Current.GetInstance(type);
        view.Recargar = true;
        MainPage.Detail = new NavigationPage(displayPage)
        {
          BarBackgroundColor = Color.FromHex("#585858"),
          BarTextColor = Color.Black
        };
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
      }
    }

    public string CurrentPageKey
    {
      get
      {
        return _currentPageKey;
      }
    }

    #endregion
  }
}
