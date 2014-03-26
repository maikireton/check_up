using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace check_up02
{
    class DBPeople
    {
        /// <summary>
        /// 添加体检人
        /// </summary>
        /// <param name="people"></param>
        /// <returns></returns>
        public static bool CreatePeople(ref TJ_PEOPLE people)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "INSERT INTO tj_people (name, sex, age, tel, corporation, address) VALUES(@name, @sex, @age, @tel, @corporation, @address)";
            cmd.Prepare();

            cmd.Parameters.AddWithValue("@name", people.mName);
            cmd.Parameters.AddWithValue("@sex", people.mSex);
            cmd.Parameters.AddWithValue("@age", people.mAge);
            cmd.Parameters.AddWithValue("@tel", people.mTel);
            cmd.Parameters.AddWithValue("@corporation", people.mCorporation);
            cmd.Parameters.AddWithValue("@address", people.mAddress);
            int affectRow = cmd.ExecuteNonQuery();
            if (affectRow == 0)
                return false;

            people.mID = check_up_db.GetLastID(cmd);
            cmd.Connection.Close();
            return true;
        }

        public static List<TJ_PEOPLE> GetPeople()
        {
            List<TJ_PEOPLE> listSuit = new List<TJ_PEOPLE>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "tj_people";
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listSuit.Add(new TJ_PEOPLE()
                {
                    mID = reader.GetInt32(0),
                    mName = reader.GetString(1),
                    mSex = reader.GetString(2),
                    mAge = reader.GetInt32(3),
                    mTel = reader.GetString(4),
                    mCorporation = reader.GetString(5),
                    mAddress = reader.GetString(6),
                });
            }
            cmd.Connection.Close();
            return listSuit;
        }
        /// <summary>
        /// 删除体检人
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeletePeople(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "delete from tj_people where id=@id";
            cmd.Prepare();

            cmd.Parameters.AddWithValue("@id", id);
            int affectRow = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;
        }

        /// <summary>
        /// 更新体检人信息
        /// </summary>
        /// <param name="people"></param>
        public static void UpdatePeople(TJ_PEOPLE people)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = check_up_db.GetDbConn();
            cmd.CommandText = "update tj_people set name=@name, sex=@sex, age=@age, tel=@tel, corporation=@corporation, address=@address where id=@id";
            cmd.Prepare();

            cmd.Parameters.AddWithValue("@name", people.mName);
            cmd.Parameters.AddWithValue("@sex", people.mSex);
            cmd.Parameters.AddWithValue("@age", people.mAge);
            cmd.Parameters.AddWithValue("@tel", people.mTel);
            cmd.Parameters.AddWithValue("@corporation", people.mCorporation);
            cmd.Parameters.AddWithValue("@address", people.mAddress);
            cmd.Parameters.AddWithValue("@id", people.mID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
