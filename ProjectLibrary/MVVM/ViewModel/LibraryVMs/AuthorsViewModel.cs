using Grpc.Core;
using Grpc.Net.Client;
using ProjectLibrary.Client.Author;
using ProjectLibrary.Core;
using ProjectLibrary.Core.Types.Client;
using ProjectLibrary.MVVM.View.CoreViews;
using ProjectLibrary.Utils;
using System.Collections.ObjectModel;

namespace ProjectLibrary.MVVM.ViewModel.LibraryVMs
{
    class AuthorsViewModel : Core.BaseViewModel
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
        private bool isContentLoading;
        public bool IsContentLoading
        {
            get { return isContentLoading; }
            set { isContentLoading = value; onPropertyChanged(nameof(IsContentLoading)); }
        }
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; onPropertyChanged(nameof(IsLoading)); }
        }

        public List<string> AllSortFilter { get; set; } = new();
        public string SelectedSort { get; set; }

        public ObservableCollection<AuthorCardType> AllAuthors { get; set; } = new();

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
                    if (obj is AuthorCardType SelectedAuthor)
                    {
                        Constants.PreviousVM = new List<PreviousViewModels?>();
                        Constants.PreviousVM.Add(PreviousViewModels.AuthorsVM);
                        LibraryNavigation.NavigateLibraryTo<PreviewAuthorViewModel>(SelectedAuthor.Id);
                    }
                }, obj => true);
            }
        }

        private RelayCommand nextPage;
        public RelayCommand NextPage
        {
            get
            {
                return nextPage ??= new RelayCommand( async obj =>
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
                return previousPage ??= new RelayCommand( async obj =>
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
                    var sortAuthorsTask = SortAuthors(AllAuthors);
                    await Task.WhenAll(sortAuthorsTask);
                    await Task.Run(() => IsContentLoading = false);
                }, obj => IsLoading != true);
            }
        }
        #endregion
        #region AuthorsViewModelFunctionality
        private async void InitAuthorsViewModel()
        {
            await Task.Run(() => IsLoading = true);
            var loadMiscTask = LoadStartUp();
            var authorCountityTask = LoadPagesCountity();
            var loadAuthorsTask = ChangePage();
            await Task.WhenAll(loadMiscTask, authorCountityTask, loadAuthorsTask);
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
            var Client = new AuthorService.AuthorServiceClient(Channel);
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
        private async Task SortAuthors(ObservableCollection<AuthorCardType> UnSortedList)
        {
            switch (SelectedSort)
            {
                case "По алфавиту (А-Я)":
                    AllAuthors = new ObservableCollection<AuthorCardType>(UnSortedList.OrderBy(i => i.FullName));
                    await Task.Delay(100);
                    break;
                case "По алфавиту (Я-А)":
                    AllAuthors = new ObservableCollection<AuthorCardType>(UnSortedList.OrderByDescending(i => i.FullName));
                    await Task.Delay(100);
                    break;
                case "По актуальности":
                    AllAuthors = new ObservableCollection<AuthorCardType>(UnSortedList.OrderBy(i => i.Id));
                    await Task.Delay(100);
                    break;
                default:
                    break;
            }
            onPropertyChanged(nameof(AllAuthors));
        }
        private async Task ChangePage()
        {
            AllAuthors.Clear();
            using var Channel = GrpcChannel.ForAddress(Constants.ServerAdress);
            var Client = new AuthorService.AuthorServiceClient(Channel);
            try
            {
                ResponseAuthorsByPage response = await Client.GetAuthorsByPageAsync(new RequestAuthorsByPage { CountityOnPage = Constants.CountityOnPage, Page = CurrentPage });
                var UnsortedOC = new ObservableCollection<AuthorCardType>(response.Authors.Select(i => new AuthorCardType
                {
                    Id = i.Id,
                    FullName = i.AuthorFullnameShort,
                    ImageAvatar = i.Image.ToByteArray(),
                }));
                await SortAuthors(UnsortedOC);
            }
            catch (RpcException ex)
            {
                var ModalWindow = new DialogWindow("Ошибка!", $"{ex.Status.Detail}");
                ModalWindow.Show();
            }
        }
        #endregion
        public AuthorsViewModel(ILibraryNavigationService libraryNavigation)
        {
            LibraryNavigation = libraryNavigation;
            InitAuthorsViewModel();
        }
    }
}
