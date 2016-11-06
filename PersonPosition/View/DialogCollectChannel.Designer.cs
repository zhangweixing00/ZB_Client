namespace PersonPosition.View
{
    partial class DialogCollectChannel
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
            this.text_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_Channel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.com_ChannelType = new System.Windows.Forms.ComboBox();
            this.textBox_PerK = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_PerC = new System.Windows.Forms.TextBox();
            this.textBox_Unit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_ValueMin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_ValueMax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Canel = new System.Windows.Forms.Button();
            this.listview_CanUseChannel = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.textBox_ChannelComment = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // text_Name
            // 
            this.text_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_Name.Location = new System.Drawing.Point(65, 40);
            this.text_Name.Name = "text_Name";
            this.text_Name.Size = new System.Drawing.Size(120, 21);
            this.text_Name.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "通道名称：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "通 道 号：";
            // 
            // textBox_Channel
            // 
            this.textBox_Channel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Channel.Location = new System.Drawing.Point(65, 12);
            this.textBox_Channel.Name = "textBox_Channel";
            this.textBox_Channel.ReadOnly = true;
            this.textBox_Channel.Size = new System.Drawing.Size(120, 21);
            this.textBox_Channel.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 54;
            this.label1.Text = "通道类型：";
            // 
            // com_ChannelType
            // 
            this.com_ChannelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_ChannelType.FormattingEnabled = true;
            this.com_ChannelType.Items.AddRange(new object[] {
            "电压电流型",
            "振弦型",
            "开关型"});
            this.com_ChannelType.Location = new System.Drawing.Point(65, 69);
            this.com_ChannelType.Name = "com_ChannelType";
            this.com_ChannelType.Size = new System.Drawing.Size(120, 20);
            this.com_ChannelType.TabIndex = 55;
            // 
            // textBox_PerK
            // 
            this.textBox_PerK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_PerK.Location = new System.Drawing.Point(84, 95);
            this.textBox_PerK.Name = "textBox_PerK";
            this.textBox_PerK.Size = new System.Drawing.Size(39, 21);
            this.textBox_PerK.TabIndex = 56;
            this.textBox_PerK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_PerK_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 57;
            this.label2.Text = "通道参数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 58;
            this.label4.Text = "K";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(129, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 60;
            this.label6.Text = "C";
            // 
            // textBox_PerC
            // 
            this.textBox_PerC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_PerC.Location = new System.Drawing.Point(146, 95);
            this.textBox_PerC.Name = "textBox_PerC";
            this.textBox_PerC.Size = new System.Drawing.Size(39, 21);
            this.textBox_PerC.TabIndex = 59;
            this.textBox_PerC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_PerC_KeyPress);
            // 
            // textBox_Unit
            // 
            this.textBox_Unit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Unit.Location = new System.Drawing.Point(92, 153);
            this.textBox_Unit.Name = "textBox_Unit";
            this.textBox_Unit.Size = new System.Drawing.Size(93, 21);
            this.textBox_Unit.TabIndex = 61;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 62;
            this.label7.Text = "通道数据单位：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(113, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 67;
            this.label8.Text = "下限";
            // 
            // textBox_ValueMin
            // 
            this.textBox_ValueMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ValueMin.Location = new System.Drawing.Point(145, 201);
            this.textBox_ValueMin.Name = "textBox_ValueMin";
            this.textBox_ValueMin.Size = new System.Drawing.Size(39, 21);
            this.textBox_ValueMin.TabIndex = 66;
            this.textBox_ValueMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_ValueMin_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 65;
            this.label9.Text = "上限";
            // 
            // textBox_ValueMax
            // 
            this.textBox_ValueMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ValueMax.Location = new System.Drawing.Point(68, 201);
            this.textBox_ValueMax.Name = "textBox_ValueMax";
            this.textBox_ValueMax.Size = new System.Drawing.Size(39, 21);
            this.textBox_ValueMax.TabIndex = 63;
            this.textBox_ValueMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_ValueMax_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 184);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 64;
            this.label10.Text = "通道数据超限值：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(194, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 68;
            this.label11.Text = "可用通道号列表：";
            // 
            // btn_Save
            // 
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Save.Image = global::PersonPosition.Properties.Resources.Sure;
            this.btn_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Save.Location = new System.Drawing.Point(25, 285);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(68, 27);
            this.btn_Save.TabIndex = 69;
            this.btn_Save.Text = "确定";
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
            this.btn_Canel.Location = new System.Drawing.Point(102, 285);
            this.btn_Canel.Name = "btn_Canel";
            this.btn_Canel.Size = new System.Drawing.Size(68, 27);
            this.btn_Canel.TabIndex = 70;
            this.btn_Canel.Text = "取消";
            this.btn_Canel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Canel.UseVisualStyleBackColor = true;
            this.btn_Canel.Click += new System.EventHandler(this.btn_Canel_Click);
            // 
            // listview_CanUseChannel
            // 
            this.listview_CanUseChannel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listview_CanUseChannel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listview_CanUseChannel.FullRowSelect = true;
            this.listview_CanUseChannel.GridLines = true;
            this.listview_CanUseChannel.Location = new System.Drawing.Point(197, 25);
            this.listview_CanUseChannel.MultiSelect = false;
            this.listview_CanUseChannel.Name = "listview_CanUseChannel";
            this.listview_CanUseChannel.Size = new System.Drawing.Size(96, 287);
            this.listview_CanUseChannel.TabIndex = 71;
            this.listview_CanUseChannel.UseCompatibleStateImageBehavior = false;
            this.listview_CanUseChannel.View = System.Windows.Forms.View.Details;
            this.listview_CanUseChannel.SelectedIndexChanged += new System.EventHandler(this.listview_CanUseChannel_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "通道号";
            this.columnHeader1.Width = 69;
            // 
            // textBox_ChannelComment
            // 
            this.textBox_ChannelComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ChannelComment.Location = new System.Drawing.Point(65, 124);
            this.textBox_ChannelComment.Name = "textBox_ChannelComment";
            this.textBox_ChannelComment.Size = new System.Drawing.Size(120, 21);
            this.textBox_ChannelComment.TabIndex = 72;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 73;
            this.label12.Text = "通道描述：";
            // 
            // DialogCollectChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 317);
            this.Controls.Add(this.textBox_ChannelComment);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.listview_CanUseChannel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_Canel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_ValueMin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_ValueMax);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox_Unit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_PerC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_PerK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.com_ChannelType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Channel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.text_Name);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogCollectChannel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "**采集器通道";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Channel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox com_ChannelType;
        private System.Windows.Forms.TextBox textBox_PerK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_PerC;
        private System.Windows.Forms.TextBox textBox_Unit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_ValueMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_ValueMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Canel;
        private System.Windows.Forms.ListView listview_CanUseChannel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox textBox_ChannelComment;
        private System.Windows.Forms.Label label12;
    }
}