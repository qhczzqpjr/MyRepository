using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSearch.Business.Models;
using XSearch.Common;

namespace XSearch.Business.Services
{
    public interface IXSearchDBManagementService : IDisposable
    {
        IEnumerable<ObjMapping> GetObjMappings(ICriteria criteria);

        IEnumerable<ObjMapping> GetAllObjMappings();

        int GetTotalObjMappings();

        ObjMapping CreateObjMapping(ObjMapping contactType);

        ObjMapping GetObjMapping(int id);

        ObjMapping UpdateObjMapping(ObjMapping contactType);

        ObjMapping DeleteObjMapping(ObjMapping contactType);


        IEnumerable<Obj> GetObjs(ICriteria criteria);
        IEnumerable<Obj> GetAllObjs();

        int GetTotalObjs();

        Obj CreateObj(Obj contact);

        Obj GetObj(int id);

        Obj UpdateObj(Obj contact);

        Obj DeleteObj(Obj contact);
    }
}
