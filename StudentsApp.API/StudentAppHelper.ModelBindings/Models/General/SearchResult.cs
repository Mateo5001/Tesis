using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentAppHelper.ModelBindings.Models.General
{
  enum TipoContenido
  {
    Texto=1,
    Dibujo=2,
    Voz=3
  }
  public class SearchResult
  {
    private string _SaechName;
    private string _IdContent;
    private string _Icon;
    private TipoContenido _Tipo;
    private ICommand _NavegateCommand;

    public string SaechName { get => _SaechName; set => _SaechName = value; }
    public string IdContent { get => _IdContent; set => _IdContent = value; }
    public string Icon { get => _Icon; set => _Icon = value; }
    public ICommand NavegateCommand { get => _NavegateCommand; set => _NavegateCommand = value; }
    internal TipoContenido Tipo { get => _Tipo; set => _Tipo = value; }
  }
}
