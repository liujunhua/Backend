using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSocket.Share;

namespace Server.User
{
    //用户对象
    public class UserInfo
    {
        public string UserName { get; set; }
        public BufferList BuffManger { get; set; }

        public UserInfo()
        {
            BuffManger = new BufferList(RConfig.ReadInt("MaxBufferSize"));
        }
    }
}
