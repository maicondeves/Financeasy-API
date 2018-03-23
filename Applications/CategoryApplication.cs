using System.Collections.Generic;
using System.Linq;
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

        public void Insert(CategoryPostModel categoryModel)
        {
            var category = categoryModel.ToEntity();
            Insert(category);
        }

        public void Insert(Category category)
        {
            _repository.Insert(category);
            _repository.Save();
        }

        public void Update(CategoryPutModel categoryModel)
        {
            var category = categoryModel.ToEntity();
            Update(category);
        }

        public void Update(Category category)
        {
            _repository.Update(category);
            _repository.Save();
        }

        public void Delete(Category category)
        {
            _repository.Delete(category);
            _repository.Save();
        }

        public Category FindById(int id) => _repository.FindById(id);

        public IEnumerable<Category> FindByType(CategoryType type) => _repository.GetAll().Where(x => x.Type == type).ToList();

        public IEnumerable<Category> GetAll() => _repository.GetAll().ToList();
    }
}