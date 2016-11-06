namespace PersonPosition.View
{
    partial class FrmShowInfo
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
            this.panel_ShowInfo = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.text_ShowInfo = new System.Windows.Forms.TextBox();
            this.com_ShowInfoIndex = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.com_ShowInfoStyle = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.com_StationID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_ShowInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ShowInfo
            // 
            this.panel_ShowInfo.Controls.Add(this.label19);
            this.panel_ShowInfo.Controls.Add(this.text_ShowInfo);
            this.panel_ShowInfo.Controls.Add(this.com_ShowInfoIndex);
            this.panel_ShowInfo.Controls.Add(this.label18);
            this.panel_ShowInfo.Controls.Add(this.com_ShowInfoStyle);
            this.panel_ShowInfo.Controls.Add(this.label17);
            this.panel_ShowInfo.Location = new System.Drawing.Point(11, 28);
            this.panel_ShowInfo.Name = "panel_ShowInfo";
            this.panel_ShowInfo.Size = new System.Drawing.Size(203, 93);
            this.panel_ShowInfo.TabIndex = 62;
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
            this.text_ShowInfo.MaxLength = 20;
            this.text_ShowInfo.Name = "text_ShowInfo";
            this.text_ShowInfo.Size = new System.Drawing.Size(197, 21);
            this.text_ShowInfo.TabIndex = 3;
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
            this.com_ShowInfoIndex.Size = new System.Drawing.Size(132, 20);
            this.com_ShowInfoIndex.TabIndex = 2;
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
            this.com_ShowInfoStyle.Size = new System.Drawing.Size(132, 20);
            this.com_ShowInfoStyle.TabIndex = 1;
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
            // com_StationID
            // 
            this.com_StationID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_StationID.FormattingEnabled = true;
            this.com_StationID.Location = new System.Drawing.Point(78, 6);
            this.com_StationID.Name = "com_StationID";
            this.com_StationID.Size = new System.Drawing.Size(132, 20);
            this.com_StationID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 65;
            this.label1.Text = "考勤基站：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "设置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmShowInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 124);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.com_StationID);
            this.Controls.Add(this.panel_ShowInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmShowInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置考勤基站显示信息";
            this.panel_ShowInfo.ResumeLayout(false);
            this.panel_ShowInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_ShowInfo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox text_ShowInfo;
        private System.Windows.Forms.ComboBox com_ShowInfoIndex;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox com_ShowInfoStyle;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox com_StationID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}