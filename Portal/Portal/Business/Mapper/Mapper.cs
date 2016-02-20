using Business.Services;
using Data;
using Repository.Repository;
using StructureMap;

namespace Business.Mapper
{
    public static class ObjectMapper
    {
        public static void CreateMaps()
        {
            AutoMapper.Mapper.CreateMap<Requirement, Models.Requirement>();
            AutoMapper.Mapper.CreateMap<Models.Requirement, Requirement>();
        }

        public static void CreateServiceMaps()
        {
            ObjectFactory.Configure(factory => { factory.For<IDbManagementService>().Use<DbManagementService>(); });
            ObjectFactory.Configure(factory => { factory.For<IDbManagementRepository>().Use<DbManagementRepository>(); });
        }
    }
}