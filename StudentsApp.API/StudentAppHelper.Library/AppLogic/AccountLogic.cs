using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBEntityModel.DBModel;
using StudentAppHelper.Library.Util;
using StudentAppHelper.Library.Models;
using StudentAppHelper.Library.Auth;

namespace StudentAppHelper.Library.AppLogic
{
  public class AccountLogic
  {


    public LoginUser Registrar(UserRegistrationModel model)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var Usuario = (from US in conect.Usurio where US.DocumentoUsuario == model.Identificacion && US.TipoDocumento == (int)model.TipoDocumento select US).FirstOrDefault();
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
          LoginUser user = new LoginUser()
          {
            UserName = LoginRej.loginUser,
            UserModel = model
          };
          return user;
        }
      }
      return null;
    }

    public string getPasswordHash(string id)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var login = (from Keys in conect.LoginKey
                     join KeyUsers in conect.Logueo on Keys.LoginKeyID equals KeyUsers.LoginKeyID
                     join users in conect.Usurio on KeyUsers.IdUsuario equals users.IdUsuario
                     where Keys.loginKey1 == id
                     select Keys.loginPass).FirstOrDefault();
        if (login != null)
          return login;
      }
      return null;
    }


    public LoginUser GetUserById(string userId)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var login = (from Keys in conect.LoginKey
                     join KeyUsers in conect.Logueo on Keys.LoginKeyID equals KeyUsers.LoginKeyID
                     join users in conect.Usurio on KeyUsers.IdUsuario equals users.IdUsuario
                     where Keys.loginKey1 == userId
                     select new LoginUser()
                     {
                       Id = Keys.loginKey1,
                       UserName = Keys.loginUser,
                       UserModel = new UserRegistrationModel()
                       {
                         ApellidoP = users.ApellidoPrimer,
                         ApellidoS = users.ApellidoSegundo,
                         NombreP = users.NombrePrimero,
                         NombreS = users.NombreSegundo,
                         UserName = Keys.loginUser,
                         Password = Keys.loginPass,
                         ConfirmPassword = Keys.loginPass,
                         Identificacion = users.DocumentoUsuario,
                         TipoDocumento = (TipoDocumento)users.TipoDocumento
                       }
                     }).FirstOrDefault();
        if (login != null)
          return login;
      }
      return null;
    }

    public List<LoginUser> FindByName(string userName)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var login = (from Keys in conect.LoginKey
                     join KeyUsers in conect.Logueo on Keys.LoginKeyID equals KeyUsers.LoginKeyID
                     join users in conect.Usurio on KeyUsers.IdUsuario equals users.IdUsuario
                     where Keys.loginUser == userName
                     select new LoginUser()
                     {
                       Id = Keys.loginKey1,
                       UserName = Keys.loginUser,
                       UserModel = new UserRegistrationModel()
                       {
                         ApellidoP = users.ApellidoPrimer,
                         ApellidoS = users.ApellidoSegundo,
                         NombreP = users.NombrePrimero,
                         NombreS = users.NombreSegundo,
                         UserName = Keys.loginUser,
                         Password = Keys.loginPass,
                         ConfirmPassword = Keys.loginPass,
                         Identificacion = users.DocumentoUsuario,
                         TipoDocumento = (TipoDocumento)users.TipoDocumento
                       }
                     }).ToList();
        if (login != null)
          return login;
      }
      return null;
    }
  }
}