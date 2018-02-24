using Plugin.AudioRecorder;
using StudentApp.Movile.Util.CustomViewModel;
using StudentAppHelper.ModelBindings.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using StudentAppHelper.Services.Contract;

namespace StudentApp.Movile.ViewModel
{
  public class AudioContentViewModel : CustomAppViewModel
  {
    private IAudioPlayer _audioPlayer;
    private Stopwatch _stopwatch = new Stopwatch();
    private AudioRecorderService recorderService;
    private string _anotationText;
    private int _IndexMatter;
    private int _IndexTopic;
    private List<string> _MatterList;
    private List<string> _TopicList;
    private string _TextButtonAudio;
    
    public string AnotationText { get => _anotationText; set { _anotationText = value; OnPropertyChanged(); } }
    public int IndexMatter { get => _IndexMatter; set { _IndexMatter = value; OnPropertyChanged(); } }
    public int IndexTopic { get => _IndexTopic; set { _IndexTopic = value; OnPropertyChanged(); } }
    public List<string> MatterList { get => _MatterList; set { _MatterList = value; OnPropertyChanged(); } }
    public List<string> TopicList { get => _TopicList; set { _TopicList = value; OnPropertyChanged(); } }

    public AudioContentViewModel() : base()
    {
      srcCMD = cmdSearch;
      btnPlay = cmdplay;
      cAbout = About;
      WriteCMD = cmdWrite;
      btnGuardar = btnGdef;
      _audioPlayer = GetInstance<IAudioPlayer>();
    }

    public override void recargarActions()
    {
      base.recargarActions();
      RecorderService = new AudioRecorderService
      {
        StopRecordingAfterTimeout = true,
        TotalAudioTimeout = TimeSpan.FromHours(1),
        AudioSilenceTimeout = TimeSpan.FromSeconds(2)
      };
      TextButtonAudio = "Grabar";
      AnotationText = Stopwatch.Elapsed.Seconds.ToString();
    }

    async Task RecordAudio()
    {
      try
      {
        if (!RecorderService.IsRecording) 
        {
          RecorderService.StopRecordingOnSilence = false;
          TextButtonAudio = "Detener";
          var audioRecordTask = await RecorderService.StartRecording();
          Stopwatch.Start();

          var audio = await audioRecordTask;
          if (audio != null)
          {
            Stopwatch.Stop();
            using (MemoryStream st = new MemoryStream())
            {
              RecorderService.GetAudioFileStream().CopyTo(st);
              _files.guardar("wave.wav", st.ToArray());
              TextButtonAudio = "Grabar";
            }
          }
        }
        else 
        {
          TextButtonAudio = "Grabar";
          await RecorderService.StopRecording();
        }
      }
      catch (Exception ex)
      {
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

    public ICommand btnPlay { get; protected set; }

    public Command cmdplay
    {
      get
      {
        return new Command(async () =>
        {
          await play();
        });
      }
    }

    private async Task play()
    {
      _audioPlayer.Play("/sdcard/Music/wave.wav");
    }


    public string TextButtonAudio { get => _TextButtonAudio; set { _TextButtonAudio = value; OnPropertyChanged(); } }

    public AudioRecorderService RecorderService { get => recorderService; set => recorderService = value; }
    public Stopwatch Stopwatch { get => _stopwatch; set => _stopwatch = value; }
  }
}
