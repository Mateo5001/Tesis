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

namespace StudentApp.Movile.Droid
{
  [Activity(Label = "StudentApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IOnConnectionFailedListener,IConnectionCallbacks
  {
    #region atrib only for google auth with android new type of auth
    private GoogleApiClient mGoogleApiClient;
    private GoogleSignInOptions mGoogleSigninOptions;
    private SignInButton btnGoogleSingIn;
    private GoogleSignInResult mGoogleSignInResult;
    private GoogleSignInAccount mGoogleSignInAccount;

    private static string TAG = "SignInActivity";
    private static int RC_SIGN_IN = 9001;
    #endregion

    //private global::Xamarin.Forms.Button btnGoogleSingIn;
    private bool intentInProgres;
    private bool signInClicked;
    private ConnectionResult mConnectionResult;

    Bundle bun;

    public void OnConnected(Bundle connectionHint)
    {
      if(mGoogleSignInAccount!=null)
      {
        LoadApplication(new App());
      }
    }

    public void OnConnectionFailed(ConnectionResult result)
    {
      if (!intentInProgres)
      {
        mConnectionResult = result;
        if (signInClicked)
        {
          resolveSignInError();
        }
      }
    }

    public void OnConnectionSuspended(int cause)
    {
      throw new NotImplementedException();
    }

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      bun = bundle;
      //SetContentView(Resource.Layout.logInLayout);
      global::Xamarin.Forms.Forms.Init(this, bundle);
      //LoadApplication(new App(ref btnGoogleSingIn));
      InicioGPlusStart();
      //ConfigureBtnSignInGplus();
      LoadApplication(new App());
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
      #region Vieja forma de conexion 
      //mGoogleApiClient = new GoogleApiClient.Builder(this)
      // .AddConnectionCallbacks(this)
      // .AddOnConnectionFailedListener(this)
      // .AddApi(PlusClass.API)
      // .AddScope(PlusClass.ScopePlusProfile)
      // .AddScope(PlusClass.ScopePlusLogin)
      // .Build();
      #endregion

      mGoogleSigninOptions = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
        .RequestEmail()
        .RequestProfile()
        .Build();


      mGoogleApiClient = new GoogleApiClient.Builder(this)
       .EnableAutoManage(this, this)
        .AddApi(Auth.GOOGLE_SIGN_IN_API, mGoogleSigninOptions)
        .Build();

      mGoogleApiClient.Connect();

    }

    protected override void OnStart()
    {
      base.OnStart();
      mGoogleApiClient.Connect();
    }

    protected override void OnStop()
    {
      base.OnStop();
      if (mGoogleApiClient.IsConnected)
      {
        mGoogleApiClient.Disconnect();
      }
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
      #region anterior metodo
      //if (requestCode == 0)
      //{
      //  if (resultCode != Result.Ok)
      //  {
      //    signInClicked = false;

      //  }
      //  intentInProgres = false;
      //  if (!mGoogleApiClient.IsConnecting)
      //  {
      //    mGoogleApiClient.Connect();
      //  }
      //}
      #endregion
      if (requestCode == RC_SIGN_IN)
      {
        mGoogleSignInResult = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
        handleSignInResult();
      }
    }

    private void handleSignInResult()
    {
      //Log.d(TAG, "handleSignInResult:" + result.IsSuccess);
      if (mGoogleSignInResult.IsSuccess)
      {
        mGoogleSignInAccount = mGoogleSignInResult.SignInAccount;
        
        Xamarin.Forms.Forms.Init(this, bun);
        LoadApplication(new App());
      }
    }

    private void btnGoogleSingIn_Click(object sender, EventArgs e)
    {
      #region forma anterior
      //if (!mGoogleApiClient.IsConnecting)
      //{
      //  signInClicked = true;
      //  mGoogleApiClient.Connect();
      //  resolveSignInError();
      //}
      //if (!signInClicked && mGoogleApiClient.IsConnected)
      //{
      //  mGoogleApiClient.Disconnect();
      //}
      #endregion

      Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
      StartActivityForResult(signInIntent, RC_SIGN_IN);
    }

    private void resolveSignInError()
    {
      if (mGoogleApiClient.IsConnected)
      {
        return;
      }
      if (mConnectionResult.HasResolution)
      {
        try
        {
          intentInProgres = true;
          StartIntentSenderForResult(mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0);
        }
        catch (Android.Content.IntentSender.SendIntentException e)
        {
          intentInProgres = false;
          mGoogleApiClient.Connect();
        }
      }
    }
  }
}

