using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using PersonPosition.StaticService;
using PersonPosition.Common;
using PersonPosition.Model;

namespace PersonPosition.View
{
    public partial class FrmAlarm : Form
    {
        private DataTable TempAlarmTable;
        private bool IsSystemEvent = false;
        private MainForm mainform;

        public FrmAlarm(MainForm _mainform)
        {
            InitializeComponent();

            this.Tag = this.MainPanel;
            this.mainform = _mainform;

            TempAlarmTable = new DataTable();

            //所有选项卡初始化
            //根据当前选中的选项卡，初始化下拉框内容
            InitCombox();
            //根据当前选中的选项卡，初始化DataGridView
            InitDataGrid();
            //初始化所有控件的默认显示
            InitAllControl(null);
            //根据全局变量IsUseHongWai初始化红外的显示
            if (!Global.IsUseHongWai)
                tabControl1.TabPages.RemoveAt(4);
        }

        #region 提供的通用服务

        /// <summary>
        /// 显示指定员工的报警信息
        /// </summary>
        /// <param name="PID"></param>
        public void ShowPersonSendAlarmByPID(string PID)
        {
            tex_PIDPersonSend.Text = PID;
        }

        /// <summary>
        /// 显示指定员工的报警信息
        /// </summary>
        /// <param name="CardID"></param>
        public void ShowPersonSendAlarmByCardID(int CardID)
        {
            tex_CardIDPersonSend.Text = CardID.ToString();
        }

        /// <summary>
        /// 显示基站报警信息
        /// </summary>
        public void ShowStationAlarm()
        {
            tabControl1.SelectedIndex = 1;
        }

        /// <summary>
        /// 显示缺电报警信息
        /// </summary>
        public void ShowLowPowerAlarm()
        {
            tabControl1.SelectedIndex = 2;
        }

        /// <summary>
        /// 显示无卡进入报警信息
        /// </summary>
        public void ShowNoCardAlarm()
        {
            tabControl1.SelectedIndex = 4;
        }

        #endregion


