using StudentAppHelper.ModelBindings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.Services.Contract
{
  public interface IHttpClientService
  {
    Task<TResponse> CallAsync<TSentType, TResponse>(string pPathCall, TSentType pObToSend );
    TResponse Call<TSentType, TResponse>(string pPathCall, TSentType pObToSend);
    void CallGet(string pPathCall);
    TResponse CallGet<TResponse>(string pPathCall);
    Task CallGetAsync(string pPathCall);
    Task<TResponse> CallGetAsync<TResponse>(string pPathCall);
    bool IsAuthenticated { get; set; }


  }
}
