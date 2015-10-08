using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using XSearch.Business.Models;
using XSearch.Common;
using AutoMapper;
using XSearch.DAL.Repository;

namespace XSearch.Business.Services
{
    public class XSearchDBManagementService : IXSearchDBManagementService
    {
        private readonly IXSearchDBManagementRepository _xSearchDBManagementRepository;

        public XSearchDBManagementService()
        {
            _xSearchDBManagementRepository = ObjectFactory.GetInstance<IXSearchDBManagementRepository>();
        }

        #region ObjMapping
        public IEnumerable<ObjMapping> GetObjMappings(ICriteria criteria)
        {
            return _xSearchDBManagementRepository.GetObjMappings(criteria)
                    .Select(Mapper.Map<Data.ObjMapping, ObjMapping>);
        }

        public IEnumerable<ObjMapping> GetAllObjMappings()
        {
            return _xSearchDBManagementRepository.GetAllObjMappings()
                     .Select(Mapper.Map<Data.ObjMapping, ObjMapping>);
        }

        public int GetTotalObjMappings()
        {
            return _xSearchDBManagementRepository.GetTotalObjMappings();
        }

        public ObjMapping CreateObjMapping(ObjMapping objMapping)
        {
            return Mapper.Map<Data.ObjMapping, ObjMapping>(_xSearchDBManagementRepository
                .Insert(Mapper.Map<ObjMapping, Data.ObjMapping>(objMapping)));
        }

        public ObjMapping GetObjMapping(int id)
        {
            return Mapper.Map<Data.ObjMapping, ObjMapping>(_xSearchDBManagementRepository.GetObjMapping(id));
        }

        public ObjMapping UpdateObjMapping(ObjMapping objMapping)
        {
            return Mapper.Map<Data.ObjMapping, ObjMapping>(_xSearchDBManagementRepository
                .Update(Mapper.Map<ObjMapping, Data.ObjMapping>(objMapping)));
        }

        public ObjMapping DeleteObjMapping(ObjMapping objMapping)
        {
            return Mapper.Map<Data.ObjMapping,ObjMapping>(_xSearchDBManagementRepository
                .Delete(Mapper.Map<ObjMapping,Data.ObjMapping>(objMapping)));
        }
        #endregion

        #region Obj
        public IEnumerable<Obj> GetObjs(ICriteria criteria)
        {
            return _xSearchDBManagementRepository.GetObjs(criteria)
                .Select(Mapper.Map<Data.Obj, Obj>);
        }

        public IEnumerable<Obj> GetAllObjs()
        {
            return _xSearchDBManagementRepository.GetAllObjs()
                     .Select(Mapper.Map<Data.Obj, Obj>);
        }

        public int GetTotalObjs()
        {
            return _xSearchDBManagementRepository.GetTotalObjs();
        }

        public Obj CreateObj(Obj obj)
        {
            return Mapper.Map<Data.Obj,Obj>(_xSearchDBManagementRepository.Insert(Mapper.Map<Obj, Data.Obj>(obj)));
        }

        public Obj GetObj(int id)
        {
            return Mapper.Map<Data.Obj,Obj>(_xSearchDBManagementRepository.GetObj(id));
        }

        public Obj UpdateObj(Obj obj)
        {
            return Mapper.Map<Data.Obj,Obj>(_xSearchDBManagementRepository.Update(Mapper.Map<Obj, Data.Obj>(obj)));
        }

        public Obj DeleteObj(Obj obj)
        {
            return Mapper.Map<Data.Obj,Obj>(_xSearchDBManagementRepository.Delete(Mapper.Map<Obj, Data.Obj>(obj)));
        }
        #endregion

        public void Dispose()
        {
            _xSearchDBManagementRepository.Dispose();
        }
    }
}
