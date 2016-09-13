using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Content;
using Android.Gms.Plus.Model.People;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Android.Gms.Auth.Api;
using Android.Support.V7.App;
using StudentApp.Views.MastePage;

namespace StudentApp.Droid
{
  [Activity(Label = "StudentApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,  IOnConnectionFailedListener
  {
    #region atrib only for google auth with android new type of auth
    private GoogleApiClient mGoogleApiClient;
    private GoogleSignInOptions mGoogleSigninOptions;
    private SignInButton btnGoogleSingIn;

    private static string TAG = "SignInActivity";
    private static int RC_SIGN_IN = 9001;
    #endregion
    
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(Resource.Layout.logInLayout);
      global::Xamarin.Forms.Forms.Init(this, bundle);
      //LoadApplication(new App(ref btnGoogleSingIn));
      InicioGPlusStart();
      ConfigureBtnSignInGplus();
    }

    private void ConfigureBtnSignInGplus()
    {
      btnGoogleSingIn = (SignInButton)FindViewById(Resource.Id.sign_in_button);
      btnGoogleSingIn.SetSize(SignInButton.SizeWide);
      btnGoogleSingIn.SetScopes(mGoogleSigninOptions.GetScopeArray());
      btnGoogleSingIn.Click += btnGoogleSingIn_Click;
    }

    private void InicioGPlusStart()
    {
      mGoogleSigninOptions  = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
        .RequestEmail()
        .RequestProfile()
        .Build();

      mGoogleApiClient = new GoogleApiClient.Builder(this)
       .EnableAutoManage(this,this)
        .AddApi(Auth.GOOGLE_SIGN_IN_API, mGoogleSigninOptions)
        .Build();
    }

    protected override void OnStart()
    {
      base.OnStart();
    }

    protected override void OnStop()
    {
      base.OnStop();
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
      if (requestCode == RC_SIGN_IN)
      {
        GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
        handleSignInResult(result);
      }
    }

    private void handleSignInResult(GoogleSignInResult result)
    {
      //Log.d(TAG, "handleSignInResult:" + result.IsSuccess);
      if (result.IsSuccess)
      {
        GoogleSignInAccount acct = result.SignInAccount;
        LoadApplication(new App());
      }
      
    }

    protected override void OnSaveInstanceState(Bundle outState)
    {
    }

    private void btnGoogleSingIn_Click(object sender, EventArgs e)
    {
      Intent signInIntent =Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
      StartActivityForResult(signInIntent, RC_SIGN_IN);
    }

    public void OnConnectionFailed(ConnectionResult result)
    {
      throw new NotImplementedException();
    }
  }
}

