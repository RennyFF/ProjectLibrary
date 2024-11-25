using MahApps.Metro.IconPacks;
using ProjectLibrary.Utils.Components.CustomView;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectLibrary.Utils.Components
{
    /// <summary>
    /// Логика взаимодействия для RatingStars.xaml
    /// </summary>
    public partial class RatingStars : UserControl
    {
        private SolidColorBrush GrayColor = new SolidColorBrush(Color.FromRgb(80, 80, 80));
        private SolidColorBrush OrangeColor = new SolidColorBrush(Color.FromRgb(255, 149, 0));
        public int Stars
        {
            get { return (int)GetValue(StarsProperty); }
            set { SetValue(StarsProperty, value); }
        }
        public static readonly DependencyProperty StarsProperty =
            DependencyProperty.Register("Stars", typeof(int), typeof(RatingStars), new PropertyMetadata(0, OnStarsChanged));
        private static void OnStarsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as RatingStars;
            control?.SetUpRating((int)d.GetValue(StarsProperty));
        }
        public RatingStars()
        {
            InitializeComponent();
        }
        private void SetUpRating(int Rating)
        {
            int EmptyRating = 5 - Rating;
            for (int i = 1; i <= Rating; i++)
            {
                var starIcon = new PackIconFontAwesome
                {
                    Kind = PackIconFontAwesomeKind.StarSolid,
                    Foreground = OrangeColor,
                    Width = 16,
                    Height = 16
                };
                StarsPanel.Children.Add(starIcon);
            }
            if (EmptyRating != 0)
            {
                for (int i = 1; i <= EmptyRating; i++)
                {
                    var starIcon = new PackIconFontAwesome
                    {
                        Kind = PackIconFontAwesomeKind.StarSolid,
                        Foreground = GrayColor,
                        Width = 16,
                        Height = 16
                    };
                    StarsPanel.Children.Add(starIcon);
                }
            }
        }
    }
}
