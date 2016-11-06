using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PersonPosition.Common;
using PersonPosition.StaticService;
using PersonPosition.Model;

namespace PersonPosition.View
{
    public partial class FrmAlarmArea : Form
    {
        private DataRow SpecalRow = null;
        private MainForm mainform;

        public FrmAlarmArea(MainForm _mainform)
        {
            InitializeComponent();
            this.Tag = this.MainPanel;
            this.mainform = _mainform;

            InitAllCombox();
            RefreshDataTable();
        }

        private void RefreshDataTable()
        {
            string strSQL_Specal = "select Name as 方案名称,Time as 创建时间 from SpecalTable";
            dataGridView_Specal.DataSource = DB_Service.GetTable("SpecalTable", strSQL_Specal);

            string strSQL_InArea = "select Alarm_ID as 序号,InAreaName as 监控方案名称,StationID as 基站编号,StationTable.Name as 基站名称,AlarmInAreaTable.CardID as 卡号,PersonTable.Name as 姓名,WorkType as 职务,Department as 部门,Time as 报警时间 from AlarmInAreaTable,CardTable,PersonTable,StationTable where AlarmInAreaTable.CardID = CardTable.CardID and CardTable.PID = PersonTable.PID and AlarmInAreaTable.StationID = StationTable.ID order by Alarm_ID ASC";
            DataGrid_InArea.DataSource = DB_Service.GetTable("AlarmInAreaTable", strSQL_InArea);
            DataGrid_InArea.Columns["序号"].Width = 80;
            DataGrid_InArea.Columns["监控方案名称"].Width = 150;
            DataGrid_InArea.Columns["基站编号"].Width = 80;
            DataGrid_InArea.Columns["基站名称"].Width = 140;
            DataGrid_InArea.Columns["卡号"].Width = 80;
            DataGrid_InArea.Columns["职务"].Width = 120;
            DataGrid_InArea.Columns["部门"].Width = 120;
            DataGrid_InArea.Columns["报警时间"].Width = 150;
        }

        private void btn_AddStep_Click(object sender, EventArgs e)
        {
            if (radio_HandType.Checked)
            {
                if (!panel_HandStep2.Visible)
                {
                    panel_HandStep2.Visible = true;
                    //初始化panel_HandStep2上所有控件为默认空显示
                    com_HandStation2.SelectedIndex = -1;
                    text_PeopleNumHand2.Text = "0";
                }
                else if (!panel_HandStep3.Visible)
                {
                    panel_HandStep3.Visible = true;
                    //初始化panel_Step3上所有控件为默认空显示
                    com_HandStation3.SelectedIndex = -1;
                    text_PeopleNumHand3.Text = "0";
                }
                else if (!panel_HandStep4.Visible)
                {
                    panel_HandStep4.Visible = true;
                    //初始化panel_Step4上所有控件为默认空显示
                    com_HandStation4.SelectedIndex = -1;
                    text_PeopleNumHand4.Text = "0";
                }
                else if (!panel_HandStep5.Visible)
                {
                    panel_HandStep5.Visible = true;
                    //初始化panel_Step5上所有控件为默认空显示
                    com_HandStation5.SelectedIndex = -1;
                    text_PeopleNumHand5.Text = "0";
                }
                else if (!panel_HandStep6.Visible)
                {
                    panel_HandStep6.Visible = true;
                    //初始化panel_Step6上所有控件为默认空显示
                    com_HandStation6.SelectedIndex = -1;
                    text_PeopleNumHand6.Text = "0";
                }
            }
            else
            {
                if (!panel_Step2.Visible)
                {
                    panel_Step2.Visible = true;
                    //初始化panel_Step2上所有控件为默认空显示
                    com_StartHour2.SelectedIndex = 0;
                    com_StartMinute2.SelectedIndex = 0;
                    com_EndHour2.SelectedIndex = 0;
                    com_EndMinute2.SelectedIndex = 0;
                    com_Station2.SelectedIndex = -1;
                    text_PeopleNum2.Text = "0";
                }
                else if (!panel_Step3.Visible)
                {
                    panel_Step3.Visible = true;
                    //初始化panel_Step3上所有控件为默认空显示
                    com_StartHour3.SelectedIndex = 0;
                    com_StartMinute3.SelectedIndex = 0;
                    com_EndHour3.SelectedIndex = 0;
                    com_EndMinute3.SelectedIndex = 0;
                    com_Station3.SelectedIndex = -1;
                    text_PeopleNum3.Text = "0";
                }
                else if (!panel_Step4.Visible)
                {
                    panel_Step4.Visible = true;
                    //初始化panel_Step4上所有控件为默认空显示
                    com_StartHour4.SelectedIndex = 0;
                    com_StartMinute4.SelectedIndex = 0;
                    com_EndHour4.SelectedIndex = 0;
                    com_EndMinute4.SelectedIndex = 0;
                    com_Station4.SelectedIndex = -1;
                    text_PeopleNum4.Text = "0";
                }
                else if (!panel_Step5.Visible)
                {
                    panel_Step5.Visible = true;
                    //初始化panel_Step5上所有控件为默认空显示
                    com_StartHour5.SelectedIndex = 0;
                    com_StartMinute5.SelectedIndex = 0;
                    com_EndHour5.SelectedIndex = 0;
                    com_EndMinute5.SelectedIndex = 0;
                    com_Station5.SelectedIndex = -1;
                    text_PeopleNum5.Text = "0";
                }
                else if (!panel_Step6.Visible)
                {
                    panel_Step6.Visible = true;
                    //初始化panel_Step6上所有控件为默认空显示
                    com_StartHour6.SelectedIndex = 0;
                    com_StartMinute6.SelectedIndex = 0;
                    com_EndHour6.SelectedIndex = 0;
                    com_EndMinute6.SelectedIndex = 0;
                    com_Station6.SelectedIndex = -1;
                    text_PeopleNum6.Text = "0";
                }
            }
        }

