using Grpc.Core;
using Grpc.Net.Client;
using ProjectLibrary.Client.Genre;
using ProjectLibrary.Core;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class GenresViewModel : Core.BaseViewModel
    {
        #region Values
        private readonly Dictionary<int, string> SortDictionary = new() {
            { 1, "По актуальности" },
            { 2, "По алфавиту (А-Я)" },
            { 3, "По алфавиту (Я-А)" }
        };
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
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; onPropertyChanged(nameof(IsLoading)); }
        }
        private bool isContentLoading;
        public bool IsContentLoading
        {
            get { return isContentLoading; }
            set { isContentLoading = value; onPropertyChanged(nameof(IsContentLoading)); }
        }

        public List<string> AllSortFilter { get; set; } = new();
        public string SelectedSort { get; set; }

        public ObservableCollection<GenreCardType> AllGenres { get; set; } = new();
        public int AllPages { get; set; }

        private int currentPage = 1;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; onPropertyChanged(nameof(CurrentPage)); }
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
                    if (obj is GenreCardType SelectedGenre)
                    {
                        Constants.PreviousVM = new List<PreviousViewModels?>();
                        Constants.PreviousVM.Add(PreviousViewModels.GenresVM);
                        LibraryNavigation.NavigateLibraryTo<PreviewGenreViewModel>(SelectedGenre.Id);
                    }
                }, obj => true);
            }
        }

        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ??= new RelayCommand(async obj =>
                {
                    CurrentPage += 1;
                    await Task.Run(() => IsContentLoading = true);
                    var changePageTask = ChangePage();
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => CurrentPage != AllPages);
            }
        }
        private RelayCommand previousPage;
        public RelayCommand PreviousPage
        {
            get
            {
                return previousPage ??= new RelayCommand(async obj =>
                {
                    CurrentPage -= 1;
                    await Task.Run(() => IsContentLoading = true);
                    var changePageTask = ChangePage();
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => CurrentPage != 1);
            }
        }
        private RelayCommand firstPage;
        public RelayCommand FirstPage
        {
            get
            {
                return firstPage ??= new RelayCommand(async obj =>
                {
                    CurrentPage = 1;
                    await Task.Run(() => IsContentLoading = true);
                    var changePageTask = ChangePage();
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => (CurrentPage != 1));
            }
        }
        private RelayCommand lastPage;
        public RelayCommand LastPage
        {
            get
            {
                return lastPage ??= new RelayCommand(async obj =>
                {
                    CurrentPage = AllPages;
                    await Task.Run(() => IsContentLoading = true);
                    var changePageTask = ChangePage();
                    await Task.WhenAll(changePageTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => CurrentPage != AllPages);
            }
        }
        private RelayCommand sortChanged;
        public RelayCommand SortChanged
        {
            get
            {
                return sortChanged ??= new RelayCommand(async obj =>
                {
                    await Task.Run(() => IsContentLoading = true);
                    var sortGenreTask = SortGenres(AllGenres);
                    await Task.WhenAll(sortGenreTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => IsLoading != true);
            }
        }
        #endregion
        #region GenresViewModelFunctionality
        private async void InitGenresViewModel()
        {
            await Task.Run(() => IsLoading = true);
            var genresCountityTask = LoadPagesCountity();
            var loadGenresTask = ChangePage();
            var loadMiscTask = LoadStartUp();
            await Task.WhenAll(genresCountityTask, loadGenresTask, loadMiscTask);
            onPropertyChanged(nameof(SelectedSort));
            await Task.Run(() => IsLoading = false);
        }
        private async Task LoadStartUp()
        {
            foreach (var item in SortDictionary)
            {
                AllSortFilter.Add(item.Value);
            }
            SelectedSort = SortDictionary[1];
        }

        private async Task LoadPagesCountity()
        {
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new GenreService.GenreServiceClient(Channel);
            try
            {
                ResponseCountity response = await Client.GetCountityAsync(new RequestCountity() { CountityOnPage = Constants.CountityOnPage });
                AllPages = response.Countity;
                onPropertyChanged(nameof(AllPages));
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }

        private async Task SortGenres(ObservableCollection<GenreCardType> UnSortedList)
        {
            switch (SelectedSort)
            {
                case "По алфавиту (А-Я)":
                    AllGenres = new ObservableCollection<GenreCardType>(UnSortedList.OrderBy(i => i.GenreName));
                    await Task.Delay(100);
                    break;
                case "По алфавиту (Я-А)":
                    AllGenres = new ObservableCollection<GenreCardType>(UnSortedList.OrderByDescending(i => i.GenreName));
                    await Task.Delay(100);
                    break;
                case "По актуальности":
                    AllGenres = new ObservableCollection<GenreCardType>(UnSortedList.OrderBy(i => i.Id));
                    await Task.Delay(100);
                    break;
                default:
                    break;
            }
            onPropertyChanged(nameof(AllGenres));
        }
        private async Task ChangePage()
        {
            AllGenres.Clear();
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new GenreService.GenreServiceClient(Channel);
            try
            {
                ResponseGenresByPage response = await Client.GetGenresByPageAsync(new RequestGenresByPage { CountityOnPage = Constants.CountityOnPage, Page = CurrentPage });
                var UnsortedOC = new ObservableCollection<GenreCardType>(response.Genres.Select(i => new GenreCardType
                {
                    Id = i.Id,
                    GenreName = i.GenreName,
                    ImageAvatar = i.Image.ToByteArray(),
                }));
                await SortGenres(UnsortedOC);
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        #endregion
        public GenresViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
            InitGenresViewModel();
        }
    }
}
