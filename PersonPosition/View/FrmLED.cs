using System;
using System.Collections;
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
    public partial class FrmLED : Form
    {
        private Point mouse_offset;
        private Point mousePos;
        private FrmLED_Setting frmsetting;

        private int PresentShowIndex = 0;

        private string BasicTitle = "";
        private Color AdvTextColor;
        private Color AdvLineColor;
        private bool IsAdvShow = false;
        private string HengLineKeyStr = "";
        private string ShuLineKeyStr = "";
        private string DrawAdvText = "";
        private Font DrawAdvFont;
        private bool IsAreaInMineNum = false;

        #region 无意义的窗体拖动代码

        private void FrmLED_MouseDown(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mouse_offset = e.Location;
                }
            }
        }

        private void FrmLED_MouseMove(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mousePos = Control.MousePosition;
                    mousePos.Offset(-mouse_offset.X, -mouse_offset.Y);
                    this.Location = mousePos;
                }
            }
        }

        private void FrmLED_MouseUp(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                //保存LED的位置
                Global.LEDLeft = this.Left;
                Global.LEDTop = this.Top;
            }
        }

        private void pic_AdvShow_MouseDown(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mouse_offset = e.Location;
                }
            }
        }

        private void pic_AdvShow_MouseMove(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mousePos = Control.MousePosition;
                    mousePos.Offset(-mouse_offset.X - pic_AdvShow.Left, -mouse_offset.Y - pic_AdvShow.Top);
                    this.Location = mousePos;
                }
            }
        }

        private void pic_AdvShow_MouseUp(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                //保存LED的位置
                Global.LEDLeft = this.Left;
                Global.LEDTop = this.Top;
            }
        }

        private void label_Title_MouseDown(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mouse_offset = e.Location;
                }
            }
        }

        private void label_Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mousePos = Control.MousePosition;
                    mousePos.Offset(-mouse_offset.X, -mouse_offset.Y);
                    this.Location = mousePos;
                }
            }
        }

        private void label_Title_MouseUp(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                //保存LED的位置
                Global.LEDLeft = this.Left;
                Global.LEDTop = this.Top;
            }
        }

        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mouse_offset = e.Location;
                }
            }
        }

        private void label6_MouseMove(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mousePos = Control.MousePosition;
                    mousePos.Offset(-mouse_offset.X - label_Name.Left, -mouse_offset.Y - label_Name.Top);
                    this.Location = mousePos;
                }
            }
        }

        private void label6_MouseUp(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                //保存LED的位置
                Global.LEDLeft = this.Left;
                Global.LEDTop = this.Top;
            }
        }

        private void label7_MouseDown(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mouse_offset = e.Location;
                }
            }
        }

        private void label7_MouseMove(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mousePos = Control.MousePosition;
                    mousePos.Offset(-mouse_offset.X - label_WorkType.Left, -mouse_offset.Y - label_WorkType.Top);
                    this.Location = mousePos;
                }
            }
        }

        private void label7_MouseUp(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                //保存LED的位置
                Global.LEDLeft = this.Left;
                Global.LEDTop = this.Top;
            }
        }

        private void label_Area_MouseDown(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mouse_offset = e.Location;
                }
            }
        }

        private void label_Area_MouseMove(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mousePos = Control.MousePosition;
                    mousePos.Offset(-mouse_offset.X - label_Area.Left, -mouse_offset.Y - label_Area.Top);
                    this.Location = mousePos;
                }
            }
        }

        private void label_Area_MouseUp(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                //保存LED的位置
                Global.LEDLeft = this.Left;
                Global.LEDTop = this.Top;
            }
        }

        private void label8_MouseDown(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mouse_offset = e.Location;
                }
            }
        }

        private void label8_MouseMove(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mousePos = Control.MousePosition;
                    mousePos.Offset(-mouse_offset.X - label_Time.Left, -mouse_offset.Y - label_Time.Top);
                    this.Location = mousePos;
                }
            }
        }

        private void label8_MouseUp(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                //保存LED的位置
                Global.LEDLeft = this.Left;
                Global.LEDTop = this.Top;
            }
        }

        private void panel_Basic_MouseDown(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mouse_offset = e.Location;
                }
            }
        }

        private void panel_Basic_MouseMove(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mousePos = Control.MousePosition;
                    mousePos.Offset(-mouse_offset.X - panel_Basic.Left, -mouse_offset.Y - panel_Basic.Top);
                    this.Location = mousePos;
                }
            }
        }

        private void panel_Basic_MouseUp(object sender, MouseEventArgs e)
        {
            if (!钉在桌面ToolStripMenuItem.Checked)
            {
                //保存LED的位置
                Global.LEDLeft = this.Left;
                Global.LEDTop = this.Top;
            }
        }

        #endregion

        public FrmLED()
        {
            InitializeComponent();

            InitShow(Global.LEDBasicTextColor, Global.LEDAdvTextColor, Global.LEDAdvLineColor, Global.LEDWidth, Global.LEDHeight, Global.LEDTop, Global.LEDLeft, Global.LEDTopMost, Global.LEDBasicTitle,Global.LEDIsAreaInMineNum, Global.LEDTextLoopTime, Global.LEDIsAdvShow, Global.LEDHengLineKeyStr, Global.LEDShuLineKeyStr, Global.LEDAdvText, Global.LEDBasicFont, Global.LEDAdvFont);

            timer_LoopHuman_Tick(null, null);
        }

        /// <summary>
        /// 从数据表中解析出采集信息，从传入的字串中解析出位置，将采集信息插入到 FrmLED.DrawAdvText
        /// </summary>
        /// <param name="AdvTextNoCollect">不包含采集信息的原始AdvText</param>
        public void GetCollectInsertAdvText(string AdvTextNoCollect)
        {
            DataTable copyCollectChannelValueTable = DB_Service.MainDataSet.Tables["CollectChannelValueTable"].Copy();

            string[] TextList = AdvTextNoCollect.Split('&');
            if (TextList.Length >= 3)
            {
                string AdvTextHadCollect = "";
                for (int i = 0; i < TextList.Length; i++)
                {
                    if (TextList[i] != "")
                    {
                        string[] SubText = TextList[i].Split('$');
                        if (SubText.Length == 2)
                        {
                            //采集信息
                            string CollectValue = "        ";
                            try
                            {
                                int StationID = Convert.ToInt32(SubText[0].Trim());
                                int ChannelNum = Convert.ToInt32(SubText[1].Trim());
                                DataRow[] rows = copyCollectChannelValueTable.Select("StationID = " + StationID + " and ChannelNum = " + ChannelNum);
                                if (rows.Length > 0)
                                {
                                    CollectValue = rows[0]["ChannelValueStr"].ToString();
                                    if (CollectValue.Length < 8)
                                    {
                                        CollectValue = CollectValue.PadRight(8, ' ');
                                    }
                                    else if (CollectValue.Length > 8)
                                    {
                                        CollectValue = CollectValue.Substring(0, 8);
                                    }
                                }
                            }
                            catch
                            {   }
                            AdvTextHadCollect += CollectValue;
                        }
                        else
                        {
                            //正常文字
                            AdvTextHadCollect += TextList[i];
                        }
                    }
                }
                DrawAdvText = AdvTextHadCollect;
            }
            else
            {
                DrawAdvText = AdvTextNoCollect;
            }
            this.pic_AdvShow.Refresh();
        }

        public void InitShow(Color _BasicTextColor,Color _AdvTextColor,Color _AdvLineColor,int _LEDWidth, int _LEDHeight, int _LEDTop, int _LEDLeft, bool _LEDTopMost,string _BasicTitle,bool _IsAreaInMineNum, int _LEDTextLoopTime, bool _IsAdvShow, string _HengLineKeyStr, string _ShuLineKeyStr, string _AdvText,Font _BasicFont, Font _AdvFont)
        {
            this.label_Title.ForeColor = _BasicTextColor;
            this.label_Name.ForeColor = _BasicTextColor;
            this.label_WorkType.ForeColor = _BasicTextColor;
            this.label_Area.ForeColor = _BasicTextColor;
            this.label_Time.ForeColor = _BasicTextColor;
            this.AdvTextColor = _AdvTextColor;
            this.AdvLineColor = _AdvLineColor;
            this.Width = _LEDWidth;
            this.Height = _LEDHeight;
            this.Top = _LEDTop;
            this.Left = _LEDLeft;
            this.TopMost = _LEDTopMost;
            this.IsAreaInMineNum = _IsAreaInMineNum;
            this.BasicTitle = _BasicTitle;
            //这里的字号都是整数，且都>6。所以不必担心诸如：FontSize=9.75 的问题
            this.label_Title.Font = _BasicFont;
            this.label_Name.Font = _BasicFont;
            this.label_WorkType.Font = _BasicFont;
            this.label_Area.Font = _BasicFont;
            this.label_Time.Font = _BasicFont;
            int FontSizeInt = Convert.ToInt32(label_Title.Font.Size);
            label_Title.Height = 4 * FontSizeInt + 2;
            this.timer_LoopHuman.Interval = _LEDTextLoopTime * 1000;
            this.IsAdvShow = _IsAdvShow;

            if (IsAdvShow)
            {
                panel_Basic.Width = (FontSizeInt - 7) * (FontSizeInt - 7) + 3 * FontSizeInt + 155;
                HengLineKeyStr = _HengLineKeyStr;
                ShuLineKeyStr = _ShuLineKeyStr;
                //从数据表中解析出采集信息，从传入的字串中解析出位置，将采集信息插入到 FrmLED.DrawAdvText
                GetCollectInsertAdvText(_AdvText);
                this.DrawAdvFont = _AdvFont;
                pic_AdvShow.Visible = true;
                pic_AdvShow.Refresh();
            }
            else
            {
                panel_Basic.Width = this.Width;
                HengLineKeyStr = "";
                ShuLineKeyStr = "";
                DrawAdvText = "";
                this.DrawAdvFont = new Font("宋体", 9);
                pic_AdvShow.Visible = false;
            }
            if (IsAreaInMineNum)
            {
                label_Area.Visible = true;
                label_Area.Left = panel_Basic.Width / 2 + 10;
                label_WorkType.Left = panel_Basic.Width / 4 - 1;
            }
            else
            {
                label_Area.Visible = false;
                label_WorkType.Left = panel_Basic.Width / 3;
            }
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmsetting == null || frmsetting.IsDisposed)
            {
                frmsetting = new FrmLED_Setting(this);
                frmsetting.Show(this);
            }
            else
            {
                frmsetting.BringToFront();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_LoopHuman_Tick(object sender, EventArgs e)
        {
            try
            {
                DataTable copyPositionTable = DB_Service.MainDataSet.Tables["PositionTable"].Copy();
                string InMineNum = "";
                if (this.IsAreaInMineNum)
                {
                    for (int i = 0; i < DB_Service.MainDataSet.Tables["MapAreaTable"].Rows.Count; i++)
                    {
                        string AreaName = DB_Service.MainDataSet.Tables["MapAreaTable"].Rows[i]["MapAreaName"].ToString();
                        int InAreaNum = copyPositionTable.Select("InMineTime is not null and Area = '" + AreaName + "'").Length;
                        InMineNum += AreaName + ":" + InAreaNum.ToString() + " ";
                    }
                    if (InMineNum.Length > 0)
                    {
                        InMineNum = InMineNum.Substring(0, InMineNum.Length - 1);
                    }
                    else
                    {
                        InMineNum = "未设置区域信息";
                    }
                }
                else
                {
                    InMineNum = "进洞总人数:" + copyPositionTable.Select("InMineTime is not null").Length.ToString();
                }

                label_Title.Text = this.BasicTitle + "\n" + DateTime.Now.ToString().Substring(0, DateTime.Now.ToString().Length - 3) + "\n" + InMineNum;


                label_Name.Text = "姓名\n";
                label_WorkType.Text = "部门\n";
                label_Area.Text = "区域\n";
                label_Time.Text = "时间\n";
                DataRow[] rows_InMine = copyPositionTable.Select("InMineTime is not null","Area asc");

                if (rows_InMine.Length > 0)
                {
                    for (int i = 0; i < 99; i++)
                    {
                        if (PresentShowIndex + i < rows_InMine.Length)
                        {
                            label_Name.Text += rows_InMine[PresentShowIndex + i]["Name"].ToString() + "\n";
                            label_WorkType.Text += rows_InMine[PresentShowIndex + i]["Department"].ToString() + "\n";
                            label_Area.Text += rows_InMine[PresentShowIndex + i]["Area"].ToString() + "\n";
                            if (rows_InMine[PresentShowIndex + i]["Name"].ToString() != "")
                            {
                                DateTime Time = Convert.ToDateTime(rows_InMine[PresentShowIndex + i]["InMineTime"]);
                                if (Time.Minute < 10)
                                {
                                    label_Time.Text += Time.Hour.ToString() + ":0" + Time.Minute.ToString() + "\n";
                                }
                                else
                                {
                                    label_Time.Text += Time.Hour.ToString() + ":" + Time.Minute.ToString() + "\n";
                                }
                            }
                            else
                            {
                                label_Time.Text += " \n";
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    PresentShowIndex++;
                    if (PresentShowIndex >= rows_InMine.Length)
                    {
                        PresentShowIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Global.IsShowBug)
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.TargetSite + "\n" + ex.StackTrace, "客户端LED动态显示进洞人员错误");
            }
        }

        private void 钉在桌面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            钉在桌面ToolStripMenuItem.Checked = !钉在桌面ToolStripMenuItem.Checked;
        }

        public void pic_AdvShow_Paint(object sender, PaintEventArgs e)
        {
            //绘制文字
            e.Graphics.DrawString(DrawAdvText, DrawAdvFont, new SolidBrush(AdvTextColor), new PointF(0, 2));
            Pen LinePen = new Pen(new SolidBrush(AdvLineColor));
            //画矩形框
            e.Graphics.DrawRectangle(LinePen, new Rectangle(1, 1, this.pic_AdvShow.Width - 4, this.pic_AdvShow.Height - 3));
            string[] DrawHengList = this.HengLineKeyStr.Split(',');
            if (DrawHengList.Length == 1)
            {
                if (DrawHengList[0] != "")
                {
                    int HengAdd = Convert.ToInt32(DrawHengList[0]);
                    //有1条横线
                    e.Graphics.DrawLine(LinePen, 1, pic_AdvShow.Height / 2 + HengAdd, pic_AdvShow.Width, pic_AdvShow.Height / 2 + HengAdd);
                }
            }
            else
            {
                //有1条以上的横线
                for (int i = 0; i < DrawHengList.Length; i++)
                {
                    int HengAdd = Convert.ToInt32(DrawHengList[i]);
                    e.Graphics.DrawLine(LinePen, 1, pic_AdvShow.Height / Convert.ToSingle(DrawHengList.Length + 1) * (i + 1) + HengAdd, pic_AdvShow.Width, pic_AdvShow.Height / Convert.ToSingle(DrawHengList.Length + 1) * (i + 1) + HengAdd);
                }
            }

            string[] DrawShuList = this.ShuLineKeyStr.Split(',');
            if (DrawShuList.Length == 1)
            {
                if (DrawShuList[0] != "")
                {
                    int ShuAdd = Convert.ToInt32(DrawShuList[0]);
                    //有1条竖线
                    e.Graphics.DrawLine(LinePen, pic_AdvShow.Width / 2 + ShuAdd, 1, pic_AdvShow.Width / 2 + ShuAdd, pic_AdvShow.Height);
                }
            }
            else
            {
                //有1条以上的竖线
                for (int j = 0; j < DrawShuList.Length; j++)
                {
                    int ShuAdd = Convert.ToInt32(DrawShuList[j]);
                    e.Graphics.DrawLine(LinePen, pic_AdvShow.Width / Convert.ToSingle(DrawShuList.Length + 1) * (j + 1) + ShuAdd, 1, pic_AdvShow.Width / Convert.ToSingle(DrawShuList.Length + 1) * (j + 1) + ShuAdd, pic_AdvShow.Height);
                }
            }
        }

        private void panel_Basic_Resize(object sender, EventArgs e)
        {
            label_Name.Top = label_Title.Height + 1;
            label_WorkType.Top = label_Title.Height + 1;
            label_Area.Top = label_Title.Height + 1;
            label_Time.Top = label_Title.Height + 1;

            pic_AdvShow.Left = panel_Basic.Width;
            pic_AdvShow.Width = this.Width - panel_Basic.Width+2;
        }

        private void label_Title_Resize(object sender, EventArgs e)
        {
            label_Name.Top = label_Title.Height + 1;
            label_WorkType.Top = label_Title.Height + 1;
            label_Area.Top = label_Title.Height + 1;
            label_Time.Top = label_Title.Height + 1;
        }

        private void label8_Resize(object sender, EventArgs e)
        {
            label_Time.Left = panel_Basic.Width - label_Time.Width + 3;
        }
    }
}