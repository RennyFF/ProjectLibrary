using ProjectLibrary.Utils.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectLibrary.Utils.Components.CustomView
{
    public static class ScrollViewerBehavior
    {
        public static readonly DependencyProperty AnimatedHorizontalOffsetProperty =
            DependencyProperty.RegisterAttached("AnimatedHorizontalOffset", typeof(double), typeof(ScrollViewerBehavior),
                new PropertyMetadata(0.0, OnAnimatedHorizontalOffsetChanged));

        public static double GetAnimatedHorizontalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(AnimatedHorizontalOffsetProperty);
        }

        public static void SetAnimatedHorizontalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(AnimatedHorizontalOffsetProperty, value);
        }

        private static void OnAnimatedHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToHorizontalOffset((double)e.NewValue);
            }
        }
    }
    public partial class CardSlider : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CardSlider));
        public ObservableCollection<Cards> Books
        {
            get { return (ObservableCollection<Cards>)GetValue(BooksProperty); }
            set { SetValue(BooksProperty, value); }
        }
        public static readonly DependencyProperty BooksProperty =
            DependencyProperty.Register("Books", typeof(ObservableCollection<Cards>), typeof(CardSlider));

        private double StepOffsetX { get; set; } = 320;
        public CardSlider()
        {
            InitializeComponent();
        }

        private void SmoothScrollToHorizontalOffset(double TargetOffset)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = TargetOffset,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new QuadraticEase()
            };
            ScrollArea.BeginAnimation(ScrollViewerBehavior.AnimatedHorizontalOffsetProperty, animation);
        }


        private void LeftArrow_Click(object sender, RoutedEventArgs e)
        {
            double CurrentOffsetX = ScrollArea.HorizontalOffset - StepOffsetX;
            SmoothScrollToHorizontalOffset(CurrentOffsetX);
        }

        private void RightArrow_Click(object sender, RoutedEventArgs e)
        {
            double CurrentOffsetX = ScrollArea.HorizontalOffset + StepOffsetX;
            SmoothScrollToHorizontalOffset(CurrentOffsetX);
        }
    }
}