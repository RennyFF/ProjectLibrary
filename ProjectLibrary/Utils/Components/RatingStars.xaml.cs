using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            StarsPanel.Children.Clear();
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
