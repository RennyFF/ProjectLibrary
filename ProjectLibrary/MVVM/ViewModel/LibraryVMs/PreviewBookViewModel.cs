using Npgsql;
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
            set {  isOwned = value; onPropertyChanged(); }
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
        }
    }
}
