using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSocket.Share;
namespace BufferLibrary
{
    /// <summary>
    /// 用于发送消息
    /// </summary>
    [Serializable] //Demo3.0
    [FormatClassAttributes(500)]
    public class Message
    {
        /// <summary>
        /// 消息类型,1 登入失败,2登入成功..其他 未定义
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 消息字符串
        /// </summary>
        public string MessageStr { get; set; }
    }

    /// <summary>
    /// 登入数据包
    /// </summary>
    [Serializable] //Demo3.0
    [FormatClassAttributes(1000)]
    public class Login
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }

    /// <summary>
    /// PING 数据包
    /// </summary>
    [Serializable] //Demo3.0
    [FormatClassAttributes(800)]
    public class Ping
    {
        /// <summary>
        /// 发送的时候记录的时间
        /// </summary>
        public DateTime UserSendTime { get; set; }
        /// <summary>
        /// 服务器接受记录的时间
        /// </summary>
        public DateTime ServerReviceTime { get; set; }
    }

    public class DataValue
    {
        public string V1 { get; set; }
        public string V2 { get; set; }
        public string V3 { get; set; }
        public string V4 { get; set; }
        public string V5 { get; set; }

    }

    /// <summary>
    /// 读取DATASET请求
    /// </summary>  
    [Serializable]  //Demo3.0
    [FormatClassAttributes(1002)]
    public class ReadDataSet
    {
        public string TableName { get; set; }
        public List<DataValue> Data { get; set; }
    }

}