        private void btn_SubStep_Click(object sender, EventArgs e)
        {
            if (radio_HandType.Checked)
            {
                if (panel_HandStep6.Visible)
                {
                    panel_HandStep6.Visible = false;
                }
                else if (panel_HandStep5.Visible)
                {
                    panel_HandStep5.Visible = false;
                }
                else if (panel_HandStep4.Visible)
                {
                    panel_HandStep4.Visible = false;
                }
                else if (panel_HandStep3.Visible)
                {
                    panel_HandStep3.Visible = false;
                }
                else if (panel_HandStep2.Visible)
                {
                    panel_HandStep2.Visible = false;
                } 
            }
            else
            {
                if (panel_Step6.Visible)
                {
                    panel_Step6.Visible = false;
                }
                else if (panel_Step5.Visible)
                {
                    panel_Step5.Visible = false;
                }
                else if (panel_Step4.Visible)
                {
                    panel_Step4.Visible = false;
                }
                else if (panel_Step3.Visible)
                {
                    panel_Step3.Visible = false;
                }
                else if (panel_Step2.Visible)
                {
                    panel_Step2.Visible = false;
                } 
            }
        }

        private void InitAllCombox()
        {
            com_Station1.Items.Clear();
            com_Station2.Items.Clear();
            com_Station3.Items.Clear();
            com_Station4.Items.Clear();
            com_Station5.Items.Clear();
            com_Station6.Items.Clear();
            com_HandStation1.Items.Clear();
            com_HandStation2.Items.Clear();
            com_HandStation3.Items.Clear();
            com_HandStation4.Items.Clear();
            com_HandStation5.Items.Clear();
            com_HandStation6.Items.Clear();
            foreach (DataRow row in DB_Service.MainDataSet.Tables["StationTable"].Select("StationFunction = '人员定位'"))
            {
                com_Station1.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_Station2.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_Station3.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_Station4.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_Station5.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_Station6.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_HandStation1.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_HandStation2.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_HandStation3.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_HandStation4.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_HandStation5.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
                com_HandStation6.Items.Add("编号：" + row["ID"].ToString() + "，名称：" + row["Name"].ToString());
            }
            //刷新音频列表
            com_ProjectRing.Items.Clear();
            string[] WAVFiles = Directory.GetFiles(Global.AudioPath, "*.wav");
            for (int i = 0; i < WAVFiles.Length; i++)
            {
                string[] temp = WAVFiles[i].Split('\\');
                if (temp.Length > 1)
                {
                    com_ProjectRing.Items.Add(temp[temp.Length - 1]);
                }
            }
        }

