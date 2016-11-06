using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PersonPosition.Common;
using PersonPosition.StaticService;

namespace PersonPosition.View
{
    public partial class FrmLED_Setting : Form
    {
        private FrmLED frmLED;
        private string _HengLineKeyStr = "";
        private string _ShuLineKeyStr = "";
        private Color _BasicTextColor;
        private Color _AdvLineColor;
        private int AdvTextNum = 0;
        private bool IsSystemClick = false;
        private Font _BasicFont;

        public FrmLED_Setting(FrmLED _frmLED)
        {
            InitializeComponent();

            frmLED = _frmLED;

            //初始化界面显示
            text_BasicTitle.Text = Global.LEDBasicTitle;
            _BasicTextColor = Global.LEDBasicTextColor;
            _AdvLineColor = Global.LEDAdvLineColor;
            com_LoopHuman.Text = Global.LEDTextLoopTime.ToString();
            textBox1.Text = Global.LEDWidth.ToString();
            textBox2.Text = Global.LEDHeight.ToString();
            check_TopMost.Checked = Global.LEDTopMost;
            _BasicFont = Global.LEDBasicFont;
            if (Global.LEDIsAreaInMineNum)
            {
                radio_InMineNumArea.Checked = true;
            }
            else
            {
                radio_InMineNumTotal.Checked = true;
            }
            if (Global.LEDIsAdvShow)
            {
                check_Adv.Checked = true;
                //因为在IDE时就为：check_Adv.Checked = true。所以为了触发初始化，手动调用一次。
                check_Adv_CheckedChanged(null, null);
            }
            else
            {
                check_Adv.Checked = false;
            }
            //刷新所有的采集器通道信息列表和已插入的列表
            btn_RefreshCollect_Click(null, null);
        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            frmLED.InitShow(_BasicTextColor,this.text_AdvText.ForeColor,_AdvLineColor, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Global.LEDTop, Global.LEDLeft, Convert.ToBoolean(check_TopMost.Checked),text_BasicTitle.Text,radio_InMineNumArea.Checked, Convert.ToInt32(com_LoopHuman.Text), check_Adv.Checked, _HengLineKeyStr, _ShuLineKeyStr, text_AdvText.Text, _BasicFont, text_AdvText.Font);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Global.LEDBasicTitle = text_BasicTitle.Text;
                Global.LEDBasicTextColor = _BasicTextColor;
                Global.LEDAdvTextColor = text_AdvText.ForeColor;
                Global.LEDAdvLineColor = _AdvLineColor;
                Global.LEDTextLoopTime = Convert.ToInt32(com_LoopHuman.Text);
                Global.LEDWidth = Convert.ToInt32(textBox1.Text);
                Global.LEDHeight = Convert.ToInt32(textBox2.Text);
                Global.LEDTopMost = Convert.ToBoolean(check_TopMost.Checked);
                Global.LEDBasicFont = _BasicFont;
                Global.LEDIsAdvShow = check_Adv.Checked;
                Global.LEDHengLineKeyStr = _HengLineKeyStr;
                Global.LEDShuLineKeyStr = _ShuLineKeyStr;
                Global.LEDAdvText = text_AdvText.Text;
                Global.LEDAdvFont = text_AdvText.Font;
                Global.LEDIsAreaInMineNum = radio_InMineNumArea.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void check_Adv_CheckedChanged(object sender, EventArgs e)
        {
            if (check_Adv.Checked)
            {
                text_AdvText.ForeColor = Global.LEDAdvTextColor;
                text_AdvText.Text = Global.LEDAdvText;
                text_AdvText.Font = Global.LEDAdvFont;
                AdvTextNum = text_AdvText.Text.Length;
                _HengLineKeyStr = Global.LEDHengLineKeyStr;
                _ShuLineKeyStr = Global.LEDShuLineKeyStr;
                group_Adv.Visible = true;
                this.Height = 644;
                //初始化横线参数
                IsSystemClick = true;
                string[] HengList = _HengLineKeyStr.Split(',');
                if (HengList[0] == "")
                {
                    com_HengNum.SelectedIndex = 0;
                }
                else
                {
                    com_HengNum.SelectedIndex = HengList.Length;
                }
                //初始化竖线参数
                string[] ShuList = _ShuLineKeyStr.Split(',');
                if (ShuList[0] == "")
                {
                    com_ShuNum.SelectedIndex = 0;
                }
                else
                {
                    com_ShuNum.SelectedIndex = ShuList.Length;
                }
                IsSystemClick = false;

                this.Top = Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2;
            }
            else
            {
                group_Adv.Visible = false;
                this.Height = 230;
                text_AdvText.ForeColor = Color.Red;
                _HengLineKeyStr = "";
                _ShuLineKeyStr = "";
                text_AdvText.Text = "";
                text_AdvText.Font = new Font("宋体", 9);
            }
        }

        private void btn_Abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLED_Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.frmLED.InitShow(Global.LEDBasicTextColor,Global.LEDAdvTextColor,Global.LEDAdvLineColor, Global.LEDWidth, Global.LEDHeight, Global.LEDTop, Global.LEDLeft, Global.LEDTopMost,Global.LEDBasicTitle,Global.LEDIsAreaInMineNum, Global.LEDTextLoopTime, Global.LEDIsAdvShow, Global.LEDHengLineKeyStr, Global.LEDShuLineKeyStr, Global.LEDAdvText,Global.LEDBasicFont, Global.LEDAdvFont);
        }

