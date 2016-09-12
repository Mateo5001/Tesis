using StudentApp.Views.MastePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace StudentApp
{
  public partial class App : Application
  {
    public App(string NameLogIn)
    {
      InitializeComponent();

      MainPage = new MasterPage();
    }
    public App()
    {
      InitializeComponent();

      MainPage = new MasterPage();
    }

    public App(ref Button btnSignIn)
    {
      InitializeComponent();

      MainPage = new MainPage(ref btnSignIn);
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
