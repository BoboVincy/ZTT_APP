﻿#pragma checksum "..\..\..\OPCClient\ClientMainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2B8DE81340D6258DEB3A3F0EC88B7EAF65271F7B"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Model.OPCClientModel;
using Panuon.UI.Silver;
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


namespace View.OPCClient {
    
    
    /// <summary>
    /// ClientMainWindow
    /// </summary>
    public partial class ClientMainWindow : Panuon.UI.Silver.WindowX, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Root;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonGet;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboNode;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboName;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonConnect;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Panuon.UI.Silver.Loading NodeLoding;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox DeviceListBox;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAdd;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonDelete;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\OPCClient\ClientMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridTag;
        
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
            System.Uri resourceLocater = new System.Uri("/View;component/opcclient/clientmainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\OPCClient\ClientMainWindow.xaml"
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
            this.Root = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.ButtonGet = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\OPCClient\ClientMainWindow.xaml"
            this.ButtonGet.Click += new System.Windows.RoutedEventHandler(this.GetNodeClicked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ComboNode = ((System.Windows.Controls.ComboBox)(target));
            
            #line 37 "..\..\..\OPCClient\ClientMainWindow.xaml"
            this.ComboNode.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SelectedToGetServerName);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ComboName = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.ButtonConnect = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\OPCClient\ClientMainWindow.xaml"
            this.ButtonConnect.Click += new System.Windows.RoutedEventHandler(this.ConnectClicked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.NodeLoding = ((Panuon.UI.Silver.Loading)(target));
            return;
            case 7:
            this.DeviceListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.ButtonAdd = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\..\OPCClient\ClientMainWindow.xaml"
            this.ButtonAdd.Click += new System.Windows.RoutedEventHandler(this.AddTagClicked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ButtonDelete = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\OPCClient\ClientMainWindow.xaml"
            this.ButtonDelete.Click += new System.Windows.RoutedEventHandler(this.DeleteTagClicked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.GridTag = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

