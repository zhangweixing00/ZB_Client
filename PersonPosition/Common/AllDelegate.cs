using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using PersonPosition.Model;

namespace PersonPosition.Common
{
    /// <summary>
    /// 服务器定位信息更新的委托
    /// </summary>
    public delegate void UpdatePositionEventHandler(bool IsServiceing, string PositionStr, string InMineListStr, string[] ErrorStationList, string AlarmAreaName, int InArea, bool IsExceedInArea, bool IsAlarmMaxPerson, bool IsAlarmMaxHour, bool IsHW_OverNum,string[] JustNowIn, string[] JustNowOut);
    /// <summary>
    /// 服务器采集器通道信息更新的委托
    /// </summary>
    public delegate void UpdateCollectChannelValueEventHandler(int StationID,int Channel_Num,double ChannelValue,int Channel_ID,DateTime LastUpdate_Time);
    /// <summary>
    /// 进入特殊区域人员信息的委托
    /// </summary>
    public delegate void InAreaEventHandler(int InAreaNum, Dictionary<int,int> InAreaList);
    /// <summary>
    /// 缺电的委托
    /// </summary>
    public delegate void LowPowerHandler(int CardID, DateTime Time);
    /// <summary>
    /// 上行短信的委托
    /// </summary>
    public delegate void UpMessageEventHandler(int CardID,string MessageType,DateTime Time);
    /// <summary>
    /// 服务器端返回的下行短信类型的委托
    /// </summary>
    public delegate void DownMesTypeEventHandler(string[] DownMesTypeList);
    /// <summary>
    /// 数据库更新的委托
    /// </summary>
    public delegate void UpdateDBEventHandler(string TableName1, string TableName2, string TableName3, string TableName4, string TableName5, string TableName6, string TableName7, string TableName8, string TableName9);
}
