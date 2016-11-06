using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using PersonPosition.StaticService;
using PersonPosition.Common;

using SharpMap.Layers;
using SharpMap.Styles;
using SharpMap.Data;
using SharpMap.Data.Providers;

namespace PersonPosition.View
{
    public partial class FrmSystem : Form
    {
        bool isNewUser = false;
        DataRow MapLayerRow = null;

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

        public FrmSystem()
        {
            InitializeComponent();
            this.Tag = this.MainPanel;

            tabControl2_SelectedIndexChanged(null, null);
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl2.SelectedIndex)
            {
                case 0:
                    //综合设置
                    DataTable TempPower = DB_Service.GetTable("TempPower", "select * from PowerTable");
                    listView_Power.Items.Clear();
                    foreach (DataRow row in TempPower.Rows)
                    {
                        ListViewItem addItem = new ListViewItem(new string[] { row["PowerName"].ToString(), row["Comment"].ToString() });
                        addItem.Name = row["PowerName"].ToString();
                        listView_Power.Items.Add(addItem);
                    }
                    TempPower = null;
                    //初始化用户列表
                    InitUserListView();
                    //默认选中第一个用户
                    if (listView_User.Items.Count > 0)
                        listView_User.Items[0].Selected = true;
                    //初始化系统设置
                    InitSystemSetting();
                    break;
                case 1:
                    //地图设置
                    textBox_MapName.Text = Global.MapName;
                    textBox_MapComment.Text = Global.MapComment;
                    textBox_MapDistanceKey.Text = Global.MapDistanceKey.ToString();
                    textBox_MapBackgroundPic.Text = Global.MapBackgroundPic;
                    textBox_MapBackgroundPicGISZoom.Text = Global.MapBackgroundPicGISZoom.ToString();
                    textBox_MapBackgroundPicGISCenterX.Text = Global.MapBackgroundPicGISCenterX.ToString();
                    textBox_MapBackgroundPicGISCenterY.Text = Global.MapBackgroundPicGISCenterY.ToString();
                    //刷新地图设置
                    RefreshMapSet();
                    //刷新区域
                    RefreshMapArea();
                    //选中人员与车辆图层
                    DataRow[] rows_LayerSort = DB_Service.MainDataSet.Tables["LayerSortTable"].Select("TableOrShapeFile = 'PositionTable'");
                    if (rows_LayerSort.Length > 0)
                    {
                        string SortKey = rows_LayerSort[0]["ColumnName"].ToString();
                        switch (SortKey)
                        {
                            case "WorkType":
                                com_LayerSort.SelectedIndex = 0;
                                break;
                            case "Department":
                                com_LayerSort.SelectedIndex = 1;
                                break;
                            case "CardType":
                                com_LayerSort.SelectedIndex = 2;
                                break;
                        }
                    }
                    btn_SaveMap.Visible = false;
                    btn_AbortMap.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// 刷新地图设置
        /// </summary>
        private void RefreshMapSet()
        {
            this.mapImage.Map.Layers.Clear();
            listView_Layr.Items.Clear();
            com_MainMap.Items.Clear();
            MapLayerRow = null;
            //加载地图图层
            DataRow[] rows = DB_Service.MainDataSet.Tables["LayerTable"].Select("DataSourceType = 2");
            for (int i = 0; i < rows.Length; i++)
            {
                listView_Layr.Items.Add(new ListViewItem(new string[] { rows[i]["ViewOrder"].ToString(), rows[i]["LayerName"].ToString(), rows[i]["TableOrShapeFile"].ToString() }));
                com_MainMap.Items.Add(rows[i]["TableOrShapeFile"].ToString());
                CommonFun.AddLayer(rows[i]["TableOrShapeFile"].ToString(), this.mapImage, 5, -13);
            }
            //刷新Com_MainMap的Index
            RefreshComMainMapIndex();
            //清空详细显示
            tex_LayerName.Text = "";
            tex_MapFile.Text = "";
            tex_MinLabelZoom.Text = "";
            tex_MaxLabelZoom.Text = "";
            tex_ShowOrder.Text = "";
            tex_LabelLayerColName.Text = "";
            //初始化mapImage
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
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
                this.mapImage.Refresh();
                label_ZoomRuler.Text = "比例尺 1:" + Convert.ToString(Math.Round(mapImage.Map.Zoom, 0));
            }
            btn_MapSave.Visible = false;
            btn_MapAbort.Visible = false;
        }

