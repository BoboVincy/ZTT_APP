using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using ZdfFlatUI.Primitives;

namespace ZdfFlatUI
{
    public class ZDataGrid:DataGrid
    {
        static ZDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZDataGrid), new FrameworkPropertyMetadata(typeof(ZDataGrid)));                                  
        }
    }
}
