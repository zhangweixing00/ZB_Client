namespace PersonPosition.View
{
    partial class FrmLogin
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
            this.btn_Login = new System.Windows.Forms.Button();
            this.btn_Abort = new System.Windows.Forms.Button();
            this.texUserName = new System.Windows.Forms.TextBox();
            this.texPassword = new System.Windows.Forms.TextBox();
            this.btn_Min = new System.Windows.Forms.PictureBox();
            this.btn_Close = new System.Windows.Forms.PictureBox();
            this.label_ProductVerson = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.check_RememberName = new System.Windows.Forms.CheckBox();
            this.check_RememberPW = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label_CompanyName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.com_ServerList = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Login
            // 
            this.btn_Login.BackColor = System.Drawing.Color.Transparent;
            this.btn_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Login.Image = global::PersonPosition.Properties.Resources.Sure;
            this.btn_Login.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Login.Location = new System.Drawing.Point(281, 209);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(61, 26);
            this.btn_Login.TabIndex = 2;
            this.btn_Login.Text = "登录";
            this.btn_Login.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.MouseLeave += new System.EventHandler(this.btn_Login_MouseLeave);
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            this.btn_Login.MouseEnter += new System.EventHandler(this.btn_Login_MouseEnter);
            // 
            // btn_Abort
            // 
            this.btn_Abort.BackColor = System.Drawing.Color.Transparent;
            this.btn_Abort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Abort.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Abort.Image = global::PersonPosition.Properties.Resources.Abort;
            this.btn_Abort.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Abort.Location = new System.Drawing.Point(352, 209);
            this.btn_Abort.Name = "btn_Abort";
            this.btn_Abort.Size = new System.Drawing.Size(61, 26);
            this.btn_Abort.TabIndex = 3;
            this.btn_Abort.Text = "退出";
            this.btn_Abort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Abort.UseVisualStyleBackColor = false;
            this.btn_Abort.MouseLeave += new System.EventHandler(this.btn_Abort_MouseLeave);
            this.btn_Abort.Click += new System.EventHandler(this.btn_Abort_Click);
            this.btn_Abort.MouseEnter += new System.EventHandler(this.btn_Abort_MouseEnter);
            // 
            // texUserName
            // 
            this.texUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.texUserName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.texUserName.Location = new System.Drawing.Point(161, 132);
            this.texUserName.Name = "texUserName";
            this.texUserName.Size = new System.Drawing.Size(158, 23);
            this.texUserName.TabIndex = 0;
            this.texUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.texUserName_KeyPress);
            // 
            // texPassword
            // 
            this.texPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.texPassword.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.texPassword.Location = new System.Drawing.Point(161, 165);
            this.texPassword.Name = "texPassword";
            this.texPassword.PasswordChar = '*';
            this.texPassword.Size = new System.Drawing.Size(158, 23);
            this.texPassword.TabIndex = 1;
            this.texPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.texPassword_KeyPress);
            // 
            // btn_Min
            // 
            this.btn_Min.BackColor = System.Drawing.Color.Transparent;
            this.btn_Min.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Min.Image = global::PersonPosition.Properties.Resources.Min;
            this.btn_Min.Location = new System.Drawing.Point(393, 0);
            this.btn_Min.Name = "btn_Min";
            this.btn_Min.Size = new System.Drawing.Size(19, 14);
            this.btn_Min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_Min.TabIndex = 4;
            this.btn_Min.TabStop = false;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Close.Image = global::PersonPosition.Properties.Resources.Close;
            this.btn_Close.Location = new System.Drawing.Point(415, 1);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(19, 14);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_Close.TabIndex = 3;
            this.btn_Close.TabStop = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // label_ProductVerson
            // 
            this.label_ProductVerson.AutoSize = true;
            this.label_ProductVerson.BackColor = System.Drawing.Color.Transparent;
            this.label_ProductVerson.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_ProductVerson.Location = new System.Drawing.Point(326, 255);
            this.label_ProductVerson.Name = "label_ProductVerson";
            this.label_ProductVerson.Size = new System.Drawing.Size(59, 12);
            this.label_ProductVerson.TabIndex = 15;
            this.label_ProductVerson.Text = "软件版本:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("华文彩云", 17.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(1, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(431, 31);
            this.label1.TabIndex = 18;
            this.label1.Text = "产品名";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // check_RememberName
            // 
            this.check_RememberName.AutoSize = true;
            this.check_RememberName.BackColor = System.Drawing.Color.Transparent;
            this.check_RememberName.Location = new System.Drawing.Point(163, 209);
            this.check_RememberName.Name = "check_RememberName";
            this.check_RememberName.Size = new System.Drawing.Size(84, 16);
            this.check_RememberName.TabIndex = 19;
            this.check_RememberName.Text = "记住用户名";
            this.check_RememberName.UseVisualStyleBackColor = false;
            this.check_RememberName.Visible = false;
            // 
            // check_RememberPW
            // 
            this.check_RememberPW.AutoSize = true;
            this.check_RememberPW.BackColor = System.Drawing.Color.Transparent;
            this.check_RememberPW.Location = new System.Drawing.Point(163, 230);
            this.check_RememberPW.Name = "check_RememberPW";
            this.check_RememberPW.Size = new System.Drawing.Size(72, 16);
            this.check_RememberPW.TabIndex = 20;
            this.check_RememberPW.Text = "记住密码";
            this.check_RememberPW.UseVisualStyleBackColor = false;
            this.check_RememberPW.Visible = false;
            this.check_RememberPW.CheckedChanged += new System.EventHandler(this.check_RememberPW_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.DisabledLinkColor = System.Drawing.Color.Gainsboro;
            this.linkLabel1.LinkColor = System.Drawing.Color.Gray;
            this.linkLabel1.Location = new System.Drawing.Point(322, 110);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(53, 12);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "连接参数";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label_CompanyName
            // 
            this.label_CompanyName.AutoSize = true;
            this.label_CompanyName.BackColor = System.Drawing.Color.Transparent;
            this.label_CompanyName.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_CompanyName.Location = new System.Drawing.Point(17, 255);
            this.label_CompanyName.Name = "label_CompanyName";
            this.label_CompanyName.Size = new System.Drawing.Size(53, 12);
            this.label_CompanyName.TabIndex = 22;
            this.label_CompanyName.Text = "公司名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("黑体", 13.5F);
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(88, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 23;
            this.label2.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("黑体", 13.5F);
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(88, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 24;
            this.label3.Text = "密  码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("黑体", 13.5F);
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(88, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 25;
            this.label4.Text = "服务器：";
            // 
            // com_ServerList
            // 
            this.com_ServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_ServerList.Font = new System.Drawing.Font("宋体", 10.5F);
            this.com_ServerList.FormattingEnabled = true;
            this.com_ServerList.Location = new System.Drawing.Point(161, 101);
            this.com_ServerList.Name = "com_ServerList";
            this.com_ServerList.Size = new System.Drawing.Size(158, 22);
            this.com_ServerList.TabIndex = 4;
            this.com_ServerList.SelectedIndexChanged += new System.EventHandler(this.com_ServerList_SelectedIndexChanged);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = global::PersonPosition.Properties.Resources.Login1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(435, 280);
            this.Controls.Add(this.com_ServerList);
            this.Controls.Add(this.label_CompanyName);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.check_RememberPW);
            this.Controls.Add(this.check_RememberName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_ProductVerson);
            this.Controls.Add(this.texPassword);
            this.Controls.Add(this.texUserName);
            this.Controls.Add(this.btn_Abort);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.btn_Min);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btn_Min;
        private System.Windows.Forms.PictureBox btn_Close;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_Abort;
        private System.Windows.Forms.TextBox texUserName;
        private System.Windows.Forms.TextBox texPassword;
        private System.Windows.Forms.Label label_ProductVerson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox check_RememberName;
        private System.Windows.Forms.CheckBox check_RememberPW;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label_CompanyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox com_ServerList;
    }
}