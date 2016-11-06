namespace PersonPosition.View
{
    partial class DialogSearch
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
            this.listView_Person = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.listView_Station = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Info = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "满足搜索条件的人员：";
            // 
            // listView_Person
            // 
            this.listView_Person.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView_Person.FullRowSelect = true;
            this.listView_Person.Location = new System.Drawing.Point(8, 23);
            this.listView_Person.Name = "listView_Person";
            this.listView_Person.Size = new System.Drawing.Size(458, 88);
            this.listView_Person.TabIndex = 1;
            this.listView_Person.UseCompatibleStateImageBehavior = false;
            this.listView_Person.View = System.Windows.Forms.View.Details;
            this.listView_Person.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Person_MouseDoubleClick);
            this.listView_Person.SelectedIndexChanged += new System.EventHandler(this.listView_Person_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "工号";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "姓名";
            this.columnHeader2.Width = 103;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "卡号";
            this.columnHeader3.Width = 64;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "职务";
            this.columnHeader4.Width = 104;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "部门";
            this.columnHeader5.Width = 102;
            // 
            // listView_Station
            // 
            this.listView_Station.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView_Station.FullRowSelect = true;
            this.listView_Station.Location = new System.Drawing.Point(8, 138);
            this.listView_Station.Name = "listView_Station";
            this.listView_Station.Size = new System.Drawing.Size(458, 88);
            this.listView_Station.TabIndex = 2;
            this.listView_Station.UseCompatibleStateImageBehavior = false;
            this.listView_Station.View = System.Windows.Forms.View.Details;
            this.listView_Station.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Station_MouseDoubleClick);
            this.listView_Station.SelectedIndexChanged += new System.EventHandler(this.listView_Station_SelectedIndexChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "编号";
            this.columnHeader6.Width = 86;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "基站名称";
            this.columnHeader7.Width = 145;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "基站类型";
            this.columnHeader8.Width = 191;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "满足搜索条件的基站：";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(276, 234);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(99, 27);
            this.btn_Sure.TabIndex = 4;
            this.btn_Sure.Text = "去地图中查看";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(381, 234);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(85, 27);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "关闭";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(7, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "请选择一个您欲查询的对象";
            this.label3.Visible = false;
            // 
            // btn_Info
            // 
            this.btn_Info.Location = new System.Drawing.Point(171, 234);
            this.btn_Info.Name = "btn_Info";
            this.btn_Info.Size = new System.Drawing.Size(99, 27);
            this.btn_Info.TabIndex = 7;
            this.btn_Info.Text = "查看详细信息";
            this.btn_Info.UseVisualStyleBackColor = true;
            this.btn_Info.Click += new System.EventHandler(this.btn_Info_Click);
            // 
            // DialogSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 269);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Info);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView_Station);
            this.Controls.Add(this.listView_Person);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DialogSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "搜索地图元素";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView_Person;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ListView listView_Station;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Info;
    }
}