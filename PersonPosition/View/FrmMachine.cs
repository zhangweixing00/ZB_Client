using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using PersonPosition.StaticService;
using PersonPosition.Common;


namespace PersonPosition.View
{
    public partial class FrmMachine : Form
    {
        private bool IsSystemEvent = false;
        private MainForm mainform;

        public FrmMachine(MainForm _mainform)
        {
            InitializeComponent();
            this.Tag = this.MainPanel;
            this.mainform = _mainform;

            //根据当前选中的选项卡，初始化下拉框内容
            InitCombox();
            //根据选项卡初始化DataGirdView
            InitMachineView();
            //初始化所有控件的默认显示
            InitAllControl(null);
        }

        #region 提供的通用服务

        /// <summary>
        /// 通过指定的StationID初始化本窗体默认显示的基站
        /// </summary>
        /// <param name="DataGridViewIndex"></param>
        public void ShowStationInfoByID(int stationID)
        {
            DialogStation diglog = new DialogStation(false, stationID,this.mainform);
            diglog.ShowDialog(this);
        }

        #endregion

        /// <summary>
        /// 根据选项卡初始化DataGirdView
        /// </summary>
        private void InitMachineView()
        {
            DB_Service.MainDataSet.Tables["StationTable"].DefaultView.RowFilter = "";
            DB_Service.MainDataSet.Tables["CardTable"].DefaultView.RowFilter = "";
            DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "";

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    label_AllStation.Text = "共有基站 " + DB_Service.MainDataSet.Tables["StationTable"].Rows.Count.ToString() + " 个";
                    DataGridView_Station.DataSource = DB_Service.MainDataSet.Tables["StationTable"];
                    DataGridView_Station.Columns["ID"].HeaderText = "编号";
                    DataGridView_Station.Columns["Name"].HeaderText = "基站名称";
                    DataGridView_Station.Columns["Area"].HeaderText = "所在区域";
                    DataGridView_Station.Columns["IP"].HeaderText = "IP地址";
                    DataGridView_Station.Columns["Port"].HeaderText = "端口";
                    DataGridView_Station.Columns["DutyOrder"].HeaderText = "考勤基站编号";
                    DataGridView_Station.Columns["StationType"].HeaderText = "基站类型";
                    DataGridView_Station.Columns["StationFunction"].HeaderText = "基站功能";
                    DataGridView_Station.Columns["RepairRSSI"].HeaderText = "信号修正";
                    DataGridView_Station.Columns["MaxChannelNum"].HeaderText = "采集器通道数";
                    DataGridView_Station.Columns["CollectChannelIDStr"].HeaderText = "通道编号队列";
                    DataGridView_Station.Columns["MapName"].HeaderText = "地图名称";
                    DataGridView_Station.Columns["FatherStationID"].HeaderText = "父基站编号";
                    DataGridView_Station.Columns["SonStationIDs"].HeaderText = "子基站编号队列";
                    DataGridView_Station.Columns["Geo_X"].HeaderText = "X坐标";
                    DataGridView_Station.Columns["Geo_Y"].HeaderText = "Y坐标";
                    //设置排序列
                    DataGridView_Station.Sort(DataGridView_Station.Columns["ID"], ListSortDirection.Ascending);
                    break;
                case 1:
                    label_AllCard.Text = "共有卡片" + DB_Service.MainDataSet.Tables["CardTable"].Rows.Count.ToString() + "张。其中有" + DB_Service.MainDataSet.Tables["CardTable"].Select("PID is null").Length.ToString() + "张尚未与员工绑定。";
                    //卡片表
                    this.dataGridView_Card.DataSource = DB_Service.MainDataSet.Tables["CardTable"];
                    dataGridView_Card.Columns["CardID"].HeaderText = "卡号";
                    dataGridView_Card.Columns["CardType"].HeaderText = "卡片类型";
                    dataGridView_Card.Columns["PID"].HeaderText = "持卡人工号";
                    //设置排序列
                    dataGridView_Card.Sort(dataGridView_Card.Columns["CardID"], ListSortDirection.Ascending);
                    //人员表
                    dataGridView_Person.DataSource = DB_Service.MainDataSet.Tables["PersonTable"];
                    dataGridView_Person.Columns["PID"].HeaderText = "工号";
                    dataGridView_Person.Columns["Name"].HeaderText = "姓名";
                    dataGridView_Person.Columns["Sex"].HeaderText = "性别";
                    dataGridView_Person.Columns["Age"].HeaderText = "年龄";
                    dataGridView_Person.Columns["Blood"].HeaderText = "血型";
                    dataGridView_Person.Columns["WorkType"].HeaderText = "职务";
                    dataGridView_Person.Columns["Department"].HeaderText = "部门";
                    dataGridView_Person.Columns["Tele"].HeaderText = "电话";
                    dataGridView_Person.Columns["Mobile"].HeaderText = "手机";
                    dataGridView_Person.Columns["PersonKey"].HeaderText = "身份证号";
                    dataGridView_Person.Columns["BirthDay"].HeaderText = "生日";
                    dataGridView_Person.Columns["Email"].HeaderText = "电子邮件";
                    dataGridView_Person.Columns["FamilyAdd"].HeaderText = "家庭住址";
                    dataGridView_Person.Columns["Photo"].HeaderText = "照片";
                    //设置排序列
                    dataGridView_Person.Sort(dataGridView_Person.Columns["PID"], ListSortDirection.Ascending);
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
                    if (tex_StationID != UnCleanControl)
                        tex_StationID.Text = "";
                    if (tex_StationName != UnCleanControl)
                        tex_StationName.Text = "";
                    if (com_StationType != UnCleanControl && com_StationType.Items.Count > 0)
                        com_StationType.SelectedIndex = 0;
                    if (com_StationFunction != UnCleanControl && com_StationFunction.Items.Count > 0)
                        com_StationFunction.SelectedIndex = 0;
                    break;
                case 1:
                    if (tex_SelectCardID != UnCleanControl)
                        tex_SelectCardID.Text = "";
                    if (tex_SelectCardPID != UnCleanControl)
                        tex_SelectCardPID.Text = "";
                    if (com_SelectCardType != UnCleanControl && com_SelectCardType.Items.Count > 0)
                        com_SelectCardType.SelectedIndex = 0;
                    if (tex_SelectPID != UnCleanControl)
                        tex_SelectPID.Text = "";
                    if (tex_SelectName != UnCleanControl)
                        tex_SelectName.Text = "";
                    if (com_SelectDepartment != UnCleanControl && com_SelectDepartment.Items.Count > 0)
                        com_SelectDepartment.SelectedIndex = 0;
                    if (com_SelectWorkType != UnCleanControl && com_SelectWorkType.Items.Count > 0)
                        com_SelectWorkType.SelectedIndex = 0;
                    break;
            }
            IsSystemEvent = false;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            //根据当前选中的选项卡，初始化下拉框内容
            InitCombox();
            //根据当前选中的选项卡，初始化DataGridView
            InitMachineView();
            //初始化所有控件的默认显示
            InitAllControl(null);
        }

