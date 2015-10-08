using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using XSearch.Business.Models;
using XSearch.Business.Services;
using Data = XSearch.Data;

namespace XSearch.Business.Mapperings
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
                factory.For<IXSearchDBManagementService>().Use<XSearchDBManagementService>();
                factory.For<IXSearchDBManagementService>().Use<XSearchDBManagementService>();
            });
        }
    }
}
