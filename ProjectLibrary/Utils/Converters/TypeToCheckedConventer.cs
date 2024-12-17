using ProjectLibrary.MVVM.ViewModel.LibraryVMs;
using System.Windows.Data;

namespace ProjectLibrary.Utils.Converters
{
    public class TypeToCheckedConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = false;
            if (value is SearchType && parameter is string)
            {
               switch (value) {
                    case SearchType.GenreSearch:
                        result = (string)parameter == "genreParam";
                        break;
                    case SearchType.AuthorSearch:
                        result = (string)parameter == "authorParam";
                        break;
                    case SearchType.BookSearch:
                        result = (string)parameter == "bookParam";
                        break;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
