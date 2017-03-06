﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAppHelper.Library.Models.CustomUserModel
{
  public class UserApp
  {
    private string _LoginKey;
    private int _idUser;

    public string LoginKey
    {
      get
      {
        return _LoginKey;
      }

      set
      {
        _LoginKey = value;
      }
    }

    public int IdUser
    {
      get
      {
        return _idUser;
      }

      set
      {
        _idUser = value;
      }
    }
  }
}