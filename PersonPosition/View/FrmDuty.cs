using System;
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
    public partial class FrmDuty : Form
    {
        private DataTable TempDutyTable;
        private MainForm mainform;

        public FrmDuty(MainForm _mainform)
        {
            InitializeComponent();

            this.Tag = this.MainPanel;
            this.mainform = _mainform;

            //初始化考勤明细的时间段是本月
            linkLabel3_LinkClicked(null, null);
            radioButton10.Checked = true;
            //初始化考勤统计的时间段是本月
            linkLabel7_LinkClicked(null, null);
            radioButton2.Checked = true;
            //初始化考勤统计的按月统计的起始日期为1日
            com_StartMonthDay.SelectedIndex = 0;
            //初始化考勤分析的按日分析
            radio_AnalysicsByDay.Checked = true;
            //水晶报表控件的Bug，需要每次强制显示工具条和状态条
            ReportViewerDetail.DisplayToolbar = true;
            ReportViewerDetail.DisplayStatusBar = true;
            DutyReportView.DisplayToolbar = true;
            DutyReportView.DisplayStatusBar = true;
            AnalysicsReportView.DisplayToolbar = true;
            AnalysicsReportView.DisplayStatusBar = true;
        }

        #region 提供的通用服务

        /// <summary>
        /// 显示指定员工的当月考勤信息
        /// </summary>
        /// <param name="PID"></param>
        public void ShowPersonDutyByPID(string PID)
        {
            radioBtn_PID.Checked = true;
            tex_Search.Text = PID;
            btn_SearchDuty_Click(null, null);
        }

        /// <summary>
        /// 显示指定员工的当月考勤信息
        /// </summary>
        /// <param name="CardID"></param>
        public void ShowPersonDutyByCardID(int CardID)
        {
            radioBtn_CardID.Checked = true;
            tex_Search.Text = CardID.ToString();
            btn_SearchDuty_Click(null, null);
        }

        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    //初始化部门combox
                    com_Department.Items.Clear();
                    try
                    {
                        foreach (DataRow row in DB_Service.MainDataSet.Tables["DepartmentTable"].Rows)
                        {
                            com_Department.Items.Add(row["DepartmentName"]);
                        }
                    }
                    catch
                    {  }
                    if (com_Department.Items.Count > 0)
                        com_Department.SelectedIndex = 0;
                    break;
                case 2:
                    break;
            }
        }

        #region 考勤明细

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            DataGridView.Visible = false;
            ReportViewerDetail.Visible = true;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            DataGridView.Visible = true;
            ReportViewerDetail.Visible = false;
        }

        private void radioBtn_PID_CheckedChanged(object sender, EventArgs e)
        {
            label_SearchKey.Text = "工号：";
            tex_Search.Visible = true;
            com_Search.Visible = false;
        }

        private void radioBtn_CardID_CheckedChanged(object sender, EventArgs e)
        {
            label_SearchKey.Text = "卡号：";
            tex_Search.Visible = true;
            com_Search.Visible = false;
        }

        private void radioBtn_Name_CheckedChanged(object sender, EventArgs e)
        {
            label_SearchKey.Text = "姓名：";
            tex_Search.Visible = true;
            com_Search.Visible = false;
        }

        private void radioBtn_Department_CheckedChanged(object sender, EventArgs e)
        {
            label_SearchKey.Text = "部门：";
            tex_Search.Visible = false;
            com_Search.Visible = true;

            com_Search.Items.Clear();
            try
            {
                foreach (DataRow row in DB_Service.MainDataSet.Tables["DepartmentTable"].Rows)
                {
                    com_Search.Items.Add(row["DepartmentName"]);
                }
            }
            catch
            {  }
            if (com_Search.Items.Count > 0)
            {
                com_Search.SelectedIndex = 0;
            }
        }

        private void radioBtn_WorkType_CheckedChanged(object sender, EventArgs e)
        {
            label_SearchKey.Text = "职务：";
            tex_Search.Visible = false;
            com_Search.Visible = true;

            com_Search.Items.Clear();
            try
            {
                foreach (DataRow row in DB_Service.MainDataSet.Tables["WorkTypeTable"].Rows)
                {
                    com_Search.Items.Add(row["WorkTypeName"].ToString());
                }
            }
            catch
            {   }
            if (com_Search.Items.Count > 0)
            {
                com_Search.SelectedIndex = 0;
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                label7.Text = "日期：";
                linkLabel1.Visible = true;
                linkLabel2.Visible = true;
                linkLabel3.Visible = false;
                linkLabel4.Visible = false;
                dateTimePicker6.Format = DateTimePickerFormat.Long;
                dateTimePicker6.Visible = true;
                panel1.Visible = false;
                comboBox1.Visible = false;
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                label7.Text = "月份：";
                linkLabel1.Visible = false;
                linkLabel2.Visible = false;
                linkLabel3.Visible = true;
                linkLabel4.Visible = true;
                dateTimePicker6.Format = DateTimePickerFormat.Custom;
                dateTimePicker6.Visible = true;
                panel1.Visible = false;
                comboBox1.Visible = false;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                label7.Text = "季度：";
                linkLabel1.Visible = false;
                linkLabel2.Visible = false;
                linkLabel3.Visible = false;
                linkLabel4.Visible = false;
                dateTimePicker6.Visible = false;
                panel1.Visible = false;
                comboBox1.Visible = true;
                comboBox1.SelectedIndex = 0;
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                dateTimePicker6.Visible = false;
                panel1.Visible = true;
                comboBox1.Visible = false;
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker6.Value = DateTime.Now.AddMonths(-1);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker6.Value = DateTime.Now;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker6.Value = DateTime.Now.AddDays(-1);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker6.Value = DateTime.Now;
        }

        private void btn_SearchDuty_Click(object sender, EventArgs e)
        {
            try
            {
                string SQLConditionStr = "";
                string ReportConditionStr = "";
                string SQLTimeStr = "";
                string ReportTimeStr = "";
                DateTime StartDay;
                DateTime EndDay;

                switch (label_SearchKey.Text)
                {
                    case "工号：":
                        if (tex_Search.Text.Trim() != "")
                        {
                            SQLConditionStr = "DutyTable.CardID = CardTable.CardID and CardTable.PID = PersonTable.PID and PersonTable.PID = '" + tex_Search.Text.Trim() + "'";
                        }
                        else
                        {
                            MessageBox.Show("对不起，请先输入您欲查询的员工工号后重试。", "考勤明细", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        break;
                    case "卡号：":
                        if (tex_Search.Text.Trim() != "")
                        {
                            SQLConditionStr = "DutyTable.CardID = " + tex_Search.Text.Trim() + " and CardTable.PID = PersonTable.PID and CardTable.CardID = " + tex_Search.Text.Trim();
                        }
                        else
                        {
                            MessageBox.Show("对不起，请先输入您欲查询的员工卡号后重试。", "考勤明细", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        break;
                    case "姓名：":
                        if (tex_Search.Text.Trim() != "")
                        {
                            SQLConditionStr = "PersonTable.PID = CardTable.PID and CardTable.CardID = DutyTable.CardID and PersonTable.Name like '%" + tex_Search.Text.Trim() + "%'";
                        }
                        else
                        {
                            MessageBox.Show("对不起，请先输入您欲查询的员工姓名后重试。", "考勤明细", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        break;
                    case "部门：":
                        SQLConditionStr = "DutyTable.CardID = CardTable.CardID and CardTable.PID = PersonTable.PID and PersonTable.Department = '" + com_Search.Text.Trim() + "'";
                        ReportConditionStr = "查询部门：" + com_Search.Text.Trim();
                        break;
                    case "职务：":
                        SQLConditionStr = "DutyTable.CardID = CardTable.CardID and CardTable.PID = PersonTable.PID and PersonTable.WorkType = '" + com_Search.Text.Trim() + "'";
                        ReportConditionStr = "查询职务：" + com_Search.Text.Trim();
                        break;
                }

                if (radioButton8.Checked)
                {
                    if (dateTimePicker1.Value.Date.CompareTo(dateTimePicker2.Value.Date) <= 0)
                    {
                        //任意时间段统计
                        ReportTimeStr = "考勤时间：" + dateTimePicker1.Value.Year + "年" + dateTimePicker1.Value.Month + "月" + dateTimePicker1.Value.Day + "日 到 " + dateTimePicker2.Value.Year + "年" + dateTimePicker2.Value.Month + "月" + dateTimePicker2.Value.Day + "日";
                        SQLTimeStr = " and Date >= '" + dateTimePicker1.Value.Date + "' and Date <= '" + dateTimePicker2.Value.Date + "'";
                        StartDay = new DateTime();
                        StartDay = dateTimePicker1.Value.Date;
                        EndDay = new DateTime();
                        EndDay = dateTimePicker2.Value.Date;
                    }
                    else
                    {
                        MessageBox.Show("对不起。您选择的终止日期小于起始日期，这不是一个有效的时间段。\n\n请选择一个正确的时间段。", "考勤管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (radioButton9.Checked)
                {
                    //日统计
                    ReportTimeStr = "考勤时间：" + dateTimePicker6.Value.Year + "年" + dateTimePicker6.Value.Month + "月" + dateTimePicker6.Value.Day + "日";
                    SQLTimeStr = " and Date = '" + dateTimePicker6.Value.Date + "'";
                    StartDay = new DateTime();
                    StartDay = dateTimePicker6.Value.Date;
                    EndDay = new DateTime();
                    EndDay = dateTimePicker6.Value.Date;
                }
                else if (radioButton10.Checked)
                {
                    //月统计
                    ReportTimeStr = "考勤时间：" + dateTimePicker6.Value.Year + "年" + dateTimePicker6.Value.Month + "月";
                    SQLTimeStr = " and DATEPART(year,Date) = " + dateTimePicker6.Value.Year + " and DATEPART(month,Date) = " + dateTimePicker6.Value.Month;
                    StartDay = new DateTime(dateTimePicker6.Value.Year, dateTimePicker6.Value.Month, 1);
                    EndDay = new DateTime(dateTimePicker6.Value.Year, dateTimePicker6.Value.Month, DateTime.DaysInMonth(dateTimePicker6.Value.Year, dateTimePicker6.Value.Month));
                }
                else
                {
                    //季度统计
                    ReportTimeStr = "考勤时间：" + DateTime.Now.Year + "年" + comboBox1.Text.Substring(2);
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            SQLTimeStr = " and DATEPART(year,Date) = " + DateTime.Now.Year + " and (DATEPART(month,Date) = 1 or DATEPART(month,Date) = 2 or DATEPART(month,Date) = 3)";
                            StartDay = new DateTime(DateTime.Now.Year, 1, 1);
                            EndDay = new DateTime(DateTime.Now.Year, 3, DateTime.DaysInMonth(DateTime.Now.Year, 3));
                            break;
                        case 1:
                            SQLTimeStr = " and DATEPART(year,Date) = " + DateTime.Now.Year + " and (DATEPART(month,Date) = 4 or DATEPART(month,Date) = 5 or DATEPART(month,Date) = 6)";
                            StartDay = new DateTime(DateTime.Now.Year, 4, 1);
                            EndDay = new DateTime(DateTime.Now.Year, 6, DateTime.DaysInMonth(DateTime.Now.Year, 6));
                            break;
                        case 2:
                            SQLTimeStr = " and DATEPART(year,Date) = " + DateTime.Now.Year + " and (DATEPART(month,Date) = 7 or DATEPART(month,Date) = 8 or DATEPART(month,Date) = 9)";
                            StartDay = new DateTime(DateTime.Now.Year, 7, 1);
                            EndDay = new DateTime(DateTime.Now.Year, 9, DateTime.DaysInMonth(DateTime.Now.Year, 9));
                            break;
                        case 3:
                            SQLTimeStr = " and DATEPART(year,Date) = " + DateTime.Now.Year + " and (DATEPART(month,Date) = 10 or DATEPART(month,Date) = 11 or DATEPART(month,Date) = 12)";
                            StartDay = new DateTime(DateTime.Now.Year, 10, 1);
                            EndDay = new DateTime(DateTime.Now.Year, 12, DateTime.DaysInMonth(DateTime.Now.Year, 12));
                            break;
                        default:
                            SQLTimeStr = " and DATEPART(year,Date) = " + DateTime.Now.Year + " and (DATEPART(month,Date) = 1 or DATEPART(month,Date) = 2 or DATEPART(month,Date) = 3)";
                            StartDay = new DateTime(DateTime.Now.Year, 1, 1);
                            EndDay = new DateTime(DateTime.Now.Year, 3, DateTime.DaysInMonth(DateTime.Now.Year, 3));
                            break;
                    }
                }

                string strSerach = "select PersonTable.Name as 姓名,PersonTable.PID as 工号,DutyTable.CardID as 卡号,PersonTable.WorkType as 职务,PersonTable.Department as 部门,DutyTable.InTime as 入井时间,DutyTable.OutTime as 出井时间,DutyTable.Date as 日期,DutyTable.DataSource as 数据来源 from CardTable,DutyTable,PersonTable where ";
                this.TempDutyTable = DB_Service.GetTable("TempDutyTable", strSerach + SQLConditionStr + SQLTimeStr + " order by 工号 ASC");

                //筛选掉职务和出井时间为空的记录
                bool IsHadNullWorkType = false;
                bool IsHadNullOutTime = false;
                for (int i = 0; i < TempDutyTable.Rows.Count; i++)
                {
                    if (TempDutyTable.Rows[i]["职务"] == DBNull.Value || TempDutyTable.Rows[i]["职务"].ToString() == "")
                    {
                        IsHadNullWorkType = true;
                    }
                    if (TempDutyTable.Rows[i]["入井时间"] == DBNull.Value || TempDutyTable.Rows[i]["出井时间"] == DBNull.Value)
                    {
                        IsHadNullOutTime = true;
                        TempDutyTable.Rows.Remove(TempDutyTable.Rows[i]);
                    }
                }
                if (IsHadNullWorkType)
                {
                    MessageBox.Show("注意！有员工的职务为空。这位员工的信息将不会统计在报表内。", "考勤统计", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (IsHadNullOutTime)
                {
                    MessageBox.Show("注意！有员工的离开时间为空。这位员工的信息将不会统计在报表内。", "考勤统计", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ////创建一个考勤报表对象
                //string Class1 = "";
                //string Class2 = "";
                //string Class3 = "";
                //string Class4 = "";
                //if (DB_Service.MainDataSet.Tables["ClassTable"].Rows.Count > 0)
                //{
                //    Class1 = DB_Service.MainDataSet.Tables["ClassTable"].Rows[0]["ClassName"].ToString();
                //    if (DB_Service.MainDataSet.Tables["ClassTable"].Rows.Count > 1)
                //    {
                //        Class2 = DB_Service.MainDataSet.Tables["ClassTable"].Rows[1]["ClassName"].ToString();
                //        if (DB_Service.MainDataSet.Tables["ClassTable"].Rows.Count > 2)
                //        {
                //            Class3 = DB_Service.MainDataSet.Tables["ClassTable"].Rows[2]["ClassName"].ToString();
                //            if (DB_Service.MainDataSet.Tables["ClassTable"].Rows.Count > 3)
                //            {
                //                Class4 = DB_Service.MainDataSet.Tables["ClassTable"].Rows[3]["ClassName"].ToString();
                //            }
                //        }
                //    }
                //}

                //ReportDetail_Duty RDD = new ReportDetail_Duty(Global.MapName + "员工考勤细目表", ReportConditionStr, ReportTimeStr, Class1, Class2, Class3, Class4);
                ReportDetail_Duty RDD = new ReportDetail_Duty(Global.MapName + "员工考勤细目表", ReportConditionStr, ReportTimeStr, "零点班", "八点班", "四点班", "");

                for (int i = 0; i < this.TempDutyTable.Rows.Count; i++)
                {
                    DataRow NewRow = RDD.DataSetReport.DetailDutyTabel.NewRow();
                    NewRow["Name"] = TempDutyTable.Rows[i]["姓名"].ToString();
                    NewRow["PID"] = TempDutyTable.Rows[i]["工号"].ToString();
                    NewRow["CardID"] = Convert.ToInt32(TempDutyTable.Rows[i]["卡号"]);
                    NewRow["Department"] = TempDutyTable.Rows[i]["部门"].ToString();
                    NewRow["WorkType"] = TempDutyTable.Rows[i]["职务"].ToString();
                    NewRow["Date"] = TempDutyTable.Rows[i]["日期"].ToString().Split(' ')[0];
                    NewRow["InTime"] = TempDutyTable.Rows[i]["入井时间"].ToString().Split(' ')[1];
                    string[] templist = TempDutyTable.Rows[i]["出井时间"].ToString().Split(' ');
                    if (templist.Length < 2)
                    {
                        continue;
                    }
                    NewRow["OutTime"] = TempDutyTable.Rows[i]["出井时间"].ToString().Split(' ')[1];
                    //计算工时
                    TimeSpan span = Convert.ToDateTime(TempDutyTable.Rows[i]["出井时间"]) - Convert.ToDateTime(TempDutyTable.Rows[i]["入井时间"]);
                    int WorkMinute = Convert.ToInt32(span.TotalMinutes);
                    NewRow["WorkTime"] = Convert.ToInt32(WorkMinute / 60.0);
                    //添加新行
                    RDD.DataSetReport.DetailDutyTabel.Rows.Add(NewRow);
                }


                int seek = 0;
                while (seek < RDD.DataSetReport.DetailDutyTabel.Rows.Count)
                {
                    string PID = RDD.DataSetReport.DetailDutyTabel.Rows[seek]["PID"].ToString();
                    string DateStr = RDD.DataSetReport.DetailDutyTabel.Rows[seek]["Date"].ToString();
                    DataRow[] row_PIDDate = RDD.DataSetReport.DetailDutyTabel.Select("PID = '" + PID + "' and Date = '" + DateStr + "'");

                    int TotalWorkTime = 0;
                    int ClassNum = 0;
                    for (int j = 0; j < row_PIDDate.Length; j++)
                    {
                        TotalWorkTime += Convert.ToInt32(row_PIDDate[j]["WorkTime"]);
                        DateTime InMineTime = Convert.ToDateTime(row_PIDDate[j]["InTime"]);
                        if (InMineTime.Hour >= 7 && InMineTime.Hour <= 14)
                        {
                            ClassNum = 8;
                        }
                        else if (InMineTime.Hour >= 15 && InMineTime.Hour <= 22)
                        {
                            ClassNum = 4;
                        }
                        else
                        {
                            ClassNum = 0;
                        }
                    }
                    //得到这个人的职务
                    DataRow row_WorkType = DB_Service.MainDataSet.Tables["WorkTypeTable"].Select("WorkTypeName = '" + RDD.DataSetReport.DetailDutyTabel.Rows[seek]["WorkType"] + "'")[0];
                    int NeedWorkHour = Convert.ToInt32(row_WorkType["NeedWorkHour"]);
                    if (TotalWorkTime >= NeedWorkHour)
                    {
                        if (ClassNum == 0)
                        {
                            RDD.DataSetReport.DetailDutyTabel.Rows[seek]["Class1Times"] = 1;
                        }
                        else if (ClassNum == 8)
                        {
                            RDD.DataSetReport.DetailDutyTabel.Rows[seek]["Class2Times"] = 1;
                        }
                        else
                        {
                            RDD.DataSetReport.DetailDutyTabel.Rows[seek]["Class3Times"] = 1;
                        }
                    }
                    seek += row_PIDDate.Length;
                }


                //将考勤报表对象的参数域传给报表控件
                ReportViewerDetail.ParameterFieldInfo = RDD.PFields;
                //将考勤报表对象的报表传给报表控件
                ReportViewerDetail.ReportSource = RDD.Report;
                //将结果集赋予数据表
                DataGridView.DataSource = TempDutyTable;

                RDD = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "考勤明细", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportViewerDetail.PrintReport();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReportViewerDetail.ExportReport();
        }

        #endregion

        #region 考勤统计

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label5.Text = "日期：";
                linkLabel5.Visible = true;
                linkLabel6.Visible = true;
                linkLabel7.Visible = false;
                linkLabel8.Visible = false;
                dateTimePicker5.Format = DateTimePickerFormat.Long;
                dateTimePicker5.Visible = true;
                panel_AnyTime.Visible = false;
                com_StartMonthDay.Visible = false;
                label8.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label5.Text = "月份：";
                linkLabel5.Visible = false;
                linkLabel6.Visible = false;
                linkLabel7.Visible = true;
                linkLabel8.Visible = true;
                dateTimePicker5.Format = DateTimePickerFormat.Custom;
                dateTimePicker5.Visible = true;
                panel_AnyTime.Visible = false;
                com_StartMonthDay.Visible = true;
                label8.Visible = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                dateTimePicker5.Visible = false;
                panel_AnyTime.Visible = true;
                com_StartMonthDay.Visible = false;
                label8.Visible = false;
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker5.Value = DateTime.Now.AddDays(-1);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker5.Value = DateTime.Now;
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker5.Value = DateTime.Now.AddMonths(-1);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker5.Value = DateTime.Now;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                string ReportTimeStr = "";
                string SQLTimeStr = "";
                DateTime StartDay = new DateTime();
                DateTime EndDay = new DateTime();
                //根据用户选择初始化时间信息变量
                if (!InitTimeInfo(ref ReportTimeStr, ref SQLTimeStr, ref StartDay, ref EndDay))
                    return;

                //以 Date  CardID  WorkType  InTime  OutTime  的组成形式，按指定部门内符合时间段内的员工筛选出的DutyTable记录
                string sqlTemp = "select DutyTable.Date,DutyTable.CardID,PersonTable.WorkType,DutyTable.InTime,DutyTable.OutTime from DutyTable,CardTable,PersonTable where DutyTable.CardID = CardTable.CardID and CardTable.PID = PersonTable.PID and PersonTable.Department = '" + com_Department.Text + "'" + SQLTimeStr + " order by DutyTable.Date";

                DataTable tempTable = DB_Service.GetTable("tempTable", sqlTemp);

                //筛选掉出井为空的记录
                bool IsHadNull = false;
                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    if (tempTable.Rows[i]["InTime"] == DBNull.Value || tempTable.Rows[i]["OutTime"] == DBNull.Value)
                    {
                        tempTable.Rows.Remove(tempTable.Rows[i]);
                        i = -1;
                        IsHadNull = true;
                    }
                }
                if (IsHadNull)
                {
                    MessageBox.Show("注意！有员工的离开时间为空。这位员工的信息将不会统计在报表内。", "考勤统计", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ////班次
                //string FirstClassName = "默认正常班次";
                //string SeondClassName = "空";
                //string ThirdClassName = "空";
                //if (DB_Service.MainDataSet.Tables["ClassTable"].Rows.Count >= 3)
                //{
                //    //前三条记录代表三个班次
                //    FirstClassName = DB_Service.MainDataSet.Tables["ClassTable"].Rows[0]["ClassName"].ToString();
                //    SeondClassName = DB_Service.MainDataSet.Tables["ClassTable"].Rows[1]["ClassName"].ToString();
                //    ThirdClassName = DB_Service.MainDataSet.Tables["ClassTable"].Rows[2]["ClassName"].ToString();
                //}
                ////创建一个考勤报表对象
                //ReportStatistic_Duty DutyReport = new ReportStatistic_Duty(Global.MapName + com_Department.Text + "考勤报表", "", ReportTimeStr, FirstClassName, SeondClassName, ThirdClassName);
                ReportStatistic_Duty DutyReport = new ReportStatistic_Duty(Global.MapName + com_Department.Text + "考勤报表", "", ReportTimeStr, "零点班进入人数", "八点班进入人数", "四点班进入人数");





                //for (int i = 0; i < this.TempDutyTable.Rows.Count; i++)
                //{
                //    DataRow NewRow = RDD.DataSetReport.DetailDutyTabel.NewRow();
                //    NewRow["Name"] = TempDutyTable.Rows[i]["姓名"].ToString();
                //    NewRow["PID"] = TempDutyTable.Rows[i]["工号"].ToString();
                //    NewRow["CardID"] = Convert.ToInt32(TempDutyTable.Rows[i]["卡号"]);
                //    NewRow["Department"] = TempDutyTable.Rows[i]["部门"].ToString();
                //    NewRow["WorkType"] = TempDutyTable.Rows[i]["职务"].ToString();
                //    NewRow["Date"] = TempDutyTable.Rows[i]["日期"].ToString().Split(' ')[0];
                //    NewRow["InTime"] = TempDutyTable.Rows[i]["入井时间"].ToString().Split(' ')[1];
                //    string[] templist = TempDutyTable.Rows[i]["出井时间"].ToString().Split(' ');
                //    if (templist.Length < 2)
                //    {
                //        continue;
                //    }
                //    NewRow["OutTime"] = TempDutyTable.Rows[i]["出井时间"].ToString().Split(' ')[1];
                //    //计算工时
                //    TimeSpan span = Convert.ToDateTime(TempDutyTable.Rows[i]["出井时间"]) - Convert.ToDateTime(TempDutyTable.Rows[i]["入井时间"]);
                //    int WorkMinute = Convert.ToInt32(span.TotalMinutes);
                //    NewRow["WorkTime"] = Convert.ToInt32(WorkMinute / 60.0);
                //    //添加新行
                //    RDD.DataSetReport.DetailDutyTabel.Rows.Add(NewRow);
                //}
                //int seek = 0;
                //while (seek < RDD.DataSetReport.DetailDutyTabel.Rows.Count)
                //{
                //    string PID = RDD.DataSetReport.DetailDutyTabel.Rows[seek]["PID"].ToString();
                //    string DateStr = RDD.DataSetReport.DetailDutyTabel.Rows[seek]["Date"].ToString();
                //    DataRow[] row_PIDDate = RDD.DataSetReport.DetailDutyTabel.Select("PID = '" + PID + "' and Date = '" + DateStr + "'");

                //    int TotalWorkTime = 0;
                //    int ClassNum = 0;
                //    for (int j = 0; j < row_PIDDate.Length; j++)
                //    {
                //        TotalWorkTime += Convert.ToInt32(row_PIDDate[j]["WorkTime"]);
                //        DateTime InMineTime = Convert.ToDateTime(row_PIDDate[j]["InTime"]);
                //        if (InMineTime.Hour >= 7 && InMineTime.Hour <= 14)
                //        {
                //            ClassNum = 8;
                //        }
                //        else if (InMineTime.Hour >= 15 && InMineTime.Hour <= 22)
                //        {
                //            ClassNum = 4;
                //        }
                //        else
                //        {
                //            ClassNum = 0;
                //        }
                //    }
                //    //得到这个人的职务
                //    DataRow row_WorkType = DB_Service.MainDataSet.Tables["WorkTypeTable"].Select("WorkTypeName = '" + RDD.DataSetReport.DetailDutyTabel.Rows[seek]["WorkType"] + "'")[0];
                //    int NeedWorkHour = Convert.ToInt32(row_WorkType["NeedWorkHour"]);
                //    if (TotalWorkTime >= NeedWorkHour)
                //    {
                //        if (ClassNum == 0)
                //        {
                //            RDD.DataSetReport.DetailDutyTabel.Rows[seek]["Class1Times"] = 1;
                //        }
                //        else if (ClassNum == 8)
                //        {
                //            RDD.DataSetReport.DetailDutyTabel.Rows[seek]["Class2Times"] = 1;
                //        }
                //        else
                //        {
                //            RDD.DataSetReport.DetailDutyTabel.Rows[seek]["Class3Times"] = 1;
                //        }
                //    }
                //    seek += row_PIDDate.Length;
                //}







                DateTime SeekDate = StartDay;
                while (SeekDate.CompareTo(EndDay) <= 0)
                {
                    DataRow NewRow = DutyReport.DataSetReport.StatisticDutyTable.NewRow();
                    NewRow["Department"] = com_Department.Text;
                    NewRow["Date"] = SeekDate.Date.ToString().Split(' ')[0];

                    DataRow[] rows = tempTable.Select("Date = '" + SeekDate + "'");
                    if (rows.Length > 0)
                    {
                        int FC_InMineNum = 0;
                        int SC_InMineNum = 0;
                        int TC_InMineNum = 0;
                        for (int i = 0; i < rows.Length; i++)
                        {
                            ////计算工时
                            //TimeSpan span = Convert.ToDateTime(rows[i]["OutTime"]) - Convert.ToDateTime(rows[i]["InTime"]);
                            //int WorkMinute = Convert.ToInt32(span.TotalHours);
                            DateTime InMineTime = Convert.ToDateTime(rows[i]["InTime"]);
                            if (InMineTime.Hour >= 7 && InMineTime.Hour <= 14)
                            {
                                SC_InMineNum++;
                                //SC_WorkTime += WorkMinute;
                            }
                            else if (InMineTime.Hour >= 15 && InMineTime.Hour <= 22)
                            {
                                TC_InMineNum++;
                                //TC_WorkTime += WorkMinute;
                            }
                            else
                            {
                                FC_InMineNum++;
                                //FC_WorkTime += WorkMinute;
                            }
                        }

                        //设置新行的值
                        if (FC_InMineNum != 0)
                        {
                            NewRow["FC_InMine"] = FC_InMineNum;
                            //NewRow["FC_WorkTime"] = FC_WorkTime;
                            //NewRow["FC_WorkPoint"] = FC_WorkPoint;
                        }

                        if (SC_InMineNum != 0)
                        {
                            NewRow["SC_InMine"] = SC_InMineNum;
                            //NewRow["SC_WorkTime"] = SC_WorkTime;
                            //NewRow["SC_WorkPoint"] = SC_WorkPoint;
                        }

                        if (TC_InMineNum != 0)
                        {
                            NewRow["TC_InMine"] = TC_InMineNum;
                            //NewRow["TC_WorkTime"] = TC_WorkTime;
                            //NewRow["TC_WorkPoint"] = TC_WorkPoint;
                        }
                    }
                    //添加新行
                    DutyReport.DataSetReport.StatisticDutyTable.Rows.Add(NewRow);
                    //循环日期加一天
                    SeekDate = SeekDate.AddDays(1);
                }
                //将考勤报表对象的参数域传给报表控件
                DutyReportView.ParameterFieldInfo = DutyReport.PFields;
                //将考勤报表对象的报表传给报表控件
                DutyReportView.ReportSource = DutyReport.Report;

                DutyReport = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "考勤统计-班次统计", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_MounthSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string ReportTimeStr = "";
                string SQLTimeStr = "";
                DateTime StartDay = new DateTime();
                DateTime EndDay = new DateTime();
                //根据用户选择初始化时间信息变量
                if (!InitTimeInfo(ref ReportTimeStr, ref SQLTimeStr, ref StartDay, ref EndDay))
                    return;

                TimeSpan ts_Temp = EndDay - StartDay;
                if (ts_Temp.TotalDays > 31)
                {
                    MessageBox.Show("对不起，标准统计的总天数不能超过31天。请重新选择。","考勤统计-标准统计");
                    return;
                }
                //填充日期表头的字符数组
                string[] DN_List = new string[31];
                DateTime temp_SeekDate = StartDay;
                for (int DN_Seek = 0; DN_Seek < DN_List.Length; DN_Seek++)
                {
                    if (temp_SeekDate.CompareTo(EndDay) <= 0)
                    {
                        DN_List[DN_Seek] = temp_SeekDate.Day.ToString();
                        //循环日期加一天
                        temp_SeekDate = temp_SeekDate.AddDays(1);
                    }
                    else
                    {
                        DN_List[DN_Seek] = "";
                    }
                }
                //得到属于这个部门的所有人员
                DataRow[] row_Person = DB_Service.MainDataSet.Tables["PersonTable"].Select("Department = '" + com_Department.Text + "'");
                //创建一个考勤报表对象
                ReportStatistic_DutyMounth DutyMounthReport = new ReportStatistic_DutyMounth(Global.MapName + com_Department.Text + "考勤报表", "部门总人数：" + row_Person.Length.ToString(), ReportTimeStr, DN_List[0], DN_List[1], DN_List[2], DN_List[3], DN_List[4], DN_List[5], DN_List[6], DN_List[7], DN_List[8], DN_List[9], DN_List[10], DN_List[11], DN_List[12], DN_List[13], DN_List[14], DN_List[15], DN_List[16], DN_List[17], DN_List[18], DN_List[19], DN_List[20], DN_List[21], DN_List[22], DN_List[23], DN_List[24], DN_List[25], DN_List[26], DN_List[27], DN_List[28], DN_List[29], DN_List[30]);
                //将这个部门的人都填充进入
                for (int i = 0; i < row_Person.Length; i++)
                {
                    //得到这个人的职务
                    DataRow[] row_WorkType = DB_Service.MainDataSet.Tables["WorkTypeTable"].Select("WorkTypeName = '" + row_Person[i]["WorkType"].ToString() + "'");
                    if (row_WorkType.Length > 0)
                    {
                        string PID = row_Person[i]["PID"].ToString();
                        int NeedWorkHour = Convert.ToInt32(row_WorkType[0]["NeedWorkHour"]);
                        DataRow NewRow = DutyMounthReport.DataSetReport.StatisticDutyMounthTable.NewRow();
                        NewRow["Name"] = row_Person[i]["Name"];
                        NewRow["PID"] = PID;
                        //得到指定人员在指定月份内所有的考勤记录 按时间排列
                        DataTable temp_PersonDuty = DB_Service.GetTable("temp_PersonDuty", "select DutyTable.Date,DutyTable.InTime,DutyTable.OutTime from DutyTable,CardTable where CardTable.PID = '" + PID + "' and CardTable.CardID = DutyTable.CardID" + SQLTimeStr + " order by DutyTable.Date");
                        int AbleDay = 0;//有效天数
                        int UnableDay = 0;//无效天数
                        DateTime SeekDate = StartDay;
                        //从第一个位置到最后一个位置循环
                        for (int j = 1; j <= ts_Temp.TotalDays; j++)
                        {
                            //取出考勤记录中这一天的记录
                            DataRow[] row_Day = temp_PersonDuty.Select("Date = '" + SeekDate + "'");
                            if (row_Day.Length > 0)
                            {
                                //有，则判断是不是22点后进入的。有则算明天的。否则为今天的
                                int key_InTime = Convert.ToDateTime(row_Day[0]["InTime"]).Hour;
                                if (key_InTime >= 22)
                                {
                                    //有 则算入第二天入井
                                    //if (j + 1 <= MaxDayInMonth)
                                    //InMineDay = Convert.ToDateTime(temp_PersonDuty[j]["Date"]).Day + 1;
                                }
                                else
                                {
                                    //其余情况为当天入井，则把所有在洞内的时间相加
                                    double InMineMinute = 0;
                                    for (int k = 0; k < row_Day.Length; k++)
                                    {
                                        if (row_Day[k]["OutTime"] != DBNull.Value)
                                        {
                                            TimeSpan ts = Convert.ToDateTime(row_Day[k]["OutTime"]) - Convert.ToDateTime(row_Day[k]["InTime"]);
                                            InMineMinute += ts.TotalMinutes;
                                        }
                                    }
                                    if (InMineMinute / 60.0 >= NeedWorkHour)
                                    {
                                        //达到此人的规定时间
                                        NewRow["D" + j] = "√";
                                        AbleDay++;
                                    }
                                    else
                                    {
                                        //未达到此人的规定时间
                                        NewRow["D" + j] = "X";
                                        UnableDay++;
                                    }
                                }
                            }
                            //循环日期加一天
                            SeekDate = SeekDate.AddDays(1);
                        }
                        NewRow["AbleDay"] = AbleDay;
                        NewRow["UnableDay"] = UnableDay;
                        //添加新行
                        DutyMounthReport.DataSetReport.StatisticDutyMounthTable.Rows.Add(NewRow);
                    }
                }
                //将考勤报表对象的参数域传给报表控件
                DutyReportView.ParameterFieldInfo = DutyMounthReport.PFields;
                //将考勤报表对象的报表传给报表控件
                DutyReportView.ReportSource = DutyMounthReport.Report;

                DutyMounthReport = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "考勤统计-标准统计", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 根据用户选择初始化考勤统计的时间信息
        /// </summary>
        /// <param name="ReportTimeStr">报表显示的时间字串</param>
        /// <param name="SQLTimeStr">时间范围的SQL语句</param>
        /// <param name="StartDay">开始时间</param>
        /// <param name="EndDay">结束时间</param>
        /// <returns>初始化成功与否</returns>
        private bool InitTimeInfo(ref string ReportTimeStr, ref string SQLTimeStr, ref DateTime StartDay, ref DateTime EndDay)
        {
            if (radioButton3.Checked)
            {
                //任意时间段统计
                if (dateTimePicker3.Value.Date.CompareTo(dateTimePicker4.Value.Date) <= 0)
                {
                    ReportTimeStr = "考勤时间：" + dateTimePicker3.Value.Year + "年" + dateTimePicker3.Value.Month + "月" + dateTimePicker3.Value.Day + "日 到 " + dateTimePicker4.Value.Year + "年" + dateTimePicker4.Value.Month + "月" + dateTimePicker4.Value.Day + "日";
                    SQLTimeStr = " and Date >= '" + dateTimePicker3.Value.Date + "' and Date <= '" + dateTimePicker4.Value.Date + "'";
                    StartDay = dateTimePicker3.Value.Date;
                    EndDay = dateTimePicker4.Value.Date;
                }
                else
                {
                    MessageBox.Show("对不起。您选择的终止日期小于起始日期，这不是一个有效的时间段。\n\n请选择一个正确的时间段。", "考勤统计", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else if (radioButton1.Checked)
            {
                //日统计
                ReportTimeStr = "考勤时间：" + dateTimePicker5.Value.Year + "年" + dateTimePicker5.Value.Month + "月" + dateTimePicker5.Value.Day + "日";
                SQLTimeStr = " and Date = '" + dateTimePicker5.Value.Date + "'";
                StartDay = dateTimePicker5.Value.Date;
                EndDay = dateTimePicker5.Value.Date;
            }
            else
            {
                //月统计
                ReportTimeStr = "考勤时间：" + dateTimePicker5.Value.Year + "年" + dateTimePicker5.Value.Month + "月";
                if (com_StartMonthDay.SelectedIndex == 0)
                {
                    //如果起始日期为1号，则按默认月统计
                    SQLTimeStr = " and DATEPART(year,Date) = " + dateTimePicker5.Value.Year + " and DATEPART(month,Date) = " + dateTimePicker5.Value.Month;
                    StartDay = new DateTime(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month, 1);
                    EndDay = new DateTime(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month, DateTime.DaysInMonth(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month));
                }
                else
                {
                    //如果起始日期为指定日，则从本月指定日期到下月指定日期的前一日
                    SQLTimeStr = " and Date >= '" + dateTimePicker5.Value.Year.ToString() + "-" + dateTimePicker5.Value.Month.ToString() + "-" + com_StartMonthDay.Text + " 0:00:00' and Date <= '" + dateTimePicker5.Value.Year.ToString() + "-" + Convert.ToString(dateTimePicker5.Value.Month + 1) + "-" + com_StartMonthDay.SelectedIndex.ToString() + " 0:00:00'";
                    try
                    {
                        StartDay = new DateTime(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month, com_StartMonthDay.SelectedIndex + 1);
                        EndDay = StartDay.AddMonths(1).AddDays(-1);
                    }
                    catch
                    {
                        MessageBox.Show("对不起，您选定的" + dateTimePicker5.Value.Month.ToString() + "月没有" + Convert.ToString(com_StartMonthDay.SelectedIndex + 1) + "天，请重新选择。", "按月统计", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
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

        #region 考勤分析

        private void btn_Analysics_Click(object sender, EventArgs e)
        {
            DataRow[] rowPerson = null;
            int CardID = 0;
            string SQLTimeStr = "";
            string ReportTimeStr = "";
            double TotalDay = 0;
            string AnalysicsText = "";
            double TotalInMine = 0;
            double TotalWorkTime = 0;
            double TotalWorkPoint = 0;
            string MapViewTitle1 = "";
            string MapViewRow1 = "";
            string MapViewTitle2 = "";
            string MapViewRow2 = "";
            int LoopTimes = 0;
            double WorkPointKey = 1;

            try
            {
                switch (label_AnalysicsKey.Text)
                {
                    case "工号：":
                        if (tex_Analysics.Text.Trim() != "")
                        {
                            rowPerson = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + tex_Analysics.Text.Trim() + "'");
                            if (rowPerson.Length > 0)
                            {
                                DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + tex_Analysics.Text.Trim() + "'");
                                if (rows_Card.Length > 0)
                                {
                                    CardID = Convert.ToInt32(rows_Card[0]["CardID"]);
                                }
                                else
                                {
                                    MessageBox.Show("对不起，这个员工没有绑定任何卡片，故无法获得其考勤信息。请您先对此员工进行卡片绑定后再继续。", "考勤分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("对不起，没有这个员工。", "考勤分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case "卡号：":
                        if (tex_Analysics.Text.Trim() != "")
                        {
                            DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + tex_Analysics.Text.Trim());
                            if (rows_Card.Length > 0)
                            {
                                rowPerson = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + rows_Card[0]["PID"].ToString() + "'");
                                if (rowPerson.Length > 0)
                                {
                                    CardID = Convert.ToInt32(tex_Analysics.Text.Trim());
                                }
                                else
                                {
                                    MessageBox.Show("对不起，没有这个员工。", "考勤分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("对不起，没有这张卡片。", "考勤分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case "姓名：":
                        if (tex_Analysics.Text.Trim() != "")
                        {
                            rowPerson = DB_Service.MainDataSet.Tables["PersonTable"].Select("Name = '" + tex_Analysics.Text.Trim() + "'");
                            if (rowPerson.Length > 0)
                            {
                                DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + rowPerson[0]["PID"].ToString() + "'");
                                if (rows_Card.Length > 0)
                                {
                                    CardID = Convert.ToInt32(rows_Card[0]["CardID"]);
                                }
                                else
                                {
                                    MessageBox.Show("对不起，这个员工没有绑定任何卡片，故无法获得其考勤信息。请您先对此员工进行卡片绑定后再继续。", "考勤分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("对不起，没有这个员工。", "考勤分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                        break;
                }

                DataRow[] WorkTypeRows = DB_Service.MainDataSet.Tables["WorkTypeTable"].Select("WorkTypeName = '" + rowPerson[0]["WorkType"].ToString() + "'");
                if (WorkTypeRows.Length > 0)
                {
                    DataRow[] WPRows = DB_Service.MainDataSet.Tables["WPTable"].Select("WPName = '" + WorkTypeRows[0]["WPName"].ToString() + "'");
                    if (WPRows.Length > 0)
                    {
                        //计算工分系数
                        WorkPointKey = (Convert.ToDouble(WPRows[0]["WPTimeMinute"]) / Convert.ToDouble(WPRows[0]["Point"]));
                    }
                    else
                    {
                        //找不到指定的工分方案
                        //MessageBox.Show("对不起，这个员工所属的职务没有工分方案。则程序无法根据其工时计算其工分。\n\n若要避免此类问题的发生，请在职务管理中完善职务信息。", "考勤分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //return;
                    }
                }
                else
                {
                    //找不到指定的职务
                    MessageBox.Show("对不起，这个员工的职务不存在，请在人员管理中完善人员信息。", "考勤分析", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;
                }


                if (radio_AnalysicsByDay.Checked)
                {
                    //按日分析
                    ReportTimeStr = dateTimePicker9.Value.Year + "年" + dateTimePicker9.Value.Month + "月";
                    SQLTimeStr = " and DATEPART(year,Date) = " + dateTimePicker9.Value.Year + " and DATEPART(month,Date) = " + dateTimePicker9.Value.Month;
                    TotalDay = DateTime.DaysInMonth(dateTimePicker9.Value.Year, dateTimePicker9.Value.Month);
                    LoopTimes = Convert.ToInt32(TotalDay);
                    MapViewRow1 = "日期";
                    MapViewRow2 = "日期";
                }
                else
                {
                    //按月分析
                    ReportTimeStr = com_Year.Text + "年";
                    SQLTimeStr = " and DATEPART(year,Date) = " + com_Year.Text;
                    for (int i = 1; i <= 12; i++)
                    {
                        TotalDay += DateTime.DaysInMonth(dateTimePicker9.Value.Year, i);
                    }
                    LoopTimes = 12;
                    MapViewRow1 = "月份";
                    MapViewRow2 = "月份";
                }

                MapViewTitle1 = rowPerson[0]["Name"].ToString() + ReportTimeStr + "进入次数柱状图";
                MapViewTitle2 = rowPerson[0]["Name"].ToString() + ReportTimeStr + "工时、工分柱状图";

                string strSerach = "select Date,InTime,OutTime from DutyTable where CardID = " + CardID;

                DataTable tempAnalysicsTable = DB_Service.GetTable("tempAnalysicsTable", strSerach + SQLTimeStr + " order by Date ASC");

                DataSetReport.AnalysicsLineViewTableDataTable tempTable = new DataSetReport.AnalysicsLineViewTableDataTable();
                System.Collections.ArrayList arrayRows = new System.Collections.ArrayList();

                for (int j = 1; j <= LoopTimes; j++)
                {
                    int inMine = 0;
                    int workTime = 0;
                    double workPoint = 0;
                    foreach (DataRow row in tempAnalysicsTable.Rows)
                    {
                        if (radio_AnalysicsByDay.Checked)
                        {
                            if (Convert.ToDateTime(row["Date"]).Day == j)
                            {
                                inMine++;
                                //计算工时
                                if (row["OutTime"] != DBNull.Value)
                                {
                                    TimeSpan span = Convert.ToDateTime(row["OutTime"]) - Convert.ToDateTime(row["InTime"]);
                                    workTime += Convert.ToInt32(span.TotalMinutes);
                                    workPoint += workTime / WorkPointKey;
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToDateTime(row["Date"]).Month == j)
                            {
                                inMine++;
                                //计算工时
                                if (row["OutTime"] != DBNull.Value)
                                {
                                    TimeSpan span = Convert.ToDateTime(row["OutTime"]) - Convert.ToDateTime(row["InTime"]);
                                    workTime += Convert.ToInt32(span.TotalMinutes);
                                    workPoint += workTime / WorkPointKey;
                                }
                            }
                        }
                    }

                    TotalInMine += inMine;
                    TotalWorkTime += workTime;
                    TotalWorkPoint += workPoint;

                    DataRow NewRow = tempTable.NewRow();
                    NewRow["InMine"] = inMine;
                    NewRow["WorkTime"] = workTime;
                    NewRow["WorkPoint"] = workPoint;
                    NewRow["Time"] = j.ToString();
                    arrayRows.Add(NewRow);
                }

                if (radio_AnalysicsByDay.Checked)
                {
                    AnalysicsText = "员工 " + rowPerson[0]["Name"].ToString() + " 在 " + ReportTimeStr + " 共" + TotalDay + "天的时间内累计进入" + TotalInMine + "次，平均每日进入" + Math.Round(TotalInMine / TotalDay, 2) + "次。累计总工时" + TotalWorkTime + "分钟，总工分" + Math.Round(TotalWorkPoint, 1) + "分。平均每日工时" + Math.Round(TotalWorkTime / TotalDay, 1) + "分钟，每日工分" + Math.Round(TotalWorkPoint / TotalDay, 1) + "分。";
                }
                else
                {
                    AnalysicsText = "员工 " + rowPerson[0]["Name"].ToString() + " 在 " + ReportTimeStr + " 共" + TotalDay + "天的时间内累计进入" + TotalInMine + "次，平均每月进入" + Math.Round(TotalInMine / 12, 2) + "次，每日进入" + Math.Round(TotalInMine / TotalDay, 2) + "次。累计总工时" + TotalWorkTime + "分钟，总工分" + Math.Round(TotalWorkPoint, 1) + "分。平均每日工时" + Math.Round(TotalWorkTime / TotalDay, 1) + "分钟，每日工分" + Math.Round(TotalWorkPoint / TotalDay, 1) + "分。";
                }

                ReportAnalysics_Duty AnalysicsReport = new ReportAnalysics_Duty(Global.MapName+"考勤分析报表", "", "", rowPerson[0]["Name"].ToString(), rowPerson[0]["PID"].ToString(), CardID, rowPerson[0]["Department"].ToString(), rowPerson[0]["WorkType"].ToString(), ReportTimeStr, "", AnalysicsText, MapViewTitle1, "进入次数", MapViewRow1, MapViewTitle2, "工时", MapViewRow2);

                //将存储在临时数组中的记录赋予到真正的表中
                foreach (object o in arrayRows)
                {
                    DataRow row = (DataRow)o;
                    AnalysicsReport.DataSetReport.AnalysicsLineViewTable.AddAnalysicsLineViewTableRow(Convert.ToInt32(row["InMine"]), Convert.ToInt32(row["WorkTime"]), Convert.ToDouble(row["WorkPoint"]), row["Time"].ToString());
                }

                tempTable.Clear();
                tempTable.Dispose();
                tempTable = null;
                arrayRows.Clear();
                arrayRows = null;

                //将考勤报表对象的参数域传给报表控件
                AnalysicsReportView.ParameterFieldInfo = AnalysicsReport.PFields;
                //将考勤报表对象的报表传给报表控件
                AnalysicsReportView.ReportSource = AnalysicsReport.Report;

                AnalysicsReport = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "考勤分析");
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
        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_AnalysicsByDay.Checked)
            {
                label10.Text = "月份：";
                dateTimePicker9.Format = DateTimePickerFormat.Custom;
                dateTimePicker9.Visible = true;
                linkLabel9.Visible = true;
                linkLabel10.Visible = true;
                linkLabel11.Visible = false;
                linkLabel12.Visible = false;
                com_Year.Visible = false;
            }
        }

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_AnalysicsByMonth.Checked)
            {
                label10.Text = "年份：";
                dateTimePicker9.Visible = false;
                linkLabel9.Visible = false;
                linkLabel10.Visible = false;
                linkLabel11.Visible = true;
                linkLabel12.Visible = true;
                com_Year.Visible = true;
                com_Year.SelectedIndex = 1;
            }
        }

        private void radioButton19_CheckedChanged(object sender, EventArgs e)
        {
            label_AnalysicsKey.Text = "工号：";
        }

        private void radioButton20_CheckedChanged(object sender, EventArgs e)
        {
            label_AnalysicsKey.Text = "卡号：";
        }

        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {
            label_AnalysicsKey.Text = "姓名：";
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker9.Value = DateTime.Now.AddMonths(-1);
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker9.Value = DateTime.Now;
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            com_Year.Text = (DateTime.Now.AddYears(-1)).Year.ToString();
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            com_Year.Text = DateTime.Now.Year.ToString();
        }

        #endregion
    }
}