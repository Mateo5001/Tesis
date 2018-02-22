using DBEntityModel.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.Library.AppLogic
{
  public class QueryLogic
  {
    public List<string> Consultar_Materia(int userId)
    {
  
      List<String> Lmaterias = new List<String>();
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      { 
         
        Lmaterias = (from Matter in conect.Matter
                     where Matter.UserId == userId
                     select Matter.MatterName
                     
                    ).ToList(); 
      }
      return Lmaterias;
    }
  }
}
