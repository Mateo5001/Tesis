using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace StudentsApp.API.Auth
{
  public class LoginOwnIdentity : IIdentity
  {
    private string _loginName;
    private bool _authValue;
    private string _role;
    public LoginOwnIdentity(string userName, string password)
    {
      if (IsValidNameAndPassword(userName, password))
      {
        _loginName = userName;
        _authValue = true;
        Role = "Admin";
      }
      else
      {
        _loginName = string.Empty;
        _authValue = false;
        Role = string.Empty;
      }
    }
    public string AuthenticationType
    {
      get
      {
        return "CustomAuth";
      }
    }

    public bool IsAuthenticated
    {
      get
      {
        return _authValue;
      }
    }

    public string Name
    {
      get
      {
        return _loginName;
      }
    }

    public string Role
    {
      get
      {
        return _role;
      }

      set
      {
        _role = value;
      }
    }

    private bool IsValidNameAndPassword(string username, string password)
    {
      string passwordStored = getPasswordStored(username);
      return passwordStored == password && !string.IsNullOrEmpty(passwordStored);
    }

    private string getPasswordStored(string username)
    {
      if (username.Equals("daniel"))
      {
        return "123";
      }
      return string.Empty;
    }
  }
}