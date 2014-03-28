using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace check_up02
{
    class DBSuit
    {
        public static void CreateSuit(TJ_SUIT suit)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "insert into tj_suit (name,suitstring,date) values (@name,@suitstring,now())";
            cmd.Parameters.AddWithValue("name", suit.mName);
            cmd.Parameters.AddWithValue("suitstring", suit.mSuitString);
            cmd.ExecuteNonQuery();
            DBResult.CreateResultTable(check_up_db.GetLastID(cmd), suit.mSuitString.Split(','));
            cmd.Connection.Close();
        }

        public static void DeleteSuit(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete tj_suit where id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static TJ_SUIT GetTjSuit(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "selet * from tj_suit where id=%id";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            TJ_SUIT suit = new TJ_SUIT();
            if (reader.Read())
            {
                suit.mID = reader.GetInt32(0);
                suit.mName = reader.GetString(1);
                suit.mSuitString = reader.GetString(2);
            }
            cmd.Connection.Close();
            return suit;
        }

        public static List<TJ_SUIT> GetTjSuits()
        {
            List<TJ_SUIT> listSuit = new List<TJ_SUIT>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "tj_suit";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TJ_SUIT suit = new TJ_SUIT()
                {
                    mID = reader.GetInt32(0),
                    mName = reader.GetString(1),
                    mSuitString = reader.GetString(2),
                    mListXiangMu = new List<TJ_XIANGMU>()
                };
                string[] list = suit.mSuitString.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    suit.mListXiangMu.Add(DBXiangMu.GetXiangMu(Int32.Parse(list[i])));
                }
                listSuit.Add(suit);
            }
            cmd.Connection.Close();
            return listSuit;
        }
    }
}