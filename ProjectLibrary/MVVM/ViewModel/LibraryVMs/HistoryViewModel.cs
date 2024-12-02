using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.Utils.Types;
using ProjectLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    if (obj is BookCard SelectedBook)
                    {
                        Constants.PreviousVM = PreviousViewModels.HistoryVM;
                        HistoryCache.AppendHistoryCacheAll(SelectedBook);
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(SelectedBook.BookId);
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
            HistoryBooks = await Task.Run(() => Model.DataBaseFunctions.GetBooksFromHistory(ConnectionDB, GettingBooks.bookHistory));
            HistoryAuthors = await Task.Run(() => Model.DataBaseFunctions.GetAuthorsFromHistory(ConnectionDB, GettingBooks.authorHistory));
            HistoryGenres = await Task.Run(() => Model.DataBaseFunctions.GetGenreFromHistory(ConnectionDB, GettingBooks.genreHistory));
        }
    }
}
