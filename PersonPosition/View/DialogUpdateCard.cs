using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PersonPosition.StaticService;

namespace PersonPosition.View
{
    public partial class DialogUpdateCard : Form
    {
        bool IsUpdateCard = false;

        public DialogUpdateCard()
        {
            InitializeComponent();
            for (int i = 0; i < DB_Service.MainDataSet.Tables["CardTypeTable"].Rows.Count; i++)
            {
                com_CardType.Items.Add(DB_Service.MainDataSet.Tables["CardTypeTable"].Rows[i]["CardType"].ToString());
            }
            com_CardType.SelectedIndex = 0;
        }

        /// <summary>
        /// 带初始化默认卡片的构造函数
        /// </summary>
        public DialogUpdateCard(int CardID,string CardType)
        {
            InitializeComponent();
            for (int i = 0; i < DB_Service.MainDataSet.Tables["CardTypeTable"].Rows.Count; i++)
            {
                com_CardType.Items.Add(DB_Service.MainDataSet.Tables["CardTypeTable"].Rows[i]["CardType"].ToString());
            }
            this.IsUpdateCard = true;
            this.Text = "修改卡片类型";
            this.tex_CardID.Enabled = false;
            this.label_Tip.Visible = false;
            this.tex_CardID.Text = CardID.ToString();
            this.com_CardType.Text = CardType;
            radio_AddOne.Visible = false;
            radio_AddMore.Visible = false;
        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {
            if (IsUpdateCard)
            {
                //修改卡片类型
                DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + this.tex_CardID.Text);
                if (rows.Length > 0)
                {
                    rows[0]["CardType"] = com_CardType.Text;

                    //将CardTable中的更新提交至数据库
                    if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["CardTable"]) == 0)
                    {
                        MessageBox.Show("修改卡片失败！\n请确保数据库连接正确。", "修改卡片");
                    }
                }
                else
                {
                    MessageBox.Show("对不起，您欲修改的卡片不存在。", "修改卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            else
            {
                //新建卡片
                try
                {
                    if (radio_AddOne.Checked)
                    {
                        //单张添加
                        if (tex_CardID.Text.Trim() != "")
                        {
                            int cardID = Convert.ToInt32(tex_CardID.Text.Trim());
                            DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + cardID);
                            if (rows.Length == 0)
                            {
                                DataRow newRow = DB_Service.MainDataSet.Tables["CardTable"].NewRow();
                                newRow["CardID"] = cardID;
                                newRow["CardType"] = com_CardType.Text;
                                DB_Service.MainDataSet.Tables["CardTable"].Rows.Add(newRow);

                                //将CardTable中的更新提交至数据库
                                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["CardTable"]) == 0)
                                {
                                    MessageBox.Show("新建卡片失败！\n请确保数据库连接正确。", "新建卡片");
                                }
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("对不起，这个卡片编号已经使用了。请核实一个正确的卡片序号再进行新建。", "新建卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tex_CardID.Text = "";
                            }
                        }
                    }
                    else
                    {
                        //批量添加
                        if (text_AddMoreStart.Text.Trim() != "" && text_AddMoreEnd.Text.Trim() != "")
                        {
                            int cardID_Start = Convert.ToInt32(text_AddMoreStart.Text.Trim());
                            int cardID_End = Convert.ToInt32(text_AddMoreEnd.Text.Trim());
                            if (cardID_Start <= cardID_End)
                            {
                                int TotalNew = 0;
                                for (int seekCardID = cardID_Start; seekCardID <= cardID_End; seekCardID++)
                                {
                                    DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + seekCardID);
                                    if (rows.Length == 0)
                                    {
                                        DataRow newRow = DB_Service.MainDataSet.Tables["CardTable"].NewRow();
                                        newRow["CardID"] = seekCardID;
                                        newRow["CardType"] = com_CardType.Text;
                                        DB_Service.MainDataSet.Tables["CardTable"].Rows.Add(newRow);
                                        TotalNew++;
                                    }
                                }
                                if (TotalNew > 0)
                                {
                                    //将CardTable中的更新提交至数据库
                                    if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["CardTable"]) > 0)
                                    {
                                        MessageBox.Show("批量新建卡片成功！\n共添加了" + TotalNew + "张卡片。", "新建卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("对不起，您输入范围内的卡号都已经被占用了，请重新输入。", "新建卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    text_AddMoreStart.Text = "";
                                    text_AddMoreEnd.Text = "";
                                }
                            }
                            else
                            {
                                MessageBox.Show("对不起，您输入的起始卡号不能大于终止卡号。请重新输入。", "新建卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                text_AddMoreStart.Text = "";
                                text_AddMoreEnd.Text = "";
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("请输入正确的卡片编号。卡片编号为纯数字构成的硬件卡片序号。", "新建卡片", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tex_CardID.Text = "";
                    text_AddMoreStart.Text = "";
                    text_AddMoreEnd.Text = "";
                }
            }
        }

        private void btn_Canel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tex_CardID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btn_Sure_Click(null, null);
                return;
            }
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_AddMoreStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void text_AddMoreEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void radio_AddOne_CheckedChanged(object sender, EventArgs e)
        {
            panel_AddMore.Visible = false;
        }

        private void radio_AddMore_CheckedChanged(object sender, EventArgs e)
        {
            panel_AddMore.Visible = true;
        }
    }
}