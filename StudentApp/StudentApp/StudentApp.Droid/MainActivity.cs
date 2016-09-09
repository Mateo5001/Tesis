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
using static Android.Gms.Common.Apis.GoogleApiClient;

namespace StudentApp.Droid
{
  [Activity(Label = "StudentApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IConnectionCallbacks, IOnConnectionFailedListener
  {

    GoogleApiClient mGoogleApiClient;
    private global::Xamarin.Forms.Button btnGoogleSingIn;
    private bool intentInProgres;
    private bool signInClicked;
    private ConnectionResult mConnectionResult;

    public void OnConnected(Bundle connectionHint)
    {
      signInClicked = true;

      IPeople pe = PlusClass.PeopleApi;
      IPerson persona = pe.GetCurrentPerson(mGoogleApiClient);

      if(persona!=null)
      {
        btnGoogleSingIn.Text = persona.DisplayName;
        LoadApplication(new App());
      }
        
        
        //PeopleApi.GetCurrentPerson(mGoogleApiClient);
      
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



      //var person = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);

      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      base.OnCreate(bundle);

      global::Xamarin.Forms.Forms.Init(this, bundle);
      LoadApplication(new App(ref btnGoogleSingIn));
      btnGoogleSingIn.Clicked += btnGoogleSingIn_Click;

      mGoogleApiClient = new GoogleApiClient.Builder(this)
       .AddConnectionCallbacks(this)
       .AddOnConnectionFailedListener(this)
       .AddApi(PlusClass.API)
       .AddScope(PlusClass.ScopePlusProfile)
       .AddScope(PlusClass.ScopePlusLogin)
       .Build();
    }


    protected override void OnStart()
    {
      base.OnStart();
      mGoogleApiClient.Connect();
      btnGoogleSingIn.Text = "Click to Conect...";
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
      if (requestCode == 0)
      {
        if (resultCode != Result.Ok)
        {
          signInClicked = false;

        }
        intentInProgres = false;
        if (!mGoogleApiClient.IsConnecting)
        {
          mGoogleApiClient.Connect();
        }
      }
    }

    private void btnGoogleSingIn_Click(object sender, EventArgs e)
    {
      btnGoogleSingIn.Text = "Conectado";
      if (!mGoogleApiClient.IsConnecting)
      {
        signInClicked = true;
        resolveSignInError();
      }
      if (!signInClicked && mGoogleApiClient.IsConnected)
      {
        mGoogleApiClient.Disconnect();
      }
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

