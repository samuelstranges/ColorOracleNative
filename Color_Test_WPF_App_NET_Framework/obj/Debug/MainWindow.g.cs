﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7E4BBEA42163A369D0927C098CF87AFEA09164ECC63C6516CB75991778A22C4E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Color_Test_WPF_App_NET_Framework;
using NHotkey.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Color_Test_WPF_App_NET_Framework {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Logo;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button duButton;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem settingsAbout;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem settingsFeed;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem settingsHelp;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem settingsDeu;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem settingsPro;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem settingsTri;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem settingsGr;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem AltKeyShortBut;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label chosenType;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ColorOracle;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\MainWindow.xaml"
            ((Color_Test_WPF_App_NET_Framework.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.dontClose);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Logo = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            
            #line 51 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.setTri);
            
            #line default
            #line hidden
            return;
            case 4:
            this.duButton = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\MainWindow.xaml"
            this.duButton.Click += new System.Windows.RoutedEventHandler(this.setDeu);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 70 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.setPro);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 79 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.setGr);
            
            #line default
            #line hidden
            return;
            case 7:
            this.settingsAbout = ((System.Windows.Controls.MenuItem)(target));
            
            #line 95 "..\..\MainWindow.xaml"
            this.settingsAbout.Click += new System.Windows.RoutedEventHandler(this.openAbout);
            
            #line default
            #line hidden
            return;
            case 8:
            this.settingsFeed = ((System.Windows.Controls.MenuItem)(target));
            
            #line 96 "..\..\MainWindow.xaml"
            this.settingsFeed.Click += new System.Windows.RoutedEventHandler(this.openSendFeedback);
            
            #line default
            #line hidden
            return;
            case 9:
            this.settingsHelp = ((System.Windows.Controls.MenuItem)(target));
            
            #line 97 "..\..\MainWindow.xaml"
            this.settingsHelp.Click += new System.Windows.RoutedEventHandler(this.openHelp);
            
            #line default
            #line hidden
            return;
            case 10:
            this.settingsDeu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 102 "..\..\MainWindow.xaml"
            this.settingsDeu.Click += new System.Windows.RoutedEventHandler(this.setDeu);
            
            #line default
            #line hidden
            return;
            case 11:
            this.settingsPro = ((System.Windows.Controls.MenuItem)(target));
            
            #line 103 "..\..\MainWindow.xaml"
            this.settingsPro.Click += new System.Windows.RoutedEventHandler(this.setPro);
            
            #line default
            #line hidden
            return;
            case 12:
            this.settingsTri = ((System.Windows.Controls.MenuItem)(target));
            
            #line 104 "..\..\MainWindow.xaml"
            this.settingsTri.Click += new System.Windows.RoutedEventHandler(this.setTri);
            
            #line default
            #line hidden
            return;
            case 13:
            this.settingsGr = ((System.Windows.Controls.MenuItem)(target));
            
            #line 105 "..\..\MainWindow.xaml"
            this.settingsGr.Click += new System.Windows.RoutedEventHandler(this.setGr);
            
            #line default
            #line hidden
            return;
            case 14:
            this.AltKeyShortBut = ((System.Windows.Controls.MenuItem)(target));
            
            #line 107 "..\..\MainWindow.xaml"
            this.AltKeyShortBut.Click += new System.Windows.RoutedEventHandler(this.changeShortcuts);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 111 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.clickWindow);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 112 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Screenshot);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 116 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Screenshot);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 126 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.clickWindow);
            
            #line default
            #line hidden
            return;
            case 19:
            this.chosenType = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

