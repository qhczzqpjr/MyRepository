using System.Windows.Input;
using XSearch.Business.Mapper;
using XSearch.Views;
using XSearch.ViewsModels;

namespace XSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow
    {
        readonly WindowResizer _ob;

        public MainWindow()
        {
            InitializeComponent();
            Mapper.CreateMaps();
            Mapper.CreateServiceMaps();
            _ob = new WindowResizer(this);
          

            ObjsView view = new ObjsView();
            ObjsViewModel model = new ObjsViewModel();
            view.DataContext = model;
            Placeholder.Content = view;

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            try
            {
                DragMove();
            }
            catch
            {
                // ignored
            }
        }

        private void Resize(object sender, MouseButtonEventArgs e)
        {
            _ob.resizeWindow(sender);
        }

        private void DisplayResizeCursor(object sender, MouseEventArgs e)
        {
            _ob.DisplayResizeCursor(sender);
        }

        private void ResetCursor(object sender, MouseEventArgs e)
        {
            _ob.ResetCursor();
        }

        private void Drag(object sender, MouseButtonEventArgs e)
        {
            _ob.DragWindow();
        }

    }
}
