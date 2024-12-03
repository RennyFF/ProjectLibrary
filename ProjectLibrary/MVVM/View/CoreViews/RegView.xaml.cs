using System.Windows.Controls;

namespace ProjectLibrary.MVVM.View.CoreViews
{
    /// <summary>
    /// Логика взаимодействия для RegView.xaml
    /// </summary>

    public partial class RegView : UserControl
    {
        public RegView()
        {
            InitializeComponent();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as DatePicker).SelectedDate.ToString()))
            {
                (sender as DatePicker).SelectedDate = DateTime.Now;
            }
        }
    }
}
