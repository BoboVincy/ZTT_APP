using System.Windows.Controls;
using System.Windows;


namespace ZdfFlatUI
{
    public class ZTitleMenuItem : MenuItem
    {

        static ZTitleMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZTitleMenuItem), new FrameworkPropertyMetadata(typeof(ZTitleMenuItem)));
        }
    }
}