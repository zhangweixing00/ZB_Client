namespace PersonPosition.View
{
    partial class DialogStation
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.text_StationID = new System.Windows.Forms.TextBox();
            this.text_Name = new System.Windows.Forms.TextBox();
            this.com_Type = new System.Windows.Forms.ComboBox();
            this.com_Map = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.text_X = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.text_Y = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStripMap = new System.Windows.Forms.ToolStrip();
            this.btn_ZommIn = new System.Windows.Forms.ToolStripButton();
            this.btn_ZommOut = new System.Windows.Forms.ToolStripButton();
            this.btn_Move = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_Brows = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_Distance = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.text_Father = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.check_IsDutyStation = new System.Windows.Forms.CheckBox();
            this.label_Duty = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.text_IP = new System.Windows.Forms.TextBox();
            this.com_Duty = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.text_Port = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mapImage = new SharpMap.Forms.MapImage();
            this.listview_Son = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.com_StationFunction = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Canel = new System.Windows.Forms.Button();
            this.panel_CollectChannel = new System.Windows.Forms.Panel();
            this.listView_Channel = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.btn_EditChannel = new System.Windows.Forms.LinkLabel();
            this.btn_DelChannel = new System.Windows.Forms.LinkLabel();
            this.btn_AddChannel = new System.Windows.Forms.LinkLabel();
            this.com_MaxChannel = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel_ShowInfo = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.text_ShowInfo = new System.Windows.Forms.TextBox();
            this.com_ShowInfoIndex = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.com_ShowInfoStyle = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.text_RepairRSSI = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.com_Area = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel_Position = new System.Windows.Forms.Panel();
            this.toolStripMap.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapImage)).BeginInit();
            this.panel_CollectChannel.SuspendLayout();
            this.panel_ShowInfo.SuspendLayout();
            this.panel_Position.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "基站ID：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "基站所在区域：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "基站名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "所属地图：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "基站类型：";
            // 
            // text_StationID
            // 
            this.text_StationID.BackColor = System.Drawing.SystemColors.Window;
            this.text_StationID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_StationID.Location = new System.Drawing.Point(56, 50);
            this.text_StationID.MaxLength = 8;
            this.text_StationID.Name = "text_StationID";
            this.text_StationID.ShortcutsEnabled = false;
            this.text_StationID.Size = new System.Drawing.Size(106, 21);
            this.text_StationID.TabIndex = 1;
            this.text_StationID.TextChanged += new System.EventHandler(this.text_StationID_TextChanged);
            this.text_StationID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_StationID_KeyPress);
            // 
            // text_Name
            // 
            this.text_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_Name.Location = new System.Drawing.Point(3, 92);
            this.text_Name.Name = "text_Name";
            this.text_Name.Size = new System.Drawing.Size(159, 21);
            this.text_Name.TabIndex = 2;
            // 
            // com_Type
            // 
            this.com_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Type.FormattingEnabled = true;
            this.com_Type.Items.AddRange(new object[] {
            "Can基站",
            "网关基站",
            "无线基站"});
            this.com_Type.Location = new System.Drawing.Point(69, 246);
            this.com_Type.Name = "com_Type";
            this.com_Type.Size = new System.Drawing.Size(93, 20);
            this.com_Type.TabIndex = 4;
            this.com_Type.SelectedIndexChanged += new System.EventHandler(this.com_Type_SelectedIndexChanged);
            // 
            // com_Map
            // 
            this.com_Map.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Map.FormattingEnabled = true;
            this.com_Map.Location = new System.Drawing.Point(3, 26);
            this.com_Map.Name = "com_Map";
            this.com_Map.Size = new System.Drawing.Size(160, 20);
            this.com_Map.TabIndex = 0;
            this.com_Map.SelectedIndexChanged += new System.EventHandler(this.com_Map_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "X坐标：";
            // 
            // text_X
            // 
            this.text_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_X.Location = new System.Drawing.Point(52, 198);
            this.text_X.Name = "text_X";
            this.text_X.ReadOnly = true;
            this.text_X.Size = new System.Drawing.Size(109, 21);
            this.text_X.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "Y坐标：";
            // 
            // text_Y
            // 
            this.text_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_Y.Location = new System.Drawing.Point(52, 221);
            this.text_Y.Name = "text_Y";
            this.text_Y.ReadOnly = true;
            this.text_Y.Size = new System.Drawing.Size(109, 21);
            this.text_Y.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(8, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(349, 19);
            this.label11.TabIndex = 29;
            this.label11.Text = "提示：左键拖动地图，点击右键定位。";
            this.label11.Visible = false;
            // 
            // toolStripMap
            // 
            this.toolStripMap.AutoSize = false;
            this.toolStripMap.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMap.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_ZommIn,
            this.btn_ZommOut,
            this.btn_Move,
            this.toolStripSeparator1,
            this.btn_Brows,
            this.toolStripSeparator2,
            this.btn_Distance,
            this.toolStripLabel1});
            this.toolStripMap.Location = new System.Drawing.Point(166, 3);
            this.toolStripMap.Name = "toolStripMap";
            this.toolStripMap.Size = new System.Drawing.Size(691, 25);
            this.toolStripMap.TabIndex = 30;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_Distance
            // 
            this.btn_Distance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Distance.Image = global::PersonPosition.Properties.Resources.Rule;
            this.btn_Distance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Distance.Name = "btn_Distance";
            this.btn_Distance.Size = new System.Drawing.Size(23, 22);
            this.btn_Distance.Text = "toolStripButton1";
            this.btn_Distance.Click += new System.EventHandler(this.btn_Distance_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel1.Text = "总长度:0m ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 32;
            this.label10.Text = "基站坐标";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 278);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 37;
            this.label12.Text = "父基站ID：";
            // 
            // text_Father
            // 
            this.text_Father.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_Father.Location = new System.Drawing.Point(68, 274);
            this.text_Father.Name = "text_Father";
            this.text_Father.ShortcutsEnabled = false;
            this.text_Father.Size = new System.Drawing.Size(94, 21);
            this.text_Father.TabIndex = 36;
            this.text_Father.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_Father_KeyPress);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(59, 183);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(101, 12);
            this.linkLabel1.TabIndex = 38;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "点击这里开始定位";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // check_IsDutyStation
            // 
            this.check_IsDutyStation.AutoSize = true;
            this.check_IsDutyStation.Checked = true;
            this.check_IsDutyStation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_IsDutyStation.Location = new System.Drawing.Point(5, 3);
            this.check_IsDutyStation.Name = "check_IsDutyStation";
            this.check_IsDutyStation.Size = new System.Drawing.Size(108, 16);
            this.check_IsDutyStation.TabIndex = 7;
            this.check_IsDutyStation.Text = "是否是考勤基站";
            this.check_IsDutyStation.UseVisualStyleBackColor = true;
            this.check_IsDutyStation.CheckedChanged += new System.EventHandler(this.check_IsDutyStation_CheckedChanged);
            // 
            // label_Duty
            // 
            this.label_Duty.AutoSize = true;
            this.label_Duty.Location = new System.Drawing.Point(1, 24);
            this.label_Duty.Name = "label_Duty";
            this.label_Duty.Size = new System.Drawing.Size(65, 12);
            this.label_Duty.TabIndex = 44;
            this.label_Duty.Text = "考勤编号：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 350);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 46;
            this.label7.Text = "IP地址：";
            this.label7.Visible = false;
            // 
            // text_IP
            // 
            this.text_IP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_IP.Location = new System.Drawing.Point(58, 346);
            this.text_IP.Name = "text_IP";
            this.text_IP.ShortcutsEnabled = false;
            this.text_IP.Size = new System.Drawing.Size(104, 21);
            this.text_IP.TabIndex = 5;
            this.text_IP.Text = "192.168.1.";
            this.text_IP.Visible = false;
            this.text_IP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_IP_KeyPress);
            // 
            // com_Duty
            // 
            this.com_Duty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Duty.FormattingEnabled = true;
            this.com_Duty.Items.AddRange(new object[] {
            "1:洞口1号",
            "2:洞口2号",
            "3:洞内基站",
            "4:安全",
            "5:报警"});
            this.com_Duty.Location = new System.Drawing.Point(67, 19);
            this.com_Duty.Name = "com_Duty";
            this.com_Duty.Size = new System.Drawing.Size(93, 20);
            this.com_Duty.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 373);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 49;
            this.label13.Text = "端口号：";
            this.label13.Visible = false;
            // 
            // text_Port
            // 
            this.text_Port.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_Port.Location = new System.Drawing.Point(58, 369);
            this.text_Port.Name = "text_Port";
            this.text_Port.ShortcutsEnabled = false;
            this.text_Port.Size = new System.Drawing.Size(104, 21);
            this.text_Port.TabIndex = 6;
            this.text_Port.Text = "50000";
            this.text_Port.Visible = false;
            this.text_Port.TextChanged += new System.EventHandler(this.text_Port_TextChanged);
            this.text_Port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_Port_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.mapImage);
            this.panel1.Location = new System.Drawing.Point(168, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(686, 601);
            this.panel1.TabIndex = 50;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
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
            this.mapImage.Size = new System.Drawing.Size(686, 601);
            this.mapImage.TabIndex = 0;
            this.mapImage.TabStop = false;
            this.mapImage.WheelZoomMagnitude = 2;
            this.mapImage.MouseUp += new SharpMap.Forms.MapImage.MouseEventHandler(this.mapImage_MouseUp);
            this.mapImage.MouseMove += new SharpMap.Forms.MapImage.MouseEventHandler(this.mapImage_MouseMove);
            this.mapImage.MouseDown += new SharpMap.Forms.MapImage.MouseEventHandler(this.mapImage_MouseDown);
            // 
            // listview_Son
            // 
            this.listview_Son.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listview_Son.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listview_Son.FullRowSelect = true;
            this.listview_Son.GridLines = true;
            this.listview_Son.Location = new System.Drawing.Point(3, 268);
            this.listview_Son.MultiSelect = false;
            this.listview_Son.Name = "listview_Son";
            this.listview_Son.Size = new System.Drawing.Size(160, 76);
            this.listview_Son.TabIndex = 51;
            this.listview_Son.UseCompatibleStateImageBehavior = false;
            this.listview_Son.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "子基站ID";
            this.columnHeader1.Width = 66;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "基站名称";
            this.columnHeader2.Width = 86;
            // 
            // com_StationFunction
            // 
            this.com_StationFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_StationFunction.FormattingEnabled = true;
            this.com_StationFunction.Items.AddRange(new object[] {
            "人员定位",
            "信息采集",
            "考勤管理"});
            this.com_StationFunction.Location = new System.Drawing.Point(70, 394);
            this.com_StationFunction.Name = "com_StationFunction";
            this.com_StationFunction.Size = new System.Drawing.Size(93, 20);
            this.com_StationFunction.TabIndex = 52;
            this.com_StationFunction.SelectedIndexChanged += new System.EventHandler(this.com_StationFunction_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 398);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 53;
            this.label6.Text = "基站功能：";
            // 
            // btn_Save
            // 
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Save.Image = global::PersonPosition.Properties.Resources.Sure;
            this.btn_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Save.Location = new System.Drawing.Point(10, 601);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(68, 27);
            this.btn_Save.TabIndex = 9;
            this.btn_Save.Text = "保存";
            this.btn_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Canel
            // 
            this.btn_Canel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Canel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Canel.Image = global::PersonPosition.Properties.Resources.Abort;
            this.btn_Canel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Canel.Location = new System.Drawing.Point(87, 601);
            this.btn_Canel.Name = "btn_Canel";
            this.btn_Canel.Size = new System.Drawing.Size(68, 27);
            this.btn_Canel.TabIndex = 10;
            this.btn_Canel.Text = "取消";
            this.btn_Canel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Canel.UseVisualStyleBackColor = true;
            this.btn_Canel.Click += new System.EventHandler(this.btn_Canel_Click);
            // 
            // panel_CollectChannel
            // 
            this.panel_CollectChannel.Controls.Add(this.listView_Channel);
            this.panel_CollectChannel.Controls.Add(this.btn_EditChannel);
            this.panel_CollectChannel.Controls.Add(this.btn_DelChannel);
            this.panel_CollectChannel.Controls.Add(this.btn_AddChannel);
            this.panel_CollectChannel.Controls.Add(this.com_MaxChannel);
            this.panel_CollectChannel.Controls.Add(this.label14);
            this.panel_CollectChannel.Controls.Add(this.panel_ShowInfo);
            this.panel_CollectChannel.Location = new System.Drawing.Point(2, 418);
            this.panel_CollectChannel.Name = "panel_CollectChannel";
            this.panel_CollectChannel.Size = new System.Drawing.Size(164, 177);
            this.panel_CollectChannel.TabIndex = 54;
            // 
            // listView_Channel
            // 
            this.listView_Channel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_Channel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView_Channel.FullRowSelect = true;
            this.listView_Channel.GridLines = true;
            this.listView_Channel.Location = new System.Drawing.Point(2, 25);
            this.listView_Channel.MultiSelect = false;
            this.listView_Channel.Name = "listView_Channel";
            this.listView_Channel.Size = new System.Drawing.Size(160, 131);
            this.listView_Channel.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_Channel.TabIndex = 61;
            this.listView_Channel.UseCompatibleStateImageBehavior = false;
            this.listView_Channel.View = System.Windows.Forms.View.Details;
            this.listView_Channel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Channel_MouseDoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "通道号";
            this.columnHeader3.Width = 52;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "名称";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "类型";
            this.columnHeader5.Width = 72;
            // 
            // btn_EditChannel
            // 
            this.btn_EditChannel.AutoSize = true;
            this.btn_EditChannel.Location = new System.Drawing.Point(1, 161);
            this.btn_EditChannel.Name = "btn_EditChannel";
            this.btn_EditChannel.Size = new System.Drawing.Size(53, 12);
            this.btn_EditChannel.TabIndex = 59;
            this.btn_EditChannel.TabStop = true;
            this.btn_EditChannel.Text = "修改通道";
            this.btn_EditChannel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btn_EditChannel_LinkClicked);
            // 
            // btn_DelChannel
            // 
            this.btn_DelChannel.AutoSize = true;
            this.btn_DelChannel.Location = new System.Drawing.Point(109, 161);
            this.btn_DelChannel.Name = "btn_DelChannel";
            this.btn_DelChannel.Size = new System.Drawing.Size(53, 12);
            this.btn_DelChannel.TabIndex = 58;
            this.btn_DelChannel.TabStop = true;
            this.btn_DelChannel.Text = "删除通道";
            this.btn_DelChannel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btn_DelChannel_LinkClicked);
            // 
            // btn_AddChannel
            // 
            this.btn_AddChannel.AutoSize = true;
            this.btn_AddChannel.Location = new System.Drawing.Point(55, 161);
            this.btn_AddChannel.Name = "btn_AddChannel";
            this.btn_AddChannel.Size = new System.Drawing.Size(53, 12);
            this.btn_AddChannel.TabIndex = 57;
            this.btn_AddChannel.TabStop = true;
            this.btn_AddChannel.Text = "添加通道";
            this.btn_AddChannel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btn_AddChannel_LinkClicked);
            // 
            // com_MaxChannel
            // 
            this.com_MaxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_MaxChannel.FormattingEnabled = true;
            this.com_MaxChannel.Items.AddRange(new object[] {
            "32",
            "64",
            "128"});
            this.com_MaxChannel.Location = new System.Drawing.Point(92, 2);
            this.com_MaxChannel.Name = "com_MaxChannel";
            this.com_MaxChannel.Size = new System.Drawing.Size(69, 20);
            this.com_MaxChannel.TabIndex = 54;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 12);
            this.label14.TabIndex = 55;
            this.label14.Text = "最大通道个数：";
            // 
            // panel_ShowInfo
            // 
            this.panel_ShowInfo.Controls.Add(this.label19);
            this.panel_ShowInfo.Controls.Add(this.text_ShowInfo);
            this.panel_ShowInfo.Controls.Add(this.com_ShowInfoIndex);
            this.panel_ShowInfo.Controls.Add(this.label18);
            this.panel_ShowInfo.Controls.Add(this.com_ShowInfoStyle);
            this.panel_ShowInfo.Controls.Add(this.label17);
            this.panel_ShowInfo.Location = new System.Drawing.Point(1, 84);
            this.panel_ShowInfo.Name = "panel_ShowInfo";
            this.panel_ShowInfo.Size = new System.Drawing.Size(162, 93);
            this.panel_ShowInfo.TabIndex = 61;
            this.panel_ShowInfo.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1, 53);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 64;
            this.label19.Text = "显示内容：";
            // 
            // text_ShowInfo
            // 
            this.text_ShowInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_ShowInfo.Location = new System.Drawing.Point(1, 68);
            this.text_ShowInfo.Name = "text_ShowInfo";
            this.text_ShowInfo.Size = new System.Drawing.Size(159, 21);
            this.text_ShowInfo.TabIndex = 63;
            // 
            // com_ShowInfoIndex
            // 
            this.com_ShowInfoIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_ShowInfoIndex.FormattingEnabled = true;
            this.com_ShowInfoIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.com_ShowInfoIndex.Location = new System.Drawing.Point(66, 26);
            this.com_ShowInfoIndex.Name = "com_ShowInfoIndex";
            this.com_ShowInfoIndex.Size = new System.Drawing.Size(93, 20);
            this.com_ShowInfoIndex.TabIndex = 61;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 62;
            this.label18.Text = "显示序号：";
            // 
            // com_ShowInfoStyle
            // 
            this.com_ShowInfoStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_ShowInfoStyle.FormattingEnabled = true;
            this.com_ShowInfoStyle.Items.AddRange(new object[] {
            "0 取消显示",
            "1 单独显示",
            "2 插入显示序列",
            "3 显示基站所有设置"});
            this.com_ShowInfoStyle.Location = new System.Drawing.Point(66, 2);
            this.com_ShowInfoStyle.Name = "com_ShowInfoStyle";
            this.com_ShowInfoStyle.Size = new System.Drawing.Size(93, 20);
            this.com_ShowInfoStyle.TabIndex = 59;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1, 7);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 60;
            this.label17.Text = "信息显示：";
            // 
            // text_RepairRSSI
            // 
            this.text_RepairRSSI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_RepairRSSI.Location = new System.Drawing.Point(70, 158);
            this.text_RepairRSSI.Name = "text_RepairRSSI";
            this.text_RepairRSSI.Size = new System.Drawing.Size(44, 21);
            this.text_RepairRSSI.TabIndex = 56;
            this.text_RepairRSSI.Text = "0";
            this.text_RepairRSSI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_RepairRSSI_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 162);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 55;
            this.label15.Text = "信号修正：";
            // 
            // com_Area
            // 
            this.com_Area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Area.FormattingEnabled = true;
            this.com_Area.Location = new System.Drawing.Point(3, 134);
            this.com_Area.Name = "com_Area";
            this.com_Area.Size = new System.Drawing.Size(159, 20);
            this.com_Area.TabIndex = 57;
            // 
            // label16
            // 
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(5, 298);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(158, 30);
            this.label16.TabIndex = 58;
            this.label16.Text = "注：若为【信息采集】基站\r\n则无需设置其父基站";
            // 
            // panel_Position
            // 
            this.panel_Position.Controls.Add(this.check_IsDutyStation);
            this.panel_Position.Controls.Add(this.label_Duty);
            this.panel_Position.Controls.Add(this.com_Duty);
            this.panel_Position.Location = new System.Drawing.Point(2, 417);
            this.panel_Position.Name = "panel_Position";
            this.panel_Position.Size = new System.Drawing.Size(161, 43);
            this.panel_Position.TabIndex = 62;
            // 
            // DialogStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 632);
            this.Controls.Add(this.panel_Position);
            this.Controls.Add(this.com_Area);
            this.Controls.Add(this.text_RepairRSSI);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel_CollectChannel);
            this.Controls.Add(this.com_StationFunction);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listview_Son);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.text_Port);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.text_IP);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.text_Father);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.toolStripMap);
            this.Controls.Add(this.btn_Canel);
            this.Controls.Add(this.text_Y);
            this.Controls.Add(this.text_X);
            this.Controls.Add(this.com_Map);
            this.Controls.Add(this.com_Type);
            this.Controls.Add(this.text_Name);
            this.Controls.Add(this.text_StationID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label16);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogStation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置基站";
            this.Load += new System.EventHandler(this.DialogStation_Load);
            this.toolStripMap.ResumeLayout(false);
            this.toolStripMap.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapImage)).EndInit();
            this.panel_CollectChannel.ResumeLayout(false);
            this.panel_CollectChannel.PerformLayout();
            this.panel_ShowInfo.ResumeLayout(false);
            this.panel_ShowInfo.PerformLayout();
            this.panel_Position.ResumeLayout(false);
            this.panel_Position.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpMap.Forms.MapImage mapImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox text_StationID;
        private System.Windows.Forms.TextBox text_Name;
        private System.Windows.Forms.ComboBox com_Type;
        private System.Windows.Forms.ComboBox com_Map;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox text_X;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox text_Y;
        private System.Windows.Forms.Button btn_Canel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStrip toolStripMap;
        private System.Windows.Forms.ToolStripButton btn_ZommIn;
        private System.Windows.Forms.ToolStripButton btn_ZommOut;
        private System.Windows.Forms.ToolStripButton btn_Move;
        private System.Windows.Forms.ToolStripButton btn_Brows;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox text_Father;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox check_IsDutyStation;
        private System.Windows.Forms.Label label_Duty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox text_IP;
        private System.Windows.Forms.ComboBox com_Duty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox text_Port;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btn_Distance;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listview_Son;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ComboBox com_StationFunction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel_CollectChannel;
        private System.Windows.Forms.ComboBox com_MaxChannel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.LinkLabel btn_EditChannel;
        private System.Windows.Forms.LinkLabel btn_DelChannel;
        private System.Windows.Forms.LinkLabel btn_AddChannel;
        private System.Windows.Forms.ListView listView_Channel;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TextBox text_RepairRSSI;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox com_Area;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox com_ShowInfoStyle;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel_ShowInfo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox text_ShowInfo;
        private System.Windows.Forms.ComboBox com_ShowInfoIndex;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel_Position;
    }
}