        private void dataGridView_Specal_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowSpecalInfoBySpecalName(this.dataGridView_Specal.Rows[e.RowIndex].Cells["方案名称"].Value.ToString());
            }
        }

        private void ShowSpecalInfoBySpecalName(string SpecalName)
        {
            panel_Edit.Enabled = false;
            tex_ProjectName.ReadOnly = true;
            DataTable SpecalTable = DB_Service.GetTable("SpecalTable", "select * from SpecalTable");
            DataRow[] temp = SpecalTable.Select("Name = '" + SpecalName + "'");
            if (temp.Length > 0)
            {
                panel_Step2.Visible = false;
                panel_Step3.Visible = false;
                panel_Step4.Visible = false;
                panel_Step5.Visible = false;
                panel_Step6.Visible = false;
                panel_HandStep2.Visible = false;
                panel_HandStep3.Visible = false;
                panel_HandStep4.Visible = false;
                panel_HandStep5.Visible = false;
                panel_HandStep6.Visible = false;
                //初始化详细人员信息
                SpecalRow = temp[0];
                //初始化一个方案中的总体信息显示
                tex_ProjectName.Text = SpecalRow["Name"].ToString();
                if (Convert.ToBoolean(SpecalRow["IsTimeSpan"]))
                {
                    radio_TimeType.Checked = true;
                }
                else
                {
                    radio_HandType.Checked = true;
                }
                if (SpecalRow["Time"].ToString() != "")
                {
                    label_ProjectTime.Text = SpecalRow["Time"].ToString();
                }
                else
                {
                    label_ProjectTime.Text = "";
                }
                if (SpecalRow["Alarm"].ToString() != "")
                {
                    com_ProjectRing.Text = SpecalRow["Alarm"].ToString();
                }
                else
                {
                    com_ProjectRing.SelectedIndex = -1;
                }
                //初始化每个项目
                if (Convert.ToBoolean(SpecalRow["IsTimeSpan"]))
                {
                    //时间段模式
                    //初始化第一步的信息显示
                    string[] TimeStart1s = SpecalRow["TimeStart1"].ToString().Split(':');
                    if (TimeStart1s.Length > 1)
                    {
                        com_StartHour1.Text = TimeStart1s[0] + " 时";
                        com_StartMinute1.Text = TimeStart1s[1] + " 分";
                    }
                    else
                    {
                        com_StartHour1.SelectedIndex = -1;
                        com_StartMinute1.SelectedIndex = -1;
                    }
                    string[] TimeEnd1s = SpecalRow["TimeEnd1"].ToString().Split(':');
                    if (TimeEnd1s.Length > 1)
                    {
                        com_EndHour1.Text = TimeEnd1s[0] + " 时";
                        com_EndMinute1.Text = TimeEnd1s[1] + " 分";
                    }
                    else
                    {
                        com_EndHour1.SelectedIndex = -1;
                        com_EndMinute1.SelectedIndex = -1;
                    }
                    if (SpecalRow["StationID1"] != DBNull.Value)
                    {
                        int StationID1 = Convert.ToInt32(SpecalRow["StationID1"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID1);
                        if (rows.Length > 0)
                        {
                            com_Station1.Text = "编号：" + StationID1 + "，名称：" + rows[0]["Name"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_Station1.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        com_Station1.SelectedIndex = -1;
                    }
                    if (SpecalRow["AllowMaxPeople1"] != DBNull.Value)
                    {
                        text_PeopleNum1.Text = SpecalRow["AllowMaxPeople1"].ToString();
                    }
                    else
                    {
                        text_PeopleNum1.Text = "0";
                    }
                    //初始化第二步的信息显示
                    string[] TimeStart2s = SpecalRow["TimeStart2"].ToString().Split(':');
                    if (TimeStart2s.Length > 1)
                    {
                        panel_Step2.Visible = true;
                        com_StartHour2.Text = TimeStart2s[0] + " 时";
                        com_StartMinute2.Text = TimeStart2s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    string[] TimeEnd2s = SpecalRow["TimeEnd2"].ToString().Split(':');
                    if (TimeEnd2s.Length > 1)
                    {
                        com_EndHour2.Text = TimeEnd2s[0] + " 时";
                        com_EndMinute2.Text = TimeEnd2s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["StationID2"] != DBNull.Value)
                    {
                        int StationID2 = Convert.ToInt32(SpecalRow["StationID2"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID2);
                        if (rows.Length > 0)
                        {
                            com_Station2.Text = "编号：" + StationID2 + "，名称：" + rows[0]["Name"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_Station2.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople2"] != DBNull.Value)
                    {
                        text_PeopleNum2.Text = SpecalRow["AllowMaxPeople2"].ToString();
                    }
                    else
                    {
                        return;
                    }
                    //初始化第三步的信息显示
                    string[] TimeStart3s = SpecalRow["TimeStart3"].ToString().Split(':');
                    if (TimeStart3s.Length > 1)
                    {
                        panel_Step3.Visible = true;
                        com_StartHour3.Text = TimeStart3s[0] + " 时";
                        com_StartMinute3.Text = TimeStart3s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    string[] TimeEnd3s = SpecalRow["TimeEnd3"].ToString().Split(':');
                    if (TimeEnd3s.Length > 1)
                    {
                        com_EndHour3.Text = TimeEnd3s[0] + " 时";
                        com_EndMinute3.Text = TimeEnd3s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["StationID3"] != DBNull.Value)
                    {
                        int StationID3 = Convert.ToInt32(SpecalRow["StationID3"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID3);
                        if (rows.Length > 0)
                        {
                            com_Station3.Text = "编号：" + StationID3 + "，名称：" + rows[0]["Name"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_Station3.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople3"] != DBNull.Value)
                    {
                        text_PeopleNum3.Text = SpecalRow["AllowMaxPeople3"].ToString();
                    }
                    else
                    {
                        return;
                    }
                    //初始化第四步的信息显示
                    string[] TimeStart4s = SpecalRow["TimeStart4"].ToString().Split(':');
                    if (TimeStart4s.Length > 1)
                    {
                        panel_Step4.Visible = true;
                        com_StartHour4.Text = TimeStart4s[0] + " 时";
                        com_StartMinute4.Text = TimeStart4s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    string[] TimeEnd4s = SpecalRow["TimeEnd4"].ToString().Split(':');
                    if (TimeEnd4s.Length > 1)
                    {
                        com_EndHour4.Text = TimeEnd4s[0] + " 时";
                        com_EndMinute4.Text = TimeEnd4s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["StationID4"] != DBNull.Value)
                    {
                        int StationID4 = Convert.ToInt32(SpecalRow["StationID4"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID4);
                        if (rows.Length > 0)
                        {
                            com_Station4.Text = "编号：" + StationID4 + "，名称：" + rows[0]["Name"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_Station4.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople4"] != DBNull.Value)
                    {
                        text_PeopleNum4.Text = SpecalRow["AllowMaxPeople4"].ToString();
                    }
                    else
                    {
                        return;
                    }
                    //初始化第五步的信息显示
                    string[] TimeStart5s = SpecalRow["TimeStart5"].ToString().Split(':');
                    if (TimeStart5s.Length > 1)
                    {
                        panel_Step5.Visible = true;
                        com_StartHour5.Text = TimeStart5s[0] + " 时";
                        com_StartMinute5.Text = TimeStart5s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    string[] TimeEnd5s = SpecalRow["TimeEnd5"].ToString().Split(':');
                    if (TimeEnd5s.Length > 1)
                    {
                        com_EndHour5.Text = TimeEnd5s[0] + " 时";
                        com_EndMinute5.Text = TimeEnd5s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["StationID5"] != DBNull.Value)
                    {
                        int StationID5 = Convert.ToInt32(SpecalRow["StationID5"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID5);
                        if (rows.Length > 0)
                        {
                            com_Station5.Text = "编号：" + StationID5 + "，名称：" + rows[0]["Name"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_Station5.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople5"] != DBNull.Value)
                    {
                        text_PeopleNum5.Text = SpecalRow["AllowMaxPeople5"].ToString();
                    }
                    else
                    {
                        return;
                    }
                    //初始化第六步的信息显示
                    string[] TimeStart6s = SpecalRow["TimeStart6"].ToString().Split(':');
                    if (TimeStart6s.Length > 1)
                    {
                        panel_Step6.Visible = true;
                        com_StartHour6.Text = TimeStart6s[0] + " 时";
                        com_StartMinute6.Text = TimeStart6s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    string[] TimeEnd6s = SpecalRow["TimeEnd6"].ToString().Split(':');
                    if (TimeEnd6s.Length > 1)
                    {
                        com_EndHour6.Text = TimeEnd6s[0] + " 时";
                        com_EndMinute6.Text = TimeEnd6s[1] + " 分";
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["StationID6"] != DBNull.Value)
                    {
                        int StationID6 = Convert.ToInt32(SpecalRow["StationID6"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID6);
                        if (rows.Length > 0)
                        {
                            com_Station6.Text = "编号：" + StationID6 + "，名称：" + rows[0]["Name"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_Station6.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople6"] != DBNull.Value)
                    {
                        text_PeopleNum6.Text = SpecalRow["AllowMaxPeople6"].ToString();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    //手动模式
                    if (SpecalRow["StationID1"] != DBNull.Value)
                    {
                        int StationID1 = Convert.ToInt32(SpecalRow["StationID1"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID1);
                        if (rows.Length > 0)
                        {
                            com_HandStation1.Text = "编号：" + StationID1 + "，名称：" + rows[0]["Name"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_HandStation1.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        com_HandStation1.SelectedIndex = -1;
                    }
                    if (SpecalRow["AllowMaxPeople1"] != DBNull.Value)
                    {
                        text_PeopleNumHand1.Text = SpecalRow["AllowMaxPeople1"].ToString();
                    }
                    else
                    {
                        text_PeopleNumHand1.Text = "0";
                    }
                    //初始化第二步的信息显示
                    if (SpecalRow["StationID2"] != DBNull.Value)
                    {
                        int StationID2 = Convert.ToInt32(SpecalRow["StationID2"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID2);
                        if (rows.Length > 0)
                        {
                            com_HandStation2.Text = "编号：" + StationID2 + "，名称：" + rows[0]["Name"].ToString();
                            panel_HandStep2.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_HandStation2.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople2"] != DBNull.Value)
                    {
                        text_PeopleNumHand2.Text = SpecalRow["AllowMaxPeople2"].ToString();
                    }
                    else
                    {
                        return;
                    }
                    //初始化第三步的信息显示
                    if (SpecalRow["StationID3"] != DBNull.Value)
                    {
                        int StationID3 = Convert.ToInt32(SpecalRow["StationID3"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID3);
                        if (rows.Length > 0)
                        {
                            com_HandStation3.Text = "编号：" + StationID3 + "，名称：" + rows[0]["Name"].ToString();
                            panel_HandStep3.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_HandStation3.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople3"] != DBNull.Value)
                    {
                        text_PeopleNumHand3.Text = SpecalRow["AllowMaxPeople3"].ToString();
                    }
                    else
                    {
                        return;
                    }
                    //初始化第四步的信息显示
                    if (SpecalRow["StationID4"] != DBNull.Value)
                    {
                        int StationID4 = Convert.ToInt32(SpecalRow["StationID4"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID4);
                        if (rows.Length > 0)
                        {
                            com_HandStation4.Text = "编号：" + StationID4 + "，名称：" + rows[0]["Name"].ToString();
                            panel_HandStep4.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_HandStation4.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople4"] != DBNull.Value)
                    {
                        text_PeopleNumHand4.Text = SpecalRow["AllowMaxPeople4"].ToString();
                    }
                    else
                    {
                        return;
                    }
                    //初始化第五步的信息显示
                    if (SpecalRow["StationID5"] != DBNull.Value)
                    {
                        int StationID5 = Convert.ToInt32(SpecalRow["StationID5"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID5);
                        if (rows.Length > 0)
                        {
                            com_HandStation5.Text = "编号：" + StationID5 + "，名称：" + rows[0]["Name"].ToString();
                            panel_HandStep5.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_HandStation5.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople5"] != DBNull.Value)
                    {
                        text_PeopleNumHand5.Text = SpecalRow["AllowMaxPeople5"].ToString();
                    }
                    else
                    {
                        return;
                    }
                    //初始化第六步的信息显示
                    if (SpecalRow["StationID6"] != DBNull.Value)
                    {
                        int StationID6 = Convert.ToInt32(SpecalRow["StationID6"]);
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + StationID6);
                        if (rows.Length > 0)
                        {
                            com_HandStation6.Text = "编号：" + StationID6 + "，名称：" + rows[0]["Name"].ToString();
                            panel_HandStep6.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("方案中的这个基站已经不存在，程序将自动剔除。", "基站查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            com_HandStation6.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (SpecalRow["AllowMaxPeople6"] != DBNull.Value)
                    {
                        text_PeopleNumHand6.Text = SpecalRow["AllowMaxPeople6"].ToString();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("无法在数据库中找到这条记录！", "数据查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_UpdateProject_Click(object sender, EventArgs e)
        {
            if (dataGridView_Specal.SelectedRows.Count > 0)
            {
                string SpecalName = this.dataGridView_Specal.SelectedRows[0].Cells["方案名称"].Value.ToString();
                if (Global.State_AreaAlarmName == SpecalName)
                {
                    MessageBox.Show("对不起，这个方案正在使用中。请先停止这个方案后再进行修改。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowSpecalInfoBySpecalName(SpecalName);
                    panel_Edit.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("请先选择一个欲修改的特殊区域方案。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void btn_NewProject_Click(object sender, EventArgs e)
        {
            SpecalRow = null;
            panel_Edit.Enabled = true;
            tex_ProjectName.ReadOnly = false;
            panel_Step2.Visible = false;
            panel_Step3.Visible = false;
            panel_Step4.Visible = false;
            panel_Step5.Visible = false;
            panel_Step6.Visible = false;
            panel_HandStep2.Visible = false;
            panel_HandStep3.Visible = false;
            panel_HandStep4.Visible = false;
            panel_HandStep5.Visible = false;
            panel_HandStep6.Visible = false;
            //初始化一个方案中的总体信息显示
            tex_ProjectName.Text = "";
            if (com_ProjectRing.Items.Count > 0)
                com_ProjectRing.SelectedIndex = 0;
            label_ProjectTime.Text = DateTime.Now.ToString();
            radio_TimeType.Checked = true;
            //清空第一步
            com_StartHour1.SelectedIndex = 0;
            com_StartMinute1.SelectedIndex = 0;
            com_EndHour1.SelectedIndex = 0;
            com_EndMinute1.SelectedIndex = 0;
            com_Station1.SelectedIndex = -1;
            text_PeopleNum1.Text = "0";
            text_PeopleNumHand1.Text = "0";
        }

        private void btn_Canel_Click(object sender, EventArgs e)
        {
            panel_Edit.Enabled = false;

            if (this.dataGridView_Specal.SelectedRows.Count > 0)
            {
                ShowSpecalInfoBySpecalName(this.dataGridView_Specal.SelectedRows[0].Cells["方案名称"].Value.ToString());
            }
        }

        private void btn_DelProject_Click(object sender, EventArgs e)
        {
            if (dataGridView_Specal.SelectedRows.Count > 0)
            {
                string DelSpecalName = dataGridView_Specal.SelectedRows[0].Cells["方案名称"].Value.ToString();
                if (Global.State_AreaAlarmName == DelSpecalName)
                {
                    MessageBox.Show("对不起，这个方案正在使用中。请先停止这个方案后再进行删除。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("您确定要删除 " + DelSpecalName + " 这个特殊区域方案吗？\n\n由这个方案产生的特殊区域报警也会一并删除。", "特殊区域方案管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable tempSpecalTable = DB_Service.GetTable("SpecalTable", "select * from SpecalTable");
                    DataRow[] rows = tempSpecalTable.Select("Name = '" + DelSpecalName + "'");
                    if (rows.Length > 0)
                    {
                        rows[0].Delete();
                        if (DB_Service.UpdateDBFromTable(tempSpecalTable) > 0)
                        {
                            DB_Service.ExecuteSQL("delete from AlarmInAreaTable where InAreaName = '" + DelSpecalName + "'", "AlarmInAreaTable");
                            RefreshDataTable();
                        }
                        else
                        {
                            MessageBox.Show("删除特殊区域方案失败！", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲删除的特殊区域方案不存在。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择一个欲删除的特殊区域方案。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (radio_TimeType.Checked)
            {
                if (tex_ProjectName.Text.Trim() == "" || com_ProjectRing.Text == "" || com_StartHour1.Text == "" || com_StartMinute1.Text == "" || com_EndHour1.Text == "" || com_EndMinute1.Text == "" || com_Station1.Text == "" || text_PeopleNum1.Text == "")
                {
                    MessageBox.Show("请输入完整的方案信息后再进行保存。\n至少包含：方案名称、报警音和一个完整的项目。", "特殊区域监控方案设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                if (tex_ProjectName.Text.Trim() == "" || com_ProjectRing.Text == "" || com_HandStation1.Text == "" || text_PeopleNumHand1.Text == "")
                {
                    MessageBox.Show("请输入完整的方案信息后再进行保存。\n至少包含：方案名称、报警音和一个完整的项目。", "特殊区域监控方案设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //如果是新建方案，初始化SpecalRow
            if (SpecalRow == null)
            {
                //方案名唯一性判断
                DataTable tempTable = DB_Service.GetTable("SpecalTable", "select * from SpecalTable");
                DataRow[] tempRows = tempTable.Select("Name = '" + tex_ProjectName.Text.Trim() + "'");
                if (tempRows.Length > 0)
                {
                    MessageBox.Show("对不起，您输入的这个方案名已经使用过了，请更换。", "特殊区域监控方案设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SpecalRow = tempTable.NewRow();
                tempTable.Rows.Add(SpecalRow);
            }
            //修改SpecalRow中全局属性的值
            SpecalRow["Name"] = tex_ProjectName.Text.Trim();
            SpecalRow["Time"] = Convert.ToDateTime(label_ProjectTime.Text);
            SpecalRow["Alarm"] = com_ProjectRing.Text;
            SpecalRow["IsTimeSpan"] = radio_TimeType.Checked;
            //修改SpecalRow中每一项的值
            if (radio_TimeType.Checked)
            {
                //修改SpecalRow中第一步的值
                SpecalRow["TimeStart1"] = com_StartHour1.Text.Split(' ')[0] + ":" + com_StartMinute1.Text.Split(' ')[0];
                SpecalRow["TimeEnd1"] = com_EndHour1.Text.Split(' ')[0] + ":" + com_EndMinute1.Text.Split(' ')[0];
                string[] strs = com_Station1.Text.Split('，');
                int StationID = Convert.ToInt32(strs[0].Substring(3, strs[0].Length - 3));
                SpecalRow["StationID1"] = StationID;
                SpecalRow["AllowMaxPeople1"] = Convert.ToInt32(text_PeopleNum1.Text);
                if (panel_Step2.Visible)
                {
                    //修改SpecalRow中第二步的值
                    string tempStationStr2 = com_Station2.Text;
                    if (tempStationStr2 != "" && text_PeopleNum2.Text != "")
                    {
                        SpecalRow["TimeStart2"] = com_StartHour2.Text.Split(' ')[0] + ":" + com_StartMinute2.Text.Split(' ')[0];
                        SpecalRow["TimeEnd2"] = com_EndHour2.Text.Split(' ')[0] + ":" + com_EndMinute2.Text.Split(' ')[0];

                        string[] strs2 = tempStationStr2.Split('，');
                        int StationID2 = Convert.ToInt32(strs2[0].Substring(3, strs2[0].Length - 3));
                        SpecalRow["StationID2"] = StationID2;
                        SpecalRow["AllowMaxPeople2"] = Convert.ToInt32(text_PeopleNum2.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第二项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["TimeStart2"] = DBNull.Value;
                    SpecalRow["TimeEnd2"] = DBNull.Value;
                    SpecalRow["StationID2"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople2"] = DBNull.Value;
                }

                if (panel_Step3.Visible)
                {
                    //修改SpecalRow中第三步的值
                    string tempStationStr3 = com_Station3.Text;
                    if (tempStationStr3 != "" && text_PeopleNum3.Text != "")
                    {
                        SpecalRow["TimeStart3"] = com_StartHour3.Text.Split(' ')[0] + ":" + com_StartMinute3.Text.Split(' ')[0];
                        SpecalRow["TimeEnd3"] = com_EndHour3.Text.Split(' ')[0] + ":" + com_EndMinute3.Text.Split(' ')[0];

                        string[] strs3 = tempStationStr3.Split('，');
                        int StationID3 = Convert.ToInt32(strs3[0].Substring(3, strs3[0].Length - 3));
                        SpecalRow["StationID3"] = StationID3;
                        SpecalRow["AllowMaxPeople3"] = Convert.ToInt32(text_PeopleNum3.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第三项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["TimeStart3"] = DBNull.Value;
                    SpecalRow["TimeEnd3"] = DBNull.Value;
                    SpecalRow["StationID3"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople3"] = DBNull.Value;
                }
                if (panel_Step4.Visible)
                {
                    //修改SpecalRow中第四步的值
                    string tempStationStr4 = com_Station4.Text;
                    if (tempStationStr4 != "" && text_PeopleNum4.Text != "")
                    {
                        SpecalRow["TimeStart4"] = com_StartHour4.Text.Split(' ')[0] + ":" + com_StartMinute4.Text.Split(' ')[0];
                        SpecalRow["TimeEnd4"] = com_EndHour4.Text.Split(' ')[0] + ":" + com_EndMinute4.Text.Split(' ')[0];

                        string[] strs4 = tempStationStr4.Split('，');
                        int StationID4 = Convert.ToInt32(strs4[0].Substring(3, strs4[0].Length - 3));
                        SpecalRow["StationID4"] = StationID4;
                        SpecalRow["AllowMaxPeople4"] = Convert.ToInt32(text_PeopleNum4.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第四项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["TimeStart4"] = DBNull.Value;
                    SpecalRow["TimeEnd4"] = DBNull.Value;
                    SpecalRow["StationID4"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople4"] = DBNull.Value;
                }
                if (panel_Step5.Visible)
                {
                    //修改SpecalRow中第五步的值
                    string tempStationStr5 = com_Station5.Text;
                    if (tempStationStr5 != "" && text_PeopleNum5.Text != "")
                    {
                        SpecalRow["TimeStart5"] = com_StartHour5.Text.Split(' ')[0] + ":" + com_StartMinute5.Text.Split(' ')[0];
                        SpecalRow["TimeEnd5"] = com_EndHour5.Text.Split(' ')[0] + ":" + com_EndMinute5.Text.Split(' ')[0];

                        string[] strs5 = tempStationStr5.Split('，');
                        int StationID5 = Convert.ToInt32(strs5[0].Substring(3, strs5[0].Length - 3));
                        SpecalRow["StationID5"] = StationID5;
                        SpecalRow["AllowMaxPeople5"] = Convert.ToInt32(text_PeopleNum5.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第五项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["TimeStart5"] = DBNull.Value;
                    SpecalRow["TimeEnd5"] = DBNull.Value;
                    SpecalRow["StationID5"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople5"] = DBNull.Value;
                }
                if (panel_Step6.Visible)
                {
                    //修改SpecalRow中第六步的值
                    string tempStationStr6 = com_Station6.Text;
                    if (tempStationStr6 != "" && text_PeopleNum6.Text != "")
                    {
                        SpecalRow["TimeStart6"] = com_StartHour6.Text.Split(' ')[0] + ":" + com_StartMinute6.Text.Split(' ')[0];
                        SpecalRow["TimeEnd6"] = com_EndHour6.Text.Split(' ')[0] + ":" + com_EndMinute6.Text.Split(' ')[0];

                        string[] strs6 = tempStationStr6.Split('，');
                        int StationID6 = Convert.ToInt32(strs6[0].Substring(3, strs6[0].Length - 3));
                        SpecalRow["StationID6"] = StationID6;
                        SpecalRow["AllowMaxPeople6"] = Convert.ToInt32(text_PeopleNum6.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第六项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["TimeStart6"] = DBNull.Value;
                    SpecalRow["TimeEnd6"] = DBNull.Value;
                    SpecalRow["StationID6"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople6"] = DBNull.Value;
                }
            }
            else
            {
                //修改SpecalRow中第一步的值
                string[] strs = com_HandStation1.Text.Split('，');
                int StationID = Convert.ToInt32(strs[0].Substring(3, strs[0].Length - 3));
                SpecalRow["StationID1"] = StationID;
                SpecalRow["AllowMaxPeople1"] = Convert.ToInt32(text_PeopleNumHand1.Text);
                if (panel_HandStep2.Visible)
                {
                    //修改SpecalRow中第二步的值
                    string tempStationStr2 = com_HandStation2.Text;
                    if (tempStationStr2 != "" && text_PeopleNumHand2.Text != "")
                    {
                        string[] strs2 = tempStationStr2.Split('，');
                        int StationID2 = Convert.ToInt32(strs2[0].Substring(3, strs2[0].Length - 3));
                        SpecalRow["StationID2"] = StationID2;
                        SpecalRow["AllowMaxPeople2"] = Convert.ToInt32(text_PeopleNumHand2.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第二项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["StationID2"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople2"] = DBNull.Value;
                }

                if (panel_HandStep3.Visible)
                {
                    //修改SpecalRow中第三步的值
                    string tempStationStr3 = com_HandStation3.Text;
                    if (tempStationStr3 != "" && text_PeopleNumHand3.Text != "")
                    {
                        string[] strs3 = tempStationStr3.Split('，');
                        int StationID3 = Convert.ToInt32(strs3[0].Substring(3, strs3[0].Length - 3));
                        SpecalRow["StationID3"] = StationID3;
                        SpecalRow["AllowMaxPeople3"] = Convert.ToInt32(text_PeopleNumHand3.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第三项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["StationID3"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople3"] = DBNull.Value;
                }
                if (panel_HandStep4.Visible)
                {
                    //修改SpecalRow中第四步的值
                    string tempStationStr4 = com_HandStation4.Text;
                    if (tempStationStr4 != "" && text_PeopleNumHand4.Text != "")
                    {
                        string[] strs4 = tempStationStr4.Split('，');
                        int StationID4 = Convert.ToInt32(strs4[0].Substring(3, strs4[0].Length - 3));
                        SpecalRow["StationID4"] = StationID4;
                        SpecalRow["AllowMaxPeople4"] = Convert.ToInt32(text_PeopleNumHand4.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第四项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["StationID4"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople4"] = DBNull.Value;
                }
                if (panel_HandStep5.Visible)
                {
                    //修改SpecalRow中第五步的值
                    string tempStationStr5 = com_HandStation5.Text;
                    if (tempStationStr5 != "" && text_PeopleNumHand5.Text != "")
                    {
                        string[] strs5 = tempStationStr5.Split('，');
                        int StationID5 = Convert.ToInt32(strs5[0].Substring(3, strs5[0].Length - 3));
                        SpecalRow["StationID5"] = StationID5;
                        SpecalRow["AllowMaxPeople5"] = Convert.ToInt32(text_PeopleNumHand5.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第五项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["StationID5"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople5"] = DBNull.Value;
                }
                if (panel_HandStep6.Visible)
                {
                    //修改SpecalRow中第六步的值
                    string tempStationStr6 = com_HandStation6.Text;
                    if (tempStationStr6 != "" && text_PeopleNumHand6.Text != "")
                    {
                        string[] strs6 = tempStationStr6.Split('，');
                        int StationID6 = Convert.ToInt32(strs6[0].Substring(3, strs6[0].Length - 3));
                        SpecalRow["StationID6"] = StationID6;
                        SpecalRow["AllowMaxPeople6"] = Convert.ToInt32(text_PeopleNumHand6.Text);
                    }
                    else
                    {
                        MessageBox.Show("对不起，您的第六项目中没有目标基站或人数限制。请补充完整方案信息后再进行保存。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    SpecalRow["StationID6"] = DBNull.Value;
                    SpecalRow["AllowMaxPeople6"] = DBNull.Value;
                }
            }

            

            //将SpecalRow中的更新提交至数据库
            if (DB_Service.UpdateDBFromTable(SpecalRow.Table) > 0)
            {
                RefreshDataTable();
                panel_Edit.Enabled = false;
            }
            else
            {
                MessageBox.Show("保存特殊区域方案信息失败！\n请确保数据库连接正确。", "特殊区域方案管理", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void radio_HandType_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_HandType.Checked)
            {
                panel_TimeSpan.Visible = false;
                panel_Hand.Visible = true;
            }
            else
            {
                panel_Hand.Visible = false;
                panel_TimeSpan.Visible = true;
            }
        }

        private void text_PeopleNum1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNum2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNum3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNum4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNum5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNum6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNumHand1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNumHand2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNumHand3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNumHand4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNumHand5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_PeopleNumHand6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataGridViewPrinter DGVP = new DataGridViewPrinter(this.DataGrid_InArea, "特殊区域报警统计","", "", "", "", false,this.DataGrid_InArea.ColumnCount);
            DGVP.Print();
        }

        private void btn_SenderInfo_Click(object sender, EventArgs e)
        {
            if (DataGrid_InArea.SelectedRows.Count > 0)
            {
                try
                {
                    this.mainform.MainBtn_Person_Click(null, null);
                    this.mainform.frmPerson.ShowPersonInfoByCardID(Convert.ToInt32(DataGrid_InArea.SelectedRows[0].Cells["卡号"].Value));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("请先选择一条报警信息。", "报警信息");
            }
        }

        private void btn_StationInfo_Click(object sender, EventArgs e)
        {
            if (DataGrid_InArea.SelectedRows.Count > 0)
            {
                try
                {
                    this.mainform.MainBtn_Machine_Click(null, null);
                    this.mainform.frmMachine.ShowStationInfoByID(Convert.ToInt32(DataGrid_InArea.SelectedRows[0].Cells["基站编号"].Value));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("请先选择一条报警信息。", "报警信息");
            }
        }

        private void btn_GoToMap_Click(object sender, EventArgs e)
        {
            if (DataGrid_InArea.SelectedRows.Count > 0)
            {
                DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + DataGrid_InArea.SelectedRows[0].Cells["基站编号"].Value);
                if (rows.Length > 0)
                {
                    this.mainform.MainBtn_Watch_Click(null, null);
                    this.mainform.ShowStationToolTipInMap(rows[0]["Name"].ToString(), Convert.ToInt32(rows[0]["ID"]), rows[0]["StationType"].ToString(), rows[0]["StationFunction"].ToString(), Convert.ToDouble(rows[0]["Geo_X"]), Convert.ToDouble(rows[0]["Geo_Y"]));
                }
                else
                {
                    MessageBox.Show("这个基站不存在！所以您无法在地图中定位。", "报警信息");
                }
            }
            else
            {
                MessageBox.Show("请先选择一条报警信息。", "报警信息");
            }
        }
    }
}