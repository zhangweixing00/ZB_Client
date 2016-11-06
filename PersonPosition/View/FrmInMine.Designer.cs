namespace PersonPosition.View
{
    partial class FrmInMine
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.label_AllNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(2, 42);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(483, 322);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "工号";
            this.columnHeader1.Width = 73;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "卡号";
            this.columnHeader2.Width = 50;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "姓名";
            this.columnHeader3.Width = 62;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "部门";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "职务";
            this.columnHeader5.Width = 71;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "进入时间";
            this.columnHeader6.Width = 123;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Image = global::PersonPosition.Properties.Resources.RePlay;
            this.btn_Refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Refresh.Location = new System.Drawing.Point(417, 10);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(68, 26);
            this.btn_Refresh.TabIndex = 2;
            this.btn_Refresh.Text = "刷新 ";
            this.btn_Refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // label_AllNum
            // 
            this.label_AllNum.AutoSize = true;
            this.label_AllNum.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_AllNum.ForeColor = System.Drawing.Color.SteelBlue;
            this.label_AllNum.Location = new System.Drawing.Point(12, 14);
            this.label_AllNum.Name = "label_AllNum";
            this.label_AllNum.Size = new System.Drawing.Size(41, 15);
            this.label_AllNum.TabIndex = 4;
            this.label_AllNum.Text = "0 人";
            // 
            // FrmInMine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(488, 366);
            this.Controls.Add(this.label_AllNum);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmInMine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人员";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Label label_AllNum;
    }
}