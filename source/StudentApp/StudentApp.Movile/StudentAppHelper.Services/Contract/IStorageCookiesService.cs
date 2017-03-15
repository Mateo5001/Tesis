﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentAppHelper.Services.Contract
{
  public interface IStorageCookiesService
  {
    void AddCookie(string cookie, string value);
    string GetCookie(string cookie);
    void SetCookie(string cookie, string value);
    void DelCookie(string cookie, string value);

    bool existsCookie(string name);
  }
}
