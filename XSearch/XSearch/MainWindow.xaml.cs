using System;
using System.Collections.Generic;
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
using XSearch.Business.Mapperings;
using XSearch.Business.Services;
using XSearch.Views;
using XSearch.ViewsModels;

namespace XSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        WindowResizer ob;

        public MainWindow()
        {
            InitializeComponent();
            Mapper.CreateMaps();
            Mapper.CreateServiceMaps();
            ob = new WindowResizer(this);
            //ObjsView view = new ObjsView();
            //ObjsViewModel model = new ObjsViewModel();
            //view.DataContext = model;
            //placeholder.Content = view;

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            try
            {
                this.DragMove();
            }
            catch { }

        }

        private void Resize(object sender, MouseButtonEventArgs e)
        {
            ob.resizeWindow(sender);
        }

        private void DisplayResizeCursor(object sender, MouseEventArgs e)
        {
            ob.displayResizeCursor(sender);
        }

        private void ResetCursor(object sender, MouseEventArgs e)
        {
            ob.resetCursor();
        }

        private void Drag(object sender, MouseButtonEventArgs e)
        {
            ob.dragWindow();
        }

    }
}
