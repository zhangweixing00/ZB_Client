using System;
using System.Collections.Generic;
using System.Text;

namespace PersonPosition.Model
{
    /// <summary>
    /// 用户信息中不能包含以下识别标记
    /// 信息类型识别标记：前导符为一个："<"，后导符为一个：">"
    /// 点击阅读识别标记："◇已阅此未读信息◇"
    /// 卡号识别标记：前导符为左括弧："("，后导符为右括弧：")"
    /// 时间识别标记：前导符为一个空格：" "，后导符为两个空格："  "
    /// </summary>
    public abstract class ServerMessage
    {
        public const string MESTYPE_LP = "缺电报警";
        public const string MESTYPE_PS = "人员短信";
        public const string MESTYPE_IO = "考勤信息";
        public string MesTypeKey;
        public string TextKey;
        public string SendTimeKey;
        public string UnReadKey;
    }

    public class LowPowerMessage : ServerMessage
    {
        public LowPowerMessage(int cardID, DateTime sendTime)
        {
            base.MesTypeKey = "<" + MESTYPE_LP + ">";
            base.TextKey = "卡片(" + cardID + ")缺电报警！";
            base.SendTimeKey = " " + sendTime.ToString() + "  ";
            base.UnReadKey = "◇已阅此未读信息◇";
        }
    }

    public class PersonSendMessage : ServerMessage
    {
        public PersonSendMessage(int cardID, string Name,string Department, string messageType, DateTime sendTime)
        {
            base.MesTypeKey = "<" + MESTYPE_PS + ">";
            base.TextKey = Name + "(" + cardID + ")-" + Department + ":" + messageType;
            base.SendTimeKey = " " + sendTime.ToString() + "  ";
            base.UnReadKey = "◇已阅此未读信息◇";
        }
    }

    public class InOutMineMessage : ServerMessage
    {
        public InOutMineMessage(string strText, DateTime sendTime)
        {
            base.MesTypeKey = "<" + MESTYPE_IO + ">";
            base.TextKey = strText;
            base.SendTimeKey = " " + sendTime.ToString() + "  ";
            base.UnReadKey = "";
        }
    }
}
