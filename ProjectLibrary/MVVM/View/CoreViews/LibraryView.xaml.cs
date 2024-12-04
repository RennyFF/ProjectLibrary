using ProjectLibrary.MVVM.ViewModel.CoreVMs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ProjectLibrary.MVVM.View.CoreViews
{
    /// <summary>
    /// Логика взаимодействия для LibraryView.xaml
    /// </summary>
    public partial class LibraryView : UserControl
    {
        readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _dispatcherTimer.Tick += _dispatcherTimer_Tick1;
            _dispatcherTimer.Stop();

            _dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            _dispatcherTimer.Start();
        }

        private void _dispatcherTimer_Tick1(object sender, EventArgs e)
        {
            MainViewGrid.Visibility = Visibility.Visible;
            LoadingViewGrid.Visibility = Visibility.Hidden;
            _dispatcherTimer.Stop();
        }
        public LibraryView()
        {
            InitializeComponent();
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            _dispatcherTimer.Start();
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DropdownPopup.IsOpen = !DropdownPopup.IsOpen;
        }

        private void ListBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is LibraryViewModel viewModel)
            {
                viewModel.ListBoxHeight = FavGenreListBox.ActualHeight;
            }
        }
    }
}
