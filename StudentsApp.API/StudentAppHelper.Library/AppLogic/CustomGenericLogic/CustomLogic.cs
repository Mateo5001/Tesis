using DBEntityModel.DBModel;
using StudentAppHelper.Library.Models.CustomUserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.Library.AppLogic.CustomGenericLogic
{
  public class CustomLogic
  {
    public UserApp getUserByLoginKey(string loginKey )
    {
      UserApp user;
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var userEncontrado = (from lk in conect.LoginKey
                    join lU in conect.Logueo on lk.LoginKeyID equals lU.LoginKeyID
                    select new UserApp()
                    {
                      LoginKey = lk.loginKey1,
                      IdUser = lU.IdUsuario.Value
                    }).FirstOrDefault();
        
        user = userEncontrado;
      }


      return user;
    }
  }
}
