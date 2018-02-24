using DBEntityModel.DBModel;
using StudentAppHelper.ModelBindings.Models;
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


    public void Guardar_Materia(int UserId , MatterModel matter)
    { 

      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var materia = new Matter();
        materia.MaterCode = matter.MaterCode;
        materia.UserId = UserId;
        materia.MatterName = matter.MatterName;
        materia.isActive = matter.IsActive;   
        materia.isEliminated = false;
        materia.CreateDate = DateTime.Now;
        conect.Matter.Add(materia);
        conect.SaveChanges();    
      }
      
    }
     
    
  }
}