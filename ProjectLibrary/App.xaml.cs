using Microsoft.Extensions.DependencyInjection;
using ProjectLibrary.MVVM.ViewModel;
using ProjectLibrary.Utils;
using System.Windows;

namespace ProjectLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<AuthViewModel>(provider => new AuthViewModel());
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, Core.BaseViewModel>>(serviceProvider =>
                viewModelType => (Core.BaseViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
