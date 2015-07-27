using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZYSocket.Share;
using ZYSocket.ClientA;
using System.Net.Sockets;
using Pack;

namespace Client
{

    /// <summary>
    /// 主界面
    /// </summary>
    public partial class FormClient : Form
    {
        /// <summary>
        /// 登入对话框
        /// </summary>
        LogOn logon;

        public FormClient()
        {
            #region Demo7.0 MsgPack
            //ReadBytesV2.ObjFormatType = BuffFormatType.MsgPack;
            //BufferFormatV2.ObjFormatType = BuffFormatType.MsgPack;
            #endregion

            #region Demo8.0 Protobuf
            ReadBytesV2.ObjFormatType = BufferFormatType.protobuf;
            BufferFormatV2.FormatType = BufferFormatType.protobuf;
            #endregion

            if (SocketManager.client.ConnectionTo(RConfig.ReadString("Host"), RConfig.ReadInt("Port"))) //连接到服务器
            {
                logon = new LogOn();
                logon.ShowDialog(); //显示登入界面
                if (!logon.Logins) //如果登入界面关闭那么检查是否登入成功如果没有登入成功 则关闭程序
                {
                    Close();
                }
                SocketManager.Disconnection += new ExceptionDisconnection(client_Disconnection); //注册断开事件
                SocketManager.DataInput += new DataOn(client_DataOn);//注册数据包输入事件
                InitializeComponent();
            }
            else
            {
                MessageBox.Show("无法连接服务器"); //无法连接 提示 并关闭
                Close();
            }
        }

        void client_DataOn(byte[] Data)
        {
            ReadBytesV2 read = new ReadBytesV2(Data);
            int length;
            int cmd;
            if (read.ReadInt32(out length) && read.ReadInt32(out cmd) && length == read.Length)
            {
                PackType cmds = (PackType)cmd;
                switch (cmds)
                {
                    case PackType.DataRes:
                        DataRes dox;
                        if (read.ReadObject<DataRes>(out dox)) //获取服务器发送过来的 DATASET 
                        {
                            if (dox != null)
                            {
                                this.BeginInvoke(new EventHandler((o, x) =>
                                {
                                    DataRes nn = o as DataRes;
                                    if (nn != null)
                                    {
                                        switch (nn.Type)
                                        {
                                            case 1:
                                                {
                                                    foreach (string p in nn.Res)
                                                    {
                                                        this.richTextBox1.AppendText(p + "\r\n");
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }), dox);
                            }
                        }
                        break;
                }
            }
        }

        void client_Disconnection(string message)
        {
            MessageBox.Show(message); //断开显示消息 并关闭窗口
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data temp = new Data() //发送一个DATASET请求
            {
                CMD = "GET"
            };
            SocketManager.client.SendTo(BufferFormatV2.FormatFCA(temp));
        }

    }
}