        /// <summary>
        /// 刷新Com_MainMap的Index
        /// </summary>
        private void RefreshComMainMapIndex()
        {
            //初始化ComMainMap
            if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
            {
                string OldMainMapName = DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MainMap"].ToString();
                for (int j = 0; j < com_MainMap.Items.Count; j++)
                {
                    if (com_MainMap.Items[j].ToString() == OldMainMapName)
                    {
                        com_MainMap.SelectedIndex = j;
                        label27.Visible = false;
                        label1.ForeColor = Color.Black;
                        return;
                    }
                }
                //至此，说明MapTable中的MainMap字段的地图在当前根本不存在，则删除。
                DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MainMap"] = "";
                DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["MapTable"]);
                com_MainMap.SelectedIndex = -1;
                label27.Visible = true;
                label1.ForeColor = Color.Red;
            }
        }

        private void RefreshMapArea()
        {
            listView_MapArea.Items.Clear();
            for (int i = 0; i < DB_Service.MainDataSet.Tables["MapAreaTable"].Rows.Count; i++)
            {
                listView_MapArea.Items.Add(DB_Service.MainDataSet.Tables["MapAreaTable"].Rows[i]["MapAreaName"].ToString());
            }
            text_MapAreaName.Text = "";
        }

        #region 综合参数设置

        /// <summary>
        /// 初始化系统设置
        /// </summary>
        private void InitSystemSetting()
        {
            check_AutoStart.Checked = Global.AutoStart;
            check_AutoLogin.Checked = Global.AutoLogin;
            check_AutoRunLED.Checked = Global.AutoRunLED;
            check_IsShowBug.Checked = Global.IsShowBug;
            tex_LockPassword.Text = Global.LockPassword;
            if (Global.TouMingLock)
            {
                radio_XiangDuiLock.Checked = true;
            }
            else
            {
                radio_JueDuiLock.Checked = true;
            }
            com_TimeToolTip.SelectedIndex = com_TimeToolTip.FindStringExact(Global.TimeToolTip.ToString(), -1);
        }

        /// <summary>
        /// 初始化listView_User的内容
        /// </summary>
        private void InitUserListView()
        {
            listView_User.Items.Clear();
            for (int i = 0; i < DB_Service.MainDataSet.Tables["UserTable"].Rows.Count; i++)
            {
                listView_User.Items.Add(new ListViewItem(new string[2] { DB_Service.MainDataSet.Tables["UserTable"].Rows[i]["UserName"].ToString(), DB_Service.MainDataSet.Tables["UserTable"].Rows[i]["RegTime"].ToString() }));
            }
        }

        private void InitUserInfo(string userName)
        {
            DataRow row = DB_Service.MainDataSet.Tables["UserTable"].Select("UserName ='" + userName + "'")[0];
            isNewUser = false;
            textBox8.ReadOnly = true;
            linkLabel1.Visible = false;
            btn_Save.Visible = false;
            btn_Abort.Visible = false;
            textBox8.Text = row["UserName"].ToString();
            textBox7.Text = row["Password"].ToString();
            textBox9.Text = textBox7.Text;
            if (Convert.ToBoolean(row["IsAlive"]))
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

            foreach (ListViewItem item in listView_Power.Items)
            {
                item.Checked = false;
            }
            foreach (string s in Regex.Split(row["PowerStr"].ToString(), "-"))
            {
                if (s != "")
                {
                    listView_Power.Items[Convert.ToInt32(s)].Checked = true;
                }
            }
        }

