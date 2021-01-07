using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OTSM
{
    public class OTSDataInfo
    {
        /// <summary>
        /// 键：需要结转的表
        /// 值：需要结转的字段-值字典（键：对应表的字段 值：字段对应的点的值）
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> BaseToServerDic
        {
            get; set;
        }
        /// <summary>
        /// 键：保存过的需要结转的表
        /// 值：需要结转的字段-值字典（键：对应的字段 值：字段对应的点的值）
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> SavedBaseToServerDic
        {
            get;set;
        }
        /// <summary>
        /// 键：Excel表中的数据表名称
        /// 值：Excel表中的表名称对应的设备
        /// </summary>
        public Dictionary<string, string> TableToDeviceDic
        {
            get; set;
        }
        /// <summary>
        /// 键：Excel表中的数据表名称
        /// 值：Excel表中数据表名称对应的字段列表
        /// </summary>
        public Dictionary<string, List<string>> TableToFieldListDic
        {
            get;set;
        }
        /// <summary>
        /// 键：Excel表中的数据表名称
        /// 值：Excel表中数据表名称对应的点列表
        /// </summary>
        public Dictionary<string, List<string>> TableToTagListDic
        {
            get;set;
        }
        /// <summary>
        /// 键：Excel表中的数据表名称
        /// 值：Excel表中的字段-点字典（键：该数据表中的字段 值：字段对应的值）
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> TableToFieldToTagDicDic
        {
            get;set;
        }
        /// <summary>
        /// 键：表
        /// 值：点对应得到标签值（用于索引采集点的值数组））
        /// </summary>
        public Dictionary<string,int> GatherFieldDic
        {
            get;set;
        }
        /// <summary>
        /// 需要采集的点列表
        /// </summary>
        public List<string> GatherTagList
        {
            get;set;
        }
        /// <summary>
        /// 键：需要结转的数据表
        /// 值：索引字典（字典：键：字段 值：索引）
        /// </summary>
        public Dictionary<string,Dictionary<string,int>> TableToFieldIndexDic
        {
            get;set;
        }
        /// <summary>
        /// 键：需要结转的数据表
        /// 值：该表对应的字段列表（从实际数据库中带出的字段列表）
        /// </summary>
        public Dictionary<string, List<string>> GatherTableToFeildList
        {
            get;set;
        }
        /// <summary>
        /// 键：需要结转的数据表
        /// 值：该表对应XML格式字段列表（临时列表，用于临时接收采集完的点字段）
        /// </summary>
        public Dictionary<string, List<string>> TableToTempDataStr
        {
            get;set;
        }
        /// <summary>
        /// 键：需要结转的数据表
        /// 值：该表需要用来结转的XML本地缓存字符串
        /// </summary>
        public Dictionary<string, string> TableToDataStr
        {
            get;set;
        }

        /// <summary>
        /// 键：表
        /// 值：字典：键：1：字段名称 2：字段值
        /// </summary>
        public Dictionary<string, Dictionary<int, string>> TableToDescriptionToFieldDicDic
        {
            get;set;
        }

        public Dictionary<string, List<string>> PortraitTableToTagListDic
        {
            get;set;
        }

        public Dictionary<string, List<string>> SavedPortraitTableToTagListDic
        {
            get;set;
        }
    }
}
