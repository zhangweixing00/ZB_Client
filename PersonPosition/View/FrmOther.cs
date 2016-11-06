using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PersonPosition.StaticService;

namespace PersonPosition.View
{
    public partial class FrmOther : Form
    {
        private MainForm mainform;
        private DataRow WPRow = null;
        private DataRow ClassRow = null;
        private DataRow WorkTypeRow = null;
        private DataRow DepartmentRow = null;

        public FrmOther(MainForm _mainform)
        {
            InitializeComponent();
            this.mainform = _mainform;

            this.Tag = this.MainPanel;
            DataGridViewWP.DataSource = DB_Service.MainDataSet.Tables["WPTable"];
            DataGridViewClass.DataSource = DB_Service.MainDataSet.Tables["ClassTable"];
            DataGridViewWorkType.DataSource = DB_Service.MainDataSet.Tables["WorkTypeTable"];
            DataGridViewDepartment.DataSource = DB_Service.MainDataSet.Tables["DepartmentTable"];
            InitWPDataView();
            InitClassDataView();
            InitWorkTypeDataView();
            InitDepartmentDataView();

            RefreshWPSelectCombox();
            InitAllInfo();
        }

        //动态布局控件
        private void MainPanel_Resize(object sender, EventArgs e)
        {
            //groupBox_WP.Left = 5;
            //groupBox_WP.Width = this.MainPanel.Width / 2 - 8;
            //groupBox_WP.Top = 60;
            //groupBox_WP.Height = (this.MainPanel.Height - 60) / 2 - 5;

            //groupBox_Class.Left = this.MainPanel.Width / 2 + 5;
            //groupBox_Class.Width = this.MainPanel.Width / 2 - 8;
            //groupBox_Class.Top = 60;
            groupBox_Class.Height = (this.MainPanel.Height - 60) / 2 - 5;

            groupBox_Department.Left = 5;
            groupBox_Department.Width = this.MainPanel.Width / 2 - 8;
            groupBox_Department.Top = (this.MainPanel.Height - 60) / 2 + 65;
            groupBox_Department.Height = (this.MainPanel.Height - 60) / 2 - 10;

            groupBox_WorkType.Left = this.MainPanel.Width / 2 + 5;
            groupBox_WorkType.Width = this.MainPanel.Width / 2 - 8;
            groupBox_WorkType.Top = (this.MainPanel.Height - 60) / 2 + 65;
            groupBox_WorkType.Height = (this.MainPanel.Height - 60) / 2 - 10;
        }

        #region 班次管理

        private void btn_NewClass_Click(object sender, EventArgs e)
        {
            panel_Class.Enabled = true;
            ClassRow = null;
            tex_ClassName.ReadOnly = false;
            tex_ClassName.Text = "";
            com_StartHourClass.SelectedIndex = 0;
            com_StartMinuteClass.SelectedIndex = 0;
            com_EndHourClass.SelectedIndex = 0;
            com_EndMinuteClass.SelectedIndex = 0;
        }

        private void btn_UpdateClass_Click(object sender, EventArgs e)
        {
            panel_Class.Enabled = true;
        }

