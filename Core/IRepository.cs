using System.Linq;

namespace Financeasy.Api.Core
{
    public interface IRepository<TEntity>
    {
        void Insert(TEntity entity);
        void Delete(TEntity entity);

        //TEntity FindById(object id);
        IQueryable<TEntity> GetAll();
    }
}
