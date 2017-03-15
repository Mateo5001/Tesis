using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.Services.Storage
{
  class StorageCookiesService
  {
    public void HttpCookie(String cookie, String value)
    {
      string name = " ";
      string value = 0;
      HttpCookieCollection MyCookieCollection = new HttpCookieCollection();        //creo coleecion de cookies   
      HttpCookie MyCookie = new HttpCookie("cookie");    // crea cookie nombre cookie     
      MyCookieCollection.Add(MyCookie);       // se agrega cookie a la coleccion
    }

    public   HttpCookie Get(String cookie, String value)
    {
      HttpCookieCollection MyCookieCollection = Request.Cookies;
      HttpCookie MyCookie = MyCookieCollection.Get("cookie");
      return cookie;
      return value;
    }

    public HttpCookie Get(String cookie, String value)
    {
      HttpCookieCollection MyCookieCollection = Request.Cookies;
      HttpCookie MyCookie = MyCookieCollection.Get("cookie");
      return cookie;

    }
    public HttpCookie set(String cookie, String value)
    {
      MyCookieCollection.Set(MyCookie);
    }
    public void Clear()

    {

      MyCookieCollection.Clear("cookie");

    }
  }
}
