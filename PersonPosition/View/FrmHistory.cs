using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SharpMap.Layers;
using SharpMap.Styles;
using SharpMap.Data;
using SharpMap.Data.Providers;

using PersonPosition.StaticService;
using PersonPosition.Common;

namespace PersonPosition.View
{
    public partial class FrmHistory : Form
    {
        private DataTable HistoryDrawLinesTable;
        private int seekDrawTable = 0;
        private bool IsReDraw = false;
        private DateTime StartTime;
        private DateTime EndTime;

        public FrmHistory()
        {
            InitializeComponent();
            
            this.Tag = this.MainPanel;

            com_StartHour.SelectedIndex = 0;
            com_StartMinute.SelectedIndex = 0;
            com_EndHour.SelectedIndex = com_EndHour.Items.Count - 1;
            com_EndMinute.SelectedIndex = com_EndMinute.Items.Count - 1;
            com_PlaySpeed.SelectedIndex = 1;

            //初始化部门列表
            com_SelectDepartment.Items.Add("所有部门");
            for (int j = 0; j < DB_Service.MainDataSet.Tables["DepartmentTable"].Rows.Count; j++)
            {
                com_SelectDepartment.Items.Add(DB_Service.MainDataSet.Tables["DepartmentTable"].Rows[j]["DepartmentName"].ToString());
            }
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
                btn_Brows_Click(null, null);
            }
        }

        #region 提供的通用服务

        /// <summary>
        /// 显示指定员工的当日历史轨迹
        /// </summary>
        /// <param name="PID"></param>
        public void ShowHistoryByPID(string PID)
        {
            btn_SearchInMine_Click(null, null);
            foreach(ListViewItem Item in listView_InMine.Items )
            {
                if (Item.SubItems[0].Text == PID)
                {
                    Item.Selected = true;
                    btn_Play_Click(null, null);
                    return;
                }
            }
            MessageBox.Show("对不起，此员工今日没有进洞活动轨迹。", "轨迹查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示指定员工的当日历史轨迹
        /// </summary>
        /// <param name="CardID"></param>
        public void ShowHistoryByCardID(int CardID)
        {
            btn_SearchInMine_Click(null, null);
            foreach (ListViewItem Item in listView_InMine.Items)
            {
                if (Convert.ToInt32(Item.SubItems[1].Text) == CardID)
                {
                    Item.Selected = true;
                    btn_Play_Click(null, null);
                    return;
                }
            }
            MessageBox.Show("对不起，此员工今日没有进洞活动轨迹。", "轨迹查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

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
                this.mapImage.Refresh();
            }
        }

        #endregion

        private void InitHistoryList()
        {
            DataGridView.Columns["ID"].HeaderText = "次序";
            DataGridView.Columns["CardID"].HeaderText = "卡号";
            DataGridView.Columns["Name"].HeaderText = "姓名";
            DataGridView.Columns["Geo_X"].HeaderText = "X坐标";
            DataGridView.Columns["Geo_Y"].HeaderText = "Y坐标";
            DataGridView.Columns["Time"].HeaderText = "时间";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(-2);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(-1);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private string GetNameByPID(string PID)
        {
            DataRow[] rows = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + PID + "'");
            if (rows.Length == 0)
            {
                throw new Exception("工号为：" + PID + " 的员工不存在！");
            }
            return rows[0]["Name"].ToString();
        }

        private string GetPIDByCardID(int CardID)
        {
            DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + CardID);
            if (rows.Length == 0)
            {
                throw new Exception("卡号为：" + CardID + " 的卡片不存在！");
            }
            else if (rows[0]["PID"] == DBNull.Value)
            {
                throw new Exception("这张卡片没有绑定任何一个员工！");
            }
            return rows[0]["PID"].ToString();
        }

        private string GetPIDByName(string Name)
        {
            DataRow[] rows = DB_Service.MainDataSet.Tables["PersonTable"].Select("Name = '" + Name + "'");
            if (rows.Length == 0)
            {
                throw new Exception("姓名为：" + Name + " 的员工不存在！");
            }
            else if (rows.Length > 1)
            {
                throw new Exception("有多个员工符合这个姓名，请用卡号或者工号查询。");
            }
            return rows[0]["PID"].ToString();
        }

        private int GetCardIDByPID(string PID)
        {
            DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + PID + "'");
            if (rows.Length == 0)
            {
                throw new Exception("工号为：" + PID + " 的员工不存在！");
            }
            else if (rows.Length > 1)
            {
                throw new Exception("这个员工绑定了多张卡片！按规定，一人只能与一张卡片唯一绑定。请在卡片管理中除去多余的卡片。");
            }
            return Convert.ToInt32(rows[0]["CardID"]);
        }

