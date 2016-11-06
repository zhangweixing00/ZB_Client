using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PersonPosition.Model;
using PersonPosition.Common;
using PersonPosition.StaticService;

namespace PersonPosition.View
{
    public partial class FrmCollect : Form
    {
        private MainForm mainform;

        #region 提供的通用服务

        /// <summary>
        /// 显示指定采集器的信息
        /// </summary>
        /// <param name="StationID"></param>
        public void ShowCollectChannelByStationID(int StationID)
        {
            DataRow[] rows_Station = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID);
            if (rows_Station.Length > 0)
            {
                if (rows_Station[0]["StationFunction"].ToString() == "信息采集")
                {
                    label_CollectID.Text = StationID.ToString();
                    label_CollectName.Text = rows_Station[0]["Name"].ToString();
                    int maxChannelNum = Convert.ToInt32(rows_Station[0]["MaxChannelNum"]);
                    int useChannel = DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Select("StationID = " + StationID).Length;
                    label_CollectChannelInfo.Text = "共有 " + maxChannelNum.ToString() + " 个通道。其中有 " + useChannel.ToString() + " 个通道已经在使用中，剩余 " + (maxChannelNum - useChannel) + " 个通道尚未使用。";
                    DB_Service.MainDataSet.Tables["CollectChannelValueTable"].DefaultView.RowFilter = "StationID = " + StationID;
                }
                else
                {
                    MessageBox.Show("对不起，您欲查看的这个基站不是采集器基站。故无法查看其采集器信息。", "采集器信息查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("对不起，您欲查看的采集器基站不存在。请核实后再试。", "采集器信息查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        public FrmCollect(MainForm _mainform)
        {
            InitializeComponent();
            this.Tag = this.MainPanel;
            this.mainform = _mainform;

            DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("StationFunction = '信息采集'");

            listView_CollectList.Items.Add("所有采集器");
            for (int i = 0; i < rows.Length; i++)
            {
                listView_CollectList.Items.Add(new ListViewItem(new string[] { rows[i]["ID"].ToString(), rows[i]["Name"].ToString() }));
            }

            DataGrid_CollectChannel.DataSource = DB_Service.MainDataSet.Tables["CollectChannelValueTable"];
            DataGrid_CollectChannel.Columns["StationID"].HeaderText = "采集器基站编号";
            DataGrid_CollectChannel.Columns["StationID"].Width = 120;
            DataGrid_CollectChannel.Columns["StationName"].HeaderText = "采集器基站名称";
            DataGrid_CollectChannel.Columns["StationName"].Width = 120;
            DataGrid_CollectChannel.Columns["Channel_ID"].HeaderText = "通道编号";
            DataGrid_CollectChannel.Columns["Channel_ID"].Width = 120;
            DataGrid_CollectChannel.Columns["ChannelNum"].HeaderText = "通道序号";
            DataGrid_CollectChannel.Columns["ChannelName"].HeaderText = "通道名称";
            DataGrid_CollectChannel.Columns["ChannelName"].Width = 120;
            DataGrid_CollectChannel.Columns["ChannelComment"].HeaderText = "通道描述";
            DataGrid_CollectChannel.Columns["ChannelComment"].Width = 120;
            DataGrid_CollectChannel.Columns["ChannelValueStr"].HeaderText = "通道值";
            DataGrid_CollectChannel.Columns["IsOverValue"].HeaderText = "是否超出警戒值";
            DataGrid_CollectChannel.Columns["IsOverValue"].Width = 120;
            DataGrid_CollectChannel.Columns["LastUpdateTime"].HeaderText = "时间";

            //设置默认显示全部。此时会触发listView_CollectList_SelectedIndexChanged
            listView_CollectList.Items[0].Selected = true;

            //初始化考勤统计的时间段是今天
            linkLabel3_LinkClicked(null, null);
            //水晶报表控件的Bug，需要每次强制显示工具条和状态条
            DutyReportView.DisplayToolbar = true;
            DutyReportView.DisplayStatusBar = true;
            AnalysicsReportView.DisplayToolbar = true;
            AnalysicsReportView.DisplayStatusBar = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    //初始化采集器combox
                    com_CollectStation.Items.Clear();
                    try
                    {
                        foreach (DataRow row in DB_Service.MainDataSet.Tables["StationTable"].Select("StationFunction = '信息采集'"))
                        {
                            com_CollectStation.Items.Add(row["ID"] + "：" + row["Name"]);
                        }
                    }
                    catch
                    { }
                    if (com_CollectStation.Items.Count > 0)
                        com_CollectStation.SelectedIndex = 0;
                    break;
                case 2:
                    //初始化采集器combox
                    com_CollectStation_1.Items.Clear();
                    try
                    {
                        foreach (DataRow row in DB_Service.MainDataSet.Tables["StationTable"].Select("StationFunction = '信息采集'"))
                        {
                            com_CollectStation_1.Items.Add(row["ID"] + "：" + row["Name"]);
                        }
                    }
                    catch
                    { }
                    if (com_CollectStation_1.Items.Count > 0)
                        com_CollectStation_1.SelectedIndex = 0;
                    break;
            }
        }

        #region 实时采集信息

        private void listView_CollectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_CollectList.SelectedItems.Count > 0)
            {
                if (listView_CollectList.SelectedItems[0].Index == 0)
                {
                    //全部采集器
                    label_CollectID.Text = "全部采集器基站";
                    label_CollectName.Text = "-";
                    label_CollectChannelInfo.Text = "-";
                    DB_Service.MainDataSet.Tables["CollectChannelValueTable"].DefaultView.RowFilter = "";
                }
                else
                {
                    int stationID = Convert.ToInt32(listView_CollectList.SelectedItems[0].SubItems[0].Text);
                    //显示指定采集器基站信息
                    ShowCollectChannelByStationID(stationID);
                }
            }
        }

