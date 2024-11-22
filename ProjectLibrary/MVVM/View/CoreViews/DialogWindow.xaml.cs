using ProjectLibrary.MVVM.ViewModel.CoreVMs;
using System.Windows;

namespace ProjectLibrary.MVVM.View.CoreViews
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow(string HeaderMessage, string MainMessage)
        {
            InitializeComponent();
            DataContext = new DialogWindowViewModel(this, HeaderMessage, MainMessage);
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
