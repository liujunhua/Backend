using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYSocket.Share;

namespace TestClass
{
    /// <summary>
    /// PPO对象 测试用的对象
    /// 他一定要打 Serializable 标志 和 FormatClassAttibutes 标志
    /// Serializable 用户序列化对象，而 FormatClassAttibutes 用于定义此数据包的命令
    /// </summary>   
    [FormatClassAttributes(1000)]
    public class PPo
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public List<Guid> GuidList { get; set; }//demo1.3 guid修改为guidList
    }

    #region Demo1.3 修改PPo 增加 PPo2

    [FormatClassAttributes(2000)]
    public class PPo2
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public List<PPo> PPoList { get; set; }
    }
    #endregion

}
