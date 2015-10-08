using System;
using System.Windows;
using XSearch.Common;

namespace XSearch.ViewsModels
{
    public class ObjViewModel : ViewModel
    {

        private int _key;

        private int _value;

        private Visibility _isVisable;

        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        public Visibility IsVisable
        {
            get { return _isVisable; }
            set
            {
                if (_isVisable != value)
                {
                    _isVisable = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
