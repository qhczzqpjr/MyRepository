using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSearch.Business.Services;

namespace XSearch.Business.Models
{
    public class Objs : INotifyPropertyChanged
    {
        ObservableCollection<Obj> _objs = new ObservableCollection<Obj>();
        //private readonly IXSearchDBManagementService _xSearchDBManagementService;
        public ObservableCollection<Obj> ObjsList
        {
            get { return _objs; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
