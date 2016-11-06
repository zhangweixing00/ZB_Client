using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

using PersonPosition.Model;
using PersonPosition.StaticService;
using PersonPosition.Common;

using SharpMap.Styles;
using SharpMap.Layers;
using SharpMap.Data;
using SharpMap.Data.Providers;


namespace PersonPosition.View
{
    public partial class MainForm : Form
    {
        private System.Media.SoundPlayer WAVPlayer;//WAV播放器
        private Point mouse_offset;
        private Point mousePos;
        private bool isSelectingAreaComBox = false;//用户是否正在选择特殊区域方案的Combox
        //人员实时信息显示
        private bool isSystemCheck = true;//是否是系统触发的选择事件
        private List<string> alwaysShowInfoList;//人员列表
        //测量距离
        private bool isStartMoveDistane = false;//开关变量
        private ArrayList arrayDistancePoint;//点数组
        //定基点
        private Point ShowMenuPoint;
        //功能窗体类
        public FrmAlarm frmAlarm;
        public FrmDuty frmDuty;
        public FrmHistory frmHistory;
        public FrmMachine frmMachine;
        public FrmPerson frmPerson;
        public FrmOther frmOther;
        public FrmCollect frmCollect;
        public FrmLED frmLED;
        //当前显示的包含功能Panel的功能窗体
        private Form PresentFunctionForm;
        //锁定地图开关变量，防止刷新时重绘
        private bool IsLockMap = false;
        //全屏幕显示地图时保存的窗体状态和分隔器大小
        private FormWindowState winState;
        private int split1Distance;
        private int split3Distance;
        private int split4Distance;
        //验证服务器连接的心跳次数
        private int MissPositionTimes = Global.DisconnectTimes-1;
        //不重绘的地图图层名
        private string MapLayerName;
        //是否加载了背景图片
        private bool IsLoadMapBackgroundPic = false;
        //下行短信类型数组
        private string[] DownMesTypeList;
        //鼠标是否在listView里的开关量。控制当鼠标在里面时不刷新
        private bool IsInListView = false;
        public static Mutex mutex = new Mutex();
        public int in_mine_activeline = 0; 

        #region 提供的通用服务

        public void ApendTextToRichTextBox(ServerMessage message)
        {
            ApendTextToRichTextBox(message.MesTypeKey, Color.Blue, message.TextKey, Color.Black, message.SendTimeKey, Color.Gray, message.UnReadKey, Color.Red);
        }

