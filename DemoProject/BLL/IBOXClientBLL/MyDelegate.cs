using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.IBOXClient;

namespace BLL.IBOXClientBLL
{
    public static class MyDelegate
    {
        public delegate void DShowMessage(string Content,string Tittle);
        public static DShowMessage dShowMessage;

        public delegate void DShowProcessMessage(string Content);
        public static DShowProcessMessage dShowProcessMessage;

        public delegate void DGetBoxinfo<T>(List<T> BoxInfo);
        public static DGetBoxinfo<FBoxDeviceItem> dGetBoxInfo;
        public static DGetBoxinfo<FBoxTagInfo> dGetTagInfo;

    }
}
