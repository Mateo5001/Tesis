using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Plus;
using Android.Gms.Plus.Model.People;
using Android.Gms.Common;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Android.Gms.Auth.Api;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Android.Content;
using StudentApp.Movile.Droid.Activity;

namespace StudentApp.Movile.Droid
{
  [Activity(Label = "StudentApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IOnConnectionFailedListener
  {
    #region atrib only for google auth with android new type of auth
    private GoogleApiClient mGoogleApiClient;
    private GoogleSignInOptions mGoogleSigninOptions;
    private SignInButton btnGoogleSingIn;
    private GoogleSignInResult mGoogleSignInResult;
    private GoogleSignInAccount mGoogleSignInAccount;
    private static int RC_SIGN_IN = 9001;
    #endregion
   

    protected override void OnCreate(Bundle bundle)
    {
      ToolbarResource = Resource.Layout.Toolbar;
      TabLayoutResource = Resource.Layout.Tabbar;
      base.OnCreate(bundle);
      SetContentView(Resource.Layout.logInLayout);
      global::Xamarin.Forms.Forms.Init(this, bundle);
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
      mGoogleSigninOptions = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
        .RequestEmail()
        .RequestProfile()
        .Build();


      mGoogleApiClient = new GoogleApiClient.Builder(this)
       .EnableAutoManage(this, this)
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
        mGoogleSignInResult = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
        handleSignInResult();
      }
    }

    private void handleSignInResult()
    {
      if (mGoogleSignInResult.IsSuccess)
      {
        mGoogleSignInAccount = mGoogleSignInResult.SignInAccount;
        var forms = new Intent(this, typeof(FormsActivity));
        forms.PutExtra("SignInAccount", mGoogleSignInAccount);
        StartActivity(forms);
      }
    }

    private void btnGoogleSingIn_Click(object sender, EventArgs e)
    {
      Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
      StartActivityForResult(signInIntent, RC_SIGN_IN);
    }

    

    public void OnConnectionFailed(ConnectionResult result)
    {
      throw new NotImplementedException();
    }
  }
}

