using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

using PersonPosition.StaticService;

namespace PersonPosition.Common
{
    public abstract class Global
    {
        //是否是演示版(在服务器返回的登录结果里初始化)
        public static bool IsTempVersion;
        //是否使用了红外(在服务器返回的登录结果里初始化)
        public static bool IsUseHongWai;
        /// <summary>
        /// 判断客户端与服务器断开的心跳次数
        /// 注：这个次数是以MainForm的timer_UpdateUI为依据
        /// </summary>
        public const int DisconnectTimes = 60;
        //地图最小的显示比例（最大的显示比例根据地图实际比例放大2倍）
        public static double MapImageMinView = 1;
        //图层名中的分隔符
        public const string SplitKey = "_#_";
        //文件路径
        public static string MapPath = AppDomain.CurrentDomain.BaseDirectory + "Map\\";
        public static string AudioPath = AppDomain.CurrentDomain.BaseDirectory + "Audio\\";
        private static string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "Config\\";
        private static string ConfigFile = ConfigPath + "MainConfig.config";
        //当前登录用户名
        public static string PresentUser;
        //状态量
        //public static Dictionary<int, DateTime> InMineList = new Dictionary<int, DateTime>();//此刻洞内人员列表（每次都清空更新）
        public static int State_UnReadPersonMes = 0;//此刻未读信息的总数
        public static int State_UnReadLowPower = 0;//此刻未读缺电报警信息的总数
        public static bool State_IsUnReadNoCardEnter = false;//此刻无卡人员是否进入
        public static bool State_IsServicing = true;//此刻服务器是否在运行(默认True是为了跳到心跳的“未连接”)
        public static bool State_IsServerRunning = true;
        public static string State_AreaAlarmName = "未启动";//当前启动的特殊区域方案名称.如果未启动，则为"未启动"
        public static bool State_IsExceedInArea = false;//此刻特殊区域人员总数是否超限
        public static bool State_IsAlarmMaxPerson = false;//此刻是否总人数超限
        public static bool State_IsAlarmMaxHour = false;//此刻是否有人员超时
        public static int State_InArea = 0;//此刻特殊区域人员总数
        public static string[] State_ErrorStationList = new string[0] { };//此刻故障基站列表
        //客户端发送的心跳包定时间隔单位为秒
        private static string _ClientPant;
        //客户端定时检测与服务端连接的定时器间隔
        private static string _ClientCheckConnTimer;
        //地图参数
        private static string _mapName;//地图名称
        private static string _mapComment;//地图备注
        private static string _mapDistanceKey;//测距系数
        private static string _mapBackgroundPic;//地图背景图
        private static string _mapBackgroundPicGISZoom;//背景图时GIS图层的比例尺
        private static string _mapBackgroundPicGISCenterX;//背景图时GIS图层的中心点X坐标
        private static string _mapBackgroundPicGISCenterY;//背景图时GIS图层的中心点Y坐标
        private static string _titleText;

        //服务器参数
        private static string _currentlyServer;//默认连接的服务器
        private static string _serverName;//服务器名称
        private static string _serverIP;//服务器IP
        private static string _serverPort;//服务器端口
        private static string _serverDBStr;//数据库连接字串
        //颜色参数
        private static string _currentlyColor;//默认的颜色方案
        private static string _mapTitleColor;//地图名称颜色
        private static string _mapCommentColor;//地图标注颜色
        private static string _mapLabelColor;//地图标注颜色
        private static string _stationNameColor;//基站名称颜色
        private static string _stationPeopleColor;//基站人员数颜色
        private static string _personNameColor;//人员名称颜色
        private static string _personDistanceColor;//人员距离颜色
        private static string _distancePointColor;//测距点颜色
        private static string _distanceLineColor;//测距线颜色
        private static string _distanceTextColor;//测距文字颜色
        //LED参数
        private static string _lEDBasicTitle;
        private static string _lEDtextLoopTime;
        private static string _lEDIsAdvShow;
        private static string _lEDWidth;//LEDWidth
        private static string _lEDHeight;//LEDHeight
        private static string _lEDLeft;//LEDLeft
        private static string _lEDTop;//LEDTop
        private static string _lEDTopMost;//LEDTopMost
        private static string _lEDIsAreaInMineNum;
        private static string _lEDAdvText;
        private static string _lEDHengLineKeyStr;
        private static string _lEDShuLineKeyStr;
        private static string _lEDBasicFontFamily;
        private static string _lEDBasicFontSize;
        private static string _lEDAdvFontFamily;
        private static string _lEDAdvFontSize;
        private static string _lEDBasicTextColor;
        private static string _lEDAdvTextColor;
        private static string _lEDAdvLineColor;

        //其他参数
        private static string _timeToolTip;//地图提示停留时间
        private static string _autoStart;//程序自动运行
        private static string _autoLogin;//自动登录
        private static string _autoLoginUser;//自动登录用户名
        private static string _autoRunLED;//自动启动LED窗体
        private static string _lockPassword;//锁定密码
        private static string _touMingLock;//是否是透明锁定
        private static string _basicPointName;
        private static string _basicPointPositionX;
        private static string _basicPointPositionY;
        private static string _isShowBug;
        //音频参数
        private static string _audioArea;
        private static string _audioPersonSend;
        private static string _audioErrorStation;
        private static string _audioLowPower;
        private static string _title;
        private static string _product;
        #region 属性_地图参数

        public static string MapName
        {
            get
            {
                if (_mapName == null)
                {
                    if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                    {
                        _mapName = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapName"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                        System.Environment.Exit(0);
                    }
                }
                return _mapName;
            }
            set
            {
                if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                {
                    DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapName"] = value;
                    _mapName = value;
                }
                else
                {
                    MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                    System.Environment.Exit(0);
                }
            }
        }

