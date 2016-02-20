using System.Data.Entity;

namespace XSearch.Repository.Core
{
    public abstract class Repo<TXSearchContext> : IRepo
        where TXSearchContext : XSearchDbContext, new()
    {
        protected Repo()
        {
            _context = new TXSearchContext();
        }

        private readonly TXSearchContext _context;
        protected TXSearchContext Context { get { return _context; } }
        public T Insert<T>(T item, bool saveImmediately = true) where T : class,new()
        {
            return PerformAction(item, EntityState.Added, saveImmediately);
        }

        public T Delete<T>(T item, bool saveImmediately) where T : class,new()
        {
            return PerformAction(item, EntityState.Deleted, saveImmediately);
        }

        public T Update<T>(T item, bool saveImmediately) where T : class,new()
        {
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

        protected virtual T PerformAction<T>(T item, EntityState entityState, bool saveImmediately = true) where T : class,new()
        {
            _context.Entry(item).State = entityState;
            if (saveImmediately)
            {
                _context.SaveChanges();
            }
            return item;
        }
    }
}
