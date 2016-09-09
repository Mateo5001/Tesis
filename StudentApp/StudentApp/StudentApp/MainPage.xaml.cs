using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentApp
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
      
    }
    public MainPage(ref Button btnSignIn)
    {

      Button newbtnSignIn = new Button();
      newbtnSignIn.HorizontalOptions = LayoutOptions.Center;
      newbtnSignIn.VerticalOptions = LayoutOptions.End;
      this.Content = new StackLayout
      {
        Children =
                {
                    newbtnSignIn
                }
      };

      btnSignIn = newbtnSignIn;
      
    }
  }
}