        /// <summary>
        /// 根据当前选中的选项卡ID初始化Combox
        /// </summary>
        private void InitCombox()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    com_WorkTypePersonSend.Items.Clear();
                    com_WorkTypePersonSend.Items.Add("全部");
                    try
                    {
                        foreach (DataRow row in DB_Service.MainDataSet.Tables["WorkTypeTable"].Rows)
                        {
                            com_WorkTypePersonSend.Items.Add(row["WorkTypeName"].ToString());
                        }
                    }
                    catch
                    {  }
                    com_DepartmentPersonSend.Items.Clear();
                    com_DepartmentPersonSend.Items.Add("全部");
                    try
                    {
                        foreach (DataRow row in DB_Service.MainDataSet.Tables["DepartmentTable"].Rows)
                        {
                            com_DepartmentPersonSend.Items.Add(row["DepartmentName"]);
                        }
                    }
                    catch
                    { }
                    break;
                case 1:
                    //故障基站的类型直接写死
                    break;
                case 2:
                    com_PowerCardType.Items.Clear();
                    com_PowerCardType.Items.Add("全部");
                    for (int i = 0; i < DB_Service.MainDataSet.Tables["CardTypeTable"].Rows.Count; i++)
                    {
                        com_PowerCardType.Items.Add(DB_Service.MainDataSet.Tables["CardTypeTable"].Rows[i]["CardType"].ToString());
                    }
                    break;
            }
        }

        /// <summary>
        /// 根据当前选中的选项卡ID初始化DataGrid
        /// </summary>
        private void InitDataGrid()
        {
            TempAlarmTable.DefaultView.RowFilter = "";
            TempAlarmTable.Clear();

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    string strSQL_PersonSend = "select Alarm_ID as 序号,PID as 工号,CardID as 卡号,IsReaded as 是否已读,Name as 姓名,WorkType as 职务,Department as 部门,SendTime as 发送时间,AlarmType as 信息类型 from AlarmPersonSendTable,PersonTable where PID = (select PID from CardTable where CardID = AlarmPersonSendTable.CardID) order by Alarm_ID ASC";
                    TempAlarmTable = DB_Service.GetTable("AlarmPersonSendTable", strSQL_PersonSend);
                    DataGrid_PersonSend.DataSource = TempAlarmTable;
                    DataGrid_PersonSend.Columns["序号"].Width = 60;
                    DataGrid_PersonSend.Columns["是否已读"].Width = 80;
                    DataGrid_PersonSend.Columns["发送时间"].Width = 150;
                    break;
                case 1:
                    string strSQL_StationError = "select Alarm_ID as 序号,StationID as 基站编号,StationType as 基站类型,StationFunction as 基站功能,Name as 基站名称,IsReaded as 是否已读,ErrorStartTime as 异常开始时刻,ResumeTime as 恢复正常时刻,Reason as 报警原因 from AlarmMachineTable,StationTable where AlarmMachineTable.StationID = StationTable.ID";
                    TempAlarmTable = DB_Service.GetTable("AlarmMachineTable", strSQL_StationError);
                    DataGrid_Machine.DataSource = TempAlarmTable;
                    DataGrid_Machine.Columns["序号"].Width = 60;
                    DataGrid_Machine.Columns["基站名称"].Width = 120;
                    DataGrid_Machine.Columns["是否已读"].Width = 80;
                    DataGrid_Machine.Columns["异常开始时刻"].Width = 150;
                    DataGrid_Machine.Columns["恢复正常时刻"].Width = 150;
                    break;
                case 2:
                    string strSQL_LowPower = "select Alarm_ID as 序号,PersonTable.PID as 工号,CardTable.CardID as 卡号,CardType as 卡片类型,IsReaded as 是否已读,Name as 姓名,WorkType as 职务,Department as 部门,ErrorStartTime as 发送时间 from AlarmPowerTable,PersonTable,CardTable where AlarmPowerTable.CardID = CardTable.CardID and PersonTable.PID = CardTable.PID order by Alarm_ID ASC";
                    TempAlarmTable = DB_Service.GetTable("AlarmPowerTable", strSQL_LowPower);
                    DataGrid_Power.DataSource = TempAlarmTable;
                    DataGrid_Power.Columns["序号"].Width = 60;
                    DataGrid_Power.Columns["是否已读"].Width = 80;
                    DataGrid_Power.Columns["发送时间"].Width = 150;
                    break;
                case 3:
                    string strSQL_AlarmMaxPerson = "select Alarm_ID as 序号,RealPersonNum as 实际人数,AllowPersonNum as 允许人数,Time as 报警时间 from AlarmMaxPersonTable order by Alarm_ID ASC";
                    TempAlarmTable = DB_Service.GetTable("AlarmMaxPerson", strSQL_AlarmMaxPerson);
                    DataGrid_AlarmMaxPerson.DataSource = TempAlarmTable;
                    DataGrid_AlarmMaxPerson.Columns["序号"].Width = 60;
                    DataGrid_AlarmMaxPerson.Columns["报警时间"].Width = 150;
                    string strSQL_AlarmMaxHour = "select Alarm_ID as 序号,PersonTable.PID as 工号,AlarmMaxHourTable.CardID as 卡号,PersonTable.Name as 姓名,PersonTable.WorkType as 职务,PersonTable.Department as 部门,AlarmMaxHourTable.InMineHour as 停留时间,AlarmMaxHourTable.AllowInMineHour as 允许停留时间,AlarmMaxHourTable.Time as 报警时间 from AlarmMaxHourTable,PersonTable,CardTable where AlarmMaxHourTable.CardID = CardTable.CardID and PersonTable.PID = CardTable.PID order by Alarm_ID ASC";
                    TempAlarmTable = DB_Service.GetTable("AlarmMaxHourTable", strSQL_AlarmMaxHour);
                    DataGrid_AlarmMaxHour.DataSource = TempAlarmTable;
                    DataGrid_AlarmMaxHour.Columns["序号"].Width = 60;
                    DataGrid_AlarmMaxHour.Columns["允许停留时间"].Width = 110;
                    DataGrid_AlarmMaxHour.Columns["报警时间"].Width = 150;
                    break;
                case 4:
                    string strSQL_NoCard = "select Alarm_ID as 序号,NoCardNum as 无卡人员人数,IsReaded as 是否已读,Time as 报警时间 from AlarmNoCardTable order by Alarm_ID ASC";
                    TempAlarmTable = DB_Service.GetTable("AlarmNoCardTable", strSQL_NoCard);
                    DataGrid_NoCard.DataSource = TempAlarmTable;
                    DataGrid_NoCard.Columns["序号"].Width = 60;
                    DataGrid_NoCard.Columns["无卡人员人数"].Width = 110;
                    DataGrid_NoCard.Columns["是否已读"].Width = 80;
                    DataGrid_NoCard.Columns["报警时间"].Width = 150;
                    break;
            }
        }

        /// <summary>
        /// 清空当前选中的选项卡上，除指定控件以外的所有控件为默认值
        /// 欲清空此选项卡上的所有控件则传null
        /// </summary>
        /// <param name="UnCleanControl"></param>
        private void InitAllControl(object UnCleanControl)
        {
            IsSystemEvent = true;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (tex_PIDPersonSend != UnCleanControl)
                        tex_PIDPersonSend.Text = "";
                    if (tex_NamePersonSend != UnCleanControl)
                        tex_NamePersonSend.Text = "";
                    if (tex_CardIDPersonSend != UnCleanControl)
                        tex_CardIDPersonSend.Text = "";
                    if (com_WorkTypePersonSend != UnCleanControl && com_WorkTypePersonSend.Items.Count > 0)
                        com_WorkTypePersonSend.SelectedIndex = 0;
                    if (com_DepartmentPersonSend != UnCleanControl && com_DepartmentPersonSend.Items.Count > 0)
                        com_DepartmentPersonSend.SelectedIndex = 0;
                    if (com_AlarmTypePersonSend != UnCleanControl && com_AlarmTypePersonSend.Items.Count > 0)
                        com_AlarmTypePersonSend.SelectedIndex = 0;
                    break;
                case 1:
                    if (tex_StationIDStationError != UnCleanControl)
                        tex_StationIDStationError.Text = "";
                    if (tex_StationNameStationError != UnCleanControl)
                        tex_StationNameStationError.Text = "";
                    if (com_StationTypeStationError != UnCleanControl && com_StationTypeStationError.Items.Count > 0)
                        com_StationTypeStationError.SelectedIndex = 0;
                    if (com_StationFunctionStationError != UnCleanControl && com_StationFunctionStationError.Items.Count > 0)
                        com_StationFunctionStationError.SelectedIndex = 0;
                    break;
                case 2:
                    if (textBox_PowerCardID != UnCleanControl)
                        textBox_PowerCardID.Text = "";
                    if (com_PowerCardType != UnCleanControl && com_PowerCardType.Items.Count > 0)
                        com_PowerCardType.SelectedIndex = 0;
                    break;
            }
            IsSystemEvent = false;
        }

        #region 公共控件的事件

        private void btn_ShowALL_Click(object sender, EventArgs e)
        {
            InitAllControl(null);
            this.TempAlarmTable.DefaultView.RowFilter = "";
        }

        private void btn_ShowUnReadInfo_Click(object sender, EventArgs e)
        {
            InitAllControl(null);
            this.TempAlarmTable.DefaultView.RowFilter = "是否已读 = 'False'";
        }

        private void btn_SetReaded_Click(object sender, EventArgs e)
        {
            DataTable tempTable;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (DataGrid_PersonSend.SelectedRows.Count > 0)
                    {
                        tempTable = DB_Service.GetTable("AlarmPersonSendTable", "select * from AlarmPersonSendTable");
                        for (int i = 0; i < DataGrid_PersonSend.SelectedRows.Count; i++)
                        {
                            if (!Convert.ToBoolean(DataGrid_PersonSend.SelectedRows[i].Cells["是否已读"].Value))
                            {
                                tempTable.Select("Alarm_ID = " + DataGrid_PersonSend.SelectedRows[i].Cells["序号"].Value)[0]["IsReaded"] = true;
                            }
                        }
                        //将AlarmPersonSendTable中的更新提交至数据库
                        int tempNum = DB_Service.UpdateDBFromTable(tempTable);
                        if (tempNum > 0)
                        {
                            for (int j = 0; j < DataGrid_PersonSend.SelectedRows.Count; j++)
                            {
                                DataGrid_PersonSend.SelectedRows[j].Cells["是否已读"].Value = true;
                            }
                            Global.State_UnReadPersonMes -= tempNum;
                            if (Global.State_UnReadPersonMes < 0)
                            {
                                Global.State_UnReadPersonMes = 0;
                            }
                        }
                    }
                    break;
                case 1:
                    if (DataGrid_Machine.SelectedRows.Count > 0)
                    {
                        tempTable = DB_Service.GetTable("AlarmMachineTable", "select * from AlarmMachineTable");
                        for (int i = 0; i < DataGrid_Machine.SelectedRows.Count; i++)
                        {
                            if (!Convert.ToBoolean(DataGrid_Machine.SelectedRows[i].Cells["是否已读"].Value))
                            {
                                tempTable.Select("Alarm_ID = " + DataGrid_Machine.SelectedRows[i].Cells["序号"].Value)[0]["IsReaded"] = true;
                            }
                        }
                        //将AlarmMachineTable中的更新提交至数据库
                        int tempNum1 = DB_Service.UpdateDBFromTable(tempTable);
                        if (tempNum1 > 0)
                        {
                            for (int j = 0; j < DataGrid_Machine.SelectedRows.Count; j++)
                            {
                                DataGrid_Machine.SelectedRows[j].Cells["是否已读"].Value = true;
                            }
                        }
                    }
                    break;
                case 2:
                    if (DataGrid_Power.SelectedRows.Count > 0)
                    {
                        tempTable = DB_Service.GetTable("AlarmPowerTable", "select * from AlarmPowerTable");
                        for (int i = 0; i < DataGrid_Power.SelectedRows.Count; i++)
                        {
                            if (!Convert.ToBoolean(DataGrid_Power.SelectedRows[i].Cells["是否已读"].Value))
                            {
                                tempTable.Select("Alarm_ID = " + DataGrid_Power.SelectedRows[i].Cells["序号"].Value)[0]["IsReaded"] = true;
                            }
                        }
                        //将AlarmPowerTable中的更新提交至数据库
                        int tempNum = DB_Service.UpdateDBFromTable(tempTable);
                        if (tempNum > 0)
                        {
                            for (int j = 0; j < DataGrid_Power.SelectedRows.Count; j++)
                            {
                                DataGrid_Power.SelectedRows[j].Cells["是否已读"].Value = true;
                            }
                            Global.State_UnReadLowPower -= tempNum;
                            if (Global.State_UnReadLowPower < 0)
                            {
                                Global.State_UnReadLowPower = 0;
                            }
                        }
                    }
                    break;
                case 4:
                    if (DataGrid_NoCard.SelectedRows.Count > 0)
                    {
                        tempTable = DB_Service.GetTable("AlarmNoCardTable", "select * from AlarmNoCardTable");
                        for (int i = 0; i < DataGrid_NoCard.SelectedRows.Count; i++)
                        {
                            if (!Convert.ToBoolean(DataGrid_NoCard.SelectedRows[i].Cells["是否已读"].Value))
                            {
                                tempTable.Select("Alarm_ID = " + DataGrid_NoCard.SelectedRows[i].Cells["序号"].Value)[0]["IsReaded"] = true;
                            }
                        }
                        //将AlarmNoCardTable中的更新提交至数据库
                        int tempNum = DB_Service.UpdateDBFromTable(tempTable);
                        if (tempNum > 0)
                        {
                            for (int j = 0; j < DataGrid_NoCard.SelectedRows.Count; j++)
                            {
                                DataGrid_NoCard.SelectedRows[j].Cells["是否已读"].Value = true;
                            }
                        }
                    }
                    break;
            }
        }

        private void btn_SetUnRead_Click(object sender, EventArgs e)
        {
            DataTable tempTable;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (DataGrid_PersonSend.SelectedRows.Count > 0)
                    {
                        tempTable = DB_Service.GetTable("AlarmPersonSendTable", "select * from AlarmPersonSendTable");
                        for (int i = 0; i < DataGrid_PersonSend.SelectedRows.Count; i++)
                        {
                            if (Convert.ToBoolean(DataGrid_PersonSend.SelectedRows[i].Cells["是否已读"].Value))
                            {
                                tempTable.Select("Alarm_ID = " + DataGrid_PersonSend.SelectedRows[i].Cells["序号"].Value)[0]["IsReaded"] = false;
                            }
                        }
                        //将AlarmPersonSendTable中的更新提交至数据库
                        int tempNum = DB_Service.UpdateDBFromTable(tempTable);
                        if (tempNum > 0)
                        {
                            for (int j = 0; j < DataGrid_PersonSend.SelectedRows.Count; j++)
                            {
                                DataGrid_PersonSend.SelectedRows[j].Cells["是否已读"].Value = false;
                            }
                            Global.State_UnReadPersonMes += tempNum;
                        }
                    }
                    break;
                case 1:
                    if (DataGrid_Machine.SelectedRows.Count > 0)
                    {
                        tempTable = DB_Service.GetTable("AlarmMachineTable", "select * from AlarmMachineTable");
                        for (int i = 0; i < DataGrid_Machine.SelectedRows.Count; i++)
                        {
                            if (Convert.ToBoolean(DataGrid_Machine.SelectedRows[i].Cells["是否已读"].Value))
                            {
                                tempTable.Select("Alarm_ID = " + DataGrid_Machine.SelectedRows[i].Cells["序号"].Value)[0]["IsReaded"] = false;
                            }
                        }
                        //将AlarmMachineTable中的更新提交至数据库
                        int tempNum1 = DB_Service.UpdateDBFromTable(tempTable);
                        if (tempNum1 > 0)
                        {
                            for (int j = 0; j < DataGrid_Machine.SelectedRows.Count; j++)
                            {
                                DataGrid_Machine.SelectedRows[j].Cells["是否已读"].Value = false;
                            }
                        }
                    }
                    break;
                case 2:
                    if (DataGrid_Power.SelectedRows.Count > 0)
                    {
                        tempTable = DB_Service.GetTable("AlarmPowerTable", "select * from AlarmPowerTable");
                        for (int i = 0; i < DataGrid_Power.SelectedRows.Count; i++)
                        {
                            if (Convert.ToBoolean(DataGrid_Power.SelectedRows[i].Cells["是否已读"].Value))
                            {
                                tempTable.Select("Alarm_ID = " + DataGrid_Power.SelectedRows[i].Cells["序号"].Value)[0]["IsReaded"] = false;
                            }
                        }
                        //将AlarmPowerTable中的更新提交至数据库
                        int tempNum = DB_Service.UpdateDBFromTable(tempTable);
                        if (tempNum > 0)
                        {
                            for (int j = 0; j < DataGrid_Power.SelectedRows.Count; j++)
                            {
                                DataGrid_Power.SelectedRows[j].Cells["是否已读"].Value = false;
                            }
                            Global.State_UnReadLowPower += tempNum;
                        }
                    }
                    break;
                case 4:
                    if (DataGrid_NoCard.SelectedRows.Count > 0)
                    {
                        tempTable = DB_Service.GetTable("AlarmNoCardTable", "select * from AlarmNoCardTable");
                        for (int i = 0; i < DataGrid_NoCard.SelectedRows.Count; i++)
                        {
                            if (Convert.ToBoolean(DataGrid_NoCard.SelectedRows[i].Cells["是否已读"].Value))
                            {
                                tempTable.Select("Alarm_ID = " + DataGrid_NoCard.SelectedRows[i].Cells["序号"].Value)[0]["IsReaded"] = false;
                            }
                        }
                        //将AlarmPowerTable中的更新提交至数据库
                        int tempNum = DB_Service.UpdateDBFromTable(tempTable);
                        if (tempNum > 0)
                        {
                            for (int j = 0; j < DataGrid_NoCard.SelectedRows.Count; j++)
                            {
                                DataGrid_NoCard.SelectedRows[j].Cells["是否已读"].Value = false;
                            }
                        }
                    }
                    break;
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            DataTable tempTable = null;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (DataGrid_PersonSend.SelectedRows.Count > 0)
                    {
                        if (MessageBox.Show("您确定要删除这些报警信息吗？", "删除报警信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            tempTable = DB_Service.GetTable("AlarmPersonSendTable", "select * from AlarmPersonSendTable");
                            for (int i = 0; i < DataGrid_PersonSend.SelectedRows.Count; i++)
                            {
                                DataRow[] tempRows = tempTable.Select("Alarm_ID = " + DataGrid_PersonSend.SelectedRows[i].Cells["序号"].Value);
                                if (tempRows.Length > 0)
                                {
                                    if (!Convert.ToBoolean(tempRows[0]["IsReaded"]))
                                    {
                                        Global.State_UnReadPersonMes--;
                                        if (Global.State_UnReadPersonMes < 0)
                                        {
                                            Global.State_UnReadPersonMes = 0;
                                        }
                                    }
                                    tempRows[0].Delete();
                                }
                            }
                        }
                    }
                    break;
                case 1:
                    if (DataGrid_Machine.SelectedRows.Count > 0)
                    {
                        if (MessageBox.Show("您确定要删除这些报警信息吗？", "删除报警信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            tempTable = DB_Service.GetTable("AlarmMachineTable", "select * from AlarmMachineTable");
                            for (int i = 0; i < DataGrid_Machine.SelectedRows.Count; i++)
                            {
                                DataRow[] tempRows = tempTable.Select("Alarm_ID = " + DataGrid_Machine.SelectedRows[i].Cells["序号"].Value);
                                if (tempRows.Length > 0)
                                {
                                    tempRows[0].Delete();
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    if (DataGrid_Power.SelectedRows.Count > 0)
                    {
                        if (MessageBox.Show("您确定要删除这些报警信息吗？", "删除报警信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            tempTable = DB_Service.GetTable("AlarmPowerTable", "select * from AlarmPowerTable");
                            for (int i = 0; i < DataGrid_Power.SelectedRows.Count; i++)
                            {
                                DataRow[] tempRows = tempTable.Select("Alarm_ID = " + DataGrid_Power.SelectedRows[i].Cells["序号"].Value);
                                if (tempRows.Length > 0)
                                {
                                    if (!Convert.ToBoolean(tempRows[0]["IsReaded"]))
                                    {
                                        Global.State_UnReadLowPower--;
                                        if (Global.State_UnReadLowPower < 0)
                                        {
                                            Global.State_UnReadLowPower = 0;
                                        }
                                    }
                                    tempRows[0].Delete();
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    if (DataGrid_AlarmMaxPerson.SelectedRows.Count > 0)
                    {
                        if (MessageBox.Show("您确定要删除这些报警信息吗？", "删除报警信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            tempTable = DB_Service.GetTable("AlarmMaxPersonTable", "select * from AlarmMaxPersonTable");
                            for (int i = 0; i < DataGrid_AlarmMaxPerson.SelectedRows.Count; i++)
                            {
                                DataRow[] tempRows = tempTable.Select("Alarm_ID = " + DataGrid_AlarmMaxPerson.SelectedRows[i].Cells["序号"].Value);
                                if (tempRows.Length > 0)
                                {
                                    tempRows[0].Delete();
                                }
                            }
                        }
                    }
                    if (DataGrid_AlarmMaxHour.SelectedRows.Count > 0)
                    {
                        if (MessageBox.Show("您确定要删除这些报警信息吗？", "删除报警信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            tempTable = DB_Service.GetTable("AlarmMaxHourTable", "select * from AlarmMaxHourTable");
                            for (int i = 0; i < DataGrid_AlarmMaxHour.SelectedRows.Count; i++)
                            {
                                DataRow[] tempRows = tempTable.Select("Alarm_ID = " + DataGrid_AlarmMaxHour.SelectedRows[i].Cells["序号"].Value);
                                if (tempRows.Length > 0)
                                {
                                    tempRows[0].Delete();
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    if (DataGrid_NoCard.SelectedRows.Count > 0)
                    {
                        if (MessageBox.Show("您确定要删除这些报警信息吗？", "删除报警信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            tempTable = DB_Service.GetTable("AlarmNoCardTable", "select * from AlarmNoCardTable");
                            for (int i = 0; i < DataGrid_NoCard.SelectedRows.Count; i++)
                            {
                                DataRow[] tempRows = tempTable.Select("Alarm_ID = " + DataGrid_NoCard.SelectedRows[i].Cells["序号"].Value);
                                if (tempRows.Length > 0)
                                {
                                    tempRows[0].Delete();
                                }
                            }
                        }
                    }
                    break;
            }
            //将tempTable中的更新提交至数据库
            if (tempTable != null)
            {
                int tempNum = DB_Service.UpdateDBFromTable(tempTable);
                if (tempNum > 0)
                {
                    //刷新列表
                    InitDataGrid();
                }
                else
                {
                    MessageBox.Show("删除信息失败！\n请确保数据库连接正确。", "报警信息管理");
                }
            }
        }

        private void btn_SenderInfo_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (DataGrid_PersonSend.SelectedRows.Count > 0)
                    {
                        try
                        {
                            this.mainform.MainBtn_Person_Click(null, null);
                            this.mainform.frmPerson.ShowPersonInfoByPID(DataGrid_PersonSend.SelectedRows[0].Cells["工号"].Value.ToString());
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
                    break;
            }
        }

        private void btn_SendStation_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    if (DataGrid_Machine.SelectedRows.Count > 0)
                    {
                        try
                        {
                            this.mainform.MainBtn_Machine_Click(null, null);
                            this.mainform.frmMachine.ShowStationInfoByID(Convert.ToInt32(DataGrid_Machine.SelectedRows[0].Cells["基站编号"].Value));
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
                    break;
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataGridViewPrinter DGVP;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    DGVP = new DataGridViewPrinter(this.DataGrid_PersonSend, "人员发送报警统计", "", "", "", "", false, this.DataGrid_PersonSend.ColumnCount);
                    break;
                case 1:
                    DGVP = new DataGridViewPrinter(this.DataGrid_Machine, "故障基站报警统计", "", "", "", "", false, this.DataGrid_Machine.ColumnCount);
                    break;
                case 2:
                    DGVP = new DataGridViewPrinter(this.DataGrid_Power, "卡片缺电报警统计", "", "", "", "", false, this.DataGrid_Power.ColumnCount);
                    break;
                case 3:
                    switch (MessageBox.Show("您是要打印《超员报警统计》还《超时报警统计》？\n\n 是 - 超员报警统计\n\n 否 - 超时报警统计", "数据表打印", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            DGVP = new DataGridViewPrinter(this.DataGrid_AlarmMaxPerson, "超员报警统计", "", "", "", "", false, this.DataGrid_AlarmMaxPerson.ColumnCount);
                            break;
                        case DialogResult.No:
                            DGVP = new DataGridViewPrinter(this.DataGrid_AlarmMaxHour, "超时报警统计", "", "", "", "", false, this.DataGrid_AlarmMaxHour.ColumnCount);
                            break;
                        default:
                            return;
                    }
                    break;
                default:
                    DGVP = new DataGridViewPrinter(this.DataGrid_NoCard, "无卡人员进入报警统计", "", "", "", "", false, this.DataGrid_NoCard.ColumnCount);
                    break;
            }
            DGVP.Print();
        }

        private void btn_GoToMap_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (DataGrid_PersonSend.SelectedRows.Count > 0)
                    {
                        DataRow[] rows = DB_Service.MainDataSet.Tables["PositionTable"].Select("ID = " + DataGrid_PersonSend.SelectedRows[0].Cells["卡号"].Value);
                        if (rows.Length > 0)
                        {
                            this.mainform.MainBtn_Watch_Click(null, null);
                            this.mainform.ShowPersonToolTipInMap(rows[0]["Name"].ToString(), Convert.ToInt32(rows[0]["ID"]), rows[0]["CardType"].ToString(), Convert.ToDouble(rows[0]["Geo_X"]), Convert.ToDouble(rows[0]["Geo_Y"]));
                        }
                        else
                        {
                            MessageBox.Show("这个员工当前不在区域中。所以您无法在地图中定位。", "报警信息");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先选择一条报警信息。", "报警信息");
                    }
                    break;
                case 1:
                    if (DataGrid_Machine.SelectedRows.Count > 0)
                    {
                        DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + DataGrid_Machine.SelectedRows[0].Cells["基站编号"].Value);
                        if (rows.Length > 0)
                        {
                            this.mainform.MainBtn_Watch_Click(null, null);
                            this.mainform.ShowStationToolTipInMap(rows[0]["Name"].ToString(), Convert.ToInt32(rows[0]["ID"]), rows[0]["StationType"].ToString(),rows[0]["StationFunction"].ToString(), Convert.ToDouble(rows[0]["Geo_X"]), Convert.ToDouble(rows[0]["Geo_Y"]));
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
                    break;
                case 2:
                    if (DataGrid_Power.SelectedRows.Count > 0)
                    {
                        DataRow[] rows = DB_Service.MainDataSet.Tables["PositionTable"].Select("ID = " + DataGrid_Power.SelectedRows[0].Cells["卡号"].Value);
                        if (rows.Length > 0)
                        {
                            this.mainform.MainBtn_Watch_Click(null, null);
                            this.mainform.ShowPersonToolTipInMap(rows[0]["Name"].ToString(), Convert.ToInt32(rows[0]["ID"]), rows[0]["CardType"].ToString(), Convert.ToDouble(rows[0]["Geo_X"]), Convert.ToDouble(rows[0]["Geo_Y"]));
                        }
                        else
                        {
                            MessageBox.Show("这个员工当前不在区域中。所以您无法在地图中定位。", "报警信息");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先选择一条报警信息。", "报警信息");
                    }
                    break;
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            //根据当前选中的选项卡，初始化下拉框内容
            InitCombox();
            //根据当前选中的选项卡，初始化DataGridView
            InitDataGrid();
            //初始化所有控件的默认显示
            InitAllControl(null);
        }

        #endregion

        #region 人员发送选项卡的事件、方法

        private void tex_PIDPersonSend_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_PIDPersonSend.Text == "")
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_PIDPersonSend);
                        this.TempAlarmTable.DefaultView.RowFilter = "工号 = '" + tex_PIDPersonSend.Text + "'";
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_PIDPersonSend.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void tex_NamePersonSend_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_NamePersonSend.Text == "")
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_NamePersonSend);
                        this.TempAlarmTable.DefaultView.RowFilter = "姓名 like '%" + tex_NamePersonSend.Text + "%'";
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_NamePersonSend.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void tex_CardIDPersonSend_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_CardIDPersonSend.Text == "")
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_CardIDPersonSend);
                        this.TempAlarmTable.DefaultView.RowFilter = "卡号 = '" + tex_CardIDPersonSend.Text + "'";
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_CardIDPersonSend.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void com_WorkTypePersonSend_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_WorkTypePersonSend.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_WorkTypePersonSend);
                    this.TempAlarmTable.DefaultView.RowFilter = "职务 = '" + com_WorkTypePersonSend.Text + "'";
                }
            }
        }

        private void com_AlarmTypePersonSend_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_AlarmTypePersonSend.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_AlarmTypePersonSend);
                    this.TempAlarmTable.DefaultView.RowFilter = "信息类型 = '" + com_AlarmTypePersonSend.Text + "'";
                }
            }
        }

        private void com_DepartmentPersonSend_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_DepartmentPersonSend.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_DepartmentPersonSend);
                    this.TempAlarmTable.DefaultView.RowFilter = "部门 = '" + com_DepartmentPersonSend.Text + "'";
                }
            }
        }

        #endregion

        #region 基站报警选项卡的事件、方法

        private void tex_StationIDStationError_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_StationIDStationError.Text == "")
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_StationIDStationError);
                        this.TempAlarmTable.DefaultView.RowFilter = "基站编号 = " + tex_StationIDStationError.Text;
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_StationIDStationError.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void tex_StationNameStationError_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_StationNameStationError.Text == "")
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_StationNameStationError);
                        this.TempAlarmTable.DefaultView.RowFilter = "基站名称 like '%" + tex_StationNameStationError.Text + "%'";
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_StationNameStationError.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void com_StationTypeStationError_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_StationTypeStationError.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_StationTypeStationError);
                    this.TempAlarmTable.DefaultView.RowFilter = "基站类型 = '" + com_StationTypeStationError.Text + "'";
                }
            }
        }

        private void com_StationFunctionStationError_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_StationFunctionStationError.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_StationFunctionStationError);
                    this.TempAlarmTable.DefaultView.RowFilter = "基站功能 = '" + com_StationFunctionStationError.Text + "'";
                }
            }
        }

        #endregion

        #region 缺电报警选项卡的事件、方法

        private void textBox_PowerCardID_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (textBox_PowerCardID.Text == "")
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(textBox_PowerCardID);
                        this.TempAlarmTable.DefaultView.RowFilter = "卡号 = '" + textBox_PowerCardID.Text + "'";
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        textBox_PowerCardID.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void com_PowerCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_PowerCardType.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_PowerCardType);
                    this.TempAlarmTable.DefaultView.RowFilter = "卡片类型 = '" + com_PowerCardType.Text + "'";
                }
            }
        }

        #endregion

        private void DataGrid_PersonSend_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void DataGrid_Machine_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void DataGrid_Power_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void DataGrid_AlarmMaxPerson_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void DataGrid_AlarmMaxHour_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void DataGrid_NoCard_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void btn_HandCheckOut_Click(object sender, EventArgs e)
        {
            if (DataGrid_AlarmMaxHour.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("您确定要强制使 " + DataGrid_AlarmMaxHour.SelectedRows[0].Cells["姓名"].Value.ToString() + " 这位员工离开吗？", "强制离开", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //启动特殊区域监控方案
                    if (!Socket_Service.SendMessage_Safe(Socket_Service.Command_C2S_HandCheckOut, DataGrid_AlarmMaxHour.SelectedRows[0].Cells["卡号"].Value.ToString() , "", "", "", "", "", "", "",""))
                    {
                        MessageBox.Show("强制离开失败。", "强制离开", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}