        /// <summary>
        /// 将信息格式化后输出在richTextBox上
        /// 空字符串为""
        /// 最后一个str4为特殊字串。要么为空，要么为"◇已阅此未读信息◇" 如果有，则显示为未读信息
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str1Color"></param>
        /// <param name="str2"></param>
        /// <param name="str2Color"></param>
        /// <param name="str3"></param>
        /// <param name="str3Color"></param>
        /// <param name="str4">要么为空，要么为"◇已阅此未读信息◇"</param>
        /// <param name="str4Color"></param>
        public void ApendTextToRichTextBox(string str1, Color str1Color, string str2, Color str2Color, string str3, Color str3Color, string str4, Color str4Color)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            if (str4 != "")
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            }
            else
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            }
            if (str1 != "")
            {
                richTextBox1.SelectionColor = str1Color;
                richTextBox1.AppendText(str1);
            }
            if (str2 != "")
            {
                richTextBox1.SelectionColor = str2Color;
                richTextBox1.AppendText(str2);
            }
            if (str3 != "")
            {
                richTextBox1.SelectionColor = str3Color;
                richTextBox1.AppendText(str3);
            }
            if (str4 != "")
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold ^ FontStyle.Underline);
                richTextBox1.SelectionColor = str4Color;
                richTextBox1.AppendText(str4);
            }
            richTextBox1.AppendText("\n");
        }

        public void ShowStationToolTipInMap(string Name, int StationID, string StationType, string StationFunction, double Geo_X, double Geo_Y)
        {
            ShowToolTipInMap(Name, "编号:" + StationID.ToString(), "类型:" + StationType, "功能:" + StationFunction, "", "", Geo_X, Geo_Y);
        }

        public void ShowPersonToolTipInMap(string Name, int CardID, string CardType, double Geo_X, double Geo_Y)
        {
            ShowToolTipInMap(Name, "卡号:" + CardID.ToString(), "卡片类型:" + CardType, "", "","", Geo_X, Geo_Y);
        }

        private void ShowToolTipInMap(string Title, string Line1, string Line2, string Line3, string Line4, string Line5, double Geo_X, double Geo_Y)
        {
            //地图中点定位到目标处
            this.mapImage.Map.Center.X = Geo_X;
            this.mapImage.Map.Center.Y = Geo_Y;
            this.mapImage.Refresh();
            MainToolTip.ToolTipTitle = Title;
            if (Line2 != "")
            {
                Line2 = "\n" + Line2;
            }
            if (Line3 != "")
            {
                Line3 = "\n" + Line3;
            }
            if (Line4 != "")
            {
                Line4 = "\n" + Line4;
            }
            if (Line5 != "")
            {
                Line5 = "\n" + Line5;
            }
            //两次调用是为了避免气泡提示的箭头指向错误
            MainToolTip.Show(Line1 + Line2 + Line3 + Line4 + Line5, this.mapImage, new Point(this.mapImage.Width / 2, this.mapImage.Height / 2), Global.TimeToolTip * 1000);
            MainToolTip.Show(Line1 + Line2 + Line3 + Line4 + Line5, this.mapImage, new Point(this.mapImage.Width / 2, this.mapImage.Height / 2), Global.TimeToolTip * 1000);
        }

        #endregion

        #region 无意义的主窗体的鼠标移动事件、最小、最大、和关闭按钮的鼠标移动事件

        private void splitContainer1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouse_offset = e.Location;
            }
        }

        private void splitContainer1_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mousePos = Control.MousePosition;
                mousePos.Offset(-mouse_offset.X, -mouse_offset.Y);
                this.Location = mousePos;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouse_offset = e.Location;
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mousePos = Control.MousePosition;
                mousePos.Offset(-mouse_offset.X - label1.Left, -mouse_offset.Y - label1.Top);
                this.Location = mousePos;
            }
        }

        private void label_ProductVerson_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouse_offset = e.Location;
            }
        }

        private void label_ProductVerson_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mousePos = Control.MousePosition;
                mousePos.Offset(-mouse_offset.X - label_ProductVerson.Left, -mouse_offset.Y - label_ProductVerson.Top);
                this.Location = mousePos;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouse_offset = e.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mousePos = Control.MousePosition;
                mousePos.Offset(-mouse_offset.X - pictureBox1.Left, -mouse_offset.Y - pictureBox1.Top);
                this.Location = mousePos;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要退出程序吗？", "退出程序", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_Max_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void splitContainer1_Panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void label_ProductVerson_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        #endregion

        #region 主工具栏按钮(包括警灯)事件(公共事件)

        public void MainBtn_Watch_Click(object sender, EventArgs e)
        {
            //没有被选中时才触发
            if (!MainBtn_Watch.Checked)
            {
                foreach (ToolStripButton btn in toolStripMain.Items)
                {
                    btn.Checked = false;
                }
                MainBtn_Watch.Checked = true;

                this.splitContainer3.Visible = true;
                //卸载之前的功能Panel
                if (this.splitContainer1.Panel2.Controls.Count > 2)
                {
                    this.splitContainer1.Panel2.Controls.RemoveAt(2);
                }
                //卸载当前的功能窗体
                if (PresentFunctionForm != null)
                {
                    PresentFunctionForm.Dispose();
                    PresentFunctionForm = null;
                }
                GC.Collect();
            }
        }

        public void MainBtn_Alarm_Click(object sender, EventArgs e)
        {
            //没有被选中时才触发
            if (!MainBtn_Alarm.Checked)
            {
                foreach (ToolStripButton btn in toolStripMain.Items)
                {
                    btn.Checked = false;
                }
                MainBtn_Alarm.Checked = true;

                ChangeFormView(frmAlarm = new FrmAlarm(this));
            }
        }

        public void MainBtn_Duty_Click(object sender, EventArgs e)
        {
            //显示按钮才能继续，否则抛出异常
            if (this.MainBtn_Duty.Visible)
            {
                //没有被选中时才触发
                if (!MainBtn_Duty.Checked)
                {
                    foreach (ToolStripButton btn in toolStripMain.Items)
                    {
                        btn.Checked = false;
                    }
                    MainBtn_Duty.Checked = true;

                    ChangeFormView(frmDuty = new FrmDuty(this));
                }
            }
            else
            {
                throw new Exception("对不起，您的操作权限限制使用这个功能。\n若需要变更，请联系系统管理员。");
            }
        }

        public void MainBtn_History_Click(object sender, EventArgs e)
        {
            //显示按钮才能继续，否则抛出异常
            if (this.MainBtn_History.Visible)
            {
                //没有被选中时才触发
                if (!MainBtn_History.Checked)
                {
                    foreach (ToolStripButton btn in toolStripMain.Items)
                    {
                        btn.Checked = false;
                    }
                    MainBtn_History.Checked = true;

                    ChangeFormView(frmHistory = new FrmHistory());
                }
            }
            else
            {
                throw new Exception("对不起，您的操作权限限制使用这个功能。\n若需要变更，请联系系统管理员。");
            }
        }

        public void MainBtn_AlarmArea_Click(object sender, EventArgs e)
        {
            //显示按钮才能继续，否则抛出异常
            if (this.MainBtn_AlarmArea.Visible)
            {
                //没有被选中时才触发
                if (!MainBtn_AlarmArea.Checked)
                {
                    foreach (ToolStripButton btn in toolStripMain.Items)
                    {
                        btn.Checked = false;
                    }
                    MainBtn_AlarmArea.Checked = true;

                    ChangeFormView(new FrmAlarmArea(this));
                }
            }
            else
            {
                throw new Exception("对不起，您的操作权限限制使用这个功能。\n若需要变更，请联系系统管理员。");
            }
        }

        public void MainBtn_Machine_Click(object sender, EventArgs e)
        {
            //显示按钮才能继续，否则抛出异常
            if (this.MainBtn_Machine.Visible)
            {
                //没有被选中时才触发
                if (!MainBtn_Machine.Checked)
                {
                    foreach (ToolStripButton btn in toolStripMain.Items)
                    {
                        btn.Checked = false;
                    }
                    MainBtn_Machine.Checked = true;

                    ChangeFormView(frmMachine = new FrmMachine(this));
                }
            }
            else
            {
                throw new Exception("对不起，您的操作权限限制使用这个功能。\n若需要变更，请联系系统管理员。");
            }
        }

        public void MainBtn_Person_Click(object sender, EventArgs e)
        {
            //显示按钮才能继续，否则抛出异常
            if (this.MainBtn_Person.Visible)
            {
                //没有被选中时才触发
                if (!MainBtn_Person.Checked)
                {
                    foreach (ToolStripButton btn in toolStripMain.Items)
                    {
                        btn.Checked = false;
                    }
                    MainBtn_Person.Checked = true;

                    ChangeFormView(frmPerson = new FrmPerson(this));
                }
            }
            else
            {
                throw new Exception("对不起，您的操作权限限制使用这个功能。\n若需要变更，请联系系统管理员。");
            }
        }

        public void MainBtn_Other_Click(object sender, EventArgs e)
        {
            //显示按钮才能继续，否则抛出异常
            if (this.MainBtn_Other.Visible)
            {
                //没有被选中时才触发
                if (!MainBtn_Other.Checked)
                {
                    foreach (ToolStripButton btn in toolStripMain.Items)
                    {
                        btn.Checked = false;
                    }
                    MainBtn_Other.Checked = true;

                    ChangeFormView(frmOther = new FrmOther(this));
                }
            }
            else
            {
                throw new Exception("对不起，您的操作权限限制使用这个功能。\n若需要变更，请联系系统管理员。");
            }
        }

        public void MainBtn_System_Click(object sender, EventArgs e)
        {
            //显示按钮才能继续，否则抛出异常
            if (this.MainBtn_System.Visible)
            {
                //没有被选中时才触发
                if (!MainBtn_System.Checked)
                {
                    foreach (ToolStripButton btn in toolStripMain.Items)
                    {
                        btn.Checked = false;
                    }
                    MainBtn_System.Checked = true;

                    ChangeFormView(new FrmSystem());
                }
            }
            else
            {
                throw new Exception("对不起，您的操作权限限制使用这个功能。\n若需要变更，请联系系统管理员。");
            }
        }

        public void MainBtn_Collect_Click(object sender, EventArgs e)
        {
            //显示按钮才能继续，否则抛出异常
            if (this.MainBtn_Collect.Visible)
            {
                //没有被选中时才触发
                if (!MainBtn_Collect.Checked)
                {
                    foreach (ToolStripButton btn in toolStripMain.Items)
                    {
                        btn.Checked = false;
                    }
                    MainBtn_Collect.Checked = true;

                    ChangeFormView(frmCollect = new FrmCollect(this));
                }
            }
            else
            {
                throw new Exception("对不起，您的操作权限限制使用这个功能。\n若需要变更，请联系系统管理员。");
            }
        }

        private void Mainbtn_LockScreen_Click(object sender, EventArgs e)
        {
            FrmLockScreen frm = new FrmLockScreen();
            frm.Show();
        }

        private void Mainbtn_ReLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" 您确定要重新登录吗？     ", "重新登录", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Global.AutoLogin = false;
                Global.AutoLoginUser = "NULL";
                this.Close();
                Application.Restart();
            }
        }

        private void btn_LightPeopleSend_Click(object sender, EventArgs e)
        {
            MainBtn_Alarm_Click(null, null);
        }

        private void btn_LightErrorMachine_Click(object sender, EventArgs e)
        {
            MainBtn_Alarm_Click(null, null);
            frmAlarm.ShowStationAlarm();
        }

        private void btn_NoCardEnter_Click(object sender, EventArgs e)
        {
            btn_NoCardEnter.Enabled = false;
            MainBtn_Alarm_Click(null, null);
            frmAlarm.ShowNoCardAlarm();
        }

        private void btn_LightAlarmArea_Click(object sender, EventArgs e)
        {
            label_StatusAreaNum_Click(null, null);
        }

        private void btn_LightAlarmCollect_Click(object sender, EventArgs e)
        {
            MainBtn_Collect_Click(null, null);
        }

        private void btn_LightAlarmMax_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 地图操作工具栏控件事件

        private void btn_ZommIn_Click(object sender, EventArgs e)
        {
            this.btn_ZommIn.Checked = true;
            this.btn_ZommOut.Checked = false;
            this.btn_Move.Checked = false;
            this.btn_Distance.Checked = false;
            this.btn_BasicPoint.Checked = false;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.ZoomIn;
        }

        private void btn_ZommOut_Click(object sender, EventArgs e)
        {
            this.btn_ZommIn.Checked = false;
            this.btn_ZommOut.Checked = true;
            this.btn_Move.Checked = false;
            this.btn_Distance.Checked = false;
            this.btn_BasicPoint.Checked = false;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.ZoomOut;
        }

        private void btn_Move_Click(object sender, EventArgs e)
        {
            this.btn_ZommIn.Checked = false;
            this.btn_ZommOut.Checked = false;
            this.btn_Move.Checked = true;
            this.btn_Distance.Checked = false;
            this.btn_BasicPoint.Checked = false;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
        }

        private void btn_Distance_Click(object sender, EventArgs e)
        {
            this.btn_ZommIn.Checked = false;
            this.btn_ZommOut.Checked = false;
            this.btn_Move.Checked = false;
            this.btn_Distance.Checked = true;
            this.btn_BasicPoint.Checked = false;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Distance;
        }

        private void btn_BasicPoint_Click(object sender, EventArgs e)
        {
            this.btn_ZommIn.Checked = false;
            this.btn_ZommOut.Checked = false;
            this.btn_Move.Checked = false;
            this.btn_Distance.Checked = false;
            this.btn_BasicPoint.Checked = true;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.BasicPoint;
        }

        private void btn_Brows_Click(object sender, EventArgs e)
        {
            if (mapImage.Map.Layers.Count > 0)
            {
                try
                {
                    mapImage.Map.ZoomToExtents();
                }
                catch
                { }
                if (mapImage.Map.Zoom == 0.0)
                    mapImage.Map.Zoom = 1.0;
                this.mapImage.Refresh();
                //初始化比例尺条
                trackBar1.Value = Convert.ToInt32(mapImage.Map.Zoom);
            }
        }

        private void btn_MaxBackgroundPic_Click(object sender, EventArgs e)
        {
            if (this.IsLoadMapBackgroundPic)
            {
                this.IsLoadMapBackgroundPic = false;
                btn_MaxBackgroundPic.Text = "切换到背景图片视图"; 
                btn_MaxBackgroundPic.Image = Resource_Service.GetImage("MapBackgroundPic_Pic");
                mapImage.Enabled = true;
                trackBar1.Enabled = true;
                btn_SmallViewKeyBtn.Visible = true;
                pic_SmallView.Visible = true;
                //切换颜色方案
                Global.CurrentlyColor = "Color1";
            }
            else
            {
                if (Global.MapBackgroundPic != "")
                {
                    if (File.Exists(Global.MapPath + Global.MapBackgroundPic))
                    {
                        this.IsLoadMapBackgroundPic = true;
                        btn_MaxBackgroundPic.Text = "切换到GIS地图视图";
                        btn_MaxBackgroundPic.Image = Resource_Service.GetImage("MapBackgroundPic_GIS");
                        mapImage.Enabled = false;
                        trackBar1.Enabled = false;
                        btn_SmallViewKeyBtn.Visible = false;
                        pic_SmallView.Visible = false;
                        //切换颜色方案
                        Global.CurrentlyColor = "Color2";
                        //切换比例尺和中心点
                        try
                        {
                            mapImage.Map.Zoom = Global.MapBackgroundPicGISZoom;
                            //调用改变Zoom事件，以修改ZoomBar
                            mapImage_MapZoomChanged(Global.MapBackgroundPicGISZoom);
                            //设置中心点
                            mapImage.Map.Center.X = Global.MapBackgroundPicGISCenterX;
                            mapImage.Map.Center.Y = Global.MapBackgroundPicGISCenterY;
                        }
                        catch
                        {
                            MessageBox.Show("对不起，背景图视图下的图层位置信息有误。无法正确缩放图层。\n\n请确保参数的正确性", "切换地图背景图片视图", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("对不起，地图背景图片文件:" + Global.MapBackgroundPic + "丢失。无法切换地图背景图片视图。\n\n请确保图片存在于" + Global.MapPath + "目录下", "切换地图背景图片视图", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("对不起，您尚未设置地图背景图片。\n\n请在服务器端的<高级工具>下的<高级参数>中设置完毕后，再进行切换。", "切换地图背景图片视图", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            this.splitContainer6.Panel1.Refresh();
            //初始化颜色
            label14.ForeColor = Global.MapTitleColor;
            label15.ForeColor = Global.MapCommentColor;
            //设置所有标注图层颜色
            for (int i = 0; i < mapImage.Map.Layers.Count; i++)
            {
                //筛选标注(LL)图层
                if (mapImage.Map.Layers[i].LayerName.Substring(0, 2) == "LL")
                {
                    string str = mapImage.Map.Layers[i].LayerName.Substring(2, 3);
                    switch (str)
                    {
                        //基站标注图层
                        case "Sta":
                            ((LabelLayer)mapImage.Map.Layers[i]).Style.ForeColor = Global.StationNameColor;
                            break;
                        //人员标注图层
                        case "Pos":
                            ((LabelLayer)mapImage.Map.Layers[i]).Style.ForeColor = Global.PersonNameColor;
                            break;
                        //地图标注图层
                        default:
                            ((LabelLayer)mapImage.Map.Layers[i]).Style.ForeColor = Global.MapLabelColor;
                            break;
                    }
                }
            }
            mapImage.Refresh();
        }
        private void clienttick_Tick(object sender, EventArgs e)
        {
            Socket_Service.SendMessage_Safe(Socket_Service.Command_C2S_ConnTick, Global.PresentUser, "", "", "", "", "", "", "", "");
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            double MapZoom = (Convert.ToDouble(trackBar1.Value) / Convert.ToDouble(trackBar1.Maximum)) * mapImage.Map.MaximumZoom;
            if (MapZoom > mapImage.Map.MaximumZoom)
            {
                mapImage.Map.Zoom = mapImage.Map.MaximumZoom;
            }
            else if (MapZoom < mapImage.Map.MinimumZoom)
            {
                mapImage.Map.Zoom = mapImage.Map.MinimumZoom;
            }
            else
            {
                mapImage.Map.Zoom = MapZoom;
            }
            mapImage.Refresh();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.label_ZoomRuler.Text = "比例尺 1:" + Math.Round(mapImage.Map.Zoom, 0).ToString();
        }

        private void btn_MaxView_Click(object sender, EventArgs e)
        {
            if (btn_MaxView.ToolTipText == "正常显示")
            {
                btn_MaxView.ToolTipText = "全屏显示";
                btn_MaxView.Image = Resource_Service.GetImage("MaxSize");
                this.WindowState = winState;

                toolStripMain.Visible = true;
                this.splitContainer1.Panel1MinSize = 25;
                this.splitContainer3.Panel1MinSize = 25;
                this.splitContainer4.Panel2MinSize = 25;
                this.splitContainer1.SplitterDistance = split1Distance;
                this.splitContainer3.SplitterDistance = split3Distance;
                this.splitContainer4.SplitterDistance = split4Distance;
            }
            else
            {
                winState = this.WindowState;
                split1Distance = this.splitContainer1.SplitterDistance;
                split3Distance = this.splitContainer3.SplitterDistance;
                split4Distance = this.splitContainer4.SplitterDistance;

                btn_MaxView.ToolTipText = "正常显示";
                btn_MaxView.Image = Resource_Service.GetImage("NormalSize");
                this.WindowState = FormWindowState.Maximized;

                toolStripMain.Visible = false;
                this.splitContainer1.Panel1MinSize = 0;
                this.splitContainer3.Panel1MinSize = 0;
                this.splitContainer4.Panel2MinSize = 0;
                this.splitContainer1.SplitterDistance = 0;
                this.splitContainer3.SplitterDistance = 0;
                this.splitContainer4.SplitterDistance = Screen.PrimaryScreen.WorkingArea.Height + toolStripMap.Height;
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (text_Search.Text.Trim() != "")
            {
                string[] Type_ID;

                using (DialogSearch dialogsearch = new DialogSearch(text_Search.Text.Trim()))
                {
                    //搜素及用户选择结果字符串。
                    //空串代表没有找到
                    //Cancel代表用户取消
                    //Map_代表在地图中查看
                    //Info_代表查看详细信息
                    //P代表人员
                    //S代表基站
                    //如：Map_P:123 代表在地图中查看人员ID为123的人
                    string result = dialogsearch.Tag.ToString();
                    if (result != "")
                    {
                        if (result != "Cancel")
                        {
                            Type_ID = result.Split(':');

                            switch (Type_ID[0])
                            {
                                case "Map_P":
                                    DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + Type_ID[1] + "'");
                                    if (rows_Card.Length > 0)
                                    {
                                        DataRow[] rows_Position = DB_Service.MainDataSet.Tables["PositionTable"].Select("ID = " + rows_Card[0]["CardID"]);
                                        if (rows_Position.Length > 0)
                                        {
                                            ShowPersonToolTipInMap(rows_Position[0]["Name"].ToString(), Convert.ToInt32(rows_Position[0]["ID"]), rows_Position[0]["CardType"].ToString(), Convert.ToDouble(rows_Position[0]["Geo_X"]), Convert.ToDouble(rows_Position[0]["Geo_Y"]));
                                        }
                                        else
                                        {
                                            MessageBox.Show("对不起，您搜索的这个人员此刻没有进入。所以无法为您定位。", "搜索", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("对不起，您搜索的这个人员没有绑定任何一张卡片。请您先在卡片管理中为其绑定定位卡片后继续。", "搜索", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    break;
                                case "Map_S":
                                    DataRow[] rows_Station = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + Type_ID[1]);
                                    if (rows_Station.Length > 0)
                                    {
                                        ShowStationToolTipInMap(rows_Station[0]["Name"].ToString(), Convert.ToInt32(rows_Station[0]["ID"]), rows_Station[0]["StationType"].ToString(), rows_Station[0]["StationFunction"].ToString(), Convert.ToDouble(rows_Station[0]["Geo_X"]), Convert.ToDouble(rows_Station[0]["Geo_Y"]));
                                    }
                                    else
                                    {
                                        MessageBox.Show("对不起，无法找到您搜索的这个基站。", "搜索", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    break;
                                case "Info_P":
                                    try
                                    {
                                        this.MainBtn_Person_Click(null, null);
                                        this.frmPerson.ShowPersonInfoByPID(Type_ID[1]);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    break;
                                case "Info_S":
                                    try
                                    {
                                        this.MainBtn_Machine_Click(null, null);
                                        this.frmMachine.ShowStationInfoByID(Convert.ToInt32(Type_ID[1]));
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有找到与您的查询条件匹配的项目。", "搜索", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void text_Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btn_Search_Click(sender, null);
            }
        }

        private void btn_SmallViewKeyBtn_Click(object sender, EventArgs e)
        {
            if (pic_SmallView.Visible)
            {
                pic_SmallView.Visible = false;
                btn_SmallViewKeyBtn.Image = Resource_Service.GetImage("OpenSmallView");
            }
            else
            {
                pic_SmallView.Visible = true;
                btn_SmallViewKeyBtn.Image = Resource_Service.GetImage("CloseSmallView");
            }
        }

        private void pic_SmallView_MouseDown(object sender, MouseEventArgs e)
        {
            double oldZOOM = this.mapImage.Map.Zoom;
            this.mapImage.Map.ZoomToExtents();
            this.mapImage.Map.Center = this.mapImage.Map.ImageToWorld(new PointF(Convert.ToSingle(Convert.ToDouble(e.X) / pic_SmallView.Width * this.mapImage.Width), Convert.ToSingle(Convert.ToDouble(e.Y) / pic_SmallView.Height * this.mapImage.Height)));
            this.mapImage.Map.Zoom = oldZOOM;
            this.mapImage.Refresh();
        }

        private void pic_SmallView_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                SharpMap.Geometries.BoundingBox bbb = this.mapImage.Map.GetExtents();
                //当前显示区域与整个区域的比例系数
                double key = this.mapImage.Width / (bbb.Width / this.mapImage.Map.PixelWidth);
                float X_Distance = Convert.ToSingle(this.pic_SmallView.Width * key);
                float Y_Distance = Convert.ToSingle(this.pic_SmallView.Height * key);
                double key2 = bbb.Width / pic_SmallView.Width;
                double key3 = bbb.Height / pic_SmallView.Height;
                PointF p1 = new PointF(Convert.ToSingle((this.mapImage.Map.Center.X - bbb.Left) / key2), this.pic_SmallView.Height - Convert.ToSingle((this.mapImage.Map.Center.Y - bbb.Bottom) / key3));
                PointF startPoint = new PointF(p1.X - X_Distance / 2 - 1, p1.Y - Y_Distance / 2 - 1);
                e.Graphics.DrawRectangle(new Pen(Color.DodgerBlue), startPoint.X, startPoint.Y, X_Distance, Y_Distance);
            }
            catch
            {   }
        }

        #endregion

        #region 短信息工具栏控件事件

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void com_SendMesPeople_MouseDown(object sender, MouseEventArgs e)
        {
            //刷新发送人员列表
            try
            {
                com_SendMesPeople.Items.Clear();
                foreach (DataRow row in DB_Service.MainDataSet.Tables["PositionTable"].Rows)
                {
                    com_SendMesPeople.Items.Add(row["Name"].ToString());
                }
            }
            catch
            { }
        }

        private void com_SendMesPeople_TextChanged(object sender, EventArgs e)
        {
            InitSendMesTypeCom(com_SendMesPeople.Text.Trim());
        }

        private void com_SendMesType_DropDown(object sender, EventArgs e)
        {
            InitSendMesTypeCom(com_SendMesPeople.Text.Trim());
        }

        /// <summary>
        /// 根据人员类型初始化短信类型
        /// </summary>
        /// <param name="Name"></param>
        private void InitSendMesTypeCom(string Name)
        {
            com_SendMesType.Text = "";
            com_SendMesType.Items.Clear();
            DataRow[] rows = DB_Service.MainDataSet.Tables["PersonTable"].Select("Name ='" + Name + "'");
            if (rows.Length > 0)
            {
                DataRow[] rowss = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + rows[0]["PID"].ToString() + "'");
                if (rowss.Length > 0)
                {
                    switch (rowss[0]["CardType"].ToString())
                    {
                        case "一般人员":
                            com_SendMesType.Items.Add("回电");
                            com_SendMesType.Items.Add("撤离");
                            com_SendMesType.Items.Add("警示");
                            break;
                        case "高级人员":
                            if (this.DownMesTypeList == null)
                            {
                                com_SendMesType.Items.Add("得到下行短信类型失败。请稍候重试。");
                                Socket_Service.SendMessage(Socket_Service.Command_C2S_RequestDownMesType, "", "", "", "", "", "", "", "","");
                            }
                            else
                            {
                                for (int i = 0; i < this.DownMesTypeList.Length; i++)
                                {
                                    com_SendMesType.Items.Add(DownMesTypeList[i]);
                                }
                            }
                            break;
                        case "特殊人员":
                            com_SendMesType.Items.Add("尚未定义命令");
                            break;
                    }
                }
            }
        }

        private void btn_SendMessage_Click(object sender, EventArgs e)
        {
            if (com_SendMesPeople.Text != "" && com_SendMesType.Text != "")
            {
                DataRow[] rows = DB_Service.MainDataSet.Tables["PersonTable"].Select("Name = '" + com_SendMesPeople.Text + "'");
                if (rows.Length > 0)
                {
                    string PID = rows[0]["PID"].ToString();
                    DataRow[] rowss = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + PID + "'");
                    if (rowss.Length > 0)
                    {
                        int CardID = Convert.ToInt32(rowss[0]["CardID"]);
                        int MessageType = 0;
                        switch (com_SendMesType.Text)
                        {
                            case "回电":
                                MessageType = 100;
                                break;
                            case "撤离":
                                MessageType = 101;
                                break;
                            case "警示":
                                MessageType = 102;
                                break;
                            default:
                                MessageType = com_SendMesType.SelectedIndex;
                                break;
                        }
                        Socket_Service.SendMessage(Socket_Service.Command_C2S_DownMessage, CardID.ToString(), MessageType.ToString(), "", "", "", "", "", "","");
                        ApendTextToRichTextBox("发送至【" + com_SendMesPeople.Text + "】:" + com_SendMesType.Text, Color.Black, " " + DateTime.Now.ToString(), Color.Gray, "", Color.Transparent, "", Color.Transparent);
                    }
                    else
                    {
                        MessageBox.Show("这个人员没有绑定卡片！", "发送短信");
                    }
                }
                else
                {
                    MessageBox.Show("没有这个人员！", "发送短信");
                }
            }
            else
            {
                MessageBox.Show("请选择短信接收人和短信内容后再进行发送。", "发送短信", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region mapImage的事件

        private void mapImage_MapZoomChanged(double zoom)
        {
            this.IsLockMap = true;
            //CX 设置Flag状态
            PublicState.Class1.LOCKFLAG = true;
            int BarZoom = Convert.ToInt32((mapImage.Map.Zoom / mapImage.Map.MaximumZoom) * this.trackBar1.Maximum);
            if (BarZoom < trackBar1.Minimum)
            {
                trackBar1.Value = trackBar1.Minimum;
            }
            else if (BarZoom > trackBar1.Maximum)
            {
                trackBar1.Value = trackBar1.Maximum;
            }
            else
            {
                trackBar1.Value = BarZoom;
            }

			System.Threading.Thread.Sleep(100);

            this.IsLockMap = false;

            pic_SmallView.Refresh();
        }

        private void mapImage_MouseDown(SharpMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            this.IsLockMap = true;
            panel_BasicPoint.Visible = false;
            MainToolTip.Hide(this);
        }

        private void mapImage_MouseUp(SharpMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            this.IsLockMap = true;

            if (ImagePos.Button == MouseButtons.Left)
            {
                //左键
                switch (this.mapImage.ActiveTool)
                {
                    case SharpMap.Forms.MapImage.Tools.Pan:
                        //平移，则显示基站下人员
                        for (int i = 0; i < DB_Service.MainDataSet.Tables["StationTable"].Rows.Count; i++)
                        {
                            DataRow Station = DB_Service.MainDataSet.Tables["StationTable"].Rows[i];
                            PointF fff = mapImage.Map.WorldToImage(new SharpMap.Geometries.Point(Convert.ToDouble(Station["Geo_X"]), Convert.ToDouble(Station["Geo_Y"])));
                            if (ImagePos.X > (fff.X - 8) && ImagePos.X < (fff.X + 8) && ImagePos.Y > (fff.Y - 8) && ImagePos.Y < (fff.Y + 8))
                            {
                                //先判断基站是否故障
                                bool IsError = false;
                                for (int j = 0; j < Global.State_ErrorStationList.Length; j++)
                                {
                                    if (Global.State_ErrorStationList[j] == Station["ID"].ToString())
                                    {
                                        //基站故障
                                        IsError = true;
                                        break;
                                    }
                                }
                                if (IsError)
                                {
                                    //基站故障,则调用单击“故障基站数”的事件
                                    Status_ErrorStation_Click(null, null);
                                }
                                else
                                {
                                    //显示基站下人员
                                    FrmInSomething frmInSomething = new FrmInSomething(Station["ID"].ToString() + "号基站附近人员", this, "NearStationID = " + Station["ID"].ToString());
                                    frmInSomething.Show(this);
                                }
                                break;
                            }
                        }
                        break;
                    //测距
                    case SharpMap.Forms.MapImage.Tools.Distance:
                        if (!isStartMoveDistane)
                        {
                            arrayDistancePoint.Clear();
                        }
                        if (arrayDistancePoint.Count == 0)
                        {
                            arrayDistancePoint.Add(WorldPos);
                        }
                        arrayDistancePoint.Add(WorldPos);

                        isStartMoveDistane = true;
                        break;
                    //定基点
                    case SharpMap.Forms.MapImage.Tools.BasicPoint:
                        panel_BasicPoint.Location = ImagePos.Location;
                        text_BasicPoint.Text = Global.BasicPointName;
                        //这里只是把坐标点放到小框的Tag里，并不往BasicPointPosition里赋值。只有当用户点“确定”才赋值
                        panel_BasicPoint.Tag = WorldPos;
                        panel_BasicPoint.Visible = true;
                        break;
                }
            }
            else if (ImagePos.Button == MouseButtons.Right)
            {
                //右键
                switch (this.mapImage.ActiveTool)
                {
                    //当平移时则弹出右键菜单
                    case SharpMap.Forms.MapImage.Tools.Pan:
                        Menu_Map.Tag = WorldPos;//把地图世界点放到Menu_Map.Tag里
                        Menu_Map.Show(mapImage, ImagePos.Location);
                        ShowMenuPoint = ImagePos.Location;
                        break;
                    //当测距时若正在测距则停止添加新点，若已经停止测距则恢复成平移模式。
                    case SharpMap.Forms.MapImage.Tools.Distance:
                        if (isStartMoveDistane)
                        {
                            isStartMoveDistane = false;
                            arrayDistancePoint.RemoveAt(arrayDistancePoint.Count - 1);
                            this.splitContainer6.Panel1.Refresh();
                        }
                        else
                        {
                            goto default;
                        }
                        break;
                    //其余的时候都恢复成平移
                    default:
                        this.btn_ZommIn.Checked = false;
                        this.btn_ZommOut.Checked = false;
                        this.btn_Move.Checked = true;
                        this.btn_Distance.Checked = false;
                        this.btn_BasicPoint.Checked = false;
                        this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
                        break;
                }
            }

            this.IsLockMap = false;
        }

        private void mapImage_MouseMove(SharpMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            if (this.mapImage.ActiveTool == SharpMap.Forms.MapImage.Tools.Distance && isStartMoveDistane)
            {
                if (arrayDistancePoint.Count > 0)
                {
                    arrayDistancePoint[arrayDistancePoint.Count - 1] = WorldPos;// ImagePos.Location;
                    this.splitContainer6.Panel1.Refresh();
                }
                else
                {
                    isStartMoveDistane = false;
                }
            }
        }

        private void mapImage_MapZooming(double zoom)
        {
            this.IsLockMap = true;
        }

        private void mapImage_MapRefreshed(Image MapImage)
        {
            if (splitContainer6.Panel1.BackgroundImage == null)
            {
                splitContainer6.Panel1.BackgroundImage = MapImage;
            }
            else
            {
                if (MapImage != null)
                {
                    //只有当背景图片有更新时才重绘背景
                    if (!MapImage.Equals(splitContainer6.Panel1.BackgroundImage))
                    {
                        splitContainer6.Panel1.BackgroundImage = MapImage;
                    }
                }
            }
        }

        private void mapImage_SizeChanged(object sender, EventArgs e)
        {
            this.mapImage.Refresh();
        }

        #endregion

        #region 订阅的类的事件

        /// <summary>
        /// 服务器定位信息更新事件的相应方法。
        /// 更新PositionTable
        /// 更新图层和List
        /// _PositionStr格式：...!CardID?StationID?Geo_X?Geo_Y?InNullRSSITime!...(注意：其中InNullRSSITime的格式为：日:时:分)
        /// </summary>
        void Socket_Service_Event_UpdatePosition(bool _IsServicing, string _PositionStr,string _InMineListStr, string[] _ErrorStationList, string _AlarmAreaName, int _InArea, bool _IsExceedInArea, bool _IsAlarmMaxPerson, bool _IsAlarmMaxHour,bool _IsHW_OverNum,string[] _JustNowIn,string[] _JustNowOut)
        {
          
            Object thisLock = new Object();
           
            //CX 如LOCKFLAG为false时，可以继续更新
            if (!PublicState.Class1.LOCKFLAG)
            {

                lock (thisLock)
                {

                    try
                    {

                        mutex.WaitOne();


                        MissPositionTimes = 0;//收到消息，心跳次数恢复0
                        Global.State_IsServicing = _IsServicing;
                        if (_ErrorStationList[0] == "")
                        {
                            Global.State_ErrorStationList = new string[0] { };
                        }
                        else
                        {
                            Global.State_ErrorStationList = _ErrorStationList;
                        }

                        Global.State_AreaAlarmName = _AlarmAreaName;
                        Global.State_InArea = _InArea;
                        Global.State_IsExceedInArea = _IsExceedInArea;
                        Global.State_IsAlarmMaxPerson = _IsAlarmMaxPerson;
                        Global.State_IsAlarmMaxHour = _IsAlarmMaxHour;
                        Global.State_IsUnReadNoCardEnter = _IsHW_OverNum;

                        if (_PositionStr == "")
                        {
                            //如果ResultPositionStr=""，说明没有任何数据。
                            DB_Service.MainDataSet.Tables["PositionTable"].Rows.Clear();
                        }
                        else
                        {
                            //准备开始更新数据
                            DB_Service.MainDataSet.Tables["PositionTable"].BeginLoadData();
                            //有新数据，更新PositionTable
                            DB_Service.MainDataSet.Tables["PositionTable"].Rows.Clear();
                            string[] CardList = _PositionStr.Split('!');
                            for (int i = 0; i < CardList.Length; i++)
                            {



                                string[] TempList = CardList[i].Split('?');
                                if (TempList.Length == 5)
                                {
                                    try
                                    {
                                        int cardID = Convert.ToInt32(TempList[0]);
                                        //在CardTable中对应的卡片
                                        DataRow[] tempRows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + cardID);
                                        //判断有这张卡片
                                        if (tempRows_Card.Length > 0)
                                        {
                                            //判断已经绑定过
                                            if (tempRows_Card[0]["PID"] != DBNull.Value)
                                            {
                                                string pid = tempRows_Card[0]["PID"].ToString();
                                                //在PersonTable中对应的人
                                                DataRow[] tempRows_PersonTable = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + pid + "'");
                                                if (tempRows_PersonTable.Length > 0)
                                                {
                                                    //创建新的一行，将以上这些数据填入
                                                    DataRow newRow = DB_Service.MainDataSet.Tables["PositionTable"].NewRow();
                                                    newRow["ID"] = cardID;
                                                    newRow["Name"] = tempRows_PersonTable[0]["Name"];
                                                    newRow["CardType"] = tempRows_Card[0]["CardType"];
                                                    newRow["WorkType"] = tempRows_PersonTable[0]["WorkType"];
                                                    newRow["Department"] = tempRows_PersonTable[0]["Department"];
                                                    newRow["NearStationID"] = Convert.ToInt32(TempList[1]);
                                                    newRow["Area"] = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + Convert.ToInt32(TempList[1]))[0]["Area"];
                                                    if (TempList[4] != "")
                                                    {
                                                        string[] tempInNullRSSITime = TempList[4].Split(':');
                                                        newRow["InNullRSSITime"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(tempInNullRSSITime[0]), Convert.ToInt32(tempInNullRSSITime[1]), Convert.ToInt32(tempInNullRSSITime[2]), 0);
                                                    }
                                                    newRow["Geo_X"] = Convert.ToDouble(TempList[2]);
                                                    newRow["Geo_Y"] = Convert.ToDouble(TempList[3]);
                                                    //将新行添加到PositionTable
                                                    DB_Service.MainDataSet.Tables["PositionTable"].Rows.Add(newRow);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        if (Global.IsShowBug)
                                            System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面解析服务器定位信息错误");
                                    }
                                }
                            }
                            //结束更新数据
                            DB_Service.MainDataSet.Tables["PositionTable"].EndLoadData();
                        }

                        string[] tempInMineList = _InMineListStr.Split('!');
                        for (int i = 0; i < tempInMineList.Length; i++)
                        {

                            try
                            {

                                string[] tempInMineCardList = tempInMineList[i].Split('?');
                                if (tempInMineCardList.Length == 2)
                                {
                                    int _cardID = Convert.ToInt32(tempInMineCardList[0]);
                                    string[] _tempDate = tempInMineCardList[1].Split('-');
                                    int _stationID = Convert.ToInt32(_tempDate[0]);
                                    DateTime intime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(_tempDate[1]), Convert.ToInt32(_tempDate[2]), Convert.ToInt32(_tempDate[3]), 0);
                                    DataRow[] rows_temp = DB_Service.MainDataSet.Tables["PositionTable"].Select("ID = " + _cardID);
                                    if (rows_temp.Length > 0)
                                    {
                                        //定位信息表中有这个人，则修改进洞时间
                                        rows_temp[0]["InMineTime"] = intime;
                                    }
                                    else
                                    {
                                        //定位信息表中没有这个人，则添加新纪录
                                        //在CardTable中对应的卡片
                                        DataRow[] tempRows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + _cardID);
                                        //判断有这张卡片
                                        if (tempRows_Card.Length > 0)
                                        {
                                            //判断已经绑定过
                                            if (tempRows_Card[0]["PID"] != DBNull.Value)
                                            {
                                                string pid = tempRows_Card[0]["PID"].ToString();
                                                //在PersonTable中对应的人
                                                DataRow[] tempRows_PersonTable = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + pid + "'");
                                                if (tempRows_PersonTable.Length > 0)
                                                {
                                                    //创建新的一行，将以上这些数据填入
                                                    DataRow newRow = DB_Service.MainDataSet.Tables["PositionTable"].NewRow();
                                                    newRow["ID"] = _cardID;
                                                    newRow["Name"] = tempRows_PersonTable[0]["Name"];
                                                    newRow["CardType"] = tempRows_Card[0]["CardType"];
                                                    newRow["WorkType"] = tempRows_PersonTable[0]["WorkType"];
                                                    newRow["Department"] = tempRows_PersonTable[0]["Department"];
                                                    newRow["NearStationID"] = _stationID;
                                                    newRow["InMineTime"] = intime;
                                                    DataRow temp_rowStation = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + _stationID)[0];
                                                    newRow["Area"] = temp_rowStation["Area"];
                                                    newRow["Geo_X"] = Convert.ToDouble(temp_rowStation["Geo_X"]);
                                                    newRow["Geo_Y"] = Convert.ToDouble(temp_rowStation["Geo_Y"]);
                                                    //将新行添加到PositionTable
                                                    DB_Service.MainDataSet.Tables["PositionTable"].Rows.Add(newRow);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            catch (Exception ex)
                            {
                                if (Global.IsShowBug)
                                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面解析服务器定位信息错误");
                            }


                        }

                        //准备开始更新地图文字信息数据
                        DB_Service.MainDataSet.Tables["MapTextTable"].BeginLoadData();
                        DB_Service.MainDataSet.Tables["MapTextTable"].Rows.Clear();
                        //绘制基站附近的人数或采集信息
                        for (int seek = 0; seek < DB_Service.MainDataSet.Tables["StationTable"].Rows.Count; seek++)
                        {
                            try
                            {
                                int stationID = Convert.ToInt32(DB_Service.MainDataSet.Tables["StationTable"].Rows[seek]["ID"]);
                                string stationType = DB_Service.MainDataSet.Tables["StationTable"].Rows[seek]["StationType"].ToString();
                                //只有这个类型的基站在图层集合中有，才绘制
                                if (mapImage.Map.Layers["StationTable" + Global.SplitKey + "StationType" + Global.SplitKey + stationType] != null)
                                {
                                    string FinallyPrintStr = "";
                                    //先判断基站是否故障
                                    for (int i = 0; i < Global.State_ErrorStationList.Length; i++)
                                    {
                                        if (Global.State_ErrorStationList[i] == stationID.ToString())
                                        {
                                            //如果故障基站是采集器，则从CollectChannelValueTable删除他的所有通道值
                                            if (DB_Service.MainDataSet.Tables["StationTable"].Rows[seek]["StationFunction"].ToString() == "信息采集")
                                            {
                                                DataRow[] DelChannellRow = DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Select("StationID = " + stationID);
                                                for (int DelSeek = 0; DelSeek < DelChannellRow.Length; DelSeek++)
                                                {
                                                    DelChannellRow[DelSeek].Delete();
                                                }
                                            }
                                            //初始化显示字串，然后直接跳到显示位置
                                            FinallyPrintStr = "基站故障";
                                            goto Line_Write;
                                        }
                                    }
                                    //至此，基站正常。则根据基站功能初始化字串
                                    if (DB_Service.MainDataSet.Tables["StationTable"].Rows[seek]["StationFunction"].ToString() == "信息采集")
                                    {
                                        DataRow[] rows_CollectFunction = DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Select("StationID = " + stationID);
                                        for (int i = 0; i < rows_CollectFunction.Length; i++)
                                        {
                                            FinallyPrintStr += rows_CollectFunction[i]["ChannelName"].ToString() + ":" + rows_CollectFunction[i]["ChannelValueStr"].ToString() + "\n";
                                        }
                                    }
                                    else
                                    {
                                        int Peonum = DB_Service.MainDataSet.Tables["PositionTable"].Select("NearStationID = " + stationID).Length;
                                        FinallyPrintStr = "共" + Peonum + "人";
                                    }
                                Line_Write://显示位置
                                    if (FinallyPrintStr != "")
                                    {
                                        //创建新的一行，将以上这些数据填入
                                        DataRow newRow = DB_Service.MainDataSet.Tables["MapTextTable"].NewRow();
                                        newRow["ID"] = stationID;
                                        newRow["Name"] = FinallyPrintStr;
                                        newRow["StationType"] = stationType;
                                        newRow["Geo_X"] = Convert.ToDouble(DB_Service.MainDataSet.Tables["StationTable"].Rows[seek]["Geo_X"]);
                                        newRow["Geo_Y"] = Convert.ToDouble(DB_Service.MainDataSet.Tables["StationTable"].Rows[seek]["Geo_Y"]);
                                        //将新行添加到MapTextTable
                                        DB_Service.MainDataSet.Tables["MapTextTable"].Rows.Add(newRow);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (Global.IsShowBug)
                                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面绘制基站附近人数或采集信息错误");
                            }
                        }
                        //结束更新数据
                        DB_Service.MainDataSet.Tables["MapTextTable"].EndLoadData();

                        //如果鼠标不在ListView里，则更新MainListView
                        if (!IsInListView)
                        {
                            RefreshMainListView(false);
                        }

                        if (!IsLockMap)
                        {
                            try
                            {
                                //强行更新人员图层，这里不刷新。统一在下面刷新
                                for (int k = 0; k < this.mapImage.Map.Layers.Count; k++)
                                {
                                    string[] templist = Regex.Split(this.mapImage.Map.Layers[k].LayerName, Global.SplitKey);
                                    if (templist.Length > 0)
                                    {
                                        if (templist[0] == "PositionTable" || templist[0] == "MapTextTable")
                                        {
                                            UpdateLayerDataSource(this.mapImage.Map.Layers[k].LayerName, 0);
                                        }
                                    }
                                }
                            }
                            catch
                            { /*这里的错误不需要捕捉*/ }

                            //根据地图图层的存在，刷新地图
                            if (mapImage.Map.Layers[this.MapLayerName] != null)
                            {
                                mapImage.RefreshWithOutLayer(this.MapLayerName);
                            }
                            else
                            {
                                mapImage.Refresh();
                            }
                        }

                        //显示进出洞信息
                        StringBuilder SB_InMine = new StringBuilder();
                        for (int i = 0; i < _JustNowIn.Length; i++)
                        {
                            if (_JustNowIn[i] != "")
                            {
                                DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + _JustNowIn[i]);
                                if (rows_Card.Length > 0 && rows_Card[0]["PID"] != DBNull.Value)
                                {
                                    DataRow[] rows_Person = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = " + rows_Card[0]["PID"].ToString());
                                    if (rows_Person.Length > 0)
                                    {
                                        SB_InMine.Append(rows_Person[0]["Name"] + "、");
                                    }
                                }
                            }
                        }
                        if (SB_InMine.Length > 0)
                        {
                            SB_InMine = SB_InMine.Remove(SB_InMine.Length - 1, 1).Append("进入。");
                        }

                        StringBuilder SB_OutMine = new StringBuilder();
                        for (int j = 0; j < _JustNowOut.Length; j++)
                        {
                            if (_JustNowOut[j] != "")
                            {
                                DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + _JustNowOut[j]);
                                if (rows_Card.Length > 0 && rows_Card[0]["PID"] != DBNull.Value)
                                {
                                    DataRow[] rows_Person = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = " + rows_Card[0]["PID"].ToString());
                                    if (rows_Person.Length > 0)
                                    {
                                        SB_OutMine.Append(rows_Person[0]["Name"] + "、");
                                    }
                                }
                            }
                        }
                        if (SB_OutMine.Length > 0)
                        {
                            SB_OutMine = SB_OutMine.Remove(SB_OutMine.Length - 1, 1).Append("离开。");
                        }

                        if (SB_InMine.Length > 0 || SB_OutMine.Length > 0)
                            ApendTextToRichTextBox(new InOutMineMessage(SB_InMine.ToString() + SB_OutMine.ToString(), DateTime.Now));


                    }

                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
            }

        void Socket_Service_Event_UpdateCollectChannelValue(int StationID, int Channel_Num, double ChannelValue, int Channel_ID, DateTime LastUpdate_Time)
        {

            Object thisLock = new Object();
            lock (thisLock)
            {



                try
                {
                    mutex.WaitOne();
                    DataRow[] row_station = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID);
                    if (row_station.Length > 0)
                    {
                        DataRow[] row_collectchannel = DB_Service.MainDataSet.Tables["CollectChannelTable"].Select("Channel_ID = " + Channel_ID);
                        if (row_collectchannel.Length > 0)
                        {
                            DataRow ChannelRow;
                            DataRow[] row_ChannelValue = DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Select("StationID = " + StationID + " and ChannelNum = " + Channel_Num);
                            if (row_ChannelValue.Length > 0)
                            {
                                //同一个基站下同一个通道的值已经存在，则修改原来的
                                ChannelRow = row_ChannelValue[0];
                            }
                            else
                            {
                                //同一个基站下同一个通道的值不存在，则插入
                                ChannelRow = DB_Service.MainDataSet.Tables["CollectChannelValueTable"].NewRow();
                                DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Rows.Add(ChannelRow);
                            }
                            ChannelRow["StationID"] = StationID;
                            ChannelRow["StationName"] = row_station[0]["Name"];
                            ChannelRow["Channel_ID"] = Channel_ID;
                            ChannelRow["ChannelNum"] = Channel_Num;
                            ChannelRow["ChannelName"] = row_collectchannel[0]["ChannelName"];
                            ChannelRow["ChannelComment"] = row_collectchannel[0]["ChannelComment"];
                            if (row_collectchannel[0]["ChannelPer_K"] != DBNull.Value)
                            {
                                ChannelValue = ChannelValue * Convert.ToDouble(row_collectchannel[0]["ChannelPer_K"]);
                            }
                            if (row_collectchannel[0]["ChannelPer_C"] != DBNull.Value)
                            {
                                ChannelValue = ChannelValue + Convert.ToDouble(row_collectchannel[0]["ChannelPer_C"]);
                            }
                            if (ChannelValue < 0)
                                ChannelValue = 0;
                            ChannelRow["ChannelValueStr"] = Math.Round(ChannelValue, 1) + " " + row_collectchannel[0]["ChannelUnit"].ToString();
                            if ((row_collectchannel[0]["ChannelValue_Max"] != DBNull.Value && ChannelValue > Convert.ToDouble(row_collectchannel[0]["ChannelValue_Max"])) || (row_collectchannel[0]["ChannelValue_Min"] != DBNull.Value && ChannelValue < Convert.ToDouble(row_collectchannel[0]["ChannelValue_Min"])))
                            {
                                ChannelRow["IsOverValue"] = "是";
                            }
                            else
                            {
                                ChannelRow["IsOverValue"] = "否";
                            }
                            ChannelRow["LastUpdateTime"] = LastUpdate_Time;
                        }
                    }

                    //如果LED存在，则从数据表中解析出采集信息，从传入的字串中解析出位置，将采集信息插入到 FrmLED.DrawAdvText
                    if (frmLED != null && !frmLED.IsDisposed)
                        frmLED.GetCollectInsertAdvText(Global.LEDAdvText);
                }
                catch (Exception ex)
                {
                    if (Global.IsShowBug)
                        System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面解析服务器数据采集卡信息错误");
                }

                finally
                {
                    mutex.ReleaseMutex();
                }

            }
        }

        void Socket_Service_Event_LowPower(int CardID, DateTime Time)
        {
            ApendTextToRichTextBox(new LowPowerMessage(CardID, Time));
            //更新当前缺电报警的总数
            Global.State_UnReadLowPower++;
        }

        void Socket_Service_Event_UpMessage(int CardID, string MessageType, DateTime Time)
        {



            Object  thisLock = new Object();
            lock (thisLock)
            {
            try
            {
                mutex.WaitOne();
                DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + CardID);
                if (rows.Length > 0)
                {
                    if (rows[0]["PID"] != DBNull.Value)
                    {
                        string PID = rows[0]["PID"].ToString();
                        DataRow[] personRow = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + PID + "'");
                        if (personRow.Length > 0)
                        {
                            //将短信息格式化后显示在richTextBox上
                            ApendTextToRichTextBox(new PersonSendMessage(CardID, personRow[0]["Name"].ToString(), personRow[0]["Department"].ToString(), MessageType, Time));
                            //更新当前未读信息的总数
                            Global.State_UnReadPersonMes++;
                        }
                        else
                        {
                            //将短信息格式化后显示在richTextBox上
                            ApendTextToRichTextBox("尚未绑定任何员工的卡片：" + CardID + " 于 " + Time + " 发送了短信：" + MessageType, Color.Red, " 请到 设备管理->卡片管理 中进行设置。", Color.Black, "", Color.Transparent, "", Color.Transparent);
                        }
                    }
                    else
                    {
                        //将短信息格式化后显示在richTextBox上
                        ApendTextToRichTextBox("尚未绑定任何员工的卡片：" + CardID + " 于 " + Time + " 发送了短信：" + MessageType, Color.Red, " 请到 设备管理->卡片管理 中进行设置。", Color.Black, "", Color.Transparent, "", Color.Transparent);
                    }
                }
                else
                {
                    //将短信息格式化后显示在richTextBox上
                    ApendTextToRichTextBox("尚未登记的卡片：" + CardID + " 于 " + Time + " 发送了短信：" + MessageType, Color.Red, " 请到 设备管理->卡片管理 中进行设置。", Color.Black, "", Color.Transparent, "", Color.Transparent);
                }
            }
            catch (Exception ex)
            {
                if (Global.IsShowBug)
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面解析服务器短消息信息错误");
            }
            finally {
                mutex.ReleaseMutex();
            }
        }}

        void Socket_Service_Event_DownMesType(string[] _downmestypeList)
        {
            DownMesTypeList = _downmestypeList;
        }

        void Socket_Service_Event_UpdateDB(string TableName1, string TableName2, string TableName3, string TableName4, string TableName5, string TableName6, string TableName7, string TableName8, string TableName9)
        {

            Object thisLock = new Object();
            lock (thisLock)
            {

                try
                {

                    mutex.WaitOne();

                    RefreshAndOperateTable(TableName1);
                    if (TableName2 != "")
                    {
                        RefreshAndOperateTable(TableName2);
                        if (TableName3 != "")
                        {
                            RefreshAndOperateTable(TableName3);
                            if (TableName4 != "")
                            {
                                RefreshAndOperateTable(TableName4);
                                if (TableName5 != "")
                                {
                                    RefreshAndOperateTable(TableName5);
                                    if (TableName6 != "")
                                    {
                                        RefreshAndOperateTable(TableName6);
                                        if (TableName7 != "")
                                        {
                                            RefreshAndOperateTable(TableName7);
                                            if (TableName8 != "")
                                            {
                                                RefreshAndOperateTable(TableName8);
                                                if (TableName9 != "")
                                                    RefreshAndOperateTable(TableName9);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Global.IsShowBug)
                        System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面解析服务器更新数据库信息错误");
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        #endregion

        #region 状态label的文字改变引发的颜色改变事件

        private void label_StatusErrorStation_TextChanged(object sender, EventArgs e)
        {
            if (label_StatusErrorStation.Text == "0")
            {
                label_StatusErrorStation.ForeColor = Color.LimeGreen;
                label_StatusErrorStation.Font = new System.Drawing.Font("黑体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label_StatusErrorStation.Cursor = Cursors.Default;
            }
            else
            {
                label_StatusErrorStation.ForeColor = Color.Red;
                label_StatusErrorStation.Font = new System.Drawing.Font("黑体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label_StatusErrorStation.Cursor = Cursors.Hand;
            }
        }

        private void label_StatusServer_TextChanged(object sender, EventArgs e)
        {
            switch (label_StatusServer.Text)
            {
                case "未连接":
                    label_StatusServer.ForeColor = Color.Yellow;
                    break;
                case "停止":
                    label_StatusServer.ForeColor = Color.Red;
                    break;
                case "运行":
                    label_StatusServer.ForeColor = Color.LimeGreen;
                    break;
            }
        }

        private void label_StatusUnRead_TextChanged(object sender, EventArgs e)
        {
            if (label_StatusUnRead.Text == "0")
            {
                label_StatusUnRead.ForeColor = Color.LimeGreen;
                label_StatusUnRead.Font = new System.Drawing.Font("黑体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label_StatusUnRead.Cursor = Cursors.Default;
            }
            else
            {
                label_StatusUnRead.ForeColor = Color.Red;
                label_StatusUnRead.Font = new System.Drawing.Font("黑体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label_StatusUnRead.Cursor = Cursors.Hand;
            }
        }

        private void label_StatusUnReadLowPower_TextChanged(object sender, EventArgs e)
        {
            if (label_StatusUnReadLowPower.Text == "0")
            {
                label_StatusUnReadLowPower.ForeColor = Color.LimeGreen;
                label_StatusUnReadLowPower.Font = new System.Drawing.Font("黑体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label_StatusUnReadLowPower.Cursor = Cursors.Default;
            }
            else
            {
                label_StatusUnReadLowPower.ForeColor = Color.Red;
                label_StatusUnReadLowPower.Font = new System.Drawing.Font("黑体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label_StatusUnReadLowPower.Cursor = Cursors.Hand;
            }
        }

        private void label_StatusAreaNum_TextChanged(object sender, EventArgs e)
        {
            if (label_StatusAreaNum.Text == "0")
            {
                label_StatusAreaNum.ForeColor = Color.LimeGreen;
                label_StatusAreaNum.Font = new System.Drawing.Font("黑体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label_StatusAreaNum.Cursor = Cursors.Default;
            }
            else
            {
                if (Global.State_IsExceedInArea)
                {
                    label_StatusAreaNum.ForeColor = Color.Red;
                }
                else
                {
                    label_StatusAreaNum.ForeColor = Color.LimeGreen;
                }
                label_StatusAreaNum.Font = new System.Drawing.Font("黑体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label_StatusAreaNum.Cursor = Cursors.Hand;
            }
        }

        #endregion

        #region 主右键菜单事件

        private void 考勤明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MainBtn_Duty_Click(sender, e);
                this.frmDuty.ShowPersonDutyByCardID(Convert.ToInt32(Menu_Main.Tag));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 历史轨迹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MainBtn_History_Click(sender, e);
                this.frmHistory.ShowHistoryByCardID(Convert.ToInt32(Menu_Main.Tag));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 报警记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainBtn_Alarm_Click(sender, e);
            this.frmAlarm.ShowPersonSendAlarmByCardID(Convert.ToInt32(Menu_Main.Tag));
        }

        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MainBtn_Person_Click(sender, e);
                this.frmPerson.ShowPersonInfoByCardID(Convert.ToInt32(Menu_Main.Tag));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 地图右键菜单事件

        private void 在这里设置基点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //模拟点击设置基点的地图按钮
            btn_BasicPoint_Click(sender, e);
            //模拟鼠标键落下
            mapImage_MouseDown(null, null);
            //模拟鼠标左键抬起
            mapImage_MouseUp((SharpMap.Geometries.Point)Menu_Map.Tag, new MouseEventArgs(MouseButtons.Left, 1, ShowMenuPoint.X, ShowMenuPoint.Y, 0));
        }

        private void 删除基点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要删除基点 " + Global.BasicPointName + " 吗？\n\n删除后，精确距离显示的参照点为每个人员所处信号强度最大的基站坐标点。", "删除基点", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Global.BasicPointName = "";
                toolStripLabel4.Text = "距离显示基点:空 ";
                Global.BasicPointPositionX = 0;
                Global.BasicPointPositionY = 0;
                删除基点ToolStripMenuItem.Enabled = false;
            }
        }

        private void Menu_Map_Opening(object sender, CancelEventArgs e)
        {
            //如果没有基点，则不显示删除基点
            if (Global.BasicPointName != "")
            {
                删除基点ToolStripMenuItem.Enabled = true;
            }
        }

        private void 从这里开始测距ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //模拟点击测距的地图按钮
            btn_Distance_Click(sender, e);
            //模拟鼠标键落下
            mapImage_MouseDown(null, null);
            //模拟鼠标左键抬起
            mapImage_MouseUp((SharpMap.Geometries.Point)Menu_Map.Tag, new MouseEventArgs(MouseButtons.Left, 1, ShowMenuPoint.X, ShowMenuPoint.Y, 0));
        }

        #endregion

        public MainForm()
        {


            /*InitializeComponent();
            alwaysShowInfoList = new List<string>();
            arrayDistancePoint = new ArrayList();
            this.Text = Application.ProductName + (Global.IsTempVersion ? "(演示版)" : "");
            this.label1.Text = Application.ProductName + (Global.IsTempVersion ? "(演示版)" : "");
            Global.CurrentlyColor = "Color1";
            label_ProductVerson.Text = "软件版本：" + Application.ProductVersion;
            label_ProductVerson.Left = this.label1.Left +this.label1.Width+ 20;
            //初始化地图名称
            this.label14.Text = Global.MapName;
            //初始化地图备注
            this.label15.Text = Global.MapComment;
            //初始化不重绘的图层名
            this.MapLayerName = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MainMap"].ToString();
            //将不重绘的图层名传给mapImage
            this.mapImage.MapLayerName = this.MapLayerName;
            //根据登录用户的权限字串初始化功能按钮的可见性
            InitMainBtn();
            //根据全局变量IsUseHongWai初始化红外的显示
            btn_NoCardEnter.Visible = Global.IsUseHongWai;*/

            InitializeComponent();
            alwaysShowInfoList = new List<string>();
            arrayDistancePoint = new ArrayList();
            //this.Text = Application.ProductName + (Global.IsTempVersion ? "(演示版)" : "");
            //this.label1.Text = Application.ProductName + (Global.IsTempVersion ? "(演示版)" : "");
            this.Text = Global.Product + (Global.IsTempVersion ? "(演示版)" : "");
            this.label1.Text = Global.Product + (Global.IsTempVersion ? "(演示版)" : "");
            
            Global.CurrentlyColor = "Color1";
            label_ProductVerson.Text = "软件版本：" + Application.ProductVersion;
            label_TitleText.Text = Global.TitleText;
            label_ProductVerson.Left = this.label1.Left + this.label1.Width + 20;
            //初始化地图名称
            this.label14.Text = Global.MapName;
            //初始化地图备注
            this.label15.Text = Global.MapComment;
            //初始化不重绘的图层名
            this.MapLayerName = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MainMap"].ToString();
            //将不重绘的图层名传给mapImage
            this.mapImage.MapLayerName = this.MapLayerName;
            //根据登录用户的权限字串初始化功能按钮的可见性
            InitMainBtn();
            //根据全局变量IsUseHongWai初始化红外的显示
            btn_NoCardEnter.Visible = Global.IsUseHongWai;



        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //根据LayerTable和LayerSortTable中的基础图层记录，构建TreeView。并添加相应的图层表到MainDataSet（不添加文件数据源的图层表）
            BuildLayerTableAndTreeView();
            //选中并展开树中全部节点
            MainTreeView.Nodes[0].Expand();
            MainTreeView.Nodes[0].Checked = true;
            MainTreeView.SelectedNode = MainTreeView.Nodes[0];
            SetNodeAllChild(MainTreeView.Nodes[0], true);
            //通过TreeView的选择情况，更新mapImage中的图层并刷新
            RefreshLayerVisable();
            //加载地图文字图层
            CommonFun.AddLayer("MapTextTable", this.mapImage, 6, 18);
            //根据特殊区域方案表刷新AreaSubjectComBox
            RefreshAreaSubjectComBox();
            //初始化未读短信息数目
            DataTable tempMessageNumTable = DB_Service.GetTable("TempAlarmTable", "select * from AlarmPersonSendTable where IsReaded = 'False'");
            try
            {
                foreach (DataRow row in tempMessageNumTable.Rows)
                {
                    int CardID = Convert.ToInt32(row["CardID"]);
                    DataRow[] row_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + CardID);
                    if (row_Card.Length > 0)
                    {
                        if (row_Card[0]["PID"] != DBNull.Value)
                        {
                            Global.State_UnReadPersonMes++;
                        }
                    }
                }
            }
            catch
            {   }
            //初始化定时器时间间隔
            timer_UpdateUI.Interval = Global.ClientCheckConnTimer;
            clienttick.Interval = Global.ClientPant;
            //初始化缺电报警数目
            DataTable tempLowPowerTable = DB_Service.GetTable("TempAlarmPowerTable", "select * from AlarmPowerTable where IsReaded = 'False'");
            Global.State_UnReadLowPower = tempLowPowerTable.Rows.Count;
            //初始化mapImage
            try
            {
                this.mapImage.Map.ZoomToExtents();
            }
            catch
            {  }
            if (mapImage.Map.Zoom == 0.0)
                mapImage.Map.Zoom = 1.0;
            this.mapImage.Map.MinimumZoom = Global.MapImageMinView;
            this.mapImage.Map.MaximumZoom = mapImage.Map.Zoom * 2;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
            //初始化比例尺条
            try
            {
                trackBar1.Maximum = Convert.ToInt32(this.mapImage.Map.MaximumZoom);
                trackBar1.Minimum = Convert.ToInt32(this.mapImage.Map.MinimumZoom);
                trackBar1.Value = Convert.ToInt32(mapImage.Map.Zoom);
            }
            catch
            {  }
            //初始化小地图显示
            pic_SmallView.Image = this.mapImage.Map.GetMap();
            btn_SmallViewKeyBtn.Image= Resource_Service.GetImage("CloseSmallView");
            //初始化距离显示基点信息
            if (Global.BasicPointName == "")
            {
                toolStripLabel4.Text = "距离显示基点:空 ";
            }
            else
            {
                toolStripLabel4.Text = "距离显示基点:" + Global.BasicPointName + " ";
            }
            //定位控件
            panel_Zoom.Top = 430;
            splitContainer6.SplitterDistance = 464;
            splitContainer4.SplitterDistance = 490;
            //初始化颜色
            label14.ForeColor = Global.MapTitleColor;
            label15.ForeColor = Global.MapCommentColor;
            //订阅Socket_Service的服务器定位信息更新事件
            Socket_Service.Event_UpdatePosition += new UpdatePositionEventHandler(Socket_Service_Event_UpdatePosition);
            //订阅Socket_Service的采集器通道信息更新事件
            Socket_Service.Event_UpdateCollectChannelValue += new UpdateCollectChannelValueEventHandler(Socket_Service_Event_UpdateCollectChannelValue);
            //订阅Socket_Service的缺电事件
            Socket_Service.Event_LowPower += new LowPowerHandler(Socket_Service_Event_LowPower);
            //订阅Socket_Service的人员发送报警信息表更新事件
            Socket_Service.Event_UpMessage += new UpMessageEventHandler(Socket_Service_Event_UpMessage);
            //订阅Socket_Service的得到下行短信类型事件
            Socket_Service.Event_DownMesType += new DownMesTypeEventHandler(Socket_Service_Event_DownMesType);
            //订阅Socket_Service的数据库更新事件
            Socket_Service.Event_UpdateDB += new UpdateDBEventHandler(Socket_Service_Event_UpdateDB);
            if (Global.AutoRunLED)
            {
                frmLED = new FrmLED();
                frmLED.Show();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WAVPlayer != null)
            {
                WAVPlayer.Stop();
            }
            try
            {
                //给服务器发送反注册命令
                Socket_Service.SendMessage(Socket_Service.Command_C2S_UnReg,Global.PresentUser, "", "", "", "", "", "", "","");
            }
            catch
            { }
            try
            {
                Socket_Service.DisconnectServer();
            }
            catch
            { }
        }

        /// <summary>
        /// 根据指定的表名，刷新指定的表并进行相应操作
        /// </summary>
        /// <param name="TableName"></param>
        private void RefreshAndOperateTable(string TableName)
        {
            switch (TableName)
            {
                case "AlarmMachineTable":
                    break;
                case "AlarmPersonSendTable":
                    //初始化未读短信息数目
                    DataTable tempMessageNumTable = DB_Service.GetTable("TempAlarmTable", "select * from AlarmPersonSendTable where IsReaded = 'False'");
                    Global.State_UnReadPersonMes = 0;
                    try
                    {
                        foreach (DataRow row in tempMessageNumTable.Rows)
                        {
                            int CardID = Convert.ToInt32(row["CardID"]);
                            DataRow[] row_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + CardID);
                            if (row_Card.Length > 0)
                            {
                                if (row_Card[0]["PID"] != DBNull.Value)
                                {
                                    Global.State_UnReadPersonMes++;
                                }
                            }
                        }
                    }
                    catch
                    { }
                    break;
                case "AlarmPowerTable":
                    //初始化缺电报警数目
                    DataTable tempLowPowerTable = DB_Service.GetTable("TempAlarmPowerTable", "select * from AlarmPowerTable where IsReaded = 'False'");
                    Global.State_UnReadLowPower = tempLowPowerTable.Rows.Count;
                    break;
                case "CardTable":
                    break;
                case "ClassTable":
                    break;
                case "CollectChannelTable":
                    break;
                case "DepartmentTable":
                    break;
                case "DutyTable":
                    break;
                case "HistoryCollectTable":
                    break;
                case "HistoryPositionTable":
                    break;
                case "LayerSortTable":
                    break;
                case "LayerTable":
                    break;
                case "MapAreaTable":
                    break;
                case "MapTable":
                    break;
                case "PersonTable":
                    break;
                case "PowerTable":
                    break;
                case "SpecalTable":
                    break;
                case "StationTable":
                    break;
                case "UserTable":
                    break;
                case "WorkTypeTable":
                    break;
                case "WPTable":
                    break;
            }
        }

        /// <summary>
        /// 更新UI的计时器事件
        /// 注：这个线程更新除地图以外的所有UI显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_UpdateUI_Tick(object sender, EventArgs e)
        {
            int aaaa=0,bbb=0;

            if (Global.State_IsServicing && Global.State_IsServerRunning)//服务器正常
            {
                //服务器巡检正常
                //在服务器连接正常下，通过心跳判断是否断开连接
                MissPositionTimes++;
                if (MissPositionTimes > Global.DisconnectTimes)
                {
                    //断开心跳大于规定值，则进入“未连接”状态
                    MissPositionTimes = Global.DisconnectTimes;
                    label_StatusServer.Text = "未连接";
                    MainListView.Clear();
                    alwaysShowInfoList.Clear();
                    listView_InMine.Columns[1].Text = "未知";
                    listView_InMine.Items.Clear();
                    label_StatusErrorStation.Text = "0";
                    if (!isSelectingAreaComBox)
                    {
                        com_AreaSubject.Text = "未启动";
                    }
                    label_StatusAreaNum.Text = "0";
                    //将几个灯都关掉
                    btn_LightErrorMachine.Enabled = false;
                    btn_LightPeopleSend.Enabled = false;
                    btn_NoCardEnter.Enabled = false;
                    //停止客户端心跳和timer_updateui
                    timer_UpdateUI.Enabled = false;
                    clienttick.Enabled = false;
                    Thread thread = new Thread(new ThreadStart(AttempToConnectServer));
                    thread.Start();
                }
                else
                {
                    //断开心跳小于规定值，说明是“运行”状态
                    label_StatusServer.Text = "运行";
                    try
                    {
                        //更新井下人员数
                        //int aaaa;
                        ListViewItem foundItem = listView_InMine.TopItem ;  
                        //ListViewItem =foundItem1 = listView_InMine.bo      
                        if (foundItem != null)
                        {
                            aaaa = foundItem.Index;
                            bbb = aaaa +2;
                            if (bbb > (listView_InMine.Items.Count - 1))
                                bbb = listView_InMine.Items.Count - 1;

                        }
                        listView_InMine.Columns[1].Text = DB_Service.MainDataSet.Tables["PositionTable"].Select("InMineTime is not null").Length.ToString();// Global.InMineList.Count.ToString();//总人数
                        listView_InMine.Items.Clear();
                        for (int i = 0; i < DB_Service.MainDataSet.Tables["MapAreaTable"].Rows.Count; i++)
                        {
                            string AreaName = DB_Service.MainDataSet.Tables["MapAreaTable"].Rows[i]["MapAreaName"].ToString();
                            listView_InMine.Items.Add(new ListViewItem(new string[2] { AreaName, DB_Service.MainDataSet.Tables["PositionTable"].Select("InMineTime is not null and Area = '" + AreaName + "'").Length.ToString() }));//将区域名和人数添加到列表里
                        }

                       
                         //   bbb = listView_InMine.Items.Count;
                        if(foundItem !=null) 
                        {
                            listView_InMine.TopItem = foundItem;
                            listView_InMine.EnsureVisible(bbb );  
                        }
                        //更新故障基站
                        label_StatusErrorStation.Text = Global.State_ErrorStationList.Length.ToString(); ;
                        //更新特殊区域
                        if (Global.State_AreaAlarmName != "未启动")
                        {
                            //更新特殊区域
                            if (!isSelectingAreaComBox)
                            {
                                com_AreaSubject.Text = Global.State_AreaAlarmName;
                            }
                            label_StatusAreaNum.Text = Global.State_InArea.ToString();
                        }
                        else
                        {
                            //更新特殊区域
                            if (!isSelectingAreaComBox)
                            {
                                com_AreaSubject.Text = "未启动";
                            }
                            label_StatusAreaNum.Text = "0";
                        }
                    }
                    catch
                    { /*这里的错误不需要捕捉*/ }
                }
            }
            else if(!Global.State_IsServicing && Global.State_IsServerRunning)//服务器停止服务
            {
                //服务器停止巡检，则清空PositionTable后刷新地图及所有控件
                MainListView.Clear();
                alwaysShowInfoList.Clear();
                label_StatusServer.Text = "停止";
                listView_InMine.Columns[1].Text = "未知";
                listView_InMine.Items.Clear();
                label_StatusErrorStation.Text = "0";
                if (!isSelectingAreaComBox)
                {
                    com_AreaSubject.Text = "未启动";
                }
                label_StatusAreaNum.Text = "0";
            }
            else if (Global.State_IsServicing && !Global.State_IsServerRunning)//服务器异常退出
            {
                MainListView.Clear();
                alwaysShowInfoList.Clear();
                 listView_InMine.Columns[1].Text = "未知";
                listView_InMine.Items.Clear();
                label_StatusErrorStation.Text = "0";
                if (!isSelectingAreaComBox)
                {
                    com_AreaSubject.Text = "未启动";
                }
                label_StatusAreaNum.Text = "0";
                label_StatusServer.Text = "异常退出";
                //服务器退出后，timer_UpdateUI必须停止，否则会导致多个AttempToConnectServer连接服务端出现错误
                timer_UpdateUI.Enabled = false;
                clienttick.Enabled = false;
                //无限循环直到服务端重新启动，必须新开线程，否则客户端界面会没有相应，调用DoEvents()依然很卡
                Thread thread = new Thread(new ThreadStart(AttempToConnectServer));
                thread.Start();
            }
            else if (!Global.State_IsServicing && !Global.State_IsServerRunning)//服务器正常退出
            {
                MainListView.Clear();
                alwaysShowInfoList.Clear();
                 listView_InMine.Columns[1].Text = "未知";
                listView_InMine.Items.Clear();
                label_StatusErrorStation.Text = "0";
                if (!isSelectingAreaComBox)
                {
                    com_AreaSubject.Text = "未启动";
                }
                label_StatusAreaNum.Text = "0";
                label_StatusServer.Text = "正常退出";
                //服务器退出后，timer_UpdateUI必须停止，否则会导致多个AttempToConnectServer连接服务端出现错误
                timer_UpdateUI.Enabled = false;
                clienttick.Enabled = false;
                //无限循环直到服务端重新启动，必须新开线程，否则客户端界面会没有相应，调用DoEvents()依然很卡
                Thread thread = new Thread(new ThreadStart(AttempToConnectServer));
                thread.Start();
            }
            ///////////////////////////////////////////
            //以下这些是跟服务器状态无关的显示
            ///////////////////////////////////////////
            //更新实时时间
            Status_NowTime.Text = DateTime.Now.ToString();
            //更新未读信息
            label_StatusUnRead.Text = Global.State_UnReadPersonMes.ToString();
            //更新未读缺电报警信息
            label_StatusUnReadLowPower.Text = Global.State_UnReadLowPower.ToString();
            //根据未读人员报警数控制警灯
            if (Global.State_UnReadPersonMes != 0)
            {
                btn_LightPeopleSend.Enabled = true;
            }
            else
            {
                btn_LightPeopleSend.Enabled = false;
            }
            //根据故障基站数控制警灯
            if (Global.State_ErrorStationList.Length != 0)
            {
                btn_LightErrorMachine.Enabled = true;
            }
            else
            {
                btn_LightErrorMachine.Enabled = false;
            }
            //根据无卡人员进入控制警灯
            if (Global.State_IsUnReadNoCardEnter)
            {
                btn_NoCardEnter.Enabled = true;
            }
            else
            {
                if (btn_NoCardEnter.Enabled == true)
                {

                }
                else
                {
                    btn_NoCardEnter.Enabled = false;
                }
            }
            //根据特殊区域人数是否超限控制警灯
            if (Global.State_IsExceedInArea)
            {
                btn_LightAlarmArea.Enabled = true;
            }
            else
            {
                btn_LightAlarmArea.Enabled = false;
            }
            //根据采集器标值控制警灯
            if (DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Select("IsOverValue = '是'").Length > 0)
            {
                btn_LightAlarmCollect.Enabled = true;
            }
            else
            {
                btn_LightAlarmCollect.Enabled = false;
            }
            //根据人员超限和超时控制警灯
            if (Global.State_IsAlarmMaxPerson || Global.State_IsAlarmMaxHour)
            {
                btn_LightAlarmMax.Enabled = true;
            }
            else
            {
                btn_LightAlarmMax.Enabled = false;
            }
            //删除CollectChannelValueTable中1分钟内没有更新的
            for (int i = 0; i < DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Rows.Count; i++)
            {
                DateTime lastUpdateTime = Convert.ToDateTime(DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Rows[i]["LastUpdateTime"]);
                TimeSpan ts = DateTime.Now - lastUpdateTime;
                if (ts.TotalSeconds > 60)
                {
                    DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Rows.RemoveAt(i);
                    break;
                }
            }
        }
        private void AttempToConnectServer()
        {
            //System.Windows.Forms.MessageBox.Show("尝试连接服务端");

            //尝试连接服务端

            while (true)
            {
                try
                {
                    //Application.DoEvents();
                    Socket_Service.DisconnectServer();
                    if (Socket_Service.ConnectServer(Global.ServerIP, Global.ServerPort))//服务端已开启
                    {
                        if (Socket_Service.SendMessage_Safe(Socket_Service.Command_C2S_Reg, Global.PresentUser, "", "", "", "", "", "", "", ""))//使用当前账号登陆成功
                        {
                            //将MissPositionTimes清零，否则timer_UpdateUI立即运行，MissPositionTimes++大于Global.DisconnectTimes将认为未连接
                            MissPositionTimes = 0;
                            //必须用委托的方式才能访问timer_UpdateUI
                            this.Invoke(new Action<System.Windows.Forms.Timer>(l =>
                                {
                                    l.Enabled = true;
                                }), timer_UpdateUI);
                            Global.State_IsServerRunning = true;

                            this.Invoke(new Action<System.Windows.Forms.Timer>(l =>
                                {
                                    l.Enabled = true;
                                }), clienttick);
                            break;
                        }
                        else//当前账号已被使用
                        {
                            //MessageBox.Show("对不起，您的帐户已经在IP地址(" + Socket_Service.theuserip + ")处登陆，请联系管理员。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                }
                catch (Exception ex)
                {
                    if (Global.IsShowBug)
                        System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "重新连接服务端错误");
                }
                Thread.Sleep(8000);

            }
        }
        /// <summary>
        /// 根据登录用户的权限字串初始化功能按钮的可见性
        /// </summary>
        private void InitMainBtn()
        {
            string[] PowerArray = Regex.Split(DB_Service.MainDataSet.Tables["UserTable"].Select("UserName = '" + Global.PresentUser + "'")[0]["PowerStr"].ToString(), "-");

            for (int i = 0; i < PowerArray.Length; i++)
            {
                if (PowerArray[i] != "")
                {
                    switch (PowerArray[i])
                    {
                        case "0":
                            MainBtn_System.Visible = true;
                            break;
                        case "1":
                           // MainBtn_AlarmArea.Visible = true;
                            break;
                        case "2":
                           // MainBtn_History.Visible = true;
                            break;
                        case "3":
                            MainBtn_Duty.Visible = true;
                            break;
                        case "4":
                            MainBtn_Collect.Visible = true;
                            break;
                        case "5":
                            MainBtn_Other.Visible = true;
                            break;
                        case "6":
                            MainBtn_Machine.Visible = true;
                            break;
                        case "7":
                            MainBtn_Person.Visible = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 根据LayerTable和LayerSortTable中的基础图层记录，构建TreeView。并添加相应的图层表到MainDataSet（不添加文件数据源的图层表）
        /// 注意：因为LayerTable与LayerSortTable已经order by ViewOrder ASC 所以树的顺序与ViewOrder的顺序完全相同
        /// </summary>
        private void BuildLayerTableAndTreeView()
        {
            MainTreeView.Nodes.Clear();

            //添加根
            string picID = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["PicID"].ToString();
            imageList_Tree.Images.Add(picID, Resource_Service.GetImage(picID));
            string nodeName = Global.MapName;
            MainTreeView.Nodes.Add(nodeName, nodeName, picID, picID);


            for (int i = 0; i < DB_Service.MainDataSet.Tables["LayerTable"].Rows.Count; i++)
            {
                //添加树枝
                //注意：因为LayerTable与LayerSortTable已经order by ViewOrder ASC 所以树的顺序与ViewOrder的顺序完全相同
                picID = DB_Service.MainDataSet.Tables["LayerTable"].Rows[i]["PicID"].ToString();
                imageList_Tree.Images.Add(picID, Resource_Service.GetImage(picID));

                TreeNode node_branch = new TreeNode();
                node_branch.Name = DB_Service.MainDataSet.Tables["LayerTable"].Rows[i]["TableOrShapeFile"].ToString();
                node_branch.Text = DB_Service.MainDataSet.Tables["LayerTable"].Rows[i]["LayerName"].ToString();
                node_branch.ImageKey = picID;
                node_branch.SelectedImageKey = picID;
                node_branch.Tag = DB_Service.MainDataSet.Tables["LayerTable"].Rows[i]["DataSourceType"];
                if (Convert.ToInt32(node_branch.Tag) != 2)
                {
                    //不是文件数据源才添加表到DataSet
                    DB_Service.AddLayerTable(node_branch.Name, Convert.ToInt32(node_branch.Tag));
                }
                //当需要在树中显示才添加
                if (Convert.ToBoolean(DB_Service.MainDataSet.Tables["LayerTable"].Rows[i]["IsShowInTree"]))
                {
                    MainTreeView.Nodes[0].Nodes.Add(node_branch);
                    //如果有子，则添加子
                    DataRow[] rows_sort = DB_Service.MainDataSet.Tables["LayerSortTable"].Select("TableOrShapeFile = '" + node_branch.Name + "'");
                    if (rows_sort.Length > 0)
                    {
                        try
                        {
                            foreach (DataRow row in rows_sort)
                            {
                                //添加树叶
                                picID = row["PicID"].ToString();
                                imageList_Tree.Images.Add(picID, Resource_Service.GetImage(picID));

                                TreeNode node_leaf = new TreeNode();
                                node_leaf.Name = node_branch.Name + Global.SplitKey + row["ColumnName"].ToString() + Global.SplitKey + row["MaybeValue"].ToString();
                                node_leaf.Text = row["Text"].ToString();
                                node_leaf.ImageKey = picID;
                                node_leaf.SelectedImageKey = picID;
                                node_leaf.Tag = node_branch.Tag;
                                MainTreeView.Nodes[0].Nodes[node_branch.Name].Nodes.Add(node_leaf);
                            }
                        }
                        catch(Exception ex)
                        {
                            if (Global.IsShowBug)
                                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面添加树叶错误");
                        }
                    }
                    rows_sort = null;
                }
            }
        }

        /// <summary>
        /// 通过TreeView的选择情况，更新mapImage中欲显示的图层并刷新
        /// </summary>
        private void RefreshLayerVisable()
        {
            try
            {
                foreach (TreeNode node in MainTreeView.Nodes[0].Nodes)
                {
                    if (node.Checked)
                    {
                        //树枝选中
                        if (node.Nodes.Count < 1)
                        {
                            //没有叶子，则直接装载表
                            int i;
                            for (i = 0; i < mapImage.Map.Layers.Count; i++)
                            {
                                if (mapImage.Map.Layers[i].LayerName == node.Name)
                                {
                                    break;
                                }
                            }
                            if (i == mapImage.Map.Layers.Count)
                            {
                                //至此说明Layers里没有选中的这个图层，则加载之
                                CommonFun.AddLayer(node.Name, this.mapImage, 5, -13);
                            }
                        }
                        else
                        {
                            //有叶子，则根据叶子的选择情况装载图层
                            foreach (TreeNode node_leaf in node.Nodes)
                            {
                                if (node_leaf.Checked)
                                {
                                    //叶子选中
                                    int i;
                                    for (i = 0; i < mapImage.Map.Layers.Count; i++)
                                    {
                                        if (mapImage.Map.Layers[i].LayerName == node_leaf.Name)
                                        {
                                            break;
                                        }
                                    }
                                    if (i == mapImage.Map.Layers.Count)
                                    {
                                        //至此说明Layers里没有选中的这个图层，则加载之
                                        CommonFun.AddLayer(node_leaf.Name, this.mapImage, 5, -13);
                                    }
                                }
                                else
                                {
                                    //叶子没有选中
                                    for (int j = 0; j < this.mapImage.Map.Layers.Count; j++)
                                    {
                                        if (mapImage.Map.Layers[j].LayerName == node_leaf.Name)
                                        {
                                            //至此说明没有选中的图层却在Layers里有，则删除之
                                            DelLayer(j);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //树枝未选中
                        if (node.Nodes.Count < 1)
                        {
                            //没有叶子
                            for (int j = 0; j < this.mapImage.Map.Layers.Count; j++)
                            {
                                if (mapImage.Map.Layers[j].LayerName == node.Name)
                                {
                                    //至此说明没有选中的图层却在Layers里有，则删除之
                                    DelLayer(j);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //有叶子
                            foreach (TreeNode node_leaf in node.Nodes)
                            {
                                for (int j = 0; j < mapImage.Map.Layers.Count; j++)
                                {
                                    if (mapImage.Map.Layers[j].LayerName == node_leaf.Name)
                                    {
                                        //至此说明没有选中的图层却在Layers里有，则删除之
                                        DelLayer(j);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                this.splitContainer6.Panel1.BackgroundImage = null;
                this.mapImage.Refresh();
            }
            catch(Exception ex)
            {
                if (Global.IsShowBug)
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面RefreshLayerVisable错误");
            }
        }

        /// <summary>
        /// 卸载mapImage中指定index的Layer,并同时卸载选择图层
        /// 如果是DB数据源，则将以这个图层名命名的DataTable从MainDataSet删除
        /// </summary>
        /// <param name="layerindex">mapImage中的索引</param>
        private void DelLayer(int layerindex)
        {
            this.mapImage.Map.Layers.RemoveAt(layerindex);
            this.mapImage.Map.Layers.RemoveAt(layerindex);
        }

        /// <summary>
        /// 根据TreeView的选择情况更新ListView
        /// 参数IsDeepRefresh：是否是深层刷新（包括表结构的刷新）
        /// </summary>
        private void RefreshMainListView(bool IsDeepRefresh)
        {
            //锁定isSystemCheck，去除系统事件对CheckBox的影响
            isSystemCheck = true;
            //为开始更新控件做停止重绘的准备
            MainListView.BeginUpdate();
            
            if (IsDeepRefresh)
            {
                MainListView.Columns.Clear();
            }
            MainListView.Items.Clear();
            //得到当前选中的项目节点
            TreeNode selectNode = MainTreeView.SelectedNode;
            label_ListViewTitle.Text =selectNode.Text + "列表";
            //只在非根节点时才填充ListView
            if (selectNode.Parent != null && selectNode.Checked)
            {
                string[] strArray = Regex.Split(selectNode.Name, Global.SplitKey);
                
                if (strArray.Length == 1)
                {
                    //以下是尚未分类的

                    if (selectNode.Nodes.Count == 0)
                    {
                        //点了没有子分类的叶子
                        AddTableToListView(strArray[0], "", "");
                    }
                    else
                    {
                        try
                        {
                            //点了有子分类的树枝，则循环加载所有的子分类
                            foreach (TreeNode node_leaf in selectNode.Nodes)
                            {
                                if (node_leaf.Checked)
                                {
                                    string[] strArrayLeaf = Regex.Split(node_leaf.Name, Global.SplitKey);
                                    AddTableToListView(strArrayLeaf[0], strArrayLeaf[1], strArrayLeaf[2]);
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            if (Global.IsShowBug)
                                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面RefreshMainListView错误");
                        }
                    }
                }
                else
                {
                    //以下是已经分类好的

                    //点了有分类的叶子，则加载制定的子分类
                    AddTableToListView(strArray[0], strArray[1], strArray[2]);
                }
            }
            //更新控件结束。重绘控件
            MainListView.EndUpdate();
            //解除锁定isSystemCheck，去除系统事件对CheckBox的影响
            isSystemCheck = false;
        }

        /// <summary>
        /// 添加制定的表到ListView，并根据alwaysShowInfoList初始化CheckBox的选中
        /// 注:如果没有筛选条件，则参数：筛选条件列名为"", 值为-1
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="colname">筛选条件列名</param>
        /// <param name="value">值</param>
        private void AddTableToListView(string tablename,string colname,string value)
        {
            //如果没有列，则初始化列
            if (MainListView.Columns.Count < 1)
            {
                if (tablename == "PositionTable")
                {
                    MainListView.Columns.Add("ID", "卡号");
                    MainListView.Columns["ID"].Width = 70;
                    MainListView.Columns.Add("Name", "姓名");
                    MainListView.Columns.Add("CardType", "卡片类型");
                    MainListView.Columns.Add("NearStationID", "所在基站");
                    //如果是人员信息，则在每项前面显示CheckBoxes
                    MainListView.CheckBoxes = true;
                    linkLabel1.Visible = true;
                    linkLabel2.Visible = true;
                }
                else
                {
                    MainListView.Columns.Add("ID", "编号");
                    MainListView.Columns.Add("Name", "名称");
                    MainListView.Columns.Add("StationType", "类型");
                    MainListView.Columns.Add("StationFunction", "功能");
                    MainListView.Columns.Add("IP", "IP地址");
                    MainListView.Columns.Add("Port", "端口");
                    MainListView.Columns.Add("Area", "区域");
                    MainListView.Columns.Add("DutyOrder", "考勤编号");
                    MainListView.Columns.Add("MapName", "地图");
                    //如果不是人员信息，则不在每项前面显示CheckBoxes
                    MainListView.CheckBoxes = false;
                    linkLabel1.Visible = false;
                    linkLabel2.Visible = false;
                }
                MainListView.Columns.Add("Geo_X", "X坐标");
                MainListView.Columns.Add("Geo_Y", "Y坐标");
            }
            //填充数据
            //根据列名的有无判断是否有筛选条件
            if (colname == "")
            {
                //注：这里如果图片标识为""，在构造Itme时则无影响
                string strPoint = DB_Service.MainDataSet.Tables["LayerTable"].Select("TableOrShapeFile = '" + tablename + "'")[0]["PointImage"].ToString();
                try
                {
                    foreach (DataRow row_db in DB_Service.MainDataSet.Tables[tablename].Rows)
                    {
                        ListViewItem.ListViewSubItem[] array = new ListViewItem.ListViewSubItem[MainListView.Columns.Count];
                        for (int i = 0; i < array.Length; i++)
                        {
                            array[i] = new ListViewItem.ListViewSubItem();
                            array[i].Name = MainListView.Columns[i].Name;
                            array[i].Text = row_db[MainListView.Columns[i].Name].ToString();//从数据库中将制定列名项的内容填充
                        }
                        ListViewItem tempItem = new ListViewItem(array, strPoint);
                        //初始化CheckBox选中
                        if (MainListView.CheckBoxes)
                        {
                            if (alwaysShowInfoList.Contains(tempItem.SubItems[0].Text))
                            {
                                tempItem.Checked = true;
                            }
                        }
                        MainListView.Items.Add(tempItem);
                    }
                }
                catch(Exception ex)
                {
                    if (Global.IsShowBug)
                        System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面AddTableToListView添加整张表错误");
                }
            }
            else
            {
                //注：这里如果图片标识为""，在构造Itme时则无影响
                string strPoint = DB_Service.MainDataSet.Tables["LayerSortTable"].Select("TableOrShapeFile = '" + tablename + "' and ColumnName = '" + colname + "' and MaybeValue = '" + value + "'")[0]["PointImage"].ToString();
                DataRow[] rowsss = DB_Service.MainDataSet.Tables[tablename].Select(colname + " = '" + value + "'");
                for (int i = 0; i < rowsss.Length; i++)
                {
                    try
                    {
                        ListViewItem.ListViewSubItem[] array = new ListViewItem.ListViewSubItem[MainListView.Columns.Count];
                        for (int j = 0; j < array.Length; j++)
                        {
                            array[j] = new ListViewItem.ListViewSubItem();
                            array[j].Name = MainListView.Columns[j].Name;
                            array[j].Text = rowsss[i][MainListView.Columns[j].Name].ToString();//从数据库中将制定列名项的内容填充
                        }
                        ListViewItem tempItem = new ListViewItem(array, strPoint);
                        //初始化CheckBox选中
                        if (MainListView.CheckBoxes)
                        {
                            if (alwaysShowInfoList.Contains(tempItem.SubItems[0].Text))
                            {
                                tempItem.Checked = true;
                            }
                        }
                        MainListView.Items.Add(tempItem);
                    }
                    catch(Exception ex)
                    {
                        if (Global.IsShowBug)
                            System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面AddTableToListView添加分类表错误");
                    }
                }
            }
        }

        /// <summary>
        /// 根据特殊区域方案表刷新AreaSubjectComBox
        /// </summary>
        private void RefreshAreaSubjectComBox()
        {
            com_AreaSubject.Items.Clear();
            com_AreaSubject.Items.Add("未启动");
            DataTable tempSpecalTable = DB_Service.GetTable("TempSpecalTable", "select * from SpecalTable");
            for (int i = 0; i < tempSpecalTable.Rows.Count; i++)
            {
                try
                {
                    string AlarmAreaName = tempSpecalTable.Rows[i]["Name"].ToString();
                    com_AreaSubject.Items.Add(AlarmAreaName);
                }
                catch(Exception ex)
                {
                    if (Global.IsShowBug)
                        System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面刷新AreaSubjectComBox错误");
                }
            }
        }

        /// <summary>
        /// 递归设置指定节点的所有子项的选择状态
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="nodeChecked"></param>
        private void SetNodeAllChild(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Expand();
                node.Checked = nodeChecked;
                SetNodeAllChild(node, nodeChecked);
            }
        }

        /// <summary>
        /// 改变主窗体显示
        /// 注：定位Panel不卸载，只是隐藏。别的功能Panel则动态加载
        /// </summary>
        /// <param name="OtherForm">包含功能Panel的功能窗体</param>
        private void ChangeFormView(Form FunctionForm)
        {
            //使人员定位Panel不可见
            this.splitContainer3.Visible = false;
            //卸载之前的功能Panel
            if (this.splitContainer1.Panel2.Controls.Count > 2)
            {
                this.splitContainer1.Panel2.Controls.RemoveAt(2);
            }
            //卸载当前的功能窗体
            if (PresentFunctionForm != null)
            {
                PresentFunctionForm.Dispose();
                PresentFunctionForm = null;
            }
            //将PresentFunctionForm赋予新值
            PresentFunctionForm = FunctionForm;
            //将FunctionForm的功能Panel添加到主窗体中
            this.splitContainer1.Panel2.Controls.Add((Panel)FunctionForm.Tag);
            GC.Collect();
        }

        private void MainTreeView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            //此判断筛选掉所有非用户真实操作事件
            if (e.Action != TreeViewAction.Unknown)
            {
                //如果用户点了根节点，则取消并跳出
                if (e.Node.Parent == null)
                {
                    e.Cancel = true;
                    return;
                }
                //选中全部子结点
                SetNodeAllChild(e.Node, !e.Node.Checked);
                //保留当前节点及值 
                bool bol = !e.Node.Checked;
                TreeNode tn = e.Node;
                //循环测试直到父结点为空
                while (tn.Parent != null)
                {
                    //判断兄弟结点中是否存在不一致的值
                    int i;
                    for (i = 0; i < tn.Parent.Nodes.Count; i++)
                    {
                        try
                        {
                            if (tn.Parent.Nodes[i].Checked != bol && tn.Parent.Nodes[i] != e.Node)
                            {
                                break;
                            }
                        }
                        catch(Exception ex)
                        {
                            if (Global.IsShowBug)
                                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端点选树控件错误");
                        }
                    }
                    tn = tn.Parent;
                    //如果存在不一致的值或当前选项为取消则所有父结点的值必为取消
                    if (bol == false  && i == tn.Nodes.Count)
                    {
                        tn.Checked = false;
                    }
                    else
                    {
                        tn.Checked = true;
                    }
                }
            }
        }

        private void MainTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //此判断筛选掉所有非用户真实操作事件
            if (e.Action != TreeViewAction.Unknown)
            {
                //更新mapImage中的图层
                RefreshLayerVisable();
                //如果当前节点没有选中，则自动将焦点移到根节点上
                if (!MainTreeView.SelectedNode.Checked)
                {
                    MainTreeView.SelectedNode = MainTreeView.Nodes[0];
                }
                //清空实时显示列表
                alwaysShowInfoList.Clear();
                //更新ListView并设置查询图层ComBox
                RefreshMainListView(true);
            }
        }

        private void MainTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //此判断筛选掉所有非用户真实操作事件
            if (e.Action != TreeViewAction.Unknown)
            {
                if (!e.Node.Checked)
                {
                    e.Cancel = true;
                }
                else if (Convert.ToInt32(e.Node.Tag) == 2)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //通过TreeView的选择情况，更新RefreshMainListView并设置查询图层ComBox
            RefreshMainListView(true);
        }

        private void MainListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (MainListView.SelectedItems.Count == 1)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        //判断是否点选的是人员
                        if (MainListView.Columns.ContainsKey("CardType"))
                        {
                            com_SendMesPeople.Text = MainListView.SelectedItems[0].SubItems["Name"].Text;
                        }
                        break;
                    case MouseButtons.Right:
                        //判断是否点选的是人员
                        if (MainListView.Columns.ContainsKey("CardType"))
                        {
                            //人员
                            Menu_Main.Tag = MainListView.SelectedItems[0].SubItems["ID"].Text;
                            Menu_Main.Show(MainListView.PointToScreen(e.Location));
                        }
                        break;
                }
            }
        }

        private void MainListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MainListView.SelectedItems.Count > 0)
            {
                switch (MainListView.Columns["Name"].Text)
                {
                    case "姓名":
                        //说明这个表里是人员
                        ShowPersonToolTipInMap(MainListView.SelectedItems[0].SubItems["Name"].Text, Convert.ToInt32(MainListView.SelectedItems[0].SubItems["ID"].Text), MainListView.SelectedItems[0].SubItems["CardType"].Text, Convert.ToDouble(MainListView.SelectedItems[0].SubItems["Geo_X"].Text), Convert.ToDouble(MainListView.SelectedItems[0].SubItems["Geo_Y"].Text));
                        break;
                    case "名称":
                        //说明这个表里是基站
                        ShowStationToolTipInMap(MainListView.SelectedItems[0].SubItems["Name"].Text, Convert.ToInt32(MainListView.SelectedItems[0].SubItems["ID"].Text), MainListView.SelectedItems[0].SubItems["StationType"].Text,MainListView.SelectedItems[0].SubItems["StationFunction"].Text, Convert.ToDouble(MainListView.SelectedItems[0].SubItems["Geo_X"].Text), Convert.ToDouble(MainListView.SelectedItems[0].SubItems["Geo_Y"].Text));
                        break;
                }
            }
        }

        /// <summary>
        /// 强行更新指定图层名的数据源
        /// </summary>
        /// <param name="LayerName">图层名</param>
        /// <param name="RefreshMapImageType">刷新方式：0不刷新，1刷新除MainMap以外的所有，2全部刷新</param>
        public void UpdateLayerDataSource(string LayerName, int RefreshMapImageType)
        {
            if (mapImage.Map.Layers[LayerName] != null)
            {
                VectorLayer tempLayer;
                tempLayer = (VectorLayer)mapImage.Map.Layers[LayerName];
                tempLayer.DataSource = new GeometryFeatureProvider(new FeatureDataTable(DB_Service.GetDataTableByLayerName(LayerName)));
                ((LabelLayer)mapImage.Map.Layers["LL" + LayerName]).DataSource = tempLayer.DataSource;
                tempLayer = null;

                switch (RefreshMapImageType)
                {
                    case 0:
                        break;
                    case 1:
                        if (mapImage.Map.Layers[this.MapLayerName] != null)
                        {
                            mapImage.RefreshWithOutLayer(this.MapLayerName);
                        }
                        break;
                    case 2:
                        mapImage.Refresh();
                        break;
                }
            }
        }

        /// <summary>
        /// mapImage背景容器的绘制方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer6_Panel1_Paint(object sender, PaintEventArgs e)
        {
            //绘制故障基站
            if (Global.State_ErrorStationList != null)
            {
                for (int es = 0; es < Global.State_ErrorStationList.Length; es++)
                {
                    int StationID = Convert.ToInt32(Global.State_ErrorStationList[es]);
                    DataRow[] stationRows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID);
                    if (stationRows.Length > 0)
                    {
                        PointF stationPoint = mapImage.Map.WorldToImage(new SharpMap.Geometries.Point(Convert.ToDouble(stationRows[0]["Geo_X"]), Convert.ToDouble(stationRows[0]["Geo_Y"])));

                        e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Red), 2), stationPoint.X - 7, stationPoint.Y - 7, stationPoint.X + 7, stationPoint.Y + 7);
                        e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Red), 2), stationPoint.X - 7, stationPoint.Y + 7, stationPoint.X + 7, stationPoint.Y - 7);
                    }
                }
            }
            //绘制背景图片
            if (this.IsLoadMapBackgroundPic)
            {
                e.Graphics.DrawImage(Image.FromFile(Global.MapPath + Global.MapBackgroundPic), new Rectangle(0, 0, splitContainer6.Panel1.Width, splitContainer6.Panel1.Height));
            }
            //绘制人员离基站或基点的距离
            try
            {
                for (int seek_D = 0; seek_D < alwaysShowInfoList.Count; seek_D++)
                {
                    DataRow[] Row_Position = DB_Service.MainDataSet.Tables["PositionTable"].Select("ID = " + Convert.ToInt32(alwaysShowInfoList[seek_D]));
                    if (Row_Position.Length > 0)
                    {
                        string Temp_Name;
                        double[] Temp_Point;
                        if (Global.BasicPointName == "")
                        {
                            int stationID1 = Convert.ToInt32(Row_Position[0]["NearStationID"]);
                            DataRow[] stationRows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + stationID1);
                            Temp_Name = stationRows[0]["Name"].ToString();
                            Temp_Point = new double[2] { Convert.ToDouble(stationRows[0]["Geo_X"]), Convert.ToDouble(stationRows[0]["Geo_Y"]) };
                        }
                        else
                        {
                            Temp_Name = Global.BasicPointName;
                            Temp_Point = new double[2] { Global.BasicPointPositionX, Global.BasicPointPositionY };
                        }

                        double person_X = Convert.ToDouble(Row_Position[0]["Geo_X"]);
                        double person_Y = Convert.ToDouble(Row_Position[0]["Geo_Y"]);
                        double Distance = Math.Round(Math.Sqrt(Math.Pow(Temp_Point[0] - person_X, 2) + Math.Pow(Temp_Point[1] - person_Y, 2)) * Global.MapDistanceKey, 1);

                        PointF startPoint1 = mapImage.Map.WorldToImage(new SharpMap.Geometries.Point(person_X, person_Y));
                        startPoint1.X -= 52;
                        startPoint1.Y += 8;
                        e.Graphics.DrawString("距" + Temp_Name + ":" + Distance + "m", new Font("黑体", 10), new SolidBrush(Global.PersonDistanceColor), startPoint1);
                    }
                }
            }
            catch(Exception ex)
            {
                if (Global.IsShowBug)
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端主界面绘制人员离基站或基点的距离错误");
            }
            //绘制测距的线条
            try
            {
                if (this.mapImage.ActiveTool == SharpMap.Forms.MapImage.Tools.Distance)
                {
                    if (arrayDistancePoint.Count > 1)
                    {
                        SharpMap.Geometries.Point[] temp = new SharpMap.Geometries.Point[arrayDistancePoint.Count];
                        for (int i = 0; i < temp.Length; i++)
                        {
                            temp[i] = (SharpMap.Geometries.Point)arrayDistancePoint[i];
                            PointF tempDrawRECTPoint = this.mapImage.Map.WorldToImage(temp[i]);
                            e.Graphics.DrawRectangle(new Pen(Global.DistancePointColor, 7), tempDrawRECTPoint.X - 1, tempDrawRECTPoint.Y - 1, 2, 2);
                        }
                        PointF[] DrawLineArray = new PointF[temp.Length];
                        for (int k = 0; k < DrawLineArray.Length; k++)
                        {
                            DrawLineArray[k] = this.mapImage.Map.WorldToImage(temp[k]);
                        }
                        e.Graphics.DrawLines(new Pen(Global.DistanceLineColor, 2), DrawLineArray);

                        double totalDistance = 0;

                        for (int j = 1; j < temp.Length; j++)
                        {

                            if (temp[j] != temp[j - 1])
                            {
                                double distance = Math.Round(Math.Sqrt(Math.Pow(temp[j].X - temp[j - 1].X, 2) + Math.Pow(temp[j].Y - temp[j - 1].Y, 2)) * Global.MapDistanceKey, 1);
                                distance = Math.Round(distance, 1);
                                totalDistance += distance;
                                SharpMap.Geometries.Point tempPoint = new SharpMap.Geometries.Point((temp[j].X - temp[j - 1].X) / 2 + temp[j - 1].X, (temp[j].Y - temp[j - 1].Y) / 2 + temp[j - 1].Y);
                                e.Graphics.DrawString(distance.ToString() + "m", this.Font, new SolidBrush(Global.DistanceTextColor), this.mapImage.Map.WorldToImage(tempPoint));
                            }

                        }
                        this.toolStripLabel5.Text = "总长度:" + totalDistance.ToString() + "m ";

                        temp = null;
                    }
                }
            }
            catch(Exception ex)
            {
                if (Global.IsShowBug)
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端绘制测距的线条错误");
            }
        }

        private void splitContainer6_Panel1_BackgroundImageChanged(object sender, EventArgs e)
        {
            arrayDistancePoint.Clear();
        }

        private void MainListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!isSystemCheck)
            {
                if (e.Item.Checked)
                {
                    alwaysShowInfoList.Add(e.Item.SubItems[0].Text);
                }
                else
                {
                    alwaysShowInfoList.Remove(e.Item.SubItems[0].Text);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                foreach (ListViewItem item in MainListView.Items)
                {
                    item.Checked = true;
                }
            }
            catch
            { }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                foreach (ListViewItem item in MainListView.Items)
                {
                    item.Checked = false;
                }
            }
            catch
            { }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //自动将滚动条滚动至文本的最后
            this.richTextBox1.ScrollToCaret();
        }

        /// <summary>
        /// 处理格式化的信息
        /// 用户信息中不能包含以下识别标记
        /// 信息类型识别标记：前导符为一个："<"，后导符为一个：">"
        /// 点击阅读识别标记："◇已阅此未读信息◇"
        /// 卡号识别标记：前导符为左括弧："("，后导符为右括弧：")"
        /// 时间识别标记：前导符为一个空格：" "，后导符为两个空格："  "
        /// </summary>
        private void OperateTip()
        {
            try
            {
                int LineStartIndex = richTextBox1.GetFirstCharIndexOfCurrentLine();
                //解析出短信类型
                int MesTypeStart = richTextBox1.Text.IndexOf('<', LineStartIndex) + 1;
                int MesTypeLength = richTextBox1.Text.IndexOf('>', LineStartIndex) - MesTypeStart;
                string MesType = richTextBox1.Text.Substring(MesTypeStart, MesTypeLength);
                //解析出卡片ID
                int CardIDStart = richTextBox1.Text.IndexOf('(', LineStartIndex) + 1;
                int CardIDLength = richTextBox1.Text.IndexOf(')', LineStartIndex) - CardIDStart;
                int CardID = Convert.ToInt32(richTextBox1.Text.Substring(CardIDStart, CardIDLength));
                //解析出时间
                int TimeStart = richTextBox1.Text.IndexOf(' ', LineStartIndex) + 1;
                int TimeLength = richTextBox1.Text.IndexOf("  ", LineStartIndex) - TimeStart;
                DateTime SendTime = Convert.ToDateTime(richTextBox1.Text.Substring(TimeStart, TimeLength));
                int resultNum = 0;
                switch (MesType)
                {
                    case ServerMessage.MESTYPE_IO:

                        break;
                    case ServerMessage.MESTYPE_LP:
                        resultNum = DB_Service.ExecuteSQL("update AlarmPowerTable set IsReaded = 'True' where CardID = " + CardID + " and ErrorStartTime = '" + SendTime + "'","AlarmPowerTable");
                        if (resultNum > 0)
                        {
                            //将未读信息状态量减resultNum
                            Global.State_UnReadLowPower -= resultNum;
                            if (Global.State_UnReadLowPower < 0)
                            {
                                Global.State_UnReadLowPower = 0;
                            }
                        }
                        break;
                    case ServerMessage.MESTYPE_PS:
                        resultNum = DB_Service.ExecuteSQL("update AlarmPersonSendTable set IsReaded = 'True' where CardID = " + CardID + " and SendTime = '" + SendTime + "'","AlarmPersonSendTable");
                        if (resultNum > 0)
                        {
                            //将未读信息状态量减resultNum
                            Global.State_UnReadPersonMes -= resultNum;
                            if (Global.State_UnReadPersonMes < 0)
                            {
                                Global.State_UnReadPersonMes = 0;
                            }
                        }
                        break;
                }
                if (resultNum > 0)
                {
                    //这个地方存在可能的BUG，应为标记已读的数量和界面修改未读短信的数量不一样
                    int InfoEndIndex = richTextBox1.Find(new char[1] { '◇' }, LineStartIndex, LineStartIndex + 100);
                    int InfoLength = InfoEndIndex - LineStartIndex;
                    richTextBox1.Select(LineStartIndex, InfoLength);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);

                    richTextBox1.Select(InfoEndIndex, 9);
                    richTextBox1.SelectedRtf = richTextBox1.SelectedRtf.Replace(@"\'a1\'f3\'d2\'d1\'d4\'c4\'b4\'cb\'ce\'b4\'b6\'c1\'d0\'c5\'cf\'a2\'a1\'f3", "");
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                }
                else
                {
                    //MessageBox.Show("保存信息失败！\n请确保数据库连接正确。", "人员报警信息管理");
                }
            }
            catch(Exception ex)
            {
                if (Global.IsShowBug)
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端处理格式化的短信错误");
            }
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            char word = richTextBox1.GetCharFromPosition(e.Location);
            int index = richTextBox1.GetCharIndexFromPosition(e.Location);

            //判断用户是否点击在了：◇已阅此未读信息◇ 上的开关变量
            bool isRealClieck = false;
            switch (word)
            {
                case '◇':
                    isRealClieck = true;
                    break;
                case '已':
                    if (richTextBox1.Text[index + 1] == '阅' || richTextBox1.Text[index - 1] == '◇')
                    {
                        isRealClieck = true;
                    }
                    break;
                case '阅':
                    if (richTextBox1.Text[index + 1] == '此' || richTextBox1.Text[index - 1] == '已')
                    {
                        isRealClieck = true;
                    }
                    break;
                case '此':
                    if (richTextBox1.Text[index + 1] == '未' || richTextBox1.Text[index - 1] == '阅')
                    {
                        isRealClieck = true;
                    }
                    break;
                case '未':
                    if (richTextBox1.Text[index + 1] == '读' || richTextBox1.Text[index - 1] == '此')
                    {
                        isRealClieck = true;
                    }
                    break;
                case '读':
                    if (richTextBox1.Text[index + 1] == '信' || richTextBox1.Text[index - 1] == '未')
                    {
                        isRealClieck = true;
                    }
                    break;
                case '信':
                    if (richTextBox1.Text[index + 1] == '息' || richTextBox1.Text[index - 1] == '读')
                    {
                        isRealClieck = true;
                    }
                    break;
                case '息':
                    if (richTextBox1.Text[index + 1] == '◇' || richTextBox1.Text[index - 1] == '信')
                    {
                        isRealClieck = true;
                    }
                    break;
            }
            //判断是否是点击 ◇已阅此未读信息◇
            if (isRealClieck)
            {
                Thread TextThread = new Thread(new ThreadStart(OperateTip));
                TextThread.Start();
            }
        }

        private void Status_UnRead_Click(object sender, EventArgs e)
        {
            if (label_StatusUnRead.Text != "0")
            {
                MainBtn_Alarm_Click(null, null);
            }
        }

        private void Status_ErrorStation_Click(object sender, EventArgs e)
        {
            if (label_StatusErrorStation.Text != "0")
            {
                MainBtn_Alarm_Click(null, null);
                frmAlarm.ShowStationAlarm();
            }
        }

        private void label_StatusUnReadLowPower_Click(object sender, EventArgs e)
        {
            if (label_StatusUnReadLowPower.Text != "0")
            {
                MainBtn_Alarm_Click(null, null);
                frmAlarm.ShowLowPowerAlarm();
            }
        }

        private void label_StatusAreaNum_Click(object sender, EventArgs e)
        {
            if (label_StatusAreaNum.Text != "0")
            {
                FrmInSomething frmInSomething = new FrmInSomething("特殊区域内人员", this, "0");
                frmInSomething.Show(this);
            }
        }

        private void btn_SaveBasicPoint_Click(object sender, EventArgs e)
        {
            if (text_BasicPoint.Text.Trim() != "")
            {
                Global.BasicPointName = text_BasicPoint.Text;
                toolStripLabel4.Text = "距离显示基点: " + text_BasicPoint.Text + " ";
                SharpMap.Geometries.Point point = (SharpMap.Geometries.Point)panel_BasicPoint.Tag;
                Global.BasicPointPositionX = point.X;
                Global.BasicPointPositionY = point.Y;
                panel_BasicPoint.Tag = null;
                text_BasicPoint.Text = "";
                panel_BasicPoint.Visible = false;
                this.btn_ZommIn.Checked = false;
                this.btn_ZommOut.Checked = false;
                this.btn_Move.Checked = true;
                this.btn_Distance.Checked = false;
                this.btn_BasicPoint.Checked = false;
                this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
            }
            else
            {
                MessageBox.Show("请输入一个基点的名称！", "定义基点", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void com_AreaSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            isSelectingAreaComBox = true;
            //如果选择相同的下拉菜单则不触发
            if (Global.State_AreaAlarmName != com_AreaSubject.Text)
            {
                if (label_StatusServer.Text == "运行")
                {
                    if (com_AreaSubject.Text != "未启动")
                    {
                        string tempAreaName = com_AreaSubject.Text;
                        if (Global.State_AreaAlarmName == "未启动")
                        {
                            if (MessageBox.Show("您确定要启动：" + tempAreaName + " 这个特殊区域监控方案吗？\n\n如果选择<是>，则程序立即开始按照方案中的设置监视特殊区域并报警。", "启动特殊区域监控方案", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //启动特殊区域监控方案
                                if (Socket_Service.SendMessage_Safe(Socket_Service.Command_C2S_AreaSubject, "ON", tempAreaName, "", "", "", "", "", "",""))
                                {
                                    Global.State_AreaAlarmName = tempAreaName;
                                }
                                else
                                {
                                    MessageBox.Show("与服务器通信失败！请重试。", "启动特殊区域监控方案", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("您确定要将：" + Global.State_AreaAlarmName + " 这个特殊区域监控方案替换成：" + tempAreaName + " 方案吗？\n\n如果选择<是>，则程序立即开始按照新方案中的设置监视特殊区域并报警。", "更换特殊区域监控方案", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //更换特殊区域监控方案
                                if (Socket_Service.SendMessage_Safe(Socket_Service.Command_C2S_AreaSubject, "CHANGE", tempAreaName, "", "", "", "", "", "",""))
                                {
                                    Global.State_AreaAlarmName = tempAreaName;
                                }
                                else
                                {
                                    com_AreaSubject.Text = Global.State_AreaAlarmName;
                                    MessageBox.Show("与服务器通信失败！请重试。", "更换特殊区域监控方案", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("您确定要停止特殊区域监控方案吗？", "停止特殊区域监控方案", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //停止特殊区域监控方案
                            if (Socket_Service.SendMessage_Safe(Socket_Service.Command_C2S_AreaSubject, "OFF", "", "", "", "", "", "", "",""))
                            {
                                label_StatusAreaNum.Text = "0";
                                Global.State_AreaAlarmName = "未启动";
                            }
                            else
                            {
                                com_AreaSubject.Text = Global.State_AreaAlarmName;
                                MessageBox.Show("与服务器通信失败！请重试。", "停止特殊区域监控方案", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请连接服务器后再进行操作。", "特殊区域监控方案", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            isSelectingAreaComBox = false;
        }

        private void com_AreaSubject_DropDown(object sender, EventArgs e)
        {
            isSelectingAreaComBox = true;
            RefreshAreaSubjectComBox();
        }

        private void com_AreaSubject_DropDownClosed(object sender, EventArgs e)
        {
            isSelectingAreaComBox = false;
        }

        private void btn_LightAlarmArea_EnabledChanged(object sender, EventArgs e)
        {
            if (btn_LightAlarmArea.Enabled && WAVPlayer == null)
            {
                try
                {
                    WAVPlayer = new System.Media.SoundPlayer(Global.AudioPath + Global.AudioArea + ".wav");
                    WAVPlayer.PlayLooping();
                }
                catch
                { }
            }
            else if (!btn_LightPeopleSend.Enabled)
            {
                if (WAVPlayer != null)
                {
                    WAVPlayer.Stop();
                    WAVPlayer.Dispose();
                    WAVPlayer = null;
                }
            }
        }

        private void btn_LightPeopleSend_EnabledChanged(object sender, EventArgs e)
        {
            if (btn_LightPeopleSend.Enabled && WAVPlayer == null)
            {
                try
                {
                    WAVPlayer = new System.Media.SoundPlayer(Global.AudioPath + Global.AudioPersonSend + ".wav");
                    WAVPlayer.PlayLooping();
                }
                catch
                {   }
            }
            else if (!btn_LightAlarmArea.Enabled)
            {
                if (WAVPlayer != null)
                {
                    WAVPlayer.Stop();
                    WAVPlayer.Dispose();
                    WAVPlayer = null;
                }
            }
        }

        private void MainListView_MouseEnter(object sender, EventArgs e)
        {
            IsInListView = true;
        }

        private void MainListView_MouseLeave(object sender, EventArgs e)
        {
            IsInListView = false;
        }

        private void listView_InMine_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView_InMine.Columns[1].Text != "未知" && listView_InMine.Columns[1].Text != "0")
            {
                FrmInSomething frmInSomething = new FrmInSomething("人员", this, "InMineTime is not null");
                frmInSomething.Show(this);

                //FrmInMine FIM = new FrmInMine();
                //FIM.Show(this);
            }
        }

        private void listView_InMine_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = listView_InMine.GetItemAt(e.X,e.Y);
            if (item != null)
            {
                if (item.SubItems[1].Text != "未知" && item.SubItems[1].Text != "0")
                {
                    string AreaName = item.SubItems[0].Text;
                    string tempSQL = "";
                    DataRow[] tempRows = DB_Service.MainDataSet.Tables["StationTable"].Select("Area = '" + AreaName + "'");
                    for (int i = 0; i < tempRows.Length; i++)
                    {
                        tempSQL += "NearStationID = " + tempRows[i]["ID"].ToString() + " or ";
                    }
                    string tempFinallySQL = "InMineTime is not null";
                    if (tempSQL != "")
                    {
                        tempFinallySQL = tempFinallySQL + " and (" + tempSQL.Substring(0, tempSQL.Length - 4) + ")";
                    }
                    FrmInSomething frmInSomething = new FrmInSomething(item.SubItems[0].Text + "区域人员", this, tempFinallySQL);
                    frmInSomething.Show(this);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmShowInfo FSI = new FrmShowInfo();
            FSI.ShowDialog(this);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label_TitleText_Click(object sender, EventArgs e)
        {

        }

        private void mapImage_Click(object sender, EventArgs e)
        {

        }

       

       

       

       
    }
}