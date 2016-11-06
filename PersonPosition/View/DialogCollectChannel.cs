using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PersonPosition.StaticService;

namespace PersonPosition.View
{
    public partial class DialogCollectChannel : Form
    {
        private int MaxChannelNum;
        private Dictionary<int, int> UseChannelList = new Dictionary<int, int>();
        private DataRow channelRow = null;
        private DialogStation frmDialogStation = null;

        public DialogCollectChannel(int _maxChannelNum, Dictionary<int, int> _useChannelList, int _selectChannel, int _selectChannelID)
        {
            InitializeComponent();
            this.MaxChannelNum = _maxChannelNum;
            this.UseChannelList = _useChannelList;
            //刷新可用的通道列表
            RefreshCanUseChannel();
            //修改通道
            this.Text = "修改采集器通道";
            textBox_Channel.Text = _selectChannel.ToString();
            this.Width = 200;
            this.listview_CanUseChannel.Visible = false;
            channelRow = DB_Service.MainDataSet.Tables["CollectChannelTable"].Select("Channel_ID = " + _selectChannelID)[0];

            text_Name.Text = channelRow["ChannelName"].ToString();
            com_ChannelType.Text = channelRow["ChannelType"].ToString();
            textBox_PerK.Text = channelRow["ChannelPer_K"].ToString();
            textBox_PerC.Text = channelRow["ChannelPer_C"].ToString();
            textBox_ChannelComment.Text = channelRow["ChannelComment"].ToString();
            textBox_Unit.Text = channelRow["ChannelUnit"].ToString();
            textBox_ValueMax.Text = channelRow["ChannelValue_Max"].ToString();
            textBox_ValueMin.Text = channelRow["ChannelValue_Min"].ToString();
        }

        public DialogCollectChannel(DialogStation _frmDialogStation, int _maxChannelNum, Dictionary<int, int> _useChannelList)
        {
            InitializeComponent();
            this.frmDialogStation = _frmDialogStation;
            this.MaxChannelNum = _maxChannelNum;
            this.UseChannelList = _useChannelList;

            //新建通道
            this.Text = "添加采集器通道";
            //刷新可用的通道列表
            RefreshCanUseChannel();
            //将列表的第1项放到textBox_Channel
            if (listview_CanUseChannel.Items.Count < 1)
            {
                MessageBox.Show("对不起，这个采集器的所有通道都已经使用了。您无法再添加新的通道了。", "通道设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                textBox_Channel.Text = listview_CanUseChannel.Items[0].SubItems[0].Text;
            }
            com_ChannelType.SelectedIndex = 0;
            this.Width = 303;
            this.listview_CanUseChannel.Visible = true;
        }

        /// <summary>
        /// 根据MaxChannelNum、UseChannelList刷新可用的通道列表
        /// </summary>
        private void RefreshCanUseChannel()
        {
            listview_CanUseChannel.Items.Clear();
            for (int i = 0; i < MaxChannelNum; i++)
            {
                if (!UseChannelList.ContainsKey(i))
                {
                    listview_CanUseChannel.Items.Add(i.ToString());
                }
            }
        }

        private void textBox_PerK_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字和句点
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void textBox_PerC_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字和句点
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void textBox_ValueMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字和句点
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox_ValueMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能输入数字和句点
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btn_Canel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (textBox_Channel.Text == "" || text_Name.Text == "" || com_ChannelType.Text == "")
            {
                MessageBox.Show("请输入完整的通道信息。\n至少包含：通道号、通道名称、通道类型", "通道设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (channelRow == null)
                {
                    //新建通道，则初始化channelRow
                    channelRow = DB_Service.MainDataSet.Tables["CollectChannelTable"].NewRow();
                    DB_Service.MainDataSet.Tables["CollectChannelTable"].Rows.Add(channelRow);
                }
                //修改channelRow的值
                channelRow["ChannelName"] = text_Name.Text.Trim();
                channelRow["ChannelType"] = com_ChannelType.Text;
                if (textBox_PerK.Text.Trim() == "")
                {
                    channelRow["ChannelPer_K"] = DBNull.Value;
                }
                else
                {
                    channelRow["ChannelPer_K"] = textBox_PerK.Text.Trim(); 
                }
                if (textBox_PerC.Text.Trim() == "")
                {
                    channelRow["ChannelPer_C"] = DBNull.Value;
                }
                else
                {
                    channelRow["ChannelPer_C"] = textBox_PerC.Text.Trim();
                }
                channelRow["ChannelUnit"] = textBox_Unit.Text.Trim();
                if (textBox_ValueMax.Text.Trim() == "")
                {
                    channelRow["ChannelValue_Max"] = DBNull.Value;
                }
                else
                {
                    channelRow["ChannelValue_Max"] = textBox_ValueMax.Text.Trim();
                }
                if (textBox_ValueMin.Text.Trim() == "")
                {
                    channelRow["ChannelValue_Min"] = DBNull.Value;
                }
                else
                {
                    channelRow["ChannelValue_Min"] = textBox_ValueMin.Text.Trim();
                }
                channelRow["ChannelComment"] = textBox_ChannelComment.Text.Trim();
                //将channelRow中的更新提交至数据库
                if (DB_Service.UpdateDBFromTable(DB_Service.MainDataSet.Tables["CollectChannelTable"]) > 0)
                {
                    if (frmDialogStation != null)
                    {
                        //将本次添加的新通道字串传给DialogStation         
                        //格式："通道号|名称|类型|通道ID"  如："2|桥墩温度传感器|电压电流型|172"
                        frmDialogStation.AddCollectChannelStr = textBox_Channel.Text + "|" + text_Name.Text.Trim() + "|" + com_ChannelType.Text + "|" + DB_Service.GetLastID("CollectChannelTable", "Channel_ID").ToString();
                        //由于在添加通道操作中。ID是自增量。必须要从数据库中更新一次才能把MainDataSet中的ID列更新
                        DB_Service.UpdateTableFromDB(DB_Service.MainDataSet.Tables["CollectChannelTable"]);
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("保存通道信息失败！\n请确保数据库连接正确。", "通道管理");
                } 
            }
        }

        private void listview_CanUseChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listview_CanUseChannel.SelectedItems.Count>0)
                textBox_Channel.Text = listview_CanUseChannel.SelectedItems[0].SubItems[0].Text;
        }
    }
}