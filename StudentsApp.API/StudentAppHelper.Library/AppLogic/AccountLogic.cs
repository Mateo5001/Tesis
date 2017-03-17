using System;
using System.Collections.Generic;
using System.Linq;
using DBEntityModel.DBModel;
using StudentAppHelper.Library.Auth;
using StudentAppHelper.ModelBindings.Models;
using StudentAppHelper.ModelBindings.Util;

namespace StudentAppHelper.Library.AppLogic
{
  public class AccountLogic
  {


    public LoginUser Registrar(UserRegistrationModel model)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var findUser = (from US in conect.User
                        where US.UserDocumentNumber == model.DocumentNumber && US.UserDocuemntTypeId == (int)model.DocumentType
                        select US).FirstOrDefault();
        if (findUser == null)
        {
          return CreateUser(model, EUserType.Administrador);
        }
        else
        {
          return FindByName(model.UserNick).FirstOrDefault();
        }
      }
    }

    private static LoginUser CreateUser(UserRegistrationModel model,EUserType UserType)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var CreatedUser = new LoginUser();
        var newUser = new User();
        newUser.UserFirstName = model.FirstName;
        newUser.UserSecondName = model.SecondName;
        newUser.UserFirsLastName = model.FirstLastName;
        newUser.UserSecondLastName = model.SecondLastName;
        newUser.UserDocuemntTypeId = (int)model.DocumentType;
        newUser.UserDocumentNumber = model.DocumentNumber;
        newUser.UserType = (int)UserType;
        newUser.UserBirthDate = null;
        conect.User.Add(newUser);
        conect.SaveChanges();

        var findLoginKey = (from LK in conect.LoginKey
                            where LK.LoginNick == model.UserNick
                            select LK).FirstOrDefault();
        if (findLoginKey == null)
        {
          var newLoginKey = new LoginKey();
          newLoginKey.LoginNick = model.UserNick;
          newLoginKey.LoginPass = model.Password;
          newLoginKey.LoginKey1 = Guid.NewGuid().ToString();
          newLoginKey.AccessDate = null;
          conect.LoginKey.Add(newLoginKey);
          conect.SaveChanges();

          Login newLogin = new Login();
          newLogin.UserId = newUser.UserId;
          newLogin.LoginKeyId = newLoginKey.LoginKeyId;
          newLogin.LoginTypeId = (int)ELoginType.Custom;
          conect.Login.Add(newLogin);
          conect.SaveChanges();


          CreatedUser.UserName = newLoginKey.LoginNick;
          CreatedUser.UserModel = model;
        }
        return CreatedUser;
      }
    }

    public string getPasswordHash(string id)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var login = (from Keys in conect.LoginKey
                     join KeyUsers in conect.Login on Keys.LoginKeyId equals KeyUsers.LoginKeyId
                     join users in conect.User on KeyUsers.UserId equals users.UserId
                     where Keys.LoginKey1 == id
                     select Keys.LoginPass).FirstOrDefault();
        if (login != null)
          return login;
      }
      return null;
    }

    // userId hace referencia a el Key de grid creado
    public List<LoginUser> GetUserById(string userId)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var login = (from Keys in conect.LoginKey
                     join KeyUsers in conect.Login on Keys.LoginKeyId equals KeyUsers.LoginKeyId
                     join users in conect.User on KeyUsers.UserId equals users.UserId
                     where Keys.LoginKey1 == userId
                     select new LoginUser()
                     {
                       Id = Keys.LoginKey1,
                       UserName = Keys.LoginNick,
                       UserModel = new UserRegistrationModel()
                       {
                         FirstName = users.UserFirstName,
                         SecondName = users.UserSecondName,
                         FirstLastName = users.UserFirsLastName,
                         SecondLastName = users.UserSecondLastName,
                         UserNick = Keys.LoginNick,
                         Password = Keys.LoginPass,
                         ConfirmationPassword = Keys.LoginPass,
                         DocumentNumber = users.UserDocumentNumber,
                         DocumentType = (EDocumentType)users.UserDocuemntTypeId
                       }
                     }).ToList();
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
                     join KeyUsers in conect.Login on Keys.LoginKeyId equals KeyUsers.LoginKeyId
                     join users in conect.User on KeyUsers.UserId equals users.UserId
                     where Keys.LoginNick == userName
                     select new LoginUser()
                     {
                       Id = Keys.LoginKey1,
                       UserName = Keys.LoginNick,
                       UserModel = new UserRegistrationModel()
                       {
                         FirstName = users.UserFirstName,
                         SecondName = users.UserSecondName,
                         FirstLastName = users.UserFirsLastName,
                         SecondLastName = users.UserSecondLastName,
                         UserNick = Keys.LoginNick,
                         Password = Keys.LoginPass,
                         ConfirmationPassword = Keys.LoginPass,
                         DocumentNumber = users.UserDocumentNumber,
                         DocumentType = (EDocumentType)users.UserDocuemntTypeId
                       }
                     }).ToList();
        if (login != null)
          return login;
      }
      return null;
    }
  }
}