using System;
using System.Collections.Generic;
using System.Text;

namespace ZYSocket.Share
{
    /// <summary>
    /// 数据包格式化类（凡是打了此标记的类才能够被 BufferFormat.FormatFCA 处理)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class FormatClassAttributes : Attribute
    {
        /// <summary>
        /// 数据包格式化类
        /// </summary>
        /// <param name="bufferCmdType">数据包命令类型</param>
        public FormatClassAttributes(int bufferCmdType)
        {
            this.BufferCmdType = bufferCmdType;
        }

        public int BufferCmdType { get; set; }

    }
}
