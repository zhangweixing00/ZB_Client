using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using PersonPosition.StaticService;
using PersonPosition.Common;

namespace PersonPosition.View
{
    public partial class FrmLogin : Form
    {
        private Point mouse_offset;
        private Point mousePos;

        #region 窗体的鼠标移动事件、最小、最大、和关闭按钮的鼠标移动事件

        private void btn_Close_Click(object sender, EventArgs e)
        {
            btn_Abort_Click(sender, e);
        }

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouse_offset = e.Location;
            }
        }

        private void FrmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mousePos = Control.MousePosition;
                mousePos.Offset(-mouse_offset.X, -mouse_offset.Y);
                this.Location = mousePos;
            }
        }

        private void btn_Login_MouseEnter(object sender, EventArgs e)
        {
            btn_Login.BackColor = Color.CornflowerBlue;
        }

        private void btn_Login_MouseLeave(object sender, EventArgs e)
        {
            btn_Login.BackColor = Color.Transparent;
        }

        private void btn_Abort_MouseEnter(object sender, EventArgs e)
        {
            btn_Abort.BackColor = Color.CornflowerBlue;
        }

        private void btn_Abort_MouseLeave(object sender, EventArgs e)
        {
            btn_Abort.BackColor = Color.Transparent;
        }

        #endregion

        public FrmLogin()
        {
            InitializeComponent();
            this.Tag = false;
            //label1.Text = Application.ProductName + (Global.IsTempVersion ? "(演示版)" : "");
            //label_CompanyName.Text = Application.CompanyName;
            label1.Text = Global.Product + (Global.IsTempVersion ? "(演示版)" : "");
            label_CompanyName.Text = Global.Company;
            label_ProductVerson.Text = "软件版本：" + Application.ProductVersion;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //初始化服务器列表、设置默认服务器项
            RefreshServerList();

            if (Global.AutoLogin)
            {
                texUserName.Text = Global.AutoLoginUser;
                btn_Login_Click(sender, e);
            }
        }

        //初始化服务器列表、设置默认服务器项
        private void RefreshServerList()
        {
            com_ServerList.Items.Clear();
            string[] serverList = Global.GetServerNameList();
            for (int i = 0; i < serverList.Length; i++)
            {
                com_ServerList.Items.Add(serverList[i]);
            }
            com_ServerList.Text = Global.CurrentlyServer;

            if (com_ServerList.Text == "")
                Global.CurrentlyServer = "";
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (com_ServerList.Text == "")
            {
                MessageBox.Show("对不起，请先选择连接服务器后再登录。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (texUserName.Text.Trim() == "")
            {
                MessageBox.Show("请输入登录用户名。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //初始化全局数据库缓存MainDataSet
                DB_Service.InitMainDataSet();
                DataRow[] rows = DB_Service.MainDataSet.Tables["UserTable"].Select("UserName = '" + texUserName.Text + "'");
                if (rows.Length == 1)
                {
                    if (rows[0]["Password"].ToString().CompareTo(texPassword.Text) == 0 || Global.AutoLogin)
                    {
                        if (Convert.ToBoolean(rows[0]["IsAlive"]))
                        {
                            if (Socket_Service.ConnectServer(Global.ServerIP, Global.ServerPort))
                            {
                                if (Socket_Service.SendMessage_Safe(Socket_Service.Command_C2S_Reg, texUserName.Text.Trim(), "", "", "", "", "", "", "",""))
                                {
                                    this.Tag = true;
                                    Global.PresentUser = texUserName.Text.Trim();
                                    this.Close();
                                }
                                else
                                {
                                    try
                                    {
                                        Socket_Service.DisconnectServer();
                                    }
                                    catch
                                    { }
                                    MessageBox.Show("对不起，您的帐户已经在别处登陆或者连接数目超限，请联系管理员。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("与服务器的连接失败，请确保服务器已经启动后重新运行。", "服务器通信失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("对不起，您的帐户已停用，请联系管理员。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        return;
                    }
                    else
                    {
                        MessageBox.Show("请输入正确的用户名和密码。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        texPassword.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("请输入正确的用户名和密码。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    texPassword.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName + (Global.IsTempVersion ? "(演示版)" : ""));
            }
        }

        private void btn_Abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void texUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btn_Login_Click(sender, e);
            }
        }

        private void texPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btn_Login_Click(sender, e);
            }
        }

        private void check_RememberPW_CheckedChanged(object sender, EventArgs e)
        {
            if (check_RememberPW.Checked)
            {
                if (DialogResult.No == MessageBox.Show("您确定要记住密码吗？这样做有一定的安全风险。", "记住密码", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    check_RememberPW.Checked = false;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogLoginSetting loginSetting = new DialogLoginSetting();
            loginSetting.ShowDialog(this);
            RefreshServerList();
        }

        private void com_ServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Global.CurrentlyServer != com_ServerList.Text)
                Global.CurrentlyServer = com_ServerList.Text;
        }
    }
}