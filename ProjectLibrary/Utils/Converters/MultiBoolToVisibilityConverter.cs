using System.Globalization;
using System.Windows.Data;
using System.Windows;
using ProjectLibrary.MVVM.ViewModel.LibraryVMs;

namespace ProjectLibrary.Utils.Converters
{
    public class MultiBoolToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || !(values[0] is bool) || !(values[1] is bool))
                return Visibility.Collapsed;

            bool isLoading = (bool)values[0];
            bool isHasItems = (bool)values[1];

            string condition = parameter as string;

            return condition switch
            {
                "Loading" => isLoading ? Visibility.Visible : Visibility.Collapsed,
                "ZeroContent" => !isLoading && !isHasItems ? Visibility.Visible : Visibility.Collapsed,
                "ListBox" => !isLoading && isHasItems ? Visibility.Visible : Visibility.Collapsed,
                _ => Visibility.Collapsed,
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MultiBoolToVisibilitySearchConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is bool) || !(values[1] is bool))
                return Visibility.Collapsed;

            bool isLoading = (bool)values[0];
            bool isHasItems = (bool)values[1];
            string condition = parameter as string;

            if (values.Length < 3)
            {
                return condition switch
                {
                    "Loading" => isLoading ? Visibility.Visible : Visibility.Collapsed,
                    "ZeroContent" => !isLoading && !isHasItems ? Visibility.Visible : Visibility.Collapsed,
                    "PageButtons" => !isLoading && isHasItems ? Visibility.Visible : Visibility.Collapsed,
                    _ => Visibility.Collapsed,
                };
            }

            SearchType CurrentSearchType = (SearchType)values[2];
            return condition switch
            {
                "ListBoxAuthors" => !isLoading && isHasItems && CurrentSearchType == SearchType.AuthorSearch ? Visibility.Visible : Visibility.Collapsed,
                "ListBoxGenres" => !isLoading && isHasItems && CurrentSearchType == SearchType.GenreSearch ? Visibility.Visible : Visibility.Collapsed,
                "ListBoxBooks" => !isLoading && isHasItems && CurrentSearchType == SearchType.BookSearch ? Visibility.Visible : Visibility.Collapsed,
                _ => Visibility.Collapsed,
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
