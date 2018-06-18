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

            return Response(HttpStatusCode.OK, _customerApplication.GetList(auth.UserId));
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(long id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            var customer = _customerApplication.FindById(id, auth.UserId);
            return customer == null ? Response(HttpStatusCode.NotFound, "Cliente não encontrado.") : Response(HttpStatusCode.OK, customer);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] CustomerPostModel customerModel)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);
            
            try
            {
                customerModel.UserId = auth.UserId;
                var operationResult = _customerApplication.Insert(customerModel);
                if (!operationResult.Success)
                    return Response(HttpStatusCode.InternalServerError, operationResult.Message);

                return Response(HttpStatusCode.OK, operationResult.Message);
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] CustomerPutModel customerModel, long id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            if (id != customerModel.Id)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            try
            {
                var operationResult = _customerApplication.Update(customerModel, auth.UserId);
                if (!operationResult.Success)
                    return Response(HttpStatusCode.InternalServerError, operationResult.Message);

                return Response(HttpStatusCode.OK, operationResult.Message);
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(long id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            try
            {
                var isValid = _customerApplication.SearchForRegisters(id, auth.UserId);
                if (!isValid)
                    return Response(HttpStatusCode.BadRequest, "Esse cliente não pode ser deletado pois está sendo utilizada em algum projeto.");

                var operationResult = _customerApplication.Delete(id, auth.UserId);
                if (!operationResult.Success)
                    return Response(HttpStatusCode.InternalServerError, operationResult.Message);

                return Response(HttpStatusCode.OK, operationResult.Message);
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
