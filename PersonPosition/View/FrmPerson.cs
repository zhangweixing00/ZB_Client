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
using PersonPosition.Model;
using PersonPosition.Common;

namespace PersonPosition.View
{
    public partial class FrmPerson : Form
    {
        private DataRow PersonRow = null;
        private string NewPhotoType = "";
        private bool IsSystemEvent = false;
        private MainForm mainform;

        public FrmPerson(MainForm _mainform)
        {
            InitializeComponent();

            this.Tag = this.MainPanel;
            this.mainform = _mainform;

            DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "";
            this.dataView_Person.DataSource = DB_Service.MainDataSet.Tables["PersonTable"];

            //初始化下拉框内容
            InitCombox();
            //初始化DataGridView
            InitPersonView();
            //初始化所有控件的默认显示
            InitAllControl(null);
            //初始化默认显示的人员信息
            //ShowPersonInfoByIndex(0);
        }

        #region 提供的通用服务

        /// <summary>
        /// 通过指定的DataGridView中的行号初始化本窗体默认显示的人员
        /// </summary>
        /// <param name="DataGridViewIndex"></param>
        public void ShowPersonInfoByIndex(int DataGridViewIndex)
        {
            if (dataView_Person.Rows[DataGridViewIndex] != null)
            {
                ShowPersonInfoByPID(dataView_Person.Rows[DataGridViewIndex].Cells["工号"].Value.ToString());
            }
            else
            {
                MessageBox.Show("无法在数据库中找到这个人员的记录！", "数据查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 通过指定的PID，初始化本窗体默认显示的人员
        /// </summary>
        /// <param name="PID"></param>
        public void ShowPersonInfoByPID(string PID)
        {
            DataRow[] temp = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + PID.ToString() + "'");
            if (temp.Length > 0)
            {
                for (int i = 0; i < this.dataView_Person.Rows.Count; i++)
                {
                    if (this.dataView_Person.Rows[i].Cells["工号"].Value.ToString() == PID)
                    {
                        //设置行选中
                        this.dataView_Person.Rows[i].Selected = true;
                        //初始化详细人员信息
                        PersonRow = temp[0];
                        InitViewFromPersonRow();
                        return;
                    }
                }
                MessageBox.Show("无法在数据库中找到这个人员的记录！", "数据查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("无法在数据库中找到这个人员的记录！", "数据查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 通过指定的CardID，初始化本窗体默认显示的人员
        /// </summary>
        /// <param name="CardID"></param>
        public void ShowPersonInfoByCardID(int CardID)
        {
            DataRow[] temp = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + CardID);
            if (temp.Length > 0)
            {
                ShowPersonInfoByPID(temp[0]["PID"].ToString());
            }
            else
            {
                MessageBox.Show("无法在数据库中找到这条记录！", "数据查询错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 通过指定的部门，初始化本窗体默认显示的人员
        /// </summary>
        /// <param name="DataGridViewIndex"></param>
        public void ShowPersonInfoByDepartment(string Department)
        {
            com_SelectDepartment.Text = Department;
        }

        /// <summary>
        /// 通过指定的部门，初始化本窗体默认显示的人员
        /// </summary>
        /// <param name="DataGridViewIndex"></param>
        public void ShowPersonInfoByWorkType(string WorkType)
        {
            com_SelectWorkType.Text = WorkType;
        }

        #endregion

        private void InitPersonView()
        {
            label14.Text = "共有 " + DB_Service.MainDataSet.Tables["PersonTable"].Rows.Count.ToString() + " 人";
            //改这个是为了让报表打印里列名是中文
            dataView_Person.Columns["PID"].Name = "工号";
            dataView_Person.Columns["Name"].Name = "姓名";
            dataView_Person.Columns["Sex"].Name = "性别";
            dataView_Person.Columns["Age"].Name = "年龄";
            dataView_Person.Columns["Blood"].Name = "血型";
            dataView_Person.Columns["WorkType"].Name = "职务";
            dataView_Person.Columns["Department"].Name = "部门";
            dataView_Person.Columns["Tele"].Name = "电话";
            dataView_Person.Columns["Mobile"].Name = "手机";
            dataView_Person.Columns["PersonKey"].Name = "身份证号";
            dataView_Person.Columns["BirthDay"].Name = "生日";
            dataView_Person.Columns["Email"].Name = "电子邮件";
            dataView_Person.Columns["FamilyAdd"].Name = "家庭住址";
            dataView_Person.Columns["Photo"].Name = "照片";

            dataView_Person.Columns["工号"].HeaderText = "工号";
            dataView_Person.Columns["姓名"].HeaderText = "姓名";
            dataView_Person.Columns["性别"].HeaderText = "性别";
            dataView_Person.Columns["年龄"].HeaderText = "年龄";
            dataView_Person.Columns["血型"].HeaderText = "血型";
            dataView_Person.Columns["职务"].HeaderText = "职务";
            dataView_Person.Columns["部门"].HeaderText = "部门";
            dataView_Person.Columns["电话"].HeaderText = "电话";
            dataView_Person.Columns["手机"].HeaderText = "手机";
            dataView_Person.Columns["身份证号"].HeaderText = "身份证号";
            dataView_Person.Columns["生日"].HeaderText = "生日";
            dataView_Person.Columns["电子邮件"].HeaderText = "电子邮件";
            dataView_Person.Columns["家庭住址"].HeaderText = "家庭住址";
            dataView_Person.Columns["照片"].HeaderText = "照片";

            dataView_Person.Columns["工号"].Width = 80;
            dataView_Person.Columns["姓名"].Width = 70;
            dataView_Person.Columns["性别"].Width = 40;
            dataView_Person.Columns["年龄"].Width = 40;
            dataView_Person.Columns["职务"].Width = 80;
            dataView_Person.Columns["部门"].Width = 80;
            dataView_Person.Columns["血型"].Width = 40;
            dataView_Person.Columns["电话"].Width = 87;
            dataView_Person.Columns["手机"].Width = 80;
            dataView_Person.Columns["身份证号"].Width = 123;
            //设置排序列
            dataView_Person.Sort(dataView_Person.Columns["工号"], ListSortDirection.Ascending);
        }

        private void DataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                groupBox_PersonView.Enabled = false;
                ShowPersonInfoByPID(this.dataView_Person.Rows[e.RowIndex].Cells["工号"].Value.ToString());
            }
        }

        private void InitViewFromPersonRow()
        {
            tex_PID.ReadOnly = true;
            tex_Name.Text = PersonRow["Name"].ToString();
            tex_PID.Text = PersonRow["PID"].ToString();
            com_Sex.Text = PersonRow["Sex"].ToString();
            com_Age.Text = PersonRow["Age"].ToString();
            com_Blood.Text = PersonRow["Blood"].ToString();
            if (PersonRow["WorkType"].ToString() == "")
            {
                com_WorkType.SelectedIndex = -1;
            }
            else
            {
                com_WorkType.Text = PersonRow["WorkType"].ToString();
            }
            if (PersonRow["Department"].ToString() == "")
            {
                com_Department.SelectedIndex = -1;
            }
            else
            {
                com_Department.Text = PersonRow["Department"].ToString(); 
            }
            tex_Mobile.Text = PersonRow["Mobile"].ToString();
            tex_Tele.Text = PersonRow["Tele"].ToString();
            tex_PersonKey.Text = PersonRow["PersonKey"].ToString();
            tex_BirthDay.Text = PersonRow["BirthDay"].ToString();
            tex_Email.Text = PersonRow["Email"].ToString();
            tex_FamilyAdd.Text = PersonRow["FamilyAdd"].ToString();
            if (PersonRow["Photo"] != null && PersonRow["Photo"] != DBNull.Value)
            {
                MemoryStream mmm = new MemoryStream((byte[])PersonRow["Photo"]);
                pic_Photo.Image = Image.FromStream(mmm);
            }
            else
            {
                //没有照片则加载默认的空图片
                pic_Photo.Image = Resource_Service.GetImage("Person");
            }
        }

        private void InitCombox()
        {
            com_WorkType.Items.Clear();
            foreach (DataRow row in DB_Service.MainDataSet.Tables["WorkTypeTable"].Rows)
            {
                com_WorkType.Items.Add(row["WorkTypeName"].ToString());
            }
            com_Department.Items.Clear();
            foreach (DataRow row in DB_Service.MainDataSet.Tables["DepartmentTable"].Rows)
            {
                com_Department.Items.Add(row["DepartmentName"]);
            }
            com_SelectWorkType.Items.Clear();
            com_SelectWorkType.Items.Add("全部");
            foreach (DataRow row in DB_Service.MainDataSet.Tables["WorkTypeTable"].Rows)
            {
                com_SelectWorkType.Items.Add(row["WorkTypeName"].ToString());
            }
            com_SelectDepartment.Items.Clear();
            com_SelectDepartment.Items.Add("全部");
            foreach (DataRow row in DB_Service.MainDataSet.Tables["DepartmentTable"].Rows)
            {
                com_SelectDepartment.Items.Add(row["DepartmentName"]);
            }
        }

        private void pic_Photo_MouseClick(object sender, MouseEventArgs e)
        {
            linkLabel_InsertPic_LinkClicked(null,null);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (tex_Name.Text.Trim() == "" || tex_PID.Text.Trim() == "" || com_Department.Text == "" || com_WorkType.Text == "")
            {
                MessageBox.Show("请输入完整的人员信息后再进行保存。\n至少包含：姓名、工号、职务、部门。\n如果职务或部门列表为空，请先在综合设置里添加。", "人员设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //如果是新建人员，则判断工号的唯一性并初始化PersonRow
                if (PersonRow == null)
                {
                    if (Global.IsTempVersion && DB_Service.MainDataSet.Tables["PersonTable"].Rows.Count >= 20)
                    {
                        MessageBox.Show("对不起，您的软件为演示版。人员数目不能超过10个。请购买正式版本。", "新增人员", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + tex_PID.Text.Trim() + "'").Length > 0)
                    {
                        MessageBox.Show("对不起，您所输入的这个工号已经存在，请重新输入一个未被占用的工号。", "新增人员", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tex_PID.Text = "";
                        return;
                    }
                    //初始化PersonRow
                    PersonRow = DB_Service.MainDataSet.Tables["PersonTable"].NewRow();
                    DB_Service.MainDataSet.Tables["PersonTable"].Rows.Add(PersonRow);
                }
                //修改PersonRow的值
                PersonRow["PID"] = tex_PID.Text.Trim();
                PersonRow["Name"] = tex_Name.Text.Trim();
                PersonRow["Sex"] = com_Sex.Text;
                PersonRow["Age"] = com_Age.Text;
                PersonRow["Blood"] = com_Blood.Text;
                PersonRow["WorkType"] = com_WorkType.Text;
                PersonRow["Department"] = com_Department.Text;
                PersonRow["Mobile"] = tex_Mobile.Text.Trim();
                PersonRow["Tele"] = tex_Tele.Text.Trim();
                PersonRow["PersonKey"] = tex_PersonKey.Text.Trim();
                PersonRow["BirthDay"] = tex_BirthDay.Text.Trim();
                PersonRow["Email"] = tex_Email.Text.Trim();
                PersonRow["FamilyAdd"] = tex_FamilyAdd.Text.Trim();

                //当代表新图片类型的NewPhotoType不为空时才上传图片
                if (NewPhotoType != "")
                {
                    MemoryStream ms = new MemoryStream();
                    switch (NewPhotoType)
                    {
                        case "bmp":
                            pic_Photo.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            PersonRow["Photo"] = ms.ToArray();
                            break;
                        case "jpeg":
                            pic_Photo.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            PersonRow["Photo"] = ms.ToArray();
                            break;
                        case "png":
                            pic_Photo.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            PersonRow["Photo"] = ms.ToArray();
                            break;
                        case "gif":
                            pic_Photo.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                            PersonRow["Photo"] = ms.ToArray();
                            break;
                        case "none":
                            PersonRow["Photo"] = null;
                            break;
                    }
                }

                groupBox_PersonView.Enabled = false;

                //将PersonRow中的更新提交至数据库
                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["PersonTable"]) > 0)
                {
                    label14.Text = "共有 " + DB_Service.MainDataSet.Tables["PersonTable"].Rows.Count.ToString() + " 人";
                }
                else
                {
                    MessageBox.Show("保存人员信息失败！\n请确保数据库连接正确。", "人员管理");
                }
            }
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            groupBox_PersonView.Enabled = true;
            PersonRow = null;
            tex_PID.ReadOnly = false;

            tex_Name.Text = "";
            tex_PID.Text = "";
            com_Sex.SelectedIndex = 0;
            com_Age.SelectedIndex = 0;
            com_Blood.SelectedIndex = 0;
            if (com_WorkType.Items.Count > 0)
                com_WorkType.SelectedIndex = 0;
            if (com_Department.Items.Count > 0)
                com_Department.SelectedIndex = 0;
            tex_Mobile.Text = "";
            tex_Tele.Text = "";
            tex_PersonKey.Text = "";
            tex_BirthDay.Text = "";
            tex_Email.Text = "";
            tex_FamilyAdd.Text = "";
            pic_Photo.Image = Resource_Service.GetImage("Person");
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (dataView_Person.SelectedRows.Count > 0)
            {
                string DelPID = dataView_Person.SelectedRows[0].Cells["工号"].Value.ToString();
                if (MessageBox.Show("您确定要删除编号为：" + DelPID + " 的这个人员吗？\n如果这个人员绑定了卡片，则会自动取消绑定。同时他的所有定位、报警、考勤等相关历史信息也会随之删除。", "人员管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DataRow[] rows = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + DelPID + "'");
                        if (rows.Length > 0)
                        {
                            //SQL语句数组
                            List<string> strSQLs = new List<string>();
                            //删除人员
                            strSQLs.Add("delete from PersonTable where PID = '" + DelPID + "'");
                            //级联操作
                            DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + DelPID + "'");
                            if (rows_Card.Length > 0)
                            {
                                //得到CardID
                                int CardID = Convert.ToInt32(rows_Card[0]["CardID"]);
                                //取消卡片的绑定
                                strSQLs.Add("update CardTable set PID = NULL where PID = '" + DelPID + "'");
                                //删除考勤记录
                                strSQLs.Add("delete from DutyTable where CardID = " + CardID);
                                //删除定位记录
                                strSQLs.Add("delete from HistoryPositionTable where CardID = " + CardID);
                                //删除报警记录
                                strSQLs.Add("delete from AlarmPersonSendTable where CardID = " + CardID);
                            }

                            if (DB_Service.ExecuteSQLs(strSQLs, new string[5] { "PersonTable", "CardTable", "DutyTable", "HistoryPositionTable", "AlarmPersonSendTable" }) > 0)
                            {
                                DB_Service.UpdateTableFromDB(DB_Service.MainDataSet.Tables["CardTable"]);
                                DB_Service.UpdateTableFromDB(DB_Service.MainDataSet.Tables["PersonTable"]);
                                label14.Text = "共有 " + DB_Service.MainDataSet.Tables["PersonTable"].Rows.Count.ToString() + " 人";
                            }
                            else
                            {
                                MessageBox.Show("删除失败！", "人员管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("您欲删除的人员不存在。", "人员管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "人员管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一个您欲删除的员工。", "人员管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        

        private void tex_SelectPID_TextChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (tex_SelectPID.Text == "")
                {
                    btn_ShowALL_Click(sender, e);
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
                    btn_ShowALL_Click(sender, e);
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

        private void com_SelectWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_SelectWorkType.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_SelectWorkType);
                    DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "WorkType = '" + com_SelectWorkType.Text + "'";
                }
            }
        }

        private void com_SelectDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemEvent)
            {
                if (com_SelectDepartment.SelectedIndex == 0)
                {
                    btn_ShowALL_Click(sender, e);
                }
                else
                {
                    InitAllControl(com_SelectDepartment);
                    DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "Department = '" + com_SelectDepartment.Text + "'";
                }
            }
        }

        private void btn_ShowALL_Click(object sender, EventArgs e)
        {
            InitAllControl(null);
            DB_Service.MainDataSet.Tables["PersonTable"].DefaultView.RowFilter = "";
        }

        /// <summary>
        /// 清空除指定控件以外的所有控件为默认值
        /// 全部清空则传null
        /// </summary>
        /// <param name="UnCleanControl"></param>
        private void InitAllControl(object UnCleanControl)
        {
            IsSystemEvent = true;
            if (tex_SelectPID != UnCleanControl)
                tex_SelectPID.Text = "";
            if (tex_SelectName != UnCleanControl)
                tex_SelectName.Text = "";
            if (com_SelectWorkType != UnCleanControl && com_SelectWorkType.Items.Count > 0)
                com_SelectWorkType.SelectedIndex = 0;
            if (com_SelectDepartment != UnCleanControl && com_SelectDepartment.Items.Count > 0)
                com_SelectDepartment.SelectedIndex = 0;
            IsSystemEvent = false;
        }

        private void linkLabel_InsertPic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "位图文件 (*.bmp)|*.bmp|JPEG图像 (*.jpg)|*.jpg|PNG图像 (*.png)|*.png|Gif图像 (*.gif)|*.gif";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream = openFileDialog1.OpenFile();
                if (stream != null)
                {
                    pic_Photo.Image = Bitmap.FromStream(stream);
                    switch (openFileDialog1.FilterIndex)
                    {
                        case 1:
                            NewPhotoType = "bmp";
                            break;
                        case 2:
                            NewPhotoType = "jpeg";
                            break;
                        case 3:
                            NewPhotoType = "png";
                            break;
                        case 4:
                            NewPhotoType = "gif";
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("您选择的这张图片不是有效的图片格式。请重新选择。", "选择图片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void linkLabel_DelPic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pic_Photo.Image = PersonPosition.Properties.Resources.Person;
            NewPhotoType = "none";
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (tex_PID.Text != "")
            {
                groupBox_PersonView.Enabled = true;
            }
        }

        private void btn_Canel_Click(object sender, EventArgs e)
        {
            groupBox_PersonView.Enabled = false;

            if (dataView_Person.SelectedRows.Count > 0)
            {
                ShowPersonInfoByPID(dataView_Person.SelectedRows[0].Cells["工号"].Value.ToString());
            }
        }

        private void btn_SearchAlarm_Click(object sender, EventArgs e)
        {
            if (dataView_Person.SelectedRows.Count > 0)
            {
                this.mainform.MainBtn_Alarm_Click(null, null);
                this.mainform.frmAlarm.ShowPersonSendAlarmByPID(dataView_Person.SelectedRows[0].Cells["工号"].Value.ToString());
            }
            else
            {
                MessageBox.Show("请先选择一个员工。", "查询报警信息");
            }
        }

        private void btn_SearchHistory_Click(object sender, EventArgs e)
        {
            if (dataView_Person.SelectedRows.Count > 0)
            {
                try
                {
                    this.mainform.MainBtn_History_Click(null, null);
                    this.mainform.frmHistory.ShowHistoryByPID(dataView_Person.SelectedRows[0].Cells["工号"].Value.ToString());
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

        private void btn_SearchPosition_Click(object sender, EventArgs e)
        {
            if (dataView_Person.SelectedRows.Count > 0)
            {
                DataRow[] rows_Card = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + dataView_Person.SelectedRows[0].Cells["工号"].Value.ToString() + "'");
                if (rows_Card.Length > 0)
                {
                    DataRow[] rows = DB_Service.MainDataSet.Tables["PositionTable"].Select("ID = " + Convert.ToInt32(rows_Card[0]["CardID"]));
                    if (rows.Length > 0)
                    {
                        this.mainform.MainBtn_Watch_Click(null, null);
                        this.mainform.ShowPersonToolTipInMap(rows[0]["Name"].ToString(), Convert.ToInt32(rows[0]["ID"]), rows[0]["CardType"].ToString(), Convert.ToInt32(rows[0]["Geo_X"]), Convert.ToInt32(rows[0]["Geo_Y"]));
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
            if (dataView_Person.SelectedRows.Count > 0)
            {
                try
                {
                    this.mainform.MainBtn_Duty_Click(null, null);
                    this.mainform.frmDuty.ShowPersonDutyByPID(dataView_Person.SelectedRows[0].Cells["工号"].Value.ToString());
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

        private void tex_PID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //如果数据源发生错误则不处理
        }

        private void tex_Mobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataGridViewPrinter DGVP = new DataGridViewPrinter(this.dataView_Person, "人员列表", "", "", "", "", false, this.dataView_Person.ColumnCount - 1);
            DGVP.Print();
        }
    }
}