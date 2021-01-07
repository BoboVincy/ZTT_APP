using System.Windows.Controls;
using System.Windows;


namespace ZdfFlatUI
{
    public class ZTitleMenu : Menu
    {

        static ZTitleMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZTitleMenu), new FrameworkPropertyMetadata(typeof(ZTitleMenu)));
        }
    }
}