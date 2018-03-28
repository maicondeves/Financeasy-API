using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Financeasy.Api.Applications;
using Financeasy.Api.Authentication;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Models;
using Financeasy.Api.Utils.Extensions;

namespace Financeasy.Api.Controllers
{
    [RoutePrefix("accounts")]
    public class AccountController : WebApiController
    {
        [Inject]
        private UserApplication _userApplication { get; set; }

        [Inject]
        private AuthenticationProvider _authProvider { get; set; }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register([FromBody] UserRegisterModel userModel)
        {
            try
            {
                var operationResult = _userApplication.Register(userModel);
                if (!operationResult.Success)
                    return Response(HttpStatusCode.BadRequest, operationResult.Message);
                
                return Response(HttpStatusCode.OK, operationResult.Message);
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Authenticate([FromBody] UserAuthenticateModel userModel)
        {
            try
            {
                var operationResult = _userApplication.Authenticate(userModel);
                if (!operationResult.Success)
                    return Response(HttpStatusCode.BadRequest, operationResult.Message);
                
                return Response(HttpStatusCode.OK, operationResult.Message);
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Route("profile")]
        [HttpGet]
        public HttpResponseMessage Profile()
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            var user = _userApplication.FindById(auth.UserId);
            if (user == null)
                return Response(HttpStatusCode.NotFound, "Usuário não encontrado.");

            UserProfileModel userModel = user.ToModel();
            return Response(HttpStatusCode.OK, userModel);
        }
    }
}
