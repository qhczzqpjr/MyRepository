using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSearch.DAL.Repository;
using XSearch.Data;

namespace XSearch
{
    public static class StaticClass
    {
        public static ObservableCollection<Obj> ObjLists
        {
            get
            {
                using (XSearchDbManagementRepository repo = new XSearchDbManagementRepository())
                {
                    return new ObservableCollection<Obj>(repo.GetAllObjs());
                }

            }
            set { ObjLists = value; }
        }
    }
}
