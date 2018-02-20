using StudentApp.Movile.Views.MenuPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StudentApp.Movile.Views.MasterPage
{
  public partial class Master : MasterDetailPage
  {
    public Master()
    {
      //InitializeComponent();
      Master = new MenuAccountPage();
      Detail = new NavigationPage();
      //NavigationPage.SetHasNavigationBar(this, false);
    }
  }
}
