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
    public partial class DialogSearch : Form
    {
        private string searchStr;

        public DialogSearch(string _searchStr)
        {
            InitializeComponent();

            this.searchStr = _searchStr;

            Search();
        }

        private void Search()
        {
            bool isNum = true;
            try
            {
                int iii = Convert.ToInt32(searchStr);
            }
            catch
            {
                isNum = false;
            }

            if (isNum)
            {
                DataRow[] rows_PersonPID = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + searchStr + "'");
                DataRow[] rows_CardID = DB_Service.MainDataSet.Tables["CardTable"].Select("CardID = " + searchStr);
                DataRow[] rows_StationID = DB_Service.MainDataSet.Tables["StationTable"].Select("ID = " + searchStr);
                //有这个人的工号
                if (rows_PersonPID.Length > 0)
                {
                    string temp_CardID = "尚未绑定卡片";
                    DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + rows_PersonPID[0]["PID"].ToString() + "'");
                    if (rows.Length > 0)
                    {
                        temp_CardID = rows[0]["CardID"].ToString();
                    }
                    listView_Person.Items.Add(new ListViewItem(new string[] { rows_PersonPID[0]["PID"].ToString(), rows_PersonPID[0]["Name"].ToString(), temp_CardID, rows_PersonPID[0]["WorkType"].ToString(), rows_PersonPID[0]["Department"].ToString() }));
                }
                //有这张卡的卡号并且绑定了人员
                if (rows_CardID.Length > 0 && rows_CardID[0]["PID"] != DBNull.Value)
                {
                    string temp_PID = rows_CardID[0]["PID"].ToString();
                    //如果绑定这张卡片的人员的工号就等于搜索关键字，说明在上面已经显示过，则不重复显示
                    if (temp_PID != searchStr)
                    {
                        DataRow row = DB_Service.MainDataSet.Tables["PersonTable"].Select("PID = '" + temp_PID + "'")[0];
                        listView_Person.Items.Add(new ListViewItem(new string[] { row["PID"].ToString(), row["Name"].ToString(), rows_CardID[0]["CardID"].ToString(), row["WorkType"].ToString(), row["Department"].ToString() })); 
                    }
                }
                //有这个基站号
                if (rows_StationID.Length > 0)
                {
                    listView_Station.Items.Add(new ListViewItem(new string[] { rows_StationID[0]["ID"].ToString(), rows_StationID[0]["Name"].ToString(), rows_StationID[0]["StationType"].ToString() }));
                }
            }
            else
            {
                DataRow[] rows_PersonName = DB_Service.MainDataSet.Tables["PersonTable"].Select("Name like '%" + searchStr + "%'");
                DataRow[] rows_StationName = DB_Service.MainDataSet.Tables["StationTable"].Select("Name like '%" + searchStr + "%'");
                if (rows_PersonName.Length > 0)
                {
                    for (int i = 0; i < rows_PersonName.Length; i++)
                    {
                        DataRow[] rows = DB_Service.MainDataSet.Tables["CardTable"].Select("PID = '" + rows_PersonName[i]["PID"].ToString() + "'");
                        if (rows.Length > 0)
                        {
                            listView_Person.Items.Add(new ListViewItem(new string[] { rows_PersonName[i]["PID"].ToString(), rows_PersonName[i]["Name"].ToString(), rows[0]["CardID"].ToString(), rows_PersonName[i]["WorkType"].ToString(), rows_PersonName[i]["Department"].ToString() }));
                        }
                        else
                        {
                            listView_Person.Items.Add(new ListViewItem(new string[] { rows_PersonName[i]["PID"].ToString(), rows_PersonName[i]["Name"].ToString(), "", rows_PersonName[i]["WorkType"].ToString(), rows_PersonName[i]["Department"].ToString() }));
                        }
                    }
                }
                if (rows_StationName.Length > 0)
                {
                    for (int i = 0; i < rows_StationName.Length; i++)
                    {
                        listView_Station.Items.Add(new ListViewItem(new string[] { rows_StationName[i]["ID"].ToString(), rows_StationName[i]["Name"].ToString(), rows_StationName[i]["StationType"].ToString() }));
                    }
                }
            }

            //根据list的条目情况判断是显示窗体还是直接返回
            //空串代表没有找到
            //Cancel代表用户取消
            //Map_代表在地图中查看
            //Info_代表查看详细信息
            //P代表人员
            //S代表基站
            //如：Map_P:123 代表在地图中查看人员ID为123的人
            if (listView_Person.Items.Count == 0 && listView_Station.Items.Count == 0)
            {
                //没有任何记录
                this.Tag = "";
                this.Dispose();
            }
            else if (listView_Person.Items.Count == 0)
            {
                //没有人员的记录
                if(listView_Station.Items.Count == 1)
                {
                    //基站有1条记录
                    this.Tag = "Map_S:" + listView_Station.Items[0].SubItems[0].Text;
                    this.Dispose();
                }
                else
                {
                    //基站有多条记录
                    this.ShowDialog();
                }
            }
            else if (listView_Station.Items.Count == 0)
            {
                //没有基站的记录
                if (listView_Person.Items.Count == 1)
                {
                    //人员有1条记录
                    this.Tag = "Map_P:" + listView_Person.Items[0].SubItems[0].Text;
                    this.Dispose();
                }
                else
                {
                    //人员有多条记录
                    this.ShowDialog();
                }
            }
            else
            {
                //人员与基站都有记录
                this.ShowDialog();
            }
        }

        private void btn_Info_Click(object sender, EventArgs e)
        {
            if (listView_Person.SelectedItems.Count > 0 || listView_Station.SelectedItems.Count > 0)
            {
                //空串代表没有找到
                //Cancel代表用户取消
                //Map_代表在地图中查看
                //Info_代表查看详细信息
                //P代表人员
                //S代表基站
                //如：Map_P:123 代表在地图中查看人员ID为123的人
                if (listView_Person.SelectedItems.Count > 0)
                {
                    this.Tag = "Info_P:" + listView_Person.SelectedItems[0].SubItems[0].Text;
                }
                else
                {
                    this.Tag = "Info_S:" + listView_Station.SelectedItems[0].SubItems[0].Text;
                }
                this.Dispose();
            }
            else
            {
                label3.Visible = true;
            }
        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {
            if (listView_Person.SelectedItems.Count > 0 || listView_Station.SelectedItems.Count > 0)
            {
                //空串代表没有找到
                //Cancel代表用户取消
                //Map_代表在地图中查看
                //Info_代表查看详细信息
                //P代表人员
                //S代表基站
                //如：Map_P:123 代表在地图中查看人员ID为123的人
                if (listView_Person.SelectedItems.Count > 0)
                {
                    this.Tag = "Map_P:" + listView_Person.SelectedItems[0].SubItems[0].Text;
                }
                else
                {
                    this.Tag = "Map_S:" + listView_Station.SelectedItems[0].SubItems[0].Text;
                }
                this.Dispose();
            }
            else
            {
                label3.Visible = true;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Tag = "Cancel";
            this.Dispose();
        }

        private void listView_Person_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Person.SelectedItems.Count > 0)
            {
                label3.Visible = false;
            }
        }

        private void listView_Station_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Station.SelectedItems.Count > 0)
            {
                label3.Visible = false;
            }
        }

        private void listView_Person_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_Sure_Click(sender, e);
        }

        private void listView_Station_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_Sure_Click(sender, e);
        }
    }
}