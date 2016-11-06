using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using PersonPosition.Model;
using PersonPosition.Common;

namespace PersonPosition.StaticService
{
    public static class Socket_Service
    {
        //服务器定位信息更新事件
        public static event UpdatePositionEventHandler Event_UpdatePosition;
        //服务器采集器通道信息更新事件
        public static event UpdateCollectChannelValueEventHandler Event_UpdateCollectChannelValue;
        //服务器返回的特殊区域内人员事件
        public static event InAreaEventHandler Event_InArea;
        //缺电事件
        public static event LowPowerHandler Event_LowPower;
        //人员发送报警表更新事件
        public static event UpMessageEventHandler Event_UpMessage;
        //得到下行短信类型事件
        public static event DownMesTypeEventHandler Event_DownMesType;
        //数据库更新事件
        public static event UpdateDBEventHandler Event_UpdateDB;

        private static TcpClient clientSocket;
        private static Thread thread_receive;
        private static bool key_receive;
        private const int BufferSize = 32768;

        //
        //命令返回开关变量
        //
        //代表登录命令的返回的状态
        private static bool Result_Reg = false;
        //代表点亮灯命令的返回的状态
        private static bool Result_LightUp = false;
        //代表特殊区域命令返回的状态
        private static bool Result_AreaSubject = false;
        //代表强制离开命令返回的状态
        private static bool Result_HandCheckOut = false;

