using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.Utils.Types;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    public class PreviewGenreViewModel : Core.BaseViewModel
    {
        #region Values
        private int CountityOnPage = 27;
        private NpgsqlConnection connectionDB;
        public NpgsqlConnection ConnectionDB
        {
            get => connectionDB;
            set => connectionDB = value;
        }
        private ILibraryNavigationService _libraryNavigation;
        public ILibraryNavigationService LibraryNavigation
        {
            get => _libraryNavigation;
            set
            {
                _libraryNavigation = value;
                onPropertyChanged(nameof(LibraryNavigation));
            }
        }
        private GenreCard previewedGenre;

        public GenreCard PreviewedGenre
        {
            get { return previewedGenre; }
            set { previewedGenre = value; onPropertyChanged(nameof(PreviewedGenre)); }
        }

        private ObservableCollection<BookCard> genreBooks = new ObservableCollection<BookCard>();

        public ObservableCollection<BookCard> GenreBooks
        {
            get { return genreBooks; }
            set { genreBooks = value; onPropertyChanged(nameof(GenreBooks)); }
        }
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; onPropertyChanged(nameof(IsLoading)); }
        }

        private int allPages;
        public int AllPages
        {
            get { return allPages; }
            set { allPages = value; onPropertyChanged(nameof(AllPages)); }
        }

        private int currentPage;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; onPropertyChanged(nameof(CurrentPage)); }
        }
        #endregion
        #region Commands

        private RelayCommand goToPreview;
        public RelayCommand GoToPreview
        {
            get
            {
                return goToPreview ??= new RelayCommand(obj =>
                {
                    if (obj is BookCard SelectedBook)
                    {
                        Constants.PreviousVM = PreviousViewModels.GenrePreviewVM;
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(SelectedBook.Id);
                    }
                }, obj => true);
            }
        }

        private RelayCommand goBack;
        public RelayCommand GoBack
        {
            get
            {
                return goBack ??= new RelayCommand(obj =>
                {
                    switch (Constants.PreviousVM)
                    {
                        case PreviousViewModels.MainVM:
                            LibraryNavigation.NavigateLibraryTo<MainViewModel>();
                            break;
                        case PreviousViewModels.HistoryVM:
                            LibraryNavigation.NavigateLibraryTo<HistoryViewModel>();
                            break;
                        case PreviousViewModels.BookPreviewVM:
                            LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>();
                            break;
                        default:
                            break;
                    }
                }, obj => true);
            }
        }
        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ??= new RelayCommand(obj =>
                {
                    CurrentPage += 1;
                    PageChanged();
                }, obj => CurrentPage != AllPages);
            }
        }
        private RelayCommand previousPage;
        public RelayCommand PreviousPage
        {
            get
            {
                return previousPage ??= new RelayCommand(obj =>
                {
                    CurrentPage -= 1;
                    PageChanged();
                }, obj => CurrentPage != 1);
            }
        }
        private RelayCommand firstPage;
        public RelayCommand FirstPage
        {
            get
            {
                return firstPage ??= new RelayCommand(obj =>
                {
                    CurrentPage = 1;
                    PageChanged();
                }, obj => (CurrentPage != 1));
            }
        }
        private RelayCommand lastPage;
        public RelayCommand LastPage
        {
            get
            {
                return lastPage ??= new RelayCommand(obj =>
                {
                    CurrentPage = AllPages;
                    PageChanged();
                }, obj => CurrentPage != AllPages);
            }
        }
        #endregion
        public PreviewGenreViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            ConnectionDB = connection;
        }
        public async void GetPreviewedGenre(int GenreId)
        {
            HistoryCache.AppendHistoryCache(GenreId, HistoryType.Genre);
            await Task.Run(async () => PreviewedGenre = await Model.DataBaseFunctions.GetFullGenre(ConnectionDB, GenreId));
            InitGenreViewModel(ConnectionDB);
        }
        private async void InitGenreViewModel(NpgsqlConnection connection)
        {
            CurrentPage = 1;
            AllPages = await Model.DataBaseFunctions.GetBooksCountity(connection, PreviewedGenre.Id);
            PageChanged();
        }
        #region BooksFunc
        private async void PageChanged()
        {
            GenreBooks.Clear();
            ChangeLoadingState();
            ObservableCollection<BookCard> gettingBooks = await Model.DataBaseFunctions.GetBooksByPageGenre(ConnectionDB, CurrentPage - 1, CountityOnPage, PreviewedGenre.Id);
            await Task.Run(
                () =>
                {
                    GenreBooks = gettingBooks; 
                    onPropertyChanged(nameof(GenreBooks));
                }
                );
            await Task.Run(() => ChangeLoadingState());
        }

        private void ChangeLoadingState()
        {
            IsLoading = !IsLoading;
        }
        #endregion
    }
}
