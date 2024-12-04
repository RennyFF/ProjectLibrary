using System.Globalization;
using System.Windows.Data;
using System.Windows;

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
}
