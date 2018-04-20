using System;
using System.Linq;
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
    [RoutePrefix("expenses")]
    public class ExpenseController : WebApiController
    {
        [Inject]
        private ExpenseApplication _expenseApplication { get; set; }

        [Inject]
        private AuthenticationProvider _authProvider { get; set; }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            return Response(HttpStatusCode.OK, _expenseApplication.GetAll(auth.UserId).ToList());
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

            var revenue = _expenseApplication.FindById(id, auth.UserId);
            return revenue == null ? Response(HttpStatusCode.NotFound, "Receita não encontrada.") : Response(HttpStatusCode.OK, revenue);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] ExpensePostModel expenseModel)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            try
            {
                expenseModel.UserId = auth.UserId;
                var operationResult = _expenseApplication.Insert(expenseModel);
                if (!operationResult.Success)
                    return Response(HttpStatusCode.BadRequest, operationResult.Message);

                return Response(HttpStatusCode.OK, operationResult.Message);
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] ExpensePutModel expenseModel, long id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            if (id != expenseModel.Id)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            try
            {
                var operationResult = _expenseApplication.Update(expenseModel, auth.UserId);
                if (!operationResult.Success)
                    return Response(HttpStatusCode.BadRequest, operationResult.Message);

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
                var revenue = _expenseApplication.FindById(id, auth.UserId);
                if (revenue == null)
                    return Response(HttpStatusCode.NotFound, "Receita não encontrada.");

                var operationResult = _expenseApplication.DeleteAndSave(revenue);
                if (!operationResult.Success)
                    return Response(HttpStatusCode.BadRequest, operationResult.Message);

                return Response(HttpStatusCode.OK, operationResult.Message);
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
