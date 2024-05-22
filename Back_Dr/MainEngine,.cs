using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace Back_Dr.Sale
{
    public  class MainEngine_
    {

        public static readonly string SERVER_PATH = @"Data Source=localhost\sqlexpress;Initial Catalog=drsale;Integrated Security=True;";
        public static async Task Add<T>(T Model,string store_name)
        {

             
            using (var connection = new SqlConnection(SERVER_PATH))
            {

                var execute = await connection.ExecuteAsync(store_name,Model,commandType:CommandType.StoredProcedure);
                
            }


        }


        public static List<T> GetData<T>(string qr)
        {
            List<T> result = new List<T>();

            using (var conn = new SqlConnection(SERVER_PATH))
            {
                conn.Open();
                result = conn.Query<T>(qr, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        public static List<T> GetDataScript<T>(string qr)
        {
            List<T> result = new List<T>();

            using (var conn = new SqlConnection(SERVER_PATH))
            {
                conn.Open();
                result = conn.Query<T>(qr).ToList();
            }

            return result;
        }

        
        public static List<T> GetData<T>(string qr, T par)
        {
            List<T> result = new List<T>();

            using (var conn = new SqlConnection(SERVER_PATH))
            {
                conn.Open();
                result = conn.Query<T>(qr, par, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

       
    }


}






