using System;
using System.Windows;
using System.Windows.Input;
using XSearch.Business.Models;
using Microsoft.Practices.Prism.Mvvm;
using XSearch.Repository;

namespace XSearch.Views
{
    /// <summary>
    /// Interaction logic for ObjView.xaml
    /// </summary>
    public partial class ObjView : IView
    {
        public ObjView()
        {
            InitializeComponent();
        }
        private void MenuItem_Click_Insert(object sender, RoutedEventArgs e)
        {
            if (!(string.IsNullOrEmpty(this.Tbkey.Text) || string.IsNullOrEmpty(this.TbValue.Text)))
            {
                using (var repo = new XSearchDbManagementRepository())
                {
                    repo.Insert<Obj>(new Obj() { Key = Tbkey.Text, Value = TbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                }
            }
        }

        private void MenuItem_Click_Update(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TbId.Text))
            {
                using (var repo = new XSearchDbManagementRepository())
                {
                    repo.Update<Obj>(new Obj() { Id = int.Parse(TbId.Text), Key = Tbkey.Text, Value = TbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                }
            }
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TbId.Text))
            {
                using (var repo = new XSearchDbManagementRepository())
                {
                    repo.Delete<Obj>(new Obj() { Id = int.Parse(TbId.Text), Key = Tbkey.Text, Value = TbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                }
            }
        }

        private void tbkey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.U && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                if (!string.IsNullOrEmpty(this.TbId.Text))
                {
                    using (var repo = new XSearchDbManagementRepository())
                    {
                        repo.Update<Obj>(new Obj() { Id = int.Parse(TbId.Text), Key = Tbkey.Text, Value = TbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                    }
                }
            }

            if (e.Key == Key.N && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                if (!(string.IsNullOrEmpty(this.Tbkey.Text) || string.IsNullOrEmpty(this.TbValue.Text)))
                {
                    using (var repo = new XSearchDbManagementRepository())
                    {
                        repo.Insert<Obj>(new Obj() { Key = Tbkey.Text, Value = TbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                    }
                }
            }

            if (e.Key == Key.D && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                if (!string.IsNullOrEmpty(this.TbId.Text))
                {
                    using (var repo = new XSearchDbManagementRepository())
                    {
                        repo.Delete<Obj>(new Obj() { Id = int.Parse(TbId.Text), Key = Tbkey.Text, Value = TbValue.Text, CreateDateTime = DateTime.UtcNow }, true);
                    }
                }
            }
        }
    }
}