        private void btn_DelClass_Click(object sender, EventArgs e)
        {
            if (DataGridViewClass.SelectedRows.Count > 0)
            {
                string DelClass = DataGridViewClass.SelectedRows[0].Cells["ClassName"].Value.ToString();
                if (MessageBox.Show("您确定要删除：" + DelClass + " 这个班次吗？", "班次管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] rows = DB_Service.MainDataSet.Tables["ClassTable"].Select("ClassName = '" + DelClass + "'");
                    if (rows.Length > 0)
                    {
                        rows[0].Delete();
                        if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["ClassTable"]) == 0)
                        {
                            MessageBox.Show("删除失败！", "班次管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲删除的班次不存在。", "班次管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一个您欲删除的班次。", "班次管理");
            }
        }

        private void btn_SaveClass_Click(object sender, EventArgs e)
        {
            if (tex_ClassName.Text.Trim() == "" || com_StartHourClass.Text.Trim() == "" || com_StartMinuteClass.Text.Trim() == "" || com_EndHourClass.Text.Trim() == "" || com_EndMinuteClass.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整的班次信息后再进行保存。\n至少包含：班次名、上班时间、下班时间", "班次设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //如果是新建班次，则判断班次名称的唯一性并初始化ClassRow
                if (ClassRow == null)
                {
                    if (DB_Service.MainDataSet.Tables["ClassTable"].Select("ClassName = '" + tex_ClassName.Text.Trim() + "'").Length > 0)
                    {
                        MessageBox.Show("对不起，您所输入的这个班次名称已经存在，请重新输入一个未被占用的班次名称。", "新增班次", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tex_ClassName.Text = "";
                        return;
                    }
                    else
                    {
                        //初始化ClassRow
                        ClassRow = DB_Service.MainDataSet.Tables["ClassTable"].NewRow();
                        DB_Service.MainDataSet.Tables["ClassTable"].Rows.Add(ClassRow);
                    }
                }

                //修改ClassRow的值
                ClassRow["ClassName"] = tex_ClassName.Text.Trim();
                ClassRow["StartTime"] = com_StartHourClass.Text.Split(' ')[0] + ":" + com_StartMinuteClass.Text.Split(' ')[0];
                ClassRow["EndTime"] = com_EndHourClass.Text.Split(' ')[0] + ":" + com_EndMinuteClass.Text.Split(' ')[0];

                panel_Class.Enabled = false;

                //将ClassRow中的更新提交至数据库
                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["ClassTable"]) == 0)
                {
                    MessageBox.Show("保存班次信息失败！\n请确保数据库连接正确。", "班次管理");
                }
            }
        }

        private void btn_CanelClass_Click(object sender, EventArgs e)
        {
            panel_Class.Enabled = false;
        }

        private void DataGridViewClass_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                panel_Class.Enabled = false;
                ShowClassInfo(this.DataGridViewClass.Rows[e.RowIndex].Cells["ClassName"].Value.ToString());
            }
        }

        #endregion

        #region 工分管理

        private void btn_NewWP_Click(object sender, EventArgs e)
        {
            panel_WP.Enabled = true;
            WPRow = null;
            tex_WPName.ReadOnly = false;
            tex_WPName.Text = "";
            tex_WPTimeMinute.Text = "";
            tex_WPPoint.Text = "";
        }

        private void btn_UpdateWP_Click(object sender, EventArgs e)
        {
            panel_WP.Enabled = true;
        }

        private void btn_DelWP_Click(object sender, EventArgs e)
        {
            if (DataGridViewWP.SelectedRows.Count > 0)
            {
                string DelWP = DataGridViewWP.SelectedRows[0].Cells["WPName"].Value.ToString();
                if (MessageBox.Show("您确定要删除：" + DelWP + " 这个方案吗？", "工分管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] rows = DB_Service.MainDataSet.Tables["WPTable"].Select("WPName = '" + DelWP + "'");
                    if (rows.Length > 0)
                    {
                        rows[0].Delete();
                        if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["WPTable"]) > 0)
                        {
                            //级联删除WorkTypeTable中的相应字段
                            DataRow[] DeepRows = DB_Service.MainDataSet.Tables["WorkTypeTable"].Select("WPName = '" + DelWP + "'");
                            if (DeepRows.Length > 0)
                            {
                                for (int i = 0; i < DeepRows.Length; i++)
                                {
                                    DeepRows[i]["WPName"] = DBNull.Value;
                                }
                                DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["WorkTypeTable"]);
                            }
                            DeepRows = null;
                            //刷新职务显示中的工分方案列表
                            RefreshWPSelectCombox();
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "工分管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲删除的工分方案不存在。", "工分管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一个您欲删除的工分方案。", "工分管理");
            }
        }

