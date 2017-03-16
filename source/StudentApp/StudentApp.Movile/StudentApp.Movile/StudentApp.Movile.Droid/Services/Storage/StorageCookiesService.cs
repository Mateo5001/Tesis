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

    public void DelCookie(string cookie)
    {
      HttpCookie httpcookie = GetCookie(cookie);
      cm.CookieStore.Remove(new URI(@"http://studentapphelper-api-test.azurewebsites.net"), httpcookie);
    }

    private HttpCookie GetCookie(string cookie)
    {
      var h = cm.CookieStore.Get(new URI(@"http://studentapphelper-api-test.azurewebsites.net"));
      var httpcookie = h.Where(x => x.Name == cookie).DefaultIfEmpty().FirstOrDefault();
      return httpcookie;
    }

    public bool existsCookie(string name)
    {
      var existeCookie = GetCookieValue(name);
      return string.IsNullOrEmpty(existeCookie);
    }

    public string GetCookieValue(string cookie)
    {
      HttpCookie httpcookie = GetCookie(cookie);
      if (httpcookie != null)
        return httpcookie.Value;
      return string.Empty;
    }
    
    public void SetCookie(string cookie, string value)
    {
      HttpCookie httpcookie = GetCookie(cookie);
      httpcookie.Value = value;
      DelCookie(cookie);
      cm.CookieStore.Add(new URI(@"http://studentapphelper-api-test.azurewebsites.net"), httpcookie);
    }
  }
}
