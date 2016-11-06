namespace PersonPosition.View
{
    partial class FrmCollect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_CollectName = new System.Windows.Forms.Label();
            this.label_CollectID = new System.Windows.Forms.Label();
            this.label_CollectChannelInfo = new System.Windows.Forms.Label();
            this.listView_CollectList = new System.Windows.Forms.ListView();
            this.col_StationID = new System.Windows.Forms.ColumnHeader();
            this.col_StationName = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Info = new System.Windows.Forms.Button();
            this.btn_GoToMap = new System.Windows.Forms.Button();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.DataGrid_CollectChannel = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DutyReportView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.btn_Print = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.com_CollectStation = new System.Windows.Forms.ComboBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.com_CollectStation_1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Analysics = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.AnalysicsReportView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.MainPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_CollectChannel)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.MainPanel.Controls.Add(this.tabControl1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1077, 703);
            this.MainPanel.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.ItemSize = new System.Drawing.Size(82, 30);
            this.tabControl1.Location = new System.Drawing.Point(3, 49);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(6, 6);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1072, 652);
            this.tabControl1.TabIndex = 58;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox20);
            this.tabPage1.ImageKey = "Duty_3.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1064, 614);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "  实时采集信息   ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label_CollectName);
            this.groupBox1.Controls.Add(this.label_CollectID);
            this.groupBox1.Controls.Add(this.label_CollectChannelInfo);
            this.groupBox1.Controls.Add(this.listView_CollectList);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_Info);
            this.groupBox1.Controls.Add(this.btn_GoToMap);
            this.groupBox1.Location = new System.Drawing.Point(6, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1052, 133);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "采集器基站列表";
            // 
            // label_CollectName
            // 
            this.label_CollectName.AutoSize = true;
            this.label_CollectName.Location = new System.Drawing.Point(399, 44);
            this.label_CollectName.Name = "label_CollectName";
            this.label_CollectName.Size = new System.Drawing.Size(11, 12);
            this.label_CollectName.TabIndex = 74;
            this.label_CollectName.Text = "-";
            // 
            // label_CollectID
            // 
            this.label_CollectID.AutoSize = true;
            this.label_CollectID.Location = new System.Drawing.Point(399, 24);
            this.label_CollectID.Name = "label_CollectID";
            this.label_CollectID.Size = new System.Drawing.Size(89, 12);
            this.label_CollectID.TabIndex = 73;
            this.label_CollectID.Text = "全部采集器基站";
            // 
            // label_CollectChannelInfo
            // 
            this.label_CollectChannelInfo.AutoSize = true;
            this.label_CollectChannelInfo.Location = new System.Drawing.Point(362, 67);
            this.label_CollectChannelInfo.Name = "label_CollectChannelInfo";
            this.label_CollectChannelInfo.Size = new System.Drawing.Size(11, 12);
            this.label_CollectChannelInfo.TabIndex = 72;
            this.label_CollectChannelInfo.Text = "-";
            // 
            // listView_CollectList
            // 
            this.listView_CollectList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.listView_CollectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_StationID,
            this.col_StationName});
            this.listView_CollectList.FullRowSelect = true;
            this.listView_CollectList.GridLines = true;
            this.listView_CollectList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_CollectList.Location = new System.Drawing.Point(13, 20);
            this.listView_CollectList.MultiSelect = false;
            this.listView_CollectList.Name = "listView_CollectList";
            this.listView_CollectList.Size = new System.Drawing.Size(270, 103);
            this.listView_CollectList.TabIndex = 71;
            this.listView_CollectList.UseCompatibleStateImageBehavior = false;
            this.listView_CollectList.View = System.Windows.Forms.View.Details;
            this.listView_CollectList.SelectedIndexChanged += new System.EventHandler(this.listView_CollectList_SelectedIndexChanged);
            // 
            // col_StationID
            // 
            this.col_StationID.Text = "采集器基站编号";
            this.col_StationID.Width = 102;
            // 
            // col_StationName
            // 
            this.col_StationName.Text = "采集器基站名称";
            this.col_StationName.Width = 146;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 70;
            this.label3.Text = "通道信息：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 69;
            this.label2.Text = "采集器基站名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 68;
            this.label1.Text = "采集器基站编号：";
            // 
            // btn_Info
            // 
            this.btn_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Info.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Info.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Info.Image = global::PersonPosition.Properties.Resources.Text;
            this.btn_Info.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Info.Location = new System.Drawing.Point(932, 53);
            this.btn_Info.Name = "btn_Info";
            this.btn_Info.Size = new System.Drawing.Size(109, 26);
            this.btn_Info.TabIndex = 67;
            this.btn_Info.Text = "基站详细信息";
            this.btn_Info.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Info.UseVisualStyleBackColor = true;
            this.btn_Info.Click += new System.EventHandler(this.btn_Info_Click);
            // 
            // btn_GoToMap
            // 
            this.btn_GoToMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GoToMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_GoToMap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_GoToMap.Image = global::PersonPosition.Properties.Resources.Map;
            this.btn_GoToMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_GoToMap.Location = new System.Drawing.Point(932, 20);
            this.btn_GoToMap.Name = "btn_GoToMap";
            this.btn_GoToMap.Size = new System.Drawing.Size(109, 26);
            this.btn_GoToMap.TabIndex = 66;
            this.btn_GoToMap.Text = "去地图中查看";
            this.btn_GoToMap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_GoToMap.UseVisualStyleBackColor = true;
            this.btn_GoToMap.Click += new System.EventHandler(this.btn_GoToMap_Click);
            // 
            // groupBox20
            // 
            this.groupBox20.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox20.Controls.Add(this.DataGrid_CollectChannel);
            this.groupBox20.Location = new System.Drawing.Point(6, 156);
            this.groupBox20.Margin = new System.Windows.Forms.Padding(12);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(1052, 457);
            this.groupBox20.TabIndex = 56;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "采集器通道信息表";
            // 
            // DataGrid_CollectChannel
            // 
            this.DataGrid_CollectChannel.AllowUserToAddRows = false;
            this.DataGrid_CollectChannel.AllowUserToOrderColumns = true;
            this.DataGrid_CollectChannel.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.DataGrid_CollectChannel.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGrid_CollectChannel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DataGrid_CollectChannel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid_CollectChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid_CollectChannel.Location = new System.Drawing.Point(3, 17);
            this.DataGrid_CollectChannel.MultiSelect = false;
            this.DataGrid_CollectChannel.Name = "DataGrid_CollectChannel";
            this.DataGrid_CollectChannel.ReadOnly = true;
            this.DataGrid_CollectChannel.RowHeadersVisible = false;
            this.DataGrid_CollectChannel.RowTemplate.Height = 23;
            this.DataGrid_CollectChannel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid_CollectChannel.ShowCellErrors = false;
            this.DataGrid_CollectChannel.ShowRowErrors = false;
            this.DataGrid_CollectChannel.Size = new System.Drawing.Size(1046, 437);
            this.DataGrid_CollectChannel.TabIndex = 0;
            this.DataGrid_CollectChannel.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGrid_CollectChannel_DataError);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.ImageKey = "Duty_2.png";
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(997, 614);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "   历史采集信息   ";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.DutyReportView);
            this.groupBox3.Location = new System.Drawing.Point(6, 99);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(985, 509);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "统计报表";
            // 
            // DutyReportView
            // 
            this.DutyReportView.ActiveViewIndex = -1;
            this.DutyReportView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DutyReportView.DisplayGroupTree = false;
            this.DutyReportView.DisplayStatusBar = false;
            this.DutyReportView.DisplayToolbar = false;
            this.DutyReportView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DutyReportView.Location = new System.Drawing.Point(3, 17);
            this.DutyReportView.Name = "DutyReportView";
            this.DutyReportView.SelectionFormula = "";
            this.DutyReportView.ShowGroupTreeButton = false;
            this.DutyReportView.ShowRefreshButton = false;
            this.DutyReportView.Size = new System.Drawing.Size(979, 489);
            this.DutyReportView.TabIndex = 1;
            this.DutyReportView.ViewTimeSelectionFormula = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.linkLabel3);
            this.groupBox5.Controls.Add(this.linkLabel1);
            this.groupBox5.Controls.Add(this.linkLabel2);
            this.groupBox5.Controls.Add(this.dateTimePicker1);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.btn_Print);
            this.groupBox5.Controls.Add(this.btn_Export);
            this.groupBox5.Controls.Add(this.com_CollectStation);
            this.groupBox5.Controls.Add(this.btn_Search);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(6, 16);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(984, 70);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "统计条件";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(569, 32);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(29, 12);
            this.linkLabel3.TabIndex = 81;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "今天";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(499, 32);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 12);
            this.linkLabel1.TabIndex = 79;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "前天";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(534, 32);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(29, 12);
            this.linkLabel2.TabIndex = 80;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "昨天";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(321, 28);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(164, 21);
            this.dateTimePicker1.TabIndex = 77;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(279, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 78;
            this.label15.Text = "日期：";
            // 
            // btn_Print
            // 
            this.btn_Print.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Print.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Print.Image = global::PersonPosition.Properties.Resources.Print;
            this.btn_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Print.Location = new System.Drawing.Point(805, 25);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(79, 26);
            this.btn_Print.TabIndex = 76;
            this.btn_Print.Text = "打印  ";
            this.btn_Print.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Export.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Export.Image = global::PersonPosition.Properties.Resources.Excel;
            this.btn_Export.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Export.Location = new System.Drawing.Point(890, 25);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(79, 26);
            this.btn_Export.TabIndex = 75;
            this.btn_Export.Text = "导出  ";
            this.btn_Export.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // com_CollectStation
            // 
            this.com_CollectStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_CollectStation.FormattingEnabled = true;
            this.com_CollectStation.Location = new System.Drawing.Point(94, 29);
            this.com_CollectStation.Name = "com_CollectStation";
            this.com_CollectStation.Size = new System.Drawing.Size(135, 20);
            this.com_CollectStation.TabIndex = 74;
            // 
            // btn_Search
            // 
            this.btn_Search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Search.Image = global::PersonPosition.Properties.Resources.ShowAny;
            this.btn_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Search.Location = new System.Drawing.Point(720, 25);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(79, 26);
            this.btn_Search.TabIndex = 63;
            this.btn_Search.Text = "统计  ";
            this.btn_Search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(40, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 64;
            this.label10.Text = "采集器：";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.ImageKey = "Duty_5.png";
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(997, 614);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "   采集信息分析   ";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.linkLabel4);
            this.groupBox6.Controls.Add(this.linkLabel5);
            this.groupBox6.Controls.Add(this.linkLabel6);
            this.groupBox6.Controls.Add(this.dateTimePicker2);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.com_CollectStation_1);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.btn_Analysics);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.button3);
            this.groupBox6.Location = new System.Drawing.Point(6, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(984, 70);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "分析条件";
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(569, 32);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(29, 12);
            this.linkLabel4.TabIndex = 100;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "今天";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(499, 32);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(29, 12);
            this.linkLabel5.TabIndex = 98;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "前天";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Location = new System.Drawing.Point(534, 32);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(29, 12);
            this.linkLabel6.TabIndex = 99;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "昨天";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(321, 28);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(164, 21);
            this.dateTimePicker2.TabIndex = 96;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(279, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 97;
            this.label5.Text = "日期：";
            // 
            // com_CollectStation_1
            // 
            this.com_CollectStation_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_CollectStation_1.FormattingEnabled = true;
            this.com_CollectStation_1.Location = new System.Drawing.Point(94, 29);
            this.com_CollectStation_1.Name = "com_CollectStation_1";
            this.com_CollectStation_1.Size = new System.Drawing.Size(135, 20);
            this.com_CollectStation_1.TabIndex = 95;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(40, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 94;
            this.label4.Text = "采集器：";
            // 
            // btn_Analysics
            // 
            this.btn_Analysics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Analysics.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Analysics.Image = global::PersonPosition.Properties.Resources.ShowAny;
            this.btn_Analysics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Analysics.Location = new System.Drawing.Point(720, 25);
            this.btn_Analysics.Name = "btn_Analysics";
            this.btn_Analysics.Size = new System.Drawing.Size(79, 26);
            this.btn_Analysics.TabIndex = 63;
            this.btn_Analysics.Text = "统计  ";
            this.btn_Analysics.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Analysics.UseVisualStyleBackColor = true;
            this.btn_Analysics.Click += new System.EventHandler(this.btn_Analysics_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Image = global::PersonPosition.Properties.Resources.Print;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(805, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 26);
            this.button2.TabIndex = 76;
            this.button2.Text = "打印  ";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Image = global::PersonPosition.Properties.Resources.Excel;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(890, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 26);
            this.button3.TabIndex = 75;
            this.button3.Text = "导出  ";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.AnalysicsReportView);
            this.groupBox7.Location = new System.Drawing.Point(6, 99);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(985, 509);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "分析报表";
            // 
            // AnalysicsReportView
            // 
            this.AnalysicsReportView.ActiveViewIndex = -1;
            this.AnalysicsReportView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AnalysicsReportView.DisplayGroupTree = false;
            this.AnalysicsReportView.DisplayStatusBar = false;
            this.AnalysicsReportView.DisplayToolbar = false;
            this.AnalysicsReportView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnalysicsReportView.Location = new System.Drawing.Point(3, 17);
            this.AnalysicsReportView.Name = "AnalysicsReportView";
            this.AnalysicsReportView.SelectionFormula = "";
            this.AnalysicsReportView.ShowGroupTreeButton = false;
            this.AnalysicsReportView.ShowRefreshButton = false;
            this.AnalysicsReportView.Size = new System.Drawing.Size(979, 489);
            this.AnalysicsReportView.TabIndex = 0;
            this.AnalysicsReportView.ViewTimeSelectionFormula = "";
            // 
            // FrmCollect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 703);
            this.Controls.Add(this.MainPanel);
            this.Name = "FrmCollect";
            this.Text = "FrmCollect";
            this.MainPanel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_CollectChannel)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.DataGridView DataGrid_CollectChannel;
        private System.Windows.Forms.Button btn_GoToMap;
        private System.Windows.Forms.Label label_CollectChannelInfo;
        private System.Windows.Forms.ListView listView_CollectList;
        private System.Windows.Forms.ColumnHeader col_StationID;
        private System.Windows.Forms.ColumnHeader col_StationName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Info;
        private System.Windows.Forms.Label label_CollectName;
        private System.Windows.Forms.Label label_CollectID;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.ComboBox com_CollectStation;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btn_Analysics;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox7;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer AnalysicsReportView;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer DutyReportView;
        private System.Windows.Forms.ComboBox com_CollectStation_1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label5;
    }
}