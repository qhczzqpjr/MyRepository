using System.Collections.Generic;
using XSearch.Common;
using XSearch.Data;
using XSearch.Repository.Core;

namespace XSearch.Repository
{
    public interface IXSearchDbManagementRepository : IRepo
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
