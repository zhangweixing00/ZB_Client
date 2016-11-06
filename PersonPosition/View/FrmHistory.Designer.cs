namespace PersonPosition.View
{
    partial class FrmHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.group_HistoryMap = new System.Windows.Forms.GroupBox();
            this.toolStripMap = new System.Windows.Forms.ToolStrip();
            this.btn_ZommIn = new System.Windows.Forms.ToolStripButton();
            this.btn_ZommOut = new System.Windows.Forms.ToolStripButton();
            this.btn_Move = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_Brows = new System.Windows.Forms.ToolStripButton();
            this.panel_BackImage = new System.Windows.Forms.Panel();
            this.mapImage = new SharpMap.Forms.MapImage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_ShowMore = new System.Windows.Forms.CheckBox();
            this.com_SelectDepartment = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Export = new System.Windows.Forms.Button();
            this.text_History = new System.Windows.Forms.TextBox();
            this.text_DutyInfo = new System.Windows.Forms.TextBox();
            this.listView_InMine = new System.Windows.Forms.ListView();
            this.InMine_PID = new System.Windows.Forms.ColumnHeader();
            this.InMine_CardID = new System.Windows.Forms.ColumnHeader();
            this.InMine_Name = new System.Windows.Forms.ColumnHeader();
            this.InMine_WorkType = new System.Windows.Forms.ColumnHeader();
            this.InMine_Department = new System.Windows.Forms.ColumnHeader();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_Play = new System.Windows.Forms.Button();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.btn_SearchHistory = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.com_PlaySpeed = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_SearchInMine = new System.Windows.Forms.Button();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.com_EndMinute = new System.Windows.Forms.ComboBox();
            this.com_EndHour = new System.Windows.Forms.ComboBox();
            this.com_StartMinute = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.com_StartHour = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label_InMine = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.MainPanel.SuspendLayout();
            this.group_HistoryMap.SuspendLayout();
            this.toolStripMap.SuspendLayout();
            this.panel_BackImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapImage)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.MainPanel.Controls.Add(this.group_HistoryMap);
            this.MainPanel.Controls.Add(this.groupBox3);
            this.MainPanel.Controls.Add(this.groupBox4);
            this.MainPanel.Controls.Add(this.groupBox2);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1077, 703);
            this.MainPanel.TabIndex = 0;
            // 
            // group_HistoryMap
            // 
            this.group_HistoryMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.group_HistoryMap.BackColor = System.Drawing.Color.LightSteelBlue;
            this.group_HistoryMap.Controls.Add(this.toolStripMap);
            this.group_HistoryMap.Controls.Add(this.panel_BackImage);
            this.group_HistoryMap.Location = new System.Drawing.Point(524, 183);
            this.group_HistoryMap.Name = "group_HistoryMap";
            this.group_HistoryMap.Size = new System.Drawing.Size(547, 514);
            this.group_HistoryMap.TabIndex = 12;
            this.group_HistoryMap.TabStop = false;
            this.group_HistoryMap.Text = "历史移动轨迹";
            // 
            // toolStripMap
            // 
            this.toolStripMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStripMap.AutoSize = false;
            this.toolStripMap.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMap.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMap.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_ZommIn,
            this.btn_ZommOut,
            this.btn_Move,
            this.toolStripSeparator2,
            this.btn_Brows});
            this.toolStripMap.Location = new System.Drawing.Point(7, 14);
            this.toolStripMap.Name = "toolStripMap";
            this.toolStripMap.Size = new System.Drawing.Size(532, 25);
            this.toolStripMap.TabIndex = 7;
            // 
            // btn_ZommIn
            // 
            this.btn_ZommIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ZommIn.Image = global::PersonPosition.Properties.Resources.Add;
            this.btn_ZommIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ZommIn.Name = "btn_ZommIn";
            this.btn_ZommIn.Size = new System.Drawing.Size(23, 22);
            this.btn_ZommIn.Text = "放大";
            this.btn_ZommIn.Click += new System.EventHandler(this.btn_ZommIn_Click);
            // 
            // btn_ZommOut
            // 
            this.btn_ZommOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ZommOut.Image = global::PersonPosition.Properties.Resources.jian;
            this.btn_ZommOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ZommOut.Name = "btn_ZommOut";
            this.btn_ZommOut.Size = new System.Drawing.Size(23, 22);
            this.btn_ZommOut.Text = "缩小";
            this.btn_ZommOut.Click += new System.EventHandler(this.btn_ZommOut_Click);
            // 
            // btn_Move
            // 
            this.btn_Move.Checked = true;
            this.btn_Move.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btn_Move.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Move.Image = global::PersonPosition.Properties.Resources.Move;
            this.btn_Move.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Move.Name = "btn_Move";
            this.btn_Move.Size = new System.Drawing.Size(23, 22);
            this.btn_Move.Text = "平移";
            this.btn_Move.Click += new System.EventHandler(this.btn_Move_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_Brows
            // 
            this.btn_Brows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Brows.Image = global::PersonPosition.Properties.Resources.Brows;
            this.btn_Brows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Brows.Name = "btn_Brows";
            this.btn_Brows.Size = new System.Drawing.Size(23, 22);
            this.btn_Brows.Text = "标准大小";
            this.btn_Brows.Click += new System.EventHandler(this.btn_Brows_Click);
            // 
            // panel_BackImage
            // 
            this.panel_BackImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_BackImage.BackColor = System.Drawing.Color.White;
            this.panel_BackImage.Controls.Add(this.mapImage);
            this.panel_BackImage.Location = new System.Drawing.Point(7, 39);
            this.panel_BackImage.Name = "panel_BackImage";
            this.panel_BackImage.Size = new System.Drawing.Size(533, 469);
            this.panel_BackImage.TabIndex = 12;
            this.panel_BackImage.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_BackImage_Paint);
            // 
            // mapImage
            // 
            this.mapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.None;
            this.mapImage.BackColor = System.Drawing.Color.Transparent;
            this.mapImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.mapImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapImage.FineZoomFactor = 10;
            this.mapImage.Location = new System.Drawing.Point(0, 0);
            this.mapImage.Name = "mapImage";
            this.mapImage.QueryLayerIndex = 0;
            this.mapImage.Size = new System.Drawing.Size(533, 469);
            this.mapImage.TabIndex = 6;
            this.mapImage.TabStop = false;
            this.mapImage.WheelZoomMagnitude = 2;
            this.mapImage.SizeChanged += new System.EventHandler(this.mapImage_SizeChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.checkBox_ShowMore);
            this.groupBox3.Controls.Add(this.com_SelectDepartment);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btn_Export);
            this.groupBox3.Controls.Add(this.text_History);
            this.groupBox3.Controls.Add(this.text_DutyInfo);
            this.groupBox3.Controls.Add(this.listView_InMine);
            this.groupBox3.Controls.Add(this.btn_Stop);
            this.groupBox3.Controls.Add(this.btn_Play);
            this.groupBox3.Controls.Add(this.DataGridView);
            this.groupBox3.Controls.Add(this.btn_SearchHistory);
            this.groupBox3.Location = new System.Drawing.Point(10, 183);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(506, 514);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "人员列表(双击回放历史轨迹)";
            // 
            // checkBox_ShowMore
            // 
            this.checkBox_ShowMore.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_ShowMore.Checked = true;
            this.checkBox_ShowMore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ShowMore.Location = new System.Drawing.Point(5, 17);
            this.checkBox_ShowMore.Name = "checkBox_ShowMore";
            this.checkBox_ShowMore.Size = new System.Drawing.Size(167, 22);
            this.checkBox_ShowMore.TabIndex = 82;
            this.checkBox_ShowMore.Text = "    显示文字描述信息 >>";
            this.checkBox_ShowMore.UseVisualStyleBackColor = true;
            this.checkBox_ShowMore.CheckedChanged += new System.EventHandler(this.checkBox_ShowMore_CheckedChanged);
            // 
            // com_SelectDepartment
            // 
            this.com_SelectDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_SelectDepartment.FormattingEnabled = true;
            this.com_SelectDepartment.Location = new System.Drawing.Point(49, 41);
            this.com_SelectDepartment.Name = "com_SelectDepartment";
            this.com_SelectDepartment.Size = new System.Drawing.Size(123, 20);
            this.com_SelectDepartment.TabIndex = 81;
            this.com_SelectDepartment.SelectedIndexChanged += new System.EventHandler(this.com_SelectDepartment_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(8, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 80;
            this.label6.Text = "部门：";
            // 
            // btn_Export
            // 
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Export.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Export.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Export.Image = global::PersonPosition.Properties.Resources.Excel;
            this.btn_Export.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Export.Location = new System.Drawing.Point(420, 481);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(79, 26);
            this.btn_Export.TabIndex = 78;
            this.btn_Export.Text = "导出  ";
            this.btn_Export.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // text_History
            // 
            this.text_History.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.text_History.Location = new System.Drawing.Point(282, 16);
            this.text_History.Multiline = true;
            this.text_History.Name = "text_History";
            this.text_History.ReadOnly = true;
            this.text_History.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_History.Size = new System.Drawing.Size(222, 461);
            this.text_History.TabIndex = 13;
            // 
            // text_DutyInfo
            // 
            this.text_DutyInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.text_DutyInfo.Location = new System.Drawing.Point(175, 16);
            this.text_DutyInfo.Multiline = true;
            this.text_DutyInfo.Name = "text_DutyInfo";
            this.text_DutyInfo.ReadOnly = true;
            this.text_DutyInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_DutyInfo.Size = new System.Drawing.Size(108, 461);
            this.text_DutyInfo.TabIndex = 68;
            // 
            // listView_InMine
            // 
            this.listView_InMine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listView_InMine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.listView_InMine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.InMine_PID,
            this.InMine_CardID,
            this.InMine_Name,
            this.InMine_WorkType,
            this.InMine_Department});
            this.listView_InMine.FullRowSelect = true;
            this.listView_InMine.GridLines = true;
            this.listView_InMine.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_InMine.Location = new System.Drawing.Point(5, 61);
            this.listView_InMine.MultiSelect = false;
            this.listView_InMine.Name = "listView_InMine";
            this.listView_InMine.Size = new System.Drawing.Size(167, 416);
            this.listView_InMine.TabIndex = 67;
            this.listView_InMine.UseCompatibleStateImageBehavior = false;
            this.listView_InMine.View = System.Windows.Forms.View.Details;
            this.listView_InMine.DoubleClick += new System.EventHandler(this.listView_InMine_DoubleClick);
            this.listView_InMine.Click += new System.EventHandler(this.listView_InMine_Click);
            // 
            // InMine_PID
            // 
            this.InMine_PID.Text = "工号";
            this.InMine_PID.Width = 45;
            // 
            // InMine_CardID
            // 
            this.InMine_CardID.Text = "卡号";
            this.InMine_CardID.Width = 45;
            // 
            // InMine_Name
            // 
            this.InMine_Name.Text = "姓名";
            this.InMine_Name.Width = 71;
            // 
            // InMine_WorkType
            // 
            this.InMine_WorkType.Text = "职务";
            // 
            // InMine_Department
            // 
            this.InMine_Department.Text = "部门";
            // 
            // btn_Stop
            // 
            this.btn_Stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Stop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Stop.Image = global::PersonPosition.Properties.Resources.Abort;
            this.btn_Stop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Stop.Location = new System.Drawing.Point(92, 482);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(65, 26);
            this.btn_Stop.TabIndex = 48;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_Play
            // 
            this.btn_Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Play.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Play.Image = global::PersonPosition.Properties.Resources.RePlay;
            this.btn_Play.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Play.Location = new System.Drawing.Point(5, 482);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(81, 26);
            this.btn_Play.TabIndex = 32;
            this.btn_Play.Text = "回放轨迹";
            this.btn_Play.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Play.UseVisualStyleBackColor = true;
            this.btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.DataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.DataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Location = new System.Drawing.Point(5, 497);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.RowTemplate.Height = 23;
            this.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.Size = new System.Drawing.Size(157, 10);
            this.DataGridView.TabIndex = 1;
            this.DataGridView.Visible = false;
            this.DataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDoubleClick);
            // 
            // btn_SearchHistory
            // 
            this.btn_SearchHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SearchHistory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SearchHistory.Image = global::PersonPosition.Properties.Resources.ShowAny;
            this.btn_SearchHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_SearchHistory.Location = new System.Drawing.Point(0, 489);
            this.btn_SearchHistory.Name = "btn_SearchHistory";
            this.btn_SearchHistory.Size = new System.Drawing.Size(59, 29);
            this.btn_SearchHistory.TabIndex = 31;
            this.btn_SearchHistory.Text = "回放人员历史轨迹";
            this.btn_SearchHistory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_SearchHistory.UseVisualStyleBackColor = true;
            this.btn_SearchHistory.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.com_PlaySpeed);
            this.groupBox4.Location = new System.Drawing.Point(792, 65);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(212, 107);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "动画回放参数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(24, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 47;
            this.label7.Text = "动画播放速度：";
            // 
            // com_PlaySpeed
            // 
            this.com_PlaySpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_PlaySpeed.FormattingEnabled = true;
            this.com_PlaySpeed.Items.AddRange(new object[] {
            "慢",
            "中",
            "快"});
            this.com_PlaySpeed.Location = new System.Drawing.Point(112, 41);
            this.com_PlaySpeed.Name = "com_PlaySpeed";
            this.com_PlaySpeed.Size = new System.Drawing.Size(91, 20);
            this.com_PlaySpeed.TabIndex = 46;
            this.com_PlaySpeed.SelectedIndexChanged += new System.EventHandler(this.com_PlaySpeed_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btn_SearchInMine);
            this.groupBox2.Controls.Add(this.linkLabel3);
            this.groupBox2.Controls.Add(this.linkLabel1);
            this.groupBox2.Controls.Add(this.linkLabel2);
            this.groupBox2.Controls.Add(this.com_EndMinute);
            this.groupBox2.Controls.Add(this.com_EndHour);
            this.groupBox2.Controls.Add(this.com_StartMinute);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.com_StartHour);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label_InMine);
            this.groupBox2.Location = new System.Drawing.Point(10, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(771, 107);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "人员历史轨迹查询";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(470, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 61;
            this.label5.Text = "历史情况报告：";
            // 
            // btn_SearchInMine
            // 
            this.btn_SearchInMine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SearchInMine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SearchInMine.Image = global::PersonPosition.Properties.Resources.ShowAny;
            this.btn_SearchInMine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_SearchInMine.Location = new System.Drawing.Point(632, 74);
            this.btn_SearchInMine.Name = "btn_SearchInMine";
            this.btn_SearchInMine.Size = new System.Drawing.Size(131, 26);
            this.btn_SearchInMine.TabIndex = 59;
            this.btn_SearchInMine.Text = "查询人员历史信息";
            this.btn_SearchInMine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_SearchInMine.UseVisualStyleBackColor = true;
            this.btn_SearchInMine.Click += new System.EventHandler(this.btn_SearchInMine_Click);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(133, 54);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(29, 12);
            this.linkLabel3.TabIndex = 43;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "今天";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(63, 54);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 12);
            this.linkLabel1.TabIndex = 41;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "前天";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(98, 54);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(29, 12);
            this.linkLabel2.TabIndex = 42;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "昨天";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // com_EndMinute
            // 
            this.com_EndMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_EndMinute.FormattingEnabled = true;
            this.com_EndMinute.Items.AddRange(new object[] {
            "00 分",
            "01 分",
            "02 分",
            "03 分",
            "04 分",
            "05 分",
            "06 分",
            "07 分",
            "08 分",
            "09 分",
            "10 分",
            "11 分",
            "12 分",
            "13 分",
            "14 分",
            "15 分",
            "16 分",
            "17 分",
            "18 分",
            "19 分",
            "20 分",
            "21 分",
            "22 分",
            "23 分",
            "24 分",
            "25 分",
            "26 分",
            "27 分",
            "28 分",
            "29 分",
            "30 分",
            "31 分",
            "32 分",
            "33 分",
            "34 分",
            "35 分",
            "36 分",
            "37 分",
            "38 分",
            "39 分",
            "40 分",
            "41 分",
            "42 分",
            "43 分",
            "44 分",
            "45 分",
            "46 分",
            "47 分",
            "48 分",
            "49 分",
            "50 分",
            "51 分",
            "52 分",
            "53 分",
            "54 分",
            "55 分",
            "56 分",
            "57 分",
            "58 分",
            "59 分"});
            this.com_EndMinute.Location = new System.Drawing.Point(371, 45);
            this.com_EndMinute.Name = "com_EndMinute";
            this.com_EndMinute.Size = new System.Drawing.Size(60, 20);
            this.com_EndMinute.TabIndex = 38;
            // 
            // com_EndHour
            // 
            this.com_EndHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_EndHour.FormattingEnabled = true;
            this.com_EndHour.Items.AddRange(new object[] {
            "00 时",
            "01 时",
            "02 时",
            "03 时",
            "04 时",
            "05 时",
            "06 时",
            "07 时",
            "08 时",
            "09 时",
            "10 时",
            "11 时",
            "12 时",
            "13 时",
            "14 时",
            "15 时",
            "16 时",
            "17 时",
            "18 时",
            "19 时",
            "20 时",
            "21 时",
            "22 时",
            "23 时"});
            this.com_EndHour.Location = new System.Drawing.Point(305, 45);
            this.com_EndHour.Name = "com_EndHour";
            this.com_EndHour.Size = new System.Drawing.Size(60, 20);
            this.com_EndHour.TabIndex = 37;
            // 
            // com_StartMinute
            // 
            this.com_StartMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_StartMinute.FormattingEnabled = true;
            this.com_StartMinute.Items.AddRange(new object[] {
            "00 分",
            "01 分",
            "02 分",
            "03 分",
            "04 分",
            "05 分",
            "06 分",
            "07 分",
            "08 分",
            "09 分",
            "10 分",
            "11 分",
            "12 分",
            "13 分",
            "14 分",
            "15 分",
            "16 分",
            "17 分",
            "18 分",
            "19 分",
            "20 分",
            "21 分",
            "22 分",
            "23 分",
            "24 分",
            "25 分",
            "26 分",
            "27 分",
            "28 分",
            "29 分",
            "30 分",
            "31 分",
            "32 分",
            "33 分",
            "34 分",
            "35 分",
            "36 分",
            "37 分",
            "38 分",
            "39 分",
            "40 分",
            "41 分",
            "42 分",
            "43 分",
            "44 分",
            "45 分",
            "46 分",
            "47 分",
            "48 分",
            "49 分",
            "50 分",
            "51 分",
            "52 分",
            "53 分",
            "54 分",
            "55 分",
            "56 分",
            "57 分",
            "58 分",
            "59 分"});
            this.com_StartMinute.Location = new System.Drawing.Point(371, 22);
            this.com_StartMinute.Name = "com_StartMinute";
            this.com_StartMinute.Size = new System.Drawing.Size(60, 20);
            this.com_StartMinute.TabIndex = 36;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(56, 23);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(164, 21);
            this.dateTimePicker1.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(236, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 34;
            this.label4.Text = "终止时间：";
            // 
            // com_StartHour
            // 
            this.com_StartHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_StartHour.FormattingEnabled = true;
            this.com_StartHour.Items.AddRange(new object[] {
            "00 时",
            "01 时",
            "02 时",
            "03 时",
            "04 时",
            "05 时",
            "06 时",
            "07 时",
            "08 时",
            "09 时",
            "10 时",
            "11 时",
            "12 时",
            "13 时",
            "14 时",
            "15 时",
            "16 时",
            "17 时",
            "18 时",
            "19 时",
            "20 时",
            "21 时",
            "22 时",
            "23 时"});
            this.com_StartHour.Location = new System.Drawing.Point(305, 22);
            this.com_StartHour.Name = "com_StartHour";
            this.com_StartHour.Size = new System.Drawing.Size(60, 20);
            this.com_StartHour.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(236, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "起始时间：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(14, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 27;
            this.label15.Text = "日期：";
            // 
            // label_InMine
            // 
            this.label_InMine.ForeColor = System.Drawing.Color.Blue;
            this.label_InMine.Location = new System.Drawing.Point(470, 36);
            this.label_InMine.Name = "label_InMine";
            this.label_InMine.Size = new System.Drawing.Size(295, 37);
            this.label_InMine.TabIndex = 62;
            this.label_InMine.Text = "历史情况报告...";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "历史移动轨迹说明.txt";
            this.saveFileDialog1.Filter = "文本文件|*.txt";
            this.saveFileDialog1.Title = "导出到文件";
            // 
            // FrmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 703);
            this.Controls.Add(this.MainPanel);
            this.Name = "FrmHistory";
            this.Text = "FrmHistory";
            this.MainPanel.ResumeLayout(false);
            this.group_HistoryMap.ResumeLayout(false);
            this.toolStripMap.ResumeLayout(false);
            this.toolStripMap.PerformLayout();
            this.panel_BackImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapImage)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_SearchHistory;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox com_StartHour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox com_StartMinute;
        private System.Windows.Forms.ComboBox com_EndMinute;
        private System.Windows.Forms.ComboBox com_EndHour;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox group_HistoryMap;
        private System.Windows.Forms.ToolStrip toolStripMap;
        private System.Windows.Forms.ToolStripButton btn_ZommIn;
        private System.Windows.Forms.ToolStripButton btn_ZommOut;
        private System.Windows.Forms.ToolStripButton btn_Move;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btn_Brows;
        private SharpMap.Forms.MapImage mapImage;
        private System.Windows.Forms.Button btn_Play;
        private System.Windows.Forms.Panel panel_BackImage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox com_PlaySpeed;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_SearchInMine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_InMine;
        private System.Windows.Forms.ListView listView_InMine;
        private System.Windows.Forms.ColumnHeader InMine_PID;
        private System.Windows.Forms.ColumnHeader InMine_CardID;
        private System.Windows.Forms.ColumnHeader InMine_Name;
        private System.Windows.Forms.ColumnHeader InMine_WorkType;
        private System.Windows.Forms.ColumnHeader InMine_Department;
        private System.Windows.Forms.TextBox text_DutyInfo;
        private System.Windows.Forms.TextBox text_History;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox_ShowMore;
        private System.Windows.Forms.ComboBox com_SelectDepartment;
    }
}