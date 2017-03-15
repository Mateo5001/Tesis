using Java.Net;
using StudentApp.Movile.Droid.Services.Storage;
using StudentAppHelper.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(StorageCookiesService))]
namespace StudentApp.Movile.Droid.Services.Storage
{
  class StorageCookiesService : IStorageCookiesService
  {
    CookieManager cm = new CookieManager();
    public void AddCookie(string cookie, string value)
    {
      HttpCookie MyCookie = new HttpCookie(cookie, value);
      cm.CookieStore.Add(new URI(@"http://studentapphelper-api-test.azurewebsites.net"), MyCookie);
      string header = MyCookie.ToString();
    }

    public void DelCookie(string cookie, string value)
    {
      throw new NotImplementedException();
    }

    public bool existsCookie(string name)
    {
      var existeCookie = GetCookie(name);
      return string.IsNullOrEmpty(existeCookie);
    }

    public string GetCookie(string cookie)
    {
      var h = cm.CookieStore.Get(new URI(@"http://studentapphelper-api-test.azurewebsites.net"));
      var httpcookie = h.Where(x => x.Name == cookie).DefaultIfEmpty().FirstOrDefault();
      if (httpcookie != null)
        return httpcookie.Value;
      return string.Empty;
    }

    public void SetCookie(string cookie, string value)
    {
      throw new NotImplementedException();
    }
  }
}
