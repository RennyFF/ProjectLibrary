using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.MVVM.ViewModel.CoreVMs;
using ProjectLibrary.MVVM.ViewModel.LibraryVMs;
using ProjectLibrary.MVVM.ViewModel.LibraryVMs.Items;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Types;
using System.Windows;

namespace ProjectLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public User ActiveUser { get; set; }
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<NpgsqlConnection>(provider =>
            {
                var connection = new NpgsqlConnection(Constants.ConnectionStringDB);
                connection.Open();
                return connection;
            });
            services.AddSingleton<BaseWindow>(provider => new BaseWindow
            {
                DataContext = provider.GetRequiredService<BaseVM>()
            });
            services.AddSingleton<BaseVM>();
            services.AddSingleton<AuthViewModel>();
            services.AddSingleton<RegViewModel>();
            services.AddSingleton<LibraryViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<CatalogViewModel>();
            services.AddSingleton<PreviewBookViewModel>();
            services.AddSingleton<CardViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ILibraryNavigationService, LibraryNavigationService>();

            services.AddSingleton<Func<Type, Core.BaseViewModel>>(serviceProvider =>
                viewModelType => (Core.BaseViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<BaseWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
