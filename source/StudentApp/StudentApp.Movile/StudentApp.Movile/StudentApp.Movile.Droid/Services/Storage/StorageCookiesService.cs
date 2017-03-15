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
    #region MyRegion
    //public void HttpCookie(String cookie, String value)
    //{
    //  string name = " ";
    //  string value = 0;
    //  HttpCookieCollection MyCookieCollection = new HttpCookieCollection();        //creo coleecion de cookies   
    //  HttpCookie MyCookie = new HttpCookie("cookie");    // crea cookie nombre cookie     
    //  MyCookieCollection.Add(MyCookie);       // se agrega cookie a la coleccion
    //}

    //public HttpCookie Get(String cookie, String value)
    //{
    //  HttpCookieCollection MyCookieCollection = Request.Cookies;
    //  HttpCookie MyCookie = MyCookieCollection.Get("cookie");
    //  return cookie;
    //  return value;
    //}

    //public HttpCookie Get(String cookie, String value)
    //{
    //  HttpCookieCollection MyCookieCollection = Request.Cookies;
    //  HttpCookie MyCookie = MyCookieCollection.Get("cookie");
    //  return cookie;

    //}
    //public HttpCookie set(String cookie, String value)
    //{
    //  MyCookieCollection.Set(MyCookie);
    //}
    //public void Clear()

    //{

    //  MyCookieCollection.Clear("cookie");

    //}
    #endregion
    public void AddCookie(string cookie, string value)
    {
      HttpCookie MyCookie = new HttpCookie(cookie, value);
      CookieManager cm = new CookieManager();
      cm.CookieStore.Add(new URI(@"http://studentapphelper-api-test.azurewebsites.net"), MyCookie);
      
      string header = MyCookie.ToString();
    }

    public void DelCookie(string cookie, string value)
    {
      throw new NotImplementedException();
    }

    public string GetCookie(string cookie)
    {
      CookieManager cm =new CookieManager();
      var h =cm.CookieStore.Get(new URI(@"http://studentapphelper-api-test.azurewebsites.net"));
      HttpCookie coockie = h.Where(x => x.Name == cookie).DefaultIfEmpty().FirstOrDefault();
      return coockie.Value;
    }

    public void SetCookie(string cookie, string value)
    {
      throw new NotImplementedException();
    }
  }
}
