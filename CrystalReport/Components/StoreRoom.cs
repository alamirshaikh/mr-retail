﻿using Back_Dr.Sale;
using Dapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using ExcelDataReader;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace Dr.Sale.Components
{



    public class StoreRoom
    {
        private int count;

        public static string GetData { get; set; }

        public static string SendReport { get; set; }


        public static DateTime dateTimePicker1 { get; set; }
        public static DateTime dateTimePicker2 { get; set; }





public static DataTable AddExcel(string file)
    {
        DataTable dataTable = new DataTable();

        // Register the necessary encodings
        //System.Text.Encoding.(System.Text.CodePagesEncodingProvider.Instance);

        try
        {
            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // Use first row as column names
                        }
                    });

                    dataTable = result.Tables[0];
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., logging)
            Console.WriteLine($"Error reading Excel file: {ex.Message}");
        }

        return dataTable;
    }




    public decimal SupplierName { get; set; }
        
        public static string ShopName { get; set; }

        public static DialogResult Dialog(string message, string title)
        {
            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return result;
        }



        public static string GetStyle()
        {
            return MainEngine_.GetDataScript<string>("select SettingValue from SYSTEM_SETTINGS where SettingKey = 'Style-form'").FirstOrDefault();
        }




        public static string GetState()
        {
            return MainEngine_.GetDataScript<string>("select State from OwnerInformation").FirstOrDefault();
        }


        public static string GetSupplierBlc()
        {
            return MainEngine_.GetDataScript<string>("select *  Parties  ").FirstOrDefault();
        }







        public static decimal GST(decimal amount, int percent)
        {
            decimal gstAmount = (amount * percent) / 100;
            decimal totalAmount = amount + gstAmount;
            return totalAmount;
        }





        public static string Printer_Name()
        {
            return MainEngine_.GetDataScript<string>("select Printer_Name from Printer_setings").FirstOrDefault();
        }

        public static string Printer_Size()
        {
            return MainEngine_.GetDataScript<string>("select size from Printer_setings").FirstOrDefault();
        }



        public static string GetSaleType()
        {
            return MainEngine_.GetDataScript<string>("select defaults from DefaultGet where Comp = 'POS' AND sub ='Sale Type'").FirstOrDefault();
        }



        public static string GetBillOption()
        {
            return MainEngine_.GetDataScript<string>("select defaults from DefaultGet where Comp = 'POS' AND sub ='Bill Option'").FirstOrDefault();
        }




        public static string GetPriview()
        {
            return MainEngine_.GetDataScript<string>("select defaults from DefaultGet where Comp = 'POS' AND sub ='PRINTVIEW'").FirstOrDefault();
        }



        public static string Template()
        {
            return MainEngine_.GetDataScript<string>("select Template_Name from Printer_setings").FirstOrDefault();
        }


         public static  int GetSID(string order)
        {

            string t=  MainEngine_.GetDataScript<string>("select partiname from Purches_Order where BillID='"+order+"'").FirstOrDefault();

            return MainEngine_.GetDataScript<int>("select ID from Parties where company = '"+t+"'").FirstOrDefault();



        }




        public static string Purches_Tax()
        {
            return MainEngine_.GetDataScript<string>("select purches_tax from TaxSettings").FirstOrDefault();
        }

        public static string Sale_Tax()
        {
            return MainEngine_.GetDataScript<string>("select sale_tax from TaxSettings").FirstOrDefault();
        }




        public static Color IdentifyBackgroundColor(Bitmap bitmap)
        {
            // This method assumes the background color is uniform and uses the corner pixels
            int sampleSize = 10; // Number of pixels to sample from each corner
            Color[] corners = new Color[4];
            corners[0] = bitmap.GetPixel(0, 0); // Top-left
            corners[1] = bitmap.GetPixel(bitmap.Width - 1, 0); // Top-right
            corners[2] = bitmap.GetPixel(0, bitmap.Height - 1); // Bottom-left
            corners[3] = bitmap.GetPixel(bitmap.Width - 1, bitmap.Height - 1); // Bottom-right

            // Calculate the average color
            int r = 0, g = 0, b = 0;
            foreach (Color color in corners)
            {
                r += color.R;
                g += color.G;
                b += color.B;
            }
            r /= corners.Length;
            g /= corners.Length;
            b /= corners.Length;

            return Color.FromArgb(r, g, b);
        }

        public static DataTable GetTable(string sql)
        {
            DataTable table = new DataTable();

            // Assuming you have a valid connection string


            using (SqlConnection conn = new SqlConnection(MainEngine_.SERVER_PATH))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }

        public static string OwnerName()
        {
            return MainEngine_.GetDataScript<string>("select OwnerName from OwnerInformation").FirstOrDefault();

        }

        public StoreRoom()
        {

        }




        public static decimal GetValueMonth(int m)
        {

            return MainEngine_.GetDataScript<decimal>($"SELECT SUM(TotalBill) as total FROM SInvoice where MONTH(invdate) = {m} GROUP BY MONTH(invdate); ").FirstOrDefault();


        }

        public static string getpassword()
        {

            return MainEngine_.GetDataScript<string>($"SELECT password from UserAccount where userId = 'bill'").FirstOrDefault();


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

            decimal dem = MainEngine_.GetDataScript<decimal>("exec GetBalance '" + name + "'").FirstOrDefault();
            if (dem <= 0)
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


        public static string TotalBill(string invoice)
        {
            return MainEngine_.GetDataScript<string>("SELECT TotalBill FROM SInvoice WHERE InvoiceID = '" + invoice + "'").FirstOrDefault();

        }




        public static string TotalTax(string invoice)
        {
            return MainEngine_.GetDataScript<string>("SELECT other FROM SInvoice WHERE InvoiceID = '" + invoice + "'").FirstOrDefault();

        }



        public static string MobileNumber()
        {
            return MainEngine_.GetDataScript<string>("select ShopMobile from OwnerInformation").FirstOrDefault();

        }








        public static string Balance()
        {
            return MainEngine_.GetDataScript<string>("SELECT Balance FROM Accounts").FirstOrDefault();

        }



        public static decimal CustomerBalace(string cust_name)
        {
            return MainEngine_.GetDataScript<decimal>("Select Balance from Customer where cust_name = '" + cust_name + "'").FirstOrDefault();

        }





        public static string ShopNames()
        {
            return MainEngine_.GetDataScript<string>("select ShopName from OwnerInformation").FirstOrDefault();

        }



        public static decimal TotalPaid(string invoice)
        {
            return MainEngine_.GetDataScript<decimal>("select SUM(PAID) from CustomerTransactions  where InvoiceId = '" + invoice + "'").FirstOrDefault();

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

        public void SearchRecords(string searchTerm, DataGridView dataGridView1)
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


        public async Task NewData(string key, string address, string phone, string city)
        {
            try
            {
                string query = "SELECT cust_name FROM Customer WHERE cust_name = '" + key + "'";
                var parameters = new { CustName = key };
                string ch = MainEngine_.GetDataScript<string>(query).FirstOrDefault();

                if (string.IsNullOrEmpty(ch))
                {
                    var customer = new
                    {
                        cust_name = key,
                        cust_phone = phone,
                        cust_address = address,
                        cust_service = "customer",
                        cust_date = DateTime.Now,
                        pcity = city,
                        pstate = StoreRoom.GetState(),
                        Place = address
                    };

                    await MainEngine_.Add(customer, "sp_Customer");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }



        public static void ClearData(Control.ControlCollection ctrlCollection)
        {
            try
            {
                foreach (Control ctrl in ctrlCollection)
                {
                    switch (ctrl)
                    {
                        case TextBoxBase textBox:
                            textBox.Text = string.Empty;
                            break;
                        case ComboBox comboBox:
                            comboBox.SelectedIndex = 0; // Clear selection
                         //   comboBox.Text = string.Empty; // Clear text
                            break;
                        default:
                            if (ctrl.HasChildren)
                            {
                                ClearData(ctrl.Controls);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            { 
            }
        }

    }
}