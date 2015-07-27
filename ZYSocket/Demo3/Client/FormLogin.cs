using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZYSocket.Share;
using BufferLibrary;
using ZYSocket.Security;
namespace Client
{
    /// <summary>
    /// 登入界面
    /// </summary>
    public partial class FormLogin : Form
    {
        /// <summary>
        /// 登入成功为TRUE
        /// </summary>
        public bool Logins { get; set; }
        byte[] keys = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };//Demo3.0

        public FormLogin()
        {
            //注册数据包输入事件
            SocketManager.DataInput += new ZYSocket.ClientA.DataOn(SocketManager_DataInput);
            InitializeComponent();
            //测试关闭事件
            this.Closed += new EventHandler(FormLogin_Closed);
            Logins = false;
        }

        void FormLogin_Closed(object sender, EventArgs e)
        {
            //删除数据包输入事件
            SocketManager.DataInput -= new ZYSocket.ClientA.DataOn(SocketManager_DataInput);
        }

        /// <summary>
        /// 输入包输入处理
        /// </summary>
        /// <param name="Data"></param>
        void SocketManager_DataInput(byte[] Data)
        {
            //  ReadBytes read = new ReadBytes(Data);demo2.0
            ReadBytes read = new ReadBytes(Data, 4, -1, (pdata) => DES.DecryptDES(pdata, keys, "------TEST------"));
            int length;
            int cmd;
            //if (read.ReadInt32(out length) && read.ReadInt32(out cmd) && length == read.Length) demo2.0
            if (read.IsDataExtraSuccess && read.ReadInt32(out length) && read.ReadInt32(out cmd) && length == read.Length)
            {
                switch (cmd)
                {
                    case 500: //如果是Message类型数据包
                        BufferLibrary.Message ms;
                        if (read.ReadObject<BufferLibrary.Message>(out ms))
                        {
                            if (ms != null)
                            {
                                if (ms.Type == 1) //1登入失败
                                    MessageBox.Show(ms.MessageStr);
                                else if (ms.Type == 2) //2登入成功
                                {
                                    Logins = true; //设置 LOGINS
                                    this.BeginInvoke(new EventHandler((o, p) => Close()));  //关闭窗口                                   
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login temp = new Login() //发送一个登入数据包
            {
                UserName = this.txtUserName.Text,
                Password = this.txtPassword.Text
            };
            //SocketManager.client.SendTo(BufferFormat.FormatFCA(temp));Demo2.0
            SocketManager.client.SendTo(BufferFormat.FormatFCA(temp, (pdata) => DES.EncryptDES(pdata, keys, "------TEST------")));
        }
    }
}
