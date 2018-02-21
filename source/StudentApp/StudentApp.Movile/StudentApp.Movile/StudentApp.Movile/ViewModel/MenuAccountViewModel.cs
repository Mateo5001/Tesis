using StudentApp.Movile.Util.CustomViewModel;
using StudentAppHelper.ModelBindings.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudentApp.Movile.ViewModel
{
  public class MenuAccountViewModel: CustomAppViewModel
  {
    private List<CustomMenuItem> menuItemList;
    public MenuAccountViewModel():base()
    {
      List<CustomMenuItem>  lit =  new List<CustomMenuItem>();
      CustomMenuItem item = new CustomMenuItem()
      {
        MenuName = "Materia",
        MenuKey = "Matter",
        NavegateCommand = navegateCmd,
      };
      lit.Add(item);
      item = new CustomMenuItem()
      {
        MenuName = "Tema",
        MenuKey = "Topic",
        NavegateCommand = navegateCmd,
      };
      lit.Add(item); item = new CustomMenuItem()
      {
        MenuName = "Acerca de ...",
        MenuKey = "About",
        NavegateCommand = navegateCmd,
      };
      lit.Add(item);
      MenuItemList = lit;

      navegate = navegateCmd;
    }

    public ICommand navegate { get; protected set; }

    public Command<string> navegateCmd 
    {
      get
      {
        return new Command<string>(async (pKey) =>
        {
          await NavegateMatter(pKey);
        });
      }
    }

    public List<CustomMenuItem> MenuItemList
    {
      get
      {
        return menuItemList;
      }
      set
      {
        menuItemList = value;
        OnPropertyChanged();
      }
    }

    private async Task NavegateMatter(string _pKey)
    {
      _navigate.NavigateTo(_pKey);
    }
  }
}
