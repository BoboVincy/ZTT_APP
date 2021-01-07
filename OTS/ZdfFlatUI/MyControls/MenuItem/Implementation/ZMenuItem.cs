using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ZMenuItem : MenuItem
    {
        static ZMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZMenuItem), new FrameworkPropertyMetadata(typeof(ZMenuItem)));
        }
    }
}