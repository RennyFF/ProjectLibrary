using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Max 3 items
            var items = new List<Item>();

            items.Add(new Item
            {
                Name = "Роман",
            });
            items.Add(new Item
            {
                Name = "Роман",
            });
            items.Add(new Item
            {
                Name = "Детские приключенческие книги",
            });
            GenreBox.ItemsSource = items;
        }

        public class Item
        {
            public string Name { get; set; }
            public int RandomWidth { get; set; }
        }
        private void DragArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ResizeToFullScreen_Click(object sender, RoutedEventArgs e)
        {
            Resize();
        }
        
        private void Resize()
        {
            Thickness thicknessSmall = new Thickness(0);
            Thickness thicknessFS = new Thickness(7);
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                Tools.Margin = thicknessSmall;
            }
            else if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                Tools.Margin = thicknessFS;
            }
        }

        private void HideWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}