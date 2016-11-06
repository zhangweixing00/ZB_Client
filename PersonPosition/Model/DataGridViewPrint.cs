using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Windows.Forms;

namespace PersonPosition.Model
{
    public class DataGridViewPrinter
    {
        private DataGridView dataGridView1;
        private PrintDocument printDocument;
        private PageSetupDialog pageSetupDialog;
        private PrintPreviewDialog printPreviewDialog;

        int currentPageIndex = 0;
        int colCount = 0;
        int rowCount = 0;
        int pageCount = 0;

        int titleSize = 20;
        bool isCustomHeader = false;

        Brush alertBrush = new SolidBrush(Color.Red);

        string[] header = null;//如果自定义就填入字符串，如果需要斜线分隔，就用\表示，例如：个数#名字 其中#为splitChar 
        string[] uplineHeader = null;//上行文字数组 
        int[] upLineHeaderIndex = null;//上行的文字index,如果没有上行就设为-1； 

        public bool isEveryPagePrintTitle = true;//是否每一页都要打印标题。 
        public int headerHeight = 30;//标题高度。 
        public int topMargin = 30; //顶边距 
        public int cellTopMargin = 6;//单元格顶边距 
        public int cellLeftMargin = 4;//单元格左边距 
        public char splitChar = '#';//当header要用斜线表示的时候 
        public string falseStr = "×";//如果传进来的DataGridView中有 false,把其转换得字符。 
        public string trueStr = "√";//如果传进来的DataGridView中有 true,把其转换得字符。 
        public int pageRowCount = 30;//每页行数 
        public int rowGap = 28;//行高 
        public int colGap = 5;//每列间隔 
        public int leftMargin = 45;//左边距 
        public Font titleFont = new Font("黑体", 24, FontStyle.Bold);//标题字体 
        public Font font = new Font("宋体", 10);//正文字体 
        public Font headerFont = new Font("黑体", 11, FontStyle.Bold);//列名标题 
        public Font footerFont = new Font("Arial", 8);//页脚显示页数的字体 
        public Font upLineFont = new Font("Arial", 9, FontStyle.Bold);//当header分两行显示的时候，上行显示的字体。 
        public Font underLineFont = new Font("Arial", 8);//当header分两行显示的时候，下行显示的字体。 
        public Brush brush = new SolidBrush(Color.Black);//画刷 
        public bool isAutoPageRowCount = true;//是否自动计算行数。 
        public int buttomMargin = 80;//底边距 
        public bool needPrintPageIndex = true;//是否打印页脚页数 
        public bool setTongji = false;       //设置是否显示统计

        string Title = "";  //主标题
        string SubTitle;    //副标题
        string TongJi1 = "";//统计01
        string TongJi2 = "";//统计02
        string TongJi3 = "";//统计03
        bool IsShowTongJi = false;//是否显示统计

        Font tongJiFont = new Font("宋体", 14);     //统计
        Font dateFont = new Font("宋体", 12, FontStyle.Bold);     //日期标题


        /// <summary>
        /// 日统计报表打印
        /// </summary>
        /// <param name="dGView">DataGridView</param>
        /// <param name="title">标题</param>
        /// <param name="times01">起始时间</param>
        /// <param name="times02">中止时间</param>
        /// <param name="tj01">统计01</param>
        /// <param name="tj02">统计02</param>
        /// <param name="tj03">统计03</param>
        /// <param name="tj">确认是否打印统计</param>
        /// <param name="_ShowColCount">要显示的列数</param>
        public DataGridViewPrinter(DataGridView dGView, string _Title, string _SubTitle, string _TongJi1, string _TongJi2, string _TongJi3, bool _IsShowTongJi,int _ShowColCount)
        {
            this.Title = _Title;
            this.SubTitle = _SubTitle;
            this.TongJi1 = _TongJi1;
            this.TongJi2 = _TongJi2;
            this.TongJi3 = _TongJi3;
            this.setTongji = _IsShowTongJi;
            this.colCount = _ShowColCount;

            this.dataGridView1 = dGView;
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
        }


