using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.IBOXClientBLL;
using Model.IBOXClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        IBOXUIproperty UIProperty;
        GetIBOXServer GetServer;
        public Form1()
        {
            InitializeComponent();
            UIProperty = new IBOXUIproperty();
            GetServer = new GetIBOXServer(UIProperty.ClientID, UIProperty.ClientKey, UIProperty.UserName, UIProperty.PassWord, UIProperty.IDServer, UIProperty.APPServer, UIProperty.HDataServer);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetServer.IBOXLogin();
        }
    }
}
