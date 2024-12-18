﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ProjectLibrary.MVVM.View.LibraryViews
{
    /// <summary>
    /// Логика взаимодействия для MainPageView.xaml
    /// </summary>
    /// 
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
    public partial class MainPageView : UserControl
    {
        private double StepOffsetX { get; set; } = 320;
        public MainPageView()
        {
            InitializeComponent();
        }

            private void SmoothScrollToHorizontalOffset(ScrollViewer ScrollArea, double TargetOffset)
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
                double CurrentOffsetX = (((Button)sender).Tag as ScrollViewer).HorizontalOffset - StepOffsetX;
                SmoothScrollToHorizontalOffset((((Button)sender).Tag as ScrollViewer), CurrentOffsetX);
            }

            private void RightArrow_Click(object sender, RoutedEventArgs e)
            {
                double CurrentOffsetX = (((Button)sender).Tag as ScrollViewer).HorizontalOffset + StepOffsetX;
                SmoothScrollToHorizontalOffset((((Button)sender).Tag as ScrollViewer), CurrentOffsetX);
            }
        }
    }
