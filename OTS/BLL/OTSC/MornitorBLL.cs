using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.OTSC
{
    /// <summary>
    /// 监控事件类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MornitorBLL<T>
    {
        /// <summary>
        /// 判断前两个值是否等于后一个值
        /// </summary>
        /// <param name="Value1"></param>
        /// <param name="Value2"></param>
        /// <param name="Value"></param>
        public void ValueEqual(T Value1,T Value2,T Value)
        {
            if (Value1.Equals(Value) &Value1.Equals(Value2))
            {
                WhenMyValueEqualed();
            }
        }

        /// <summary>
        /// 声明一个事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        public delegate void MyValueEqualed(object Sender, EventArgs e);
        public event MyValueEqualed OnMyValueEqualed;

        private void WhenMyValueEqualed()
        {
            if (OnMyValueEqualed != null)
            {
                OnMyValueEqualed(this, null);
            }
        }
    }
}
