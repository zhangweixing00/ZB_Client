using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

using PersonPosition.Common;
using SharpMap.Layers;
using SharpMap.Data.Providers;
using SharpMap.Data;

namespace PersonPosition.StaticService
{
    public static class DB_Service
    {
        //数据集
        public static DataSet MainDataSet = null;

        /// <summary>
        /// 初始化全局数据库缓存MainDataSet
        /// </summary>
        public static void InitMainDataSet()
        {
            try
            {
                if (MainDataSet == null)
                {
                    MainDataSet = new DataSet("MainDataSet");
                    ////添加 系统表(数据库)
                    MainDataSet.Tables.Add(GetTable("CardTable", "Select * from CardTable"));
                    MainDataSet.Tables.Add(GetTable("CardTypeTable", "Select * from CardTypeTable"));
                    MainDataSet.Tables.Add(GetTable("ClassTable", "Select * from ClassTable"));
                    MainDataSet.Tables.Add(GetTable("DepartmentTable", "Select * from DepartmentTable"));
                    MainDataSet.Tables.Add(GetTable("LayerSortTable", "Select * from LayerSortTable order by ViewOrder ASC"));
                    MainDataSet.Tables.Add(GetTable("LayerTable", "Select * from LayerTable order by ViewOrder ASC"));
                    MainDataSet.Tables.Add(GetTable("MapTable", "Select * from MapTable"));
                    MainDataSet.Tables.Add(GetTable("MapAreaTable", "Select * from MapAreaTable"));
                    MainDataSet.Tables.Add(GetTable("PersonTable", "Select * from PersonTable"));
                    MainDataSet.Tables.Add(GetTable("UserTable", "Select * from UserTable"));
                    MainDataSet.Tables.Add(GetTable("WorkTypeTable", "Select * from WorkTypeTable"));
                    MainDataSet.Tables.Add(GetTable("WPTable", "Select * from WPTable"));
                    MainDataSet.Tables.Add(GetTable("CollectChannelTable", "Select * from CollectChannelTable"));
                    ////添加 系统表(内存库)
                    MainDataSet.Tables.Add(DataTableFactory_Service.MakeCollectChannelValueTable("CollectChannelValueTable"));
                }
            }
            catch (Exception e)
            {
                MainDataSet = null;
                throw new Exception("数据库初始化失败。\n\n" + e.Message);
            }
        }

