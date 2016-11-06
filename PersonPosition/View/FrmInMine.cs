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
    public partial class FrmInMine : Form
    {


        public FrmInMine()
        {
            InitializeComponent();

            btn_Refresh_Click(null, null);
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            //listView1.Items.Clear();
            //foreach (int cardID in Global.InMineList.Keys)
            //{
            //    int PID = Convert.ToInt32(DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + cardID)[0]["PID"]);
            //    string Name = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = " + PID)[0]["Name"].ToString();
            //    string Department = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = " + PID)[0]["Department"].ToString();
            //    string WorkType = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = " + PID)[0]["WorkType"].ToString();
            //    listView1.Items.Add(new ListViewItem(new string[] { PID.ToString(), cardID.ToString(), Name, Department, WorkType, Global.InMineList[cardID].ToString() }));
            //}
            //label_AllNum.Text = listView1.Items.Count.ToString() + " 人";
        }
    }
}