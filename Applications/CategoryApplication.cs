using System;
using System.Collections.Generic;
using System.Linq;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Enums;
using Financeasy.Api.Domain.Models;
using Financeasy.Api.Persistence.Repositories;
using Financeasy.Api.Utils.Extensions;

namespace Financeasy.Api.Applications
{
    public class CategoryApplication
    {
        [Inject]
        private CategoryRepository _repository { get; set; }

        public OperationResult Insert(CategoryPostModel categoryModel)
        {
            if (string.IsNullOrWhiteSpace(categoryModel.Name))
                return new OperationResult(false, "Descrição inválida.");

            if (categoryModel.Name.Length < 2 || categoryModel.Name.Length > 30)
                return new OperationResult(false, "Descrição deve conter no mínimo 2 caracteres e no máximo 30.");

            var category = categoryModel.ToEntity();
            return InsertAndSave(category);
        }

        public OperationResult InsertAndSave(Category category)
        {
            try
            {
                category.RegisterDate = DateTime.Now;
                _repository.Insert(category);
                _repository.Save();
                return new OperationResult(true, "Categoria inserida com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult Update(CategoryPutModel categoryModel)
        {
            var currentCategory = FindById(categoryModel.Id);

            if (string.IsNullOrWhiteSpace(categoryModel.Name))
                return new OperationResult(false, "Descrição inválida.");

            if (categoryModel.Name.Length < 2 || categoryModel.Name.Length > 30)
                return new OperationResult(false, "Descrição deve conter no mínimo 2 caracteres e no máximo 30.");

            var category = categoryModel.ToEntity(currentCategory);
            return UpdateAndSave(category);
        }

        public OperationResult UpdateAndSave(Category category)
        {
            try
            {
                category.UpdateDate = DateTime.Now;
                _repository.Update(category);
                _repository.Save();
                return new OperationResult(true, "Categoria atualizada com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult DeleteAndSave(Category category)
        {
            try
            {
                _repository.Delete(category);
                _repository.Save();
                return new OperationResult(true, "Categoria excluída com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public Category FindById(long id) => _repository.FindById(id);

        public IEnumerable<Category> FindByType(CategoryType type) => _repository.GetAll().Where(x => x.Type == type).ToList();

        public IEnumerable<Category> GetAll() => _repository.GetAll().ToList();
    }
}