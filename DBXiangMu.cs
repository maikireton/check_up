using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace check_up02
{
    class DBXiangMu
    {
        public static List<TJ_XIANGMU> GetXiangMus()
        {
            List<TJ_XIANGMU> listXiangMu = new List<TJ_XIANGMU>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "tj_xiangmu";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listXiangMu.Add(new TJ_XIANGMU()
                {
                    mID = reader.GetInt32(0),
                    mName = reader.GetString(1),
                    mType = reader.GetString(2)
                });
            }
            cmd.Connection.Close();
            return listXiangMu;
        }

        public static TJ_XIANGMU GetXiangMu(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_xiangmu where id=@id";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            TJ_XIANGMU xiangmu = new TJ_XIANGMU();
            if (reader.Read())
            {
                xiangmu.mID = reader.GetInt32(0);
                xiangmu.mName = reader.GetString(1);
                xiangmu.mType = reader.GetString(2);
            }
            cmd.Connection.Close();
            return xiangmu;
        }

        private static bool CheckXiangMuExist(string name)
        {
            bool exist = false;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "select count(*) from tj_xiangmu where name=@name";
            cmd.Parameters.AddWithValue("@name", name);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetInt32(0) > 0)
                    exist = true;
            }
            cmd.Connection.Close();
            return exist;
        }

        public static void CreateXiangMu(TJ_XIANGMU xiangmu)
        {
            if (CheckXiangMuExist(xiangmu.mName))
                return;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "INSERT INTO tj_xiangmu (name,type) VALUES (@name,@type)";
            cmd.Parameters.AddWithValue("@name", xiangmu.mName);
            cmd.Parameters.AddWithValue("@type", xiangmu.mType);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
