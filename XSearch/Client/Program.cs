using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSearch.Business.Mapperings;
using XSearch.Business.Services;
using XSearch.DAL.Repository;
using XSearch.Business.Models;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.CreateMaps();
            Mapper.CreateServiceMaps();

            using (var xSearchDBManagementService = new XSearchDBManagementService())
            {
                xSearchDBManagementService.CreateObj(new Obj() { Key = "cr", Value = "Create Table",CreateDateTime=DateTime.UtcNow });
                xSearchDBManagementService.CreateObj(new Obj() { Key = "dr", Value = "Drop Table",CreateDateTime=DateTime.UtcNow });
                xSearchDBManagementService.CreateObjMapping(new ObjMapping() { LId = xSearchDBManagementService.GetObj(1), RId = xSearchDBManagementService.GetObj(2), CreateDateTime = DateTime.UtcNow });
                List<Obj> objs = xSearchDBManagementService.GetAllObjs().ToList();

                Console.WriteLine(objs.Count());
                Console.ReadLine();
            }
        }
    }
}
