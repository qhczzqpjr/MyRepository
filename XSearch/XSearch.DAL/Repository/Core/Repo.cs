using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSearch.DAL;
using XSearch.DAL.Repository.Core;

namespace XSearch.DAL.Repository.Core
{
    public abstract class Repo<XSearchContext> : IRepo
        where XSearchContext : XSearchDBContext, new()
    {
        protected Repo()
        {
            _context = new XSearchContext();
        }

        private readonly XSearchContext _context;
        protected XSearchContext Context { get { return _context; } }
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
