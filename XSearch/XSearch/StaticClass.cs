using System.Collections.ObjectModel;
using XSearch.Data;
using XSearch.Repository;

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
