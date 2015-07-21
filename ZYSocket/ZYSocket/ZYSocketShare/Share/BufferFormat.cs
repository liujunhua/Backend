using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace ZYSocket.Share
{

    public enum BufferFormatType
    {
        XML = 0,
        Binary = 1,
        SharpSerializerXML = 2,
        SharpSerializerBinary = 3,
#if Net4
        MsgPack = 4,
        protobuf = 5,
#endif

    }

    /// <summary>
    /// 数据包在格式化完毕后回调方法。（例如加密，压缩等）
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public delegate byte[] FormatDataCompletedEventHandler(byte[] data);

    /// <summary>
    /// 数据包格式化类(此类功能是把.NET数据转换成通讯数据包）
    /// </summary>
    public class BufferFormat
    {

        /// <summary>
        /// 对象格式化方式
        /// </summary>
        public static BufferFormatType FormatType { get; set; }

        static BufferFormat()
        {

#if Net4
            FormatType = BufferFormatType.MsgPack;
#else
            FormatType = BufferFormatType.SharpSerializerBinary;
#endif
        }


        protected List<byte> bufferList;

        /// <summary>
        /// 字符串格式化字符编码
        /// </summary>
        public Encoding Encode { get; set; }

        protected FormatDataCompletedEventHandler FormatDataCompleted;

        protected bool isFinish;
        /// <summary>
        /// 数据包格式化类
        /// </summary>
        /// <param name="buffType">包类型</param>
        /// <param name="dataExtra">数据包在格式化完毕后回调方法。（例如加密，压缩等）</param>
        public BufferFormat(int buffType, FormatDataCompletedEventHandler formatDataCompleted)
        {

            bufferList = new List<byte>();
            bufferList.AddRange(GetSocketBytes(buffType));
            Encode = Encoding.Unicode;
            isFinish = false;
            this.FormatDataCompleted = formatDataCompleted;
        }


        /// <summary>
        /// 数据包格式化类
        /// </summary>
        /// <param name="buffType">包类型</param>
        public BufferFormat(int buffType)
        {

            bufferList = new List<byte>();
            bufferList.AddRange(GetSocketBytes(buffType));
            Encode = Encoding.Unicode;
            isFinish = false;
        }

        #region 布尔值
        /// <summary>
        /// 添加一个布尔值
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddItem(bool data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");
            bufferList.AddRange(GetSocketBytes(data));
        }

        #endregion

        #region 整数
        /// <summary>
        /// 添加一个1字节的整数
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddItem(byte data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            bufferList.Add(data);
        }

        /// <summary>
        /// 添加一个2字节的整数
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddItem(Int16 data)
        {
            bufferList.AddRange(GetSocketBytes(data));
        }

        /// <summary>
        /// 添加一个2字节的整数
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddItem(UInt16 data)
        {
            bufferList.AddRange(GetSocketBytes(data));
        }

        /// <summary>
        /// 添加一个4字节的整数
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddItem(Int32 data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            bufferList.AddRange(GetSocketBytes(data));
        }


        /// <summary>
        /// 添加一个4字节的整数
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddItem(UInt32 data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            bufferList.AddRange(GetSocketBytes(data));
        }

        /// <summary>
        /// 添加一个8字节的整数
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddItem(Int64 data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            bufferList.AddRange(GetSocketBytes(data));
        }

        /// <summary>
        /// 添加一个8字节的整数
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddItem(UInt64 data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            bufferList.AddRange(GetSocketBytes(data));
        }

        #endregion

        #region 浮点数

        /// <summary>
        /// 添加一个4字节的浮点
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddItem(float data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            bufferList.AddRange(GetSocketBytes(data));
        }

        /// <summary>
        /// 添加一个8字节的浮点
        /// </summary>
        /// <param name="data"></param>
        public void AddItem(double data)
        {
            bufferList.AddRange(GetSocketBytes(data));
        }

        #endregion

        #region 数据包

        /// <summary>
        /// 添加一个BYTE[]数据包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual void AddItem(Byte[] data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            byte[] ldata = GetSocketBytes(data.Length);
            bufferList.AddRange(ldata);
            bufferList.AddRange(data);

        }

        #endregion

        #region 字符串
        /// <summary>
        /// 添加一个字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual void AddItem(String data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            Byte[] bytes = Encode.GetBytes(data);
            bufferList.AddRange(GetSocketBytes(bytes.Length));
            bufferList.AddRange(bytes);

        }

        #endregion

        #region 时间
        /// <summary>
        /// 添加一个一个DATATIME
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual void AddItem(DateTime data)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            AddItem(data.ToString());
        }

        #endregion

        #region 对象
        /// <summary>
        /// 将一个对象转换为二进制数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void AddItem(object obj)
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            byte[] data = SerializeObject(obj);
            bufferList.AddRange(GetSocketBytes(data.Length));
            bufferList.AddRange(data);
        }

        #endregion

        /// <summary>
        /// 直接格式化一个带FormatClassAttibutes 标签的类，返回BYTE[]数组，此数组可以直接发送不需要组合所数据包。所以也称为类抽象数据包
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static byte[] FormatFCA(object o)
        {
            return FormatFCA(o, null);
        }

        /// <summary>
        /// 直接格式化一个带FormatClassAttibutes 标签的类，返回BYTE[]数组，此数组可以直接发送不需要组合所数据包。所以也称为类抽象数据包
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dataExtra">数据加密回调</param>
        /// <returns></returns>
        public static byte[] FormatFCA(object o, FormatDataCompletedEventHandler dataExtra)
        {
            Type otype = o.GetType();
            Attribute[] Attributes = Attribute.GetCustomAttributes(otype);

            foreach (Attribute p in Attributes)
            {
                FormatClassAttributes fca = p as FormatClassAttributes;

                if (fca != null)
                {
                    List<byte> bufferList = new List<byte>();

                    bufferList.AddRange(GetSocketBytes(fca.BufferCmdType));

                    byte[] classdata = SerializeObject(o);
                    bufferList.AddRange(GetSocketBytes(classdata.Length));
                    bufferList.AddRange(classdata);

                    if (dataExtra != null)
                    {
                        byte[] fdata = dataExtra(bufferList.ToArray());
                        bufferList.Clear();
                        bufferList.AddRange(fdata);
                    }


                    int l = bufferList.Count + 4;
                    byte[] data = GetSocketBytes(l);
                    for (int i = data.Length - 1; i >= 0; i--)
                    {
                        bufferList.Insert(0, data[i]);
                    }

                    byte[] datap = new byte[bufferList.Count];

                    bufferList.CopyTo(0, datap, 0, datap.Length);

                    bufferList.Clear();

                    return datap;
                }
            }
            throw new EntryPointNotFoundException("无法找到 FormatClassAttibutes 标签");
        }


        /// <summary>
        /// 完毕
        /// </summary>
        /// <returns></returns>
        public virtual byte[] Finish()
        {
            if (isFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");
            if (FormatDataCompleted != null)
            {
                byte[] fdata = FormatDataCompleted(bufferList.ToArray());
                bufferList.Clear();
                bufferList.AddRange(fdata);
            }
            int l = bufferList.Count + 4;
            byte[] data = GetSocketBytes(l);
            for (int i = data.Length - 1; i >= 0; i--)
            {
                bufferList.Insert(0, data[i]);
            }
            byte[] datap = new byte[bufferList.Count];
            bufferList.CopyTo(0, datap, 0, datap.Length);
            bufferList.Clear();
            isFinish = true;
            return datap;
        }



        #region V对象

        #region 注释
        ///// <summary>
        ///// 把对象序列化并返回相应的字节
        ///// </summary>
        ///// <param name="pObj">需要序列化的对象</param>
        ///// <returns>byte[]</returns>
        //public  static byte[] SerializeObject(object pObj)
        //{
        //    System.IO.MemoryStream _memory = new System.IO.MemoryStream();
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    // formatter.TypeFormat=System.Runtime.Serialization.Formatters.FormatterTypeStyle.XsdString;
        //    formatter.Serialize(_memory, pObj);
        //    _memory.Position = 0;
        //    byte[] read = new byte[_memory.Length];
        //    _memory.Read(read, 0, read.Length);
        //    _memory.Close();
        //    return read;
        //}
        #endregion

        /// <summary>
        /// 把对象序列化并返回相应的字节
        /// </summary>
        /// <param name="pObj">需要序列化的对象</param>
        /// <returns>byte[]</returns>
        public static byte[] SerializeObject(object obj)
        {
            #region 注释
            //StringBuilder sBuilder = new StringBuilder();
            //XmlSerializer xmlSerializer = new XmlSerializer(pObj.GetType());
            //XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            //xmlWriterSettings.Encoding = Encoding.Unicode;
            //XmlWriter xmlWriter = XmlWriter.Create(sBuilder, xmlWriterSettings);
            //xmlSerializer.Serialize(xmlWriter, pObj);
            //xmlWriter.Close();
            //return Encoding.UTF8.GetBytes(sBuilder.ToString());
            #endregion

            switch (FormatType)
            {
                case BufferFormatType.Binary:
                    {
                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(memoryStream, obj);
                            return memoryStream.ToArray();

                            #region 注释
                            //formatter.TypeFormat=System.Runtime.Serialization.Formatters.FormatterTypeStyle.XsdString;
                            //formatter.Serialize(memoryStream, obj);
                            //_memory.Position = 0;
                            //byte[] read = new byte[_memory.Length];
                            //_memory.Read(read, 0, read.Length);
                            //_memory.Close();                            
                            //return read;
                            #endregion
                        }
                    }
                case BufferFormatType.XML:
                    {
                        StringBuilder sBuilder = new StringBuilder();
                        XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                        XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                        xmlWriterSettings.Encoding = Encoding.Unicode;
                        XmlWriter xmlWriter = XmlWriter.Create(sBuilder, xmlWriterSettings);
                        xmlSerializer.Serialize(xmlWriter, obj);
                        xmlWriter.Close();
                        return Encoding.UTF8.GetBytes(sBuilder.ToString());
                    }
                case BufferFormatType.SharpSerializerBinary:
                    {
                        return SerializeObjects(obj, false);
                    }
                case BufferFormatType.SharpSerializerXML:
                    {
                        return SerializeObjects(obj, true);
                    }
#if Net4
                case BufferFormatType.MsgPack:
                    {
                        return MsgPack.Serialization.SerializationContext.Default.GetSerializer(obj.GetType()).PackSingleObject(obj);
                    }
                case BufferFormatType.protobuf:
                    {
                        using (System.IO.MemoryStream _memory = new System.IO.MemoryStream())
                        {
                            ProtoBuf.Meta.RuntimeTypeModel.Default.Serialize(_memory, obj);
                            return _memory.ToArray();
                        }
                    }
#endif
                default:
                    {
                        return SerializeObjects(obj, false);
                    }
            }
        }

        /// <summary>
        /// 把对象序列化并返回相应的字节
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns>byte[]</returns>
        public static byte[] SerializeObjects(object obj, bool isXml)
        {
            Polenter.Serialization.SharpSerializer serializer = new Polenter.Serialization.SharpSerializer(!isXml);
            return serializer.Serialize(obj);
        }

        #endregion

        #region V整数
        /// <summary>
        /// 将一个32位整形转换成一个BYTE[]4字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(Int32 data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个32位整形转换成一个BYTE[]4字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(UInt32 data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个64位整形转换成一个BYTE[]8字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(UInt64 data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个64位整形转换成一个BYTE[]8字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(Int64 data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个 1位CHAR转换成1位的BYTE[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(Char data)
        {
            Byte[] bytes = new Byte[] { (Byte)data };
            return bytes;
        }

        /// <summary>
        /// 将一个 16位整数转换成2位的BYTE[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(Int16 data)
        {
            return BitConverter.GetBytes(data);
        }


        /// <summary>
        /// 将一个 16位整数转换成2位的BYTE[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(UInt16 data)
        {
            return BitConverter.GetBytes(data);
        }


        #endregion

        #region V布尔值
        /// <summary>
        /// 将一个布尔值转换成一个BYTE[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(bool data)
        {
            return BitConverter.GetBytes(data);
        }
        #endregion

        #region V浮点数
        /// <summary>
        /// 将一个32位浮点数转换成一个BYTE[]4字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(float data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个64位浮点数转换成一个BYTE[]8字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(double data)
        {
            return BitConverter.GetBytes(data);
        }
        #endregion

    }
}