        private void btn_SaveWP_Click(object sender, EventArgs e)
        {
            if (tex_WPName.Text.Trim() == "" || tex_WPTimeMinute.Text.Trim() == "" || tex_WPPoint.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整的工分信息后再进行保存。\n至少包含：方案名、工作时间、工分", "工分设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //如果是新建工分方案，则判断工分名称的唯一性并初始化WPRow
                if (WPRow == null)
                {
                    if (DB_Service.MainDataSet.Tables["WPTable"].Select("WPName = '" + tex_WPName.Text.Trim() + "'").Length > 0)
                    {
                        MessageBox.Show("对不起，您所输入的这个工分名称已经存在，请重新输入一个未被占用的工分名称。", "新增工分方案", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tex_WPName.Text = "";
                        return;
                    }
                    else
                    {
                        //初始化WPRow
                        WPRow = DB_Service.MainDataSet.Tables["WPTable"].NewRow();
                        DB_Service.MainDataSet.Tables["WPTable"].Rows.Add(WPRow);
                    }
                }
                
                try
                {
                    //修改WPRow的值
                    WPRow["WPName"] = tex_WPName.Text.Trim();
                    WPRow["WPTimeMinute"] = Convert.ToInt32(tex_WPTimeMinute.Text.Trim());
                    WPRow["Point"] = Convert.ToInt32(tex_WPPoint.Text.Trim());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "工分管理", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                panel_WP.Enabled = false;

                //将WPRow中的更新提交至数据库
                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["WPTable"]) > 0)
                {
                    //刷新职务显示中的工分方案列表
                    RefreshWPSelectCombox();
                }
                else
                {
                    MessageBox.Show("保存工分方案信息失败！\n请确保数据库连接正确。", "工分管理");
                }
            }
        }

        private void btn_CanelWP_Click(object sender, EventArgs e)
        {
            panel_WP.Enabled = false;
        }

