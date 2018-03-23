using System;
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

            return Response(HttpStatusCode.OK, _categoryApplication.GetAll());
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

            var category = _categoryApplication.FindById(id);
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

            var categories = _categoryApplication.FindByType(type.Value);
            return categories == null ? Response(HttpStatusCode.NotFound, "Nenhuma categoria encontrada com este tipo.") : Response(HttpStatusCode.OK, categories);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] CategoryPostModel categoryModel)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            //Validação dos campos preenchidos
            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest, ModelState);

            try
            {
                categoryModel.UserId = auth.UserId;
                _categoryApplication.Insert(categoryModel);
                return Response(HttpStatusCode.OK, "Registro inserido com sucesso.");
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] CategoryPutModel categoryModel, int id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            //Validação dos campos preenchidos
            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest, ModelState);

            if (id != categoryModel.Id)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            try
            {
                _categoryApplication.Update(categoryModel);
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

            var category = _categoryApplication.FindById(id);
            if (category == null)
                return Response(HttpStatusCode.NotFound, "Categoria não encontrada.");

            try
            {
                _categoryApplication.Delete(category);
                return Response(HttpStatusCode.OK, "Registro excluído com sucesso.");
            }
            catch (Exception e)
            {
                return Response(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
