﻿#pragma checksum "..\..\..\OTSV\OTSSettingPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9B115564220A0876FCAD3D572BA311EF61DB5915"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using OTS.OTSV;
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
using ZdfFlatUI;


namespace OTS.OTSV {
    
    
    /// <summary>
    /// OTSSettingPage
    /// </summary>
    public partial class OTSSettingPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\OTSV\OTSSettingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZdfFlatUI.ZComboBox KepServerNodeList;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\OTSV\OTSSettingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZdfFlatUI.ZComboBox ChannelList;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\OTSV\OTSSettingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZdfFlatUI.ZComboBox DeviceList;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\OTSV\OTSSettingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox KepServerTagView;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\OTSV\OTSSettingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ShowViewList;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\OTSV\OTSSettingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZdfFlatUI.ZComboBox DataTableList;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\OTSV\OTSSettingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox TableFieldView;
        
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
            System.Uri resourceLocater = new System.Uri("/OPC结转程序;component/otsv/otssettingpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\OTSV\OTSSettingPage.xaml"
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
            this.KepServerNodeList = ((ZdfFlatUI.ZComboBox)(target));
            
            #line 31 "..\..\..\OTSV\OTSSettingPage.xaml"
            this.KepServerNodeList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ClickServerChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ChannelList = ((ZdfFlatUI.ZComboBox)(target));
            
            #line 32 "..\..\..\OTSV\OTSSettingPage.xaml"
            this.ChannelList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ClickChannelChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DeviceList = ((ZdfFlatUI.ZComboBox)(target));
            
            #line 33 "..\..\..\OTSV\OTSSettingPage.xaml"
            this.DeviceList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ClickDeviceChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 34 "..\..\..\OTSV\OTSSettingPage.xaml"
            ((ZdfFlatUI.FlatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickServerRead);
            
            #line default
            #line hidden
            return;
            case 5:
            this.KepServerTagView = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            
            #line 45 "..\..\..\OTSV\OTSSettingPage.xaml"
            ((ZdfFlatUI.FlatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickSave);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ShowViewList = ((System.Windows.Controls.ListBox)(target));
            
            #line 46 "..\..\..\OTSV\OTSSettingPage.xaml"
            this.ShowViewList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ClickSelectedResult);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 64 "..\..\..\OTSV\OTSSettingPage.xaml"
            ((ZdfFlatUI.FlatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickImport);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 65 "..\..\..\OTSV\OTSSettingPage.xaml"
            ((ZdfFlatUI.FlatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickAutoPair);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 101 "..\..\..\OTSV\OTSSettingPage.xaml"
            ((ZdfFlatUI.FlatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickAddItem);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 102 "..\..\..\OTSV\OTSSettingPage.xaml"
            ((ZdfFlatUI.FlatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickDeleteItem);
            
            #line default
            #line hidden
            return;
            case 12:
            this.DataTableList = ((ZdfFlatUI.ZComboBox)(target));
            
            #line 113 "..\..\..\OTSV\OTSSettingPage.xaml"
            this.DataTableList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ClickTableChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 114 "..\..\..\OTSV\OTSSettingPage.xaml"
            ((ZdfFlatUI.FlatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickTableRead);
            
            #line default
            #line hidden
            return;
            case 14:
            this.TableFieldView = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

