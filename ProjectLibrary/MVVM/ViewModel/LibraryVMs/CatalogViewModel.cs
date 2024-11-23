using Npgsql;
using ProjectLibrary.Utils;
using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class CatalogViewModel : Core.BaseViewModel
    {
		private List<Genre> allGenresFilter;

		public List<Genre> AllGenresFilter
        {
			get { return allGenresFilter; }
			set { allGenresFilter = value; }
		}
        private ILibraryNavigationService _libraryNavigation;
        public ILibraryNavigationService LibraryNavigation
        {
            get => _libraryNavigation;
            set
            {
                _libraryNavigation = value;
                onPropertyChanged();
            }
        }
        public CatalogViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            InitMainViewModel(connection);
        }

        private async void InitMainViewModel(NpgsqlConnection connection)
        {
            AllGenresFilter = await Model.DataBaseFunctions.GetAllGenres(connection);
        }

    }
}
