using Npgsql;
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
    class MainViewModel : Core.BaseViewModel
    {
        private ObservableCollection<Cards> allBooks = new();

        public ObservableCollection<Cards> AllBooks
        {
            get { return allBooks; }
            set { allBooks = value; }
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
        public MainViewModel(ILibraryNavigationService libraryNavigation, NpgsqlConnection connection)
        {
            LibraryNavigation = libraryNavigation;
            var booksall = Task.Run(() => Model.DataBaseFunctions.GetAllBooks(connection)).Result;
            foreach (var item in booksall)
            {
                Cards newCard = new Cards() { Author = item.Author.SecondName + " " + item.Author.FirstName[0] + "." + item.Author.PatronomycName[0] + ".", Title = item.Title, ImageSource = item.Image };
                AllBooks.Add(newCard);
            }
        }
    }
}