        public static string MapComment
        {
            get
            {
                if (_mapComment == null)
                {
                    if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                    {
                        _mapComment = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["Comment"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                        System.Environment.Exit(0);
                    }
                }
                return _mapComment;
            }
            set
            {
                if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                {
                    DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["Comment"] = value;
                    _mapComment = value;
                }
                else
                {
                    MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                    System.Environment.Exit(0);
                }
            }
        }

        public static double MapDistanceKey
        {
            get
            {
                if (_mapDistanceKey == null)
                {
                    if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                    {
                        _mapDistanceKey = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapDistanceKey"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                        System.Environment.Exit(0);
                    }
                }
                return Convert.ToDouble(_mapDistanceKey);
            }
            set
            {
                if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                {
                    DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapDistanceKey"] = value;
                    _mapDistanceKey = value.ToString();
                }
                else
                {
                    MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                    System.Environment.Exit(0);
                }
            }
        }

        public static string MapBackgroundPic
        {
            get
            {
                if (_mapBackgroundPic == null)
                {
                    if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                    {
                        _mapBackgroundPic = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["BackgroundPic"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                        System.Environment.Exit(0);
                    }
                }
                return _mapBackgroundPic;
            }
            set
            {
                if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                {
                    DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["BackgroundPic"] = value;
                    _mapBackgroundPic = value;
                }
                else
                {
                    MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                    System.Environment.Exit(0);
                }
            }
        }

        public static double MapBackgroundPicGISZoom
        {
            get
            {
                if (_mapBackgroundPicGISZoom == null)
                {
                    if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                    {
                        _mapBackgroundPicGISZoom = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapBackgroundPicGISZoom"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                        System.Environment.Exit(0);
                    }
                }
                return Convert.ToDouble(_mapBackgroundPicGISZoom);
            }
            set
            {
                if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                {
                    DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapBackgroundPicGISZoom"] = value;
                    _mapBackgroundPicGISZoom = value.ToString();
                }
                else
                {
                    MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                    System.Environment.Exit(0);
                }
            }
        }

        public static double MapBackgroundPicGISCenterX
        {
            get
            {
                if (_mapBackgroundPicGISCenterX == null)
                {
                    if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                    {
                        _mapBackgroundPicGISCenterX = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapBackgroundPicGISCenterX"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                        System.Environment.Exit(0);
                    }
                }
                return Convert.ToDouble(_mapBackgroundPicGISCenterX);
            }
            set
            {
                if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                {
                    DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapBackgroundPicGISCenterX"] = value;
                    _mapBackgroundPicGISCenterX = value.ToString();
                }
                else
                {
                    MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                    System.Environment.Exit(0);
                }
            }
        }

        public static double MapBackgroundPicGISCenterY
        {
            get
            {
                if (_mapBackgroundPicGISCenterY == null)
                {
                    if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                    {
                        _mapBackgroundPicGISCenterY = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapBackgroundPicGISCenterY"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                        System.Environment.Exit(0);
                    }
                }
                return Convert.ToDouble(_mapBackgroundPicGISCenterY);
            }
            set
            {
                if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                {
                    DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MapBackgroundPicGISCenterY"] = value;
                    _mapBackgroundPicGISCenterY = value.ToString();
                }
                else
                {
                    MessageBox.Show("数据库中缺少地图信息，无法启动程序。请确保完整的地图信息在数据库中存在。");
                    System.Environment.Exit(0);
                }
            }
        }

        #endregion

        #region 属性_服务器参数

        public static string CurrentlyServer
        {
            get
            {
                if (_currentlyServer == null)
                {
                    if (File.Exists(ConfigFile))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(ConfigFile);
                        XmlNode node = xmlDoc.SelectSingleNode("/config/CurrentlyServer");
                        if (node != null)
                        {
                            _currentlyServer = node.InnerText;
                        }
                        else
                        {
                            _currentlyServer = "默认服务器";
                            AddNodeNoAttrib(ConfigFile, "/config/CurrentlyServer", _currentlyServer);
                        }
                    }
                    else
                    {
                        MessageBox.Show("配置文件：" + ConfigFile + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                        System.Environment.Exit(0);
                    }
                }
                return _currentlyServer;
            }
            set
            {
                _currentlyServer = value;
                //设置了新的服务器方案，则更新全部。
                _serverName = null;
                _serverIP = null;
                _serverPort = null;
                _serverDBStr = null;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/CurrentlyServer");
                if (node != null)
                {
                    node.InnerText = _currentlyServer;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, "/config/CurrentlyServer", _currentlyServer);
                }
            }
        }

        public static string ServerName
        {
            get
            {
                if (_serverName == null)
                {
                    if (File.Exists(ConfigFile))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(ConfigFile);
                        XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + Global.CurrentlyServer + "']/ServerName");
                        if (node != null)
                        {
                            _serverName = node.InnerText;
                        }
                        else
                        {
                            _serverName = "默认服务器";
                            AddNodeNoAttrib(ConfigFile, "/config/ServerList/Server/ServerName", _serverName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("配置文件：" + ConfigFile + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                        System.Environment.Exit(0);
                    }
                }
                return _serverName;
            }
            set
            {
                _serverName = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + Global.CurrentlyServer + "']/ServerName");
                if (node != null)
                {
                    node.InnerText = _serverName;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, "/config/ServerList/Server/ServerName", _serverName);
                }
            }
        }

