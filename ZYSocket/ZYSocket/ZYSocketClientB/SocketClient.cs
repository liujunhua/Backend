/*
 * 北风之神SOCKET框架(ZYSocket)
 *  Borey Socket Frame(ZYSocket)
 *  by luyikk@126.com
 *  Updated 2011-7-29
 *  .NET  2.0
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;

namespace ZYSocket.ClientB
{

    /// <summary>
    /// 数据包输入代理
    /// </summary>
    /// <param name="data">输入包</param>
    /// <param name="socketAsync"></param>
    public delegate void ClientBinaryInputHandler(byte[] data);
    /// <summary>
    /// 异常错误通常是用户断开的代理
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="socketAsync"></param>
    /// <param name="erorr">错误代码</param>
    public delegate void ClientMessageInputHandler(string message);
    public delegate void ErrorLogOutHandler(string msg);
    public delegate void ConnectionHandler(Socket socket, bool conn);

    public class SocketClient
    {

        private Socket clientSocket;
        /// <summary>
        /// Socket对象
        /// </summary>
        public Socket ClientSocket { get { return clientSocket; } }

        /// <summary>
        /// 数据包长度
        /// </summary>
        public int BufferLength { get; set; }

        /// <summary>
        /// 数据输入处理
        /// </summary>
        public event ClientBinaryInputHandler BinaryInput;

        /// <summary>
        /// 异常错误通常是用户断开处理
        /// </summary>
        public event ClientMessageInputHandler MessageInput;

        /// <summary>
        /// 有连接
        /// </summary>
        public event ConnectionHandler ConnInput;

        public event ErrorLogOutHandler ErrorLogOut;

        private SocketError socketError;

        public SocketClient()
        {
            BufferLength = 4096;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            //add by john at 2012-12-03
            //uint dummy = 0;
            //byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
            //BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
            //BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
            //BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
            //sock.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }

        private void ErrorLog(string msg)
        {
            if (ErrorLogOut != null)
            {
                ErrorLogOut(msg);
            }
        }

        /// <summary>
        ///连接到目标主机
        /// </summary>
        /// <param name="host">IP</param>
        /// <param name="prot">端口</param>
        public bool Connection(string host, int port)
        {
            try
            {
                #region IPFormat

                IPEndPoint myEnd = null;
                try
                {
                    myEnd = new IPEndPoint(IPAddress.Parse(host), port);
                }
                catch (FormatException)
                {
                    foreach (IPAddress s in Dns.GetHostAddresses(host))
                    {
                        if (!s.IsIPv6LinkLocal)
                        {
                            myEnd = new IPEndPoint(s, port);
                        }
                    }
                }

                #endregion

                clientSocket.Connect(myEnd);
                if (clientSocket.Connected)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (SocketException er)
            {
                ErrorLog(er.ToString());
                return false;
            }
            catch (Exception ee)
            {
                if (ErrorLogOut == null)
                {
                    throw ee;
                }
                else
                {
                    ErrorLog(ee.ToString());
                    return false;
                }
            }

        }

        /// <summary>
        ///连接到目标主机
        /// </summary>
        /// <param name="host">IP</param>
        /// <param name="prot">端口</param>
        public void BeginConnect(string host, int port)
        {
            try
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

                clientSocket.BeginConnect(myEnd, new AsyncCallback(ConnAsyncCallBack), clientSocket);
            }
            catch (Exception er)
            {
                if (ErrorLogOut == null)
                {
                    throw er;
                }
                else
                {
                    ErrorLog(er.ToString());
                }
            }

        }

        void ConnAsyncCallBack(IAsyncResult result)
        {
            try
            {
                clientSocket.EndConnect(result);

                if (clientSocket.Connected)
                {
                    if (ConnInput != null)
                    {
                        ConnInput(clientSocket, true);
                    }
                }
                else
                {
                    if (ConnInput != null)
                    {
                        ConnInput(clientSocket, false);
                    }
                }

            }
            catch (Exception er)
            {
                ErrorLog(er.ToString());

                if (ConnInput != null)
                {
                    ConnInput(clientSocket, false);
                }
            }
        }

        /// <summary>
        /// 开始读取数据
        /// </summary>
        public void StartRead()
        {
            BeginReceive();
        }

        void BeginReceive()
        {
            try
            {
                byte[] data = new byte[BufferLength];
                IAsyncResult reault = clientSocket.BeginReceive(data, 0, data.Length, SocketFlags.None, out socketError, Receive_Completed, data);
            }
            catch (ObjectDisposedException er)
            {
            }
        }

        void Receive_Completed(IAsyncResult reault)
        {
            //接收到的字节数
            int count = 0;
            try
            {
                count = clientSocket.EndReceive(reault);
            }
            catch (SocketException e)
            {
                socketError = e.SocketErrorCode;
            }
            catch (ObjectDisposedException)
            {
                socketError = SocketError.HostDown;
            }
            catch (Exception er)
            {
                ErrorLog(er.ToString());
                socketError = SocketError.HostDown;
            }

            if (socketError == SocketError.Success && count > 0)
            {
                try
                {
                    byte[] buffer = reault.AsyncState as byte[];
                    byte[] data = new byte[count];
                    Array.Copy(buffer, 0, data, 0, data.Length);
                    if (this.BinaryInput != null)
                    {
                        this.BinaryInput(data);
                    }
                    BeginReceive();
                }
                catch (Exception er)
                {
                    if (ErrorLogOut != null)
                    {
                        ErrorLog(er.ToString());
                    }
                }
            }
            else
            {
                try
                {
                    clientSocket.Close();
                }
                catch
                {
                }

                if (MessageInput != null)
                {
                    MessageInput("与服务器连接断开");
                }
            }
        }

        public virtual void Send(byte[] data)
        {
            try
            {
                clientSocket.Send(data);
            }
            catch (ObjectDisposedException)
            {
                if (MessageInput != null)
                {
                    MessageInput("sock 对象已释放");
                }
            }
            catch (SocketException)
            {
                try
                {
                    clientSocket.Close();
                }
                catch
                {
                }

                if (MessageInput != null)
                {
                    MessageInput("与服务器连接断开");
                }
            }
            catch (Exception er)
            {
                if (ErrorLogOut == null)
                {
                    throw er;
                }
                else
                {
                    ErrorLog(er.ToString());
                }
            }
        }

        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="data"></param>
        public virtual void BeginSendData(byte[] data)
        {
            try
            {
                clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynCallBack, clientSocket);
            }
            catch (Exception er)
            {
                if (ErrorLogOut == null)
                {
                    throw er;
                }
                else
                {
                    ErrorLog(er.ToString());
                }
            }
        }

        void AsynCallBack(IAsyncResult result)
        {
            try
            {
                Socket sock = result.AsyncState as Socket;
                if (sock != null)
                {
                    sock.EndSend(result);
                }
            }
            catch (Exception er)
            {
                ErrorLog(er.ToString());
            }
        }

        public void Close()
        {
            try
            {
                clientSocket.Close();
            }
            catch (Exception er)
            {
                if (ErrorLogOut == null)
                {
                    throw er;
                }
                else
                {
                    ErrorLog(er.ToString());
                }
            }
        }

    }
}