        private void dataGV_Table_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btn_RefreshCollect_Click(object sender, EventArgs e)
        {
            //刷新所有的采集器通道信息列表
            listView_AllCollect.Items.Clear();
            try
            {
                for (int i = 0; i < DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Rows.Count; i++)
                {
                    ListViewItem newItem = new ListViewItem(new string[3] { DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Rows[i]["ChannelName"].ToString(), DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Rows[i]["StationID"].ToString(), DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Rows[i]["ChannelNum"].ToString() });
                    listView_AllCollect.Items.Add(newItem);
                }
            }
            catch
            {  }
        }

        private void btn_InsertCollect_Click(object sender, EventArgs e)
        {
            if (listView_AllCollect.SelectedItems.Count > 0)
            {
                string temp_Info = listView_AllCollect.SelectedItems[0].SubItems[1].Text.Trim() + "$" + listView_AllCollect.SelectedItems[0].SubItems[2].Text.Trim();
                if (temp_Info.Length <= 6)
                {
                    if (temp_Info.Length < 6)
                        temp_Info = temp_Info.PadRight(6, ' ');
                    text_AdvText.Text = text_AdvText.Text.Insert(text_AdvText.SelectionStart, "&" + temp_Info + "&");      
                }
                else
                {
                    MessageBox.Show("对不起，添加失败。原因：基站ID长度或者通道号长度超限。", "插入采集信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("对不起，请先在下边的列表中选择一个欲插入的采集信息。", "插入采集信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void text_AdvText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '&' || e.KeyChar == '$')
            {
                e.Handled = true;
            }
        }

        private void text_AdvText_MouseDown(object sender, MouseEventArgs e)
        {
            label_AdvTextSeek.Text = text_AdvText.SelectionStart.ToString();
        }

        private void text_AdvText_MouseUp(object sender, MouseEventArgs e)
        {
            label_AdvTextSeek.Text = text_AdvText.SelectionStart.ToString();
            AdvTextNum = text_AdvText.Text.Length;
        }

        private void text_AdvText_KeyUp(object sender, KeyEventArgs e)
        {
            label_AdvTextSeek.Text = text_AdvText.SelectionStart.ToString();
        }

        private void text_AdvText_TextChanged(object sender, EventArgs e)
        {
            if (text_AdvText.SelectionStart > 0 && AdvTextNum < text_AdvText.Text.Length)
            {
                if (text_AdvText.Text.Substring(text_AdvText.SelectionStart - 1, 1) == "&" || text_AdvText.Text.Substring(text_AdvText.SelectionStart - 1, 1) == "$")
                {
                    text_AdvText.Text = text_AdvText.Text.Remove(text_AdvText.SelectionStart - 1, 1);
                    MessageBox.Show("对不起，请勿输入 & 与 $ 符号。", "LED参数设置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void com_HengNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemClick)
            {
                _HengLineKeyStr = "";
                for (int i = 0; i < com_HengNum.SelectedIndex; i++)
                {
                    _HengLineKeyStr += "0,";
                }
                if (_HengLineKeyStr.Length > 0)
                    _HengLineKeyStr = _HengLineKeyStr.Substring(0, _HengLineKeyStr.Length - 1);
            }
            com_HengChangeIndex.Items.Clear();
            label_HengChangeValue.Text = "0";
            if (com_HengNum.SelectedIndex > 0)
            {
                panel_HengChange.Visible = true;
                for (int i = 0; i < com_HengNum.SelectedIndex; i++)
                {
                    com_HengChangeIndex.Items.Add(Convert.ToString(i + 1));
                }
                com_HengChangeIndex.SelectedIndex = 0;
            }
            else
            {
                panel_HengChange.Visible = false;
            }
        }

        private void com_ShuNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSystemClick)
            {
                _ShuLineKeyStr = "";
                for (int i = 0; i < com_ShuNum.SelectedIndex; i++)
                {
                    _ShuLineKeyStr += "0,";
                }
                if (_ShuLineKeyStr.Length > 0)
                    _ShuLineKeyStr = _ShuLineKeyStr.Substring(0, _ShuLineKeyStr.Length - 1);
            }
            com_ShuChangeIndex.Items.Clear();
            label_ShuChangeValue.Text = "0";
            if (com_ShuNum.SelectedIndex > 0)
            {
                panel_ShuChange.Visible = true;
                for (int i = 0; i < com_ShuNum.SelectedIndex; i++)
                {
                    com_ShuChangeIndex.Items.Add(Convert.ToString(i + 1));
                }
                com_ShuChangeIndex.SelectedIndex = 0;
            }
            else
            {
                panel_ShuChange.Visible = false;
            }
        }

        private string GetHengLineKeyStr()
        {
            return "";
        }

        private void btn_UpLine_Click(object sender, EventArgs e)
        {
            if (com_HengChangeIndex.SelectedIndex != -1)
            {
                string[] tempHengLineList = _HengLineKeyStr.Split(',');
                if (tempHengLineList[com_HengChangeIndex.SelectedIndex] != null && tempHengLineList[com_HengChangeIndex.SelectedIndex] != "")
                {
                    int tempValue = Convert.ToInt32(tempHengLineList[com_HengChangeIndex.SelectedIndex]);
                    tempValue -= 1;
                    tempHengLineList[com_HengChangeIndex.SelectedIndex] = tempValue.ToString();
                    _HengLineKeyStr = "";
                    for (int i = 0; i < tempHengLineList.Length; i++)
                    {
                        _HengLineKeyStr += tempHengLineList[i] + ",";
                    }
                    _HengLineKeyStr = _HengLineKeyStr.Substring(0, _HengLineKeyStr.Length - 1);
                    label_HengChangeValue.Text = tempValue.ToString();
                }
            }
        }

        private void btn_DownLine_Click(object sender, EventArgs e)
        {
            if (com_HengChangeIndex.SelectedIndex != -1)
            {
                string[] tempHengLineList = _HengLineKeyStr.Split(',');
                if (tempHengLineList[com_HengChangeIndex.SelectedIndex] != null && tempHengLineList[com_HengChangeIndex.SelectedIndex] != "")
                {
                    int tempValue = Convert.ToInt32(tempHengLineList[com_HengChangeIndex.SelectedIndex]);
                    tempValue += 1;
                    tempHengLineList[com_HengChangeIndex.SelectedIndex] = tempValue.ToString();
                    _HengLineKeyStr = "";
                    for (int i = 0; i < tempHengLineList.Length; i++)
                    {
                        _HengLineKeyStr += tempHengLineList[i] + ",";
                    }
                    _HengLineKeyStr = _HengLineKeyStr.Substring(0, _HengLineKeyStr.Length - 1);
                    label_HengChangeValue.Text = tempValue.ToString();
                }
            }
        }

        private void btn_LeftLine_Click(object sender, EventArgs e)
        {
            if (com_ShuChangeIndex.SelectedIndex != -1)
            {
                string[] tempShuLineList = _ShuLineKeyStr.Split(',');
                if (tempShuLineList[com_ShuChangeIndex.SelectedIndex] != null && tempShuLineList[com_ShuChangeIndex.SelectedIndex] != "")
                {
                    int tempValue = Convert.ToInt32(tempShuLineList[com_ShuChangeIndex.SelectedIndex]);
                    tempValue -= 1;
                    tempShuLineList[com_ShuChangeIndex.SelectedIndex] = tempValue.ToString();
                    _ShuLineKeyStr = "";
                    for (int i = 0; i < tempShuLineList.Length; i++)
                    {
                        _ShuLineKeyStr += tempShuLineList[i] + ",";
                    }
                    _ShuLineKeyStr = _ShuLineKeyStr.Substring(0, _ShuLineKeyStr.Length - 1);
                    label_ShuChangeValue.Text = tempValue.ToString();
                }
            }
        }

        private void btn_RightLine_Click(object sender, EventArgs e)
        {
            if (com_ShuChangeIndex.SelectedIndex != -1)
            {
                string[] tempShuLineList = _ShuLineKeyStr.Split(',');
                if (tempShuLineList[com_ShuChangeIndex.SelectedIndex] != null && tempShuLineList[com_ShuChangeIndex.SelectedIndex] != "")
                {
                    int tempValue = Convert.ToInt32(tempShuLineList[com_ShuChangeIndex.SelectedIndex]);
                    tempValue += 1;
                    tempShuLineList[com_ShuChangeIndex.SelectedIndex] = tempValue.ToString();
                    _ShuLineKeyStr = "";
                    for (int i = 0; i < tempShuLineList.Length; i++)
                    {
                        _ShuLineKeyStr += tempShuLineList[i] + ",";
                    }
                    _ShuLineKeyStr = _ShuLineKeyStr.Substring(0, _ShuLineKeyStr.Length - 1);
                    label_ShuChangeValue.Text = tempValue.ToString();
                }
            }
        }

        private void com_HengChangeIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_HengChangeIndex.SelectedIndex != -1)
            {
                string[] tempHengLineList = _HengLineKeyStr.Split(',');
                if (tempHengLineList[com_HengChangeIndex.SelectedIndex] != null && tempHengLineList[com_HengChangeIndex.SelectedIndex] != "")
                {
                    label_HengChangeValue.Text = tempHengLineList[com_HengChangeIndex.SelectedIndex];
                }
            }
        }

        private void com_ShuChangeIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_ShuChangeIndex.SelectedIndex != -1)
            {
                string[] tempShuLineList = _ShuLineKeyStr.Split(',');
                if (tempShuLineList[com_ShuChangeIndex.SelectedIndex] != null && tempShuLineList[com_ShuChangeIndex.SelectedIndex] != "")
                {
                    label_ShuChangeValue.Text = tempShuLineList[com_ShuChangeIndex.SelectedIndex];
                }
            }
        }

        private void btn_BasicFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = _BasicFont;
            fontDialog1.ShowDialog(this);
            //统一将字体显示成为整数大小字号的字
            int FontSizeInt = Convert.ToInt32(fontDialog1.Font.Size);
            if (FontSizeInt < 6)
            {
                MessageBox.Show("对不起，您选择的字体不能小于6号。请重新选择。", "设置LED字体", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _BasicFont = new Font(fontDialog1.Font.FontFamily, FontSizeInt);
            }
        }

        private void btn_AdvFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = text_AdvText.Font;
            fontDialog1.ShowDialog(this);
            text_AdvText.Font = fontDialog1.Font;
        }

        private void btn_AdvLineColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = _AdvLineColor;
            colorDialog1.ShowDialog(this);
            _AdvLineColor = colorDialog1.Color;
        }

        private void btn_AdvTextColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = text_AdvText.ForeColor;
            colorDialog1.ShowDialog(this);
            text_AdvText.ForeColor = colorDialog1.Color;
        }

        private void btn_BasicTextColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = _BasicTextColor;
            colorDialog1.ShowDialog(this);
            _BasicTextColor = colorDialog1.Color;
        }
    }
}