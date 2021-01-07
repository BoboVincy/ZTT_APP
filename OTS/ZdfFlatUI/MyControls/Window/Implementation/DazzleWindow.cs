using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZdfFlatUI.Utils;
using System.Windows;

namespace ZdfFlatUI
{
    public class DazzleWindow : Window
    {
        public DazzleWindow()
        {
            this.DefaultStyleKey = typeof(DazzleWindow);

            //缩放，最大化修复
            WindowBehaviorHelper wh = new WindowBehaviorHelper(this);
            wh.RepairBehavior();

            this.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(DazzleWindow_MouseLeftButtonDown);
            this.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(DazzleWindow_MouseRightButtonDown);
            this.Closed += new System.EventHandler(DazzleWindow_Closed);
        }
        public virtual void DazzleWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception)
            {
            }
        }

        public virtual void DazzleWindow_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if(e.ClickCount == 2)
                {
                    if (this.WindowState != System.Windows.WindowState.Maximized)
                    {
                        this.WindowState = System.Windows.WindowState.Maximized;
                    }
                    else
                    {
                        this.WindowState = System.Windows.WindowState.Normal;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void DazzleWindow_Closed(object sender, EventArgs e)
        {
            GC.Collect();
        }
    }
}
