using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModbusClient
{
    public class ModbusFactory
    {

        public ModbusBase ModbusBLL(string ModbusType)
        {
            switch (ModbusType)
            {
                case "RTU":
                    return new ModbusRTU();
                case "TCP":
                    return new ModbusTCP();
            }
            return null;
        }
    }
}
