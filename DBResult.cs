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
        public static List<TJ_RESULT> GetResults(int suitID)
        {
            List<TJ_RESULT> listResult = new List<TJ_RESULT>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = string.Format("tj_suit_{0}", suitID);
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TJ_RESULT result = new TJ_RESULT()
                {
                    mID = reader.GetInt32(0),
                    mPeopleID = reader.GetInt32(1),
                    mListResult = new List<string>()
                };
                for (int i = 2; i < reader.FieldCount; i++)
                    result.mListResult.Add(reader.GetString(i));
                listResult.Add(result);
            }
            cmd.Connection.Close();
            return listResult;
        }

        public static TJ_RESULT GetResult(int suitID, int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = string.Format("select * from tj_suit_{0} where id={1}", suitID, id);
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            TJ_RESULT result = new TJ_RESULT();
            if (reader.Read())
            {
                result.mID = reader.GetInt32(0);
                result.mPeopleID = reader.GetInt32(1);
                result.mListResult = new List<string>();
                for (int i = 2; i < reader.FieldCount; i++)
                    result.mListResult.Add(reader.GetString(i));
            }
            cmd.Connection.Close();
            return result;
        }

        public static void CreateResultTable(int suitID, string[] col)
        {
            string name = string.Format("tj_suit_{0}", suitID);
            string sql = "create table " + name;
            sql += "(id int(11) NOT NULL AUTO_INCREMENT";
            sql += ",peopleid int(11) NOT NULL, PRIMARY KEY (id)";
            for (int i = 0; i < col.Length; i++)
            {
                sql += ", xiangmu_" + col[i] + " char(20) NOT NULL default ''";
            }
            sql += ")";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static void CreateResult(int suitID, int peopleID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = string.Format("INSERT INTO tj_suit_{0:G} (peopleid) VALUES (@peopleid)", suitID);
            cmd.Parameters.AddWithValue("@peopleid", peopleID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static void UpdateResult(int suitID, int id, TJ_RESULT result)
        {
            string name = string.Format("tj_suit_{0}", suitID);
            string sql = "update " + name + " set ";
            foreach (KeyValuePair<int, string> kvp in result.mXiangMuResult)
            {
                sql += string.Format("xiangmu_{0}={1},", kvp.Key, kvp.Value);                 
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += string.Format(" where id={0}", id);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
