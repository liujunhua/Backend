﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Reflection;

namespace ZYSocket.Share
{
    public delegate void CmdCallHandler<Server,ReadType,UserT>(Server server,ReadType read,UserT user);

    public class CmdToCallManager<Server,CmdType,ReadType,UserT>
    {
                       
        #region 全局静态唯一对象
        static object lockthis = new object();

        static CmdToCallManager<Server,CmdType, ReadType, UserT> _My;

        public static CmdToCallManager<Server,CmdType, ReadType, UserT> GetInstance()
        {
            lock (lockthis)
            {
              
                if (_My == null)
                    _My = new CmdToCallManager<Server,CmdType, ReadType, UserT>();
            }

            return _My;
        }

        private CmdToCallManager()
        {
            CmdDiy = new Dictionary<CmdType, CmdCallHandler<Server, ReadType, UserT>>();
        }
        #endregion


        private Dictionary<CmdType, CmdCallHandler<Server,ReadType, UserT>> CmdDiy { get; set; }


        public void AddCall(CmdType cmd, CmdCallHandler<Server,ReadType, UserT> call)
        {
            if (CmdDiy.ContainsKey(cmd))
            {
                throw new Exception("have the key:" + cmd);
            }
            else
            {
                CmdDiy.Add(cmd, call);
            }
        }


        public bool pointerRun(Server server,CmdType cmd, ReadType read, UserT user)
        {
            if (CmdDiy.ContainsKey(cmd))
            {
                CmdDiy[cmd](server, read, user);

                return true;
            }

            return false;
        }


     
    }

    public class CmdToCallManager<Server, ReadType, UserT>
    {

        #region 全局静态唯一对象
        static object lockthis = new object();

        static CmdToCallManager<Server, ReadType, UserT> _My;

        public static CmdToCallManager<Server, ReadType, UserT> GetInstance()
        {
            lock (lockthis)
            {

                if (_My == null)
                    _My = new CmdToCallManager<Server, ReadType, UserT>();
            }

            return _My;
        }

        private CmdToCallManager()
        {
            CmdDiy = new Dictionary<int, MethodCall>();
        }
        #endregion


        private Dictionary<int, MethodCall> CmdDiy { get; set; }


        public void AddPackerObj(ICmdToCall obj)
        {
            if (obj == null)
                return;

            Type objType = obj.GetType();

            foreach (var item in objType.GetMethods())
            {
                var attr = item.GetCustomAttributes(typeof(CmdTypeOfAttibutes), true);

                if (attr.Length ==1)
                {
                    CmdTypeOfAttibutes attrcmdtype = attr[0] as CmdTypeOfAttibutes;

                    if (attrcmdtype != null)
                    {
                        MethodCall tmp = new MethodCall(attrcmdtype.CmdType, obj, item);

                        if (CmdDiy.ContainsKey(tmp.Cmd))
                        {
                            throw new Exception("have the key:"+tmp.Cmd);
                        }else
                            CmdDiy.Add(tmp.Cmd, tmp);
                    }

                }                
            }
        }


        public bool pointerRun(Server server, int cmd, ReadType read, UserT user)
        {
            if (CmdDiy.ContainsKey(cmd))
            {
                CmdDiy[cmd].Call(server, read, user);

                return true;
            }

            return false;
        }



        private class MethodCall
        {

            public int Cmd { get; private set; }
            public object Obj { get;  private set; }
            public MethodInfo Method { get;  private set; }



            public MethodCall(int cmd,object obj,MethodInfo method)
            {
                this.Cmd = cmd;
                this.Obj = obj;
                this.Method = method;
            }

            public void Call(Server server,ReadType read,UserT userinfo)
            {
                Method.Invoke(Obj, new object[] { server, read, userinfo });
            }
        }
    }




    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CmdTypeOfAttibutes : Attribute
    {
        public int CmdType { get; set; }

        /// <summary>
        /// 数据包格式化类
        /// </summary>
        /// <param name="bufferCmdType">数据包命令类型</param>
        public CmdTypeOfAttibutes(int bufferCmdType)
        {
            this.CmdType = bufferCmdType;
        }
    }

    public interface ICmdToCall
    {
        void Loading();

    }


}
