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
  public class MatterViewModel:CustomAppViewModel
  {
    private string _nomMateria;
    private string _codMateria;
    private int _indexEstado;

    public bool estado { get { return IndexEstado == 0; } }

    public MatterViewModel() : base()
    {
      cmdCrearMat = cmdCrearMateria;
    }

    public override void recargarActions()
    {
      base.recargarActions();
      limpiarViewModel();
    }

    public void limpiarViewModel()
    {
      NomMateria = string.Empty;
      CodMateria = string.Empty;
      IndexEstado = 0;
    }

    public ICommand cmdCrearMat { get; protected set; }

    public Command cmdCrearMateria
    {
      get
      {
        return new Command(async () =>
        {
          await crearMateria();
        });
      }
    }

    public string NomMateria { get => _nomMateria; set { _nomMateria = value; OnPropertyChanged(); } }
    public string CodMateria { get => _codMateria; set { _codMateria = value; OnPropertyChanged(); } }
    public int IndexEstado { get => _indexEstado; set { _indexEstado = value; OnPropertyChanged(); } }

    private async Task crearMateria()
    {
      MatterModel matter = new MatterModel();
      matter.IsActive = IndexEstado == 0;
      matter.MaterCode = CodMateria;
      matter.MatterName = NomMateria;

      _client.IsAuthenticated = true;
       await _client.CallAsync<MatterModel, List<string>>("api/Matter/crearMateria", matter);
      _navigate.NavigateTo("Main");
    }
  }
}
