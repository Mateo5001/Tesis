using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using Android.Text;

namespace StudentApp.Movile.Droid.Services.Droid
{

  public class CookiesStore : ICookieStore
  {

    private static String LOG_TAG = "PersistentCookieStore";
    private static  String COOKIE_PREFS = "CookiePrefsFile";
    private static  String COOKIE_NAME_STORE = "names";
    private static  String COOKIE_NAME_PREFIX = "cookie_";
    private  IList<HttpCookie> cookies;
    private  ISharedPreferences cookiePrefs;
    private bool omitNonPersistentCookies = false;

    /**
     * Construct a persistent cookie store.
     *
     * @param context Context to attach cookie store to
     */
    public CookiesStore(Context context)
    {
      cookiePrefs = context.GetSharedPreferences(COOKIE_PREFS, 0);
      cookies = new List<HttpCookie>();

      // Load any previously stored cookies into the store
      string storedCookieNames = cookiePrefs.GetString(COOKIE_NAME_STORE, null);
      if (storedCookieNames != null)
      {
        string[] cookieNames = TextUtils.Split(storedCookieNames, ",");
        foreach (string name in  cookieNames)
        {
          string encodedCookie = cookiePrefs.GetString(COOKIE_NAME_PREFIX + name, null);
          if (encodedCookie != null)
          {
            string decodedCookie = decodeCookie(encodedCookie);
            if (decodedCookie != null)
            {
              cookies.Add(new HttpCookie(name, decodedCookie));
            }
          }
        }

        // Clear out expired cookies
        //clearExpired(new DateTime());
      }
    }
    
    public void Add(URI uri ,HttpCookie cookie)
    {
      string name = cookie.Name + uri.Host;

      // Save cookie into local store, or remove if expired
      if (!cookie.HasExpired)
      {
        cookies.Add(cookie);
      }
      else
      {
        cookies.Remove(cookie);
      }

      // Save cookie into persistent store
      ISharedPreferencesEditor prefsWriter = cookiePrefs.Edit();
      prefsWriter.PutString(COOKIE_NAME_STORE, TextUtils.Join(",", cookies.ToArray()));
      prefsWriter.PutString(COOKIE_NAME_PREFIX + name, encodeCookie(new SerializableCookie(cookie)));
      prefsWriter.Commit();
    }
    
    public void Clear()
    {
      // Clear cookies from persistent store
      ISharedPreferencesEditor prefsWriter = cookiePrefs.Edit();
      foreach (string name in  cookies)
      {
        prefsWriter.Remove(COOKIE_NAME_PREFIX + name);
      }
      prefsWriter.Remove(COOKIE_NAME_STORE);
      prefsWriter.Commit();

      // Clear cookies from local store
      cookies.Clear();
    }

    @Override
    public boolean clearExpired(Date date)
    {
      boolean clearedAny = false;
      SharedPreferences.Editor prefsWriter = cookiePrefs.edit();

      for (ConcurrentHashMap.Entry<String, Cookie> entry : cookies.entrySet())
      {
        String name = entry.getKey();
        Cookie cookie = entry.getValue();
        if (cookie.isExpired(date))
        {
          // Clear cookies from local store
          cookies.remove(name);

          // Clear cookies from persistent store
          prefsWriter.remove(COOKIE_NAME_PREFIX + name);

          // We've cleared at least one
          clearedAny = true;
        }
      }

      // Update names in persistent store
      if (clearedAny)
      {
        prefsWriter.putString(COOKIE_NAME_STORE, TextUtils.join(",", cookies.keySet()));
      }
      prefsWriter.commit();

      return clearedAny;
    }

    @Override
    public List<Cookie> getCookies()
    {
      return new ArrayList<Cookie>(cookies.values());
    }

    /**
     * Will make PersistentCookieStore instance ignore Cookies, which are non-persistent by
     * signature (`Cookie.isPersistent`)
     *
     * @param omitNonPersistentCookies true if non-persistent cookies should be omited
     */
    

    /**
     * Non-standard helper method, to delete cookie
     *
     * @param cookie cookie to be removed
     */
    public void deleteCookie(Cookie cookie)
    {
      String name = cookie.getName() + cookie.getDomain();
      cookies.remove(name);
      SharedPreferences.Editor prefsWriter = cookiePrefs.edit();
      prefsWriter.remove(COOKIE_NAME_PREFIX + name);
      prefsWriter.commit();
    }

    /**
     * Serializes Cookie object into String
     *
     * @param cookie cookie to be encoded, can be null
     * @return cookie encoded as String
     */
    protected String encodeCookie(SerializableCookie cookie)
    {
      if (cookie == null)
        return null;
      ByteArrayOutputStream os = new ByteArrayOutputStream();
      try
      {
        ObjectOutputStream outputStream = new ObjectOutputStream(os);
        outputStream.writeObject(cookie);
      }
      catch (IOException e)
      {
        AsyncHttpClient.log.d(LOG_TAG, "IOException in encodeCookie", e);
        return null;
      }

      return byteArrayToHexString(os.toByteArray());
    }

    /**
     * Returns cookie decoded from cookie string
     *
     * @param cookieString string of cookie as returned from http request
     * @return decoded cookie or null if exception occured
     */
    protected string decodeCookie(string cookieString)
    {
      byte[] bytes = hexStringToByteArray(cookieString);
      ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream(bytes);
      Cookie cookie = null;
      try
      {
        ObjectInputStream objectInputStream = new ObjectInputStream(byteArrayInputStream);
        cookie = ((SerializableCookie)objectInputStream.readObject()).getCookie();
      }
      catch (IOException e)
      {
        AsyncHttpClient.log.d(LOG_TAG, "IOException in decodeCookie", e);
      }
      catch (ClassNotFoundException e)
      {
        AsyncHttpClient.log.d(LOG_TAG, "ClassNotFoundException in decodeCookie", e);
      }

      return cookie;
    }

    /**
     * Using some super basic byte array &lt;-&gt; hex conversions so we don't have to rely on any
     * large Base64 libraries. Can be overridden if you like!
     *
     * @param bytes byte array to be converted
     * @return string containing hex values
     */
    protected String byteArrayToHexString(byte[] bytes)
    {
      StringBuilder sb = new StringBuilder(bytes.length * 2);
      for (byte element : bytes)
      {
        int v = element & 0xff;
        if (v < 16)
        {
          sb.append('0');
        }
        sb.append(Integer.toHexString(v));
      }
      return sb.toString().toUpperCase(Locale.US);
    }

    /**
     * Converts hex values from strings to byte arra
     *
     * @param hexString string of hex-encoded values
     * @return decoded byte array
     */
    protected byte[] hexStringToByteArray(String hexString)
    {
      int len = hexString.length();
      byte[] data = new byte[len / 2];
      for (int i = 0; i < len; i += 2)
      {
        data[i / 2] = (byte)((Character.digit(hexString.charAt(i), 16) << 4) + Character.digit(hexString.charAt(i + 1), 16));
      }
      return data;
    }





    public IList<HttpCookie> Cookies
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public IntPtr Handle
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public IList<URI> URIs
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public bool OmitNonPersistentCookies
    {
      get
      {
        return omitNonPersistentCookies;
      }

      set
      {
        this.omitNonPersistentCookies = value;
      }
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public IList<HttpCookie> Get(URI uri)
    {
      throw new NotImplementedException();
    }

    public bool Remove(URI uri, HttpCookie cookie)
    {
      throw new NotImplementedException();
    }

    public bool RemoveAll()
    {
      throw new NotImplementedException();
    }
  }
}