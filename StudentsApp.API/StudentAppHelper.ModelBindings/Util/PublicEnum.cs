using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.ModelBindings.Util
{
  public enum EDocumentType
  {
    Cedula = 1,
    TarjetaIdentidad = 2,
    RegistroCivil = 3
  }
  public enum EUserType
  {
    Administrador = 1,
    CoAdministrador = 2
  }
  public enum ELoginType
  {
    Custom=2,
    GoogleSignIn=3
  } 
}
