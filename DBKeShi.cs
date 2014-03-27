using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace check_up02
{
    class DBKeShi
    {
        private static bool CheckKeShiExist(string name)
        {
            bool exist = false;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "select count(*) from yy_keshi where name=@name";
            cmd.Parameters.AddWithValue("@name", name);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {
                if (reader.GetInt32(0) > 0)
                    exist = true;
            }
            cmd.Connection.Close();
            return exist;
        }

        public static void CreateKeShi(string name)
        {
            if (CheckKeShiExist(name))
                return;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "INSERT INTO yy_keshi (name) VALUES (@name)";
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static List<TJ_KESHI> GetKeShi()
        {
            List<TJ_KESHI> listKeShi = new List<TJ_KESHI>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "yy_keshi";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listKeShi.Add(new TJ_KESHI()
                {
                    mID = reader.GetInt32(0),
                    mName = reader.GetString(1),
                });
            }
            cmd.Connection.Close();
            return listKeShi;
        }
    }
}
