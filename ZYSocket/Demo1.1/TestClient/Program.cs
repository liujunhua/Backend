using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSocket.ClientA;
using System.IO;
using ZYSocket.Share;

namespace TestClient
{
    class Program
    {
        /// <summary>
        /// 建立一个SOCKET客户端
        /// </summary>
        public static SocketClient client = new SocketClient();

        static void Main(string[] args)
        {
            client.DataOn += new DataOn(Client_DataOn); //数据包进入事件
            client.Disconnection += new ExceptionDisconnection(Client_Disconnection); //数据包断开事件
            if (client.ConnectionTo(RConfig.ReadString("Host"), RConfig.ReadInt("Port"))) //使用同步连接到服务器，异步就用Begin开头的那个
            {
                while (true)
                {
                    Console.ReadLine();
                    TestClass.PPo temp = new TestClass.PPo();
                    temp.Id = 1;
                    temp.Message = "通过对象通讯";
                    temp.GuidList = new List<Guid>();
                    for (int i = 0; i < 2; i++)
                    {
                        temp.GuidList.Add(Guid.NewGuid());
                    }
                    client.SendTo(BufferFormat.FormatFCA(temp));  //将一个PPO对象发送出去

                    // Console.ReadLine();

                    //BufferFormat buffmat = new BufferFormat(1001);
                    //buffmat.AddItem(2);
                    //buffmat.AddItem("通过组合数据包通讯，GUID is object");
                    //buffmat.AddItem(Guid.NewGuid());
                    //client.SendTo(buffmat.Finish()); //用组合数据包模拟PPO对象
                    // Console.ReadLine();

                    // Console.ReadLine();
                    //BufferFormat buffmat2 = new BufferFormat(1002);
                    //buffmat2.AddItem(3);
                    //buffmat2.AddItem("通过组合数据包通讯 all buff");
                    //buffmat2.AddItem(Guid.NewGuid().ToString());
                    //client.SendTo(buffmat2.Finish()); //用组合数据包模拟PPO对象 但GUID 是字符串类型
                }
            }
            else
            {
                Console.WriteLine("无法连接服务器");
            }
            Console.ReadLine();
        }

        #region Demo1.3
        //static void Main(string[] args)
        //{
        //    client.DataOn += new DataOn(Client_DataOn); //数据包进入事件
        //    client.Disconnection += new ExceptionDisconnection(Client_Disconnection); //数据包断开事件
        //    if (client.ConnectionTo(RConfig.ReadString("Host"), RConfig.ReadInt("Port"))) //使用同步连接到服务器，一步就用Begin开头的那个
        //    {
        //        int i = 0;
        //        while (true)
        //        {
        //            Console.ReadLine();
        //            TestClass.PPo temp = new TestClass.PPo();
        //            temp.Id = i;
        //            temp.Message = "通过对象通讯";
        //            temp.guidList = new List<Guid>();
        //            i++;
        //            for (int x = 0; x <= i; x++)
        //            {
        //                temp.guidList.Add(Guid.NewGuid());
        //            }

        //            client.SendTo(new byte[] { 1, 2, 3, 4, 5, 6 });
        //            client.SendTo(BufferFormatV2.FormatFCA(temp));  //讲一个PPO对象发送出去
        //            BufferFormat buffmat = new BufferFormatV2(1001);
        //            buffmat.AddItem(i);
        //            buffmat.AddItem("通过组合数据包通讯，GUID is object");
        //            buffmat.AddItem(Guid.NewGuid());
        //            client.SendTo(buffmat.Finish()); //用组合数据包模拟PPO对象

        //            BufferFormat buffmat2 = new BufferFormatV2(1002);
        //            buffmat2.AddItem(i);
        //            buffmat2.AddItem("通过组合数据包通讯 all buff");
        //            buffmat2.AddItem(Guid.NewGuid().ToString());
        //            client.SendTo(buffmat2.Finish()); //用组合数据包模拟PPO对象 但GUID 是字符串类型

        //            List<TestClass.PPo2> tmplist = new List<TestClass.PPo2>();
        //            for (int l = 0; l < 30; l++)
        //            {
        //                TestClass.PPo2 ver2 = new TestClass.PPo2();
        //                ver2.Id = i;
        //                ver2.Message = "列表复用";
        //                ver2.PPoList = new List<TestClass.PPo>();
        //                for (int j = 0; j < 10; j++)
        //                {
        //                    TestClass.PPo temp2 = new TestClass.PPo();
        //                    temp2.Id = i;
        //                    temp2.Message = "通过对象通讯";
        //                    temp2.guidList = new List<Guid>();

        //                    for (int x = 0; x <= 20; x++)
        //                    {
        //                        temp2.guidList.Add(Guid.NewGuid());
        //                    }
        //                    ver2.PPoList.Add(temp2);
        //                }
        //                tmplist.Add(ver2);
        //            }
        //            BufferFormatV2 buffer = new BufferFormatV2(2000);
        //            buffer.AddItem(tmplist);
        //            client.SendTo(buffer.Finish());
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("无法连接服务器");
        //    }
        //    Console.ReadLine();
        //}
        #endregion

        static void Client_Disconnection(string message)
        {
            Console.WriteLine(message);
        }

        static void Client_DataOn(byte[] Data)
        {
            throw new NotImplementedException();
        }

    }
}
