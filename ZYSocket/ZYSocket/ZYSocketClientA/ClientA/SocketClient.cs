﻿/*
 * 北风之神SOCKET框架(ZYSocket)
 *  Borey Socket Frame(ZYSocket)
 *  by luyikk@126.com
 *  Updated 2010-12-26 
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
namespace ZYSocket.ClientA
{

    public delegate void ConnectionOK(string message, bool isConnection);
    public delegate void DataOn(byte[] Data);
    public delegate void ExceptionDisconnection(string message);

    /// <summary>
    /// ZYSOCKET 客户端
    /// （一个简单的异步SOCKET客户端，性能不错。支持.NET 3.0以上版本。适用于silverlight)
    /// </summary>
    public class SocketClient
    {

        /// <summary>
        /// SOCKET对象
        /// </summary>
        private Socket clientSocket;

        /// <summary>
        /// 连接成功事件
        /// </summary>
        public event ConnectionOK Connection;

        /// <summary>
        /// 数据包进入事件
        /// </summary>
        public event DataOn DataOn;

        /// <summary>
        /// 出错或断开触发事件
        /// </summary>
        public event ExceptionDisconnection Disconnection;

        private System.Threading.AutoResetEvent wait = new System.Threading.AutoResetEvent(false);

        public SocketClient()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private bool isConnection;

        /// <summary>
        /// 异步连接到指定的服务器
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void BeginConnectionTo(string host, int port)
        {
            #region IPFormat
            IPEndPoint myEnd = null;
            try
            {
                myEnd = new IPEndPoint(IPAddress.Parse(host), port);
            }
            catch (FormatException)
            {
                IPHostEntry p = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress s in p.AddressList)
                {
                    if (!s.IsIPv6LinkLocal)
                    {
                        myEnd = new IPEndPoint(s, port);
                    }
                }
            }
            #endregion

            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.RemoteEndPoint = myEnd;
            e.Completed += new EventHandler<SocketAsyncEventArgs>(e_Completed);
            //开始一个对远程主机连接的异步请求
            //如果 I/O 操作挂起，将返回 true。操作完成时，将引发 context 参数的 System.Net.Sockets.SocketAsyncEventArgs.Completed事件。 
            //如果 I/O 操作同步完成，将返回 false。在这种情况下，将不会引发 context 参数的 System.Net.Sockets.SocketAsyncEventArgs.Completed事件，
            //并且可能在方法调用返回后立即检查作为参数传递的 context 对象以检索操作的结果。
            if (!clientSocket.ConnectAsync(e))
            {
                eCompleted(e);
            }
        }

        public bool ConnectionTo(string host, int port)
        {
            IPEndPoint myEnd = null;

            #region IPFormat
            try
            {
                myEnd = new IPEndPoint(IPAddress.Parse(host), port);
            }
            catch (FormatException)
            {
                IPHostEntry p = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress s in p.AddressList)
                {
                    if (!s.IsIPv6LinkLocal)
                    {
                        myEnd = new IPEndPoint(s, port);
                    }
                }
            }
            #endregion

            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.RemoteEndPoint = myEnd;
            e.Completed += new EventHandler<SocketAsyncEventArgs>(e_Completed);
            if (!clientSocket.ConnectAsync(e))
            {
                eCompleted(e);
            }
            wait.WaitOne();
            wait.Reset();
            return isConnection;
        }

        void e_Completed(object sender, SocketAsyncEventArgs e)
        {
            eCompleted(e);
        }

        void eCompleted(SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    if (e.SocketError == SocketError.Success)
                    {
                        isConnection = true;
                        wait.Set();
                        if (Connection != null)
                        {
                            Connection("连接成功", true);
                        }
                        byte[] data = new byte[4098];
                        e.SetBuffer(data, 0, data.Length);  //设置数据包
                        if (!clientSocket.ReceiveAsync(e)) //开始读取数据包
                        {
                            eCompleted(e);
                        }
                    }
                    else
                    {
                        isConnection = false;
                        wait.Set();
                        if (Connection != null)
                        {
                            Connection("连接失败", false);
                        }
                    }
                    break;
                case SocketAsyncOperation.Receive:
                    if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
                    {
                        byte[] data = new byte[e.BytesTransferred];
                        Buffer.BlockCopy(e.Buffer, 0, data, 0, data.Length);
                        byte[] dataLast = new byte[4098];
                        e.SetBuffer(dataLast, 0, dataLast.Length);
                        if (!clientSocket.ReceiveAsync(e))
                        {
                            eCompleted(e);
                        }
                        if (DataOn != null)
                        {
                            DataOn(data);
                        }
                    }
                    else
                    {
                        if (Disconnection != null)
                        {
                            Disconnection("与服务器断开连接");
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="data"></param>
        public virtual void SendTo(byte[] data)
        {
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.SendPacketsSendSize = 4096;
            e.SetBuffer(data, 0, data.Length);
            clientSocket.SendAsync(e);
        }

        public virtual void BeginSend(byte[] data)
        {
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.SetBuffer(data, 0, data.Length);
            clientSocket.SendAsync(e);
        }

        public void Close()
        {
            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Disconnect(false);
                clientSocket.Close();
                wait.Close();
            }
            catch (ObjectDisposedException)
            {
            }
            catch (NullReferenceException)
            {
            }
        }

    }
}