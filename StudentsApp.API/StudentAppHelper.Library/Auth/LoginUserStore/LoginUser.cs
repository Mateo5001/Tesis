using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentAppHelper.ModelBindings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAppHelper.Library.Auth
{
  public class LoginUser : IUser<string> 
  {
    public LoginUser()
    {

    }
    public LoginUser(UserRegistrationModel pUserModel)
    {
      UserName = pUserModel.UserNick;
      UserModel = pUserModel;
    }
    public string Id { get; set; }
    public string UserName { get; set;  }
    public UserRegistrationModel UserModel { get; set; }
  }
}