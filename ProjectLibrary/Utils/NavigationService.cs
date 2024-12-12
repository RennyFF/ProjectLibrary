using ProjectLibrary.Core;
using ProjectLibrary.Core.Types;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.ViewModel.CoreVMs;
using ProjectLibrary.MVVM.ViewModel.LibraryVMs;

namespace ProjectLibrary.Utils
{
    public interface INavigationService
    {
        Core.BaseViewModel CurrentView { get; }
        void NavigateTo<T>(object? Param = null) where T : Core.BaseViewModel;
    }
    public class NavigationService : ObservableObject, INavigationService
    {
        private Core.BaseViewModel _currentView;
        private readonly Func<Type, Core.BaseViewModel> _viewModelFactory;

        public Core.BaseViewModel CurrentView
        {
            get { return _currentView; }
            private set { _currentView = value; onPropertyChanged(); }
        }


        public NavigationService(Func<Type, Core.BaseViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>(object Param = null) where TViewModel : Core.BaseViewModel
        {
            Core.BaseViewModel ViewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            if (ViewModel is LibraryViewModel LibraryViewModel && Param is UserShort UserDataContext)
            {
                LibraryViewModel.CurrentUser = UserDataContext;
            }
            CurrentView = ViewModel;
        }
    }
    public interface ILibraryNavigationService
    {
        Core.BaseViewModel CurrentLibraryView { get; }
        void NavigateLibraryTo<T>(object Param = null) where T : Core.BaseViewModel;
    }
    public class LibraryNavigationService : ObservableObject, ILibraryNavigationService
    {
        private Core.BaseViewModel _currentLibraryView;
        private readonly Func<Type, Core.BaseViewModel> _viewModelFactory;

        public Core.BaseViewModel CurrentLibraryView
        {
            get { return _currentLibraryView; }
            private set { _currentLibraryView = value; onPropertyChanged(); }
        }


        public LibraryNavigationService(Func<Type, Core.BaseViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateLibraryTo<TViewModel>(object Param = null) where TViewModel : Core.BaseViewModel
        {
            Core.BaseViewModel ViewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            if (ViewModel is PreviewBookViewModel previewBookViewModel && Param is int BookId)
            {
                previewBookViewModel.GetPreviewedBook(BookId);
            }
            else if (ViewModel is PreviewAuthorViewModel previewAuthorViewModel && Param is int AuthorId)
            {
                previewAuthorViewModel.GetPreviewedAuthor(AuthorId);
            }
            else if (ViewModel is PreviewGenreViewModel previewGenreViewModel && Param is int GenreId)
            {
                previewGenreViewModel.GetPreviewedGenre(GenreId);
            }
            CurrentLibraryView = ViewModel;
        }
    }
}
