using System;
using System.Collections.Generic;
using System.Linq;
using XSearch.Business.Mapper;
using XSearch.Business.Services;
using XSearch.Business.Models;


namespace Client
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            if (args == null) throw new ArgumentNullException("args");
            Mapper.CreateMaps();
            Mapper.CreateServiceMaps();
       

            //using (var xSearchDbManagementService = new XSearchDbManagementService())
            //{
            //    xSearchDbManagementService.CreateObj(new Obj() { Key = "cr", Value = "Create Table",CreateDateTime=DateTime.UtcNow });
            //    xSearchDbManagementService.CreateObj(new Obj() { Key = "dr", Value = "Drop Table",CreateDateTime=DateTime.UtcNow });
            //    xSearchDbManagementService.CreateObjMapping(new ObjMapping() { LId = xSearchDbManagementService.GetObj(1), RId = xSearchDbManagementService.GetObj(2), CreateDateTime = DateTime.UtcNow });
            //    List<Obj> objs = xSearchDbManagementService.GetAllObjs().ToList();

            //    Console.WriteLine(objs.Count());
            //    Console.ReadLine();
            //}
        }
    }
}
