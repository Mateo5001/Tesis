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
  public class WriteContentViewModel:CustomAppViewModel
  {
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

    private async void llenarTemas()
    {
      _client.IsAuthenticated = true;
      intBinding indexMatter = new intBinding() { entero = 2 };
      var listatopic = await _client.CallAsync<intBinding, List<string>>("api/Account/topiList", indexMatter);
      TopicList = listatopic;
    }

    public WriteContentViewModel() : base()
    {
      srcCMD = cmdSearch;
      AudioCMD = cmdAudio;
      cAbout = About;
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

    public ICommand AudioCMD { get; protected set; }

    public Command cmdAudio
    {
      get
      {
        return new Command(async () =>
        {
          await goAudio();
        });
      }
    }

    private async Task goAudio()
    {
      _navigate.NavigateTo("Audio");
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
          await guardar();
        });
      }
    }
    private async Task guardar()
    {

    }
  }
}
