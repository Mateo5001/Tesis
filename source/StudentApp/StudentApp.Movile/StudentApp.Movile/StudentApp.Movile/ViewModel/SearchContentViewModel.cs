using StudentApp.Movile.Util.CustomViewModel;
using StudentAppHelper.ModelBindings.Models;
using StudentAppHelper.ModelBindings.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudentApp.Movile.ViewModel
{
  public class SearchContentViewModel : CustomAppViewModel
  {
    private List<SearchResult> _SearchListResult;
    private string _filtro;
    public SearchContentViewModel() :base()
    {
      Filtro = string.Empty;
    }

    public override void recargarActions()
    {
      base.recargarActions();
      Filtro = string.Empty;
      SearchListResult = new List<SearchResult>();
    }

    public string Filtro { get => _filtro; set { _filtro = value; Search(); ; OnPropertyChanged(); } }

    private async void Search()
    {
      if (!string.IsNullOrEmpty(Filtro) && Filtro.Length > 3)
      {
        _client.IsAuthenticated = true;
        List<SearchResult> result = await _client.CallAsync<SearchTextModel, List<SearchResult>>("api/Content/busqueda", new SearchTextModel() { filtro = Filtro });
         SearchListResult = result;
      }
    }
    
    public ICommand cmdNavegateList { get; protected set; }

    public Command cmdNL
    {
      get
      {
        return new Command<string>(async (pKey) =>
        {
          await methodNavegate(pKey);
        });
      }
    }

    private async Task methodNavegate(string pKey)
    {

    }

    public List<SearchResult> SearchListResult { get => _SearchListResult;
      set
      { 
        _SearchListResult = value;
        OnPropertyChanged();
      }
    }
  }
}
