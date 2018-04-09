using System.Data.Entity;
using System.Linq;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Persistence.Configs;

namespace Financeasy.Api.Core
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        [Inject]
        public FinanceasyContext Context { get; set; }

        public void Insert(TEntity entity) => Context.Set<TEntity>().Add(entity);

        public void Delete(TEntity entity) => Context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => Context.Entry(entity).State = EntityState.Modified;

        public IQueryable<TEntity> GetAll() => Context.Set<TEntity>().AsNoTracking().AsQueryable();

        public TEntity FindById(object id) => Context.Set<TEntity>().Find(id);

        public void Save() => Context.SaveChanges();

        public void Dispose() => Context.Dispose();

    }
}