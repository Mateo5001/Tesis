using StudentApp.Movile.Util.CustomViewModel;
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
      SearchListResult = new List<SearchResult>();
      
    }

    public string Filtro { get => _filtro; set { _filtro = value; Search(); ; OnPropertyChanged(); } }

    private void Search()
    {
      if (!string.IsNullOrEmpty(Filtro))
      {
        List<SearchResult> result = new List<SearchResult>();
        result.Add(new SearchResult()
        {
          Icon = "icon.png",
          IdContent = "1",
          SearchName = "registro 1",
          NavegateCommand = cmdNavegateList

        });
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
      set {
        _SearchListResult = value;
        OnPropertyChanged(); }
    }
  }
}
