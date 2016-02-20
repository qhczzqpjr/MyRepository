using System;
using XSearch.Business.Services;
using XSearch.Business.Models;
using XSearch.Common;


namespace Client
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            var xSearchDbManagementService = new XSearchDbManagementService();
            xSearchDbManagementService.GetAllObjs();
            xSearchDbManagementService.CreateObj(new Obj(){Key = "cr",Value = "Create Table"});
        }
        public class Criteria : ICriteria
        {
            public Criteria()
            {
                    
            }
            public bool IsSearch
            {
                get { throw new NotImplementedException(); }
            }

            public int PageSize
            {
                get { throw new NotImplementedException(); }
            }

            public int PageIndex
            {
                get { throw new NotImplementedException(); }
            }

            public string SortColumn
            {
                get { return "Key"; }
            }

            public string SortOrder
            {
                get { return "Asc"; }
            }

            public string GetFieldData(string fieldName)
            {
                return "Key";
            }

            public string FilterColumn
            {
                get { throw new NotImplementedException(); }
            }
        }

    }
}
