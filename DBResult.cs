using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace check_up02
{
    class DBResult
    {
        /// <summary>
        /// 体检结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>

        public static void CreateResultTable(string name, string[] col)
        {
            string sql = "create table " + name;
            sql += "(id int(11) NOT NULL AUTO_INCREMENT";
            for (int i = 0; i < col.Length; i++)
            {
                sql += ", xiangmu_" + col[i] + " char(20) NOT NULL default ''";
            }
            sql += ",resultid int(11) NOT NULL, PRIMARY KEY (id))";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void GetResultWaiKe(int resultID, ref TJ_RESULT_WAIKE waike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_result_waike where resultid=@resultid";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@resultid", resultID);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                waike.mHeight = reader.GetInt32(2);
            }
            cmd.Connection.Close();
        }

        private static void CreateResultWaiKe(int resultID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "INSERT INTO tj_result_waike (resultid, height) VALUES (@resultid, -1)";
            cmd.Parameters.AddWithValue("@resultid", resultID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void UpdateResultWaiKe(int resultID, ref TJ_RESULT_WAIKE waike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "update tj_result_waike set height=@height where resultid=@resultid";
            cmd.Parameters.AddWithValue("@resultid", resultID);
            cmd.Parameters.AddWithValue("@height", waike.mHeight);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void DeleteResultWaiKe(int resultID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete from tj_result_waike where resultid=@resultid";
            cmd.Parameters.AddWithValue("@resultid", resultID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void CreateResultNeiKe(int resultID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "INSERT INTO tj_result_neike (resultid,xueya,xinlv) VALUES (@resultid,'','')";
            cmd.Parameters.AddWithValue("@suitid", resultID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void GetResultNeiKe(int resultID, ref TJ_RESULT_NEIKE neike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_result_neike where resultid=@resultid";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@resultid", resultID);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                neike.mXueYa = reader.GetString(2);
                neike.mXinLv = reader.GetString(3);
            }
            cmd.Connection.Close();
        }

        private static void UpdateResultNeiKe(int resultID, ref TJ_RESULT_NEIKE neike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "update tj_result_neike set xueya=@xueya, xinlv=@xinlv where resultid=@resultid";
            cmd.Parameters.AddWithValue("@resultid", resultID);
            cmd.Parameters.AddWithValue("@xueya", neike.mXueYa);
            cmd.Parameters.AddWithValue("@xinlv", neike.mXinLv);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void DeleteResultNeiKe(int resultID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete from tj_result_neike where resultid=@resultid";
            cmd.Parameters.AddWithValue("@resultid", resultID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static void CreateResult(ref TJ_RESULT result)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "INSERT INTO tj_result (tj_people, tj_suit, tj_date) VALUES (@people, @suit, now())";
            cmd.Parameters.AddWithValue("@people", result.mPeopleID);
            cmd.Parameters.AddWithValue("@suit", result.mSuitID);
            cmd.ExecuteNonQuery();
            result.mID = check_up_db.GetLastID(cmd);
            cmd.Connection.Close();
            CreateResultWaiKe(result.mID);
            CreateResultNeiKe(result.mID);
        }

        public static void UpdateResult(ref TJ_RESULT result)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "update tj_result set tj_people=@people, tj_suit=@tj_suit where id=@id";
            cmd.Parameters.AddWithValue("@people", result.mPeopleID);
            cmd.Parameters.AddWithValue("@suit", result.mSuitID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            UpdateResultWaiKe(result.mID, ref result.mWaiKe);
            UpdateResultNeiKe(result.mID, ref result.mNeiKe);
        }

        public static void DeleteResult(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete tj_result where id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            DeleteResultWaiKe(id);
            DeleteResultNeiKe(id);
        }

        public static TJ_RESULT GetTjResult(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "selet * from tj_result where id=%id";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            TJ_RESULT result = new TJ_RESULT();
            result.mID = id;
            result.mWaiKe = new TJ_RESULT_WAIKE();
            result.mNeiKe = new TJ_RESULT_NEIKE();
            if (reader.Read())
            {
                result.mPeopleID = reader.GetInt32(1);
                result.mSuitID = reader.GetInt32(2);
                GetResultWaiKe(result.mID, ref result.mWaiKe);
                GetResultNeiKe(result.mID, ref result.mNeiKe);
            }
            cmd.Connection.Close();
            return result;
        }

        public static List<TJ_RESULT> GetTjResults()
        {
            List<TJ_RESULT> listResult = new List<TJ_RESULT>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "tj_suit";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TJ_RESULT result = new TJ_RESULT()
                {
                    mID = reader.GetInt32(0),
                    mPeopleID = reader.GetInt32(1),
                    mSuitID = reader.GetInt32(2)
                };
                GetResultWaiKe(result.mID, ref result.mWaiKe);
                GetResultNeiKe(result.mID, ref result.mNeiKe);
                listResult.Add(result);
            }
            cmd.Connection.Close();
            return listResult;
        }
    }
}
