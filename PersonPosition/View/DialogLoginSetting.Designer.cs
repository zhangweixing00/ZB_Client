namespace PersonPosition.View
{
    partial class DialogLoginSetting
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
            this.text_IP = new System.Windows.Forms.TextBox();
            this.text_Port = new System.Windows.Forms.TextBox();
            this.text_DBName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_DBUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.text_DBPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.list_Server = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.group_ServerInfo = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_DBIP = new System.Windows.Forms.TextBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.text_ServerTitle = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btn_DelServer = new System.Windows.Forms.Button();
            this.btn_NewServer = new System.Windows.Forms.Button();
            this.btn_SetDefault = new System.Windows.Forms.Button();
            this.link_CurrentlyServer = new System.Windows.Forms.LinkLabel();
            this.group_ServerInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器IP\r\n或主机名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务器端口：";
            // 
            // text_IP
            // 
            this.text_IP.Location = new System.Drawing.Point(91, 46);
            this.text_IP.Name = "text_IP";
            this.text_IP.Size = new System.Drawing.Size(127, 21);
            this.text_IP.TabIndex = 6;
            // 
            // text_Port
            // 
            this.text_Port.Location = new System.Drawing.Point(91, 73);
            this.text_Port.Name = "text_Port";
            this.text_Port.Size = new System.Drawing.Size(127, 21);
            this.text_Port.TabIndex = 7;
            this.text_Port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_Port_KeyPress);
            // 
            // text_DBName
            // 
            this.text_DBName.Location = new System.Drawing.Point(91, 127);
            this.text_DBName.Name = "text_DBName";
            this.text_DBName.Size = new System.Drawing.Size(127, 21);
            this.text_DBName.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "数据库名称：";
            // 
            // text_DBUserName
            // 
            this.text_DBUserName.Location = new System.Drawing.Point(91, 155);
            this.text_DBUserName.Name = "text_DBUserName";
            this.text_DBUserName.Size = new System.Drawing.Size(127, 21);
            this.text_DBUserName.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "数据库用户：";
            // 
            // text_DBPassword
            // 
            this.text_DBPassword.Location = new System.Drawing.Point(91, 182);
            this.text_DBPassword.Name = "text_DBPassword";
            this.text_DBPassword.Size = new System.Drawing.Size(127, 21);
            this.text_DBPassword.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "数据库密码：";
            // 
            // list_Server
            // 
            this.list_Server.FormattingEnabled = true;
            this.list_Server.ItemHeight = 12;
            this.list_Server.Location = new System.Drawing.Point(9, 51);
            this.list_Server.Name = "list_Server";
            this.list_Server.Size = new System.Drawing.Size(124, 100);
            this.list_Server.TabIndex = 18;
            this.list_Server.SelectedIndexChanged += new System.EventHandler(this.list_Server_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(7, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "服务器列表：";
            // 
            // group_ServerInfo
            // 
            this.group_ServerInfo.Controls.Add(this.label3);
            this.group_ServerInfo.Controls.Add(this.text_DBIP);
            this.group_ServerInfo.Controls.Add(this.btn_Save);
            this.group_ServerInfo.Controls.Add(this.label8);
            this.group_ServerInfo.Controls.Add(this.text_ServerTitle);
            this.group_ServerInfo.Controls.Add(this.label2);
            this.group_ServerInfo.Controls.Add(this.text_DBPassword);
            this.group_ServerInfo.Controls.Add(this.text_IP);
            this.group_ServerInfo.Controls.Add(this.label6);
            this.group_ServerInfo.Controls.Add(this.text_Port);
            this.group_ServerInfo.Controls.Add(this.text_DBUserName);
            this.group_ServerInfo.Controls.Add(this.label5);
            this.group_ServerInfo.Controls.Add(this.text_DBName);
            this.group_ServerInfo.Controls.Add(this.label4);
            this.group_ServerInfo.Controls.Add(this.label1);
            this.group_ServerInfo.Enabled = false;
            this.group_ServerInfo.Location = new System.Drawing.Point(9, 165);
            this.group_ServerInfo.Name = "group_ServerInfo";
            this.group_ServerInfo.Size = new System.Drawing.Size(226, 243);
            this.group_ServerInfo.TabIndex = 20;
            this.group_ServerInfo.TabStop = false;
            this.group_ServerInfo.Text = "服务器详细配置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "数据库地址：";
            // 
            // text_DBIP
            // 
            this.text_DBIP.Location = new System.Drawing.Point(91, 100);
            this.text_DBIP.Name = "text_DBIP";
            this.text_DBIP.Size = new System.Drawing.Size(127, 21);
            this.text_DBIP.TabIndex = 22;
            // 
            // btn_Save
            // 
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Save.Image = global::PersonPosition.Properties.Resources.Sure;
            this.btn_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Save.Location = new System.Drawing.Point(154, 209);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(64, 27);
            this.btn_Save.TabIndex = 20;
            this.btn_Save.Text = "保存";
            this.btn_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "服务器标题：";
            // 
            // text_ServerTitle
            // 
            this.text_ServerTitle.Location = new System.Drawing.Point(91, 19);
            this.text_ServerTitle.Name = "text_ServerTitle";
            this.text_ServerTitle.Size = new System.Drawing.Size(127, 21);
            this.text_ServerTitle.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.SteelBlue;
            this.label15.Location = new System.Drawing.Point(7, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 12);
            this.label15.TabIndex = 23;
            this.label15.Text = "当前默认服务器：  空";
            // 
            // btn_DelServer
            // 
            this.btn_DelServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DelServer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_DelServer.Image = global::PersonPosition.Properties.Resources.Del;
            this.btn_DelServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DelServer.Location = new System.Drawing.Point(139, 115);
            this.btn_DelServer.Name = "btn_DelServer";
            this.btn_DelServer.Size = new System.Drawing.Size(96, 26);
            this.btn_DelServer.TabIndex = 28;
            this.btn_DelServer.Text = "删除服务器";
            this.btn_DelServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_DelServer.UseVisualStyleBackColor = true;
            this.btn_DelServer.Click += new System.EventHandler(this.btn_DelServer_Click);
            // 
            // btn_NewServer
            // 
            this.btn_NewServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_NewServer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_NewServer.Image = global::PersonPosition.Properties.Resources.Add;
            this.btn_NewServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_NewServer.Location = new System.Drawing.Point(139, 83);
            this.btn_NewServer.Name = "btn_NewServer";
            this.btn_NewServer.Size = new System.Drawing.Size(96, 26);
            this.btn_NewServer.TabIndex = 26;
            this.btn_NewServer.Text = "新建服务器";
            this.btn_NewServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_NewServer.UseVisualStyleBackColor = true;
            this.btn_NewServer.Click += new System.EventHandler(this.btn_NewServer_Click);
            // 
            // btn_SetDefault
            // 
            this.btn_SetDefault.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SetDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SetDefault.Image = global::PersonPosition.Properties.Resources.Edit;
            this.btn_SetDefault.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_SetDefault.Location = new System.Drawing.Point(139, 51);
            this.btn_SetDefault.Name = "btn_SetDefault";
            this.btn_SetDefault.Size = new System.Drawing.Size(96, 26);
            this.btn_SetDefault.TabIndex = 27;
            this.btn_SetDefault.Text = "设置为默认";
            this.btn_SetDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_SetDefault.UseVisualStyleBackColor = true;
            this.btn_SetDefault.Click += new System.EventHandler(this.btn_SetDefault_Click);
            // 
            // link_CurrentlyServer
            // 
            this.link_CurrentlyServer.AutoSize = true;
            this.link_CurrentlyServer.Location = new System.Drawing.Point(109, 11);
            this.link_CurrentlyServer.Name = "link_CurrentlyServer";
            this.link_CurrentlyServer.Size = new System.Drawing.Size(47, 12);
            this.link_CurrentlyServer.TabIndex = 29;
            this.link_CurrentlyServer.TabStop = true;
            this.link_CurrentlyServer.Text = "服务器1";
            // 
            // DialogLoginSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(242, 414);
            this.Controls.Add(this.link_CurrentlyServer);
            this.Controls.Add(this.btn_DelServer);
            this.Controls.Add(this.btn_NewServer);
            this.Controls.Add(this.btn_SetDefault);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.group_ServerInfo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.list_Server);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogLoginSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "登录参数设置";
            this.Load += new System.EventHandler(this.DialogLoginSetting_Load);
            this.group_ServerInfo.ResumeLayout(false);
            this.group_ServerInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_IP;
        private System.Windows.Forms.TextBox text_Port;
        private System.Windows.Forms.TextBox text_DBName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_DBUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox text_DBPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox list_Server;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox group_ServerInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox text_ServerTitle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btn_DelServer;
        private System.Windows.Forms.Button btn_NewServer;
        private System.Windows.Forms.Button btn_SetDefault;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.LinkLabel link_CurrentlyServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_DBIP;
    }
}