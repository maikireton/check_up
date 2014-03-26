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
        public static int GetLastID(MySqlCommand cmd)
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
    }
}
