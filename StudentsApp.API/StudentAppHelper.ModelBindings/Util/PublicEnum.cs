using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.ModelBindings.Util
{
  public enum TipoDocumento
  {
    Cedula = 1,
    TargetaIdentidad = 2,
    RegistroCivil = 3
  }
  public enum TipoUsuarios
  {
    Administrador = 1,
    CoAdministrador = 2
  }
  public enum TipoLogin
  {
    Custom=2,
    GoogleSignIn=3
  } 
}
