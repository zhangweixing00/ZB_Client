using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PersonPosition.Common;

namespace PersonPosition.View
{
    public partial class DialogLoginSetting : Form
    {
        private bool IsNew = false;

        public DialogLoginSetting()
        {
            InitializeComponent();
        }

        private void DialogLoginSetting_Load(object sender, EventArgs e)
        {
            //刷新服务器列表、显示默认服务器
            RefreshServerList();
        }

        private void RefreshServerList()
        {
            list_Server.Items.Clear();
            //初始化服务器列表
            string[] serverList = Global.GetServerNameList();
            for (int i = 0; i < serverList.Length; i++)
            {
                list_Server.Items.Add(serverList[i]);
            }
            link_CurrentlyServer.Text = Global.CurrentlyServer;
        }

        private void list_Server_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_Server.SelectedIndex != -1)
            {
                IsNew = false;
                string selectServerName = list_Server.Items[list_Server.SelectedIndex].ToString();
                try
                {
                    text_ServerTitle.ReadOnly = true;
                    text_ServerTitle.Text = Global.GetServerInfo(selectServerName, "ServerName");
                    text_IP.Text = Global.GetServerInfo(selectServerName, "ServerIP");
                    text_Port.Text = Global.GetServerInfo(selectServerName, "ServerPort");
                    string[] arrayList = Global.GetServerInfo(selectServerName, "ServerDBStr").Split(';');
                    if (arrayList.Length >= 4)
                    {
                        text_DBIP.Text = arrayList[0].Split('=')[1];
                        text_DBName.Text = arrayList[1].Split('=')[1];
                        text_DBUserName.Text = arrayList[2].Split('=')[1];
                        text_DBPassword.Text = arrayList[3].Split('=')[1];
                    }
                    else
                    {
                        MessageBox.Show("数据库字符串错误。请检查配置文件！", "初始化服务器详细配置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    group_ServerInfo.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n配置文件损坏，请更新配置文件后重试。","初始化服务器详细配置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                group_ServerInfo.Enabled = false;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (text_ServerTitle.Text.Trim() != "" && text_IP.Text.Trim() != "" && text_Port.Text.Trim() != "" && text_DBIP.Text.Trim() != "" && text_DBName.Text.Trim() != "" && text_DBUserName.Text.Trim() != "")
            {
                try
                {
                    if (IsNew)
                    {
                        //创建新的
                        for (int i = 0; i < list_Server.Items.Count; i++)
                        {
                            if (list_Server.Items[i].ToString() == text_ServerTitle.Text.Trim())
                            {
                                MessageBox.Show("对不起，“" + text_ServerTitle.Text.Trim() + "”这个服务器标题已经使用过了，请更换一个尚未使用过的名称。", "新建服务器", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        if (Global.CreateServerInfo(text_ServerTitle.Text.Trim(), text_IP.Text.Trim(), text_Port.Text.Trim(), "server=" + text_DBIP.Text.Trim() + ";Database=" + text_DBName.Text.Trim() + ";User ID=" + text_DBUserName.Text.Trim() + ";Password=" + text_DBPassword.Text.Trim() + ";"))
                        {
                            RefreshServerList();
                        }
                        else
                        {
                            MessageBox.Show("创建服务器失败！ 请检查配置文件。", "新建服务器", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        //修改老的
                        if (Global.CurrentlyServer == text_ServerTitle.Text.Trim())
                        {
                            //如果是默认的，则直接修改
                            Global.ServerIP = text_IP.Text.Trim();
                            Global.ServerPort = Convert.ToInt32(text_Port.Text.Trim());
                            Global.ServerDBStr = "server=" + text_DBIP.Text.Trim() + ";Database=" + text_DBName.Text.Trim() + ";User ID=" + text_DBUserName.Text.Trim() + ";Password=" + text_DBPassword.Text.Trim() + ";";
                        }
                        else
                        {
                            //如果不是默认的，则把欲修改的设为默认的，然后修改，最后再还原真正默认的。
                            string OldCurrentlyServer = Global.CurrentlyServer;
                            string OldServerIP = Global.ServerIP;
                            int OldServerPort = Global.ServerPort;
                            string OldServerDBStr = Global.ServerDBStr;
                            //借助系统变量修改配置文件。
                            Global.CurrentlyServer = text_ServerTitle.Text.Trim();
                            Global.ServerIP = text_IP.Text.Trim();
                            Global.ServerPort = Convert.ToInt32(text_Port.Text.Trim());
                            Global.ServerDBStr = "server=" + text_DBIP.Text.Trim() + ";Database=" + text_DBName.Text.Trim() + ";User ID=" + text_DBUserName.Text.Trim() + ";Password=" + text_DBPassword.Text.Trim() + ";";
                            //还原当前服务器信息。
                            Global.CurrentlyServer = OldCurrentlyServer;
                            Global.ServerIP = OldServerIP;
                            Global.ServerPort = OldServerPort;
                            Global.ServerDBStr = OldServerDBStr;
                        }
                    }
                    ClearAllInfo();
                    IsNew = false;
                    group_ServerInfo.Enabled = false;
                    MessageBox.Show("服务器配置信息保存成功！", "服务器详细配置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("请输入完整的服务器信息后再进行保存。", "服务器详细配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void text_Port_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void ClearAllInfo()
        {
            text_ServerTitle.Text = "新建服务器";
            text_IP.Text = "*.*.*.*";
            text_Port.Text = "7898";
            text_DBIP.Text = @"*.*.*.*\SQLEXPRESS";
            text_DBName.Text = "PersonPosition";
            text_DBUserName.Text = "sa";
            text_DBPassword.Text = "123456"; 
        }

        private void btn_NewServer_Click(object sender, EventArgs e)
        {
            ClearAllInfo();
            IsNew = true;
            text_ServerTitle.ReadOnly = false;
            group_ServerInfo.Enabled = true;
        }

        private void btn_DelServer_Click(object sender, EventArgs e)
        {
            if (list_Server.SelectedIndex != -1)
            {
                string DelServerName = list_Server.Items[list_Server.SelectedIndex].ToString();
                if (MessageBox.Show("您确定要删除“" + DelServerName + "”这个服务器项吗？", "删除服务器", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Global.DelServer(DelServerName))
                    {
                        ClearAllInfo();
                        IsNew = false;
                        group_ServerInfo.Enabled = false;
                        RefreshServerList();
                        MessageBox.Show("删除服务器成功！", "删除服务器", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("删除服务器失败！请检查配置文件。", "删除服务器", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择一个服务器项", "设置默认连接服务器", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_SetDefault_Click(object sender, EventArgs e)
        {
            if (list_Server.SelectedIndex != -1)
            {
                ClearAllInfo();
                IsNew = false;
                group_ServerInfo.Enabled = false;
                Global.CurrentlyServer = list_Server.Items[list_Server.SelectedIndex].ToString();
                link_CurrentlyServer.Text = list_Server.Items[list_Server.SelectedIndex].ToString();
                MessageBox.Show("设置默认连接服务器成功！", "设置默认连接服务器", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("请先选择一个服务器项", "设置默认连接服务器", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}