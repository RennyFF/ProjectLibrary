﻿#pragma checksum "..\..\..\..\..\..\Utils\Components\CustomView\BooksSlider.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DDABE7DCEFFC94942E4C97416F81558D3627EB3C"
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
using ProjectLibrary.Utils.Components.CustomView;
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


namespace ProjectLibrary.Utils.Components.CustomView {
    
    
    /// <summary>
    /// BooksSlider
    /// </summary>
    public partial class BooksSlider : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\..\..\Utils\Components\CustomView\BooksSlider.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ProjectLibrary.Utils.Components.CustomView.BooksSlider This;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\..\Utils\Components\CustomView\BooksSlider.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ScrollArea;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\..\..\Utils\Components\CustomView\BooksSlider.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LeftArrow;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\..\..\..\Utils\Components\CustomView\BooksSlider.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RightArrow;
        
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
            System.Uri resourceLocater = new System.Uri("/ProjectLibrary;component/utils/components/customview/booksslider.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Utils\Components\CustomView\BooksSlider.xaml"
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
            this.This = ((ProjectLibrary.Utils.Components.CustomView.BooksSlider)(target));
            return;
            case 2:
            this.ScrollArea = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 3:
            this.LeftArrow = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\..\..\..\Utils\Components\CustomView\BooksSlider.xaml"
            this.LeftArrow.Click += new System.Windows.RoutedEventHandler(this.LeftArrow_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.RightArrow = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\..\..\..\..\Utils\Components\CustomView\BooksSlider.xaml"
            this.RightArrow.Click += new System.Windows.RoutedEventHandler(this.RightArrow_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