        private void btn_GoToMap_Click(object sender, EventArgs e)
        {
            if (DataGrid_CollectChannel.SelectedRows.Count > 0)
            {
                DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + DataGrid_CollectChannel.SelectedRows[0].Cells["StationID"].Value);
                if (rows.Length > 0)
                {
                    this.mainform.MainBtn_Watch_Click(null, null);
                    this.mainform.ShowStationToolTipInMap(rows[0]["Name"].ToString(), Convert.ToInt32(rows[0]["ID"]), rows[0]["StationType"].ToString(), rows[0]["StationFunction"].ToString(), Convert.ToDouble(rows[0]["Geo_X"]), Convert.ToDouble(rows[0]["Geo_Y"]));
                }
                else
                {
                    MessageBox.Show("这个采集器基站不存在！所以您无法在地图中定位。", "采集器基站信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请先在下表中选择一条采集器通道信息。", "采集器基站信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Info_Click(object sender, EventArgs e)
        {
            if (this.DataGrid_CollectChannel.SelectedRows.Count > 0)
            {
                try
                {
                    this.mainform.MainBtn_Machine_Click(null, null);
                    this.mainform.frmMachine.ShowStationInfoByID(Convert.ToInt32(this.DataGrid_CollectChannel.SelectedRows[0].Cells["StationID"].Value));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("请先在下表中选择一个您要查看的采集器基站。", "详细信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataGrid_CollectChannel_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        #endregion

        #region 历史采集信息

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (com_CollectStation.Items.Count == 0 || com_CollectStation.SelectedIndex == -1)
            {
                MessageBox.Show("请您先选择一个欲统计的采集器。", "历史采集信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                //采集器基站ID
                int CollectStationID = Convert.ToInt32(com_CollectStation.Text.Split('：')[0]);
                //采集器基站名称
                string CollectStationName = com_CollectStation.Text.Split('：')[1];
                //采集器通道序号与通道ID字典(通过基站字符构建的)
                Dictionary<int, int> ChannelTable_Station = new Dictionary<int, int>();

                DataRow[] row_Station = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + CollectStationID);
                if (row_Station.Length == 0)
                {
                    MessageBox.Show("对不起，您选择的这个采集器不存在。请重新选择。", "历史采集信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string ChannelStrs = row_Station[0]["CollectChannelIDStr"].ToString();
                    //从基站表的ChannelStrs中解析出通道数据 放到ChannelTable_Station中
                    if (ChannelStrs == "")
                    {
                        //采集通道字符为空
                        MessageBox.Show("对不起，您选择的这个采集器尚未使用任何通道。请重新选择。", "历史采集信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        string[] tempChannel_ID_List = ChannelStrs.Split('-');
                        for (int k = 0; k < tempChannel_ID_List.Length; k++)
                        {
                            if (tempChannel_ID_List[k] != "")
                            {
                                string[] tempFinally = tempChannel_ID_List[k].Split(':');
                                if (tempFinally.Length == 2)
                                {
                                    ChannelTable_Station.Add(Convert.ToInt32(tempFinally[0]), Convert.ToInt32(tempFinally[1]));
                                }
                            }
                        }
                    }
                }
                ////////至此，ChannelTable_Station中已经存放了从基站表中形成的通道信息///////
                //日统计
                DateTime StartTime = dateTimePicker1.Value.Date;
                DateTime EndTime = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59);
                string ReportTimeStr = "数据采集日期：" + dateTimePicker1.Value.Year + "年" + dateTimePicker1.Value.Month + "月" + dateTimePicker1.Value.Day + "日";
                //创建一个统计采集信息报表对象
                ReportStatistic_Collect DutyReport = new ReportStatistic_Collect(Global.MapName + CollectStationName + "数据采集报表", "采集器编号：" + CollectStationID.ToString() + "                                  采集器名称：" + CollectStationName, ReportTimeStr);
                //从历史表中取数据
                string sqlTemp = "select * from HistoryCollectTable where Station_ID = " + CollectStationID + " and Time >= '" + StartTime + "' and Time <= '" + EndTime + "' order by Time";
                DataTable tempTable = DB_Service.GetTable("tempTable", sqlTemp);
                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    DataRow row = tempTable.Rows[i];
                    int ChannelNum = Convert.ToInt32(row["ChannelNum"]);
                    int Channel_ID = Convert.ToInt32(row["Channel_ID"]);
                    //判断这个通道合法性：是否在采集器表中的字符串中存在 + 通道号是否一样
                    if (ChannelTable_Station.ContainsKey(ChannelNum) && Channel_ID == ChannelTable_Station[ChannelNum])
                    {
                        //判断这个通道合法性：通道记录是否存在
                        DataRow[] ChannelInfo = DB_Service.MainDataSet.Tables["CollectChannelTable"].Select("Channel_ID = " + Channel_ID);
                        if (ChannelInfo.Length > 0)
                        {
                            //至此 通道合法 如果同一个通道下同一个时间段内的记录已经有了。则不添加新的记录
                            double CollectValue = Convert.ToDouble(row["ChannelValue"]);
                            //通过K、C计算出最终的值
                            if (ChannelInfo[0]["ChannelPer_K"] != DBNull.Value)
                            {
                                CollectValue = CollectValue * Convert.ToDouble(ChannelInfo[0]["ChannelPer_K"]);
                            }
                            if (ChannelInfo[0]["ChannelPer_C"] != DBNull.Value)
                            {
                                CollectValue = CollectValue + Convert.ToDouble(ChannelInfo[0]["ChannelPer_C"]);
                            }
                            if (CollectValue < 0)
                                CollectValue = 0;
                            DateTime Time = Convert.ToDateTime(row["Time"]);
                            //得到这个Time所属的时间段字串
                            string TimeStr = Time.Hour.ToString() + ":00 ～ " + Time.Hour.ToString() + ":59";
                            DataRow[] ExistRows = DutyReport.DataSetReport.StatisticCollectTable.Select("ChannelNum = " + ChannelNum + " and Time = '" + TimeStr + "'");
                            if (ExistRows.Length > 0)
                            {
                                //已经存在那个时间段的记录。则直接修改老的
                                ExistRows[0]["ChannelValue_" + GetTimeIndex(Time.Minute)] = CollectValue;
                            }
                            else
                            {
                                //不存在那个时间段的记录。添加新的记录
                                DataRow NewRow = DutyReport.DataSetReport.StatisticCollectTable.NewRow();
                                NewRow["ChannelNum"] = ChannelNum;
                                NewRow["ChannelName"] = ChannelInfo[0]["ChannelName"];
                                NewRow["ChannelType"] = ChannelInfo[0]["ChannelType"];
                                NewRow["ChannelComment"] = ChannelInfo[0]["ChannelComment"];
                                NewRow["ChannelUnit"] = ChannelInfo[0]["ChannelUnit"];
                                NewRow["Time"] = TimeStr;
                                NewRow["ChannelValue_" + GetTimeIndex(Time.Minute)] = CollectValue;

                                //添加新行
                                DutyReport.DataSetReport.StatisticCollectTable.Rows.Add(NewRow);
                            }
                        }
                    }
                }
                //将考勤报表对象的参数域传给报表控件
                DutyReportView.ParameterFieldInfo = DutyReport.PFields;
                //将考勤报表对象的报表传给报表控件
                DutyReportView.ReportSource = DutyReport.Report;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetTimeIndex(int Minute)
        {
            if (Minute >= 0 && Minute <= 4)
            {
                return 1;
            }
            else if (Minute >= 5 && Minute <= 9)
            {
                return 2;
            }
            else if (Minute >= 10 && Minute <= 14)
            {
                return 3;
            }
            else if (Minute >= 15 && Minute <= 19)
            {
                return 4;
            }
            else if (Minute >= 20 && Minute <= 24)
            {
                return 5;
            }
            else if (Minute >= 25 && Minute <= 29)
            {
                return 6;
            }
            else if (Minute >= 30 && Minute <= 34)
            {
                return 7;
            }
            else if (Minute >= 35 && Minute <= 39)
            {
                return 8;
            }
            else if (Minute >= 40 && Minute <= 44)
            {
                return 9;
            }
            else if (Minute >= 45 && Minute <= 49)
            {
                return 10;
            }
            else if (Minute >= 50 && Minute <= 54)
            {
                return 11;
            }
            else
            {
                return 12;
            }
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

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DutyReportView.PrintReport();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            DutyReportView.ExportReport();
        }

        #endregion

        #region 采集信息分析

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now.AddDays(-2);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now.AddDays(-1);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now;
        }

        private void btn_Analysics_Click(object sender, EventArgs e)
        {
            if (com_CollectStation_1.Items.Count == 0 || com_CollectStation_1.SelectedIndex == -1)
            {
                MessageBox.Show("请您先选择一个欲分析的采集器。", "采集信息分析", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                //采集器基站ID
                int CollectStationID = Convert.ToInt32(com_CollectStation_1.Text.Split('：')[0]);
                //采集器基站名称
                string CollectStationName = com_CollectStation_1.Text.Split('：')[1];
                //采集器通道序号与通道ID字典(通过基站字符构建的)
                Dictionary<int, int> ChannelTable_Station = new Dictionary<int, int>();

                DataRow[] row_Station = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + CollectStationID);
                if (row_Station.Length == 0)
                {
                    MessageBox.Show("对不起，您选择的这个采集器不存在。请重新选择。", "采集信息分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string ChannelStrs = row_Station[0]["CollectChannelIDStr"].ToString();
                    //从基站表的ChannelStrs中解析出通道数据 放到ChannelTable_Station中
                    if (ChannelStrs == "")
                    {
                        //采集通道字符为空
                        MessageBox.Show("对不起，您选择的这个采集器尚未使用任何通道。请重新选择。", "采集信息分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        string[] tempChannel_ID_List = ChannelStrs.Split('-');
                        for (int k = 0; k < tempChannel_ID_List.Length; k++)
                        {
                            if (tempChannel_ID_List[k] != "")
                            {
                                string[] tempFinally = tempChannel_ID_List[k].Split(':');
                                if (tempFinally.Length == 2)
                                {
                                    ChannelTable_Station.Add(Convert.ToInt32(tempFinally[0]), Convert.ToInt32(tempFinally[1]));
                                }
                            }
                        }
                    }
                }
                ////////至此，ChannelTable_Station中已经存放了从基站表中形成的通道信息///////
                //日统计
                DateTime StartTime = dateTimePicker2.Value.Date;
                DateTime EndTime = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 23, 59, 59);
                string ReportTimeStr = "数据采集日期：" + dateTimePicker2.Value.Year + "年" + dateTimePicker2.Value.Month + "月" + dateTimePicker2.Value.Day + "日";
                //创建一个分析采集信息报表对象
                ReportAnalysics_Collect CollectAnalysicsReport = new ReportAnalysics_Collect(Global.MapName + CollectStationName + "数据采集报表", "采集器编号：" + CollectStationID.ToString() + "                  采集器名称：" + CollectStationName, ReportTimeStr);
                //从历史表中取数据
                string sqlTemp = "select * from HistoryCollectTable where Station_ID = " + CollectStationID + " and Time >= '" + StartTime + "' and Time <= '" + EndTime + "' order by ChannelNum";
                DataTable tempTable = DB_Service.GetTable("tempTable", sqlTemp);
                //确保按通道为单位取数据的操作指针
                int seek = 0;
                while (seek < tempTable.Rows.Count)
                {
                    DataRow row = tempTable.Rows[seek];
                    int ChannelNum = Convert.ToInt32(row["ChannelNum"]);
                    int Channel_ID = Convert.ToInt32(row["Channel_ID"]);
                    DataRow[] SameChannelNumRows = tempTable.Select("ChannelNum = " + ChannelNum);
                    //移动指针
                    seek += SameChannelNumRows.Length;
                    //判断这个通道合法性：是否在采集器表中的字符串中存在 + 通道号是否一样
                    if (ChannelTable_Station.ContainsKey(ChannelNum) && Channel_ID == ChannelTable_Station[ChannelNum])
                    {
                        //判断这个通道合法性：通道记录是否存在
                        DataRow[] ChannelInfo = DB_Service.MainDataSet.Tables["CollectChannelTable"].Select("Channel_ID = " + Channel_ID);
                        if (ChannelInfo.Length > 0)
                        {
                            //至此 通道合法 
                            //得到K、C
                            double Per_K = 1;
                            double Per_C = 0;
                            if (ChannelInfo[0]["ChannelPer_K"] != DBNull.Value)
                                Per_K = Convert.ToDouble(ChannelInfo[0]["ChannelPer_K"]);
                            if (ChannelInfo[0]["ChannelPer_C"] != DBNull.Value)
                                Per_C = Convert.ToDouble(ChannelInfo[0]["ChannelPer_C"]);
                            //计算这个通道下的指定时间段内的数据
                            for (int i = 0; i < 24; i++)
                            {
                                DataRow NewRow = CollectAnalysicsReport.DataSetReport.AnalysicsTable_Collect.NewRow();
                                NewRow["ChannelNum"] = ChannelNum;
                                NewRow["ChannelName"] = ChannelInfo[0]["ChannelName"];
                                NewRow["ChannelType"] = ChannelInfo[0]["ChannelType"];
                                NewRow["ChannelComment"] = ChannelInfo[0]["ChannelComment"];
                                NewRow["ChannelUnit"] = ChannelInfo[0]["ChannelUnit"];
                                NewRow["Time"] = i.ToString();
                                //算这个时间段内的平均值
                                int times = 0;
                                double total = 0;
                                for (int j = 0; j < SameChannelNumRows.Length; j++)
                                {
                                    DateTime tempTime = Convert.ToDateTime(SameChannelNumRows[j]["Time"]);
                                    if (tempTime.Hour == i)
                                    {
                                        times++;
                                        //通过K、C计算出最终的值
                                        double CollectValue = Convert.ToDouble(SameChannelNumRows[j]["ChannelValue"]);
                                        CollectValue = CollectValue * Per_K;
                                        CollectValue = CollectValue + Per_C;
                                        if (CollectValue < 0)
                                            CollectValue = 0;
                                        total += CollectValue;
                                    }
                                }
                                if (total != 0)
                                {
                                    NewRow["Value"] = total / times;
                                }
                                //添加新行
                                CollectAnalysicsReport.DataSetReport.AnalysicsTable_Collect.Rows.Add(NewRow);
                            }
                        }
                    }
                }
                //将考勤报表对象的参数域传给报表控件
                AnalysicsReportView.ParameterFieldInfo = CollectAnalysicsReport.PFields;
                //将考勤报表对象的报表传给报表控件
                AnalysicsReportView.ReportSource = CollectAnalysicsReport.Report;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnalysicsReportView.PrintReport();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnalysicsReportView.ExportReport();
        }

        #endregion
    }
}