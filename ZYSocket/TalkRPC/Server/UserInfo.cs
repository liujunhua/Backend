using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSocket.Share;

namespace Server
{
    public class UserInfo
    {
        public string UserName { get; set; }

        public ZYNetBufferReadStreamV2 Stream { get; set; }

        public System.Net.Sockets.SocketAsyncEventArgs Asyn { get; set; }

        public UserInfo(System.Net.Sockets.SocketAsyncEventArgs asyn)
        {
            this.Asyn = asyn;
            this.Stream = new ZYNetBufferReadStreamV2(1024 * 1024 * 4);
        }
    }

}
