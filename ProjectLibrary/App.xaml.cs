using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ProjectLibrary.Core.Types;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.MVVM.ViewModel.CoreVMs;
using ProjectLibrary.MVVM.ViewModel.LibraryVMs;
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
            services.AddSingleton<RegViewModel>();
            services.AddSingleton<LibraryViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddSingleton<CatalogViewModel>();
            services.AddSingleton<AuthorsViewModel>();
            services.AddSingleton<GenresViewModel>();
            services.AddSingleton<PreviewBookViewModel>();
            services.AddSingleton<PreviewAuthorViewModel>();
            services.AddSingleton<PreviewGenreViewModel>();
            services.AddTransient<HistoryViewModel>();
            services.AddTransient<FavoriteBooksViewModel>();
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
