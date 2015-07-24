﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using ZYSocket.Server;
using ZYSocket.Share;

namespace TestServer
{
    class Program
    {
        //建立一个ZYSOCKETSERVER 对象 注意启动前应该先设置 App.Config 文件,如果你不想设置App.Config文件 那么可以在构造方法里面传入相关的设置
        static ZYSocketSuper server = new ZYSocketSuper();

        //程序入口
        static void Main(string[] args)
        {
            server.BinaryInput = new BinaryInputHandler(BinaryInputHandler); //设置输入代理
            server.Connetions = new ConnectionFilter(ConnectionFilter); //设置连接代理
            server.MessageInput = new MessageInputHandler(MessageInputHandler); //设置 客户端断开
            server.Start(); //启动服务器
            Console.ReadLine();
        }

        /// <summary>
        /// 用户断开代理（你可以根据socketAsync 读取到断开的
        /// </summary>
        /// <param name="message">断开消息</param>
        /// <param name="socketAsync">断开的SOCKET</param>
        /// <param name="erorr">错误的ID</param>
        static void MessageInputHandler(string message, SocketAsyncEventArgs socketAsync, int erorr)
        {
            //Demo1.3增加异常捕获
            try
            {
                Console.WriteLine(message);
                socketAsync.UserToken = null;
                socketAsync.AcceptSocket.Close();
                socketAsync.AcceptSocket.Dispose();//Demo1.3增加
            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
            }
        }

