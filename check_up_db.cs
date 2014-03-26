using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

/// 列名 SELECT column_name from information_schema.columns WHERE table_name ='tj_suit';
/// 
namespace check_up02
{
    class check_up_db
    {
        /// <summary>
        /// 获得连接
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection GetDbConn()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=dalianyanhua;port=3306;password=8882210;");
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Done.");
            return conn;
        }

        /// <summary>
        /// 插入数据生成的id
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private static int GetLastID(MySqlCommand cmd)
        {
            //SELECT LAST_INSERT_ID();
            int lastID = -1;
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lastID = reader.GetInt32(0);
                Console.WriteLine(reader.GetValue(0));
            }
            return lastID;
        }

        /// <summary>
        /// 添加体检人
        /// </summary>
        /// <param name="people"></param>
        /// <returns></returns>
        public static bool CreatePeople(ref TJ_PEOPLE people)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
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

            people.mID = GetLastID(cmd);
            cmd.Connection.Close();
            return true;
        }

        public static List<TJ_PEOPLE> GetPeople()
        {
            List<TJ_PEOPLE> listSuit = new List<TJ_PEOPLE>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "tj_people";
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
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

        /// <summary>
        /// 套餐
        /// </summary>
        /// <param name="suit"></param>
        /// 
        private static void GetSuitWaiKe(int suitID, ref TJ_SUIT_WAIKE waike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_suit_waike where suitid=@suitid";
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
            cmd.CommandText = "INSERT INTO tj_suit_waike (suitid, height) VALUES (@suitid, @height)";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.Parameters.AddWithValue("@height", waike.mHeight);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void UpdateSuitWaiKe(int suitID, ref TJ_SUIT_WAIKE waike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
            cmd.CommandText = "update tj_suit_waike set height=@height where suitid=@suitid";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.Parameters.AddWithValue("@height", waike.mHeight);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void DeleteSuitWaiKe(int suitID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
            cmd.CommandText = "delete from tj_suit_waike where suitid=@suitid";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void CreateSuitNeiKe(int suitID, ref TJ_SUIT_NEIKE neike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
            cmd.CommandText = "update tj_suit_neike set xueya=@xueya where suitid=@suitid";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.Parameters.AddWithValue("@xueya", neike.mXueYa);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void DeleteSuitNeiKe(int suitID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
            cmd.CommandText = "delete from tj_suit_neike where suitid=@suitid";
            cmd.Parameters.AddWithValue("@suitid", suitID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static void CreateSuit(ref TJ_SUIT suit)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
            cmd.CommandText = "INSERT INTO tj_suit (name, date) VALUES (@name, now())";
            cmd.Parameters.AddWithValue("@name", suit.mName);
            cmd.ExecuteNonQuery();
            suit.mID = GetLastID(cmd);
            cmd.Connection.Close();
            CreateSuitWaiKe(suit.mID, ref suit.mWaiKe);
            CreateSuitNeiKe(suit.mID, ref suit.mNeiKe);
        }

        public static void UpdateSuit(ref TJ_SUIT suit)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
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
        
        public static List<TJ_SUIT> GetTjSuit()
        {
            List<TJ_SUIT> listSuit = new List<TJ_SUIT>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "tj_suit";
            cmd.Connection = GetDbConn();
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

        /// <summary>
        /// 体检结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
         
        private static void GetResultWaiKe(int resultID, ref TJ_RESULT_WAIKE waike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_result_waike where resultid=@resultid";
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
            cmd.CommandText = "INSERT INTO tj_result_waike (resultid, height) VALUES (@resultid, -1)";
            cmd.Parameters.AddWithValue("@resultid", resultID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void UpdateResultWaiKe(int resultID, ref TJ_RESULT_WAIKE waike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
            cmd.CommandText = "update tj_result_waike set height=@height where resultid=@resultid";
            cmd.Parameters.AddWithValue("@resultid", resultID);
            cmd.Parameters.AddWithValue("@height", waike.mHeight);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void DeleteResultWaiKe(int resultID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
            cmd.CommandText = "delete from tj_result_waike where resultid=@resultid";
            cmd.Parameters.AddWithValue("@resultid", resultID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void CreateResultNeiKe(int resultID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
            cmd.CommandText = "INSERT INTO tj_result_neike (resultid,xueya,xinlv) VALUES (@resultid,'','')";
            cmd.Parameters.AddWithValue("@suitid", resultID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private static void GetResultNeiKe(int resultID, ref TJ_RESULT_NEIKE neike)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from tj_result_neike where resultid=@resultid";
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
            cmd.CommandText = "delete from tj_result_neike where resultid=@resultid";
            cmd.Parameters.AddWithValue("@resultid", resultID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static void CreateResult(ref TJ_RESULT result)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
            cmd.CommandText = "INSERT INTO tj_result (tj_people, tj_suit, tj_date) VALUES (@people, @suit, now())";
            cmd.Parameters.AddWithValue("@people", result.mPeopleID);
            cmd.Parameters.AddWithValue("@suit", result.mSuitID);
            cmd.ExecuteNonQuery();
            result.mID = GetLastID(cmd);
            cmd.Connection.Close();
            CreateResultWaiKe(result.mID);
            CreateResultNeiKe(result.mID);
        }

        public static void UpdateResult(ref TJ_RESULT result)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
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
            cmd.Connection = GetDbConn();
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

        public static List<TJ_RESULT> GetTjResult()
        {
            List<TJ_RESULT> listResult = new List<TJ_RESULT>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "tj_suit";
            cmd.Connection = GetDbConn();
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

        public static void Test()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "member";
            cmd.Connection = GetDbConn();
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
            }

        }
    }
}
