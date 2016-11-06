using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Drawing;

using SharpMap.Forms;
using SharpMap.Styles;
using SharpMap.Layers;
using SharpMap.Data;
using SharpMap.Data.Providers;

using PersonPosition.Common;

namespace PersonPosition.StaticService
{
    public static class CommonFun
    {
        //系统导入结构体：系统时间
        [StructLayout(LayoutKind.Sequential)]
        private struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }
        //系统导入函数：设置时间
        [DllImport("Kernel32.dll")]
        private static extern bool SetLocalTime(ref SystemTime sysTime);


        public static bool SetAutoRunWhenStart(bool started, string exeName, string path)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);//打开注册表子项
            if (key == null)//如果该项不存在的话，则创建该子项
            {
                key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            }
            if (started == true)
            {
                try
                {
                    key.SetValue(exeName, path);//设置为开机启动
                    key.Close();
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    key.DeleteValue(exeName);//取消开机启动
                    key.Close();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 设置本机时间
        /// </summary>
        /// <param name="Time"></param>
        public static void SetSystemTime(DateTime Time)
        {
            //更新本地系统时间
            SystemTime sysTime = new SystemTime();
            sysTime.wYear = Convert.ToUInt16(Time.Year);
            sysTime.wMonth = Convert.ToUInt16(Time.Month);
            sysTime.wDay = Convert.ToUInt16(Time.Day);
            sysTime.wHour = Convert.ToUInt16(Time.Hour);
            sysTime.wMinute = Convert.ToUInt16(Time.Minute);
            sysTime.wSecond = Convert.ToUInt16(Time.Second);
            //注意：
            //结构体的wDayOfWeek属性一般不用赋值，函数会自动计算，写了如果不对应反而会出错
            //wMiliseconds属性默认值为一，可以赋值
            SetLocalTime(ref sysTime);
        }


        /// <summary>
        /// 加载指定的Layer至mapImage
        /// 注：不刷新地图
        /// 标准图层：
        ///     名称：TableOrShapeFile
        ///     标题：LayerName
        /// 分类图层：
        ///     名称：TableOrShapeFile + splitKey + ColumnName + splitKey + MaybeValue
        ///     标题：Text
        /// </summary>
        /// <param name="layerName">图层名</param>
        /// <param name="mapImage">欲加载到的控件</param>
        public static void AddLayer(string layerName, MapImage mapImage, float LabelOffset_X, float LabelOffset_Y)
        {
            try
            {
                string[] strArray = Regex.Split(layerName, Global.SplitKey);
                DataRow[] rows_layer = DB_Service.MainDataSet.Tables["LayerTable"].Select("TableOrShapeFile = '" + strArray[0] + "'");
                //图层
                VectorLayer layer;
                //图层样式
                VectorStyle style = new VectorStyle();
                //数据源类型
                int DataSourceType = Convert.ToInt32(rows_layer[0]["DataSourceType"]);
                //显示次序
                int viewOrder = Convert.ToInt32(rows_layer[0]["ViewOrder"]);
                //图层点样式
                string PointImageID = "";
                switch (strArray.Length)
                {
                    case 1:
                        if (DataSourceType == 0 || DataSourceType == 1)
                        {
                            PointImageID = rows_layer[0]["PointImage"].ToString();
                        }
                        break;
                    case 3:
                        DataRow[] roww = DB_Service.MainDataSet.Tables["LayerSortTable"].Select("TableOrShapeFile = '" + strArray[0] + "' and ColumnName = '" + strArray[1] + "' and MaybeValue = '" + strArray[2] + "'");
                        PointImageID = roww[0]["PointImage"].ToString();
                        break;
                }
                //图层线、面样式
                style.Line.Color = Color.FromArgb(Convert.ToInt32(rows_layer[0]["Line_Color"].ToString().Split(',')[0]), Convert.ToInt32(rows_layer[0]["Line_Color"].ToString().Split(',')[1]), Convert.ToInt32(rows_layer[0]["Line_Color"].ToString().Split(',')[2]), Convert.ToInt32(rows_layer[0]["Line_Color"].ToString().Split(',')[3]));
                style.Line.Width = Convert.ToInt32(rows_layer[0]["Line_Width"]);
                if (Convert.ToBoolean(rows_layer[0]["Fill_IsSolid"]))
                {
                    style.Fill = new SolidBrush(Color.FromArgb(Convert.ToInt32(rows_layer[0]["Fill_Color"].ToString().Split(',')[0]), Convert.ToInt32(rows_layer[0]["Fill_Color"].ToString().Split(',')[1]), Convert.ToInt32(rows_layer[0]["Fill_Color"].ToString().Split(',')[2]), Convert.ToInt32(rows_layer[0]["Fill_Color"].ToString().Split(',')[3])));
                }
                else
                {
                    style.Fill = new TextureBrush(Resource_Service.GetImage(rows_layer[0]["Fill_Image"].ToString()));
                }
                if (Convert.ToBoolean(rows_layer[0]["FillLine_Enable"]))
                {
                    style.EnableOutline = true;
                    style.Outline.Color = Color.FromArgb(Convert.ToInt32(rows_layer[0]["FillLine_Color"].ToString().Split(',')[0]), Convert.ToInt32(rows_layer[0]["FillLine_Color"].ToString().Split(',')[1]), Convert.ToInt32(rows_layer[0]["FillLine_Color"].ToString().Split(',')[2]), Convert.ToInt32(rows_layer[0]["FillLine_Color"].ToString().Split(',')[3]));
                    style.Outline.Width = Convert.ToInt32(rows_layer[0]["FillLine_Width"]);
                }
                else
                {
                    style.EnableOutline = false;
                }
                //加载数据源
                if (DataSourceType == 0 || DataSourceType == 1)
                {
                    //数据库数据源
                    layer = new VectorLayer(layerName,viewOrder, new GeometryFeatureProvider(new FeatureDataTable(DB_Service.GetDataTableByLayerName(layerName))));
                    //设置图层样式
                    if (PointImageID != "")
                        style.Symbol = (System.Drawing.Bitmap)Resource_Service.GetImage(PointImageID);
                }
                else
                {
                    //文件数据源
                    string file = Global.MapPath + strArray[0];
                    if (File.Exists(file))
                    {
                        ShapeFile shpfile = new ShapeFile(file, true);
                        shpfile.Encoding = System.Text.Encoding.GetEncoding("gb2312");
                        layer = new VectorLayer(layerName,viewOrder, shpfile);
                    }
                    else
                    {
                        MessageBox.Show("加载图层失败。\n\n没有找到 " + file + " 的图层文件！", "装载图层");
                        return;
                    }
                }
                //启用图层样式
                style.Enabled = true;
                layer.Style = style;
                //标注图层
                LabelLayer labellayer = new LabelLayer("LL" + layerName);
                labellayer.Enabled = true;
                labellayer.Style.Offset = new PointF(LabelOffset_X, LabelOffset_Y);
                if (rows_layer[0]["LabelLayerColName"].ToString() != "")
                {
                    labellayer.LabelColumn = rows_layer[0]["LabelLayerColName"].ToString();
                }
                else
                {
                    labellayer.LabelColumn = "Name";
                }
                if (rows_layer[0]["LabelLayerMinShow"] != DBNull.Value)
                {
                    labellayer.MinVisible = Convert.ToDouble(rows_layer[0]["LabelLayerMinShow"]);
                }
                else
                {
                    labellayer.MinVisible = 0;
                }
                if (rows_layer[0]["LabelLayerMaxShow"] != DBNull.Value)
                {
                    labellayer.MaxVisible = Convert.ToDouble(rows_layer[0]["LabelLayerMaxShow"]);
                }
                else
                {
                    labellayer.MaxVisible = 9999999;
                }

                labellayer.DataSource = layer.DataSource;

                //根据不同的图层，设置不同的标注图层风格
                switch (strArray[0])
                {
                    case "StationTable":
                        labellayer.Style.ForeColor = Global.StationNameColor;
                        labellayer.Style.Font = new Font("黑体", 15, FontStyle.Underline);
                        break;
                    case "PositionTable":
                        //labellayer.Style.ForeColor = Global.PersonNameColor;
                        labellayer.Style.ForeColor = Color.DodgerBlue ;//Color.Black;
                       
                        labellayer.Style.Font = new Font("黑体", 15, FontStyle.Underline);
                        break;
                    case "MapTextTable":
                        //labellayer.Style.ForeColor = Color.Blue;
                        labellayer.Style.ForeColor = Color.Black;
                        labellayer.Style.Font = new Font("黑体", 13, FontStyle.Regular);
                        break;
                    default:
                        labellayer.Style.ForeColor = Color.Black;
                        labellayer.Style.Font = new Font("黑体", 13, FontStyle.Regular);
                        break;
                }

                if (mapImage.Map.Layers.Count == 0)
                {
                    //如果没有最后一个图层，则直接添加
                    mapImage.Map.Layers.Add(layer);
                    mapImage.Map.Layers.Add(labellayer);
                }
                else
                {
                    if (mapImage.Map.Layers[mapImage.Map.Layers.Count - 2].ViewOrder < viewOrder)
                    {
                        //最后一个图层的ViewOrder小于将添加的图层：直接添加
                        mapImage.Map.Layers.Add(layer);
                        mapImage.Map.Layers.Add(labellayer);
                    }
                    else
                    {
                        //最后一个图层的ViewOrder大于或者等于要添加的图层：循环判断找出位置插入
                        for (int i = 0; i < mapImage.Map.Layers.Count; i = i + 2)
                        {
                            if (mapImage.Map.Layers[i].ViewOrder >= viewOrder)
                            {
                                mapImage.Map.Layers.Insert(i, layer);
                                mapImage.Map.Layers.Insert(i + 1, labellayer);
                                break;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("图层 " + layerName + " 加载失败！\n\n" + ex.Message, "装载图层", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}