        //服务器命令
        public const string Command_S2C_ShutDown = "S2C_SD";
        public const string Command_S2C_LowPower = "S2C_LP";
        public const string Command_S2C_UpMessage = "S2C_UM";
        public const string Command_S2C_DownMesType = "S2C_MT";
        public const string Command_S2C_InArea = "S2C_IA";
        public const string Command_S2C_UpdatePosition = "S2C_UP";
        public const string Command_S2C_UpdateDB = "S2C_UD";
        public const string Command_S2C_UpdateCollectChannel = "S2C_UC";
        //客户端命令
        public const string Command_C2S_Reg = "C2S_RE";
        public const string Command_C2S_ConnTick = "C2S_CT";
        public const string Command_C2S_UnReg = "C2S_UR";
        public const string Command_C2S_AddRelation = "C2S_AR";
        public const string Command_C2S_DelRelation = "C2S_DR";
        public const string Command_C2S_DownMessage = "C2S_DM";
        public const string Command_C2S_RequestDownMesType = "C2S_MT";
        public const string Command_C2S_RequestInArea = "C2S_IA";
        public const string Command_C2S_LightUp = "C2S_LU";
        public const string Command_C2S_UpdateDB = "C2S_UD";
        public const string Command_C2S_AreaSubject = "C2S_AS";
        public const string Command_C2S_HandCheckOut = "C2S_HC";
        public const string Command_C2S_SetInfo = "C2S_SI";
        //客户端命令的返回
        public const string RES_S2C_Reg = "RES_RE";
        public const string RES_S2C_LightUp = "RES_LU";
        public const string RES_S2C_AreaSubject = "RES_AS";
        public const string RES_S2C_HandCheckOut = "RES_HC";
       // public static Mutex mutex =  new Mutex();

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool ConnectServer(string IP,int port)
        {
            try
            {
                clientSocket = new TcpClient();
                clientSocket.ReceiveBufferSize = BufferSize;
                string[] temp = IP.Split('.');
                if (temp.Length == 4)
                {
                    if (temp[0] != "" && temp[1] != "" && temp[2] != "" && temp[3] != "")
                    {
                        clientSocket.Connect(IPAddress.Parse(IP), port);
                        key_receive = true;
                        thread_receive = new Thread(new ThreadStart(Receive));
                        thread_receive.Start();
                        return true;
                    }
                }
                clientSocket.Connect(IP, port);
                key_receive = true;
                thread_receive = new Thread(new ThreadStart(Receive));
                thread_receive.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void DisconnectServer()
        {
            try
            {
                clientSocket.Client.Shutdown(SocketShutdown.Both);
                thread_receive.Abort();
                key_receive = false;

                clientSocket.Client.Close();

            }
            catch { ;}
        }

        /// <summary>
        /// 监听线程的数据处理方法
        /// </summary>
        private static void Receive()
        {
            try
            {
                while (key_receive)
                {
                    byte[] buffer = new byte[BufferSize];
                    int bytes = clientSocket.Client.Receive(buffer);
                    if (bytes == 0)
                    {
                        continue;
                    }
                    string[] TempStrs = Encoding.Unicode.GetString(buffer, 0, bytes).Split('|');
                    string Command = "";
                    string Parameter1 = "";
                    string Parameter2 = "";
                    string Parameter3 = "";
                    string Parameter4 = "";
                    string Parameter5 = "";
                    string Parameter6 = "";
                    string Parameter7 = "";
                    string Parameter8 = "";
                    string Parameter9 = "";

                    //解析出命令字、命令参数
                    switch (TempStrs.Length)
                    {
                        case 1:
                            Command = TempStrs[0];
                            break;
                        case 2:
                            Command = TempStrs[0];
                            Parameter1 = TempStrs[1];
                            break;
                        case 3:
                            Command = TempStrs[0];
                            Parameter1 = TempStrs[1];
                            Parameter2 = TempStrs[2];
                            break;
                        case 4:
                            Command = TempStrs[0];
                            Parameter1 = TempStrs[1];
                            Parameter2 = TempStrs[2];
                            Parameter3 = TempStrs[3];
                            break;
                        case 5:
                            Command = TempStrs[0];
                            Parameter1 = TempStrs[1];
                            Parameter2 = TempStrs[2];
                            Parameter3 = TempStrs[3];
                            Parameter4 = TempStrs[4];
                            break;
                        case 6:
                            Command = TempStrs[0];
                            Parameter1 = TempStrs[1];
                            Parameter2 = TempStrs[2];
                            Parameter3 = TempStrs[3];
                            Parameter4 = TempStrs[4];
                            Parameter5 = TempStrs[5];
                            break;
                        case 7:
                            Command = TempStrs[0];
                            Parameter1 = TempStrs[1];
                            Parameter2 = TempStrs[2];
                            Parameter3 = TempStrs[3];
                            Parameter4 = TempStrs[4];
                            Parameter5 = TempStrs[5];
                            Parameter6 = TempStrs[6];
                            break;
                        case 8:
                            Command = TempStrs[0];
                            Parameter1 = TempStrs[1];
                            Parameter2 = TempStrs[2];
                            Parameter3 = TempStrs[3];
                            Parameter4 = TempStrs[4];
                            Parameter5 = TempStrs[5];
                            Parameter6 = TempStrs[6];
                            Parameter7 = TempStrs[7];
                            break;
                        case 9:
                            Command = TempStrs[0];
                            Parameter1 = TempStrs[1];
                            Parameter2 = TempStrs[2];
                            Parameter3 = TempStrs[3];
                            Parameter4 = TempStrs[4];
                            Parameter5 = TempStrs[5];
                            Parameter6 = TempStrs[6];
                            Parameter7 = TempStrs[7];
                            Parameter8 = TempStrs[8];
                            break;
                        case 10:
                            Command = TempStrs[0];
                            Parameter1 = TempStrs[1];
                            Parameter2 = TempStrs[2];
                            Parameter3 = TempStrs[3];
                            Parameter4 = TempStrs[4];
                            Parameter5 = TempStrs[5];
                            Parameter6 = TempStrs[6];
                            Parameter7 = TempStrs[7];
                            Parameter8 = TempStrs[8];
                            Parameter9 = TempStrs[9];
                            break;
                    }

                    switch (Command)
                    {
                        case Socket_Service.Command_S2C_UpdatePosition:
                            //异步抛出服务器定位信息更新事件
                            if (Event_UpdatePosition != null)
                            {
                                string[] tempAreaList = Parameter5.Split('!');
                                string[] tempJustNowInOut = Parameter9.Split('!');
                                Delegate[] delegateList = Event_UpdatePosition.GetInvocationList();
                                foreach (UpdatePositionEventHandler UPEH in delegateList)
                                {
                                    if (tempAreaList[0] == "Stop")
                                    {
                                        UPEH.BeginInvoke(Convert.ToBoolean(Parameter1), Parameter2, Parameter3, Parameter4.Split('!'), tempAreaList[0], 0, Convert.ToBoolean(tempAreaList[1]), Convert.ToBoolean(Parameter6), Convert.ToBoolean(Parameter7), Convert.ToBoolean(Parameter8), tempJustNowInOut[0].Split('?'), tempJustNowInOut[1].Split('?'), null, null);
                                    }
                                    else
                                    {
                                        UPEH.BeginInvoke(Convert.ToBoolean(Parameter1), Parameter2, Parameter3, Parameter4.Split('!'), tempAreaList[0], Convert.ToInt32(tempAreaList[1]), Convert.ToBoolean(tempAreaList[2]), Convert.ToBoolean(Parameter6), Convert.ToBoolean(Parameter7), Convert.ToBoolean(Parameter8), tempJustNowInOut[0].Split('?'), tempJustNowInOut[1].Split('?'), null, null);
                                    }
                                }
                            }
                            break;
                        case Socket_Service.Command_S2C_UpdateCollectChannel:
                            //异步抛出服务器采集器通道信息更新事件
                            if (Event_UpdateCollectChannelValue != null)
                            {
                                Delegate[] delegateList = Event_UpdateCollectChannelValue.GetInvocationList();
                                foreach (UpdateCollectChannelValueEventHandler UPEH in delegateList)
                                {
                                    UPEH.BeginInvoke(Convert.ToInt32(Parameter1), Convert.ToInt32(Parameter2), Convert.ToDouble(Parameter3), Convert.ToInt32(Parameter4), DateTime.Now, null, null);
                                }
                            }
                            break;
                        case Socket_Service.Command_S2C_ShutDown:
                            System.Environment.Exit(0);
                            break;
                        case Socket_Service.Command_S2C_LowPower:
                            //异步抛出缺电事件
                            if (Event_LowPower != null)
                            {
                                Delegate[] delegateList = Event_LowPower.GetInvocationList();
                                foreach (LowPowerHandler UPEH in delegateList)
                                {
                                    UPEH.BeginInvoke(Convert.ToInt32(Parameter1), Convert.ToDateTime(Parameter2), null, null);
                                }
                            }
                            break;
                        case Socket_Service.Command_S2C_UpMessage:
                            //异步抛出人员发送报警信息事件
                            if (Event_UpMessage != null)
                            {
                                Delegate[] delegateList = Event_UpMessage.GetInvocationList();
                                foreach (UpMessageEventHandler UPEH in delegateList)
                                {
                                    UPEH.BeginInvoke(Convert.ToInt32(Parameter1), Parameter2, Convert.ToDateTime(Parameter3), null, null);
                                }
                            }
                            break;
                        case Socket_Service.Command_S2C_DownMesType:
                            //异步抛出得到下行短信类型的事件
                            if (Event_DownMesType != null)
                            {
                                Delegate[] delegateList = Event_DownMesType.GetInvocationList();
                                foreach (DownMesTypeEventHandler UPEH in delegateList)
                                {
                                    UPEH.BeginInvoke(Parameter1.Split('='), null, null);
                                }
                            }
                            break;
                        case Socket_Service.Command_S2C_InArea:
                            //解析出所有进入特殊区域人员信息
                            Dictionary<int, int> tempTable1 = new Dictionary<int, int>();
                            string[] CardList1 = Parameter2.Split('!');
                            for (int i = 0; i < CardList1.Length; i++)
                            {
                                string[] TempList1 = CardList1[i].Split('?');
                                if (TempList1.Length == 2)
                                {
                                    tempTable1.Add(Convert.ToInt32(TempList1[0]), Convert.ToInt32(TempList1[1]));
                                }

                            }
                            //异步抛出得到全部进入特殊区域人员信息事件
                            if (Event_InArea != null)
                            {
                                Delegate[] delegateList = Event_InArea.GetInvocationList();
                                foreach (InAreaEventHandler UPEH in delegateList)
                                {
                                    UPEH.BeginInvoke(Convert.ToInt32(Parameter1), tempTable1, null, null);
                                }
                            }
                            break;
                        case Socket_Service.Command_S2C_UpdateDB:
                            //异步抛出数据库更新消息
                            if (Event_UpdateDB != null)
                            {
                                Delegate[] delegateList = Event_UpdateDB.GetInvocationList();
                                foreach (UpdateDBEventHandler UPEH in delegateList)
                                {
                                    UPEH.BeginInvoke(Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6, Parameter7, Parameter8, Parameter9, null, null);
                                }
                            }
                            break;
                        case Socket_Service.RES_S2C_Reg:
                            switch (Parameter1)
                            {
                                case "0":
                                    DateTime ServerTime = Convert.ToDateTime(Parameter2);
                                    //如果时间相差5秒以上则自动校时
                                    TimeSpan TS = DateTime.Now - ServerTime;
                                    if (Math.Abs(TS.TotalSeconds) > 5.0)
                                    {
                                        CommonFun.SetSystemTime(ServerTime);
                                    }
                                    //是否使用了红外设备
                                    Global.IsUseHongWai = Convert.ToBoolean(Parameter3);
                                    //是否是演示版
                                    Global.IsTempVersion = Convert.ToBoolean(Parameter4);
                                    Result_Reg = true;
                                    break;
                                case "1":
                                    Result_Reg = false;
                                    break;
                                case "2":
                                    Result_Reg = false;
                                    break;
                            }
                            break;
                        case Socket_Service.RES_S2C_LightUp:
                            Result_LightUp = true;
                            break;
                        case Socket_Service.RES_S2C_AreaSubject:
                            Result_AreaSubject = true;
                            break;
                        case Socket_Service.RES_S2C_HandCheckOut:
                            Result_HandCheckOut = true;
                            break;
                    }
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionReset && Global.State_IsServicing)//服务器未停止即退出
                {
                    Global.State_IsServerRunning = false;
                    key_receive = false;
                    DisconnectServer();

                }

                if (Global.IsShowBug)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端解析服务器命令错误");
                }
            }
            finally
            {
                thread_receive.Abort();
                ;
                DisconnectServer();
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Message"></param>
        public static void SendMessage(string Command, string Parameter1, string Parameter2, string Parameter3, string Parameter4, string Parameter5, string Parameter6, string Parameter7, string Parameter8, string Parameter9)
        {
            clientSocket.Client.Send(Encoding.Unicode.GetBytes(Command + "|" + Parameter1 + "|" + Parameter2 + "|" + Parameter3 + "|" + Parameter4 + "|" + Parameter5 + "|" + Parameter6 + "|" + Parameter7 + "|" + Parameter8 + "|" + Parameter9));
        }

        /// <summary>
        /// 安全的发送消息
        /// 阻塞发送线程直到收到返回的消息或者达到超时。超时：50毫秒×60=3秒
        /// 没有的参数则传""
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static bool SendMessage_Safe(string Command, string Parameter1, string Parameter2, string Parameter3, string Parameter4, string Parameter5, string Parameter6, string Parameter7, string Parameter8, string Parameter9)
        {
            switch (Command)
            {
                //登录命令
                case Socket_Service.Command_C2S_Reg:
                    Result_Reg = false;
                    clientSocket.Client.Send(Encoding.Unicode.GetBytes(Command + "|" + Parameter1 + "|" + Parameter2 + "|" + Parameter3 + "|" + Parameter4 + "|" + Parameter5 + "|" + Parameter6 + "|" + Parameter7 + "|" + Parameter8 + "|" + Parameter9));
                    int tempNumReg = 0;
                    //循环等待命令的成功返回
                    while (!Result_Reg)
                    {
                        Thread.Sleep(50);
                        tempNumReg++;
                        if (tempNumReg >= 60)
                        {
                            //超时，则把开关变量继续置为False后返回失败
                            Result_Reg = false;
                            return false;
                        }
                    }
                    //至此，说明成功收到返回，则把开关变量继续置为False后返回成功
                    Result_Reg = false;
                    break;
                //客户端心跳
                case Socket_Service.Command_C2S_ConnTick:
                    try
                    {

                        clientSocket.Client.Send(Encoding.Unicode.GetBytes(Command + "|" + Parameter1 + "|" + Parameter2 + "|" + Parameter3 + "|" + Parameter4 + "|" + Parameter5 + "|" + Parameter6 + "|" + Parameter7 + "|" + Parameter8 + "|" + Parameter9));
                    }
                    catch (SocketException socketex)
                    {

                        //服务端停止服务时，不会断开已有的连接，若发送心跳包失败，则认为服务端异常退出，此时
                        //应强制客户端重新连接服务端
                        Console.WriteLine("发送心跳包异常" + socketex.SocketErrorCode.ToString());
                        if (socketex.SocketErrorCode == SocketError.ConnectionReset && !Global.State_IsServicing)//服务器停止后异常退出
                        {
                            Global.State_IsServerRunning = false;
                            DisconnectServer();
                        }
                        else if (socketex.SocketErrorCode == SocketError.ConnectionReset && Global.State_IsServicing)//服务器未停止即退出
                        {
                            Global.State_IsServerRunning = false;
                            DisconnectServer();
                        }
                        else if (socketex.SocketErrorCode == SocketError.ConnectionAborted && !Global.State_IsServicing)
                        {
                            Global.State_IsServerRunning = false;
                            DisconnectServer();
                        }


                    }
                    catch (Exception ex)
                    {
                        if (Global.IsShowBug)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "发送心跳包错误");
                        }
                    }
                    break;
                //点亮灯命令
                case Socket_Service.Command_C2S_LightUp:
                    Result_LightUp = false;
                    clientSocket.Client.Send(Encoding.Unicode.GetBytes(Command + "|" + Parameter1 + "|" + Parameter2 + "|" + Parameter3 + "|" + Parameter4 + "|" + Parameter5 + "|" + Parameter6 + "|" + Parameter7 + "|" + Parameter8 + "|" + Parameter9));
                    int tempNumLightUp = 0;
                    //循环等待命令的成功返回
                    while (!Result_LightUp)
                    {
                        Thread.Sleep(50);
                        tempNumLightUp++;
                        if (tempNumLightUp >= 60)
                        {
                            //超时，则把开关变量继续置为False后返回失败
                            Result_LightUp = false;
                            return false;
                        }
                    }
                    //至此，说明成功收到返回，则把开关变量继续置为False后返回成功
                    Result_LightUp = false;
                    break;
                //特殊区域命令
                case Socket_Service.Command_C2S_AreaSubject:
                    Result_AreaSubject = false;
                    clientSocket.Client.Send(Encoding.Unicode.GetBytes(Command + "|" + Parameter1 + "|" + Parameter2 + "|" + Parameter3 + "|" + Parameter4 + "|" + Parameter5 + "|" + Parameter6 + "|" + Parameter7 + "|" + Parameter8 + "|" + Parameter9));
                    int tempAreaCommand = 0;
                    //循环等待命令的成功返回
                    while (!Result_AreaSubject)
                    {
                        Thread.Sleep(50);
                        tempAreaCommand++;
                        if (tempAreaCommand >= 60)
                        {
                            //超时，则把开关变量继续置为False后返回失败
                            Result_AreaSubject = false;
                            return false;
                        }
                    }
                    //至此，说明成功收到返回，则把开关变量继续置为False后返回成功
                    Result_AreaSubject = false;
                    break;
                //强制离开命令
                case Socket_Service.Command_C2S_HandCheckOut:
                    Result_HandCheckOut = false;
                    clientSocket.Client.Send(Encoding.Unicode.GetBytes(Command + "|" + Parameter1 + "|" + Parameter2 + "|" + Parameter3 + "|" + Parameter4 + "|" + Parameter5 + "|" + Parameter6 + "|" + Parameter7 + "|" + Parameter8 + "|" + Parameter9));
                    int tempHandCheckOut = 0;
                    //循环等待命令的成功返回
                    while (!Result_HandCheckOut)
                    {
                        Thread.Sleep(50);
                        tempHandCheckOut++;
                        if (tempHandCheckOut >= 60)
                        {
                            //超时，则把开关变量继续置为False后返回失败
                            Result_HandCheckOut = false;
                            return false;
                        }
                    }
                    //至此，说明成功收到返回，则把开关变量继续置为False后返回成功
                    Result_HandCheckOut = false;
                    break;
            }
            return true;
        }
    }
}
