using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using XSearch.Business.Services;

namespace XSearch.Business.Models
{
    public class Objs : INotifyPropertyChanged
    {
        //ObservableCollection<Obj> _objs = new ObservableCollection<Obj>();

        private readonly XSearchDbManagementService service = new XSearchDbManagementService();
        public ObservableCollection<Obj> ObjsList
        {
            get
            {
                return new ObservableCollection<Obj>(service.GetAllObjs().ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
