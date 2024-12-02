using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.MVVM.ViewModel.CoreVMs;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class PreviewBookViewModel : Core.BaseViewModel
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
        private Book previewedBook;

        public Book PreviewedBook
        {
            get { return previewedBook; }
            set { previewedBook = value; }
        }
        private bool isOwned;

        public bool IsOwned
        {
            get { return isOwned; }
            set { isOwned = value; onPropertyChanged(); }
        }
        private bool isFavorite;

        public bool IsFavorite
        {
            get { return isFavorite; }
            set { isFavorite = value; onPropertyChanged(); }
        }
        #endregion
        #region Commands
        private RelayCommand goBack;
        public RelayCommand GoBack
        {
            get
            {
                return goBack ??= new RelayCommand(obj =>
                {
                    switch (Constants.PreviousVM)
                    {
                        case PreviousViewModels.CatalogVM:
                            LibraryNavigation.NavigateLibraryTo<CatalogViewModel>();
                            break;
                        case PreviousViewModels.MainVM:
                            LibraryNavigation.NavigateLibraryTo<MainViewModel>();
                            break;
                        case PreviousViewModels.HistoryVM:
                            LibraryNavigation.NavigateLibraryTo<HistoryViewModel>();
                            break;
                        default:
                            break;
                    }
                }, obj => true);
            }
        }
        private RelayCommand changeFavoriteValue;
        public RelayCommand ChangeFavoriteValue
        {
            get
            {
                return changeFavoriteValue ??= new RelayCommand(obj =>
                {
                    IsFavorite = !IsFavorite;
                    Task.Run(() => Model.DataBaseFunctions.ChangeFavoriteBook(ConnectionDB, Constants.ActiveUserId, PreviewedBook.Id, IsFavorite));
                }, obj => true);
            }
        }
        #endregion
        public PreviewBookViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            ConnectionDB = connection;
        }
        public async void GetPreviewedBook(int BookId)
        {
            await Task.Run(async () => PreviewedBook = await Model.DataBaseFunctions.GetFullBook(ConnectionDB, BookId));
            await Task.Run(async () => IsOwned = await Model.DataBaseFunctions.CheckIfOwned(ConnectionDB, Constants.ActiveUserId, BookId));
            await Task.Run(async () => IsFavorite = await Model.DataBaseFunctions.CheckIfFavorite(ConnectionDB, Constants.ActiveUserId, BookId));
        }
    }
}
