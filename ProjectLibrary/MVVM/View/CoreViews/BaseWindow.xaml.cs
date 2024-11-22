using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectLibrary.MVVM.View.CoreViews
{
    /// <summary>
    /// Логика взаимодействия для BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {
            InitializeComponent();
        }
        private void DragArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ResizeToFullScreen_Click(object sender, RoutedEventArgs e)
        {
            Resize();
        }

        private void Resize()
        {
            Thickness thicknessSmall = new Thickness(0);
            Thickness thicknessFS = new Thickness(0, 7, 7, 0);
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
