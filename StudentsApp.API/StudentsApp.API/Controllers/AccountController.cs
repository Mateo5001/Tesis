using StudentsApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using StudentAppHelper.Library.Util;
using StudentsApp.API.AppLogic;
using DBEntityModel.DBModel;

namespace StudentsApp.API.Controllers
{
  [RoutePrefix("api/Account")]
  [Authorize]
  public class AccountController : ApiController
  {
    AccountLogic Account = new AccountLogic(); 
    [HttpPost]
    [AllowAnonymous]
    [Route("RegisterUser/{id}")]
    [Route("RegisterUser")]
    public async Task<string> RegisterUser(logInModel id)
    {
      return id.User;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("RegisterAdmin")]
    public bool RegisterAdmin(UserRegistrationModel model)
    {
      bool registra=Account.Registrar(model);
      return registra;
    }


    [AllowAnonymous]
    [Route("get/{id}")]
    public async Task<object> get(string id)
    {
      logInModel model = new logInModel()
      {
        User = id,
        Password = "1234"
      };
      return model;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("LogIn")]
    public async Task<HttpResponseMessage> logIn(logInModel logUser)
    {
      var request = HttpContext.Current.Request;
      var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "/api/Token";
      using (var client = new HttpClient())
      {
        var requestParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", logUser.User),
                new KeyValuePair<string, string>("password", logUser.Password)
            };
        var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
        var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
        var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
        var responseCode = tokenServiceResponse.StatusCode;
        var responseMsg = new HttpResponseMessage(responseCode)
        {
          Content = new StringContent(responseString, Encoding.UTF8, "application/json")
        };
        return responseMsg;
      }
    }

  }
}