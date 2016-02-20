using System.Collections.Generic;
using System.Linq;
using Business.Models;
using Common;
using Repository.Repository;
using StructureMap;

namespace Business.Services
{
    public class DbManagementService : IDbManagementService
    {
        private readonly IDbManagementRepository _iDbManagementRepository;

        public DbManagementService()
        {
            _iDbManagementRepository = ObjectFactory.GetInstance<DbManagementRepository>();
        }

        public void Dispose()
        {
            _iDbManagementRepository.Dispose();
        }

        #region Requirement

        public IEnumerable<Requirement> GetRequirements(ICriteria criteria)
        {
            return _iDbManagementRepository.GetRequirements(criteria)
                .Select(AutoMapper.Mapper.Map<Data.Requirement, Requirement>);
        }

        public IEnumerable<Requirement> GetRequirements(string code)//Need to improve, do not support other column for filter
        {
            return _iDbManagementRepository.GetRequirements(code)
                .Select(AutoMapper.Mapper.Map<Data.Requirement, Requirement>);
        }

        public IEnumerable<Requirement> GetAllRequirements()
        {
            return _iDbManagementRepository.GetAllRequirements()
                .Select(AutoMapper.Mapper.Map<Data.Requirement, Requirement>);
        }

        public int GetTotalRequirements()
        {
            return _iDbManagementRepository.GetTotalRequirements();
        }

        public Requirement CreateRequirement(Requirement obj)
        {
            return
                AutoMapper.Mapper.Map<Data.Requirement, Requirement>(
                    _iDbManagementRepository.Insert(AutoMapper.Mapper.Map<Requirement, Data.Requirement>(obj)));
        }

        public Requirement GetRequirement(int id)
        {
            return AutoMapper.Mapper.Map<Data.Requirement, Requirement>(_iDbManagementRepository.GetRequirement(id));
        }

        public Requirement GetRequirement(string code)
        {
            return AutoMapper.Mapper.Map<Data.Requirement, Requirement>(_iDbManagementRepository.GetRequirement(code));
        }

        public Requirement UpdateRequirement(Requirement obj)
        {
            return
                AutoMapper.Mapper.Map<Data.Requirement, Requirement>(
                    _iDbManagementRepository.Update(AutoMapper.Mapper.Map<Requirement, Data.Requirement>(obj)));
        }

        public Requirement DeleteRequirement(Requirement obj)
        {
            return
                AutoMapper.Mapper.Map<Data.Requirement, Requirement>(
                    _iDbManagementRepository.Delete(AutoMapper.Mapper.Map<Requirement, Data.Requirement>(obj)));
        }

        #endregion
    }
}