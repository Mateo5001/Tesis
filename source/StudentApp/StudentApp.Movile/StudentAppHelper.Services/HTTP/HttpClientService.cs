using Newtonsoft.Json;
using StudentAppHelper.ModelBindings.Models;
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
  public class HttpClientService
  {
    static HttpClient _client = new HttpClient();
    private bool wihtAuthentication;

    public bool WithAtetntication
    {
      get { return wihtAuthentication; }
      set { wihtAuthentication = value; }
    }

    public HttpClientService()
    {
      _client.BaseAddress = new Uri(@"http://localhost:45778/");
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    }

    public async Task<TResponse> Call<TSentType, TResponse>(string pathCall, TSentType obToSend)
    {
      if (WithAtetntication)
        return await callServiceWithAuthentication<TSentType, TResponse>(pathCall, obToSend);
      else
        return await callServiceWithOutAuthentication<TSentType, TResponse>(pathCall, obToSend);
    }
    public async Task<string> callExample (string pathCall, logInModel obToSend)
    {
      try
      {
          HttpResponseMessage response =  _client.PostAsJsonAsync(pathCall, obToSend).Result;
          response.EnsureSuccessStatusCode();
          if (response.IsSuccessStatusCode)
          {
            return await response.Content.ReadAsAsync<string>();
          }
          else
          {
            return null;
          }
      }
      catch (Exception e)
      {
        throw e;
        return null;
      }
    }

    internal async Task<TResponse> callServiceWithOutAuthentication<TSentType,TResponse>(string pathCall, TSentType obToSend)
    {
      TSentType obj;
      try
      {
          string json = JsonConvert.SerializeObject(obToSend);
          var content = new StringContent(json, Encoding.UTF8, "application/json");

          _client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");
        
          HttpResponseMessage response = await _client.PostAsJsonAsync(pathCall, content);

          response.EnsureSuccessStatusCode();
          if (response.IsSuccessStatusCode)
          {
            return await response.Content.ReadAsAsync<TResponse>();
          }
          else
          {
            return default(TResponse);
          }
      }
      catch (Exception e)
      {
        throw e;
        return default(TResponse);
      }
      
    }

    internal async Task<TResponse> callServiceWithAuthentication<TSentType, TResponse>(string pathCall, object obToSend)
    {
      object obj = null;
      using (HttpClient client = new HttpClient())
      {

        client.BaseAddress = new Uri("");
        client.DefaultRequestHeaders.Add("loginkey", string.Empty);
        HttpResponseMessage response = await client.PostAsJsonAsync("", obToSend);

      }
      return default(TResponse);
    }
  }
}
