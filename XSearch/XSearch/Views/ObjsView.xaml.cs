using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XSearch;
using XSearch.DAL;
using XSearch.DAL.Repository;
using XSearch.Business.Models;
using Microsoft.Practices.Prism.Mvvm;
using XSearch.ViewsModels;

namespace XSearch.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class ObjsView   : UserControl, IView
    {
        public ObjsView()
        {
            InitializeComponent();
            //var objView = new ObjView();
            //var objViewModel = new ObjViewModel();
            //objView.DataContext = objViewModel;

            //ItemsView = CollectionViewSource.GetDefaultView(StaticClass.objLists);
            //CbSearch.ItemsSource = StaticClass.objLists;
            //cbSearch.ItemsSource = StaticClass.objLists;
            //cbSearch.DisplayMemberPath = "Key";
            //cbSearch.SelectedValuePath = "Id";

            //iw.Hide();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CbSearch.Focus();
        }

        private void OnComboBoxTextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void cbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && CbSearch.SelectedIndex != -1)
            {
                 Clipboard.SetDataObject(((Obj)(CbSearch.Items.CurrentItem)).Value);
            }
        }
    }
}
