using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSearch.Common;
using XSearch.DAL.Repository.Core;
using XSearch.Data;

namespace XSearch.DAL.Repository
{
    public interface IXSearchDBManagementRepository : IRepo
    {
        int GetTotalObjs();
        Obj GetObj(int id);
        ObjMapping GetObjMapping(int id);
        IEnumerable<Obj> GetObjs(ICriteria criteria);

        int GetTotalObjMappings();
        IEnumerable<Obj> GetAllObjs();
        IEnumerable<ObjMapping> GetAllObjMappings();
        IEnumerable<ObjMapping> GetObjMappings(ICriteria criteria);

        //IEnumerable<Obj> GetPath();

        //int GetPathLength();

    }
}
