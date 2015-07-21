using System;
using System.Collections.Generic;
using System.Text;

namespace ZYSocket.Share
{
    public class BufferFormatV2 : BufferFormat
    {
        public BufferFormatV2(int buffType, FormatDataCompletedEventHandler formatDataCompleted)
            : base(buffType, formatDataCompleted)
        {
            bufferList.Clear();
            bufferList.AddRange(GetBytes(buffType));
            Encode = Encoding.Unicode;
            isFinish = false;
            this.FormatDataCompleted = formatDataCompleted;

        }

        public BufferFormatV2(int buffType)
            : base(buffType)
        {
            bufferList.Clear();
            bufferList.AddRange(GetBytes(buffType));
            Encode = Encoding.Unicode;
            isFinish = false;
        }

        public override void AddItem(int data)
        {
            for (; ; )
            {
                if ((data & ~127) == 0)
                {
                    AddItem((byte)data);
                    return;
                }
                AddItem((byte)(data & 127 | 128));
                data = data >> 7;
            }
        }

        public override void AddItem(object obj)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            byte[] data = SerializeObject(obj);
            bufferList.AddRange(GetBytes(data.Length));
            bufferList.AddRange(data);
        }



        public override void AddItem(string data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            Byte[] bytes = Encode.GetBytes(data);
            bufferList.AddRange(GetBytes(bytes.Length));
            bufferList.AddRange(bytes);
        }


        public override void AddItem(byte[] data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            byte[] ldata = GetBytes(data.Length);
            bufferList.AddRange(ldata);
            bufferList.AddRange(data);
        }

        public override byte[] Finish()
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");


            if (FormatDataCompleted != null)
            {
                byte[] fdata = FormatDataCompleted(bufferList.ToArray());
                bufferList.Clear();
                bufferList.AddRange(fdata);
            }




            int x = bufferList.Count;

            if ((bufferList.Count + 1) < 128)
            {
                x += 1;
            }
            else if ((bufferList.Count + 2) < 16384)
            {
                x += 2;
            }
            else if ((bufferList.Count + 3) < 2097152)
            {
                x += 3;
            }
            else
            {
                x += 4;
            }


            byte[] tmp = GetBytes(x);

            int l = bufferList.Count + tmp.Length;

            byte[] data = GetBytes(l);

            for (int i = data.Length - 1; i >= 0; i--)
            {
                bufferList.Insert(0, data[i]);
            }
            bufferList.Insert(0, 0xff);

            byte[] datap = new byte[bufferList.Count];

            bufferList.CopyTo(0, datap, 0, datap.Length);

            bufferList.Clear();
            isFinish = true;

            return datap;
        }

        /// <summary>
        /// 直接格式化一个带FormatClassAttibutes 标签的类，返回BYTE[]数组，此数组可以直接发送不需要组合所数据包。所以也称为类抽象数据包
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static new byte[] FormatFCA(object o)
        {
            return FormatFCA(o, null);
        }

        /// <summary>
        /// 直接格式化一个带FormatClassAttibutes 标签的类，返回BYTE[]数组，此数组可以直接发送不需要组合所数据包。所以也称为类抽象数据包
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dataExtra">数据加密回调</param>
        /// <returns></returns>
        public static new byte[] FormatFCA(object o, FormatDataCompletedEventHandler dataExtra)
        {
            Type otype = o.GetType();
            Attribute[] Attributes = Attribute.GetCustomAttributes(otype);

            foreach (Attribute p in Attributes)
            {
                FormatClassAttributes fca = p as FormatClassAttributes;

                if (fca != null)
                {
                    List<byte> bufferList = new List<byte>();

                    bufferList.AddRange(GetBytes(fca.BufferCmdType));

                    byte[] classdata = SerializeObject(o);
                    bufferList.AddRange(GetBytes(classdata.Length));
                    bufferList.AddRange(classdata);

                    if (dataExtra != null)
                    {
                        byte[] fdata = dataExtra(bufferList.ToArray());
                        bufferList.Clear();
                        bufferList.AddRange(fdata);
                    }


                    int x = bufferList.Count;

                    if ((bufferList.Count + 1) < 128)
                    {
                        x += 1;
                    }
                    else if ((bufferList.Count + 2) < 16384)
                    {
                        x += 2;
                    }
                    else if ((bufferList.Count + 3) < 2097152)
                    {
                        x += 3;
                    }
                    else
                    {
                        x += 4;
                    }

                    byte[] tmp = GetBytes(x);

                    int l = bufferList.Count + tmp.Length;

                    byte[] data = GetBytes(l);

                    for (int i = data.Length - 1; i >= 0; i--)
                    {
                        bufferList.Insert(0, data[i]);
                    }

                    bufferList.Insert(0, 0xff);

                    byte[] datap = new byte[bufferList.Count];

                    bufferList.CopyTo(0, datap, 0, datap.Length);

                    bufferList.Clear();

                    return datap;
                }
            }

            throw new EntryPointNotFoundException("无法找到 FormatClassAttibutes 标签");
        }

        public static byte[] GetBytes(int data)
        {
            List<byte> pdata = new List<byte>();

            for (; ; )
            {
                if ((data & ~127) == 0)
                {
                    pdata.Add((byte)data);
                    return pdata.ToArray();
                }
                pdata.Add((byte)(data & 127 | 128));
                data = data >> 7;
            }
        }
    }
}
