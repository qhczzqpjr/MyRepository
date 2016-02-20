using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Windows.Input;
using XSearch.Business.Models;
using Microsoft.Practices.Prism.Mvvm;

namespace XSearch.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class ObjsView   : IView
    {
        public ObjsView()
        {
            InitializeComponent();
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
