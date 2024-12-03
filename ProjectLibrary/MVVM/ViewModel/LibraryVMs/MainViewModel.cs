using Npgsql;
using ProjectLibrary.Utils.Types;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;
using ProjectLibrary.Core;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class MainViewModel : Core.BaseViewModel
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
        private ObservableCollection<BookCard> newBooksCategory = new();

        public ObservableCollection<BookCard> NewBooksCategory
        {
            get { return newBooksCategory; }
            set { newBooksCategory = value; }
        }

        private ObservableCollection<BookCard> bestSellerCategory = new();
        public ObservableCollection<BookCard> BestSellerCategory
        {
            get { return bestSellerCategory; }
            set { bestSellerCategory = value; }
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
                        Constants.PreviousVM = PreviousViewModels.MainVM;
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(SelectedBook.Id);
                    }
                }, obj => true);
            }
        }
        private RelayCommand goToMagicBook;
        public RelayCommand GoToMagicBook
        {
            get
            {
                return goToMagicBook ??= new RelayCommand(async obj =>
                {
                    Constants.PreviousVM = PreviousViewModels.MainVM;
                    LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(await Task.Run( () => Model.DataBaseFunctions.GetMagicBook(ConnectionDB)));
                }, obj => true);
            }
        }
        #endregion
        public MainViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            ConnectionDB = connection;
            InitMainViewModel(connection);
        }

        private async void InitMainViewModel(NpgsqlConnection connection)
        {
            var GettingBooksFirstCategroy = await Model.DataBaseFunctions.GetNewBooks(connection);
            await Task.Run(() => NewBooksCategory = GettingBooksFirstCategroy);
            await Task.Run(() => onPropertyChanged(nameof(NewBooksCategory)));
            var GettingBooksSecondCategroy = await Model.DataBaseFunctions.GetBestSellers(connection);
            await Task.Run(() => BestSellerCategory = GettingBooksSecondCategroy);
            await Task.Run(() => onPropertyChanged(nameof(BestSellerCategory)));
        }
    }
}
