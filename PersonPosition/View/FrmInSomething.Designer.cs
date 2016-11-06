namespace PersonPosition.View
{
    partial class FrmInSomething
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
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch
            {   }
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label_AllNum = new System.Windows.Forms.Label();
            this.dataGV_Table = new System.Windows.Forms.DataGridView();
            this.btn_SearchPosition = new System.Windows.Forms.Button();
            this.btn_SearchHistory = new System.Windows.Forms.Button();
            this.btn_SearchAlarm = new System.Windows.Forms.Button();
            this.btn_SearchDuty = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Info = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV_Table)).BeginInit();
            this.SuspendLayout();
            // 
            // label_AllNum
            // 
            this.label_AllNum.AutoSize = true;
            this.label_AllNum.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_AllNum.ForeColor = System.Drawing.Color.SteelBlue;
            this.label_AllNum.Location = new System.Drawing.Point(7, 6);
            this.label_AllNum.Name = "label_AllNum";
            this.label_AllNum.Size = new System.Drawing.Size(41, 15);
            this.label_AllNum.TabIndex = 3;
            this.label_AllNum.Text = "0 人";
            // 
            // dataGV_Table
            // 
            this.dataGV_Table.AllowUserToAddRows = false;
            this.dataGV_Table.AllowUserToOrderColumns = true;
            this.dataGV_Table.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGV_Table.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGV_Table.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGV_Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV_Table.Location = new System.Drawing.Point(3, 56);
            this.dataGV_Table.MultiSelect = false;
            this.dataGV_Table.Name = "dataGV_Table";
            this.dataGV_Table.ReadOnly = true;
            this.dataGV_Table.RowHeadersVisible = false;
            this.dataGV_Table.RowTemplate.Height = 23;
            this.dataGV_Table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGV_Table.ShowCellErrors = false;
            this.dataGV_Table.ShowRowErrors = false;
            this.dataGV_Table.Size = new System.Drawing.Size(546, 317);
            this.dataGV_Table.TabIndex = 4;
            this.dataGV_Table.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGV_AllInMine_DataError);
            // 
            // btn_SearchPosition
            // 
            this.btn_SearchPosition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SearchPosition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SearchPosition.Image = global::PersonPosition.Properties.Resources.ssjk_Small;
            this.btn_SearchPosition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_SearchPosition.Location = new System.Drawing.Point(3, 27);
            this.btn_SearchPosition.Name = "btn_SearchPosition";
            this.btn_SearchPosition.Size = new System.Drawing.Size(104, 26);
            this.btn_SearchPosition.TabIndex = 2;
            this.btn_SearchPosition.Text = "去地图查看";
            this.btn_SearchPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_SearchPosition.UseVisualStyleBackColor = true;
            this.btn_SearchPosition.Click += new System.EventHandler(this.btn_SearchPosition_Click);
            // 
            // btn_SearchHistory
            // 
            this.btn_SearchHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SearchHistory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SearchHistory.Image = global::PersonPosition.Properties.Resources.rygj_Small;
            this.btn_SearchHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_SearchHistory.Location = new System.Drawing.Point(297, 27);
            this.btn_SearchHistory.Name = "btn_SearchHistory";
            this.btn_SearchHistory.Size = new System.Drawing.Size(86, 26);
            this.btn_SearchHistory.TabIndex = 5;
            this.btn_SearchHistory.Text = "历史轨迹";
            this.btn_SearchHistory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_SearchHistory.UseVisualStyleBackColor = true;
            this.btn_SearchHistory.Click += new System.EventHandler(this.btn_SearchHistory_Click);
            // 
            // btn_SearchAlarm
            // 
            this.btn_SearchAlarm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SearchAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SearchAlarm.Image = global::PersonPosition.Properties.Resources.ssbj_Small;
            this.btn_SearchAlarm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_SearchAlarm.Location = new System.Drawing.Point(205, 27);
            this.btn_SearchAlarm.Name = "btn_SearchAlarm";
            this.btn_SearchAlarm.Size = new System.Drawing.Size(86, 26);
            this.btn_SearchAlarm.TabIndex = 4;
            this.btn_SearchAlarm.Text = "报警记录";
            this.btn_SearchAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_SearchAlarm.UseVisualStyleBackColor = true;
            this.btn_SearchAlarm.Click += new System.EventHandler(this.btn_SearchAlarm_Click);
            // 
            // btn_SearchDuty
            // 
            this.btn_SearchDuty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SearchDuty.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SearchDuty.Image = global::PersonPosition.Properties.Resources.rykq_Small;
            this.btn_SearchDuty.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_SearchDuty.Location = new System.Drawing.Point(113, 27);
            this.btn_SearchDuty.Name = "btn_SearchDuty";
            this.btn_SearchDuty.Size = new System.Drawing.Size(86, 26);
            this.btn_SearchDuty.TabIndex = 3;
            this.btn_SearchDuty.Text = "考勤记录";
            this.btn_SearchDuty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_SearchDuty.UseVisualStyleBackColor = true;
            this.btn_SearchDuty.Click += new System.EventHandler(this.btn_SearchDuty_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Image = global::PersonPosition.Properties.Resources.RePlay;
            this.btn_Refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Refresh.Location = new System.Drawing.Point(481, 27);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(68, 26);
            this.btn_Refresh.TabIndex = 1;
            this.btn_Refresh.Text = "刷新 ";
            this.btn_Refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Info
            // 
            this.btn_Info.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Info.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Info.Image = global::PersonPosition.Properties.Resources.Text;
            this.btn_Info.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Info.Location = new System.Drawing.Point(389, 27);
            this.btn_Info.Name = "btn_Info";
            this.btn_Info.Size = new System.Drawing.Size(86, 26);
            this.btn_Info.TabIndex = 6;
            this.btn_Info.Text = "详细信息";
            this.btn_Info.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Info.UseVisualStyleBackColor = true;
            this.btn_Info.Click += new System.EventHandler(this.btn_Info_Click);
            // 
            // FrmInSomething
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(553, 376);
            this.Controls.Add(this.btn_Info);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.btn_SearchPosition);
            this.Controls.Add(this.btn_SearchHistory);
            this.Controls.Add(this.btn_SearchAlarm);
            this.Controls.Add(this.btn_SearchDuty);
            this.Controls.Add(this.dataGV_Table);
            this.Controls.Add(this.label_AllNum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FrmInSomething";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人员";
            ((System.ComponentModel.ISupportInitialize)(this.dataGV_Table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_AllNum;
        private System.Windows.Forms.DataGridView dataGV_Table;
        private System.Windows.Forms.Button btn_SearchPosition;
        private System.Windows.Forms.Button btn_SearchHistory;
        private System.Windows.Forms.Button btn_SearchAlarm;
        private System.Windows.Forms.Button btn_SearchDuty;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_Info;

    }
}