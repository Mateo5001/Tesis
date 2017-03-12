using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
  public static class Extencions
  {
    public static async Task<HttpResponseMessage> ownPostAsJsonAsync(this HttpClient client, string addr, object obj)
    {
      var response = await client.PostAsync(addr, new StringContent(
              Newtonsoft.Json.JsonConvert.SerializeObject(obj),
              Encoding.UTF8, "application/json"));

      return response;
    }
  }
