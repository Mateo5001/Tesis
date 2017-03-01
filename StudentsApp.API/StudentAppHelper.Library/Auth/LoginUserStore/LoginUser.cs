using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentAppHelper.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAppHelper.Library.Auth
{
  public class LoginUser : IdentityUser,IUser<string> 
  {

    public string UserName { get; set;  }
    public UserRegistrationModel UserModel { get; set; }
    public string Id { get; set; }
  }
}