        /// <summary>
        /// 用户连接的代理
        /// </summary>
        /// <param name="socketAsync">连接的SOCKET</param>
        /// <returns>如果返回FALSE 则断开连接,这里注意下 可以用来封IP</returns>
        static bool ConnectionFilter(SocketAsyncEventArgs socketAsync)
        {
            //Demo1.3增加异常捕获
            try
            {
                Console.WriteLine("UserConn {0}", socketAsync.AcceptSocket.RemoteEndPoint.ToString());
                socketAsync.UserToken = null;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary>
        /// 数据包输入
        /// </summary>
        /// <param name="data">输入数据</param>
        /// <param name="socketAsync">该数据包的通讯SOCKET</param>
        static void BinaryInputHandler(byte[] data, SocketAsyncEventArgs socketAsync)
        {
            if (socketAsync.UserToken == null) //如果此SOCKET绑定的对象为NULL
            {
                //注意这里为了 简单 所以就绑定了个 BufferList 类，本来这里应该绑定用户类对象，
                //并在用户类里面建立 初始化 一个 BufferList 类，这样就能通过用户类保存更多的信息了。
                //比如用户名，权限等等
                //socketAsync.UserToken = new BufferList(409600);
                socketAsync.UserToken = new ZYNetBufferReadStream(409600);
            }

            //BufferList 数据包组合类 如果不想丢数据就用这个类吧
            ZYNetBufferReadStream buffer = socketAsync.UserToken as ZYNetBufferReadStream;

            //List<byte[]> datax;
            //if (buff.InsertByteArray(data, 4,out datax)) //强力插入数据包,如果数据包组合成功返回TURE 并输出数据包
            //{
            //    if (datax != null)
            //    {
            //        foreach (byte[] xdata in datax)
            //        {
            //            DataOn(xdata, socketAsync);
            //        }

            //    }
            //}

            buffer.Write(data);
            byte[] pdata;
            while (buffer.Read(out pdata))
            {
                DataOn(pdata, socketAsync);
            }
        }

        #region Demo1.2
        ///// <summary>
        ///// 数据包输入
        ///// </summary>
        ///// <param name="data">输入数据</param>
        ///// <param name="socketAsync">该数据包的通讯SOCKET</param>
        //static void BinaryInputHandler(byte[] data, SocketAsyncEventArgs socketAsync)
        //{
        //    // server.SendData(socketAsync.AcceptSocket, data);
        //    // server.Send(socketAsync.AcceptSocket, data);
        //    socketAsync.AcceptSocket.Send(data);
        //}
        #endregion

        #region Demo1.3
        ///// <summary>
        ///// 数据包输入
        ///// </summary>
        ///// <param name="data">输入数据</param>
        ///// <param name="socketAsync">该数据包的通讯SOCKET</param>
        //static void BinaryInputHandler(byte[] data, SocketAsyncEventArgs socketAsync)
        //{
        //    try
        //    {
        //        if (socketAsync.UserToken == null) //如果此SOCKET绑定的对象为NULL
        //        {
        //            //注意这里为了 简单 所以就绑定了个 ZYNetBufferReadStreamV2 类，本来这里应该绑定用户类对象，
        //            //并在用户类里面建立 初始化 一个 ZYNetBufferReadStreamV2 类，这样就能通过用户类保存更多的信息了。
        //            //比如用户名，权限等等
        //            socketAsync.UserToken = new ZYNetBufferReadStreamV2(4096000);
        //        }

        //        ZYNetBufferReadStream stream = socketAsync.UserToken as ZYNetBufferReadStreamV2; //最新的数据包整合类

        //        stream.Write(data);

        //        byte[] datax;
        //        while (stream.Read(out datax))
        //        {
        //            DataOn(datax, socketAsync);
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        Console.WriteLine(er.ToString());
        //    }
        //}
        #endregion

        #region Demo1.4
        ///// <summary>
        ///// 数据包输入
        ///// </summary>
        ///// <param name="data">输入数据</param>
        ///// <param name="socketAsync">该数据包的通讯SOCKET</param>
        //static void BinaryInputHandler(byte[] data, SocketAsyncEventArgs socketAsync)
        //{
        //    if (socketAsync.UserToken == null) //如果此SOCKET绑定的对象为NULL
        //    {
        //        //注意这里为了 简单 所以就绑定了个 BuffList 类，本来这里应该绑定用户类对象，
        //        //并在用户类里面建立 初始化 一个 BuffList 类，这样就能通过用户类保存更多的信息了。
        //        //比如用户名，权限等等
        //        socketAsync.UserToken = new BuffList(409000);
        //    }

        //    //BuffList 数据包组合类 如果不想丢数据就用这个类吧
        //    BuffList buff = socketAsync.UserToken as BuffList;

        //    List<byte[]> datax;

        //    if (buff.InsertByteArray(data, 4, out datax)) //强力插入数据包,如果数据包组合成功返回TURE 并输出数据包
        //    {
        //        if (datax != null)
        //        {
        //            foreach (byte[] xdata in datax)
        //            {
        //                DataOn(xdata, socketAsync);
        //            }

        //        }
        //    }
        //}
        #endregion

        static void DataOn(byte[] data, SocketAsyncEventArgs e)
        {
            try
            {
                //建立一个读取数据包的类 参数是数据包
                //这个类的功能很强大,可以读取数据包的数据,并可以把你发送过来的对象数据,转换对象引用
                ReadBytes read = new ReadBytes(data);
                int lengt; //数据包长度,用于验证数据包的完整性
                int cmd; //数据包命令类型
                //注意这里一定要这样子写,这样子可以保证所有你要度的数据是完整的,如果读不出来 Raed方法会返回FALSE,从而避免了错误的数据导致崩溃
                if (read.ReadInt32(out lengt) && read.Length == lengt && read.ReadInt32(out cmd))
                {
                    //read.Read系列函数是不会产生异常的
                    //根据命令读取数据包
                    switch (cmd)
                    {
                        case 1000:
                            TestClass.PPo temp;
                            if (read.ReadObject<TestClass.PPo>(out temp))
                            {
                                if (temp != null)
                                {
                                    Console.WriteLine("Id:{0}\r\n Mn:{1} \r\n Guid:{2} \r\n DataLength:{3} \r\n\r\n", temp.Id, temp.Message, temp.guidList, read.Length);
                                }
                            }
                            break;
                        case 1001:
                            {
                                int id;
                                string mn;
                                Guid guid;
                                if (read.ReadInt32(out id) && read.ReadString(out mn) && read.ReadObject<Guid>(out guid))
                                {
                                    Console.WriteLine("Id:{0}\r\n Mn:{1} \r\n Guid:{2} \r\n DataLength:{3} \r\n\r\n", id, mn, guid, read.Length);
                                }
                            }
                            break;
                        case 1002:
                            {
                                int id;
                                string mn;
                                string guid;
                                if (read.ReadInt32(out id) && read.ReadString(out mn) && read.ReadString(out guid))
                                {
                                    Console.WriteLine("Id:{0}\r\n Mn:{1} \r\n Guid:{2} \r\n DataLength:{3} \r\n\r\n", id, mn, guid, read.Length);
                                }
                            }
                            break;
                        case 1003:
                            {
                                #region 第八步：发送数据
                                 server.SendData(e.AcceptSocket, data);
                                #endregion
                               
                            }
                            break;
                        #region Demo1.3
                        //case 2000:
                        //    {
                        //        List<TestClass.PPo2> tmp;
                        //        if (read.ReadObject<List<TestClass.PPo2>>(out tmp))
                        //        {
                        //        }
                        //    }
                        //    break;
                        #endregion

                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
            }
        }

    }
}
