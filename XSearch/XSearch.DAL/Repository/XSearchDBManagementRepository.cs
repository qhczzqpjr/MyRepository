using System.Collections.Generic;
using System.Linq;
using XSearch.Common;
using XSearch.Data;
using XSearch.Repository.Core;

namespace XSearch.Repository
{
    public class XSearchDbManagementRepository : Repo<XSearchDbContext>, IXSearchDbManagementRepository
    {
        #region Obj
        public IEnumerable<Obj> GetAllObjs()
        {
            return Context.Objs.OrderBy(one => one.Id);
        }

        public Obj GetObj(int id)
        {
            return Context.Objs.FirstOrDefault(one => one.Id == id);
        }

        public IEnumerable<Obj> GetObjs(ICriteria criteria)
        {
            IQueryable<Obj> query = Context.Objs;
            if (criteria.IsSearch)
            {
                var value = criteria.GetFieldData(criteria.FilterColumn);
                query = query.Where(one => one.Key.Contains(value));
            }
            if (criteria.SortColumn == "Key" && criteria.SortOrder == "asc")
            {
                query = query.OrderBy(one => one.Key);
            }
            else if (criteria.SortColumn == "Key" && criteria.SortOrder == "desc")
            {
                query = query.OrderByDescending(one => one.Key);
            }
            else
                query = query.OrderBy(one => one.Id);
            query = query.Skip((criteria.PageIndex - 1) * criteria.PageSize).Take(criteria.PageSize);
            return query;
        }
#endregion

        #region ObjMapping

        public int GetTotalObjs()
        {
            return Context.Objs.Count();
        }

        public int GetTotalObjMappings()
        {
            return Context.ObjMappings.Count();
        }

        public ObjMapping GetObjMapping(int id)
        {
            return Context.ObjMappings.FirstOrDefault(one => one.Id == id);
        }

        public IEnumerable<ObjMapping> GetAllObjMappings()
        {
            return Context.ObjMappings.OrderBy(one => one.Id);
        }

        public IEnumerable<ObjMapping> GetObjMappings(ICriteria criteria)
        {
            IQueryable<ObjMapping> query = Context.ObjMappings;
            if (criteria.IsSearch)
            {
                var value = criteria.GetFieldData(criteria.FilterColumn);
                query = query.Where(one => one.LId.ToString().Contains(value));
            }
            if (criteria.SortColumn == "LId" && criteria.SortOrder == "asc")
            {
                query = query.OrderBy(one => one.LId);
            }
            else if (criteria.SortColumn == "LId" && criteria.SortOrder == "desc")
            {
                query = query.OrderByDescending(one => one.LId);
            }
            else
                query = query.OrderBy(one => one.Id);
            query = query.Skip((criteria.PageIndex - 1) * criteria.PageSize).Take(criteria.PageSize);
            return query;

        }
        #endregion

        //public IEnumerable<Obj> GetPath()
        //{
        //    return Context.Objs;
        //}

        //public int GetPathLength()
        //{
        //    return Context.Objs.Count();
        //}

        //public Obj FindById(int id)
        //{
        //    return Context.Objs.Where(p => p.Id == id).FirstOrDefault();
        //}
    }
}