        private void listView_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_User.SelectedItems.Count > 0)
            {
                InitUserInfo(listView_User.SelectedItems[0].SubItems[0].Text);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBox8.Text.Trim() != "")
            {
                try
                {
                    if (CheckUserName(textBox8.Text.Trim()))
                    {
                        MessageBox.Show("恭喜您，这个用户名可以使用。", "用户管理");
                    }
                    else
                    {
                        MessageBox.Show("这个用户名已经使用过了，请更换一个。", "用户管理");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool CheckUserName(string UserName)
        {
            try
            {
                DB_Service.UpdateTableFromDB(DB_Service.MainDataSet.Tables["UserTable"]);

                DataRow[] rows = DB_Service.MainDataSet.Tables["UserTable"].Select("UserName = '" + UserName + "'");
                if (rows.Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            isNewUser = true;
            textBox8.ReadOnly = false;
            linkLabel1.Visible = true;
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            radioButton2.Checked = true;
            foreach (ListViewItem item in listView_Power.Items)
            {
                item.Checked = false;
            }
            if (listView_Power.Items["信息采集管理权限"] != null)
                listView_Power.Items["信息采集管理权限"].Checked = true;
            btn_Save.Visible = true;
            btn_Abort.Visible = true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (textBox7.Text.Trim() == textBox9.Text.Trim())
            {
                DataRow updateRow = null;
                if (isNewUser)
                {
                    if (textBox8.Text.Trim().Length >= 2)
                    {
                        if (CheckUserName(textBox8.Text.Trim()))
                        {
                            updateRow = DB_Service.MainDataSet.Tables["UserTable"].NewRow();
                            DB_Service.MainDataSet.Tables["UserTable"].Rows.Add(updateRow);
                        }
                        else
                        {
                            MessageBox.Show("这个用户名已经使用过了，请更换一个。", "用户管理");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("必须输入用户名，长度不小于2。", "用户管理");
                        return;
                    }
                }
                else
                {
                    DataRow[] tempRow = DB_Service.MainDataSet.Tables["UserTable"].Select("UserName = '" + textBox8.Text.Trim() + "'");
                    if (tempRow.Length > 0)
                    {
                        updateRow = tempRow[0];
                    }
                    else
                    {
                        DB_Service.UpdateTableFromDB(DB_Service.MainDataSet.Tables["UserTable"]);
                        return;
                    }
                }
                updateRow["UserName"] = textBox8.Text.Trim();
                updateRow["Password"] = textBox7.Text.Trim();
                updateRow["RegTime"] = DateTime.Now;
                if (radioButton2.Checked)
                {
                    updateRow["IsAlive"] = true;
                }
                else
                {
                    updateRow["IsAlive"] = false;
                }
                string tempStrPower = "";
                for (int i = 0; i < listView_Power.Items.Count; i++)
                {
                    if (listView_Power.Items[i].Checked)
                    {
                        tempStrPower = tempStrPower + i.ToString() + "-";
                    }
                }
                updateRow["PowerStr"] = tempStrPower;
                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["UserTable"]) > 0)
                {
                    btn_Save.Visible = false;
                    btn_Abort.Visible = false;
                }
                else
                {
                    MessageBox.Show("保存用户信息失败！\n请确保数据库连接正确。", "用户管理");
                }
            }
            else
            {
                MessageBox.Show("两次密码不一致，请确认密码。", "用户管理");
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {

            btn_Save.Visible = true;
            btn_Abort.Visible = true;
        }

        private void btn_Abort_Click(object sender, EventArgs e)
        {
            //默认选中第一个用户
            if (listView_User.Items.Count > 0)
                listView_User.Items[0].Selected = true;
            btn_Save.Visible = false;
            btn_Abort.Visible = false;
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (listView_User.SelectedItems.Count > 0)
            {
                string DelUserName = listView_User.SelectedItems[0].SubItems[0].Text;
                if (DelUserName != Global.PresentUser)
                {
                    if (MessageBox.Show("您确定要删除 " + DelUserName + " 这个用户吗？", "删除用户", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataRow[] rows = DB_Service.MainDataSet.Tables["UserTable"].Select("UserName = '" + DelUserName + "'");
                        if (rows.Length > 0)
                        {
                            rows[0].Delete();
                            if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["UserTable"]) == 0)
                            {
                                MessageBox.Show("删除失败！", "删除用户", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("您欲删除的用户不存在。", "删除用户", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                       
                    }
                }
                else
                {
                    MessageBox.Show("对不起，您不能删除当前登录用户。您可以登录其他有用户管理权限的账户来进行删除。", "用户管理");
                }
                listView_User.Items.Remove(listView_User.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("请先选择一个您欲删除的用户。", "用户管理");
            }
        }

        private void btn_SaveSetting_Click(object sender, EventArgs e)
        {
            Global.AutoStart = check_AutoStart.Checked;
            if (check_AutoStart.Checked)
            {
                CommonFun.SetAutoRunWhenStart(true, "PersonPosition.exe", AppDomain.CurrentDomain.BaseDirectory + "PersonPosition.exe");
            }
            else
            {
                CommonFun.SetAutoRunWhenStart(false, "PersonPosition.exe", AppDomain.CurrentDomain.BaseDirectory + "PersonPosition.exe");
            }
            Global.AutoLogin = check_AutoLogin.Checked;
            if (check_AutoLogin.Checked)
            {
                Global.AutoLoginUser = Global.PresentUser;
            }
            else
            {
                Global.AutoLoginUser = "NULL";
            }
            Global.AutoRunLED = check_AutoRunLED.Checked;
            if (radio_XiangDuiLock.Checked)
            {
                Global.TouMingLock = true;
            }
            else
            {
                Global.TouMingLock = false;
            }
            Global.IsShowBug = check_IsShowBug.Checked;
            Global.LockPassword = tex_LockPassword.Text.Trim();
            Global.TimeToolTip = Convert.ToInt32(com_TimeToolTip.Text);
        }

        private void btn_AbortSetting_Click(object sender, EventArgs e)
        {
            InitSystemSetting();
        }

        private void linkLabel_RecoverLED_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Global.LEDLeft = Screen.PrimaryScreen.WorkingArea.Width / 2;
            Global.LEDTop = Screen.PrimaryScreen.WorkingArea.Height / 2;
        }

        #endregion

        #region 地图参数设置

        private void com_LayerSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView_LayerSort.Items.Clear();
            switch (com_LayerSort.Text)
            {
                case "按职位":
                    for (int i = 0; i < DB_Service.MainDataSet.Tables["WorkTypeTable"].Rows.Count; i++)
                    {
                        listView_LayerSort.Items.Add(new ListViewItem(new string[2] { DB_Service.MainDataSet.Tables["WorkTypeTable"].Rows[i]["WorkTypeName"].ToString(), DB_Service.MainDataSet.Tables["WorkTypeTable"].Rows[i]["WorkTypeName"].ToString() }));
                    }
                    break;
                case "按部门":
                    for (int i = 0; i < DB_Service.MainDataSet.Tables["DepartmentTable"].Rows.Count; i++)
                    {
                        listView_LayerSort.Items.Add(new ListViewItem(new string[2] { DB_Service.MainDataSet.Tables["DepartmentTable"].Rows[i]["DepartmentName"].ToString(), DB_Service.MainDataSet.Tables["DepartmentTable"].Rows[i]["DepartmentName"].ToString() }));
                    }
                    break;
                case "按卡片类型":
                    for (int i = 0; i < DB_Service.MainDataSet.Tables["CardTypeTable"].Rows.Count; i++)
                    {
                        listView_LayerSort.Items.Add(new ListViewItem(new string[2] { DB_Service.MainDataSet.Tables["CardTypeTable"].Rows[i]["CardType"].ToString(), DB_Service.MainDataSet.Tables["CardTypeTable"].Rows[i]["CardType"].ToString() }));
                    }
                    break;
            }

            btn_SaveMap.Visible = true;
            btn_AbortMap.Visible = true;
        }

        private void btn_DelMapArea_Click(object sender, EventArgs e)
        {
            if (listView_MapArea.SelectedItems.Count > 0)
            {
                string DelAreaName = listView_MapArea.SelectedItems[0].SubItems[0].Text;
                if (MessageBox.Show("您确定要删除 " + DelAreaName + " 这个区域吗？同时还会删除基站里的相应区域信息。", "删除区域", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] delRows = DB_Service.MainDataSet.Tables["MapAreaTable"].Select("MapAreaName = '" + DelAreaName + "'");
                    if (delRows.Length > 0)
                    {
                        delRows[0].Delete();
                        if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["MapAreaTable"]) > 0)
                        {
                            //同时删除基站里的相应区域信息
                            DB_Service.ExecuteSQL("update StationTable set Area = null where Area = '" + DelAreaName + "'","StationTable");
                            //更新
                            DB_Service.UpdateTableFromDB(DB_Service.MainDataSet.Tables["StationTable"]);
                            //刷新区域
                            RefreshMapArea();
                        }
                        else
                        {
                            MessageBox.Show("删除失败！请检查数据库连接。", "删除区域", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲删除的区域不存在。", "区域管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择一个欲删除的区域。", "删除区域", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_AddMapArea_Click(object sender, EventArgs e)
        {
            string NewMapAreaName = text_MapAreaName.Text.Trim();
            if (NewMapAreaName != "")
            {
                DataTable tempMapAreaTable=DB_Service.GetTable("TempMapAreaTable", "select * from MapAreaTable");

                if (DB_Service.MainDataSet.Tables["MapAreaTable"].Select("MapAreaName = '" + NewMapAreaName + "'").Length > 0)
                {
                    MessageBox.Show("对不起，这个区域名称已经使用过，请换一个名称后再添加。", "添加区域", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataRow newRow = DB_Service.MainDataSet.Tables["MapAreaTable"].NewRow();
                    newRow["MapAreaName"] = NewMapAreaName;
                    DB_Service.MainDataSet.Tables["MapAreaTable"].Rows.Add(newRow);
                    if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["MapAreaTable"]) > 0)
                    {
                        //刷新区域
                        RefreshMapArea();
                    }
                    else
                    {
                        MessageBox.Show("添加失败！请检查数据库连接。", "添加区域", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btn_SaveMap_Click(object sender, EventArgs e)
        {
            try
            {
                //保存地图全局参数
                Global.MapName = textBox_MapName.Text;
                Global.MapComment = textBox_MapComment.Text;
                Global.MapDistanceKey = Convert.ToDouble(textBox_MapDistanceKey.Text);
                //以上仅仅是更新MapTable表，下面真正更新数据库
                DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["MapTable"]);
                //保存图层分类依据
                if (listView_LayerSort.Items.Count > 0)
                {
                    if (listView_LayerSort.Items.Count <= 17)
                    {
                        string SortKey;
                        switch (com_LayerSort.SelectedIndex)
                        {
                            case 0:
                                SortKey = "WorkType";
                                break;
                            case 1:
                                SortKey = "Department";
                                break;
                            default:
                                SortKey = "CardType";
                                break;
                        }
                        DB_Service.ExecuteSQL("delete from LayerSortTable where TableOrShapeFile = 'PositionTable'", "LayerSortTable");
                        List<string> TempSQL = new List<string>();
                        for (int i = 0; i < listView_LayerSort.Items.Count; i++)
                        {
                            TempSQL.Add("insert into LayerSortTable (TableOrShapeFile,ColumnName,MaybeValue,Text,ViewOrder,PicID,PointImage) values ('PositionTable','" + SortKey + "','" + listView_LayerSort.Items[i].SubItems[0].Text + "','" + listView_LayerSort.Items[i].SubItems[1].Text + "'," + i.ToString() + ",'Person_" + Convert.ToString(i + 1) + "','Person_" + Convert.ToString(i + 1) + "')");
                        }
                        DB_Service.ExecuteSQLs(TempSQL, new string[1] { "LayerSortTable" });
                        MessageBox.Show("设置成功！重启软件后生效。", "设置人员车辆图层", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("对不起，分类图层数超出系统最大限制：17。");
                    }
                }
                else
                {
                    MessageBox.Show("对不起，按这个分类不会产生任何图层");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "保存地图参数异常");
            }
        }

        private void btn_AbortMap_Click(object sender, EventArgs e)
        {
            tabControl2_SelectedIndexChanged(null, null);
        }

        private void textBox_MapName_TextChanged(object sender, EventArgs e)
        {
            btn_SaveMap.Visible = true;
            btn_AbortMap.Visible = true;
        }

        private void textBox_MapComment_TextChanged(object sender, EventArgs e)
        {
            btn_SaveMap.Visible = true;
            btn_AbortMap.Visible = true;
        }

        private void textBox_MapDistanceKey_TextChanged(object sender, EventArgs e)
        {
            btn_SaveMap.Visible = true;
            btn_AbortMap.Visible = true;
        }

        #endregion

        #region 地图图层设置

        private void mapImage_SizeChanged(object sender, EventArgs e)
        {
            this.mapImage.Refresh();
        }

        private void mapImage_MapZoomChanged(double zoom)
        {
            label_ZoomRuler.Text = "比例尺 1:" + Convert.ToString(Math.Round(mapImage.Map.Zoom, 0));
        }

        private void btn_MapCreate_Click(object sender, EventArgs e)
        {
            MapLayerRow = null;

            //清空详细显示
            tex_LayerName.Text = "";
            tex_MapFile.Text = "";
            tex_MinLabelZoom.Text = "0";
            tex_MaxLabelZoom.Text = "9999999";
            tex_ShowOrder.Text = "";
            tex_LabelLayerColName.Text = "Name";

            btn_MapSave.Visible = true;
            btn_MapAbort.Visible = true;
        }

        private void btn_MapAbort_Click(object sender, EventArgs e)
        {
            //清空详细显示
            tex_LayerName.Text = "";
            tex_MapFile.Text = "";
            tex_MinLabelZoom.Text = "";
            tex_MaxLabelZoom.Text = "";
            tex_ShowOrder.Text = "";
            tex_LabelLayerColName.Text = "";

            btn_MapSave.Visible = false;
            btn_MapAbort.Visible = false;
        }

        private void btn_MapEdit_Click(object sender, EventArgs e)
        {
            if (listView_Layr.SelectedItems.Count > 0)
            {
                btn_MapSave.Visible = true;
                btn_MapAbort.Visible = true;
            }
            else
            {
                MessageBox.Show("请先选择一个图层。", "修改地图图层", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_MapDel_Click(object sender, EventArgs e)
        {
            if (listView_Layr.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("您确定要删除 " + listView_Layr.SelectedItems[0].SubItems[1].Text + " 这个图层吗？", "删除地图图层", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] rows = DB_Service.MainDataSet.Tables["LayerTable"].Select("TableOrShapeFile = '" + listView_Layr.SelectedItems[0].SubItems[2].Text + "'");
                    if (rows.Length > 0)
                    {
                        rows[0].Delete();
                        MapLayerRow = null;
                        if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["LayerTable"]) > 0)
                        {
                            //刷新所有地图的显示
                            RefreshMapSet();
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "地图图层管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("您欲删除的图层不存在。", "地图图层管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择一个欲删除的图层。", "删除地图图层", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listView_Layr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Layr.SelectedItems.Count > 0)
            {
                DataRow[] rows = DB_Service.MainDataSet.Tables["LayerTable"].Select("TableOrShapeFile = '" + listView_Layr.SelectedItems[0].SubItems[2].Text + "'");
                if (rows.Length > 0)
                {
                    //初始化MapLayerRow
                    MapLayerRow = rows[0];
                    tex_LayerName.Text = MapLayerRow["LayerName"].ToString();
                    tex_MapFile.Text = MapLayerRow["TableOrShapeFile"].ToString();
                    tex_LabelLayerColName.Text = MapLayerRow["LabelLayerColName"].ToString();
                    tex_MinLabelZoom.Text = MapLayerRow["LabelLayerMinShow"].ToString();
                    tex_MaxLabelZoom.Text = MapLayerRow["LabelLayerMaxShow"].ToString();
                    tex_ShowOrder.Text = MapLayerRow["ViewOrder"].ToString();
                    //判断地图文件的存在性
                    if (!File.Exists(Global.MapPath + tex_MapFile.Text))
                    {
                        MessageBox.Show("这个图层的地图文件不存在！\n您如果不需要这个图层，可以在上方的删除图层里删去这个图层。", "查看地图图层", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("这个图层不存在！。", "编辑地图图层", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listView_Layr.Items.Remove(listView_Layr.SelectedItems[0]);
                }
            }
        }

        private void btn_MapSave_Click(object sender, EventArgs e)
        {
            if (tex_LayerName.Text.Trim() == "" || tex_MapFile.Text.Trim() == "" || tex_ShowOrder.Text.Trim() == "" || tex_LabelLayerColName.Text.Trim() == "" || tex_MinLabelZoom.Text.Trim() == "" || tex_MaxLabelZoom.Text.Trim() == "")
            {
                MessageBox.Show("请输入完整的地图图层信息后再进行保存。\n至少包含：图层名称、地图文件、显示次序、标注列名、标注最小、最大显示比例", "地图图层设置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //如果是新建地图图层，则判断地图图层名称的唯一性并初始化MapLayerRow
                if (MapLayerRow == null)
                {
                    if (DB_Service.MainDataSet.Tables["LayerTable"].Select("TableOrShapeFile = '" + tex_MapFile.Text.Trim() + "'").Length > 0)
                    {
                        MessageBox.Show("对不起，您所使用的地图文件已经添加过了，请使用一个新的地图文件。", "新增地图图层", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tex_MapFile.Text = "";
                        return;
                    }
                    else
                    {
                        //初始化MapLayerRow
                        MapLayerRow = DB_Service.MainDataSet.Tables["LayerTable"].NewRow();
                        DB_Service.MainDataSet.Tables["LayerTable"].Rows.Add(MapLayerRow);
                        //添加用户不可见的隐含字段数据
                        MapLayerRow["DataSourceType"] = 2;
                        MapLayerRow["MapName"] = Global.MapName;
                        MapLayerRow["PicID"] = "Map";
                        MapLayerRow["PointImage"] = "Map";
                    }
                }

                //修改MapLayerRow的值
                MapLayerRow["LayerName"] = tex_LayerName.Text.Trim();
                MapLayerRow["TableOrShapeFile"] = tex_MapFile.Text.Trim();
                MapLayerRow["LabelLayerColName"] = tex_LabelLayerColName.Text.Trim();
                MapLayerRow["LabelLayerMinShow"] = tex_MinLabelZoom.Text.Trim();
                MapLayerRow["LabelLayerMaxShow"] = tex_MaxLabelZoom.Text.Trim();
                MapLayerRow["ViewOrder"] = tex_ShowOrder.Text.Trim();

                MapLayerRow["Line_Color"] = "255,0,0,0";
                MapLayerRow["Line_Width"] = "1";
                MapLayerRow["Fill_IsSolid"] = "True";
                MapLayerRow["Fill_Color"] = "255,0,0,0";
                MapLayerRow["Fill_Image"] = "";
                MapLayerRow["FillLine_Enable"] = "False";
                MapLayerRow["FillLine_Color"] = "255,0,0,0";
                MapLayerRow["FillLine_Width"] = "1";
                MapLayerRow["IsShowInTree"] = "True";

                //将MapLayerRow中的更新提交至数据库
                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["LayerTable"]) > 0)
                {
                    btn_MapSave.Visible = false;
                    btn_MapAbort.Visible = false;
                    //刷新所有地图的显示
                    RefreshMapSet();
                    MessageBox.Show("保存地图图层成功！\n重启软件后生效。", "地图图层管理");
                }
                else
                {
                    MessageBox.Show("保存地图图层信息失败！\n请确保数据库连接正确。", "地图图层管理");
                }
            }
        }

        private void tex_ShowOrder_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(tex_ShowOrder.Text);
            }
            catch
            {
                if (tex_ShowOrder.Text.Length > 0)
                {
                    tex_ShowOrder.Text = tex_ShowOrder.Text.Substring(0, tex_ShowOrder.Text.Length - 1);
                }
                else
                {
                    tex_ShowOrder.Text = "";
                }
            }
        }

        private void tex_ShowOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void tex_MinLabelZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void tex_MaxLabelZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void link_MapOpenFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = Global.MapPath;
            openFileDialog1.Filter = "GIS地图文件 (*.shp)|*.shp";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] templist = Regex.Split(openFileDialog1.FileName, @"\\");
                tex_MapFile.Text = templist[templist.Length - 1];
            }
        }

        private void com_MainMap_DropDownClosed(object sender, EventArgs e)
        {
            if (com_MainMap.Text != "")
            {
                if (MessageBox.Show("您是否确定将 <" + com_MainMap.Text + "> 设置为：免绘主地图？", "设置免绘主地图", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //更新MapTable中的MainMap字段
                    if (DB_Service.MainDataSet.Tables["MapTable"].Rows.Count > 0)
                    {
                        DB_Service.MainDataSet.Tables["MapTable"].Rows[0]["MainMap"] = com_MainMap.Text.Trim();
                        DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["MapTable"]);
                    }
                }
                //刷新Com_MainMap的Index
                RefreshComMainMapIndex();
            }
        }

        #endregion
    }
}