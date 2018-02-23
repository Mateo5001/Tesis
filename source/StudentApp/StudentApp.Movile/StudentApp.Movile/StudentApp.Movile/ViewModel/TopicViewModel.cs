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
  public class TopicViewModel : CustomAppViewModel
  {
    private int _indexMatter;
    private string _codTopic;
    private string _nomTopic;
    private int _indexEstado;
    private List<string> _matterList;
    public TopicViewModel() : base()
    {
      //List<string> listaMat = new List<string>();
      //listaMat.Add("Matematicas");
      //MatterList = listaMat;
      cmdCrearTopic = CmdCrearTopic;
    }

    public override void recargarActions()
    {
      base.recargarActions();
      limpiarDatos();
    }

    private void limpiarDatos()
    {
      MatterList = new List<string>();
      IndexEstado = 1;
      IndexMatter = 0;
      CodTopic = string.Empty;
      NomTopic = string.Empty;
    }

    public List<string> MatterList { get { return _matterList; } set { _matterList = value; OnPropertyChanged(); } }

    public ICommand cmdCrearTopic { get; protected set; }

    public Command CmdCrearTopic
    {
      get
      {
        return new Command(async () =>
        {
          await CrearTopic();
        });
      }
    }

    public int IndexMatter { get => _indexMatter; set { _indexMatter = value; OnPropertyChanged(); } }
    public string NomTopic { get => _nomTopic; set { _nomTopic = value; OnPropertyChanged(); } }
    public int IndexEstado { get => _indexEstado; set { _indexEstado = value; OnPropertyChanged(); } }
    public string CodTopic { get => _codTopic; set { _codTopic = value; OnPropertyChanged(); } }

    private async Task CrearTopic()
    {
      TopicModel top = new TopicModel();
      top.MatterIndex = IndexMatter;
      top.TopicCode = CodTopic;
      top.TopicName = NomTopic;
      top.isActive = IndexEstado == 1;
      _client.IsAuthenticated = true;
      var topicCreate = await _client.CallAsync<TopicModel, bool >("api/Topic/createTopic", top );
      if(topicCreate)
      {
        _navigate.NavigateTo("Main");
      }
    }
  }
}