        public static string ServerIP
        {
            get
            {
                if (_serverIP == null)
                {
                    if (File.Exists(ConfigFile))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(ConfigFile);
                        XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + Global.CurrentlyServer + "']/ServerIP");
                        if (node != null)
                        {
                            _serverIP = node.InnerText;
                        }
                        else
                        {
                            _serverIP = "127.0.0.1";
                            AddNodeNoAttrib(ConfigFile, "/config/ServerList/Server/ServerIP", _serverIP);
                        }
                    }
                    else
                    {
                        MessageBox.Show("配置文件：" + ConfigFile + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                        System.Environment.Exit(0);
                    }
                }
                return _serverIP;
            }
            set
            {
                _serverIP = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + Global.CurrentlyServer + "']/ServerIP");
                if (node != null)
                {
                    node.InnerText = _serverIP;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, "/config/ServerList/Server/ServerIP", _serverIP);
                }
            }
        }

        public static int ServerPort
        {
            get
            {
                if (_serverPort == null)
                {
                    if (File.Exists(ConfigFile))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(ConfigFile);
                        XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + Global.CurrentlyServer + "']/ServerPort");
                        if (node != null)
                        {
                            _serverPort = node.InnerText;
                        }
                        else
                        {
                            _serverPort = "7898";
                            AddNodeNoAttrib(ConfigFile, "/config/ServerList/Server/ServerPort", _serverPort);
                        }
                    }
                    else
                    {
                        MessageBox.Show("配置文件：" + ConfigFile + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                        System.Environment.Exit(0);
                    }
                }
                return Convert.ToInt32(_serverPort);
            }
            set
            {
                _serverPort = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + Global.CurrentlyServer + "']/ServerPort");
                if (node != null)
                {
                    node.InnerText = _serverPort;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, "/config/ServerList/Server/ServerPort", _serverPort);
                }
            }
        }

        public static string ServerDBStr
        {
            get
            {
                if (_serverDBStr == null)
                {
                    if (File.Exists(ConfigFile))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(ConfigFile);
                        XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + Global.CurrentlyServer + "']/ServerDBStr");
                        if (node != null)
                        {
                            _serverDBStr = node.InnerText;
                        }
                        else
                        {
                            _serverDBStr = @"server=127.0.0.1;Database=PersonPosition;User ID=sa;Password=123456;";
                            AddNodeNoAttrib(ConfigFile, "/config/ServerList/Server/ServerDBStr", _serverDBStr);
                        }
                    }
                    else
                    {
                        MessageBox.Show("配置文件：" + ConfigFile + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                        System.Environment.Exit(0);
                    }
                }
                return _serverDBStr;
            }
            set
            {
                _serverDBStr = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + Global.CurrentlyServer + "']/ServerDBStr");
                if (node != null)
                {
                    node.InnerText = _serverDBStr;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, "/config/ServerList/Server/ServerDBStr", _serverDBStr);
                }
            }
        }

        #endregion

        #region 属性_颜色参数

        public static string CurrentlyColor
        {
            get
            {
                if (_currentlyColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/CurrentlyColor");
                    if (node != null)
                    {
                        _currentlyColor = node.InnerText;
                    }
                    else
                    {
                        _currentlyColor = "Color1";
                        AddNodeNoAttrib(ConfigFile, "/config/CurrentlyColor", _currentlyColor);
                    }
                }
                return _currentlyColor;
            }
            set
            {
                _currentlyColor = value;
                //设置了新的颜色方案，则更新全部颜色。
                _mapTitleColor = null;
                _mapCommentColor = null;
                _mapLabelColor = null;
                _stationNameColor = null;
                _stationPeopleColor = null;
                _personNameColor = null;
                _personDistanceColor = null;
                _distancePointColor = null;
                _distanceLineColor = null;
                _distanceTextColor = null;
                
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/CurrentlyColor");
                if (node != null)
                {
                    node.InnerText = _currentlyColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, "/config/CurrentlyColor", _currentlyColor);
                }
            }
        }

        public static Color MapTitleColor
        {
            get
            {
                if (_mapTitleColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/MapTitleColor");
                    if (node != null)
                    {
                        _mapTitleColor = node.InnerText;
                    }
                    else
                    {
                        _mapTitleColor = "255,0,192,0";
                        AddNodeNoAttrib(ConfigFile, "/config/ColorList/" + Global.CurrentlyColor + "/MapTitleColor", _mapTitleColor);
                    }
                }
                string[] ARGB = _mapTitleColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 0, 192, 0);
                }
                return resultColor;
            }
            set
            {
                _mapTitleColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/MapTitleColor");
                if (node != null)
                {
                    node.InnerText = _mapTitleColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/MapTitleColor", _mapTitleColor);
                }
            }
        }

        public static Color MapCommentColor
        {
            get
            {
                if (_mapCommentColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/MapCommentColor");
                    if (node != null)
                    {
                        _mapCommentColor = node.InnerText;
                    }
                    else
                    {
                        _mapCommentColor = "255,0,0,0";
                        AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/MapCommentColor", _mapCommentColor);
                    }
                }
                string[] ARGB = _mapCommentColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 0, 0, 0);
                }
                return resultColor;
            }
            set
            {
                _mapCommentColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/MapCommentColor");
                if (node != null)
                {
                    node.InnerText = _mapCommentColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/MapCommentColor", _mapCommentColor);
                }
            }
        }

        public static Color MapLabelColor
        {
            get
            {
                if (_mapLabelColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/MapLabelColor");
                    if (node != null)
                    {
                        _mapLabelColor = node.InnerText;
                    }
                    else
                    {
                        _mapLabelColor = "255,0,0,0";
                        AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/MapLabelColor", _mapLabelColor);
                    }
                }
                string[] ARGB = _mapLabelColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 0, 0, 0);
                }
                return resultColor;
            }
            set
            {
                _mapLabelColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/MapLabelColor");
                if (node != null)
                {
                    node.InnerText = _mapLabelColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/MapLabelColor", _mapLabelColor);
                }
            }
        }

        public static Color StationNameColor
        {
            get
            {
                if (_stationNameColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/StationNameColor");
                    if (node != null)
                    {
                        _stationNameColor = node.InnerText;
                    }
                    else
                    {
                        _stationNameColor = "255,0,128,0";
                        AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/StationNameColor", _stationNameColor);
                    }
                }
                string[] ARGB = _stationNameColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 0, 128, 0);
                }
                return resultColor;
            }
            set
            {
                _stationNameColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/StationNameColor");
                if (node != null)
                {
                    node.InnerText = _stationNameColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/StationNameColor", _stationNameColor);
                }
            }
        }

        public static Color StationPeopleColor
        {
            get
            {
                if (_stationPeopleColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/StationPeopleColor");
                    if (node != null)
                    {
                        _stationPeopleColor = node.InnerText;
                    }
                    else
                    {
                        _stationPeopleColor = "255,0,0,255";
                        AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/StationPeopleColor", _stationPeopleColor);
                    }
                }
                string[] ARGB = _stationPeopleColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 0, 0, 255);
                }
                return resultColor;
            }
            set
            {
                _stationPeopleColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/StationPeopleColor");
                if (node != null)
                {
                    node.InnerText = _stationPeopleColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/StationPeopleColor", _stationPeopleColor);
                }
            }
        }

        public static Color PersonNameColor
        {
            get
            {
                if (_personNameColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/PersonNameColor");
                    if (node != null)
                    {
                        _personNameColor = node.InnerText;
                    }
                    else
                    {
                        _personNameColor = "255,0,0,255";
                        AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/PersonNameColor", _personNameColor);
                    }
                }
                string[] ARGB = _personNameColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 0, 0, 255);
                }
                return resultColor;
            }
            set
            {
                _personNameColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/PersonNameColor");
                if (node != null)
                {
                    node.InnerText = _personNameColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/PersonNameColor", _personNameColor);
                }
            }
        }

        public static Color PersonDistanceColor
        {
            get
            {
                if (_personDistanceColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/PersonDistanceColor");
                    if (node != null)
                    {
                        _personDistanceColor = node.InnerText;
                    }
                    else
                    {
                        _personDistanceColor = "255,103,0,206";
                        AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/PersonDistanceColor", _personDistanceColor);
                    }
                }
                string[] ARGB = _personDistanceColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 103, 0, 206);
                }
                return resultColor;
            }
            set
            {
                _personDistanceColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/PersonDistanceColor");
                if (node != null)
                {
                    node.InnerText = _personDistanceColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/PersonDistanceColor", _personDistanceColor);
                }
            }
        }

        public static Color DistancePointColor
        {
            get
            {
                if (_distancePointColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/DistancePointColor");
                    if (node != null)
                    {
                        _distancePointColor = node.InnerText;
                    }
                    else
                    {
                        _distancePointColor = "255,0,255,0";
                        AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/DistancePointColor", _distancePointColor);
                    }
                }
                string[] ARGB = _distancePointColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 0, 255, 0);
                }
                return resultColor;
            }
            set
            {
                _distancePointColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/DistancePointColor");
                if (node != null)
                {
                    node.InnerText = _distancePointColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/DistancePointColor", _distancePointColor);
                }
            }
        }

        public static Color DistanceLineColor
        {
            get
            {
                if (_distanceLineColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/DistanceLineColor");
                    if (node != null)
                    {
                        _distanceLineColor = node.InnerText;
                    }
                    else
                    {
                        _distanceLineColor = "255,0,0,255";
                        AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/DistanceLineColor", _distanceLineColor);
                    }
                }
                string[] ARGB = _distanceLineColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 0, 0, 255);
                }
                return resultColor;
            }
            set
            {
                _distanceLineColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/DistanceLineColor");
                if (node != null)
                {
                    node.InnerText = _distanceLineColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/DistanceLineColor", _distanceLineColor);
                }
            }
        }

        public static Color DistanceTextColor
        {
            get
            {
                if (_distanceTextColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/DistanceTextColor");
                    if (node != null)
                    {
                        _distanceTextColor = node.InnerText;
                    }
                    else
                    {
                        _distanceTextColor = "255,123,123,123";
                        AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/DistanceTextColor", _distanceTextColor);
                    }
                }
                string[] ARGB = _distanceTextColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 123, 123, 123);
                }
                return resultColor;
            }
            set
            {
                _distanceTextColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ColorList/" + Global.CurrentlyColor + "/DistanceTextColor");
                if (node != null)
                {
                    node.InnerText = _distanceTextColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ColorList/" + Global.CurrentlyColor + "/DistanceTextColor", _distanceTextColor);
                }
            }
        }

        #endregion

        #region LED参数

        public static string LEDBasicTitle
        {
            get
            {
                if (_lEDBasicTitle == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDBasicTitle");
                    if (node != null)
                    {
                        _lEDBasicTitle = node.InnerText;
                    }
                    else
                    {
                        _lEDBasicTitle = Global.MapName;
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDBasicTitle", _lEDBasicTitle);
                    }
                }
                return _lEDBasicTitle;
            }
            set
            {
                _lEDBasicTitle = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDBasicTitle");
                if (node != null)
                {
                    node.InnerText = _lEDBasicTitle;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDBasicTitle", _lEDBasicTitle);
                }
            }
        }

        public static int LEDTextLoopTime
        {
            get
            {
                if (_lEDtextLoopTime == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDTextLoopTime");
                    if (node != null)
                    {
                        _lEDtextLoopTime = node.InnerText;
                    }
                    else
                    {
                        _lEDtextLoopTime = "2";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDTextLoopTime", _lEDtextLoopTime);
                    }
                }
                return Convert.ToInt32(_lEDtextLoopTime);
            }
            set
            {
                _lEDtextLoopTime = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDTextLoopTime");
                if (node != null)
                {
                    node.InnerText = _lEDtextLoopTime;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDTextLoopTime", _lEDtextLoopTime);
                }
            }
        }

        public static bool LEDIsAdvShow
        {
            get
            {
                if (_lEDIsAdvShow == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDIsAdvShow");
                    if (node != null)
                    {
                        _lEDIsAdvShow = node.InnerText;
                    }
                    else
                    {
                        _lEDIsAdvShow = "False";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDIsAdvShow", _lEDIsAdvShow);
                    }
                }
                return Convert.ToBoolean(_lEDIsAdvShow);
            }
            set
            {
                _lEDIsAdvShow = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDIsAdvShow");
                if (node != null)
                {
                    node.InnerText = _lEDIsAdvShow;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDIsAdvShow", _lEDIsAdvShow);
                }
            }
        }

        public static int LEDWidth
        {
            get
            {
                if (_lEDWidth == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDWidth");
                    if (node != null)
                    {
                        _lEDWidth = node.InnerText;
                    }
                    else
                    {
                        _lEDWidth = "160";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDWidth", _lEDWidth);
                    }
                }
                return Convert.ToInt32(_lEDWidth);
            }
            set
            {
                _lEDWidth = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDWidth");
                if (node != null)
                {
                    node.InnerText = _lEDWidth;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDWidth", _lEDWidth);
                }
            }
        }
        public static int LEDHeight
        {
            get
            {
                if (_lEDHeight == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDHeight");
                    if (node != null)
                    {
                        _lEDHeight = node.InnerText;
                    }
                    else
                    {
                        _lEDHeight = "96";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDHeight", _lEDHeight);
                    }
                }
                return Convert.ToInt32(_lEDHeight);
            }
            set
            {
                _lEDHeight = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDHeight");
                if (node != null)
                {
                    node.InnerText = _lEDHeight;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDHeight", _lEDHeight);
                }
            }
        }

        public static int LEDLeft
        {
            get
            {
                if (_lEDLeft == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDLeft");
                    if (node != null)
                    {
                        _lEDLeft = node.InnerText;
                    }
                    else
                    {
                        _lEDLeft = "418";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDLeft", _lEDLeft);
                    }
                }
                return Convert.ToInt32(_lEDLeft);
            }
            set
            {
                _lEDLeft = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDLeft");
                if (node != null)
                {
                    node.InnerText = _lEDLeft;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDLeft", _lEDLeft);
                }
            }
        }

        public static int LEDTop
        {
            get
            {
                if (_lEDTop == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDTop");
                    if (node != null)
                    {
                        _lEDTop = node.InnerText;
                    }
                    else
                    {
                        _lEDTop = "286";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDTop", _lEDTop);
                    }
                }
                return Convert.ToInt32(_lEDTop);
            }
            set
            {
                _lEDTop = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDTop");
                if (node != null)
                {
                    node.InnerText = _lEDTop;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDTop", _lEDTop);
                }
            }
        }

        public static bool LEDTopMost
        {
            get
            {
                if (_lEDTopMost == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDTopMost");
                    if (node != null)
                    {
                        _lEDTopMost = node.InnerText;
                    }
                    else
                    {
                        _lEDTopMost = "False";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDTopMost", _lEDTopMost);
                    }
                }
                return Convert.ToBoolean(_lEDTopMost);
            }
            set
            {
                _lEDTopMost = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDTopMost");
                if (node != null)
                {
                    node.InnerText = _lEDTopMost;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDTopMost", _lEDTopMost);
                }
            }
        }

        public static bool LEDIsAreaInMineNum
        {
            get
            {
                if (_lEDIsAreaInMineNum == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDIsAreaInMineNum");
                    if (node != null)
                    {
                        _lEDIsAreaInMineNum = node.InnerText;
                    }
                    else
                    {
                        _lEDIsAreaInMineNum = "False";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDIsAreaInMineNum", _lEDIsAreaInMineNum);
                    }
                }
                return Convert.ToBoolean(_lEDIsAreaInMineNum);
            }
            set
            {
                _lEDIsAreaInMineNum = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDIsAreaInMineNum");
                if (node != null)
                {
                    node.InnerText = _lEDIsAreaInMineNum;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDIsAreaInMineNum", _lEDIsAreaInMineNum);
                }
            }
        }

        public static string LEDAdvText
        {
            get
            {
                if (_lEDAdvText == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDAdvText");
                    if (node != null)
                    {
                        _lEDAdvText = node.InnerText;
                    }
                    else
                    {
                        _lEDAdvText = "";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvText", _lEDAdvText);
                    }
                }
                return _lEDAdvText;
            }
            set
            {
                _lEDAdvText = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDAdvText");
                if (node != null)
                {
                    node.InnerText = _lEDAdvText;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvText", _lEDAdvText);
                }
            }
        }

        public static string LEDHengLineKeyStr
        {
            get
            {
                if (_lEDHengLineKeyStr == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDHengLineKeyStr");
                    if (node != null)
                    {
                        _lEDHengLineKeyStr = node.InnerText;
                    }
                    else
                    {
                        _lEDHengLineKeyStr = "";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDHengLineKeyStr", _lEDHengLineKeyStr);
                    }
                }
                return _lEDHengLineKeyStr;
            }
            set
            {
                _lEDHengLineKeyStr = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDHengLineKeyStr");
                if (node != null)
                {
                    node.InnerText = _lEDHengLineKeyStr;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDHengLineKeyStr", _lEDHengLineKeyStr);
                }
            }
        }

        public static string LEDShuLineKeyStr
        {
            get
            {
                if (_lEDShuLineKeyStr == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDShuLineKeyStr");
                    if (node != null)
                    {
                        _lEDShuLineKeyStr = node.InnerText;
                    }
                    else
                    {
                        _lEDShuLineKeyStr = "";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDShuLineKeyStr", _lEDShuLineKeyStr);
                    }
                }
                return _lEDShuLineKeyStr;
            }
            set
            {
                _lEDShuLineKeyStr = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDShuLineKeyStr");
                if (node != null)
                {
                    node.InnerText = _lEDShuLineKeyStr;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDShuLineKeyStr", _lEDShuLineKeyStr);
                }
            }
        }

        public static Font LEDBasicFont
        {
            get
            {
                if (_lEDBasicFontFamily == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDBasicFontFamily");
                    if (node != null)
                    {
                        _lEDBasicFontFamily = node.InnerText;
                    }
                    else
                    {
                        _lEDBasicFontFamily = "宋体";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDBasicFontFamily", _lEDBasicFontFamily);
                    }
                }
                if (_lEDBasicFontSize == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDBasicFontSize");
                    if (node != null)
                    {
                        _lEDBasicFontSize = node.InnerText;
                    }
                    else
                    {
                        _lEDBasicFontSize = "9";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDBasicFontSize", _lEDBasicFontSize);
                    }
                }
                return new Font(_lEDBasicFontFamily, Convert.ToSingle(_lEDBasicFontSize));
            }
            set
            {
                _lEDBasicFontFamily = value.FontFamily.Name;
                _lEDBasicFontSize = value.Size.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDBasicFontFamily");
                if (node != null)
                {
                    node.InnerText = _lEDBasicFontFamily;
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDBasicFontFamily", _lEDBasicFontFamily);
                }
                XmlNode node_1 = xmlDoc.SelectSingleNode("/config/LED/LEDBasicFontSize");
                if (node_1 != null)
                {
                    node_1.InnerText = _lEDBasicFontSize;
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDBasicFontSize", _lEDBasicFontSize);
                }
                xmlDoc.Save(ConfigFile);
            }
        }

        public static Font LEDAdvFont
        {
            get
            {
                if (_lEDAdvFontFamily == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDAdvFontFamily");
                    if (node != null)
                    {
                        _lEDAdvFontFamily = node.InnerText;
                    }
                    else
                    {
                        _lEDAdvFontFamily = "宋体";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvFontFamily", _lEDAdvFontFamily);
                    }
                }
                if (_lEDAdvFontSize == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDAdvFontSize");
                    if (node != null)
                    {
                        _lEDAdvFontSize = node.InnerText;
                    }
                    else
                    {
                        _lEDAdvFontSize = "9";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvFontSize", _lEDAdvFontSize);
                    }
                }
                return new Font(_lEDAdvFontFamily, Convert.ToSingle(_lEDAdvFontSize));
            }
            set
            {
                _lEDAdvFontFamily = value.FontFamily.Name;
                _lEDAdvFontSize = value.Size.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDAdvFontFamily");
                if (node != null)
                {
                    node.InnerText = _lEDAdvFontFamily;
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvFontFamily", _lEDAdvFontFamily);
                }
                XmlNode node_1 = xmlDoc.SelectSingleNode("/config/LED/LEDAdvFontSize");
                if (node_1 != null)
                {
                    node_1.InnerText = _lEDAdvFontSize;
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvFontSize", _lEDAdvFontSize);
                }
                xmlDoc.Save(ConfigFile);
            }
        }

        public static Color LEDBasicTextColor
        {
            get
            {
                if (_lEDBasicTextColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDBasicTextColor");
                    if (node != null)
                    {
                        _lEDBasicTextColor = node.InnerText;
                    }
                    else
                    {
                        _lEDBasicTextColor = "255,255,0,0";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDBasicTextColor", _lEDBasicTextColor);
                    }
                }
                string[] ARGB = _lEDBasicTextColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 255, 0, 0);
                }
                return resultColor;
            }
            set
            {
                _lEDBasicTextColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDBasicTextColor");
                if (node != null)
                {
                    node.InnerText = _lEDBasicTextColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDBasicTextColor", _lEDBasicTextColor);
                }
            }
        }
        public static Color LEDAdvTextColor
        {
            get
            {
                if (_lEDAdvTextColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDAdvTextColor");
                    if (node != null)
                    {
                        _lEDAdvTextColor = node.InnerText;
                    }
                    else
                    {
                        _lEDAdvTextColor = "255,255,0,0";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvTextColor", _lEDAdvTextColor);
                    }
                }
                string[] ARGB = _lEDAdvTextColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 255, 0, 0);
                }
                return resultColor;
            }
            set
            {
                _lEDAdvTextColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDAdvTextColor");
                if (node != null)
                {
                    node.InnerText = _lEDAdvTextColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvTextColor", _lEDAdvTextColor);
                }
            }
        }
        public static Color LEDAdvLineColor
        {
            get
            {
                if (_lEDAdvLineColor == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDAdvLineColor");
                    if (node != null)
                    {
                        _lEDAdvLineColor = node.InnerText;
                    }
                    else
                    {
                        _lEDAdvLineColor = "255,255,0,0";
                        AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvLineColor", _lEDAdvLineColor);
                    }
                }
                string[] ARGB = _lEDAdvLineColor.Split(',');
                Color resultColor;
                try
                {
                    resultColor = Color.FromArgb(Convert.ToInt32(ARGB[0]), Convert.ToInt32(ARGB[1]), Convert.ToInt32(ARGB[2]), Convert.ToInt32(ARGB[3]));
                }
                catch
                {
                    MessageBox.Show("配置文件中的颜色信息错误，程序将自动恢复为默认值。","初始化颜色配置方案");
                    resultColor = Color.FromArgb(255, 255, 0, 0);
                }
                return resultColor;
            }
            set
            {
                _lEDAdvLineColor = value.A + "," + value.R + "," + value.G + "," + value.B;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LED/LEDAdvLineColor");
                if (node != null)
                {
                    node.InnerText = _lEDAdvLineColor;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LED/LEDAdvLineColor", _lEDAdvLineColor);
                }
            }
        }

        #endregion

        #region 属性_其他参数

        public static string BasicPointName
        {
            get
            {
                if (_basicPointName == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/BasicPoint/BasicPointName");
                    if (node != null)
                    {
                        _basicPointName = node.InnerText;
                    }
                    else
                    {
                        _basicPointName = "";
                        AddNodeNoAttrib(ConfigFile, @"/config/BasicPoint/BasicPointName", _basicPointName);
                    }
                }
                return _basicPointName;
            }
            set
            {
                _basicPointName = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/BasicPoint/BasicPointName");
                if (node != null)
                {
                    node.InnerText = _basicPointName;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/BasicPoint/BasicPointName", _basicPointName);
                }
            }
        }
        public static string Company
        {
            get
            {
                if (_title == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/Company");
                    if (node != null)
                    {
                        _title = node.InnerText;
                    }
                    else
                    {
                        _title = "";
                        AddNodeNoAttrib(ConfigFile, @"/config/Company", _title);
                    }
                }
                return _title;
            }
            set
            {
                _title = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/Company");
                if (node != null)
                {
                    node.InnerText = _title;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/Company", _title);
                }
            }
        }
        public static string Product
        {
            get
            {
                if (_product == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/Product");
                    if (node != null)
                    {
                        _product = node.InnerText;
                    }
                    else
                    {
                        _product = "";
                        AddNodeNoAttrib(ConfigFile, @"/config/Product", _product);
                    }
                }
                return _product;
            }
            set
            {
                _product = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/Product");
                if (node != null)
                {
                    node.InnerText = _product;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/Product", _product);
                }
            }
        }
        public static string TitleText
        {
            get
            {
                if (_titleText == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/TitleText");
                    if (node != null)
                    {
                        _titleText = node.InnerText;
                    }
                    else
                    {
                        _titleText = "";
                        AddNodeNoAttrib(ConfigFile, @"/config/TitleText", _titleText);
                    }
                }
                return _titleText;
            }
            set
            {
                _titleText = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/TitleText");
                if (node != null)
                {
                    node.InnerText = _titleText;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/TitleText", _titleText);
                }
            }
        }

        public static double BasicPointPositionX
        {
            get
            {
                if (_basicPointPositionX == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/BasicPoint/BasicPointPositionX");
                    if (node != null)
                    {
                        _basicPointPositionX = node.InnerText;
                    }
                    else
                    {
                        _basicPointPositionX = "0";
                        AddNodeNoAttrib(ConfigFile, @"/config/BasicPoint/BasicPointPositionX", _basicPointPositionX);
                    }
                }
                return Convert.ToDouble(_basicPointPositionX);
            }
            set
            {
                _basicPointPositionX = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/BasicPoint/BasicPointPositionX");
                if (node != null)
                {
                    node.InnerText = _basicPointPositionX;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/BasicPoint/BasicPointPositionX", _basicPointPositionX);
                }
            }
        }

        public static double BasicPointPositionY
        {
            get
            {
                if (_basicPointPositionY == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/BasicPoint/BasicPointPositionY");
                    if (node != null)
                    {
                        _basicPointPositionY = node.InnerText;
                    }
                    else
                    {
                        _basicPointPositionY = "0";
                        AddNodeNoAttrib(ConfigFile, @"/config/BasicPoint/BasicPointPositionY", _basicPointPositionY);
                    }
                }
                return Convert.ToDouble(_basicPointPositionY);
            }
            set
            {
                _basicPointPositionY = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/BasicPoint/BasicPointPositionY");
                if (node != null)
                {
                    node.InnerText = _basicPointPositionY;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/BasicPoint/BasicPointPositionY", _basicPointPositionY);
                }
            }
        }
        public static int ClientPant
        {
            get
            {
                if (_ClientPant == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ClientPant");
                    if (node != null)
                    {
                        _ClientPant = node.InnerText;
                    }
                    else
                    {
                        _ClientPant = "2000";
                        AddNodeNoAttrib(ConfigFile, @"/config/ClientPant", _ClientPant.ToString());
                    }
                }
                return Convert.ToInt32(_ClientPant);
            }
            set
            {
                _ClientPant = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ClientPant");
                if (node != null)
                {
                    node.InnerText = _ClientPant;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ClientPant", _ClientPant);
                }
            }
        }

        public static int ClientCheckConnTimer
        {
            get
            {
                if (_ClientCheckConnTimer == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/ClientCheckConnTimer");
                    if (node != null)
                    {
                        _ClientCheckConnTimer = node.InnerText;
                    }
                    else
                    {
                        _ClientCheckConnTimer = "6000";
                        AddNodeNoAttrib(ConfigFile, @"/config/ClientCheckConnTimer", _ClientCheckConnTimer.ToString());
                    }
                }
                return Convert.ToInt32(_ClientCheckConnTimer);
            }
            set
            {
                _ClientCheckConnTimer = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ClientCheckConnTimer");
                if (node != null)
                {
                    node.InnerText = _ClientCheckConnTimer;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/ClientCheckConnTimer", _ClientCheckConnTimer);
                }
            }
        }
        public static bool TouMingLock
        {
            get
            {
                if (_touMingLock == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/TouMingLock");
                    if (node != null)
                    {
                        _touMingLock = node.InnerText;
                    }
                    else
                    {
                        _touMingLock = "True";
                        AddNodeNoAttrib(ConfigFile, @"/config/TouMingLock", _touMingLock);
                    }
                }
                return Convert.ToBoolean(_touMingLock);
            }
            set
            {
                _touMingLock = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/TouMingLock");
                if (node != null)
                {
                    node.InnerText = _touMingLock;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/TouMingLock", _touMingLock);
                }
            }
        }

        public static string LockPassword
        {
            get
            {
                if (_lockPassword == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/LockPassword");
                    if (node != null)
                    {
                        _lockPassword = node.InnerText;
                    }
                    else
                    {
                        _lockPassword = "123456";
                        AddNodeNoAttrib(ConfigFile, @"/config/LockPassword", _lockPassword);
                    }
                }
                return _lockPassword;
            }
            set
            {
                _lockPassword = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/LockPassword");
                if (node != null)
                {
                    node.InnerText = _lockPassword;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/LockPassword", _lockPassword);
                }
            }
        }

        public static string AutoLoginUser
        {
            get
            {
                if (_autoLoginUser == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/AutoLoginUser");
                    if (node != null)
                    {
                        _autoLoginUser = node.InnerText;
                    }
                    else
                    {
                        _autoLoginUser = "NULL";
                        AddNodeNoAttrib(ConfigFile, @"/config/AutoLoginUser", _autoLoginUser);
                    }
                }
                return _autoLoginUser;
            }
            set
            {
                _autoLoginUser = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/AutoLoginUser");
                if (node != null)
                {
                    node.InnerText = _autoLoginUser;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/AutoLoginUser", _autoLoginUser);
                }
            }
        }

        public static bool AutoRunLED
        {
            get
            {
                if (_autoRunLED == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/AutoRunLED");
                    if (node != null)
                    {
                        _autoRunLED = node.InnerText;
                    }
                    else
                    {
                        _autoRunLED = "True";
                        AddNodeNoAttrib(ConfigFile, @"/config/AutoRunLED", _autoRunLED);
                    }
                }
                return Convert.ToBoolean(_autoRunLED);
            }
            set
            {
                _autoRunLED = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/AutoRunLED");
                if (node != null)
                {
                    node.InnerText = _autoRunLED;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/AutoRunLED", _autoRunLED);
                }
            }
        }

        public static bool AutoLogin
        {
            get
            {
                if (_autoLogin == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/AutoLogin");
                    if (node != null)
                    {
                        _autoLogin = node.InnerText;
                    }
                    else
                    {
                        _autoLogin = "False";
                        AddNodeNoAttrib(ConfigFile, @"/config/AutoLogin", _autoLogin);
                    }
                }
                return Convert.ToBoolean(_autoLogin);
            }
            set
            {
                _autoLogin = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/AutoLogin");
                if (node != null)
                {
                    node.InnerText = _autoLogin;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/AutoLogin", _autoLogin);
                }
            }
        }

        public static bool AutoStart
        {
            get
            {
                if (_autoStart == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/AutoStart");
                    if (node != null)
                    {
                        _autoStart = node.InnerText;
                    }
                    else
                    {
                        _autoStart = "False";
                        AddNodeNoAttrib(ConfigFile, @"/config/AutoStart", _autoStart);
                    }
                }
                return Convert.ToBoolean(_autoStart);
            }
            set
            {
                _autoStart = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/AutoStart");
                if (node != null)
                {
                    node.InnerText = _autoStart;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/AutoStart", _autoStart);
                }
            }
        }

        public static int TimeToolTip
        {
            get
            {
                if (_timeToolTip == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/TimeToolTip");
                    if (node != null)
                    {
                        _timeToolTip = node.InnerText;
                    }
                    else
                    {
                        _timeToolTip = "5";
                        AddNodeNoAttrib(ConfigFile, @"/config/TimeToolTip", _timeToolTip);
                    }
                }
                return Convert.ToInt32(_timeToolTip);
            }
            set
            {
                _timeToolTip = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/TimeToolTip");
                if (node != null)
                {
                    node.InnerText = _timeToolTip;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/TimeToolTip", _timeToolTip);
                }
            }
        }

        public static bool IsShowBug
        {
            get
            {
                if (_isShowBug == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/IsShowBug");
                    if (node != null)
                    {
                        _isShowBug = node.InnerText;
                    }
                    else
                    {
                        _isShowBug = "False";
                        AddNodeNoAttrib(ConfigFile, @"/config/IsShowBug", _isShowBug);
                    }
                }
                return Convert.ToBoolean(_isShowBug);
            }
            set
            {
                _isShowBug = value.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/IsShowBug");
                if (node != null)
                {
                    node.InnerText = _isShowBug;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/IsShowBug", _isShowBug);
                }
            }
        }

        #endregion

        #region 音频参数

        public static string AudioArea
        {
            get
            {
                if (_audioArea == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/Audio/AudioArea");
                    if (node != null)
                    {
                        _audioArea = node.InnerText;
                    }
                    else
                    {
                        _audioArea = "Alarm1";
                        AddNodeNoAttrib(ConfigFile, @"/config/Audio/AudioArea", _audioArea);
                    }
                }
                return _audioArea;
            }
            set
            {
                _audioArea = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/Audio/AudioArea");
                if (node != null)
                {
                    node.InnerText = _audioArea;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/Audio/AudioArea", _audioArea);
                }
            }
        }

        public static string AudioPersonSend
        {
            get
            {
                if (_audioPersonSend == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/Audio/AudioPersonSend");
                    if (node != null)
                    {
                        _audioPersonSend = node.InnerText;
                    }
                    else
                    {
                        _audioPersonSend = "Alarm1";
                        AddNodeNoAttrib(ConfigFile, @"/config/Audio/AudioPersonSend", _audioPersonSend);
                    }
                }
                return _audioPersonSend;
            }
            set
            {
                _audioPersonSend = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/Audio/AudioPersonSend");
                if (node != null)
                {
                    node.InnerText = _audioPersonSend;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/Audio/AudioPersonSend", _audioPersonSend);
                }
            }
        }

        public static string AudioErrorStation
        {
            get
            {
                if (_audioErrorStation == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/Audio/AudioErrorStation");
                    if (node != null)
                    {
                        _audioErrorStation = node.InnerText;
                    }
                    else
                    {
                        _audioErrorStation = "Alarm1";
                        AddNodeNoAttrib(ConfigFile, @"/config/Audio/AudioErrorStation", _audioErrorStation);
                    }
                }
                return _audioErrorStation;
            }
            set
            {
                _audioErrorStation = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/Audio/AudioErrorStation");
                if (node != null)
                {
                    node.InnerText = _audioErrorStation;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/Audio/AudioErrorStation", _audioErrorStation);
                }
            }
        }

        public static string AudioLowPower
        {
            get
            {
                if (_audioLowPower == null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigFile);
                    XmlNode node = xmlDoc.SelectSingleNode("/config/Audio/AudioLowPower");
                    if (node != null)
                    {
                        _audioLowPower = node.InnerText;
                    }
                    else
                    {
                        _audioLowPower = "Alarm1";
                        AddNodeNoAttrib(ConfigFile, @"/config/Audio/AudioLowPower", _audioLowPower);
                    }
                }
                return _audioLowPower;
            }
            set
            {
                _audioLowPower = value;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/Audio/AudioLowPower");
                if (node != null)
                {
                    node.InnerText = _audioLowPower;
                    xmlDoc.Save(ConfigFile);
                }
                else
                {
                    AddNodeNoAttrib(ConfigFile, @"/config/Audio/AudioLowPower", _audioLowPower);
                }
            }
        }

        #endregion


        /// <summary>
        /// 递归添加指定的节点
        /// 注：节点无属性，非叶子节点本身没有值
        /// </summary>
        /// <param name="_ConfigFileName"></param>
        /// <param name="_FatherNodePath"></param>
        /// <param name="_NodeName"></param>
        /// <param name="_NodeValue"></param>
        private static void AddNodeNoAttrib(string _ConfigFileName,string _NodeName,string _NodeValue)
        {
            if (File.Exists(_ConfigFileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(_ConfigFileName);
                string[] tempList = _NodeName.Split('/');
                string _NewNodeName = tempList[tempList.Length - 1];

                string FatherPath = "";
                for (int i = 0; i < tempList.Length - 1; i++)
                {
                    if (tempList[i] != "")
                    {
                        FatherPath += "/" + tempList[i];
                    }
                }
                XmlNode FatherNode = xmlDoc.SelectSingleNode(FatherPath);
                if (FatherNode == null)
                {
                    //递归添加节点
                    AddNodeNoAttrib(_ConfigFileName, FatherPath, "");
                    xmlDoc.Load(_ConfigFileName);
                    FatherNode = xmlDoc.SelectSingleNode(FatherPath);
                }
                XmlElement NewElement = xmlDoc.CreateElement(_NewNodeName);
                NewElement.InnerText = _NodeValue;
                FatherNode.AppendChild(NewElement);
                xmlDoc.Save(_ConfigFileName);
            }
            else
            {
                MessageBox.Show("配置文件：" + _ConfigFileName + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                System.Environment.Exit(0);
            }
        }

        public static string[] GetServerNameList()
        {
            if (File.Exists(ConfigFile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNodeList nodeList = xmlDoc.SelectNodes("/config/ServerList/Server/ServerName");
                string[] ServerList = new string[nodeList.Count];
                for (int i = 0; i < nodeList.Count; i++)
                {
                    ServerList[i] = nodeList[i].InnerText;
                }
                return ServerList;
            }
            else
            {
                MessageBox.Show("配置文件：" + ConfigFile + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                System.Environment.Exit(0);
                return null;
            }
        }

        public static string GetServerInfo(string serverName,string wantGetInfo)
        {
            if (File.Exists(ConfigFile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + serverName + "']/" + wantGetInfo);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                MessageBox.Show("配置文件：" + ConfigFile + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                System.Environment.Exit(0);
                return "";
            }
        }

        public static bool CreateServerInfo(string _serverName, string _serverIP, string _serverPort,string _serverDBstr)
        {
            if (File.Exists(ConfigFile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList");
                if (node != null)
                {
                    XmlElement node_Server = xmlDoc.CreateElement("Server");
                    XmlElement node_ServerName = xmlDoc.CreateElement("ServerName");
                    node_ServerName.InnerText = _serverName;
                    XmlElement node_ServerIP = xmlDoc.CreateElement("ServerIP");
                    node_ServerIP.InnerText = _serverIP;
                    XmlElement node_ServerPort = xmlDoc.CreateElement("ServerPort");
                    node_ServerPort.InnerText = _serverPort;
                    XmlElement node_ServerDBStr = xmlDoc.CreateElement("ServerDBStr");
                    node_ServerDBStr.InnerText = _serverDBstr;

                    node_Server.AppendChild(node_ServerName);
                    node_Server.AppendChild(node_ServerIP);
                    node_Server.AppendChild(node_ServerPort);
                    node_Server.AppendChild(node_ServerDBStr);
                    node.AppendChild(node_Server);
                    xmlDoc.Save(ConfigFile);
                    return true;
                }
                return false;
            }
            else
            {
                MessageBox.Show("配置文件：" + ConfigFile + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                System.Environment.Exit(0);
                return false;
            }
        }

        public static bool DelServer(string _serverName)
        {
            if (File.Exists(ConfigFile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                XmlNode CurrentlyNode = xmlDoc.SelectSingleNode("/config/CurrentlyServer");
                if (CurrentlyNode.InnerText == _serverName)
                {
                    CurrentlyNode.InnerText = "";
                    Global.CurrentlyServer = "";
                }
                XmlNode node = xmlDoc.SelectSingleNode("/config/ServerList/Server[ServerName='" + _serverName + "']");
                if (node != null)
                {
                    xmlDoc.SelectSingleNode("/config/ServerList").RemoveChild(node);
                    xmlDoc.Save(ConfigFile);
                    return true;
                }
                xmlDoc.Save(ConfigFile);
                return false;
            }
            else
            {
                MessageBox.Show("配置文件：" + ConfigFile + "缺失，无法启动程序。请确保完整的配置文件或者重新安装程序");
                System.Environment.Exit(0);
                return false;
            }
        }
    }
}