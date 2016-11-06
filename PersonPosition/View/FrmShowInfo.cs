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
    public partial class FrmShowInfo : Form
    {
        public FrmShowInfo()
        {
            InitializeComponent();

            DataRow[] rows = DB_Service.MainDataSet.Tables["StationTable"].Select("StationFunction = '考勤管理'");
            for (int i = 0; i < rows.Length; i++)
            {
                com_StationID.Items.Add(rows[i]["ID"].ToString());
            }

            if (com_StationID.Items.Count > 0)
                com_StationID.SelectedIndex = 0;
            com_ShowInfoStyle.SelectedIndex = 0;
            com_ShowInfoIndex.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket_Service.SendMessage(Socket_Service.Command_C2S_SetInfo, com_StationID.Text, com_ShowInfoStyle.SelectedIndex.ToString(), com_ShowInfoIndex.SelectedIndex.ToString(), text_ShowInfo.Text.Trim(), "", "", "", "", "");
            this.Close();
        }
    }
}