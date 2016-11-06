namespace PersonPosition.View
{
    partial class FrmLED_Setting
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
            this.btn_Save = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.com_LoopHuman = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.check_TopMost = new System.Windows.Forms.CheckBox();
            this.check_Adv = new System.Windows.Forms.CheckBox();
            this.group_Adv = new System.Windows.Forms.GroupBox();
            this.btn_AdvTextColor = new System.Windows.Forms.Button();
            this.btn_AdvFont = new System.Windows.Forms.Button();
            this.btn_AdvLineColor = new System.Windows.Forms.Button();
            this.text_AdvText = new System.Windows.Forms.TextBox();
            this.panel_ShuChange = new System.Windows.Forms.Panel();
            this.com_ShuChangeIndex = new System.Windows.Forms.ComboBox();
            this.btn_LeftLine = new System.Windows.Forms.Button();
            this.label_ShuChangeValue = new System.Windows.Forms.Label();
            this.btn_RightLine = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.com_ShuNum = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.com_HengNum = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label_AdvTextSeek = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.listView_AllCollect = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.btn_RefreshCollect = new System.Windows.Forms.Button();
            this.btn_InsertCollect = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel_HengChange = new System.Windows.Forms.Panel();
            this.com_HengChangeIndex = new System.Windows.Forms.ComboBox();
            this.btn_UpLine = new System.Windows.Forms.Button();
            this.label_HengChangeValue = new System.Windows.Forms.Label();
            this.btn_DownLine = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Preview = new System.Windows.Forms.Button();
            this.btn_Abort = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.text_BasicTitle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_BasicFont = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btn_BasicTextColor = new System.Windows.Forms.Button();
            this.radio_InMineNumTotal = new System.Windows.Forms.RadioButton();
            this.radio_InMineNumArea = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.group_Adv.SuspendLayout();
            this.panel_ShuChange.SuspendLayout();
            this.panel_HengChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Location = new System.Drawing.Point(270, 581);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 26);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "文字滚动时间：";
            // 
            // com_LoopHuman
            // 
            this.com_LoopHuman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_LoopHuman.FormattingEnabled = true;
            this.com_LoopHuman.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "8",
            "10",
            "15",
            "20"});
            this.com_LoopHuman.Location = new System.Drawing.Point(109, 72);
            this.com_LoopHuman.Name = "com_LoopHuman";
            this.com_LoopHuman.Size = new System.Drawing.Size(42, 20);
            this.com_LoopHuman.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "秒";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(284, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "屏幕长(像素)：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(372, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(37, 21);
            this.textBox1.TabIndex = 7;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(372, 47);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(37, 21);
            this.textBox2.TabIndex = 9;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(284, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "屏幕宽(像素)：";
            // 
            // check_TopMost
            // 
            this.check_TopMost.AutoSize = true;
            this.check_TopMost.Location = new System.Drawing.Point(23, 99);
            this.check_TopMost.Name = "check_TopMost";
            this.check_TopMost.Size = new System.Drawing.Size(114, 16);
            this.check_TopMost.TabIndex = 10;
            this.check_TopMost.Text = "LED窗口始终置顶";
            this.check_TopMost.UseVisualStyleBackColor = true;
            // 
            // check_Adv
            // 
            this.check_Adv.AutoSize = true;
            this.check_Adv.Checked = true;
            this.check_Adv.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Adv.ForeColor = System.Drawing.Color.SteelBlue;
            this.check_Adv.Location = new System.Drawing.Point(7, 131);
            this.check_Adv.Name = "check_Adv";
            this.check_Adv.Size = new System.Drawing.Size(294, 16);
            this.check_Adv.TabIndex = 11;
            this.check_Adv.Text = "启用高级显示模式(在LED右侧显示更多定制化信息)";
            this.check_Adv.UseVisualStyleBackColor = true;
            this.check_Adv.CheckedChanged += new System.EventHandler(this.check_Adv_CheckedChanged);
            // 
            // group_Adv
            // 
            this.group_Adv.Controls.Add(this.btn_AdvTextColor);
            this.group_Adv.Controls.Add(this.btn_AdvFont);
            this.group_Adv.Controls.Add(this.btn_AdvLineColor);
            this.group_Adv.Controls.Add(this.text_AdvText);
            this.group_Adv.Controls.Add(this.panel_ShuChange);
            this.group_Adv.Controls.Add(this.com_ShuNum);
            this.group_Adv.Controls.Add(this.label12);
            this.group_Adv.Controls.Add(this.com_HengNum);
            this.group_Adv.Controls.Add(this.label5);
            this.group_Adv.Controls.Add(this.label_AdvTextSeek);
            this.group_Adv.Controls.Add(this.label11);
            this.group_Adv.Controls.Add(this.listView_AllCollect);
            this.group_Adv.Controls.Add(this.btn_RefreshCollect);
            this.group_Adv.Controls.Add(this.btn_InsertCollect);
            this.group_Adv.Controls.Add(this.label10);
            this.group_Adv.Controls.Add(this.label9);
            this.group_Adv.Controls.Add(this.panel_HengChange);
            this.group_Adv.Location = new System.Drawing.Point(7, 142);
            this.group_Adv.Name = "group_Adv";
            this.group_Adv.Size = new System.Drawing.Size(419, 434);
            this.group_Adv.TabIndex = 12;
            this.group_Adv.TabStop = false;
            // 
            // btn_AdvTextColor
            // 
            this.btn_AdvTextColor.Location = new System.Drawing.Point(314, 42);
            this.btn_AdvTextColor.Name = "btn_AdvTextColor";
            this.btn_AdvTextColor.Size = new System.Drawing.Size(97, 24);
            this.btn_AdvTextColor.TabIndex = 65;
            this.btn_AdvTextColor.Text = "更改字体颜色";
            this.btn_AdvTextColor.UseVisualStyleBackColor = true;
            this.btn_AdvTextColor.Click += new System.EventHandler(this.btn_AdvTextColor_Click);
            // 
            // btn_AdvFont
            // 
            this.btn_AdvFont.Location = new System.Drawing.Point(314, 70);
            this.btn_AdvFont.Name = "btn_AdvFont";
            this.btn_AdvFont.Size = new System.Drawing.Size(75, 24);
            this.btn_AdvFont.TabIndex = 64;
            this.btn_AdvFont.Text = "更改字体";
            this.btn_AdvFont.UseVisualStyleBackColor = true;
            this.btn_AdvFont.Click += new System.EventHandler(this.btn_AdvFont_Click);
            // 
            // btn_AdvLineColor
            // 
            this.btn_AdvLineColor.Location = new System.Drawing.Point(314, 14);
            this.btn_AdvLineColor.Name = "btn_AdvLineColor";
            this.btn_AdvLineColor.Size = new System.Drawing.Size(97, 24);
            this.btn_AdvLineColor.TabIndex = 63;
            this.btn_AdvLineColor.Text = "更改表格颜色";
            this.btn_AdvLineColor.UseVisualStyleBackColor = true;
            this.btn_AdvLineColor.Click += new System.EventHandler(this.btn_AdvLineColor_Click);
            // 
            // text_AdvText
            // 
            this.text_AdvText.BackColor = System.Drawing.Color.Black;
            this.text_AdvText.ForeColor = System.Drawing.Color.Red;
            this.text_AdvText.Location = new System.Drawing.Point(7, 110);
            this.text_AdvText.Multiline = true;
            this.text_AdvText.Name = "text_AdvText";
            this.text_AdvText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_AdvText.Size = new System.Drawing.Size(403, 176);
            this.text_AdvText.TabIndex = 58;
            this.text_AdvText.WordWrap = false;
            this.text_AdvText.TextChanged += new System.EventHandler(this.text_AdvText_TextChanged);
            this.text_AdvText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.text_AdvText_KeyUp);
            this.text_AdvText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.text_AdvText_MouseDown);
            this.text_AdvText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_AdvText_KeyPress);
            this.text_AdvText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.text_AdvText_MouseUp);
            // 
            // panel_ShuChange
            // 
            this.panel_ShuChange.Controls.Add(this.com_ShuChangeIndex);
            this.panel_ShuChange.Controls.Add(this.btn_LeftLine);
            this.panel_ShuChange.Controls.Add(this.label_ShuChangeValue);
            this.panel_ShuChange.Controls.Add(this.btn_RightLine);
            this.panel_ShuChange.Controls.Add(this.label13);
            this.panel_ShuChange.Location = new System.Drawing.Point(9, 60);
            this.panel_ShuChange.Name = "panel_ShuChange";
            this.panel_ShuChange.Size = new System.Drawing.Size(287, 28);
            this.panel_ShuChange.TabIndex = 56;
            this.panel_ShuChange.Visible = false;
            // 
            // com_ShuChangeIndex
            // 
            this.com_ShuChangeIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_ShuChangeIndex.FormattingEnabled = true;
            this.com_ShuChangeIndex.Location = new System.Drawing.Point(45, 5);
            this.com_ShuChangeIndex.Name = "com_ShuChangeIndex";
            this.com_ShuChangeIndex.Size = new System.Drawing.Size(36, 20);
            this.com_ShuChangeIndex.TabIndex = 57;
            this.com_ShuChangeIndex.SelectedIndexChanged += new System.EventHandler(this.com_ShuChangeIndex_SelectedIndexChanged);
            // 
            // btn_LeftLine
            // 
            this.btn_LeftLine.Location = new System.Drawing.Point(158, 3);
            this.btn_LeftLine.Name = "btn_LeftLine";
            this.btn_LeftLine.Size = new System.Drawing.Size(45, 23);
            this.btn_LeftLine.TabIndex = 25;
            this.btn_LeftLine.Text = "左移";
            this.btn_LeftLine.UseVisualStyleBackColor = true;
            this.btn_LeftLine.Click += new System.EventHandler(this.btn_LeftLine_Click);
            // 
            // label_ShuChangeValue
            // 
            this.label_ShuChangeValue.AutoSize = true;
            this.label_ShuChangeValue.Location = new System.Drawing.Point(264, 9);
            this.label_ShuChangeValue.Name = "label_ShuChangeValue";
            this.label_ShuChangeValue.Size = new System.Drawing.Size(11, 12);
            this.label_ShuChangeValue.TabIndex = 31;
            this.label_ShuChangeValue.Text = "0";
            // 
            // btn_RightLine
            // 
            this.btn_RightLine.Location = new System.Drawing.Point(207, 3);
            this.btn_RightLine.Name = "btn_RightLine";
            this.btn_RightLine.Size = new System.Drawing.Size(45, 23);
            this.btn_RightLine.TabIndex = 26;
            this.btn_RightLine.Text = "右移";
            this.btn_RightLine.UseVisualStyleBackColor = true;
            this.btn_RightLine.Click += new System.EventHandler(this.btn_RightLine_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(155, 12);
            this.label13.TabIndex = 27;
            this.label13.Text = "调整第       条竖线位置：";
            // 
            // com_ShuNum
            // 
            this.com_ShuNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_ShuNum.FormattingEnabled = true;
            this.com_ShuNum.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.com_ShuNum.Location = new System.Drawing.Point(207, 11);
            this.com_ShuNum.Name = "com_ShuNum";
            this.com_ShuNum.Size = new System.Drawing.Size(36, 20);
            this.com_ShuNum.TabIndex = 55;
            this.com_ShuNum.SelectedIndexChanged += new System.EventHandler(this.com_ShuNum_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(144, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 12);
            this.label12.TabIndex = 54;
            this.label12.Text = "插入竖线：       条";
            // 
            // com_HengNum
            // 
            this.com_HengNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_HengNum.FormattingEnabled = true;
            this.com_HengNum.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.com_HengNum.Location = new System.Drawing.Point(76, 11);
            this.com_HengNum.Name = "com_HengNum";
            this.com_HengNum.Size = new System.Drawing.Size(36, 20);
            this.com_HengNum.TabIndex = 53;
            this.com_HengNum.SelectedIndexChanged += new System.EventHandler(this.com_HengNum_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 52;
            this.label5.Text = "插入横线：       条";
            // 
            // label_AdvTextSeek
            // 
            this.label_AdvTextSeek.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_AdvTextSeek.ForeColor = System.Drawing.Color.Blue;
            this.label_AdvTextSeek.Location = new System.Drawing.Point(85, 296);
            this.label_AdvTextSeek.Name = "label_AdvTextSeek";
            this.label_AdvTextSeek.Size = new System.Drawing.Size(20, 11);
            this.label_AdvTextSeek.TabIndex = 51;
            this.label_AdvTextSeek.Text = "0";
            this.label_AdvTextSeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Green;
            this.label11.Location = new System.Drawing.Point(184, 409);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(221, 12);
            this.label11.TabIndex = 49;
            this.label11.Text = "删除:在文中删除&&与&&之间的部分(包括&&)";
            // 
            // listView_AllCollect
            // 
            this.listView_AllCollect.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listView_AllCollect.FullRowSelect = true;
            this.listView_AllCollect.Location = new System.Drawing.Point(7, 311);
            this.listView_AllCollect.MultiSelect = false;
            this.listView_AllCollect.Name = "listView_AllCollect";
            this.listView_AllCollect.Size = new System.Drawing.Size(403, 88);
            this.listView_AllCollect.TabIndex = 48;
            this.listView_AllCollect.UseCompatibleStateImageBehavior = false;
            this.listView_AllCollect.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "通道名";
            this.columnHeader5.Width = 82;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "基站ID";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "通道序号";
            this.columnHeader7.Width = 74;
            // 
            // btn_RefreshCollect
            // 
            this.btn_RefreshCollect.Location = new System.Drawing.Point(7, 402);
            this.btn_RefreshCollect.Name = "btn_RefreshCollect";
            this.btn_RefreshCollect.Size = new System.Drawing.Size(119, 26);
            this.btn_RefreshCollect.TabIndex = 42;
            this.btn_RefreshCollect.Text = "刷新采集通道列表";
            this.btn_RefreshCollect.UseVisualStyleBackColor = true;
            this.btn_RefreshCollect.Click += new System.EventHandler(this.btn_RefreshCollect_Click);
            // 
            // btn_InsertCollect
            // 
            this.btn_InsertCollect.Location = new System.Drawing.Point(129, 402);
            this.btn_InsertCollect.Name = "btn_InsertCollect";
            this.btn_InsertCollect.Size = new System.Drawing.Size(43, 26);
            this.btn_InsertCollect.TabIndex = 39;
            this.btn_InsertCollect.Text = "插入";
            this.btn_InsertCollect.UseVisualStyleBackColor = true;
            this.btn_InsertCollect.Click += new System.EventHandler(this.btn_InsertCollect_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 296);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(215, 12);
            this.label10.TabIndex = 36;
            this.label10.Text = "在上文中的第     位置插入采集信息：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(197, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "显示文字(请勿输入 && 与 $ 符号)：";
            // 
            // panel_HengChange
            // 
            this.panel_HengChange.Controls.Add(this.com_HengChangeIndex);
            this.panel_HengChange.Controls.Add(this.btn_UpLine);
            this.panel_HengChange.Controls.Add(this.label_HengChangeValue);
            this.panel_HengChange.Controls.Add(this.btn_DownLine);
            this.panel_HengChange.Controls.Add(this.label7);
            this.panel_HengChange.Location = new System.Drawing.Point(9, 32);
            this.panel_HengChange.Name = "panel_HengChange";
            this.panel_HengChange.Size = new System.Drawing.Size(287, 28);
            this.panel_HengChange.TabIndex = 33;
            this.panel_HengChange.Visible = false;
            // 
            // com_HengChangeIndex
            // 
            this.com_HengChangeIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_HengChangeIndex.FormattingEnabled = true;
            this.com_HengChangeIndex.Location = new System.Drawing.Point(45, 5);
            this.com_HengChangeIndex.Name = "com_HengChangeIndex";
            this.com_HengChangeIndex.Size = new System.Drawing.Size(36, 20);
            this.com_HengChangeIndex.TabIndex = 57;
            this.com_HengChangeIndex.SelectedIndexChanged += new System.EventHandler(this.com_HengChangeIndex_SelectedIndexChanged);
            // 
            // btn_UpLine
            // 
            this.btn_UpLine.Location = new System.Drawing.Point(158, 3);
            this.btn_UpLine.Name = "btn_UpLine";
            this.btn_UpLine.Size = new System.Drawing.Size(45, 23);
            this.btn_UpLine.TabIndex = 25;
            this.btn_UpLine.Text = "上移";
            this.btn_UpLine.UseVisualStyleBackColor = true;
            this.btn_UpLine.Click += new System.EventHandler(this.btn_UpLine_Click);
            // 
            // label_HengChangeValue
            // 
            this.label_HengChangeValue.AutoSize = true;
            this.label_HengChangeValue.Location = new System.Drawing.Point(264, 9);
            this.label_HengChangeValue.Name = "label_HengChangeValue";
            this.label_HengChangeValue.Size = new System.Drawing.Size(11, 12);
            this.label_HengChangeValue.TabIndex = 31;
            this.label_HengChangeValue.Text = "0";
            // 
            // btn_DownLine
            // 
            this.btn_DownLine.Location = new System.Drawing.Point(207, 3);
            this.btn_DownLine.Name = "btn_DownLine";
            this.btn_DownLine.Size = new System.Drawing.Size(45, 23);
            this.btn_DownLine.TabIndex = 26;
            this.btn_DownLine.Text = "下移";
            this.btn_DownLine.UseVisualStyleBackColor = true;
            this.btn_DownLine.Click += new System.EventHandler(this.btn_DownLine_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 12);
            this.label7.TabIndex = 27;
            this.label7.Text = "调整第       条横线位置：";
            // 
            // btn_Preview
            // 
            this.btn_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Preview.Location = new System.Drawing.Point(189, 581);
            this.btn_Preview.Name = "btn_Preview";
            this.btn_Preview.Size = new System.Drawing.Size(75, 26);
            this.btn_Preview.TabIndex = 13;
            this.btn_Preview.Text = "预览";
            this.btn_Preview.UseVisualStyleBackColor = true;
            this.btn_Preview.Click += new System.EventHandler(this.btn_Preview_Click);
            // 
            // btn_Abort
            // 
            this.btn_Abort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Abort.Location = new System.Drawing.Point(351, 581);
            this.btn_Abort.Name = "btn_Abort";
            this.btn_Abort.Size = new System.Drawing.Size(75, 26);
            this.btn_Abort.TabIndex = 14;
            this.btn_Abort.Text = "取消";
            this.btn_Abort.UseVisualStyleBackColor = true;
            this.btn_Abort.Click += new System.EventHandler(this.btn_Abort_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.SteelBlue;
            this.label6.Location = new System.Drawing.Point(5, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 59;
            this.label6.Text = "基础考勤信息显示";
            // 
            // text_BasicTitle
            // 
            this.text_BasicTitle.Location = new System.Drawing.Point(87, 23);
            this.text_BasicTitle.Name = "text_BasicTitle";
            this.text_BasicTitle.Size = new System.Drawing.Size(120, 21);
            this.text_BasicTitle.TabIndex = 61;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 60;
            this.label8.Text = "标题名称：";
            // 
            // btn_BasicFont
            // 
            this.btn_BasicFont.Location = new System.Drawing.Point(242, 95);
            this.btn_BasicFont.Name = "btn_BasicFont";
            this.btn_BasicFont.Size = new System.Drawing.Size(75, 24);
            this.btn_BasicFont.TabIndex = 62;
            this.btn_BasicFont.Text = "更改字体";
            this.btn_BasicFont.UseVisualStyleBackColor = true;
            this.btn_BasicFont.Click += new System.EventHandler(this.btn_BasicFont_Click);
            // 
            // btn_BasicTextColor
            // 
            this.btn_BasicTextColor.Location = new System.Drawing.Point(321, 95);
            this.btn_BasicTextColor.Name = "btn_BasicTextColor";
            this.btn_BasicTextColor.Size = new System.Drawing.Size(97, 24);
            this.btn_BasicTextColor.TabIndex = 66;
            this.btn_BasicTextColor.Text = "更改字体颜色";
            this.btn_BasicTextColor.UseVisualStyleBackColor = true;
            this.btn_BasicTextColor.Click += new System.EventHandler(this.btn_BasicTextColor_Click);
            // 
            // radio_InMineNumTotal
            // 
            this.radio_InMineNumTotal.AutoSize = true;
            this.radio_InMineNumTotal.Checked = true;
            this.radio_InMineNumTotal.Location = new System.Drawing.Point(87, 50);
            this.radio_InMineNumTotal.Name = "radio_InMineNumTotal";
            this.radio_InMineNumTotal.Size = new System.Drawing.Size(83, 16);
            this.radio_InMineNumTotal.TabIndex = 67;
            this.radio_InMineNumTotal.TabStop = true;
            this.radio_InMineNumTotal.Text = "按总数显示";
            this.radio_InMineNumTotal.UseVisualStyleBackColor = true;
            // 
            // radio_InMineNumArea
            // 
            this.radio_InMineNumArea.AutoSize = true;
            this.radio_InMineNumArea.Location = new System.Drawing.Point(175, 50);
            this.radio_InMineNumArea.Name = "radio_InMineNumArea";
            this.radio_InMineNumArea.Size = new System.Drawing.Size(83, 16);
            this.radio_InMineNumArea.TabIndex = 68;
            this.radio_InMineNumArea.Text = "按区域显示";
            this.radio_InMineNumArea.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(22, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 69;
            this.label14.Text = "进洞人数：";
            // 
            // FrmLED_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 612);
            this.Controls.Add(this.radio_InMineNumArea);
            this.Controls.Add(this.radio_InMineNumTotal);
            this.Controls.Add(this.btn_BasicTextColor);
            this.Controls.Add(this.check_Adv);
            this.Controls.Add(this.btn_BasicFont);
            this.Controls.Add(this.text_BasicTitle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_Abort);
            this.Controls.Add(this.btn_Preview);
            this.Controls.Add(this.check_TopMost);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.com_LoopHuman);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.group_Adv);
            this.Controls.Add(this.label14);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLED_Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LED参数设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLED_Setting_FormClosed);
            this.group_Adv.ResumeLayout(false);
            this.group_Adv.PerformLayout();
            this.panel_ShuChange.ResumeLayout(false);
            this.panel_ShuChange.PerformLayout();
            this.panel_HengChange.ResumeLayout(false);
            this.panel_HengChange.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox com_LoopHuman;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox check_TopMost;
        private System.Windows.Forms.CheckBox check_Adv;
        private System.Windows.Forms.GroupBox group_Adv;
        private System.Windows.Forms.Button btn_Preview;
        private System.Windows.Forms.Button btn_Abort;
        private System.Windows.Forms.Button btn_DownLine;
        private System.Windows.Forms.Button btn_UpLine;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_HengChangeValue;
        private System.Windows.Forms.Panel panel_HengChange;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_InsertCollect;
        private System.Windows.Forms.Button btn_RefreshCollect;
        private System.Windows.Forms.ListView listView_AllCollect;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label_AdvTextSeek;
        private System.Windows.Forms.ComboBox com_HengChangeIndex;
        private System.Windows.Forms.ComboBox com_ShuNum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox com_HengNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel_ShuChange;
        private System.Windows.Forms.ComboBox com_ShuChangeIndex;
        private System.Windows.Forms.Button btn_LeftLine;
        private System.Windows.Forms.Label label_ShuChangeValue;
        private System.Windows.Forms.Button btn_RightLine;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.TextBox text_AdvText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox text_BasicTitle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_BasicFont;
        private System.Windows.Forms.Button btn_AdvFont;
        private System.Windows.Forms.Button btn_AdvLineColor;
        private System.Windows.Forms.Button btn_AdvTextColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btn_BasicTextColor;
        private System.Windows.Forms.RadioButton radio_InMineNumTotal;
        private System.Windows.Forms.RadioButton radio_InMineNumArea;
        private System.Windows.Forms.Label label14;
    }
}