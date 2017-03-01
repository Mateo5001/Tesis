using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace StudentAppHelper.Library.Auth
{
  public class AuthPrincipal : IPrincipal
  {
    private LoginOwnIdentity _identity;

    public AuthPrincipal(string name, string password)
    {
      _identity = new LoginOwnIdentity(name, password);
    }
    public IIdentity Identity
    {
      get
      {
        return _identity;
      }
    }

    public bool IsInRole(string role)
    {
      return role == _identity.Role;
    }
  }
}