using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using StudentAppHelper.ModelBindings.Models;
using StudentAppHelper.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.Services.HTTP
{
  public class HttpClientService : IHttpClientService
  {
    private IStorageCookiesService cookies;
    public string key;
    public bool IsAuthenticated { get; set; }

    public HttpClientService()
    {
      cookies = ServiceLocator.Current.GetInstance<IStorageCookiesService>();
      cargarKey();
    }

    private void cargarKey()
    {
      key = cookies.GetCookieValue("loginKey");
    }

    public TResponse Call<TSentType, TResponse>(string pPathCall, TSentType pObToSend)
    {
      try
      {
        using (var client = new HttpClient())
        {
          string data = string.Empty;
          client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");

          if (pObToSend.GetType() != typeof(ObjectNull))
          {
            data = JsonConvert.SerializeObject(pObToSend);
          };
          StringContent body = new StringContent(data, UTF8Encoding.UTF8, "application/json");
          if (IsAuthenticated)
          {
            body.Headers.Add("loginKey", key);
          }
          var response = client.PostAsync(pPathCall, body);
          response.Wait();
          if (response.IsCompleted)
          {
            var asyc = response.Result.Content.ReadAsStringAsync();
            asyc.Wait();
            data = asyc.Result;
          }
          return JsonConvert.DeserializeObject<TResponse>(data);
        }
      }
      catch (Exception e)
      {
        throw e;
        return default(TResponse);
      }
    }

    public async Task<TResponse> CallAsync<TSentType, TResponse>(string pPathCall, TSentType pObToSend)
    {
      try
      {
        using (var client = new HttpClient())
        {
          string data = string.Empty;
          //client.BaseAddress = new Uri(@"http://localhost:45778");
          client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");
          
          if(pObToSend.GetType() != typeof(ObjectNull))
          {
             data = JsonConvert.SerializeObject(pObToSend);
          }
          StringContent body = new StringContent(data, Encoding.UTF8, "application/json");
          if (IsAuthenticated)
          {
            cargarKey();
            body.Headers.Add("loginKey", key);
          }
          var response = await client.PostAsync(pPathCall, body);
          data = await response.Content.ReadAsStringAsync();
          try
          {
            var resp = JsonConvert.DeserializeObject<TResponse>(data);
            return resp;
          }
          catch (Exception e)
          {
            return default(TResponse);
          }
        }
      }
      catch (Exception e)
      {
        throw e;
        return default(TResponse);
      }
    }

    public void CallGet(string pPathCall)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");
        if (IsAuthenticated)
          client.DefaultRequestHeaders.Add("loginKey", key);
        var response = client.GetAsync(pPathCall);
        response.Wait();
      }
    }

    public TResponse CallGet<TResponse>(string pPathCall)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");
          if (IsAuthenticated)
            client.DefaultRequestHeaders.Add("loginKey", key);
          var response = client.GetAsync(pPathCall);
          response.Wait();
          if (response.IsCompleted)
          {
            var data = response.Result.Content.ReadAsStringAsync();
            data.Wait();
            if (data.IsCompleted)
            {
              return JsonConvert.DeserializeObject<TResponse>(data.Result);
            }
          }
        }
        return default(TResponse);
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public async Task CallGetAsync(string pPathCall)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");
        if (IsAuthenticated)
          client.DefaultRequestHeaders.Add("loginKey", key);
        var response = await client.GetAsync(pPathCall);

      }
    }

    public async Task<TResponse> CallGetAsync<TResponse>(string pPathCall)
    {
      try
      {
        using (var client = new HttpClient())
        {
          //client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");
          client.BaseAddress = new Uri(@"http://localhost:45778/");
          if (IsAuthenticated)
          {
            client.DefaultRequestHeaders.Add("loginKey", key);
          }
          var response = await client.GetStringAsync(pPathCall);
          return JsonConvert.DeserializeObject<TResponse>(response);
        }
      }
      catch (Exception e)
      {
        throw e;
      }
    }
  }
}
