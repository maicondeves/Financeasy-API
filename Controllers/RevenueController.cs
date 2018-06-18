using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Financeasy.Api.Applications;
using Financeasy.Api.Authentication;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Filters;
using Financeasy.Api.Domain.Models;

namespace Financeasy.Api.Controllers
{
    [RoutePrefix("revenues")]
    public class RevenueController : WebApiController
    {
        [Inject]
        private RevenueApplication _revenueApplication { get; set; }

        [Inject]
        private AuthenticationProvider _authProvider { get; set; }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            return Response(HttpStatusCode.OK, _revenueApplication.GetAll(auth.UserId).ToList());
        }

        [Route("project")]
        [HttpPost]
        public HttpResponseMessage GetAllWithFilters([FromBody] RevenueFilter filter)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (filter == null || filter.ProjectId == 0 || filter.MonthWork == 0 || filter.YearWork == 0)
                return Response(HttpStatusCode.BadRequest, "Filtros inválidos.");

            return Response(HttpStatusCode.OK, _revenueApplication.GetAllWithFilters(auth.UserId, filter));
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

            var revenue = _revenueApplication.FindById(id, auth.UserId);
            return revenue == null ? Response(HttpStatusCode.NotFound, "Receita não encontrada.") : Response(HttpStatusCode.OK, revenue);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] RevenuePostModel revenueModel)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            try
            {
                revenueModel.UserId = auth.UserId;
                var operationResult = _revenueApplication.Insert(revenueModel);
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
        public HttpResponseMessage Put([FromBody] RevenuePutModel revenueModel, long id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            if (id != revenueModel.Id)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            try
            {
                var operationResult = _revenueApplication.Update(revenueModel, auth.UserId);
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
                var revenue = _revenueApplication.FindById(id, auth.UserId);
                if (revenue == null)
                    return Response(HttpStatusCode.NotFound, "Receita não encontrada.");

                var operationResult = _revenueApplication.DeleteAndSave(revenue);
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
