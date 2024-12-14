using ProjectLibrary.MVVM.ViewModel.LibraryVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Utils
{
    public static class BackToPreviousVM
    {
        public static void GoToPrevious(ILibraryNavigationService Navigation)
        {
            if (Constants.PreviousVM != null)
            {
                switch (Constants.PreviousVM.Last())
                {
                    case PreviousViewModels.MainVM:
                        Navigation.NavigateLibraryTo<MainViewModel>();
                        break;
                    case PreviousViewModels.CatalogVM:
                        Navigation.NavigateLibraryTo<CatalogViewModel>();
                        break;
                    case PreviousViewModels.GenresVM:
                        Navigation.NavigateLibraryTo<GenresViewModel>();
                        break;
                    case PreviousViewModels.AuthorsVM:
                        Navigation.NavigateLibraryTo<AuthorsViewModel>();
                        break;
                    case PreviousViewModels.HistoryVM:
                        Navigation.NavigateLibraryTo<HistoryViewModel>();
                        break;
                    case PreviousViewModels.FavoriteVM:
                        Navigation.NavigateLibraryTo<FavoriteBooksViewModel>();
                        break;
                    case PreviousViewModels.BookPreviewVM:
                        Navigation.NavigateLibraryTo<PreviewBookViewModel>();
                        break;
                    case PreviousViewModels.AuthorPreviewVM:
                        Navigation.NavigateLibraryTo<PreviewAuthorViewModel>();
                        break;
                    case PreviousViewModels.GenrePreviewVM:
                        Navigation.NavigateLibraryTo<PreviewGenreViewModel>();
                        break;
                    case PreviousViewModels.SearchVM:
                        Navigation.NavigateLibraryTo<SearchViewModel>();
                        break;
                    default:
                        break;
                }
                Constants.PreviousVM.Remove(Constants.PreviousVM.Last());
                return;
            }
        }
    }
}
