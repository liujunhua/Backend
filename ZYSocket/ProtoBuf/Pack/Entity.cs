using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSocket.share;
using ProtoBuf;

namespace Pack
{
    [Flags]
    public enum PackType
    {
        LogOn = 1001,
        LogOnRes = 1002,
        Data = 1003,
        DataRes = 1004
    }


    [ProtoContract] //Demo8.0 Protobuf
    [FormatClassAttibutes((int)PackType.LogOn)]
    public class Logon
    {
        // [MessagePackMember(0)] //Demo7.0 MsgPack
        [ProtoMember(1)]
        public string username { get; set; }

        // [MessagePackMember(1)] //Demo7.0 MsgPack
        [ProtoMember(2)]
        public string password { get; set; }
    }

    [ProtoContract] //Demo8.0 Protobuf
    [FormatClassAttibutes((int)PackType.LogOnRes)]
    public class LogonRes
    {
        // [MessagePackMember(0)] //Demo7.0 MsgPack
        [ProtoMember(1)] //Demo8.0 Protobuf
        public bool IsLogOn { get; set; }

        // [MessagePackMember(1)] //Demo7.0 MsgPack
        [ProtoMember(2)] //Demo8.0 Protobuf
        public string Msg { get; set; }
    }

    [ProtoContract]//Demo8.0 Protobuf
    [FormatClassAttibutes((int)PackType.Data)]
    public class Data
    {
        // [MessagePackMember(0)] //Demo7.0 MsgPack
        [ProtoMember(1)] //Demo8.0 Protobuf
        public string CMD { get; set; }
    }

    [ProtoContract] //Demo8.0 Protobuf
    [FormatClassAttibutes((int)PackType.DataRes)]
    public class DataRes
    {
        // [MessagePackMember(0)] //Demo7.0 MsgPack
        [ProtoMember(1)] //Demo8.0 Protobuf
        public int Type { get; set; }

        // [MessagePackMember(1)] //Demo7.0 MsgPack
        [ProtoMember(2)] //Demo8.0 Protobuf
        public List<string> Res { get; set; }
    }


}
