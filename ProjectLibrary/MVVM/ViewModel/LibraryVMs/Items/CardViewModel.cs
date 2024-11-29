using Npgsql;
using ProjectLibrary.Core;
using ProjectLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs.Items
{
    class CardViewModel : Core.BaseViewModel
    {
        private RelayCommand goToPreview;
        public RelayCommand GoToPreview
        {
            get
            {
                return goToPreview ??= new RelayCommand(obj =>
                {
                    if (obj is int BookId)
                    {
                        LibraryNavigation.NavigateLibraryTo<PreviewBookViewModel>(BookId);
                    }
                }, obj => true);
            }
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
        public CardViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
        }
    }
}
