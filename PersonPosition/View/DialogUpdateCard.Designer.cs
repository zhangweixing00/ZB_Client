namespace PersonPosition.View
{
    partial class DialogUpdateCard
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
            this.btn_Sure = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.com_CardType = new System.Windows.Forms.ComboBox();
            this.tex_CardID = new System.Windows.Forms.TextBox();
            this.btn_Canel = new System.Windows.Forms.Button();
            this.label_Tip = new System.Windows.Forms.Label();
            this.radio_AddOne = new System.Windows.Forms.RadioButton();
            this.radio_AddMore = new System.Windows.Forms.RadioButton();
            this.panel_AddMore = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.text_AddMoreStart = new System.Windows.Forms.TextBox();
            this.text_AddMoreEnd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel_AddMore.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(174, 118);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(75, 26);
            this.btn_Sure.TabIndex = 2;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "卡片编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "卡片类型：";
            // 
            // com_CardType
            // 
            this.com_CardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_CardType.FormattingEnabled = true;
            this.com_CardType.Location = new System.Drawing.Point(76, 67);
            this.com_CardType.Name = "com_CardType";
            this.com_CardType.Size = new System.Drawing.Size(100, 20);
            this.com_CardType.TabIndex = 1;
            // 
            // tex_CardID
            // 
            this.tex_CardID.Location = new System.Drawing.Point(76, 34);
            this.tex_CardID.Name = "tex_CardID";
            this.tex_CardID.Size = new System.Drawing.Size(100, 21);
            this.tex_CardID.TabIndex = 0;
            this.tex_CardID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tex_CardID_KeyPress);
            // 
            // btn_Canel
            // 
            this.btn_Canel.Location = new System.Drawing.Point(257, 118);
            this.btn_Canel.Name = "btn_Canel";
            this.btn_Canel.Size = new System.Drawing.Size(75, 26);
            this.btn_Canel.TabIndex = 3;
            this.btn_Canel.Text = "取消";
            this.btn_Canel.UseVisualStyleBackColor = true;
            this.btn_Canel.Click += new System.EventHandler(this.btn_Canel_Click);
            // 
            // label_Tip
            // 
            this.label_Tip.AutoSize = true;
            this.label_Tip.ForeColor = System.Drawing.Color.Black;
            this.label_Tip.Location = new System.Drawing.Point(181, 38);
            this.label_Tip.Name = "label_Tip";
            this.label_Tip.Size = new System.Drawing.Size(137, 12);
            this.label_Tip.TabIndex = 6;
            this.label_Tip.Text = "(请与输入卡片上的编号)";
            // 
            // radio_AddOne
            // 
            this.radio_AddOne.AutoSize = true;
            this.radio_AddOne.Checked = true;
            this.radio_AddOne.Location = new System.Drawing.Point(9, 8);
            this.radio_AddOne.Name = "radio_AddOne";
            this.radio_AddOne.Size = new System.Drawing.Size(71, 16);
            this.radio_AddOne.TabIndex = 7;
            this.radio_AddOne.TabStop = true;
            this.radio_AddOne.Text = "单张添加";
            this.radio_AddOne.UseVisualStyleBackColor = true;
            this.radio_AddOne.CheckedChanged += new System.EventHandler(this.radio_AddOne_CheckedChanged);
            // 
            // radio_AddMore
            // 
            this.radio_AddMore.AutoSize = true;
            this.radio_AddMore.Location = new System.Drawing.Point(87, 8);
            this.radio_AddMore.Name = "radio_AddMore";
            this.radio_AddMore.Size = new System.Drawing.Size(71, 16);
            this.radio_AddMore.TabIndex = 8;
            this.radio_AddMore.Text = "批量添加";
            this.radio_AddMore.UseVisualStyleBackColor = true;
            this.radio_AddMore.CheckedChanged += new System.EventHandler(this.radio_AddMore_CheckedChanged);
            // 
            // panel_AddMore
            // 
            this.panel_AddMore.Controls.Add(this.text_AddMoreEnd);
            this.panel_AddMore.Controls.Add(this.text_AddMoreStart);
            this.panel_AddMore.Controls.Add(this.label4);
            this.panel_AddMore.Controls.Add(this.label3);
            this.panel_AddMore.Controls.Add(this.label5);
            this.panel_AddMore.Location = new System.Drawing.Point(1, 26);
            this.panel_AddMore.Name = "panel_AddMore";
            this.panel_AddMore.Size = new System.Drawing.Size(335, 33);
            this.panel_AddMore.TabIndex = 9;
            this.panel_AddMore.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "卡号从：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "至";
            // 
            // text_AddMoreStart
            // 
            this.text_AddMoreStart.Location = new System.Drawing.Point(60, 7);
            this.text_AddMoreStart.Name = "text_AddMoreStart";
            this.text_AddMoreStart.Size = new System.Drawing.Size(43, 21);
            this.text_AddMoreStart.TabIndex = 12;
            this.text_AddMoreStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_AddMoreStart_KeyPress);
            // 
            // text_AddMoreEnd
            // 
            this.text_AddMoreEnd.Location = new System.Drawing.Point(127, 7);
            this.text_AddMoreEnd.Name = "text_AddMoreEnd";
            this.text_AddMoreEnd.Size = new System.Drawing.Size(43, 21);
            this.text_AddMoreEnd.TabIndex = 13;
            this.text_AddMoreEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_AddMoreEnd_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(170, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "(若遇到已占用卡号，则跳过)";
            // 
            // DialogUpdateCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 150);
            this.Controls.Add(this.panel_AddMore);
            this.Controls.Add(this.radio_AddMore);
            this.Controls.Add(this.radio_AddOne);
            this.Controls.Add(this.label_Tip);
            this.Controls.Add(this.btn_Canel);
            this.Controls.Add(this.tex_CardID);
            this.Controls.Add(this.com_CardType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Sure);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogUpdateCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建卡片";
            this.panel_AddMore.ResumeLayout(false);
            this.panel_AddMore.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox com_CardType;
        private System.Windows.Forms.TextBox tex_CardID;
        private System.Windows.Forms.Button btn_Canel;
        private System.Windows.Forms.Label label_Tip;
        private System.Windows.Forms.RadioButton radio_AddOne;
        private System.Windows.Forms.RadioButton radio_AddMore;
        private System.Windows.Forms.Panel panel_AddMore;
        private System.Windows.Forms.TextBox text_AddMoreEnd;
        private System.Windows.Forms.TextBox text_AddMoreStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}