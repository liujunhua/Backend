﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSocket.share;
using ZYSocket.ClientA;
using System.Net.Sockets;

namespace Client
{
    
    /// <summary>
    /// SOCKET层类
    /// </summary>
    static class SocketManager
    {
        /// <summary>
        /// 数据包输出
        /// </summary>
        public static event DataOn DataInput;
        /// <summary>
        /// 断开连接
        /// </summary>
        public static event ExceptionDisconnection Disconnection;
        /// <summary>
        /// 数据包缓冲类
        /// </summary>
        public static BuffList BuffListManger { get; set; }

        /// <summary>
        /// SOCKETCLIENT对象
        /// </summary>
        public static SocketClient client { get; set; }

        static SocketManager()
        {
            //初始化数据包缓冲区,并设置了最大数据包尽可能的大 
            BuffListManger = new BuffList(400000); 
            client=new SocketClient();
            client.DataOn += new DataOn(client_DataOn);
            client.Disconnection += new ExceptionDisconnection(client_Disconnection);
        }

        static void client_Disconnection(string message)
        {
            if (Disconnection != null)
                Disconnection(message);
        }

        static void client_DataOn(byte[] Data)
        {
            List<byte[]> datax;
            if (BuffListManger.InsertByteArray(Data, 4, out datax)) //整理从服务器上收到的数据包
            {
                if (DataInput != null)
                    foreach(byte[] mdata in datax)
                        DataInput(mdata);
            }
        }
    }
}