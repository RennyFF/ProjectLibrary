using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.Utils.Types;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class HistoryViewModel : Core.BaseViewModel
    {
        #region Values
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
        private NpgsqlConnection connectionDB;
        public NpgsqlConnection ConnectionDB
        {
            get => connectionDB;
            set => connectionDB = value;
        }
        private ObservableCollection<BookCard> historyBooks = new();

        public ObservableCollection<BookCard> HistoryBooks
        {
            get { return historyBooks; }
            set { historyBooks = value; onPropertyChanged(nameof(HistoryBooks)); }
        }

        private ObservableCollection<AuthorCard> historyAuthors = new();
        public ObservableCollection<AuthorCard> HistoryAuthors
        {
            get { return historyAuthors; }
            set { historyAuthors = value; onPropertyChanged(nameof(HistoryAuthors)); }
        }

        private ObservableCollection<GenreCard> historyGenres = new();
        public ObservableCollection<GenreCard> HistoryGenres
        {
            get { return historyGenres; }
            set { historyGenres = value; onPropertyChanged(nameof(HistoryGenres)); }
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
                    Constants.PreviousVM = PreviousViewModels.HistoryVM;
                    if (obj is BookCard SelectedBook)
                    {
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(SelectedBook.Id);
                        return;
                    }
                    else if (obj is AuthorCard SelectedAuthor)
                    {
                        LibraryNavigation.NavigateLibraryTo<PreviewAuthorViewModel>(SelectedAuthor.Id);
                        return;
                    }
                    else if (obj is GenreCard SelectedGenre)
                    {
                        LibraryNavigation.NavigateLibraryTo<PreviewGenreViewModel>(SelectedGenre.Id);
                        return;
                    }
                }, obj => true);
            }
        }
        #endregion
        public HistoryViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            ConnectionDB = connection;
            InitHistoryViewModel(connection);
        }

        private async void InitHistoryViewModel(NpgsqlConnection connection)
        {
            HistoryStruct GettingBooks = HistoryCache.GetHistory();
            HistoryBooks = await Task.Run(() => Model.DataBaseFunctions.GetAllBookCards(ConnectionDB, GettingBooks.bookHistory));
            HistoryAuthors = await Task.Run(() => Model.DataBaseFunctions.GetAllAuthorsCards(ConnectionDB, GettingBooks.authorHistory));
            HistoryGenres = await Task.Run(() => Model.DataBaseFunctions.GetAllGenreCards(ConnectionDB, GettingBooks.genreHistory));
        }
    }
}
