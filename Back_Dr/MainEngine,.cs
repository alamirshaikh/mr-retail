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




public static void BulkInsertDataTable(DataTable ostudents)
    {
        using (SqlConnection con = new SqlConnection(SERVER_PATH))
        {
            if (con.State == ConnectionState.Closed) con.Open();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
            {
                // Map the columns from the DataTable to the SQL Server table
                bulkCopy.DestinationTableName = "Product_Item";

                bulkCopy.ColumnMappings.Add("PR_CODE", "PR_CODE");
                bulkCopy.ColumnMappings.Add("ITEM_NAME", "ITEM_NAME");
                bulkCopy.ColumnMappings.Add("GST", "GST");
                bulkCopy.ColumnMappings.Add("STOCK", "STOCK");
                bulkCopy.ColumnMappings.Add("UNIT", "UNIT");
                bulkCopy.ColumnMappings.Add("BARCODE", "BARCODE");
                bulkCopy.ColumnMappings.Add("SALE_PRICE", "SALE_PRICE");
                bulkCopy.ColumnMappings.Add("ACCOUNT", "ACCOUNT");
                bulkCopy.ColumnMappings.Add("DESCRIPTION", "DESCRIPTION");
                bulkCopy.ColumnMappings.Add("COST_PRICE", "COST_PRICE");
                bulkCopy.ColumnMappings.Add("pr_ACCOUNT", "pr_ACCOUNT");
                bulkCopy.ColumnMappings.Add("pr_COSTPRICE", "pr_COSTPRICE");
                bulkCopy.ColumnMappings.Add("pr_DESCRIPTION", "pr_DESCRIPTION");
                bulkCopy.ColumnMappings.Add("IDATE", "IDATE");
                bulkCopy.ColumnMappings.Add("USER_N", "USER_N");
                bulkCopy.ColumnMappings.Add("pic", "pic");
                bulkCopy.ColumnMappings.Add("MRP", "MRP");
                bulkCopy.ColumnMappings.Add("CGST", "CGST");
                bulkCopy.ColumnMappings.Add("SGST", "SGST");
                bulkCopy.ColumnMappings.Add("IGST", "IGST");
                bulkCopy.ColumnMappings.Add("HSN", "HSN");
                bulkCopy.ColumnMappings.Add("Msg", "Msg");
                bulkCopy.ColumnMappings.Add("Exp", "Exp");
                bulkCopy.ColumnMappings.Add("Color", "Color");
                bulkCopy.ColumnMappings.Add("Style", "Style");

                try
                {
                    // Write the DataTable data to the SQL Server table
                    bulkCopy.WriteToServer(ostudents);
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    throw;
                }
            }
        }
    }













    public static void BulkInsertDataTables(DataTable dataTable, string destinationTableName)
        {
            // Convert necessary columns to appropriate types based on target table schema
        

            try
            {
                using (SqlConnection connection = new SqlConnection(SERVER_PATH))
                {
                    connection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = destinationTableName;

                        // Optional: Add column mappings if your column names don't match
                        // Example: bulkCopy.ColumnMappings.Add("ExcelColumnName", "DatabaseColumnName");

                        bulkCopy.WriteToServer(dataTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                // Handle Invalid Operation exceptions (e.g., type mismatches)
                Console.WriteLine($"Invalid Operation: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine($"Error inserting data into SQL Server: {ex.Message}");
            }
        }

        private static void ConvertColumns(DataTable dataTable, string connectionString, string destinationTableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Get the schema of the destination table
                DataTable schemaTable = connection.GetSchema("Columns", new[] { null, null, destinationTableName, null });

                foreach (DataRow schemaRow in schemaTable.Rows)
                {
                    string columnName = schemaRow["COLUMN_NAME"].ToString();
                    string dataType = schemaRow["DATA_TYPE"].ToString();

                    // If the column exists in the DataTable, convert its type
                    if (dataTable.Columns.Contains(columnName))
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[columnName] != DBNull.Value)
                            {
                                try
                                {
                                    row[columnName] = ConvertToType(row[columnName], dataType);
                                }
                                catch (Exception ex)
                                {
                                    // Handle conversion exceptions
                                    Console.WriteLine($"Conversion Error: {ex.Message} in column {columnName}");
                                }
                            }
                        }
                    }
                }
            }
        }

        private static object ConvertToType(object value, string targetType)
        {
            switch (targetType.ToLower())
            {
                case "int":
                case "int32":
                    return Convert.ToInt32(value);
                case "smallint":
                case "int16":
                    return Convert.ToInt16(value);
                case "bigint":
                case "int64":
                    return Convert.ToInt64(value);
                case "decimal":
                case "numeric":
                    return Convert.ToDecimal(value);
                case "float":
                case "real":
                    return Convert.ToSingle(value);
                case "datetime":
                case "smalldatetime":
                case "date":
                    return Convert.ToDateTime(value);
                case "bit":
                    return Convert.ToBoolean(value);
                default:
                    return value.ToString();
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






