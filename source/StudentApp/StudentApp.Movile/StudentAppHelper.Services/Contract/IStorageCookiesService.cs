using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentAppHelper.Services.Contract
{
  public interface IStorageCookiesService
  {
    void AddCookie(String cookie, String value);
    string GetCookie(String cookie);
    void SetCookie(String cookie, String value);
    void DelCookie(String cookie, String value);
  }
}
