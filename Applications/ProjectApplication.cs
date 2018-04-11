using System;
using System.Collections.Generic;
using System.Linq;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Models;
using Financeasy.Api.Persistence.Repositories;
using Financeasy.Api.Utils.Extensions;

namespace Financeasy.Api.Applications
{
    public class ProjectApplication
    {
        [Inject]
        private ProjectRepository _repository { get; set; }

        public OperationResult Insert(ProjectPostModel model)
        {
            //Validações aqui

            var project = model.ToEntity();
            return InsertAndSave(project);
        }

        public OperationResult InsertAndSave(Project project)
        {
            try
            {
                project.RegisterDate = DateTime.Now;
                _repository.Insert(project);
                _repository.Save();
                return new OperationResult(true, "Projeto inserido com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult Update(ProjectPutModel model)
        {
            var currentProject = FindById(model.Id);

            //Validações aqui
            
            var project = model.ToEntity(currentProject);
            return UpdateAndSave(project);
        }

        public OperationResult UpdateAndSave(Project project)
        {
            try
            {
                project.UpdateDate = DateTime.Now;
                _repository.Update(project);
                _repository.Save();
                return new OperationResult(true, "Projeto atualizado com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult DeleteAndSave(Project project)
        {
            try
            {
                _repository.Delete(project);
                _repository.Save();
                return new OperationResult(true, "Projeto excluído com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public Project FindById(long id) => _repository.FindById(id);

        public IEnumerable<Project> GetAll() => _repository.GetAll().ToList();
    }
}