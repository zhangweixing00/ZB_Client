using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using PersonPosition.Common;

namespace PersonPosition.StaticService
{
    public static class DataTableFactory_Service
    {
        public static DataTable MakePositionTable(string tableName)
        {
            //创建定位信息表
            DataTable table = new DataTable(tableName);
            //创建ID列，在逻辑上这个ID是CardID
            DataColumn ID = new DataColumn("ID");
            ID.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(ID);
            //创建Name列
            DataColumn Name = new DataColumn("Name");
            Name.DataType = System.Type.GetType("System.String");
            table.Columns.Add(Name);
            //创建CardType列
            DataColumn CardType = new DataColumn("CardType");
            CardType.DataType = System.Type.GetType("System.String");
            table.Columns.Add(CardType);
            //创建WorkType列
            DataColumn WorkType = new DataColumn("WorkType");
            WorkType.DataType = System.Type.GetType("System.String");
            table.Columns.Add(WorkType);
            //创建Department列
            DataColumn Department = new DataColumn("Department");
            Department.DataType = System.Type.GetType("System.String");
            table.Columns.Add(Department);
            //创建NearStationID列
            DataColumn NearStationID = new DataColumn("NearStationID");
            NearStationID.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(NearStationID);
            //创建Area列
            DataColumn Area = new DataColumn("Area");
            Area.DataType = System.Type.GetType("System.String");
            table.Columns.Add(Area);
            //创建InMineTime列
            DataColumn InMineTime = new DataColumn("InMineTime");
            InMineTime.DataType = System.Type.GetType("System.DateTime");
            table.Columns.Add(InMineTime);
            //创建InNullRSSITime列
            DataColumn InNullRSSITime = new DataColumn("InNullRSSITime");
            InNullRSSITime.DataType = System.Type.GetType("System.DateTime");
            table.Columns.Add(InNullRSSITime);
            //创建Geo_X列
            DataColumn Geo_X = new DataColumn("Geo_X");
            Geo_X.DataType = System.Type.GetType("System.Double");
            table.Columns.Add(Geo_X);
            //创建Geo_Y列
            DataColumn Geo_Y = new DataColumn("Geo_Y");
            Geo_Y.DataType = System.Type.GetType("System.Double");
            table.Columns.Add(Geo_Y);
            //添加主键
            DataColumn[] Keys = new DataColumn[1];
            Keys[0] = ID;
            table.PrimaryKey = Keys;

            return table; 
        }

        public static DataTable MakeMapTextTable(string tableName)
        {
            //创建地图文字表
            DataTable table = new DataTable(tableName);
            //创建ID列，在逻辑上这个ID是基站ID
            DataColumn ID = new DataColumn("ID");
            ID.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(ID);
            //创建Name列
            DataColumn Name = new DataColumn("Name");
            Name.DataType = System.Type.GetType("System.String");
            table.Columns.Add(Name);
            //创建StationType列
            DataColumn StationType = new DataColumn("StationType");
            StationType.DataType = System.Type.GetType("System.String");
            table.Columns.Add(StationType);
            //创建Geo_X列
            DataColumn Geo_X = new DataColumn("Geo_X");
            Geo_X.DataType = System.Type.GetType("System.Double");
            table.Columns.Add(Geo_X);
            //创建Geo_Y列
            DataColumn Geo_Y = new DataColumn("Geo_Y");
            Geo_Y.DataType = System.Type.GetType("System.Double");
            table.Columns.Add(Geo_Y);

            return table;
        }

        public static DataTable MakeCollectChannelValueTable(string tableName)
        {
            //创建采集信息表
            DataTable table = new DataTable(tableName);
            //创建StationID列
            DataColumn StationID = new DataColumn("StationID");
            StationID.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(StationID);
            //创建StationName列
            DataColumn StationName = new DataColumn("StationName");
            StationName.DataType = System.Type.GetType("System.String");
            table.Columns.Add(StationName);
            //创建Channel_ID列
            DataColumn Channel_ID = new DataColumn("Channel_ID");
            Channel_ID.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(Channel_ID);
            //创建ChannelNum列
            DataColumn ChannelNum = new DataColumn("ChannelNum");
            ChannelNum.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(ChannelNum);
            //创建ChannelName列
            DataColumn ChannelName = new DataColumn("ChannelName");
            ChannelName.DataType = System.Type.GetType("System.String");
            table.Columns.Add(ChannelName);
            //创建ChannelComment列
            DataColumn ChannelComment = new DataColumn("ChannelComment");
            ChannelComment.DataType = System.Type.GetType("System.String");
            table.Columns.Add(ChannelComment);
            //创建ChannelValueStr列（就是包含了数据单位的数值）
            DataColumn ChannelValueStr = new DataColumn("ChannelValueStr");
            ChannelValueStr.DataType = System.Type.GetType("System.String");
            table.Columns.Add(ChannelValueStr);
            //创建IsOverValue列
            DataColumn IsOverValue = new DataColumn("IsOverValue");
            IsOverValue.DataType = System.Type.GetType("System.String");
            table.Columns.Add(IsOverValue);
            //创建LastUpdateTime列
            DataColumn LastUpdateTime = new DataColumn("LastUpdateTime");
            LastUpdateTime.DataType = System.Type.GetType("System.DateTime");
            table.Columns.Add(LastUpdateTime);

            return table;
        }

        public static DataTable MakeHistoryDrawLinesTable(string tableName)
        {
            //创建历史定位信息表
            DataTable table = new DataTable(tableName);
            //创建ID列，从1自增1
            DataColumn ID = new DataColumn("ID");
            ID.AutoIncrement = true;
            ID.AutoIncrementSeed = 1;
            ID.AutoIncrementStep = 1;
            ID.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(ID);
            //创建卡片ID列
            DataColumn CardID = new DataColumn("CardID");
            CardID.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(CardID);
            //创建Name列
            DataColumn Name = new DataColumn("Name");
            Name.DataType = System.Type.GetType("System.String");
            table.Columns.Add(Name);
            //创建NearStationID列
            DataColumn NearStationID = new DataColumn("NearStationID");
            NearStationID.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(NearStationID);
            //创建Geo_X列
            DataColumn Geo_X = new DataColumn("Geo_X");
            Geo_X.DataType = System.Type.GetType("System.Double");
            table.Columns.Add(Geo_X);
            //创建Geo_Y列
            DataColumn Geo_Y = new DataColumn("Geo_Y");
            Geo_Y.DataType = System.Type.GetType("System.Double");
            table.Columns.Add(Geo_Y);
            //创建Time列
            DataColumn Time = new DataColumn("Time");
            Time.DataType = System.Type.GetType("System.DateTime");
            table.Columns.Add(Time);
            
            return table;
        }
    }
}
