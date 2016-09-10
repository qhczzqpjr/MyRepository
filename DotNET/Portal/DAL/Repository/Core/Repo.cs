using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Repository.Repository.Core
{
    public class Repo<TDbContext> : IRepo
        where TDbContext : XSearchDbContent, new()
    {
        private readonly TDbContext _context;

        protected Repo()
        {
            _context = new TDbContext();
        }

        protected TDbContext Context
        {
            get { return _context; }
        }

        public T Insert<T>(T item, bool saveImmediately = true) where T : class, new()
        {
            return PerformAction(item, EntityState.Added, saveImmediately);
        }

        public T Delete<T>(T item, bool saveImmediately) where T : class, new()
        {
            RemoveHoldingEntityInContext(item);
            return PerformAction(item, EntityState.Deleted, saveImmediately);
        }

        public T Update<T>(T item, bool saveImmediately) where T : class, new()
        {
            RemoveHoldingEntityInContext(item);
            return PerformAction(item, EntityState.Modified, saveImmediately);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        //public IEnumerable<T> Select<T>() where T: class
        //{
        //    return Context.Set<T>();
        //}

        protected virtual T PerformAction<T>(T item, EntityState entityState, bool saveImmediately = true)
            where T : class, new()
        {
            _context.Entry(item).State = entityState;
            if (saveImmediately)
            {
                _context.SaveChanges();
            }
            return item;
        }

        private bool RemoveHoldingEntityInContext<T>(T entity) where T : class, new()
        {
            var objContext = ((IObjectContextAdapter) _context).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }
    }
}