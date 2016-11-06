namespace PersonPosition.View
{
    partial class FrmLED
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
            this.components = new System.ComponentModel.Container();
            this.label_Title = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.钉在桌面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label_Name = new System.Windows.Forms.Label();
            this.timer_LoopHuman = new System.Windows.Forms.Timer(this.components);
            this.label_WorkType = new System.Windows.Forms.Label();
            this.label_Time = new System.Windows.Forms.Label();
            this.pic_AdvShow = new System.Windows.Forms.PictureBox();
            this.panel_Basic = new System.Windows.Forms.Panel();
            this.label_Area = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_AdvShow)).BeginInit();
            this.panel_Basic.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Title
            // 
            this.label_Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Title.BackColor = System.Drawing.Color.Black;
            this.label_Title.ForeColor = System.Drawing.Color.Red;
            this.label_Title.Location = new System.Drawing.Point(-3, 1);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(164, 37);
            this.label_Title.TabIndex = 0;
            this.label_Title.Text = "地图名称\r\n时钟\r\n进洞总人数:0";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_Title_MouseMove);
            this.label_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_Title_MouseDown);
            this.label_Title.Resize += new System.EventHandler(this.label_Title_Resize);
            this.label_Title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label_Title_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.钉在桌面ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 70);
            // 
            // 钉在桌面ToolStripMenuItem
            // 
            this.钉在桌面ToolStripMenuItem.Name = "钉在桌面ToolStripMenuItem";
            this.钉在桌面ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.钉在桌面ToolStripMenuItem.Text = "钉在桌面";
            this.钉在桌面ToolStripMenuItem.Click += new System.EventHandler(this.钉在桌面ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.设置ToolStripMenuItem.Text = "设置";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.设置ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.退出ToolStripMenuItem.Text = "关闭LED";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.BackColor = System.Drawing.Color.Black;
            this.label_Name.ForeColor = System.Drawing.Color.Red;
            this.label_Name.Location = new System.Drawing.Point(-1, 64);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(29, 12);
            this.label_Name.TabIndex = 5;
            this.label_Name.Text = "姓名";
            this.label_Name.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label6_MouseMove);
            this.label_Name.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label6_MouseDown);
            this.label_Name.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label6_MouseUp);
            // 
            // timer_LoopHuman
            // 
            this.timer_LoopHuman.Enabled = true;
            this.timer_LoopHuman.Interval = 2000;
            this.timer_LoopHuman.Tick += new System.EventHandler(this.timer_LoopHuman_Tick);
            // 
            // label_WorkType
            // 
            this.label_WorkType.AutoSize = true;
            this.label_WorkType.BackColor = System.Drawing.Color.Black;
            this.label_WorkType.Font = new System.Drawing.Font("宋体", 9F);
            this.label_WorkType.ForeColor = System.Drawing.Color.Red;
            this.label_WorkType.Location = new System.Drawing.Point(40, 64);
            this.label_WorkType.Name = "label_WorkType";
            this.label_WorkType.Size = new System.Drawing.Size(29, 12);
            this.label_WorkType.TabIndex = 6;
            this.label_WorkType.Text = "部门";
            this.label_WorkType.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label7_MouseMove);
            this.label_WorkType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label7_MouseDown);
            this.label_WorkType.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label7_MouseUp);
            // 
            // label_Time
            // 
            this.label_Time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Time.AutoSize = true;
            this.label_Time.BackColor = System.Drawing.Color.Black;
            this.label_Time.ForeColor = System.Drawing.Color.Red;
            this.label_Time.Location = new System.Drawing.Point(132, 64);
            this.label_Time.Name = "label_Time";
            this.label_Time.Size = new System.Drawing.Size(29, 12);
            this.label_Time.TabIndex = 7;
            this.label_Time.Text = "时间";
            this.label_Time.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_Time.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label8_MouseMove);
            this.label_Time.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label8_MouseDown);
            this.label_Time.Resize += new System.EventHandler(this.label8_Resize);
            this.label_Time.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label8_MouseUp);
            // 
            // pic_AdvShow
            // 
            this.pic_AdvShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_AdvShow.BackColor = System.Drawing.Color.Black;
            this.pic_AdvShow.Location = new System.Drawing.Point(128, 0);
            this.pic_AdvShow.Name = "pic_AdvShow";
            this.pic_AdvShow.Size = new System.Drawing.Size(33, 96);
            this.pic_AdvShow.TabIndex = 10;
            this.pic_AdvShow.TabStop = false;
            this.pic_AdvShow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_AdvShow_MouseMove);
            this.pic_AdvShow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_AdvShow_MouseDown);
            this.pic_AdvShow.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_AdvShow_Paint);
            this.pic_AdvShow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_AdvShow_MouseUp);
            // 
            // panel_Basic
            // 
            this.panel_Basic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_Basic.BackColor = System.Drawing.Color.Black;
            this.panel_Basic.Controls.Add(this.label_Time);
            this.panel_Basic.Controls.Add(this.label_WorkType);
            this.panel_Basic.Controls.Add(this.label_Name);
            this.panel_Basic.Controls.Add(this.label_Area);
            this.panel_Basic.Controls.Add(this.label_Title);
            this.panel_Basic.Location = new System.Drawing.Point(0, 0);
            this.panel_Basic.Name = "panel_Basic";
            this.panel_Basic.Size = new System.Drawing.Size(160, 96);
            this.panel_Basic.TabIndex = 11;
            this.panel_Basic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Basic_MouseMove);
            this.panel_Basic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Basic_MouseDown);
            this.panel_Basic.Resize += new System.EventHandler(this.panel_Basic_Resize);
            this.panel_Basic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Basic_MouseUp);
            // 
            // label_Area
            // 
            this.label_Area.AutoSize = true;
            this.label_Area.BackColor = System.Drawing.Color.Black;
            this.label_Area.Font = new System.Drawing.Font("宋体", 9F);
            this.label_Area.ForeColor = System.Drawing.Color.Red;
            this.label_Area.Location = new System.Drawing.Point(75, 64);
            this.label_Area.Name = "label_Area";
            this.label_Area.Size = new System.Drawing.Size(29, 12);
            this.label_Area.TabIndex = 8;
            this.label_Area.Text = "区域";
            this.label_Area.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_Area_MouseMove);
            this.label_Area.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_Area_MouseDown);
            this.label_Area.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label_Area_MouseUp);
            // 
            // FrmLED
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(160, 96);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panel_Basic);
            this.Controls.Add(this.pic_AdvShow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLED";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmLED_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLED_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmLED_MouseMove);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_AdvShow)).EndInit();
            this.panel_Basic.ResumeLayout(false);
            this.panel_Basic.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Timer timer_LoopHuman;
        private System.Windows.Forms.ToolStripMenuItem 钉在桌面ToolStripMenuItem;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_WorkType;
        private System.Windows.Forms.Label label_Time;
        private System.Windows.Forms.PictureBox pic_AdvShow;
        private System.Windows.Forms.Panel panel_Basic;
        private System.Windows.Forms.Label label_Area;
    }
}

