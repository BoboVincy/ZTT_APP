using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModbusClient
{
    public static class MyDelegate
    {
        public delegate void DSetTimer();
        public static DSetTimer dSetTimer;
        public static DSetTimer dSetTCPRecieveTimer;

        public delegate ModbusBase DSetModbusBLL();
        public static DSetModbusBLL dSetModbusBLL;
    }
}