        private void btn_SearchInMine_Click(object sender, EventArgs e)
        {
            if (com_EndHour.SelectedIndex < com_StartHour.SelectedIndex || com_EndMinute.SelectedIndex < com_StartMinute.SelectedIndex)
            {
                if (MessageBox.Show("请注意，您选择的终止时间小于起始时间，您是要跨天统计吗？\n\n选择 是 将继续，选择 否 将不进行统计。", "历史轨迹管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            //首先显示所有部门的洞内人员
            RefreshInMineListView("所有部门");
        }

        private void RefreshInMineListView(string Str_Department)
        {
            text_DutyInfo.Text = "";
            text_History.Text = "";
            listView_InMine.Items.Clear();
            string date = dateTimePicker1.Value.Date.Year.ToString() + "-" + dateTimePicker1.Value.Date.Month.ToString() + "-" + dateTimePicker1.Value.Date.Day.ToString();
            this.StartTime = Convert.ToDateTime(date + " " + com_StartHour.SelectedIndex.ToString() + ":" + com_StartMinute.SelectedIndex.ToString() + ":00");
            this.EndTime = Convert.ToDateTime(date + " " + com_EndHour.SelectedIndex.ToString() + ":" + com_EndMinute.SelectedIndex.ToString() + ":59");
            //判断是否是跨天统计，如果是，则将结束时间自动加一天
            if (this.EndTime < this.StartTime)
            {
                this.EndTime = this.EndTime.AddDays(1);
            }
            using (DataTable temp_HistoryTable = DB_Service.GetTable("temp_HistoryTable", "select DISTINCT CardID from HistoryPositionTable where Time >= '" + StartTime + "' and Time <= '" + EndTime + "'"))
            {
                using (DataTable temp_DutyTable = DB_Service.GetTable("temp_DutyTable", "select * from DutyTable where InTime >='" + StartTime.AddHours(-8).ToString() + "' and InTime <='" + EndTime.ToString() + "'"))
                {
                    int TotalPeopleNum = 0;
                    foreach (DataRow row in temp_HistoryTable.Rows)
                    {
                        try
                        {
                            int CardID = Convert.ToInt32(row["CardID"]);
                            DataRow[] row_CardDuty = temp_DutyTable.Select("CardID = " + CardID);
                            if (row_CardDuty.Length > 0)
                            {
                                if (row_CardDuty[0]["InTime"] != DBNull.Value)
                                {
                                    TotalPeopleNum++;
                                    string PID = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + CardID)[0]["PID"].ToString();
                                    DataRow PersonRow = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + PID + "'")[0];
                                    if (Str_Department == "所有部门" || Str_Department == PersonRow["Department"].ToString())
                                    {
                                        string Name = PersonRow["Name"].ToString();
                                        string WorkType = PersonRow["WorkType"].ToString();
                                        string Department = PersonRow["Department"].ToString();
                                        listView_InMine.Items.Add(new ListViewItem(new string[5] { PID, CardID.ToString(), Name, WorkType, Department })); 
                                    }
                                }
                            }
                        }
                        catch
                        { }
                    }
                    TimeSpan span = EndTime.Subtract(StartTime);
                    label_InMine.Text =  "在" + this.StartTime.ToString() + "至" + this.EndTime.ToString() + "共" + Convert.ToInt32(span.TotalMinutes).ToString() + "分钟的时间内，共有" + TotalPeopleNum.ToString() + "人在洞内停留过。";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seekDrawTable < HistoryDrawLinesTable.Rows.Count)
            {
                //绘制文字轨迹信息
                panel_BackImage.Refresh();
                seekDrawTable++;
            }
            else
            {
                seekDrawTable = HistoryDrawLinesTable.Rows.Count;
                timer1.Enabled = false;
            }
        }


        private void panel_BackImage_Paint(object sender, PaintEventArgs e)
        {
            if (IsReDraw)
            {
                for (int i = 0; i < seekDrawTable; i++)
                {
                    PointF point = mapImage.Map.WorldToImage(new SharpMap.Geometries.Point(Convert.ToDouble(HistoryDrawLinesTable.Rows[i]["Geo_X"]), Convert.ToDouble(HistoryDrawLinesTable.Rows[i]["Geo_Y"])));
                    if (i == seekDrawTable - 1)
                    {
                        //最后一个点，则画小人
                        e.Graphics.DrawImage(Resource_Service.GetImage("Person_1"), point.X - 8, point.Y - 8);
                    }
                    else
                    {
                        //不是最后一个点，则画点
                        e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), point.X - 3, point.Y - 3, 6, 6); 
                    }
                    //如果有上一个点，则画直线
                    if (i > 0)
                    {
                        PointF lastpoint = mapImage.Map.WorldToImage(new SharpMap.Geometries.Point(Convert.ToDouble(HistoryDrawLinesTable.Rows[i - 1]["Geo_X"]), Convert.ToDouble(HistoryDrawLinesTable.Rows[i - 1]["Geo_Y"])));
                        e.Graphics.DrawLine(new Pen(Color.Black, 1), lastpoint, point);
                    }
                    //画序号
                    e.Graphics.DrawString(Convert.ToString((i+1)), new Font("黑体", 8), Brushes.White, new PointF(point.X - 6, point.Y - 5));
                    //画时间
                    e.Graphics.DrawString(Convert.ToDateTime(HistoryDrawLinesTable.Rows[i]["Time"]).TimeOfDay.ToString(), new Font("黑体", 8, FontStyle.Bold), Brushes.BlueViolet, new PointF(point.X - 29, point.Y + 8));
                }
            }
        }

