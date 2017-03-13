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
    Task<TResponse> Call<TSentType, TResponse>(string pathCall, TSentType obToSend);

    Task<string> callExample(string pathCall, logInModel obToSend);

    Task<TResponse> callServiceWithOutAuthentication<TSentType, TResponse>(string pathCall, TSentType obToSend);

    Task<TResponse> callServiceWithAuthentication<TSentType, TResponse>(string pathCall, object obToSend);
  
  }
}
