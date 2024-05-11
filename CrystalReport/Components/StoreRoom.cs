using Back_Dr.Sale;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dr.Sale.Components
{

    

    public class StoreRoom 
    {
        private int count;

        public static string GetData { get; set; }
        public decimal SupplierName { get; set; }

        public static string ShopName { get; set; }
        
        public static DialogResult Dialog(string message,string title)
        {
            DialogResult result = MessageBox.Show(message,title,MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            return result;
        }


        public StoreRoom()
        {

        }




        public async Task<int> DoesDatabaseExistAsync(string databaseName)
        {
          

                using (var connection = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=master;Integrated Security=True;"))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    string query = "SELECT COUNT(*) FROM sys.databases WHERE name = @DatabaseName";
                    count = await connection.ExecuteScalarAsync<int>(query, new { DatabaseName = databaseName }).ConfigureAwait(false);




                }
            

 
            return count;
        }





        public decimal Balance(string name)
        {
           
            decimal dem=  MainEngine_.GetDataScript<decimal>("exec GetBalance '"+name+"'").FirstOrDefault();
            if(dem <=0)
            {
                return 0;
            }
            else
            {
            return dem;

            }


        }



        public static string TodaysSales()
        {
            return MainEngine_.GetDataScript<string>("SELECT Sum(TotalBill) FROM SInvoice WHERE CAST(InvDate AS DATE) = CAST(GETDATE() AS DATE)").FirstOrDefault();

        }


        public static string Balance()
        {
            return MainEngine_.GetDataScript<string>("SELECT Balance FROM Accounts").FirstOrDefault();

        }

        public static string MonthSales()
        {
            // Modify the SQL query to retrieve the sum of TotalBill for the current month
            return MainEngine_.GetDataScript<string>("SELECT Sum(TotalBill) FROM SInvoice WHERE YEAR(InvDate) = YEAR(GETDATE()) AND MONTH(InvDate) = MONTH(GETDATE())").FirstOrDefault();
        }

        public static string CustomerCount()
        {
            return MainEngine_.GetDataScript<string>("SELECT Count(ID) FROM Customer").FirstOrDefault();

        }

        public void SearchRecords(string searchTerm,DataGridView dataGridView1)
        {
            // Assuming your DataGridView is named dataGridView1
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // LINQ query to find matching rows based on the search term
                var query = from DataGridViewRow row in dataGridView1.Rows
                            where row.Cells.Cast<DataGridViewCell>()
                                       .Any(cell => cell.Value != null && cell.Value.ToString().Contains(searchTerm))
                            select row;

                if (query.Any())
                {
                    // If there are matching rows, select the first one and scroll to it
                    dataGridView1.ClearSelection();
                    DataGridViewRow firstMatchingRow = query.First();
                    firstMatchingRow.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = firstMatchingRow.Index;
                }
                else
                {
                    // If no matching records are found 
                }
            }
            else
            {
                // If the search term is empty 
            }
        }


        public async void NewData(string key, string address, string phone,string City)
        {
            try
            {

               
                    string ch = MainEngine_.GetDataScript<string>("select cust_name from Customer where cust_name = '" + key + "'").FirstOrDefault();
                
                if (ch == "" || String.IsNullOrEmpty(ch))
                {
                    var customer = new
                    {
                        cust_name = key,
                        cust_phone = phone,
                        cust_address = address,
                        cust_service = "customer",
                        cust_date = DateTime.Now,
                        pcity = City,
                        pstate ="Maharashtra",
                            Place = address

                    };

                    await MainEngine_.Add(customer, "sp_Customer");
                    

                }
                else
                {
                    try
                    {

                    }
                    catch (Exception ess)
                    {

                    }
                }
                
            }
            catch (Exception exx)
            {
               
            
            }


        }


        public static void ClearData(Control.ControlCollection ctrlCollection)
        {

            try
            {

                foreach (Control ctrl in ctrlCollection)
                {
                    if (ctrl is TextBoxBase || ctrl is ComboBox cmbx)
                    {


                        ctrl.Text = String.Empty;

                    }
                    else
                    {
                        ClearData(ctrl.Controls);
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
    }
}
