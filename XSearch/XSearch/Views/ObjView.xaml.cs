using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XSearch.DAL.Repository;
using XSearch.Business.Models;
using Microsoft.Practices.Prism.Mvvm;

namespace XSearch.Views
{
    /// <summary>
    /// Interaction logic for ObjView.xaml
    /// </summary>
    public partial class ObjView : UserControl,IView
    {
        public ObjView()
        {
            InitializeComponent();
        }
        private void MenuItem_Click_Insert(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(this.tbkey.Text) || String.IsNullOrEmpty(this.tbValue.Text)))
            {
                using (XSearchDBManagementRepository repo = new XSearchDBManagementRepository())
                {
                    repo.Insert<Obj>(new Obj() { Key = tbkey.Text, Value = tbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                }
            }
        }

        private void MenuItem_Click_Update(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.tbId.Text))
            {
                using (XSearchDBManagementRepository repo = new XSearchDBManagementRepository())
                {
                    repo.Update<Obj>(new Obj() { Id = int.Parse(tbId.Text), Key = tbkey.Text, Value = tbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                }
            }
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.tbId.Text))
            {
                using (XSearchDBManagementRepository repo = new XSearchDBManagementRepository())
                {
                    repo.Delete<Obj>(new Obj() { Id = int.Parse(tbId.Text), Key = tbkey.Text, Value = tbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                }
            }
        }

        private void tbkey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.U && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                if (!String.IsNullOrEmpty(this.tbId.Text))
                {
                    using (XSearchDBManagementRepository repo = new XSearchDBManagementRepository())
                    {
                        repo.Update<Obj>(new Obj() { Id = int.Parse(tbId.Text), Key = tbkey.Text, Value = tbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                    }
                }
            }

            if (e.Key == Key.N && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                if (!(String.IsNullOrEmpty(this.tbkey.Text) || String.IsNullOrEmpty(this.tbValue.Text)))
                {
                    using (XSearchDBManagementRepository repo = new XSearchDBManagementRepository())
                    {
                        repo.Insert<Obj>(new Obj() { Key = tbkey.Text, Value = tbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                    }
                }
            }

            if (e.Key == Key.D && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                if (!String.IsNullOrEmpty(this.tbId.Text))
                {
                    using (XSearchDBManagementRepository repo = new XSearchDBManagementRepository())
                    {
                        repo.Delete<Obj>(new Obj() { Id = int.Parse(tbId.Text), Key = tbkey.Text, Value = tbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                    }
                }
            }
        }
    }
}
