﻿using System;
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
    [RoutePrefix("projects")]
    public class ProjectController : WebApiController
    {
        [Inject]
        private ProjectApplication _projectApplication { get; set; }

        [Inject]
        private AuthenticationProvider _authProvider { get; set; }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            return Response(HttpStatusCode.OK, _projectApplication.GetAll(auth.UserId).ToList());
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

            var project = _projectApplication.FindById(id, auth.UserId);
            return project == null ? Response(HttpStatusCode.NotFound, "Projeto não encontrado.") : Response(HttpStatusCode.OK, project);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] ProjectPostModel projectModel)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            try
            {
                projectModel.UserId = auth.UserId;
                var operationResult = _projectApplication.Insert(projectModel);
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
        public HttpResponseMessage Put([FromBody] ProjectPutModel projectModel, long id = 0)
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            if (id == 0)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            if (id != projectModel.Id)
                return Response(HttpStatusCode.BadRequest, "Id inválido.");

            try
            {
                var operationResult = _projectApplication.Update(projectModel, auth.UserId);
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
                var project = _projectApplication.FindById(id, auth.UserId);
                if (project == null)
                    return Response(HttpStatusCode.NotFound, "Categoria não encontrada.");

                var isValid = _projectApplication.SearchForRegisters(id, auth.UserId);
                if (!isValid)
                    return Response(HttpStatusCode.BadRequest, "Esse projeto não pode ser deletado pois possui receitas e despesas.");

                var operationResult = _projectApplication.DeleteAndSave(project);
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
