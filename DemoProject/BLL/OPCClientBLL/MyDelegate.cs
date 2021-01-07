using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.OPCClientBLL
{
    public static class MyDelegate
    {
        public delegate  void  DShowMessage(string Content, string Tittle);

        public static DShowMessage dShowMessageBox;

        public delegate void DControlLoading(bool State);

        public static DControlLoading dControlLoading;
    }
}
