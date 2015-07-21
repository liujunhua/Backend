/*
 * 北风之神SOCKET框架(ZYSocket)
 *  Borey Socket Frame(ZYSocket)
 *  by luyikk@126.com
 *  Updated 2014-7-21  此类已支持MONO
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;

namespace ZYSocket.Server
{

    /// <summary>
    /// 连接的代理
    /// </summary>
    /// <param name="socketAsync"></param>
    public delegate bool ConnectionFilter(SocketAsyncEventArgs socketAsync);

    /// <summary>
    /// 数据包输入代理
    /// </summary>
    /// <param name="data">输入包</param>
    /// <param name="socketAsync"></param>
    public delegate void BinaryInputHandler(byte[] data, SocketAsyncEventArgs socketAsync);

    /// <summary>
    /// 异常错误通常是用户断开的代理
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="socketAsync"></param>
    /// <param name="erorr">错误代码</param>
    public delegate void MessageInputHandler(string message, SocketAsyncEventArgs socketAsync, int erorr);

    /// <summary>
    /// ZYSOCKET框架服务器端（通过6W个连接测试。理论上支持10W个连接，可谓.NET最强SOCKET模型）
    /// </summary>
    public class ZYSocketSuper : IDisposable
    {

        #region 释放
        /// <summary>
        /// 用来确定是否以释放
        /// </summary>
        private bool isDisposed;

        ~ZYSocketSuper()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed || disposing)
            {
                try
                {
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                    for (int i = 0; i < SocketAsynPool.Count; i++)
                    {
                        SocketAsyncEventArgs args = SocketAsynPool.Pop();
                        bufferManager.FreeBuffer(args);
                    }
                }
                catch
                {
                }
                isDisposed = true;
            }
        }
        #endregion

        /// <summary>
        /// 数据包管理
        /// </summary>
        private BufferManager bufferManager;

        /// <summary>
        /// Socket异步对象池
        /// </summary>
        private SocketAsyncEventArgsPool SocketAsynPool;

        /// <summary>
        /// SOCK对象
        /// </summary>
        private Socket sock;

        /// <summary>
        /// Socket对象
        /// </summary>
        public Socket Sock { get { return sock; } }

        /// <summary>
        /// 连接传入处理
        /// </summary>
        public ConnectionFilter Connetions { get; set; }

        /// <summary>
        /// 数据输入处理
        /// </summary>
        public BinaryInputHandler BinaryInput { get; set; }

        /// <summary>
        /// 异常错误通常是用户断开处理
        /// </summary>
        public MessageInputHandler MessageInput { get; set; }


        /// <summary>
        /// 是否关闭SOCKET Delay算法
        /// </summary>
        public bool NoDelay
        {
            get
            {
                return sock.NoDelay;
            }
            set
            {
                sock.NoDelay = value;
            }
        }

        /// <summary>
        /// SOCKET的ReceiveTimeout属性
        /// </summary>
        public int ReceiveTimeout
        {
            get
            {
                return sock.ReceiveTimeout;
            }
            set
            {
                sock.ReceiveTimeout = value;
            }
        }

        /// <summary>
        /// SOCKET 的SendTimeout
        /// </summary>
        public int SendTimeout
        {
            get
            {
                return sock.SendTimeout;
            }
            set
            {
                sock.SendTimeout = value;
            }
        }

        /// <summary>
        /// 接收包大小
        /// </summary>
        private int MaxBufferSize;
        public int GetMaxBufferSize
        {
            get
            {
                return MaxBufferSize;
            }
        }

        /// <summary>
        /// 最大用户连接
        /// </summary>
        private int MaxConnectionCount;
        public int GetMaxUserConnection
        {
            get
            {
                return MaxConnectionCount;
            }
        }

        /// <summary>
        /// IP
        /// </summary>
        private string Host;

        /// <summary>
        /// 端口
        /// </summary>
        private int Port;

        private System.Threading.AutoResetEvent[] reset;

        #region 消息输出
        /// <summary>
        /// 输出消息
        /// </summary>
        public event EventHandler<LogOutEventArgs> MessageOut;

        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="o"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        protected void LogOutEvent(Object sender, LogType type, string message)
        {
            if (MessageOut != null)
            {
                MessageOut.BeginInvoke(sender, new LogOutEventArgs(type, message), new AsyncCallback(CallBackEvent), MessageOut);
            }
        }

        /// <summary>
        /// 事件处理完的回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void CallBackEvent(IAsyncResult ar)
        {
            EventHandler<LogOutEventArgs> MessageOut = ar.AsyncState as EventHandler<LogOutEventArgs>;
            if (MessageOut != null)
            {
                MessageOut.EndInvoke(ar);
            }
        }

        #endregion

        public ZYSocketSuper()
        {
            this.Port = IPConfig.ReadInt("Port");
            this.Host = IPConfig.ReadString("Host");
            this.MaxBufferSize = IPConfig.ReadInt("MaxBufferSize");
            this.MaxConnectionCount = IPConfig.ReadInt("MaxConnectionCount");

            //定义一个AutoResetEvent数组,WaitHandle.WaitAll方法只收WaitHandle[]参数(数组)
            this.reset = new System.Threading.AutoResetEvent[1];
            reset[0] = new System.Threading.AutoResetEvent(false);

            this.Run();
        }

        public ZYSocketSuper(string host, int port, int maxConnectCount, int maxBufferSize)
        {
            this.Port = port;
            this.Host = host;
            this.MaxBufferSize = maxBufferSize;
            this.MaxConnectionCount = maxConnectCount;

            //定义一个AutoResetEvent数组,WaitHandle.WaitAll方法只收WaitHandle[]参数(数组)
            this.reset = new System.Threading.AutoResetEvent[1];
            reset[0] = new System.Threading.AutoResetEvent(false);

            this.Run();
        }

        /// <summary>
        /// 启动
        /// </summary>
        private void Run()
        {
            if (isDisposed == true)
            {
                throw new ObjectDisposedException("Server is Disposed");
            }
            //.NET框架用IPAddress对象来表示一个单一的IP地址
            //.NET框架用IPEndPoint对象来表示一个特定的IP地址和端口的组合，应用该对象的场景多是在讲socket绑定到本地地址或者将socket绑定到非本地地址。
            IPEndPoint myEnd = new IPEndPoint(IPAddress.Any, Port);
            if (!Host.Equals("any", StringComparison.CurrentCultureIgnoreCase))
            {
                if (String.IsNullOrEmpty(Host))
                {
                    IPHostEntry p = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress s in p.AddressList)
                    {
                        if (!s.IsIPv6LinkLocal && s.AddressFamily != AddressFamily.InterNetworkV6)
                        {
                            myEnd = new IPEndPoint(s, Port);
                            break;
                        }
                    }
                }
                else
                {
                    try
                    {
                        myEnd = new IPEndPoint(IPAddress.Parse(Host), Port);
                    }
                    catch (FormatException)
                    {
                        IPHostEntry p = Dns.GetHostEntry(Dns.GetHostName());
                        foreach (IPAddress s in p.AddressList)
                        {
                            if (!s.IsIPv6LinkLocal)
                            {
                                myEnd = new IPEndPoint(s, Port);
                                break;//2015-01-15 liujh
                            }
                        }
                    }
                }
            }

            sock = new Socket(myEnd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);

            if (Environment.OSVersion.Platform.ToString().IndexOf("NT") >= 0) //WINDOWS NT平台
            {
                //add by john at 2012-12-03 用户检测用户断开 心跳处理
                uint dummy = 0;
                byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
                sock.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                //-------------------------
            }

            sock.Bind(myEnd);
            sock.Listen(20);
            SendTimeout = 1000;
            ReceiveTimeout = 1000;

            bufferManager = new BufferManager(MaxConnectionCount * MaxBufferSize, MaxBufferSize);
            bufferManager.Inint();

            SocketAsynPool = new SocketAsyncEventArgsPool(MaxConnectionCount);
            for (int i = 0; i < MaxConnectionCount; i++)
            {
                SocketAsyncEventArgs socketasyn = new SocketAsyncEventArgs();
                socketasyn.Completed += new EventHandler<SocketAsyncEventArgs>(Asyn_Completed);
                SocketAsynPool.Push(socketasyn);
            }

            this.Accept();
        }

        public void Start()
        {
            reset[0].Set();
        }

        public void Stop()
        {
            reset[0].Reset();
        }

        void Accept()
        {
            if (SocketAsynPool.Count > 0)
            {
                SocketAsyncEventArgs sockasyn = SocketAsynPool.Pop();
                //开始一个异步操作来接受一个传入的连接操作
                if (!Sock.AcceptAsync(sockasyn))
                {
                    BeginAccept(sockasyn);
                }
            }
            else
            {
                LogOutEvent(null, LogType.Error, "The MaxUserCount");
            }
        }

        void BeginAccept(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError == SocketError.Success)
                {
                    //调用WaitHandle的方法来阻塞当前线程，当前方法是当所有异步对象接到信号时退出阻塞
                    System.Threading.WaitHandle.WaitAll(reset);
                    reset[0].Set();
                    if (this.Connetions != null)
                    {
                        if (!this.Connetions(e))
                        {
                            LogOutEvent(null, LogType.Error, string.Format("The Socket Not Connect {0}", e.AcceptSocket.RemoteEndPoint));
                            e.AcceptSocket = null;
                            SocketAsynPool.Push(e);
                            return;
                        }
                    }

                    if (bufferManager.SetBuffer(e))
                    {
                        if (!e.AcceptSocket.ReceiveAsync(e))
                        {
                            BeginReceive(e);
                        }
                    }
                }
                else
                {
                    e.AcceptSocket = null;
                    SocketAsynPool.Push(e);
                    LogOutEvent(null, LogType.Error, "Not Accep");
                }
            }
            finally
            {
                this.Accept();
            }
        }

        void BeginReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
            {
                byte[] data = new byte[e.BytesTransferred];
                Buffer.BlockCopy(e.Buffer, e.Offset, data, 0, data.Length);
                //
                if (this.BinaryInput != null)
                {
                    this.BinaryInput(data, e);
                }
                if (!e.AcceptSocket.ReceiveAsync(e))
                {
                    BeginReceive(e);
                }
            }
            else
            {
                if (e.AcceptSocket != null && e.AcceptSocket.RemoteEndPoint != null)
                {
                    string message = string.Empty;
                    try
                    {
                        message = string.Format("User Disconnect :{0}", e.AcceptSocket.RemoteEndPoint.ToString());
                    }
                    catch (System.NullReferenceException)
                    {
                        message = "User Disconect";
                    }
                    LogOutEvent(null, LogType.Error, message);
                    if (MessageInput != null)
                    {
                        MessageInput(message, e, 0);
                    }
                }
                else
                {
                    if (MessageInput != null)
                    {
                        MessageInput("User Disconnect But cannot get Ipaddress", e, 0);
                    }
                }
                e.AcceptSocket = null;
                bufferManager.FreeBuffer(e);
                SocketAsynPool.Push(e);
                if (SocketAsynPool.Count == 1)
                {
                    this.Accept();
                }
            }
        }

        void Asyn_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    BeginAccept(e);
                    break;
                case SocketAsyncOperation.Receive:
                    BeginReceive(e);
                    break;
            }
        }

        /// <summary>
        /// 同步发送数据包
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="Data"></param>
        public virtual void Send(Socket sock, byte[] Data)
        {
            try
            {
                if (sock != null && sock.Connected)
                {
                    sock.Send(Data);
                }
            }
            catch (SocketException)
            {
            }
        }

        /// <summary>
        /// 异步发送数据包
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="data"></param>
        public virtual void SendData(Socket sock, byte[] data)
        {
            try
            {
                if (sock != null && sock.Connected)
                {
                    sock.BeginSend(data, 0, data.Length, SocketFlags.None, AsynCallBack, sock);
                }
            }
            catch (SocketException)
            {
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
            catch
            {
            }
        }

        /// <summary>
        /// 断开此SOCKET
        /// </summary>
        /// <param name="sock"></param>
        public void Disconnect(Socket socks)
        {
            try
            {
                if (sock != null)
                {
                    socks.BeginDisconnect(false, AsynCallBackDisconnect, socks);
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception)
            {
            }
        }

        void AsynCallBackDisconnect(IAsyncResult result)
        {
            try
            {
                Socket sock = result.AsyncState as Socket;
                if (sock != null)
                {
                    try
                    {
                        sock.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        sock.EndDisconnect(result);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception)
            {
            }
        }
    }

    public enum LogType
    {
        Error,
    }

    public class LogOutEventArgs : EventArgs
    {

        /// <summary>
        /// 消息类型
        /// </summary>     
        private LogType messClass;

        /// <summary>
        /// 消息类型
        /// </summary>  
        public LogType MessClass
        {
            get { return messClass; }
        }

        /// <summary>
        /// 消息
        /// </summary>
        private string mess;

        public string Mess
        {
            get { return mess; }
        }

        public LogOutEventArgs(LogType messclass, string str)
        {
            messClass = messclass;
            mess = str;
        }

    }
}
