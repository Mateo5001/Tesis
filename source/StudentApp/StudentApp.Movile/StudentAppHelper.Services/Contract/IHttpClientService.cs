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
    Task<TResponse> CallAsync<TSentType, TResponse>(string pPathCall, TSentType pObToSend , bool pIsAuthenticated=true);

    TResponse Call<TSentType, TResponse>(string pPathCall, TSentType pObToSend, bool pIsAuthenticated=true);
    
    
  
  }
}
