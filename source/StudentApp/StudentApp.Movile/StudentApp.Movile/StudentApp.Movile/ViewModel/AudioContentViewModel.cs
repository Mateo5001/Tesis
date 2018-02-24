using Plugin.AudioRecorder;
using StudentApp.Movile.Util.CustomViewModel;
using StudentAppHelper.ModelBindings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudentApp.Movile.ViewModel
{
  public class AudioContentViewModel : CustomAppViewModel
  {
    private AudioRecorderService recorderService ;
    private string _anotationText;
    private int _IndexMatter;
    private int _IndexTopic;
    private List<string> _MatterList;
    private List<string> _TopicList;


    public string AnotationText { get => _anotationText; set { _anotationText = value; OnPropertyChanged(); } }
    public int IndexMatter { get => _IndexMatter; set { _IndexMatter = value; OnPropertyChanged(); } }
    public int IndexTopic { get => _IndexTopic; set { _IndexTopic = value; OnPropertyChanged(); } }
    public List<string> MatterList { get => _MatterList; set { _MatterList = value; OnPropertyChanged(); } }
    public List<string> TopicList { get => _TopicList; set { _TopicList = value; OnPropertyChanged(); } }

    public AudioContentViewModel() : base()
    {
      srcCMD = cmdSearch;
      cAbout = About;
      WriteCMD = cmdWrite;
      btnGuardar = btnGdef;
    }

    public override void recargarActions()
    {
      base.recargarActions();
      recorderService = new AudioRecorderService
      {
        StopRecordingAfterTimeout = true,
        TotalAudioTimeout = TimeSpan.FromSeconds(15),
        AudioSilenceTimeout = TimeSpan.FromSeconds(2)
        
      };

      //player = new AudioPlayer();
      //player.FinishedPlaying += Player_FinishedPlaying;
    }

    async Task RecordAudio()
    {
      try
      {
        if (!recorderService.IsRecording) //Record button clicked
        {
          recorderService.StopRecordingOnSilence = false;//TimeoutSwitch.IsToggled;
          AnotationText= recorderService.GetAudioFilePath();
          //RecordButton.IsEnabled = false;
          //PlayButton.IsEnabled = false;

          //start recording audio
          var audioRecordTask = await recorderService.StartRecording();

          //RecordButton.Text = "Stop Recording";
          //RecordButton.IsEnabled = true;

          var audio = await audioRecordTask;
          if(audio != null)
          {
            string s = recorderService.GetAudioFileStream().ToString();
            _files.guardar("wave.wav", s);
          }

          //RecordButton.Text = "Record";
          //PlayButton.IsEnabled = true;
        }
        else //Stop button clicked
        {
          //RecordButton.IsEnabled = false;

          //stop the recording...
          await recorderService.StopRecording();

          //RecordButton.IsEnabled = true;
        }
      }
      catch (Exception ex)
      {
        //blow up the app!
        throw ex;
      }
    }


    private async void llenarTemas()
    {
      _client.IsAuthenticated = true;
      intBinding indexMatter = new intBinding() { entero = 2 };
      var listatopic = await _client.CallAsync<intBinding, List<string>>("api/Account/topiList", indexMatter);
      TopicList = listatopic;
    }

    public ICommand WriteCMD { get; protected set; }

    public Command cmdWrite
    {
      get
      {
        return new Command(async () =>
        {
          await goWrite();
        });
      }
    }

    public ICommand cAbout { get; protected set; }

    public Command About
    {
      get
      {
        return new Command(async () =>
        {
          await VAbout();
        });
      }
    }

    private async Task VAbout()
    {
      _navigate.NavigateTo("About");

    }

    private async Task goWrite()
    {
      _navigate.NavigateTo("Write");
    }

    public ICommand srcCMD { get; protected set; }

    public Command cmdSearch
    {
      get
      {
        return new Command(async () =>
        {
          await goSeach();
        });
      }
    }

    private async Task goSeach()
    {
      _navigate.NavigateTo("Search");
    }

    public ICommand btnGuardar { get; protected set; }

    public Command btnGdef
    {
      get
      {
        return new Command(async () =>
        {
          await RecordAudio();
        });
      }
    }
    

  }
}