        /// <summary>
        /// 根据当前选中的选项卡ID初始化Combox
        /// </summary>
        private void InitCombox()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    //基站类型直接写死
                    break;
                case 1:
                    com_SelectCardType.Items.Clear();
                    com_SelectCardType.Items.Add("全部");
                    try
                    {
                        for (int i = 0; i < DB_Service.MainDataSet.Tables["CardTypeTable"].Rows.Count; i++)
                        {
                            com_SelectCardType.Items.Add(DB_Service.MainDataSet.Tables["CardTypeTable"].Rows[i]["CardType"].ToString());
                        }
                    }
                    catch
                    { }
                    com_SelectWorkType.Items.Clear();
                    com_SelectWorkType.Items.Add("全部");
                    try
                    {
                        foreach (DataRow row in DB_Service.MainDataSet.Tables["WorkTypeTable"].Rows)
                        {
                            com_SelectWorkType.Items.Add(row["WorkTypeName"].ToString());
                        }
                    }
                    catch
                    {  }
                    com_SelectDepartment.Items.Clear();
                    com_SelectDepartment.Items.Add("全部");
                    try
                    {
                        foreach (DataRow row in DB_Service.MainDataSet.Tables["DepartmentTable"].Rows)
                        {
                            com_SelectDepartment.Items.Add(row["DepartmentName"]);
                        }
                    }
                    catch
                    { }
                    break;
            }
        }

        #region 基站选项卡控件事件、方法

        private void tex_StationID_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_StationID.Text == "")
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_StationID);
                        DB_Service.MainDataSet.Tables["StationTable"].DefaultView.RowFilter = "ID = " + tex_StationID.Text;
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_StationID.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void tex_StationName_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_StationName.Text == "")
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_StationName);
                        DB_Service.MainDataSet.Tables["StationTable"].DefaultView.RowFilter = "Name like '%" + tex_StationName.Text + "%'";
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_StationName.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void com_StationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_StationType.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_StationType);
                    DB_Service.MainDataSet.Tables["StationTable"].DefaultView.RowFilter = "StationType = '" + com_StationType.Text + "'";
                }
            }
        }

        private void com_StationFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_StationFunction.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_StationFunction);
                    DB_Service.MainDataSet.Tables["StationTable"].DefaultView.RowFilter = "StationFunction = '" + com_StationFunction.Text + "'";
                }
            }
        }

        private void btn_ShowALL_Click(object sender, EventArgs e)
        {
            InitAllControl(null);
            DB_Service.MainDataSet.Tables["StationTable"].DefaultView.RowFilter = "";
        }

        private void DataGridView_Station_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DialogStation diglog = new DialogStation(false, Convert.ToInt32(this.DataGridView_Station.Rows[e.RowIndex].Cells["ID"].Value),this.mainform);
                diglog.ShowDialog(this);
            }
        }

        private void btn_Info_Click(object sender, EventArgs e)
        {
            if (this.DataGridView_Station.SelectedRows.Count > 0)
            {
                DialogStation diglog = new DialogStation(false, Convert.ToInt32(this.DataGridView_Station.SelectedRows[0].Cells["ID"].Value),this.mainform);
                diglog.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("请先选择一个您要查看的基站。", "详细信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            //if (Global.IsTempVersion && DB_Service.MainDataSet.Tables["StationTable"].Rows.Count >= 5)
            //{
            //    MessageBox.Show("对不起，您的软件为演示版。基站数目不能超过4个。请购买正式版本。", "新建基站", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            DialogStation diglog = new DialogStation(this.mainform);
            diglog.ShowDialog(this);
            label_AllStation.Text = "共有基站 " + DB_Service.MainDataSet.Tables["StationTable"].Rows.Count.ToString() + " 个";
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (this.DataGridView_Station.SelectedRows.Count > 0)
            {
                DialogStation diglog = new DialogStation(true, Convert.ToInt32(this.DataGridView_Station.SelectedRows[0].Cells["ID"].Value),this.mainform);
                diglog.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("请先选择一个您要查看的基站。", "详细信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_QueryFather_Click(object sender, EventArgs e)
        {
            if (this.DataGridView_Station.SelectedRows.Count > 0)
            {
                if (this.DataGridView_Station.SelectedRows[0].Cells["FatherStationID"].Value != DBNull.Value)
                {
                    DataRow[] tempRows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + Convert.ToInt32(this.DataGridView_Station.SelectedRows[0].Cells["FatherStationID"].Value));
                    if (tempRows.Length > 0)
                    {
                        DB_Service.MainDataSet.Tables["StationTable"].DefaultView.RowFilter = "ID = " + tempRows[0]["ID"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("这个基站没有父基站！", "查询父基站", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("这个基站没有父基站！", "查询父基站", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个您要查看的基站。", "详细信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_QuerySon_Click(object sender, EventArgs e)
        {
            if (this.DataGridView_Station.SelectedRows.Count > 0)
            {
                string[] array = System.Text.RegularExpressions.Regex.Split(this.DataGridView_Station.SelectedRows[0].Cells["SonStationIDs"].Value.ToString(), "-");
                string strResult = "";
                if (array.Length > 1)
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        if (array[j] != "")
                        {
                            strResult += "ID = " + array[j] + " or ";
                        }
                    }
                    DB_Service.MainDataSet.Tables["StationTable"].DefaultView.RowFilter = strResult.Substring(0, strResult.Length - 4);
                }
                else
                {
                    MessageBox.Show("这个基站没有子基站！", "查询子基站", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个您要查看的基站。", "详细信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_QueryNoFather_Click(object sender, EventArgs e)
        {
            DB_Service.MainDataSet.Tables["StationTable"].DefaultView.RowFilter = "StationType = '无线基站' and FatherStationID is NULL";
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (this.DataGridView_Station.SelectedRows.Count > 0)
            {
                int DelStationID = Convert.ToInt32(this.DataGridView_Station.SelectedRows[0].Cells["ID"].Value);
                if (MessageBox.Show("您确定要删除编号为：" + DelStationID + " 的这个基站吗？\n删除基站后，其父子关系、基站报警信息、数据采集信息也将随之删除。", "删除基站", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + DelStationID);
                    if (rows.Length > 0)
                    {
                        if (rows[0]["StationType"].ToString() == "无线基站")
                        {
                            //子基站，则在所有的父基站中的子基站字串中删除其
                            DataRow[] rows_father = DB_Service.MainDataSet.Tables["StationTable"].Select("StationType <> '无线基站'");
                            for (int i = 0; i < rows_father.Length; i++)
                            {
                                string NewSonStr = "";
                                string[] tempSonStr = rows_father[i]["SonStationIDs"].ToString().Split('-');
                                for (int k = 0; k < tempSonStr.Length; k++)
                                {
                                    if (tempSonStr[k] != "" && tempSonStr[k] != DelStationID.ToString())
                                    {
                                        NewSonStr += tempSonStr[k] + "-";
                                    }
                                }
                                rows_father[i]["SonStationIDs"] = NewSonStr;
                            }
                        }
                        else
                        {
                            //父基站，则在所有的子基站的FatherStationID删除其
                            DataRow[] rows_son = DB_Service.MainDataSet.Tables["StationTable"].Select("FatherStationID = " + DelStationID);
                            for (int j = 0; j < rows_son.Length; j++)
                            {
                                rows_son[j]["FatherStationID"] = DBNull.Value;
                            }
                        }
                        //基站本身
                        rows[0].Delete();
                        if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["StationTable"]) > 0)
                        {
                            //删除报警信息
                            DB_Service.ExecuteSQL("delete from AlarmMachineTable where StationID = " + DelStationID,"AlarmMachineTable");
                            //删除数据采集信息
                            DB_Service.ExecuteSQL("delete from HistoryCollectTable where Station_ID = " + DelStationID,"HistoryCollectTable");
                            //更新监控窗体上的显示
                            this.mainform.UpdateLayerDataSource("StationTable" + Global.SplitKey + "StationType" + Global.SplitKey + "Can基站", 0);
                            this.mainform.UpdateLayerDataSource("StationTable" + Global.SplitKey + "StationType" + Global.SplitKey + "网关基站", 0);
                            this.mainform.UpdateLayerDataSource("StationTable" + Global.SplitKey + "StationType" + Global.SplitKey + "无线基站", 1);
                            //更新基站数
                            label_AllStation.Text = "共有基站 " + DB_Service.MainDataSet.Tables["StationTable"].Rows.Count.ToString() + " 个";
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "删除基站", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲删除的基站不存在。", "删除基站", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择一个您要删除的基站。", "删除基站", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_GoToMap_Click(object sender, EventArgs e)
        {
            if (DataGridView_Station.SelectedRows.Count > 0)
            {
                DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + DataGridView_Station.SelectedRows[0].Cells["ID"].Value);
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
        }

        #endregion

        #region 卡片选项卡事件、方法

        private void tex_SelectCardID_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_SelectCardID.Text == "")
                {
                    btn_ShowALLCard_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_SelectCardID);
                        DB_Service.MainDataSet.Tables["CardTable"].DefaultView.RowFilter = "CardID = " + tex_SelectCardID.Text;
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_SelectCardID.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void tex_SelectCardPID_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_SelectCardPID.Text == "")
                {
                    btn_ShowALLCard_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_SelectCardPID);
                        DB_Service.MainDataSet.Tables["CardTable"].DefaultView.RowFilter = "PID = '" + tex_SelectCardPID.Text + "'";
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_SelectCardPID.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void com_SelectCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_SelectCardType.SelectedIndex == 0)
                {
                    btn_ShowALLCard_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_SelectCardType);
                    DB_Service.MainDataSet.Tables["CardTable"].DefaultView.RowFilter = "CardType = '" + com_SelectCardType.Text + "'";
                }
            }
        }

        private void tex_SelectPID_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_SelectPID.Text == "")
                {
                    btn_ShowAllPerson_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_SelectPID);
                        DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "PID = '" + tex_SelectPID.Text + "'";
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_SelectPID.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void tex_SelectName_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_SelectName.Text == "")
                {
                    btn_ShowAllPerson_Click(sender, e);
                }
                else
                {
                    try
                    {
                        InitAllControl(tex_SelectName);
                        DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "Name like '%" + tex_SelectName.Text + "%'";
                    }
                    catch
                    {
                        IsSystemEvent = true;
                        tex_SelectName.Text = "";
                        IsSystemEvent = false;
                    }
                }
            }
        }

        private void com_SelectDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_SelectDepartment.SelectedIndex == 0)
                {
                    btn_ShowAllPerson_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_SelectDepartment);
                    DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "Department = '" + com_SelectDepartment.Text + "'";
                }
            }
        }

        private void com_SelectWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_SelectWorkType.SelectedIndex == 0)
                {
                    btn_ShowAllPerson_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_SelectWorkType);
                    DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "WorkType = '" + com_SelectWorkType.Text + "'";
                }
            }
        }

        private void btn_NewCard_Click(object sender, EventArgs e)
        {
            DialogUpdateCard newCardDialog = new DialogUpdateCard();
            newCardDialog.ShowDialog(this);
            label_AllCard.Text = "共有卡片 " + DB_Service.MainDataSet.Tables["CardTable"].Rows.Count.ToString() + " 张。其中有 " + DB_Service.MainDataSet.Tables["CardTable"].Select("PID is null").Length.ToString() + " 张尚未与员工绑定。";
        }

        private void btn_DelCard_Click(object sender, EventArgs e)
        {
            if (dataGridView_Card.SelectedRows.Count > 0)
            {
                int DelCardID = Convert.ToInt32(dataGridView_Card.SelectedRows[0].Cells["CardID"].Value);
                if (MessageBox.Show("您确定要删除编号为：" + DelCardID + " 的这个卡片吗？\n\n删除卡片会自动删除这张卡片的考勤、报警、定位等相关记录。", "删除卡片", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + DelCardID);
                    if (rows.Length > 0)
                    {
                        rows[0].Delete();
                        if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["CardTable"]) > 0)
                        {
                            //级联删除SQL语句数组
                            List<string> strSQLs = new List<string>();
                            //删除考勤记录
                            strSQLs.Add("delete from DutyTable where CardID = " + DelCardID);
                            //删除定位记录
                            strSQLs.Add("delete from HistoryPositionTable where CardID = " + DelCardID);
                            //删除报警记录
                            strSQLs.Add("delete from AlarmPersonSendTable where CardID = " + DelCardID);
                            //因为有可能没有任何报警、考勤、定位记录。故不检测返回
                            DB_Service.ExecuteSQLs(strSQLs, new string[3] { "DutyTable", "HistoryPositionTable", "AlarmPersonSendTable" });
                            label_AllCard.Text = "共有卡片 " + DB_Service.MainDataSet.Tables["CardTable"].Rows.Count.ToString() + " 张。其中有 " + DB_Service.MainDataSet.Tables["CardTable"].Select("PID is null").Length.ToString() + " 张尚未与员工绑定。";
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "删除卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲删除的卡片不存在。", "删除卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择一个您要删除的卡片。", "删除卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_UpdateCardType_Click(object sender, EventArgs e)
        {
            if (dataGridView_Card.SelectedRows.Count > 0)
            {
                int UpdateCardID = Convert.ToInt32(dataGridView_Card.SelectedRows[0].Cells["CardID"].Value);
                DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + UpdateCardID);
                if (rows.Length > 0)
                {
                    DialogUpdateCard updateCardDialog = new DialogUpdateCard(UpdateCardID, rows[0]["CardType"].ToString());
                    updateCardDialog.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("您欲修改的卡片不存在。", "修改卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请先选择一个您要修改的卡片。", "修改卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_ShowUnuseCard_Click(object sender, EventArgs e)
        {
            InitAllControl(null);
            DB_Service.MainDataSet.Tables["CardTable"].DefaultView.RowFilter = "PID is Null";
        }

        private void btn_ShowALLCard_Click(object sender, EventArgs e)
        {
            InitAllControl(null);
            DB_Service.MainDataSet.Tables["CardTable"].DefaultView.RowFilter = "";
        }

        private void btn_ShowAllPerson_Click(object sender, EventArgs e)
        {
            InitAllControl(null);
            DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "";
        }

        private void btn_BindCard_Click(object sender, EventArgs e)
        {
            if (dataGridView_Card.SelectedRows.Count > 0 && dataGridView_Person.SelectedRows.Count > 0)
            {
                int BindCardID = Convert.ToInt32(dataGridView_Card.SelectedRows[0].Cells["CardID"].Value);
                string BindPID = dataGridView_Person.SelectedRows[0].Cells["PID"].Value.ToString();
                if (MessageBox.Show("您确定要将编号为：" + BindCardID + " 的这个卡片与工号为：" + BindPID + " 的员工绑定在一起吗？", "绑定卡片", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + BindCardID);
                    if (rows_Card.Length > 0)
                    {
                        DataRow[] rows_Person = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + BindPID + "'");
                        if (rows_Person.Length > 0)
                        {
                            //判断BindCardID是否没有绑定任何卡，绑定则跳出
                            if (rows_Card[0]["PID"] != DBNull.Value)
                            {
                                MessageBox.Show("对不起，您欲绑定的卡片(编号：" + BindCardID + ")已经绑定了别的人员，所以，您不能重复绑定。\n您可以先删除这张卡片原有的绑定后重试。", "绑定卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //判断BindPID是否没有被任何卡绑定，绑定则跳出
                            DataRow[] tempRows = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + BindPID + "'");
                            if (tempRows.Length > 0)
                            {
                                MessageBox.Show("对不起，您欲绑定的人员(工号：" + BindPID + ")已经绑定了别的卡片，所以，您不能重复绑定。\n您可以先删除这个人员原有的绑定后重试。", "绑定卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //一对一性测试通过，修改记录并保存
                            rows_Card[0]["PID"] = BindPID;
                            if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["CardTable"]) == 0)
                            {
                                MessageBox.Show("绑定失败！", "绑定卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("您欲绑定的人员不存在。", "绑定卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲绑定的卡片不存在。", "绑定卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择一个您要绑定的卡片和欲绑定的人员。", "绑定卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_UnBindCard_Click(object sender, EventArgs e)
        {
            int UnBindCardID = Convert.ToInt32(dataGridView_Card.SelectedRows[0].Cells["CardID"].Value);
            string UnBindPID = dataGridView_Person.SelectedRows[0].Cells["PID"].Value.ToString();
            if (MessageBox.Show("您确定要把编号为：" + UnBindCardID + " 的卡片与工号为：" + UnBindPID + " 的员工的绑定解除吗？\n\n解除绑定会自动删除这张卡片的考勤、报警、定位等相关记录。", "解除绑定", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + UnBindCardID);
                if (rows_Card.Length > 0)
                {
                    //修改记录
                    rows_Card[0]["PID"] = DBNull.Value;
                    if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["CardTable"]) > 0)
                    {
                        //级联删除SQL语句数组
                        List<string> strSQLs = new List<string>();
                        //删除考勤记录
                        strSQLs.Add("delete from DutyTable where CardID = " + UnBindCardID);
                        //删除定位记录
                        strSQLs.Add("delete from HistoryPositionTable where CardID = " + UnBindCardID);
                        //删除报警记录
                        strSQLs.Add("delete from AlarmPersonSendTable where CardID = " + UnBindCardID);
                        //因为有可能没有任何报警、考勤、定位记录。故不检测返回
                        DB_Service.ExecuteSQLs(strSQLs, new string[3] { "DutyTable", "HistoryPositionTable", "AlarmPersonSendTable" });
                    }
                    else
                    {
                        MessageBox.Show("解除绑定失败！", "解除绑定", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("您欲解除绑定的卡片不存在。", "解除绑定", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dataGridView_Card_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView_Card.Rows[e.RowIndex].Cells["PID"].Value == DBNull.Value)
                {
                    //在dataGridView_Card中没有这个人员的绑定
                    InitUnBindUI_CardClick(dataGridView_Card.Rows[e.RowIndex].Cells["CardID"].Value.ToString(), dataGridView_Card.Rows[e.RowIndex].Cells["CardType"].Value.ToString());
                }
                else
                {
                    //在dataGridView_Card中有这个人员的绑定
                    string PID = dataGridView_Card.Rows[e.RowIndex].Cells["PID"].Value.ToString();
                    InitBindUI(Convert.ToInt32(dataGridView_Card.Rows[e.RowIndex].Cells["CardID"].Value), PID);
                    //使dataGridView_Person定位到绑定的人员记录位置
                    try
                    {
                        foreach (DataGridViewRow row in dataGridView_Person.Rows)
                        {
                            if (row.Cells["PID"].Value.ToString() == PID)
                            {
                                row.Selected = true;
                                return;
                            }
                        }
                    }
                    catch
                    {  }
                }
            }
        }

        private void dataGridView_Person_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string PID = dataGridView_Person.Rows[e.RowIndex].Cells["PID"].Value.ToString();
                try
                {
                    foreach (DataGridViewRow row in dataGridView_Card.Rows)
                    {
                        if (row.Cells["PID"].Value != DBNull.Value && row.Cells["PID"].Value.ToString() == PID)
                        {
                            //在dataGridView_Card中有这个人员的绑定
                            InitBindUI(Convert.ToInt32(row.Cells["CardID"].Value), PID);
                            //使dataGridView_Card定位到绑定的人员记录位置
                            row.Selected = true;
                            return;
                        }
                    }
                }
                catch
                {   }
                //至此，说明在dataGridView_Card中没有这个人员的绑定
                InitUnBindUI_PersonClick(dataGridView_Person.Rows[e.RowIndex].Cells["Name"].Value.ToString(), dataGridView_Person.Rows[e.RowIndex].Cells["PID"].Value.ToString(), dataGridView_Person.Rows[e.RowIndex].Cells["Department"].Value.ToString(), dataGridView_Person.Rows[e.RowIndex].Cells["WorkType"].Value.ToString(), dataGridView_Person.Rows[e.RowIndex].Cells["Photo"].Value);
            }
        }

        private void InitBindUI(int CardID,string PID)
        {
            DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + CardID);
            DataRow[] rows_Person = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + PID + "'");
            if (rows_Card.Length > 0 && rows_Person.Length > 0)
            {
                if (rows_Person[0]["Photo"] != null && rows_Person[0]["Photo"] != DBNull.Value)
                {
                    System.IO.MemoryStream mmm = new System.IO.MemoryStream((byte[])rows_Person[0]["Photo"]);
                    pic_Photo.Image = Image.FromStream(mmm);
                }
                else
                {
                    //没有照片则加载默认的空图片
                    pic_Photo.Image = Resource_Service.GetImage("Person");
                }
                tex_Name.Text = rows_Person[0]["Name"].ToString();
                tex_PID.Text = rows_Person[0]["PID"].ToString();
                tex_Department.Text = rows_Person[0]["Department"].ToString();
                tex_WorkType.Text = rows_Person[0]["WorkType"].ToString();
                tex_CardID.Text = rows_Card[0]["CardID"].ToString();
                tex_CardType.Text = rows_Card[0]["CardType"].ToString();
                label_UnBindPerson.Visible = false;
                label_UnBindCard.Visible = false;
                btn_BindCard.Enabled = false;
                btn_UnBindCard.Enabled = true;
            }
            else
            {
                MessageBox.Show("人员或者卡片不存在！", "卡片管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InitUnBindUI_CardClick(string CardID, string CardType)
        {
            pic_Photo.Image = null;
            tex_Name.Text = "";
            tex_PID.Text = "";
            tex_Department.Text = "";
            tex_WorkType.Text = "";
            tex_CardID.Text = CardID;
            tex_CardType.Text = CardType;

            label_UnBindPerson.Visible = true;
            label_UnBindCard.Visible = false;
            btn_BindCard.Enabled = true;
            btn_UnBindCard.Enabled = false;
        }

        private void InitUnBindUI_PersonClick(string Name,string PID,string Department,string WorkType,object Photo)
        {
            if (Photo != null && Photo != DBNull.Value)
            {
                System.IO.MemoryStream mmm = new System.IO.MemoryStream((byte[])Photo);
                pic_Photo.Image = Image.FromStream(mmm);
            }
            else
            {
                //没有照片则加载默认的空图片
                pic_Photo.Image = Resource_Service.GetImage("Person");
            }
            tex_Name.Text = Name;
            tex_PID.Text = PID;
            tex_Department.Text = Department;
            tex_WorkType.Text = WorkType;
            tex_CardID.Text = "";
            tex_CardType.Text = "";

            label_UnBindPerson.Visible = false;
            label_UnBindCard.Visible = true;
            btn_BindCard.Enabled = true;
            btn_UnBindCard.Enabled = false;
        }

        #endregion

        private void DataGridView_Station_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void dataGridView_Card_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void dataGridView_Person_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        /// <summary>
        /// 导出绑定列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExportCardPerson_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                FileStream FS = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter SW = new StreamWriter(FS);
                SW.Flush();
                SW.WriteLine("          " + Application.ProductName + (Global.IsTempVersion ? "(演示版)" : "") + "人员识别卡分配列表");
                SW.WriteLine("");
                SW.WriteLine("");
                SW.WriteLine("卡号     卡片类型     工号      姓名        部门                职务");
                SW.WriteLine("");
                for (int i = 0; i < DB_Service.MainDataSet.Tables["CardTable"].Rows.Count; i++)
                {
                    string CardID = DB_Service.MainDataSet.Tables["CardTable"].Rows[i]["CardID"].ToString();
                    string CardType = DB_Service.MainDataSet.Tables["CardTable"].Rows[i]["CardType"].ToString();
                    string PID = "";
                    string Name = "";
                    string Department = "";
                    string WorkType = "";
                    if (DB_Service.MainDataSet.Tables["CardTable"].Rows[i]["PID"] != DBNull.Value)
                    {
                        DataRow[] row_Person = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + DB_Service.MainDataSet.Tables["CardTable"].Rows[i]["PID"].ToString() + "'");
                        if (row_Person.Length > 0)
                        {
                            PID = row_Person[0]["PID"].ToString();
                            Name = row_Person[0]["Name"].ToString();
                            Department = row_Person[0]["Department"].ToString();
                            WorkType = row_Person[0]["WorkType"].ToString();
                        }
                    }
                    SW.WriteLine(CardID + "     " + CardType + "     " + PID + new string(' ', 10 - PID.Length) + Name + new string(' ', 12 - Name.Length * 2) + Department + new string(' ', 20 - Department.Length * 2) + WorkType);
                }
                SW.Close();
                FS.Close();
            }
        }
    }
}