        public bool setTowLineHeader(string[] upLineHeader, int[] upLineHeaderIndex)
        {
            this.uplineHeader = upLineHeader;
            this.upLineHeaderIndex = upLineHeaderIndex;
            this.isCustomHeader = true;
            return true;
        }
        public bool setHeader(string[] header)
        {
            this.header = header;
            return true;
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int width = e.PageBounds.Width;
            int height = e.PageBounds.Height;
            this.leftMargin = 40;//重新设置左边距

            if (this.isAutoPageRowCount)
            {
                pageRowCount = (int)((height - this.topMargin - titleSize - 25 - this.headerFont.Height - this.headerHeight - this.buttomMargin) / this.rowGap);
            }

            pageCount = (int)(rowCount / pageRowCount);
            if (rowCount % pageRowCount > 0)
                pageCount++;

            if (this.setTongji && pageCount == 1)
            {
                pageRowCount = (int)((height - this.topMargin - titleSize - 25 - this.headerFont.Height - this.headerHeight - this.buttomMargin - 25) / this.rowGap);
                pageCount = (int)(rowCount / pageRowCount);
                if (rowCount % pageRowCount > 0)
                    pageCount++;
            }

            int xoffset = (int)((width - e.Graphics.MeasureString(this.Title, this.titleFont).Width) / 2);
            int xoffset2 = (int)((width - e.Graphics.MeasureString(this.SubTitle, dateFont).Width) / 2);

            int x = 0;
            int y = topMargin;
            string cellValue = "";

            int startRow = currentPageIndex * pageRowCount;
            int endRow = startRow + this.pageRowCount < rowCount ? startRow + pageRowCount : rowCount;
            int currentPageRowCount = endRow - startRow;

            if (this.currentPageIndex == 0 || this.isEveryPagePrintTitle)
            {

                e.Graphics.DrawString(this.Title, titleFont, brush, xoffset, y);
                e.Graphics.DrawString(this.SubTitle, dateFont, brush, xoffset2, y + 40);
                y += titleSize + 20;
            }

            try
            {
                y += rowGap;
                x = leftMargin;

                DrawLine(new Point(x, y), new Point(x, y + currentPageRowCount * rowGap + this.headerHeight), e.Graphics);//最左边的竖线 

                int lastIndex = -1;
                int lastLength = 0;
                int indexj = -1;

                for (int j = 0; j < colCount; j++)
                {
                    int colWidth = dataGridView1.Columns[j].Width;
                    if (colWidth > 0)
                    {
                        indexj++;
                        if (this.header == null || this.header[indexj] == "")
                            cellValue = dataGridView1.Columns[j].Name;
                        else
                            cellValue = header[indexj];

                        if (this.isCustomHeader)
                        {
                            if (this.upLineHeaderIndex[indexj] != lastIndex)
                            {

                                if (lastLength > 0 && lastIndex > -1)//开始画上一个upline 
                                {
                                    string upLineStr = this.uplineHeader[lastIndex];
                                    int upXOffset = (int)((lastLength - e.Graphics.MeasureString(upLineStr, this.upLineFont).Width) / 2);
                                    if (upXOffset < 0)
                                        upXOffset = 0;
                                    e.Graphics.DrawString(upLineStr, this.upLineFont, brush, x - lastLength + upXOffset, y + (int)(this.cellTopMargin / 2));

                                    DrawLine(new Point(x - lastLength, y + (int)(this.headerHeight / 2)), new Point(x, y + (int)(this.headerHeight / 2)), e.Graphics);//中线 
                                    DrawLine(new Point(x, y), new Point(x, y + (int)(this.headerHeight / 2)), e.Graphics);
                                }
                                lastIndex = this.upLineHeaderIndex[indexj];
                                lastLength = colWidth + colGap;
                            }
                            else
                            {
                                lastLength += colWidth + colGap;
                            }
                        }

                        int Xoffset = 10;
                        int Yoffset = 20;
                        int leftWordIndex = cellValue.IndexOf(this.splitChar);
                        if (this.upLineHeaderIndex != null && this.upLineHeaderIndex[indexj] > -1)
                        {
                            if (leftWordIndex > 0)
                            {
                                string leftWord = cellValue.Substring(0, leftWordIndex);
                                string rightWord = cellValue.Substring(leftWordIndex + 1, cellValue.Length - leftWordIndex - 1);
                                //上面的字 
                                Xoffset = (int)(colWidth + colGap - e.Graphics.MeasureString(rightWord, this.upLineFont).Width);
                                Yoffset = (int)(this.headerHeight / 2 - e.Graphics.MeasureString("a", this.underLineFont).Height);

                                Xoffset = 6;
                                Yoffset = 10;
                                e.Graphics.DrawString(rightWord, this.underLineFont, brush, x + Xoffset - 4, y + (int)(this.headerHeight / 2));
                                e.Graphics.DrawString(leftWord, this.underLineFont, brush, x + 2, y + (int)(this.headerHeight / 2) + (int)(this.cellTopMargin / 2) + Yoffset - 2);
                                DrawLine(new Point(x, y + (int)(this.headerHeight / 2)), new Point(x + colWidth + colGap, y + headerHeight), e.Graphics);
                                x += colWidth + colGap;
                                DrawLine(new Point(x, y + (int)(this.headerHeight / 2)), new Point(x, y + currentPageRowCount * rowGap + this.headerHeight), e.Graphics);
                            }
                            else
                            {
                                e.Graphics.DrawString(cellValue, headerFont, brush, x, y + (int)(this.headerHeight / 2) + (int)(this.cellTopMargin / 2));
                                x += colWidth + colGap;
                                DrawLine(new Point(x, y + (int)(this.headerHeight / 2)), new Point(x, y + currentPageRowCount * rowGap + this.headerHeight), e.Graphics);
                            }

                        }
                        else
                        {
                            if (leftWordIndex > 0)
                            {
                                string leftWord = cellValue.Substring(0, leftWordIndex);
                                string rightWord = cellValue.Substring(leftWordIndex + 1, cellValue.Length - leftWordIndex - 1);
                                //上面的字 
                                Xoffset = (int)(colWidth + colGap - e.Graphics.MeasureString(rightWord, this.upLineFont).Width);
                                Yoffset = (int)(this.headerHeight - e.Graphics.MeasureString("a", this.underLineFont).Height);

                                e.Graphics.DrawString(rightWord, this.headerFont, brush, x + Xoffset - 4, y + 2);
                                e.Graphics.DrawString(leftWord, this.headerFont, brush, x + 2, y + Yoffset - 4);
                                DrawLine(new Point(x, y), new Point(x + colWidth + colGap, y + headerHeight), e.Graphics);
                                x += colWidth + colGap;
                                DrawLine(new Point(x, y), new Point(x, y + currentPageRowCount * rowGap + this.headerHeight), e.Graphics);
                            }
                            else
                            {
                                e.Graphics.DrawString(cellValue, headerFont, brush, x, y + cellTopMargin);
                                x += colWidth + colGap;
                                DrawLine(new Point(x, y), new Point(x, y + currentPageRowCount * rowGap + this.headerHeight), e.Graphics);
                            }
                        }
                    }
                }
                ////循环结束，画最后一个的upLine 
                if (this.isCustomHeader)
                {
                    if (lastLength > 0 && lastIndex > -1)//开始画上一个upline 
                    {
                        string upLineStr = this.uplineHeader[lastIndex];
                        int upXOffset = (int)((lastLength - e.Graphics.MeasureString(upLineStr, this.upLineFont).Width) / 2);
                        if (upXOffset < 0)
                            upXOffset = 0;
                        e.Graphics.DrawString(upLineStr, this.upLineFont, brush, x - lastLength + upXOffset, y + (int)(this.cellTopMargin / 2));

                        DrawLine(new Point(x - lastLength, y + (int)(this.headerHeight / 2)), new Point(x, y + (int)(this.headerHeight / 2)), e.Graphics);//中线 
                        DrawLine(new Point(x, y), new Point(x, y + (int)(this.headerHeight / 2)), e.Graphics);
                    }
                }

                int rightBound = x;
                DrawLine(new Point(leftMargin, y), new Point(rightBound, y), e.Graphics); //最上面的线
                y += this.headerHeight;

                //print all rows 
                for (int i = startRow; i < endRow; i++)
                {
                    x = leftMargin;
                    for (int j = 0; j < colCount; j++)
                    {
                        if (dataGridView1.Columns[j].Width > 0)
                        {
                            cellValue = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            if (cellValue == "False")
                                cellValue = falseStr;
                            if (cellValue == "True")
                                cellValue = trueStr;

                            e.Graphics.DrawString(cellValue, font, brush, x + this.cellLeftMargin, y + cellTopMargin);
                            x += dataGridView1.Columns[j].Width + colGap;
                            y = y + rowGap * (cellValue.Split(new char[] { '\r', '\n' }).Length - 1);
                        }
                    }
                    DrawLine(new Point(leftMargin, y), new Point(rightBound, y), e.Graphics);
                    y += rowGap;
                }
                DrawLine(new Point(leftMargin, y), new Point(rightBound, y), e.Graphics);

                currentPageIndex++;

                if (this.setTongji && currentPageIndex == pageCount)
                    this.IsShowTongJi = true;

                if (this.IsShowTongJi)
                {
                    int xoffsetTongJi = (int)((width - e.Graphics.MeasureString(TongJi1, dateFont).Width) / 2);
                    e.Graphics.DrawString(this.TongJi1, this.tongJiFont, brush, xoffsetTongJi, y + 25);          //统计1
                    e.Graphics.DrawString(this.TongJi2, this.tongJiFont, brush, xoffsetTongJi, y + 50);          //统计2
                    e.Graphics.DrawString(this.TongJi3, this.tongJiFont, brush, xoffsetTongJi + 340, y + 50);       　 //统计3
                }

                if (this.needPrintPageIndex)
                {
                    if (pageCount != 1)
                    {
                        e.Graphics.DrawString("共 " + pageCount.ToString() + " 页,当前第 " + this.currentPageIndex.ToString() + " 页", this.footerFont, brush, width - 200, (int)(height - this.buttomMargin / 2 - this.footerFont.Height));
                    }
                }

                string s = cellValue;
                string f3 = cellValue;

                if (currentPageIndex < pageCount)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                    this.currentPageIndex = 0;
                }
            }
            catch
            {

            }
        }

        private void DrawLine(Point sp, Point ep, Graphics gp)
        {
            Pen pen = new Pen(Color.Black);
            gp.DrawLine(pen, sp, ep);
        }

        public PrintDocument GetPrintDocument()
        {
            return printDocument;
        }

        public void Print()
        {
            rowCount = 0;
            try
            {
                switch (dataGridView1.DataSource.GetType().ToString())
                {
                    case "System.Data.DataSet":
                        rowCount = ((DataSet)dataGridView1.DataSource).Tables[0].Rows.Count;
                        break;
                    case "System.Data.DataView":
                        rowCount = ((DataView)dataGridView1.DataSource).Count;
                        break;
                    default:
                        rowCount = dataGridView1.Rows.Count;
                        break;
                }
                pageSetupDialog = new PageSetupDialog();
                pageSetupDialog.AllowOrientation = true;
                pageSetupDialog.Document = printDocument;
                pageSetupDialog.Document.DefaultPageSettings.Landscape = true;//设置打印为横向
                pageSetupDialog.ShowDialog();
                printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.ClientSize = new System.Drawing.Size(800, 600);

                printPreviewDialog.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Printer error." + ex.Message); 
            }
        }
    }
}