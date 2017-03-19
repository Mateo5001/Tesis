using GalaSoft.MvvmLight.Views;
using StudentApp.Movile.Views;
using StudentApp.Movile.Views.Login;
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

    string _currentPageKey;

    public Page MainPage
    {
      get
      {
        return Application.Current.MainPage;
      }
    }

    public NavigationService()
    {
      Pages = new Dictionary<string, Type>();
      Pages.Add("Login", typeof(Login));
      Pages.Add("Main", typeof(MainPage));

    }

    #region INavigationService implementation

    public void GoBack()
    {
      if (MainPage.Navigation.ModalStack.Count > 0)
      {
        MainPage.Navigation.PopModalAsync();
      }
      else
      {
        MainPage.Navigation.PopAsync();
      }
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
        MainPage.Navigation.PushAsync(displayPage);
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
