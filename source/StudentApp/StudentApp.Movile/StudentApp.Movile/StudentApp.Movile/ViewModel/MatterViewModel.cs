using StudentApp.Movile.Util.CustomViewModel;
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
      //llamado al servicio que guarda
      _navigate.NavigateTo("Main");

    }
  }
}
