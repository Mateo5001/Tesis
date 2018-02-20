using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Auth.Api.SignIn;
using Android.Content.PM;

namespace StudentApp.Movile.Droid.Activity
{
  [Activity(Label = "StudentApp", Icon = "@drawable/Icon2", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class FormsActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    GoogleSignInAccount mGoogleSignInAccount;
    protected override void OnCreate(Bundle savedInstanceState)
    {
      mGoogleSignInAccount=(GoogleSignInAccount)Intent.GetParcelableExtra("SignInAccount");
      ToolbarResource = Resource.Layout.Toolbar;
      TabLayoutResource = Resource.Layout.Tabbar;
      base.OnCreate(savedInstanceState);
      global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
      LoadApplication(new App()); 
    }
  }
}