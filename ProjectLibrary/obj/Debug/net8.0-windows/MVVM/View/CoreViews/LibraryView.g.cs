﻿#pragma checksum "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9BB22C128CC8E8CF2E0D782C11DED09D61799CFD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
using ProjectLibrary.MVVM.ViewModel.CoreVMs;
using ProjectLibrary.Utils.Components;
using ProjectLibrary.Utils.Converters;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ProjectLibrary.MVVM.View.CoreViews {
    
    
    /// <summary>
    /// LibraryView
    /// </summary>
    public partial class LibraryView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LoadingView;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainViewGrid;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AccountInfo;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup DropdownPopup;
        
        #line default
        #line hidden
        
        
        #line 264 "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border LikedGenres;
        
        #line default
        #line hidden
        
        
        #line 276 "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LikedGenresText;
        
        #line default
        #line hidden
        
        
        #line 310 "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox FavGenreListBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Librars Cave;component/mvvm/view/coreviews/libraryview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LoadingView = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.MainViewGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.AccountInfo = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            
            #line 87 "..\..\..\..\..\..\MVVM\View\CoreViews\LibraryView.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.StackPanel_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DropdownPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 6:
            this.LikedGenres = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.LikedGenresText = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.FavGenreListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

