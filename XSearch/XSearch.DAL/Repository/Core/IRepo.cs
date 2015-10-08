using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSearch.DAL.Repository.Core
{
    public interface IRepo : IDisposable
    {
        T Insert<T>(T item, bool saveImmediately = true) where T : class,new();
        T Update<T>(T item, bool saveImmediately = true) where T : class,new();
        T Delete<T>(T item, bool saveImmediately = true) where T : class,new();
        int Save();
    }
}
