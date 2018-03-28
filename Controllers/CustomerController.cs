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
    [RoutePrefix("customers")]
    public class CustomerController : WebApiController
    {
        [Inject]
        private CustomerApplication _customerApplication { get; set; }

        [Inject]
        private AuthenticationProvider _authProvider { get; set; }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            return Response(HttpStatusCode.OK, _customerApplication.GetAll());
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(int id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            var customer = _customerApplication.FindById(id);
            return customer == null ? Response(HttpStatusCode.NotFound, "Categoria não encontrada.") : Response(HttpStatusCode.OK, customer);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] CustomerPostModel customerModel)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            //Validação dos campos preenchidos
            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest, ModelState);

            try
            {
                customerModel.UserId = auth.UserId;
                _customerApplication.Insert(customerModel);
                return Response(HttpStatusCode.OK, "Registro inserido com sucesso.");
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.BadRequest, e.Message);
            }

        }
    }
}