        private void DataGridViewWP_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                panel_WP.Enabled = false;
                ShowWPInfo(this.DataGridViewWP.Rows[e.RowIndex].Cells["WPName"].Value.ToString());
            }
        }

        #endregion

        #region 职务管理

        private void btn_NewWorkType_Click(object sender, EventArgs e)
        {
            panel_WorkType.Enabled = true;
            WorkTypeRow = null;
            tex_WorkTypeName.ReadOnly = false;
            tex_WorkTypeName.Text = "";
            com_WorkTypeWPName.SelectedIndex = -1;
            radio_WorkTypeLead2.Checked = true;
            radio_WorkTypeSpecal2.Checked = true;
        }

        private void btn_UpdateWorkType_Click(object sender, EventArgs e)
        {
            panel_WorkType.Enabled = true;
        }

        private void btn_DelWorkType_Click(object sender, EventArgs e)
        {
            if (DataGridViewWorkType.SelectedRows.Count > 0)
            {
                string DelWorkType = DataGridViewWorkType.SelectedRows[0].Cells["WorkTypeName"].Value.ToString();
                if (MessageBox.Show("您确定要删除：" + DelWorkType + " 这个职务吗？", "职务管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] rows = DB_Service.MainDataSet.Tables["WorkTypeTable"].Select("WorkTypeName = '" + DelWorkType + "'");
                    if (rows.Length > 0)
                    {
                        rows[0].Delete();
                        if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["WorkTypeTable"]) > 0)
                        {
                            //级联删除PersonTable中的相应字段
                            DataRow[] DeepRows = DB_Service.MainDataSet.Tables["PersonTable"].Select("WorkType = '" + DelWorkType + "'");
                            if (DeepRows.Length > 0)
                            {
                                for (int i = 0; i < DeepRows.Length; i++)
                                {
                                    DeepRows[i]["WorkType"] = DBNull.Value;
                                }
                                DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["PersonTable"]);
                            }
                            DeepRows = null;
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "职务管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲删除的职务不存在。", "职务管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一个您欲删除的职务。", "职务管理");
            }
        }

        private void btn_SaveWorkType_Click(object sender, EventArgs e)
        {
            if (tex_WorkTypeName.Text.Trim() == "" || tex_WorkTypeNeedWorkHour.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整的职务信息后再进行保存。\n至少包含：职务名称和每天最少工作小时数", "职务设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //如果是新建职务方案，则判断职务名称的唯一性并初始化WorkTypeRow
                if (WorkTypeRow == null)
                {
                    if (DB_Service.MainDataSet.Tables["WorkTypeTable"].Select("WorkTypeName = '" + tex_WorkTypeName.Text.Trim() + "'").Length > 0)
                    {
                        MessageBox.Show("对不起，您所输入的这个职务名称已经存在，请重新输入一个未被占用的职务名称。", "新增职务", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tex_WorkTypeName.Text = "";
                        return;
                    }
                    else
                    {
                        //初始化WorkTypeRow
                        WorkTypeRow = DB_Service.MainDataSet.Tables["WorkTypeTable"].NewRow();
                        DB_Service.MainDataSet.Tables["WorkTypeTable"].Rows.Add(WorkTypeRow);
                    }
                }

                //修改WorkTypeRow的值
                WorkTypeRow["WorkTypeName"] = tex_WorkTypeName.Text.Trim();
                WorkTypeRow["WPName"] = com_WorkTypeWPName.Text.Trim();
                WorkTypeRow["NeedWorkHour"] = tex_WorkTypeNeedWorkHour.Text.Trim();
                if (radio_WorkTypeLead1.Checked)
                {
                    WorkTypeRow["IsLead"] = true;
                }
                else
                {
                    WorkTypeRow["IsLead"] = false;
                }
                if (radio_WorkTypeSpecal1.Checked)
                {
                    WorkTypeRow["IsSpecal"] = true;
                }
                else
                {
                    WorkTypeRow["IsSpecal"] = false;
                }

                panel_WorkType.Enabled = false;

                //将WPRow中的更新提交至数据库
                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["WorkTypeTable"]) == 0)
                {
                    MessageBox.Show("保存职务信息失败！\n请确保数据库连接正确。", "职务管理");
                }
            }
        }

        private void btn_CanelWorkType_Click(object sender, EventArgs e)
        {
            panel_WorkType.Enabled = false;
        }

        private void DataGridViewWorkType_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                panel_WorkType.Enabled = false;
                ShowWorkTypeInfo(this.DataGridViewWorkType.Rows[e.RowIndex].Cells["WorkTypeName"].Value.ToString());
            }
        }

        private void btn_SearchWorkTypePersion_Click(object sender, EventArgs e)
        {
            if (tex_WorkTypeName.Text != "")
            {
                try
                {
                    this.mainform.MainBtn_Person_Click(null, null);
                    this.mainform.frmPerson.ShowPersonInfoByWorkType(tex_WorkTypeName.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion

        #region 部门管理

        private void btn_NewDepartment_Click(object sender, EventArgs e)
        {
            panel_Department.Enabled = true;
            DepartmentRow = null;
            tex_DepartmentName.ReadOnly = false;
            tex_DepartmentName.Text = "";
            tex_DepartmentChief.Text = "";
            tex_DepartmentAddress.Text = "";
            tex_DepartmentTel.Text = "";
        }

        private void btn_UpdateDepartment_Click(object sender, EventArgs e)
        {
            panel_Department.Enabled = true;
        }

        private void btn_DelDepartment_Click(object sender, EventArgs e)
        {
            if (DataGridViewDepartment.SelectedRows.Count > 0)
            {
                string DelDepartment = DataGridViewDepartment.SelectedRows[0].Cells["DepartmentName"].Value.ToString();
                if (MessageBox.Show("您确定要删除：" + DelDepartment + " 这个部门吗？", "部门管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] rows = DB_Service.MainDataSet.Tables["DepartmentTable"].Select("DepartmentName = '" + DelDepartment + "'");
                    if (rows.Length > 0)
                    {
                        rows[0].Delete();
                        if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["DepartmentTable"]) > 0)
                        {
                            //级联删除PersonTable中的相应字段
                            DataRow[] DeepRows = DB_Service.MainDataSet.Tables["PersonTable"].Select("Department = '" + DelDepartment + "'");
                            if (DeepRows.Length > 0)
                            {
                                for (int i = 0; i < DeepRows.Length; i++)
                                {
                                    DeepRows[i]["Department"] = DBNull.Value;
                                }
                                DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["PersonTable"]);
                            }
                            DeepRows = null;
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "部门管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲删除的部门不存在。", "部门管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一个您欲删除的部门。", "部门管理");
            }
        }

        private void btn_SaveDepartment_Click(object sender, EventArgs e)
        {
            if (tex_DepartmentName.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整的部门信息后再进行保存。\n至少包含：部门名称", "部门设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //如果是新建部门方案，则判断部门名称的唯一性并初始化DepartmentRow
                if (DepartmentRow == null)
                {
                    if (DB_Service.MainDataSet.Tables["DepartmentTable"].Select("DepartmentName = '" + tex_DepartmentName.Text.Trim() + "'").Length > 0)
                    {
                        MessageBox.Show("对不起，您所输入的这个部门名称已经存在，请重新输入一个未被占用的部门名称。", "新增部门", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tex_DepartmentName.Text = "";
                        return;
                    }
                    else
                    {
                        //初始化DepartmentRow
                        DepartmentRow = DB_Service.MainDataSet.Tables["DepartmentTable"].NewRow();
                        DB_Service.MainDataSet.Tables["DepartmentTable"].Rows.Add(DepartmentRow);
                    }
                }

                try
                {
                    //修改DepartmentRow的值
                    DepartmentRow["DepartmentName"] = tex_DepartmentName.Text.Trim();
                    if (tex_DepartmentChief.Text.Trim() != "")
                        DepartmentRow["ChiefPID"] = tex_DepartmentChief.Text.Trim();
                    DepartmentRow["Address"] = tex_DepartmentAddress.Text.Trim();
                    DepartmentRow["Tel"] = tex_DepartmentTel.Text.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "部门管理", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                panel_Department.Enabled = false;

                //将WPRow中的更新提交至数据库
                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["DepartmentTable"]) == 0)
                {
                    MessageBox.Show("保存部门信息失败！\n请确保数据库连接正确。", "部门管理");
                }
            }
        }

        private void btn_CanelDepartment_Click(object sender, EventArgs e)
        {
            panel_Department.Enabled = false;
        }

        private void DataGridViewDepartment_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                panel_Department.Enabled = false;
                ShowDepartmentInfo(this.DataGridViewDepartment.Rows[e.RowIndex].Cells["DepartmentName"].Value.ToString());
            }
        }

        private void btn_SearchDepartmentPersion_Click(object sender, EventArgs e)
        {
            if (tex_DepartmentName.Text != "")
            {
                try
                {
                    this.mainform.MainBtn_Person_Click(null, null);
                    this.mainform.frmPerson.ShowPersonInfoByDepartment(tex_DepartmentName.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion

        private void InitAllInfo()
        {
            if (DataGridViewWP.Rows.Count > 0)
            {
                ShowWPInfo(DataGridViewWP.Rows[0].Cells["WPName"].Value.ToString());
            }
            if (DataGridViewClass.Rows.Count > 0)
            {
                ShowClassInfo(DataGridViewClass.Rows[0].Cells["ClassName"].Value.ToString());
            }
            if (DataGridViewDepartment.Rows.Count > 0)
            {
                ShowDepartmentInfo(DataGridViewDepartment.Rows[0].Cells["DepartmentName"].Value.ToString());
            }
            if (DataGridViewWorkType.Rows.Count > 0)
            {
                ShowWorkTypeInfo(DataGridViewWorkType.Rows[0].Cells["WorkTypeName"].Value.ToString());
            }
        }

        private void ShowWPInfo(string WPName)
        {
            DataRow[] rows = DB_Service.MainDataSet.Tables["WPTable"].Select("WPName = '" + WPName + "'");
            if (rows.Length > 0)
            {
                //初始化当前工分信息记录
                WPRow = rows[0];
                //初始化界面显示
                tex_WPName.ReadOnly = true;
                tex_WPName.Text = WPRow["WPName"].ToString();
                tex_WPTimeMinute.Text = WPRow["WPTimeMinute"].ToString();
                tex_WPPoint.Text = WPRow["Point"].ToString();
            }
            else
            {
                MessageBox.Show("无法在数据库中找到这条记录！", "数据查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowClassInfo(string ClassName)
        {
            DataRow[] rows = DB_Service.MainDataSet.Tables["ClassTable"].Select("ClassName = '" + ClassName + "'");
            if (rows.Length > 0)
            {
                //初始化当前班次信息记录
                ClassRow = rows[0];
                //初始化界面显示
                tex_ClassName.ReadOnly = true;
                tex_ClassName.Text = ClassRow["ClassName"].ToString();
                string[] strTimes= ClassRow["StartTime"].ToString().Split(':');
                if (strTimes.Length > 1)
                {
                    com_StartHourClass.Text = strTimes[0] + " 时";
                    com_StartMinuteClass.Text = strTimes[1] + " 分";
                }
                else
                {
                    com_StartHourClass.SelectedIndex = 0;
                    com_StartMinuteClass.SelectedIndex = 0;
                }
                strTimes = ClassRow["EndTime"].ToString().Split(':');
                if (strTimes.Length > 1)
                {
                    com_EndHourClass.Text = strTimes[0] + " 时";
                    com_EndMinuteClass.Text = strTimes[1] + " 分";
                }
                else
                {
                    com_EndHourClass.SelectedIndex = 0;
                    com_EndMinuteClass.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("无法在数据库中找到这条记录！", "数据查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowDepartmentInfo(string DepartmentName)
        {
            DataRow[] rows = DB_Service.MainDataSet.Tables["DepartmentTable"].Select("DepartmentName = '" + DepartmentName + "'");
            if (rows.Length > 0)
            {
                //初始化当前部门信息记录
                DepartmentRow = rows[0];
                //初始化界面显示
                tex_DepartmentName.ReadOnly = true;
                tex_DepartmentName.Text = DepartmentRow["DepartmentName"].ToString();
                tex_DepartmentChief.Text = DepartmentRow["ChiefPID"].ToString();
                tex_DepartmentAddress.Text = DepartmentRow["Address"].ToString();
                tex_DepartmentTel.Text = DepartmentRow["Tel"].ToString();
            }
            else
            {
                MessageBox.Show("无法在数据库中找到这条记录！", "数据查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowWorkTypeInfo(string WorkTypeName)
        {
            DataRow[] rows = DB_Service.MainDataSet.Tables["WorkTypeTable"].Select("WorkTypeName = '" + WorkTypeName + "'");
            if (rows.Length > 0)
            {
                //初始化当前职务信息记录
                WorkTypeRow = rows[0];
                //初始化界面显示
                tex_WorkTypeName.ReadOnly = true;
                tex_WorkTypeName.Text = WorkTypeRow["WorkTypeName"].ToString();
                tex_WorkTypeNeedWorkHour.Text = WorkTypeRow["NeedWorkHour"].ToString();
                if (WorkTypeRow["WPName"].ToString() == "")
                {
                    com_WorkTypeWPName.SelectedIndex = -1;
                }
                else
                {
                    com_WorkTypeWPName.Text = WorkTypeRow["WPName"].ToString();
                }
                if (Convert.ToBoolean(WorkTypeRow["IsLead"]))
                {
                    radio_WorkTypeLead1.Checked = true;
                }
                else
                {
                    radio_WorkTypeLead2.Checked = true;
                }
                if (Convert.ToBoolean(WorkTypeRow["IsSpecal"]))
                {
                    radio_WorkTypeSpecal1.Checked = true;
                }
                else
                {
                    radio_WorkTypeSpecal2.Checked = true;
                }
            }
            else
            {
                MessageBox.Show("无法在数据库中找到这条记录！", "数据查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitWPDataView()
        {
            DataGridViewWP.Columns["WPName"].HeaderText = "方案名称";
            DataGridViewWP.Columns["WPTimeMinute"].HeaderText = "工作时间";
            DataGridViewWP.Columns["Point"].HeaderText = "工分";
        }

        private void InitClassDataView()
        {
            DataGridViewClass.Columns["ClassName"].HeaderText = "班次名称";
            DataGridViewClass.Columns["StartTime"].HeaderText = "上班时间";
            DataGridViewClass.Columns["EndTime"].HeaderText = "下班时间";
        }

        private void InitWorkTypeDataView()
        {
            DataGridViewWorkType.Columns["WorkTypeName"].HeaderText = "职务名称";
            DataGridViewWorkType.Columns["WPName"].HeaderText = "工分方案名";
            DataGridViewWorkType.Columns["IsLead"].HeaderText = "是否是领导";
            DataGridViewWorkType.Columns["IsSpecal"].HeaderText = "是否是特殊工种";
            DataGridViewWorkType.Columns["NeedWorkHour"].HeaderText = "每天最少工作小时";
        }

        private void InitDepartmentDataView()
        {
            DataGridViewDepartment.Columns["DepartmentName"].HeaderText = "部门名称";
            DataGridViewDepartment.Columns["ChiefPID"].HeaderText = "负责人工号";
            DataGridViewDepartment.Columns["Address"].HeaderText = "地址";
            DataGridViewDepartment.Columns["Tel"].HeaderText = "电话";
        }

        private void RefreshWPSelectCombox()
        {
            com_WorkTypeWPName.Items.Clear();
            try
            {
                foreach (DataRow row in DB_Service.MainDataSet.Tables["WPTable"].Rows)
                {
                    com_WorkTypeWPName.Items.Add(row["WPName"].ToString());
                }
            }
            catch
            {   }
        }

        private void tex_DepartmentChief_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void tex_WPPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void tex_WorkTypeNeedWorkHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void DataGridViewClass_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void DataGridViewWP_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void DataGridViewWorkType_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void DataGridViewDepartment_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }
    }
}