        private void btn_Play_Click(object sender, EventArgs e)
        {
            if (listView_InMine.SelectedItems.Count < 1)
            {
                MessageBox.Show("请先在上面的列表中选中一个欲查询的员工", "历史轨迹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                //停止并清空绘画
                IsReDraw = false;
                timer1.Enabled = false;
                seekDrawTable = 0;
                HistoryDrawLinesTable = DataTableFactory_Service.MakeHistoryDrawLinesTable("HistoryDrawLinesTable");
                this.DataGridView.DataSource = null;
                int Result_CardID = Convert.ToInt32(listView_InMine.SelectedItems[0].SubItems[1].Text);
                string Result_Name = listView_InMine.SelectedItems[0].SubItems[2].Text;
                string strSQL = "select * from HistoryPositionTable where CardID = " + Result_CardID + " and Time >= '" + this.StartTime + "' and Time <= '" + this.EndTime + "' order by Time ASC";

                IsReDraw = false;

                using (DataTable tempTable = DB_Service.GetTable("tempTable", strSQL))
                {
                    if (tempTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < tempTable.Rows.Count; i++)
                        {
                            //创建新的一行
                            DataRow newRow = HistoryDrawLinesTable.NewRow();
                            //赋值
                            newRow["CardID"] = Result_CardID;
                            newRow["Name"] = Result_Name;
                            newRow["Time"] = tempTable.Rows[i]["Time"];
                            newRow["NearStationID"] = tempTable.Rows[i]["MaxStationID"];
                            newRow["Geo_X"] = tempTable.Rows[i]["Geo_X"];
                            newRow["Geo_Y"] = tempTable.Rows[i]["Geo_Y"];
                            //将新行添加到PositionTable
                            HistoryDrawLinesTable.Rows.Add(newRow);
                        }
                        this.DataGridView.DataSource = this.HistoryDrawLinesTable;
                        //初始化详细定位表的表头
                        InitHistoryList();
                        //启动绘制计时器
                        IsReDraw = true;
                        timer1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("此员工没有这个时间的活动轨迹。", "轨迹查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "历史轨迹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            seekDrawTable = 0;
        }

        private void mapImage_SizeChanged(object sender, EventArgs e)
        {
            this.mapImage.Refresh();
        }

        private void com_PlaySpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (com_PlaySpeed.SelectedIndex)
            {
                case 0:
                    timer1.Interval = 1600;
                    break;
                case 1:
                    timer1.Interval = 800;
                    break;
                case 2:
                    timer1.Interval = 300;
                    break;
            }
        }

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                mapImage.Map.Center = new SharpMap.Geometries.Point(Convert.ToDouble(DataGridView.Rows[e.RowIndex].Cells["Geo_X"].Value), Convert.ToDouble(DataGridView.Rows[e.RowIndex].Cells["Geo_Y"].Value));
                mapImage.Refresh();
            }
        }

        private void listView_InMine_DoubleClick(object sender, EventArgs e)
        {
            if (listView_InMine.SelectedItems.Count > 0)
            {
                btn_Play_Click(sender, e);
            }
        }

        private void listView_InMine_Click(object sender, EventArgs e)
        {
            if (listView_InMine.SelectedItems.Count > 0)
            {
                int CardID = Convert.ToInt32(listView_InMine.SelectedItems[0].SubItems[1].Text);
                string Name = listView_InMine.SelectedItems[0].SubItems[2].Text;
                //准备显示文字轨迹信息
                text_History.Text = "   详细历史轨迹说明\r\n";
                text_History.AppendText("员工：" + Name + "    工号：" + listView_InMine.SelectedItems[0].SubItems[0].Text + "\r\n");
                text_History.AppendText("起始时间：" + this.StartTime.ToString() + "\r\n");
                text_History.AppendText("终止时间：" + this.EndTime.ToString() + "\r\n");
                text_History.AppendText(" \r\n");
                using (DataTable tempTable1 = DB_Service.GetTable("tempTable", "select * from HistoryPositionTable where CardID = " + CardID + " and Time >= '" + this.StartTime + "' and Time <= '" + this.EndTime + "' order by ID ASC"))
                {
                    int MaxStationID = -1;
                    for (int i = 0; i < tempTable1.Rows.Count; i++)
                    {
                        if (MaxStationID != Convert.ToInt32(tempTable1.Rows[i]["MaxStationID"]))
                        {
                            MaxStationID = Convert.ToInt32(tempTable1.Rows[i]["MaxStationID"]);
                            text_History.AppendText(Convert.ToString(text_History.Lines.Length - 5) + ". 进入【" + MaxStationID.ToString() + "】号基站,于" + Convert.ToDateTime(tempTable1.Rows[i]["Time"]).TimeOfDay.ToString() + "\r\n");
                        }
                    }
                }
                //显示考勤信息
                text_DutyInfo.Text = "  考勤概要\n";
                text_DutyInfo.AppendText(" \n");
                DataTable tempTable = DB_Service.GetTable("tempTable", "select * from DutyTable where CardID = " + CardID + " and InTime >='" + StartTime.ToString() + "' and InTime <='" + EndTime.ToString() + "'");
                if(tempTable.Rows.Count>0)
                {
                    for (int i = 0; i < tempTable.Rows.Count; i++)
                    {
                        string InTimeStr = "空";
                        string OutTimeStr = "空";
                        if (tempTable.Rows[i]["InTime"] != DBNull.Value)
                            InTimeStr = Convert.ToDateTime(tempTable.Rows[i]["InTime"]).TimeOfDay.ToString();
                        if (tempTable.Rows[i]["OutTime"] != DBNull.Value)
                            OutTimeStr = Convert.ToDateTime(tempTable.Rows[i]["OutTime"]).TimeOfDay.ToString();
                        text_DutyInfo.AppendText("-第" + Convert.ToString(i + 1) + "次-\r\n进入:" + InTimeStr + "\r\n离开:" + OutTimeStr + "\r\n\r\n");
                    }
                }
                else
                {
                    text_DutyInfo.AppendText("对不起，在您选定的时间段内，这个人员没有考勤记录。");
                }
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                FileStream FS = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate,FileAccess.Write);
                StreamWriter SW = new StreamWriter(FS);
                SW.Flush();
                SW.Write(text_History.Text);
                SW.Close();
                FS.Close();
            }
        }

        private void checkBox_ShowMore_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ShowMore.Checked)
            {
                groupBox3.Width = 506;
            }
            else
            {
                groupBox3.Width = 176;
            }
            group_HistoryMap.Left = groupBox3.Left + groupBox3.Width + 6;
            group_HistoryMap.Width = this.MainPanel.Width - group_HistoryMap.Left - 10;
        }

        private void com_SelectDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_SelectDepartment.SelectedIndex == 0)
            {
                RefreshInMineListView("所有部门");
            }
            else
            {
                RefreshInMineListView(com_SelectDepartment.Text);
            }
        }
    }
}