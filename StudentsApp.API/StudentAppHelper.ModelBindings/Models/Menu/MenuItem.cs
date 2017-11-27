using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentAppHelper.ModelBindings.Models.Menu
{
  public class CustomMenuItem
  {
    private string _MenuName;
    private string _MenuKey;
    private ICommand _NavegateCommand;

    public string MenuName { get => _MenuName; set => _MenuName = value; }
    public string MenuKey { get => _MenuKey; set => _MenuKey = value; }
    public ICommand NavegateCommand { get => _NavegateCommand; set => _NavegateCommand = value; }
  }
}
