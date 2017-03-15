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
using StudentAppHelper.Services.Contract;
using Xamarin.Forms;
using StudentApp.Movile.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(OAuthService))]
namespace StudentApp.Movile.Droid.Services
{
  class OAuthService: IOAuthService
  {
  }
}