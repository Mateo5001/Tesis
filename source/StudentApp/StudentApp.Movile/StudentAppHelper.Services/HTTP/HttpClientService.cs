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
    public async Task<TResponse> Call<TSentType, TResponse>(string pathCall, TSentType obToSend)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(@"http://studentapphelper-api-test.azurewebsites.net");
          string data = JsonConvert.SerializeObject(obToSend);
          StringContent body = new StringContent(data, UTF8Encoding.UTF8, "application/json");
          var response = await client.PostAsync(pathCall, body);
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

    public Task<string> callExample(string pathCall, logInModel obToSend)
    {
      throw new NotImplementedException();
    }

    public Task<TResponse> callServiceWithAuthentication<TSentType, TResponse>(string pathCall, object obToSend)
    {
      throw new NotImplementedException();
    }

    public Task<TResponse> callServiceWithOutAuthentication<TSentType, TResponse>(string pathCall, TSentType obToSend)
    {
      throw new NotImplementedException();
    }
  }
}
