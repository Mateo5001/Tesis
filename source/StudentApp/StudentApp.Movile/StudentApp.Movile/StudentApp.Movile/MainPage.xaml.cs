using StudentApp.Movile.ViewModel;
using StudentApp.Movile.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentApp.Movile
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
      MainViewModel mainView = new MainViewModel();
      BindingContext = mainView;
      Navigation.PushAsync(new Login());
    }
  }
}
