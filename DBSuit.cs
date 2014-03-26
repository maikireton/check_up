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
        /// <summary>
        /// 套餐
        /// </summary>
        /// <param name="suit"></param>
        /// 
        private static void GetSuitWaiKe(int suitID, ref TJ_SUIT_WAIKE waike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_suit_waike where suitid=@suitid";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@suitid", suitID);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                waike.mHeight = reader.GetBoolean(2);
            }
            cmd.Connection.Close();
        }

        private static void CreateSuitWaiKe(int suitID, ref TJ_SUIT_WAIKE waike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "INSERT INTO tj_suit_waike (suitid, height) VALUES (@suitid, @height)";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.Parameters.AddWithValue("@height", waike.mHeight);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void UpdateSuitWaiKe(int suitID, ref TJ_SUIT_WAIKE waike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "update tj_suit_waike set height=@height where suitid=@suitid";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.Parameters.AddWithValue("@height", waike.mHeight);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void DeleteSuitWaiKe(int suitID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete from tj_suit_waike where suitid=@suitid";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void CreateSuitNeiKe(int suitID, ref TJ_SUIT_NEIKE neike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "INSERT INTO tj_suit_neike (suitid, xueya) VALUES (@suitid, @xueya)";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.Parameters.AddWithValue("@xueya", neike.mXueYa);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void GetSuitNeiKe(int suitID, ref TJ_SUIT_NEIKE neike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_suit_neike where suitid=@suitid";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@suitid", suitID);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                neike.mXueYa = reader.GetBoolean(2);
            }
            cmd.Connection.Close();
        }

        private static void UpdateSuitNeiKe(int suitID, ref TJ_SUIT_NEIKE neike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "update tj_suit_neike set xueya=@xueya where suitid=@suitid";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.Parameters.AddWithValue("@xueya", neike.mXueYa);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void DeleteSuitNeiKe(int suitID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete from tj_suit_neike where suitid=@suitid";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static void CreateSuit(ref TJ_SUIT suit)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "INSERT INTO tj_suit (name, date) VALUES (@name, now())";
            cmd.Parameters.AddWithValue("@name", suit.mName);
            cmd.ExecuteNonQuery();
            suit.mID = check_up_db.GetLastID(cmd);
            cmd.Connection.Close();
            CreateSuitWaiKe(suit.mID, ref suit.mWaiKe);
            CreateSuitNeiKe(suit.mID, ref suit.mNeiKe);
        }

        public static void UpdateSuit(ref TJ_SUIT suit)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "update tj_suit set name=@name where id=@id";
            cmd.Parameters.AddWithValue("@name", suit.mName);
            cmd.Parameters.AddWithValue("@id", suit.mID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            UpdateSuitWaiKe(suit.mID, ref suit.mWaiKe);
            UpdateSuitNeiKe(suit.mID, ref suit.mNeiKe);
        }

        public static void DeleteSuit(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete tj_suit where id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            DeleteSuitWaiKe(id);
            DeleteSuitNeiKe(id);
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
            suit.mNeiKe = new TJ_SUIT_NEIKE();
            suit.mWaiKe = new TJ_SUIT_WAIKE();
            if (reader.Read())
            {
                suit.mID = reader.GetInt32(0);
                suit.mName = reader.GetString(1);
                GetSuitWaiKe(suit.mID, ref suit.mWaiKe);
                GetSuitNeiKe(suit.mID, ref suit.mNeiKe);
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
                    mNeiKe = new TJ_SUIT_NEIKE(),
                    mWaiKe = new TJ_SUIT_WAIKE()
                };
                GetSuitWaiKe(suit.mID, ref suit.mWaiKe);
                GetSuitNeiKe(suit.mID, ref suit.mNeiKe);
                listSuit.Add(suit);
            }
            cmd.Connection.Close();
            return listSuit;
        }
    }
}
