using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZYSocket.Share;
using ZYSocket.RPC.Client;
using Server;

namespace Client
{
    public partial class WinMain : Form
    {
        RPCClient client = null;
        public WinMain()
        {
            InitializeComponent();
        }

        private void WinMain_Load(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.ShowDialog();
            if (!string.IsNullOrEmpty(login.LoginName))
            {
                client = new RPCClient();
                if (client.Connection("127.0.0.1", 9562))
                {
                    client.Disconn += new ZYSocket.ClientB.ClientMessageInputHandler(client_Disconn);
                    client.DataOn += new ZYSocket.ClientB.ClientBinaryInputHandler(client_DataOn);
                    string msg = null; ;
                    if (client.Call<TalkService, bool>(p => p.LogOn(login.LoginName, out msg)))
                    {
                        this.rtbRecord.AppendText(msg + "\r\n");
                        this.Text = login.LoginName;
                        GetAllUser();
                        return;
                    }
                }
            }
            this.Close();
        }

        public void GetAllUser()
        {
            List<string> userlist = client.Call<TalkService, List<string>>(p => p.GetAllUser());
            this.lvUser.Items.Clear();
            this.cboUser.Items.Clear();
            this.cboUser.Items.Add("所有人");
            foreach (var item in userlist)
            {
                this.lvUser.Items.Add(new ListViewItem(item));
                this.cboUser.Items.Add(item);
            }
        }


        void client_DataOn(byte[] data)
        {
            ReadBytesV2 read = new ReadBytesV2(data);
            int lengt;
            int cmd;
            if (read.ReadInt32(out lengt) && lengt == read.Length && read.ReadInt32(out cmd))
            {
                switch (cmd)
                {
                    case 1:
                        {
                            this.BeginInvoke(new EventHandler((a, b) =>
                                {
                                    GetAllUser();
                                }));
                        }
                        break;
                    case 2:
                        {
                            string msg;
                            if (read.ReadString(out msg))
                            {
                                this.BeginInvoke(new EventHandler((a, b) =>
                                {
                                    this.rtbRecord.AppendText(msg + "\r\n");
                                }));
                            }
                        }
                        break;
                }
            }
        }

        void client_Disconn(string message)
        {
            this.BeginInvoke(new EventHandler((a, b) =>
                {
                    MessageBox.Show(message);
                    this.Close();
                }));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!client.Call<TalkService, bool>(p => p.MessageTalk(this.cboUser.Text, this.txtMessage.Text)))
            {
                this.rtbRecord.AppendText("发送失败");
            }
        }

    }
}
