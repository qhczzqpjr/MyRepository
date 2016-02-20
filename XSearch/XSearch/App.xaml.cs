using System.Windows;

namespace XSearch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    Microsoft.Practices.Prism.Mvvm.ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
        //    {
        //        var viewName = viewType.FullName;
        //        var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
        //        var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
        //        return Type.GetType(viewModelName);
        //    });
        //    //IUnityContainer _container = new UnityContainer();
        //    //ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
        //    //{
        //    //    return _container.Resolve(type);
        //    //});
        //}
    }
}
