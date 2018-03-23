using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Financeasy.Api.Applications;
using Financeasy.Api.Authentication;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Models;

namespace Financeasy.Api.Controllers
{
    [RoutePrefix("users")]
    public class UserController : WebApiController
    {
        [Inject]
        private UserApplication _userApplication { get; set; }

        [Inject]
        private AuthenticationProvider _authProvider { get; set; }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            return Response(HttpStatusCode.OK, _userApplication.GetAll());
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(int id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido");

            var user = _userApplication.FindById(id);
            return user == null ? Response(HttpStatusCode.NotFound, "Usuário não encontrado.") : Response(HttpStatusCode.OK, user);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] UserPostModel userModel)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            //Validação dos campos preenchidos
            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest, ModelState);

            try
            {
                _userApplication.Insert(userModel);
                return Response(HttpStatusCode.OK, "Registro inserido com sucesso.");
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] UserPutModel userModel, int id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message); 

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest, ModelState);

            if (id != userModel.Id)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            try
            {
                _userApplication.Update(userModel);
                return Response(HttpStatusCode.OK, "Registro atualizado com sucesso.");
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            var user = _userApplication.FindById(id);
            if (user == null)
                return Response(HttpStatusCode.NotFound, "Usuário não encontrado.");

            try
            {
                _userApplication.Delete(user);
                return Response(HttpStatusCode.OK, "Registro excluído com sucesso.");
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
