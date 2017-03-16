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
    
    public bool IsAuthenticated { get; set; }

    public TResponse Call<TSentType, TResponse>(string pPathCall, TSentType pObToSend)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");
          if (IsAuthenticated)
            client.DefaultRequestHeaders.Add("loginkey", string.Empty);
          string data = JsonConvert.SerializeObject(pObToSend);
          StringContent body = new StringContent(data, UTF8Encoding.UTF8, "application/json");
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
          client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");
          if (IsAuthenticated)
            client.DefaultRequestHeaders.Add("loginkey", string.Empty);
          string data = JsonConvert.SerializeObject(pObToSend);
          StringContent body = new StringContent(data, UTF8Encoding.UTF8, "application/json");
          var response = await client.PostAsync(pPathCall, body);
          data = await response.Content.ReadAsStringAsync();
          return JsonConvert.DeserializeObject<TResponse>(data);
        }
      }
      catch (Exception e)
      {
        throw e;
        return default(TResponse);
      }
    }
  }
}
