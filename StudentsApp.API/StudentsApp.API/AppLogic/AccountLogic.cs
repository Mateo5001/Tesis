using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBEntityModel.DBModel;
using StudentsApp.API.Models;
using StudentAppHelper.Library.Util;

namespace StudentsApp.API.AppLogic
{
  public class AccountLogic
  {
    

    internal bool Registrar(UserRegistrationModel model)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var Usuario = (from US in conect.Usurio where US.DocumentoUsuario==model.Identificacion && US.TipoDocumento== (int)model.TipoDocumento select US ).FirstOrDefault();
        if (Usuario == null)
        {
          Usuario = new Usurio();
          Usuario.NombrePrimero = model.NombreP;
          Usuario.NombreSegundo = model.NombreS;
          Usuario.ApellidoPrimer = model.ApellidoP;
          Usuario.ApellidoSegundo = model.ApellidoS;
          Usuario.TipoDocumento = (int)model.TipoDocumento;
          Usuario.DocumentoUsuario = model.Identificacion;
          Usuario.TipoUsuario = (int)TipoUsuarios.Administrador;
          Usuario.FechaNacimiento = null;
          conect.Usurio.Add(Usuario);
          conect.SaveChanges();
        }

        var LoginRej = (from LK in conect.LoginKey where LK.loginUser == model.UserName select LK).FirstOrDefault();
        if (LoginRej == null)
        {
          LoginRej = new LoginKey();
          LoginRej.loginUser = model.UserName;
          LoginRej.loginPass = model.Password;
          LoginRej.loginKey1 = Guid.NewGuid().ToString();
          LoginRej.fechaAcceso = null;
          conect.LoginKey.Add(LoginRej);
          conect.SaveChanges();

          Logueo UserLogeo = new Logueo();
          UserLogeo.IdUsuario = Usuario.IdUsuario;
          UserLogeo.LoginKeyID = LoginRej.LoginKeyID;
          UserLogeo.TipoLoginID = (int)TipoLogin.Custom;
          conect.Logueo.Add(UserLogeo);
          conect.SaveChanges();
        }
        else
        {
          return false;
        }
      }
      return true;
    }
  }
}