using Microsoft.Extensions.DependencyInjection;
using ProjectLibrary.MVVM.View;
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
            services.AddSingleton<BaseWindow>(provider => new BaseWindow
            {
                DataContext = provider.GetRequiredService<BaseVM>()
            });
            services.AddSingleton<BaseVM>();
            services.AddSingleton<AuthViewModel>();
            services.AddSingleton<LibraryViewModel>();
            //services.AddSingleton<LibraryViewModel>(provider => new LibraryViewModel());
            services.AddSingleton<INavigationService, NavigationService>();

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
