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
using XSearch.Repository;

namespace XSearch.Business.Services
{
    public class XSearchDbManagementService : IXSearchDbManagementService
    {
        private readonly IXSearchDbManagementRepository _ixSearchDbManagementRepository;

        public XSearchDbManagementService()
        {
            _ixSearchDbManagementRepository = ObjectFactory.GetInstance<IXSearchDbManagementRepository>();
        }

        #region ObjMapping
        public IEnumerable<ObjMapping> GetObjMappings(ICriteria criteria)
        {
            return _ixSearchDbManagementRepository.GetObjMappings(criteria)
                    .Select(AutoMapper.Mapper.Map<Data.ObjMapping, ObjMapping>);
        }

        public IEnumerable<ObjMapping> GetAllObjMappings()
        {
            return _ixSearchDbManagementRepository.GetAllObjMappings()
                     .Select(AutoMapper.Mapper.Map<Data.ObjMapping, ObjMapping>);
        }

        public int GetTotalObjMappings()
        {
            return _ixSearchDbManagementRepository.GetTotalObjMappings();
        }

        public ObjMapping CreateObjMapping(ObjMapping objMapping)
        {
            return AutoMapper.Mapper.Map<Data.ObjMapping, ObjMapping>(_ixSearchDbManagementRepository
                .Insert(AutoMapper.Mapper.Map<ObjMapping, Data.ObjMapping>(objMapping)));
        }

        public ObjMapping GetObjMapping(int id)
        {
            return AutoMapper.Mapper.Map<Data.ObjMapping, ObjMapping>(_ixSearchDbManagementRepository.GetObjMapping(id));
        }

        public ObjMapping UpdateObjMapping(ObjMapping objMapping)
        {
            return AutoMapper.Mapper.Map<Data.ObjMapping, ObjMapping>(_ixSearchDbManagementRepository
                .Update(AutoMapper.Mapper.Map<ObjMapping, Data.ObjMapping>(objMapping)));
        }

        public ObjMapping DeleteObjMapping(ObjMapping objMapping)
        {
            return AutoMapper.Mapper.Map<Data.ObjMapping, ObjMapping>(_ixSearchDbManagementRepository
                .Delete(AutoMapper.Mapper.Map<ObjMapping, Data.ObjMapping>(objMapping)));
        }
        #endregion

        #region Obj
        public IEnumerable<Obj> GetObjs(ICriteria criteria)
        {
            return _ixSearchDbManagementRepository.GetObjs(criteria)
                .Select(AutoMapper.Mapper.Map<Data.Obj, Obj>);
        }

        public IEnumerable<Obj> GetAllObjs()
        {
            return _ixSearchDbManagementRepository.GetAllObjs()
                     .Select(AutoMapper.Mapper.Map<Data.Obj, Obj>);
        }

        public int GetTotalObjs()
        {
            return _ixSearchDbManagementRepository.GetTotalObjs();
        }

        public Obj CreateObj(Obj obj)
        {
            return AutoMapper.Mapper.Map<Data.Obj, Obj>(_ixSearchDbManagementRepository.Insert(AutoMapper.Mapper.Map<Obj, Data.Obj>(obj)));
        }

        public Obj GetObj(int id)
        {
            return AutoMapper.Mapper.Map<Data.Obj, Obj>(_ixSearchDbManagementRepository.GetObj(id));
        }

        public Obj UpdateObj(Obj obj)
        {
            return AutoMapper.Mapper.Map<Data.Obj, Obj>(_ixSearchDbManagementRepository.Update(AutoMapper.Mapper.Map<Obj, Data.Obj>(obj)));
        }

        public Obj DeleteObj(Obj obj)
        {
            return AutoMapper.Mapper.Map<Data.Obj, Obj>(_ixSearchDbManagementRepository.Delete(AutoMapper.Mapper.Map<Obj, Data.Obj>(obj)));
        }
        #endregion

        public void Dispose()
        {
            _ixSearchDbManagementRepository.Dispose();
        }
    }
}
