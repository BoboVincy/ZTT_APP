using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Common.UIPropertyModel
{
    public class UIProperty:INotifyPropertyChanged
    {

        #region 属性变化通知事件

        /// <summary>
        /// 属性变化通知事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性变化通知
        /// </summary>
        /// <param name="e"></param>
        public void OnPropertyChanged(string Element)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Element));
            }
        }

        #endregion
    }
}
