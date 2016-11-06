using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SharpMap.Layers;
using SharpMap.Styles;
using SharpMap.Data;
using SharpMap.Data.Providers;

using PersonPosition.StaticService;
using PersonPosition.Common;

namespace PersonPosition.View
{
    public partial class DialogStation : Form
    {
        private DataRow StationRow = null;
        //临时保存的父基站ID
        private string OldFatherStationID = "";
        //临时保存的通道ID字串
        private string OldCollectChannelIDStr = "";
        //是否开始鼠标定位
        private bool IsPositionStart = false;
        //测量距离
        private bool isStartMoveDistane = false;//开关变量
        private ArrayList arrayDistancePoint = new ArrayList();//点数组

        private MainForm mainform;
        //装载弹出窗体在“添加通道”时产生的通道字符串 "通道号|名称|类型|通道ID" 如："2|桥墩温度传感器|电压电流型|172"
        public string AddCollectChannelStr = "";

        public DialogStation(MainForm _mainform)
        {
            InitializeComponent();
            this.mainform = _mainform;
        }

        /// <summary>
        /// 带初始化指定基站信息的构造函数
        /// </summary>
        /// <param name="stationID"></param>
        public DialogStation(bool isCanSave, int stationID, MainForm _mainform)
        {
            InitializeComponent();
            this.mainform = _mainform;

            if (!isCanSave)
            {
                btn_Save.Visible = false;
                btn_Canel.Visible = false;
            }

            StationRow = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + stationID)[0];
        }

        #region 地图操作工具栏控件事件

        private void btn_ZommIn_Click(object sender, EventArgs e)
        {
            this.btn_ZommIn.Checked = true;
            this.btn_ZommOut.Checked = false;
            this.btn_Move.Checked = false;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.ZoomIn;
        }

        private void btn_ZommOut_Click(object sender, EventArgs e)
        {
            this.btn_ZommIn.Checked = false;
            this.btn_ZommOut.Checked = true;
            this.btn_Move.Checked = false;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.ZoomOut;
        }

        private void btn_Move_Click(object sender, EventArgs e)
        {
            this.btn_ZommIn.Checked = false;
            this.btn_ZommOut.Checked = false;
            this.btn_Move.Checked = true;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
        }

        private void btn_Brows_Click(object sender, EventArgs e)
        {
            if (this.mapImage.Map.Layers.Count > 0)
            {
                try
                {
                    this.mapImage.Map.ZoomToExtents();
                }
                catch
                { }
                if (mapImage.Map.Zoom == 0.0)
                    mapImage.Map.Zoom = 1.0;
                mapImage.Refresh();
            }
        }

        private void btn_Distance_Click(object sender, EventArgs e)
        {
            this.btn_ZommIn.Checked = false;
            this.btn_ZommOut.Checked = false;
            this.btn_Move.Checked = false;
            this.btn_Distance.Checked = true;
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Distance;
        }

        #endregion

        private void DialogStation_Load(object sender, EventArgs e)
        {
            //初始化所有的地图combox
            foreach (DataRow row in DB_Service.MainDataSet.Tables["MapTable"].Rows)
            {
                this.com_Map.Items.Add(row["MapName"]);
            }
            //初始化所有的区域combox
            this.com_Area.Items.Add("不设置区域");
            foreach (DataRow row_Area in DB_Service.MainDataSet.Tables["MapAreaTable"].Rows)
            {
                this.com_Area.Items.Add(row_Area["MapAreaName"]);
            }
            com_Area.SelectedIndex = 0;
            com_Type.SelectedIndex = 0;
            com_Duty.SelectedIndex = 2;
            com_StationFunction.SelectedIndex = 0;
            com_MaxChannel.SelectedIndex = 0;

            if (StationRow != null)
                InitUserShow();
        }

