using Npgsql;
using ProjectLibrary.Utils.Types;
using ProjectLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class MainViewModel : Core.BaseViewModel
    {
        #region Values
        private ObservableCollection<Cards> newBooksCategory = new();

        public ObservableCollection<Cards> NewBooksCategory
        {
            get { return newBooksCategory; }
            set { newBooksCategory = value; }
        }

        private ObservableCollection<Cards> bestSellerCategory = new();
        public ObservableCollection<Cards> BestSellerCategory
        {
            get { return bestSellerCategory; }
            set { bestSellerCategory = value; }
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
        #endregion
        public MainViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
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
