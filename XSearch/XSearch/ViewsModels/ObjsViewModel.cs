using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using XSearch.Business.Models;
using XSearch.Common;

namespace XSearch.ViewsModels
{
    public class ObjsViewModel : ViewModel
    {
        public ObjsViewModel()
        {
           Model=new Objs();
        }

        public ObjsViewModel(Objs model)
        {
            Model = model;
            ImgSrc = "pack://siteoforigin:,,,/Resources/SearchIcon.png";
            //SomeCommand = new DelegateCommand(Execute, CanExecute);
        }
        public DelegateCommand SomeCommand { get; set; }
        private void Execute()
        {
            MessageBox.Show("Test");
        }

        private bool CanExecute()
        {
            Debug.WriteLine("test");
            return Model != null;
        }
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SomeCommand.RaiseCanExecuteChanged();
        }

        Objs _model;
        public Objs Model
        {
            get { return _model; }
            set
            {
                if (_model != null)
                    _model.PropertyChanged -= Model_PropertyChanged;

                _model = value;
                if (_model != null)
                    _model.PropertyChanged += Model_PropertyChanged;

                OnPropertyChanged();

            }
        }

        private string _imgSrc;
        public string ImgSrc
        {
            get { return _imgSrc; }
            set
            {
                if (_imgSrc == value) return;
                _imgSrc = value;
                OnPropertyChanged();
            }
        }

        public enum SearchMode
        {
            ObjSearch,
            RefSearch,
            IdSearch,
            Map
        }


    }
}
