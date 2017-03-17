using DBEntityModel.DBModel;
using StudentAppHelper.ModelBindings.Models.CustomUserModel;
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
        var userfind = (from lk in conect.LoginKey
                    join lU in conect.Login on lk.LoginKeyId equals lU.LoginKeyId
                    where lk.LoginKey1 == loginKey
                              select new UserApp()
                    {
                      LoginKey = lk.LoginKey1,
                      IdUser = lU.UserId.Value
                    }).ToList();
        if (userfind.Count != 1)
          return null;
        return userfind[0];
      }
    }
  }
}