        /// <summary>
        /// 得到一张表最新的ID
        /// </summary>
        /// <param name="NewTableName">表名</param>
        /// <param name="IDColumnName">ID列名</param>
        /// <returns></returns>
        public static int GetLastID(string NewTableName, string IDColumnName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Global.ServerDBStr))
                {
                    conn.Open();
                    using (SqlCommand comm = new SqlCommand("select top 1 " + IDColumnName + " from " + NewTableName + " order by " + IDColumnName + " DESC", conn))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                try
                                {
                                    if (reader.IsDBNull(0))
                                    {
                                        return -1;
                                    }
                                    else
                                    {
                                        return reader.GetInt32(0);
                                    }
                                }
                                catch
                                {
                                    return -1;
                                }
                                finally
                                {
                                    reader.Close();
                                }
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 得到一个以指定表名命名的用户表
        /// </summary>
        /// <param name="NewTableName">新表名</param>
        /// <param name="strSQL">SQL语句</param>
        /// <returns></returns>
        public static DataTable GetTable(string NewTableName,string strSQL)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Global.ServerDBStr))
                {
                    using (SqlCommand command = new SqlCommand(strSQL, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            using (DataTable table = new DataTable(NewTableName))
                            {
                                adapter.Fill(table);
                                return table;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从数据库更新表
        /// </summary>
        /// <param name="MainDataSetTable"></param>
        /// <returns></returns>
        public static bool UpdateTableFromDB(DataTable MainDataSetTable)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Global.ServerDBStr))
                {
                    using (SqlCommand command = new SqlCommand("select * from " + MainDataSetTable.TableName, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            MainDataSetTable.Clear();
                            if (adapter.Fill(MainDataSetTable) > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从表更新数据库
        /// </summary>
        /// <param name="MainDataSetTable"></param>
        /// <returns></returns>
        public static int UpdateDBFromTable(DataTable MainDataSetTable)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Global.ServerDBStr))
                {
                    using (SqlCommand command = new SqlCommand("select * from " + MainDataSetTable.TableName, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            using (SqlCommandBuilder myBuilder = new SqlCommandBuilder(adapter))
                            {
                                int result = 0;
                                result = adapter.Update(MainDataSetTable);
                                if (result > 0)
                                    //给服务器发送更新数据库命令
                                    Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, MainDataSetTable.TableName, "", "", "", "", "", "", "","");
                                return result;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行SQL语句,并且发送指定表更新信息
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="UpdateTableName"></param>
        /// <returns>收影响的行数</returns>
        public static int ExecuteSQL(string strSQL, string UpdateTableName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Global.ServerDBStr))
                {
                    using (SqlCommand command = new SqlCommand(strSQL, conn))
                    {
                        conn.Open();
                        command.CommandText = strSQL;
                        int result = 0;
                        result = command.ExecuteNonQuery();
                        if (result > 0)
                            //给服务器发送更新数据库命令
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableName, "", "", "", "", "", "", "","");
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 以一个事务执行SQL语句组
        /// </summary>
        /// <param name="strSQLs"></param>
        /// <returns>收影响的行数</returns>
        public static int ExecuteSQLs(List<string> strSQLs, string[] UpdateTableNameArray)
        {
            int result = 0;
            SqlTransaction tran = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(Global.ServerDBStr))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        conn.Open();
                        tran = conn.BeginTransaction();
                        command.Connection = conn;
                        command.Transaction = tran;
                        for (int i = 0; i < strSQLs.Count; i++)
                        {
                            command.CommandText = strSQLs[i].ToString();
                            result += command.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
                }
                if (result > 0)
                {
                    //给服务器发送更新数据库命令
                    switch (UpdateTableNameArray.Length)
                    {
                        case 1:
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableNameArray[0], "", "", "", "", "", "", "","");
                            break;
                        case 2:
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableNameArray[0], UpdateTableNameArray[1], "", "", "", "", "", "","");
                            break;
                        case 3:
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableNameArray[0], UpdateTableNameArray[1], UpdateTableNameArray[2], "", "", "", "", "","");
                            break;
                        case 4:
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableNameArray[0], UpdateTableNameArray[1], UpdateTableNameArray[2], UpdateTableNameArray[3], "", "", "", "","");
                            break;
                        case 5:
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableNameArray[0], UpdateTableNameArray[1], UpdateTableNameArray[2], UpdateTableNameArray[3], UpdateTableNameArray[4], "", "", "","");
                            break;
                        case 6:
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableNameArray[0], UpdateTableNameArray[1], UpdateTableNameArray[2], UpdateTableNameArray[3], UpdateTableNameArray[4], UpdateTableNameArray[5], "", "","");
                            break;
                        case 7:
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableNameArray[0], UpdateTableNameArray[1], UpdateTableNameArray[2], UpdateTableNameArray[3], UpdateTableNameArray[4], UpdateTableNameArray[5], UpdateTableNameArray[6], "","");
                            break;
                        case 8:
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableNameArray[0], UpdateTableNameArray[1], UpdateTableNameArray[2], UpdateTableNameArray[3], UpdateTableNameArray[4], UpdateTableNameArray[5], UpdateTableNameArray[6], UpdateTableNameArray[7],"");
                            break;
                        case 9:
                            Socket_Service.SendMessage(Socket_Service.Command_C2S_UpdateDB, UpdateTableNameArray[0], UpdateTableNameArray[1], UpdateTableNameArray[2], UpdateTableNameArray[3], UpdateTableNameArray[4], UpdateTableNameArray[5], UpdateTableNameArray[6], UpdateTableNameArray[7], UpdateTableNameArray[8]);
                            break;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将数据库或内存下的一张图层表以这个表名添加到MainDataSet中
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="DataSourceType">数据源类型</param>
        public static void AddLayerTable(string tablename, int DataSourceType)
        {
            try
            {
                DataTable table = new DataTable();
                switch (DataSourceType)
                {
                    case 0://数据库数据源
                        table = DB_Service.GetTable(tablename, "select * from " + tablename);
                        break;
                    case 1://内存数据源
                        if (tablename == "PositionTable")
                        {
                            //定位信息表
                            table = DataTableFactory_Service.MakePositionTable(tablename);
                        }
                        else
                        {
                            //地图文字表
                            table = DataTableFactory_Service.MakeMapTextTable(tablename);
                        }
                        break;
                }
                //添加到MainDataSet
                DB_Service.MainDataSet.Tables.Add(table);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 从DataSet中取出或组合出指定图层名（或带分类条件）的表
        /// </summary>
        /// <param name="layerName">图层名</param>
        /// <returns>返回的表</returns>
        public static DataTable GetDataTableByLayerName(string layerName)
        {
            string[] strArray = System.Text.RegularExpressions.Regex.Split(layerName, Global.SplitKey);
            //临时表
            DataTable tempTable = new DataTable();

            switch (strArray.Length)
            {
                //没有分类的表，则直接把DataSet中的表取过来
                case 1:
                    tempTable = DB_Service.MainDataSet.Tables[strArray[0]];
                    break;
                //有分类的表，则根据分类条件构造表
                case 3:
                    tempTable = DB_Service.MainDataSet.Tables[strArray[0]].Clone();
                    DataRow[] rows = DB_Service.MainDataSet.Tables[strArray[0]].Select(strArray[1] + " = '" + strArray[2] + "'");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        tempTable.ImportRow(rows[i]);
                    }
                    break;
            }

            return tempTable;
        }
    }
}