        private void InitUserShow()
        {
            com_Map.Text = StationRow["MapName"].ToString();
            text_StationID.Text = StationRow["ID"].ToString();
            text_StationID.ReadOnly = true;
            text_Name.Text = StationRow["Name"].ToString();
            com_Area.Text = StationRow["Area"].ToString();
            if (com_Area.SelectedIndex == -1)
                com_Area.SelectedIndex = 0;
            text_RepairRSSI.Text = StationRow["RepairRSSI"].ToString();
            text_X.Text = StationRow["Geo_X"].ToString();
            text_Y.Text = StationRow["Geo_Y"].ToString();
            //更新com_Type.SelectedIndex的同时也触发了SelectedIndexChanged事件，从而也更新了listview_Son.Visible
            com_Type.Text = StationRow["StationType"].ToString();
            switch (com_Type.Text)
            {
                case "网关基站":
                    text_IP.Text = StationRow["IP"].ToString();
                    text_Port.Text = StationRow["Port"].ToString();
                    goto case "Can基站";
                case "Can基站":
                    //初始化子基站列表
                    listview_Son.Items.Clear();
                    string[] sonArray = StationRow["SonStationIDs"].ToString().Split('-');
                    for (int i = 0; i < sonArray.Length; i++)
                    {
                        if (sonArray[i] != "")
                        {
                            DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + sonArray[i]);
                            if (rows.Length > 0)
                            {
                                ListViewItem item = new ListViewItem(new string[] { sonArray[i], rows[0]["Name"].ToString() });
                                listview_Son.Items.Add(item);
                            }
                        }
                    }
                    break;
                case "无线基站":
                    text_Father.Text = StationRow["FatherStationID"].ToString();
                    OldFatherStationID = StationRow["FatherStationID"].ToString();
                    break;
            }
            //更新com_StationFunction.SelectedIndex的同时也触发了SelectedIndexChanged事件，从而也更新了panel_CollectChannel.Visible
            com_StationFunction.Text = StationRow["StationFunction"].ToString();
            switch (com_StationFunction.Text)
            {
                case "人员定位":
                    if (StationRow["DutyOrder"] == DBNull.Value)
                    {
                        check_IsDutyStation.Checked = false;
                        label_Duty.Visible = false;
                        com_Duty.Visible = false;
                    }
                    else
                    {
                        check_IsDutyStation.Checked = true;
                        label_Duty.Visible = true;
                        com_Duty.Visible = true;
                        com_Duty.SelectedIndex = Convert.ToInt32(StationRow["DutyOrder"]) - 1;
                    }
                    break;
                case "信息采集":
                    //初始化最大通道数
                    com_MaxChannel.Text = StationRow["MaxChannelNum"].ToString();
                    OldCollectChannelIDStr = StationRow["CollectChannelIDStr"].ToString();
                    //初始化通道列表
                    RefreshChannelListView(StationRow["CollectChannelIDStr"].ToString());
                    break;
            }
        }

        /// <summary>
        /// 根据传入的字符串初始化通道列表。并把每个通道的Channel_ID放入每项的Tag中
        /// </summary>
        /// <param name="ChannelStr">基站的通道字符串</param>
        private void RefreshChannelListView(string ChannelStr)
        {
            //初始化通道列表
            listView_Channel.Items.Clear();
            string[] channelArray = StationRow["CollectChannelIDStr"].ToString().Split('-');
            for (int j = 0; j < channelArray.Length; j++)
            {
                if (channelArray[j] != "")
                {
                    string[] temp = channelArray[j].Split(':');
                    if (temp.Length == 2)
                    {
                        DataRow[] rows_Channel = DB_Service.MainDataSet.Tables["CollectChannelTable"].Select("Channel_ID = " + temp[1]);
                        if (rows_Channel.Length > 0)
                        {
                            ListViewItem item = new ListViewItem(new string[] { temp[0], rows_Channel[0]["ChannelName"].ToString(), rows_Channel[0]["ChannelType"].ToString() });
                            //将Channel_ID放到Item的Tag里
                            item.Tag = temp[1];
                            //添加Item
                            listView_Channel.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (text_StationID.Text.Trim() == "" || text_Name.Text.Trim() == "" || com_Map.SelectedIndex == -1 || com_Type.SelectedIndex == -1 || com_StationFunction.SelectedIndex == -1 || text_X.Text.Trim() == "" || text_Y.Text.Trim() == "" || text_RepairRSSI.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整的基站信息后再进行保存。\n至少包含：所属地图、基站ID、基站名称、基站类型、基站功能、信号修正、基站位置坐标", "基站设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //如果是新建基站，则判断基站ID的唯一性并初始化StationRow
                if (StationRow == null)
                {
                    if (DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + text_StationID.Text.Trim()).Length > 0)
                    {
                        MessageBox.Show("对不起，您所输入的这个基站ID已经存在，请重新输入一个未被占用的基站ID。","新增基站", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        text_StationID.Text = "";
                        return;
                    }
                    else if (com_Type.SelectedIndex == 2 && text_Father.Text == "" && com_StationFunction.SelectedIndex == 0)
                    {
                        MessageBox.Show("对不起，人员定位的无线基站必须要有父基站。请输入父基站ID后继续。", "新增基站", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        //初始化StationRow
                        StationRow = DB_Service.MainDataSet.Tables["StationTable"].NewRow();
                        DB_Service.MainDataSet.Tables["StationTable"].Rows.Add(StationRow);
                    }
                }
                //修改StationRow的值
                StationRow["ID"] = Convert.ToInt32(text_StationID.Text.Trim());//不用约束用户输入。因为
                StationRow["Name"] = text_Name.Text.Trim();
                StationRow["MapName"] = com_Map.Text;
                if (com_Area.SelectedIndex == 0)
                {
                    StationRow["Area"] = "";
                }
                else
                {
                    StationRow["Area"] = com_Area.Text.Trim();
                }
                try
                {
                    StationRow["RepairRSSI"] = Convert.ToInt32(text_RepairRSSI.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("对不起，信号修正必须是数字。可以带负号。");
                    text_RepairRSSI.Text = "";
                    return;
                }
                StationRow["Geo_X"] = text_X.Text.Trim();
                StationRow["Geo_Y"] = text_Y.Text.Trim();
                StationRow["StationType"] = com_Type.Text;
                switch (com_Type.Text)
                {
                    case "Can基站":
                        StationRow["IP"] = "";
                        StationRow["Port"] = DBNull.Value;
                        StationRow["FatherStationID"] = DBNull.Value;
                        break;
                    case "网关基站":
                        StationRow["FatherStationID"] = DBNull.Value;
                        if (text_IP.Text.Trim() != "")
                        {
                            StationRow["IP"] = text_IP.Text.Trim();
                        }
                        if (text_Port.Text.Trim() != "")
                        {
                            StationRow["Port"] = text_Port.Text.Trim();
                        }
                        break;
                    case "无线基站":
                        StationRow["IP"] = "";
                        StationRow["Port"] = DBNull.Value;
                        StationRow["SonStationIDs"] = "";
                        //判断父是否有更改
                        if (OldFatherStationID != text_Father.Text.Trim())
                        {
                            //原来没有，现在新添加了
                            if (OldFatherStationID == "")
                            {
                                DataRow[] tempFather = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + text_Father.Text.Trim());
                                if (tempFather.Length > 0)
                                {
                                    //其父存在

                                    //在其父添加自己的ID
                                    string tempStr = tempFather[0]["SonStationIDs"].ToString();
                                    tempStr += StationRow["ID"].ToString() + "-";
                                    tempFather[0]["SonStationIDs"] = tempStr;
                                    //保存SonStationIDs
                                    StationRow["FatherStationID"] = text_Father.Text;
                                    //发送命令
                                    Socket_Service.SendMessage(Socket_Service.Command_C2S_AddRelation, text_Father.Text.Trim(), text_StationID.Text.Trim(), "", "", "", "", "", "","");
                                }
                                else
                                {
                                    //其父不存在
                                    MessageBox.Show("您输入的这个父基站不存在，请核对父基站ID是否输入正确。", "添加父基站", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    text_Father.Text = "";
                                    return;
                                }
                            }
                            else
                            {
                                //原来有，现在没有了
                                if (text_Father.Text == "")
                                {
                                    DataRow[] tempFather = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + OldFatherStationID);
                                    if (tempFather.Length > 0)
                                    {
                                        //其父存在

                                        //在其父删除自己的ID
                                        string tempStr = tempFather[0]["SonStationIDs"].ToString();
                                        string[] tempArray = System.Text.RegularExpressions.Regex.Split(tempStr, "-");
                                        tempStr = "";
                                        for (int i = 0; i < tempArray.Length; i++)
                                        {
                                            if (tempArray[i] != "")
                                            {
                                                if (tempArray[i] != StationRow["ID"].ToString())
                                                {
                                                    tempStr += tempArray[i].ToString() + "-";
                                                }
                                            }
                                        }
                                        tempFather[0]["SonStationIDs"] = tempStr;
                                        //保存FatherStationID
                                        StationRow["FatherStationID"] = DBNull.Value;
                                        //发送命令
                                        Socket_Service.SendMessage(Socket_Service.Command_C2S_DelRelation, OldFatherStationID, text_StationID.Text.Trim(), "", "", "", "", "", "","");
                                    }
                                }
                                else
                                {
                                    //现在有，原来也有
                                    MessageBox.Show("您不能将此基站直接转移给另一个父基站，因为他目前已经属于一个父基站了。\n若要转移，请先将此基站的父基站设置为空，然后再设置其父基站。", "设置父基站", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    text_Father.Text = OldFatherStationID;
                                    return;
                                }
                            }
                        }
                        break;
                }
                StationRow["StationFunction"] = com_StationFunction.Text;
                switch (com_StationFunction.Text)
                {
                    case "人员定位":
                        if (check_IsDutyStation.Checked)
                        {
                            StationRow["DutyOrder"] = com_Duty.Text.Split(':')[0];
                        }
                        else
                        {
                            StationRow["DutyOrder"] = DBNull.Value;
                        }
                        StationRow["MaxChannelNum"] = DBNull.Value;
                        StationRow["CollectChannelIDStr"] = "";
                        break;
                    case "信息采集":
                        StationRow["MaxChannelNum"] = Convert.ToInt32(com_MaxChannel.Text);
                        string tempCollectChannelIDStr = "";
                        foreach (ListViewItem item in listView_Channel.Items)
                        {
                            tempCollectChannelIDStr += item.SubItems[0].Text + ":" + item.Tag.ToString() + "-";
                        }
                        //只有当通道字串改变时才更新。这里ListView是按通道号排序的。所以这样比较没有问题
                        if (OldCollectChannelIDStr != tempCollectChannelIDStr)
                        {
                            StationRow["CollectChannelIDStr"] = tempCollectChannelIDStr;
                        }
                        StationRow["DutyOrder"] = DBNull.Value;
                        break;
                }
                //将StationTable中的更新提交至数据库
                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["StationTable"]) > 0)
                {
                    //更新监控窗体上的显示
                    this.mainform.UpdateLayerDataSource("StationTable" + Global.SplitKey + "StationType" + Global.SplitKey + "Can基站", 0);
                    this.mainform.UpdateLayerDataSource("StationTable" + Global.SplitKey + "StationType" + Global.SplitKey + "网关基站", 0);
                    this.mainform.UpdateLayerDataSource("StationTable" + Global.SplitKey + "StationType" + Global.SplitKey + "无线基站", 1);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("保存基站信息失败！\n请确保数据库连接正确。", "基站管理");
                }   
            }
        }

        private void btn_Canel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void com_Map_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mapImage.Map.Layers.Clear();

            //加载地图图层
            DataRow[] rows = DB_Service.MainDataSet.Tables["LayerTable"].Select("DataSourceType = 2");
            for (int i = 0; i < rows.Length; i++)
            {
                CommonFun.AddLayer(rows[i]["TableOrShapeFile"].ToString(), this.mapImage, 5, -13);
            }

            //加载基站图层
            DataRow[] tempRows = DB_Service.MainDataSet.Tables["LayerTable"].Select("TableOrShapeFile = 'StationTable'");
            if (tempRows.Length > 0)
            {
                CommonFun.AddLayer(tempRows[0]["TableOrShapeFile"].ToString(), this.mapImage, 5, -13);
            }

            //初始化mapImage
            if (this.mapImage.Map.Layers.Count > 0)
            {
                try
                {
                    this.mapImage.Map.ZoomToExtents();
                }
                catch
                { }
                if (mapImage.Map.Zoom == 0.0)
                    mapImage.Map.Zoom = 1.0;
                this.mapImage.Map.MinimumZoom = Global.MapImageMinView;
                this.mapImage.Map.MaximumZoom = mapImage.Map.Zoom * 2;
                this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
                btn_Brows_Click(sender, e);
            }
        }

        private void mapImage_MouseDown(SharpMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            if (ImagePos.Button == MouseButtons.Right)
            {
                IsPositionStart = false;
                mapImage.Cursor = Cursors.Hand;
                label11.Visible = false;
            }
        }

        private void text_StationID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_StationID_TextChanged(object sender, EventArgs e)
        {
            if (text_StationID.Text.Length > 1)
            {
                if (text_StationID.Text.Substring(0, 1) == "0")
                {
                    text_StationID.Text = "";
                }
            }
        }

        private void text_Father_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断只允许输入数字
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || Convert.ToInt32(e.KeyChar)==8))
            {
                e.Handled = true;
            }
        }

        private void com_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (com_Type.SelectedIndex)
            {
                case 0:
                    listview_Son.Visible = true;
                    text_Father.Visible = false;
                    text_IP.Visible = false;
                    text_Port.Visible = false;
                    label7.Visible = false;
                    label13.Visible = false;
                    break;
                case 1:
                    listview_Son.Visible = true;
                    text_Father.Visible = false;
                    text_IP.Visible = true;
                    text_Port.Visible = true;
                    label7.Visible = true;
                    label13.Visible = true;
                    break;
                case 2:
                    listview_Son.Visible = false;
                    text_IP.Visible = false;
                    text_Port.Visible = false;
                    label7.Visible = false;
                    label13.Visible = false;
                    if (com_StationFunction.SelectedIndex == 0)
                    {
                        //如果不是信息采集，则显示父基站
                        label12.Visible = true;
                        text_Father.Visible = true;
                    }
                    else
                    {
                        //如果是信息采集，则不显示父基站
                        label12.Visible = false;
                        text_Father.Visible = false;
                        text_Father.Text = "";
                    }
                    break;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IsPositionStart = true;
            mapImage.Cursor = Cursors.Cross;
            label11.Visible = true;
        }

        private void mapImage_MouseMove(SharpMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            if (IsPositionStart)
            {
                text_X.Text = Convert.ToString(Math.Round(WorldPos.X, 6));
                text_Y.Text = Convert.ToString(Math.Round(WorldPos.Y, 6));
            }

            if (this.mapImage.ActiveTool == SharpMap.Forms.MapImage.Tools.Distance)
            {
                if (isStartMoveDistane)
                {
                    if (arrayDistancePoint.Count > 0)
                    {
                        arrayDistancePoint[arrayDistancePoint.Count - 1] = WorldPos;// ImagePos.Location;
                        this.panel1.Refresh();
                    }
                    else
                    {
                        isStartMoveDistane = false;
                    }
                }
            }
        }

        private void check_IsDutyStation_CheckedChanged(object sender, EventArgs e)
        {
            if (check_IsDutyStation.Checked)
            {
                label_Duty.Visible = true;
                com_Duty.Visible = true;
                com_Duty.SelectedIndex = 2;
            }
            else
            {
                label_Duty.Visible = false;
                com_Duty.Visible = false;
            }
        }

        private void text_Port_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_RepairRSSI_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字和负号
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void text_Port_TextChanged(object sender, EventArgs e)
        {
            if (text_Port.Text.Length > 1)
            {
                if (text_Port.Text.Substring(0, 1) == "0")
                {
                    text_Port.Text = "";
                }
            }
        }

        private void mapImage_MouseUp(SharpMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            if (ImagePos.Button == MouseButtons.Left)
            {
                //左键
                switch (this.mapImage.ActiveTool)
                {
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
                }
            }
            else if (ImagePos.Button == MouseButtons.Right)
            {
                //右键
                switch (this.mapImage.ActiveTool)
                {
                    //当测距时若正在测距则停止添加新点，若已经停止测距则恢复成平移模式。
                    case SharpMap.Forms.MapImage.Tools.Distance:
                        if (isStartMoveDistane)
                        {
                            isStartMoveDistane = false;
                            arrayDistancePoint.RemoveAt(arrayDistancePoint.Count - 1);
                            this.panel1.Refresh();
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
                        this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
                        break;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //绘制测距的线条
            if (this.mapImage.ActiveTool == SharpMap.Forms.MapImage.Tools.Distance)
            {
                if (arrayDistancePoint.Count > 1)
                {
                    SharpMap.Geometries.Point[] temp = new SharpMap.Geometries.Point[arrayDistancePoint.Count];
                    for (int i = 0; i < temp.Length; i++)
                    {
                        try
                        {
                            temp[i] = (SharpMap.Geometries.Point)arrayDistancePoint[i];
                            PointF tempDrawRECTPoint = this.mapImage.Map.WorldToImage(temp[i]);
                            e.Graphics.DrawRectangle(new Pen(Global.DistancePointColor, 7), tempDrawRECTPoint.X - 1, tempDrawRECTPoint.Y - 1, 2, 2);
                        }
                        catch
                        { }
                    }
                    PointF[] DrawLineArray = new PointF[temp.Length];
                    for (int k = 0; k < DrawLineArray.Length; k++)
                    {
                        try
                        {
                            DrawLineArray[k] = this.mapImage.Map.WorldToImage(temp[k]);
                        }
                        catch
                        { }
                    }
                    e.Graphics.DrawLines(new Pen(Global.DistanceLineColor, 2), DrawLineArray);

                    double totalDistance = 0;

                    for (int j = 1; j < temp.Length; j++)
                    {
                        try
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
                        catch
                        { }
                    }
                    this.toolStripLabel1.Text = "总长度:" + totalDistance.ToString() + "m ";
                    
                    temp = null;
                }
            }
        }

        private void text_IP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字和句点
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void com_StationFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (com_StationFunction.SelectedIndex)
            {
                case 0:
                    panel_Position.Visible = true;
                    panel_CollectChannel.Visible = false;
                    //panel_ShowInfo.Visible = false;
                    label12.Visible = true;
                    text_Father.Visible = true;
                    break;
                case 1:
                    panel_CollectChannel.Visible = true;
                    panel_Position.Visible = false;
                    //panel_ShowInfo.Visible = false;

                    label12.Visible = false;
                    text_Father.Visible = false;
                    text_Father.Text = "";
                    break;
                case 2:
                    //panel_ShowInfo.Visible = true;
                    panel_CollectChannel.Visible = false;
                    panel_Position.Visible = false;
                    break;
            }
        }

        private void btn_EditChannel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (listView_Channel.SelectedItems.Count > 0)
            {
                Dictionary<int, int> templist = new Dictionary<int, int>();
                foreach (ListViewItem item in listView_Channel.Items)
                {
                    templist.Add(Convert.ToInt32(item.SubItems[0].Text), Convert.ToInt32(item.Tag));
                }
                //修改通道构造函数
                DialogCollectChannel dcc = new DialogCollectChannel(Convert.ToInt32(com_MaxChannel.Text), templist, Convert.ToInt32(listView_Channel.SelectedItems[0].SubItems[0].Text), Convert.ToInt32(listView_Channel.SelectedItems[0].Tag));
                dcc.ShowDialog(this);
                //刷新通道列表(有条目，说明一定不是新建的基站。故不判断 StationRow==null)
                RefreshChannelListView(StationRow["CollectChannelIDStr"].ToString());
            }
            else
            {
                MessageBox.Show("请先选择一个欲修改的通道。", "修改通道", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_AddChannel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dictionary<int, int> templist = new Dictionary<int, int>();
            foreach (ListViewItem item in listView_Channel.Items)
            {
                templist.Add(Convert.ToInt32(item.SubItems[0].Text), Convert.ToInt32(item.Tag));
            }
            //添加通道构造函数
            DialogCollectChannel dcc = new DialogCollectChannel(this,Convert.ToInt32(com_MaxChannel.Text), templist);
            dcc.ShowDialog(this);
            //如果添加通道字符串不为空，说明成功添加了通道。则更新ListView
            if (this.AddCollectChannelStr != "")
            {
                string[] tempArray = this.AddCollectChannelStr.Split('|');
                ListViewItem item = new ListViewItem(new string[] { tempArray[0], tempArray[1], tempArray[2] });
                //将Channel_ID放到Item的Tag里
                item.Tag = tempArray[3];
                //添加Item
                listView_Channel.Items.Add(item);
                this.AddCollectChannelStr = "";
            }
        }

        private void btn_DelChannel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (listView_Channel.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("您确定要删除 " + listView_Channel.SelectedItems[0].SubItems[0].Text + " 号通道吗？", "删除采集器通道", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    listView_Channel.Items.Remove(listView_Channel.SelectedItems[0]);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个欲删除的通道。", "删除通道", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listView_Channel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_EditChannel_LinkClicked(null, null);
        }
    }
}