using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace check_up02
{
    class DBYuYue
    {
        public static List<TJ_YUYUE> GetYuYue()
        {
            List<TJ_YUYUE> listYuYue = new List<TJ_YUYUE>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "tj_yuyue";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listYuYue.Add(new TJ_YUYUE()
                {
                    mID = reader.GetInt32(0),
                    mPeopleID = reader.GetInt32(1),
                    mSuitID = reader.GetInt32(2),
                    mDate = reader.GetDateTime(3),
                });
            }
            cmd.Connection.Close();
            return listYuYue;
        }

        public static TJ_YUYUE GetYuYue(int peopleid)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_yuyue where peopleid=@peopleid";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@peopleid", peopleid);
            MySqlDataReader reader = cmd.ExecuteReader();
            TJ_YUYUE yuyue = new TJ_YUYUE();
            if (reader.Read())
            {
                yuyue.mID = reader.GetInt32(0);
                yuyue.mPeopleID = reader.GetInt32(1);
                yuyue.mSuitID = reader.GetInt32(2);
                yuyue.mDate = reader.GetDateTime(3);
            }
            cmd.Connection.Close();
            return yuyue;
        }

        public static TJ_YUYUE GetYuYue(DateTime date)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_yuyue where date=@date";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@date", date.ToShortDateString());
            MySqlDataReader reader = cmd.ExecuteReader();
            TJ_YUYUE yuyue = new TJ_YUYUE();
            if (reader.Read())
            {
                yuyue.mID = reader.GetInt32(0);
                yuyue.mPeopleID = reader.GetInt32(1);
                yuyue.mSuitID = reader.GetInt32(2);
                yuyue.mDate = reader.GetDateTime(2);
            }
            cmd.Connection.Close();
            return yuyue;
        }

        public static bool CreateYuYue(TJ_YUYUE yuyue)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "insert into tj_yuyue (peopleid, suitid, date) VALUES(@peopleid, @suitid, @date)";
            cmd.Prepare();

            cmd.Parameters.AddWithValue("@peopleid", yuyue.mPeopleID);
            cmd.Parameters.AddWithValue("@suitid", yuyue.mSuitID);
            cmd.Parameters.AddWithValue("@date", yuyue.mDate.ToShortDateString());
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;
        }

        public static bool DelYuYue(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete from tj_yuyue where id=@id";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;
        }

        /*
        public static bool DelYuYue(int peopleid)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete from tj_yuyue where peopleid=@peopleid";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@peopleid", peopleid);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;
        }*/
    }
}
