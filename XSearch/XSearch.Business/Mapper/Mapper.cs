using StructureMap;
using XSearch.Business.Models;
using XSearch.Business.Services;

namespace XSearch.Business.Mapper
{
    public static class Mapper
    {
        public static void CreateMaps()
        {
            AutoMapper.Mapper.CreateMap<Data.Obj, Obj>();
            AutoMapper.Mapper.CreateMap<Obj, Data.Obj>().ForMember(t=>t.ValueType,p=>p.Ignore());

            AutoMapper.Mapper.CreateMap<Data.ObjMapping, ObjMapping>();
            AutoMapper.Mapper.CreateMap<ObjMapping, Data.ObjMapping>();
        }

        public static void CreateServiceMaps()
        {
            ObjectFactory.Configure(factory =>
            {
                factory.For<IXSearchDbManagementService>().Use<XSearchDbManagementService>();
                factory.For<IXSearchDbManagementService>().Use<XSearchDbManagementService>();
            });
        }
    }
}
