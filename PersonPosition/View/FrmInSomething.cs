using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PersonPosition.Common;
using PersonPosition.StaticService;

namespace PersonPosition.View
{
    public partial class FrmInSomething : Form
    {
        private DataTable CopyPositionTable;
        private string RowFilter;
        private MainForm mainform;

        #region 功能按钮事件

        private void btn_SearchPosition_Click(object sender, EventArgs e)
        {
            if (dataGV_Table.SelectedRows.Count > 0)
            {
                DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + dataGV_Table.SelectedRows[0].Cells["ID"].Value);
                if (rows_Card.Length > 0)
                {
                    DataRow[] rows = DB_Service.MainDataSet.Tables["PositionTable"].Select("ID = " + Convert.ToInt32(rows_Card[0]["CardID"]));
                    if (rows.Length > 0)
                    {
                        this.mainform.ShowPersonToolTipInMap(rows[0]["Name"].ToString(), Convert.ToInt32(rows[0]["ID"]), rows[0]["CardType"].ToString(), Convert.ToInt32(rows[0]["Geo_X"]), Convert.ToInt32(rows[0]["Geo_Y"]));
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("这个员工当前不在区域中。所以您无法在地图中定位。", "查询实时位置");
                    }
                }
                else
                {
                    MessageBox.Show("这个员工没有绑定任何一张卡片。", "查询实时位置");
                }
            }
            else
            {
                MessageBox.Show("请先选择一个员工。", "查询实时位置");
            }
        }

        private void btn_SearchDuty_Click(object sender, EventArgs e)
        {
            if (dataGV_Table.SelectedRows.Count > 0)
            {
                try
                {
                    this.mainform.MainBtn_Duty_Click(null, null);
                    this.mainform.frmDuty.ShowPersonDutyByCardID(Convert.ToInt32(dataGV_Table.SelectedRows[0].Cells["ID"].Value));
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个员工。", "查询考勤信息");
            }
        }

        private void btn_SearchAlarm_Click(object sender, EventArgs e)
        {
            if (dataGV_Table.SelectedRows.Count > 0)
            {
                this.mainform.MainBtn_Alarm_Click(null, null);
                this.mainform.frmAlarm.ShowPersonSendAlarmByCardID(Convert.ToInt32(dataGV_Table.SelectedRows[0].Cells["ID"].Value));
                this.Close();
            }
            else
            {
                MessageBox.Show("请先选择一个员工。", "查询报警信息");
            }
        }

        private void btn_SearchHistory_Click(object sender, EventArgs e)
        {
            if (dataGV_Table.SelectedRows.Count > 0)
            {
                try
                {
                    try
                    {
                        this.mainform.MainBtn_History_Click(null, null);
                        this.mainform.frmHistory.ShowHistoryByCardID(Convert.ToInt32(dataGV_Table.SelectedRows[0].Cells["ID"].Value));
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个员工。", "查询历史信息");
            }
        }

        private void btn_Info_Click(object sender, EventArgs e)
        {
            if (dataGV_Table.SelectedRows.Count > 0)
            {
                try
                {
                    this.mainform.MainBtn_Person_Click(sender, e);
                    this.mainform.frmPerson.ShowPersonInfoByCardID(Convert.ToInt32(dataGV_Table.SelectedRows[0].Cells["ID"].Value));
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个员工。", "查询历史信息");
            }
        }

        #endregion

        /// <summary>
        /// 特定位置人员显示窗体
        /// </summary>
        /// <param name="frmTitle">窗体标题</param>
        /// <param name="_mainform">主窗体</param>
        /// <param name="_rowFilter">筛选条件</param>
        public FrmInSomething(string frmTitle, MainForm _mainform, string _rowFilter)
        {
            InitializeComponent();
            this.mainform = _mainform;
            this.Text = frmTitle;
            this.RowFilter = _rowFilter;
            //克隆PositionTable
            CopyPositionTable = DB_Service.MainDataSet.Tables["PositionTable"].Clone();
            //数据
            dataGV_Table.DataSource = CopyPositionTable;
            //初始化表
            dataGV_Table.Columns["ID"].HeaderText = "卡号";
            dataGV_Table.Columns["ID"].Width = 52;
            dataGV_Table.Columns["Name"].HeaderText = "姓名";
            dataGV_Table.Columns["Name"].Width = 80;
            dataGV_Table.Columns["CardType"].Visible = false;
            dataGV_Table.Columns["WorkType"].HeaderText = "职务";
            dataGV_Table.Columns["Department"].HeaderText = "部门";
            dataGV_Table.Columns["NearStationID"].HeaderText = "所在基站";
            dataGV_Table.Columns["NearStationID"].Width = 77;
            dataGV_Table.Columns["Area"].HeaderText = "区域";
            dataGV_Table.Columns["InMineTime"].HeaderText = "进入时间";
            dataGV_Table.Columns["InMineTime"].Width = 115;
            dataGV_Table.Columns["InNullRSSITime"].Visible = false;
            dataGV_Table.Columns["Geo_X"].Visible = false;
            dataGV_Table.Columns["Geo_Y"].Visible = false;
            //注册服务器的返回特殊区域内人员事件
            Socket_Service.Event_InArea += new InAreaEventHandler(Socket_Service_Event_InArea);
            //调用刷新按钮
            btn_Refresh_Click(null, null);
        }

        void Socket_Service_Event_InArea(int InSomethingNum, Dictionary<int,int> InSomethingList)
        {
            label_AllNum.Text = "特殊区域内总人数：" + InSomethingNum + " 人";
            //特殊区域不显示进入时间
            dataGV_Table.Columns["InMineTime"].Visible = false;
            //为开始更新控件做准备
            CopyPositionTable.BeginLoadData();
            //更新控件
            CopyPositionTable.Rows.Clear();
            try
            {
                foreach (int CardID in InSomethingList.Keys)
                {
                    DataRow newRow = CopyPositionTable.NewRow();
                    newRow["ID"] = CardID;
                    DataRow rowCard = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + CardID)[0];
                    DataRow rowPerson = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + rowCard["PID"].ToString() + "'")[0];
                    newRow["Name"] = rowPerson["Name"];
                    newRow["CardType"] = rowCard["CardType"];
                    newRow["Department"] = rowPerson["Department"];
                    newRow["WorkType"] = rowPerson["WorkType"];
                    newRow["NearStationID"] = InSomethingList[CardID];
                    CopyPositionTable.Rows.Add(newRow);
                }
            }
            catch
            {   }
            //更新完毕。恢复
            CopyPositionTable.EndLoadData();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            CopyPositionTable = DB_Service.MainDataSet.Tables["PositionTable"].Copy();
            //修改原表会导致DataSource失效，故再赋值一次
            dataGV_Table.DataSource = CopyPositionTable;
            if (this.Text == "特殊区域内人员")
            {
                Socket_Service.SendMessage(Socket_Service.Command_C2S_RequestInArea, "", "", "", "", "", "", "", "","");
            }
            else
            {
                CopyPositionTable.DefaultView.RowFilter = this.RowFilter;
                label_AllNum.Text = this.Text + "数：" + dataGV_Table.Rows.Count.ToString() + " 人";
            }
        }

        private void dataGV_AllInMine_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }
    }
}