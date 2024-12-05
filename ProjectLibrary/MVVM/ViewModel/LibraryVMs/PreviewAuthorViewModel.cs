using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.Utils.Types;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    public class PreviewAuthorViewModel : Core.BaseViewModel
    {
        #region Values
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
        private Author previewedAuthor;

        public Author PreviewedAuthor
        {
            get { return previewedAuthor; }
            set { previewedAuthor = value; onPropertyChanged(nameof(PreviewedAuthor)); }
        }

        private ObservableCollection<BookCard> auhorsBooks;

        public ObservableCollection<BookCard> AuhorsBooks
        {
            get { return auhorsBooks; }
            set { auhorsBooks = value; onPropertyChanged(nameof(AuhorsBooks)); }
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
                        Constants.PreviousVM.Add(PreviousViewModels.AuthorPreviewVM);
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
                    BackToPreviousVM.GoToPrevious(LibraryNavigation);
                }, obj => true);
            }
        }
        #endregion
        public PreviewAuthorViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            ConnectionDB = connection;
        }
        public async void GetPreviewedAuthor(int AuthorId)
        {
            HistoryCache.AppendHistoryCache(AuthorId, HistoryType.Author);
            await Task.Run(async () => AuhorsBooks = await Model.DataBaseFunctions.GetAllBookCards(ConnectionDB, AuthorId));
            await Task.Run(async () => PreviewedAuthor = await Model.DataBaseFunctions.GetSingleAuthorCard(ConnectionDB, AuthorId));
        }
    }
}
