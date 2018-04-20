using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Financeasy.Api.Applications;
using Financeasy.Api.Authentication;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Enums;
using Financeasy.Api.Domain.Models;

namespace Financeasy.Api.Controllers
{
    [RoutePrefix("categories")]
    public class CategoryController : WebApiController
    {
        [Inject]
        private CategoryApplication _categoryApplication { get; set; }

        [Inject]
        private AuthenticationProvider _authProvider { get; set; }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            return Response(HttpStatusCode.OK, _categoryApplication.GetAll(auth.UserId).ToList());
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

            var category = _categoryApplication.FindById(id, auth.UserId);
            return category == null ? Response(HttpStatusCode.NotFound, "Categoria não encontrada.") : Response(HttpStatusCode.OK, category);
        }

        [Route("types/{type}")]
        [HttpGet]
        public HttpResponseMessage GetByType(CategoryType? type)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (!type.HasValue)
                return Response(HttpStatusCode.BadRequest, "Tipo de categoria inválido.");

            var categories = _categoryApplication.FindByType(type.Value, auth.UserId);
            return categories == null ? Response(HttpStatusCode.NotFound, "Nenhuma categoria encontrada com este tipo.") : Response(HttpStatusCode.OK, categories);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] CategoryPostModel categoryModel)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            try
            {
                categoryModel.UserId = auth.UserId;
                var operationResult = _categoryApplication.Insert(categoryModel);
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
        public HttpResponseMessage Put([FromBody] CategoryPutModel categoryModel, long id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            if (id != categoryModel.Id)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            try
            {
                var operationResult = _categoryApplication.Update(categoryModel, auth.UserId);
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
                var category = _categoryApplication.FindById(id, auth.UserId);
                if (category == null)
                    return Response(HttpStatusCode.NotFound, "Categoria não encontrada.");

                var operationResult = _categoryApplication.DeleteAndSave(category);
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
