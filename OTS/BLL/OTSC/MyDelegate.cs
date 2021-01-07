using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.OTSM;

namespace BLL.OTSC
{
    public class MyDelegate
    {
        /// <summary>
        /// 声明一个页面显示委托
        /// </summary>
        public delegate void DShowPage();
        /// <summary>
        /// 更新主界面展示列表
        /// </summary>
        //public static DShowPage dUpdateViewList;
        /// <summary>
        ///展示主界面
        /// </summary>
        public static DShowPage dShowMainPage;
        /// <summary>
        /// 展示导入窗口
        /// </summary>
        public static DShowPage dShowPairWindow;
        /// <summary>
        /// 展示类型选择窗口
        /// </summary>
        public static DShowPage dShowTypeWindow;

        public delegate void DShowItem(List<TableFieldInfo> TagList);

        public static DShowItem dShowItem;

        public delegate void DShowMessage(string Message);
        /// <summary>
        /// 展示信息提示窗口
        /// </summary>
        public static DShowMessage dShowMessageBox;

        /// <summary>
        /// 展示配置窗口
        /// </summary>
        /// <param name="Type">表类型</param>
        /// <param name="SettingObject">配置逻辑操作基类</param>
        public delegate void DsetSettingPage(string Type, SettingVirClass SettingObject);
        public static DsetSettingPage dShowSettingPage;

        public delegate void DGetSettingObject(SettingVirClass SettingObject);
        public static DGetSettingObject dGetSettingObject;
        /// <summary>
        /// 声明一个设置状态委托
        /// </summary>
        /// <param name="State"></param>
        public delegate void DSetState(bool State);
        /// <summary>n 
        /// 设置灯的状态
        /// </summary>
        public static DSetState dSetLedState;
        /// <summary>
        /// 设置连接状态文本
        /// </summary>
        public static DSetState dSetConnectText;
        /// <summary>
        /// 设置订阅模式
        /// </summary>
        public static DSetState dSetSubscribeType;

        public static DSetState dSetReadComplete;

        /// <summary>
        /// 声明一个文件操作委托
        /// </summary>
        public delegate void DOperateFile();
        /// <summary>
        /// 写数据到文件
        /// </summary>
        public static DOperateFile dWriteToFile;
        /// <summary>
        /// 读文件
        /// </summary>
        public static DOperateFile dReadToFile;
        /// <summary>
        /// 写到配置存储文件
        /// </summary>
        public static DOperateFile dWriteToSaveFile;
        /// <summary>
        /// 返回新的实例
        /// </summary>
        public delegate void DReturnExample();

        public static DReturnExample dReturnExample;
    }
}
