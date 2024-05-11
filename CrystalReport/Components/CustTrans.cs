using Back_Dr.Sale;
using Back_Dr.Sale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class CustTrans : UserControl
    {


        public CustTrans()
        {
            InitializeComponent();
        }

        public int GetID { get; set; }

        private List<dynamic> fetchedData; // Declare as a class variable

        private async void LoadInitialData()
        {
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<dynamic>($"select Cust_Name,Last_Date,InvoiceId,Quantity,Amount,PayMode,Paid from CustomerTransactions where Last_Date = '{DateTime.Today.ToString("yyyy/MM/dd")}' "));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example
        }

        private void DisplayRecords(int startIndex, int count)
        {
            for (int i = startIndex; i < Math.Min(startIndex + count, fetchedData.Count); i++)
            {
            gunaDataGridView2.Rows.Add(fetchedData[i].Cust_Name, fetchedData[i].Last_Date, fetchedData[i].InvoiceId, fetchedData[i].Quantity, fetchedData[i].Amount, fetchedData[i].PayMode, fetchedData[i].Paid);

            }
        }

        private void DisplayMoreRecords()
        {
            // Example: Load the next 100 records
            int startIndex = gunaDataGridView2.RowCount; // Index from which to start adding new records
            int recordsToLoad = 10; // Number of records to load at a time

            DisplayRecords(startIndex, recordsToLoad);
        }


        private int currentPage = 0; // Track current page number for pagination
        private string currentSearchTerm = ""; // Track current search term
        private string PageSize = MainEngine_.GetDataScript<int>("select Count(ID) from CustomerTransactions").FirstOrDefault().ToString();

        private List<dynamic> GetNextPage(string searchTerm, int pageNumber, int size)

        {


            // Get the next page of data based on the search term and page number
            if (string.IsNullOrEmpty(searchTerm))
            {
                return MainEngine_.GetDataScript<dynamic>("SELECT Cust_Name,Last_Date,InvoiceId,Quantity,Amount,PayMode,Paid FROM CustomerTransactions ORDER BY ID OFFSET '" + pageNumber * size + "' ROWS FETCH NEXT '" + size + "' ROWS ONLY").ToList();
            }
            else
            {
                return MainEngine_.GetDataScript<dynamic>($"SELECT Cust_Name,Last_Date,InvoiceId,Quantity,Amount,PayMode,Paid FROM CustomerTransactions WHERE Cust_Name LIKE '%{searchTerm}%' ORDER BY ID OFFSET {pageNumber * size} ROWS FETCH NEXT {size} ROWS ONLY");
            }
        }

        private async Task LoadData(string searchTerm)
        {
            // Fetch data based on the search term and current page number
            int size = Convert.ToInt32(PageSize);
            List<dynamic> data = GetNextPage(searchTerm, currentPage, size);
             

            // Add fetched data to DataGridView
            gunaDataGridView2.Rows.Clear();
            foreach (var item in data)
            {
                gunaDataGridView2.Rows.Add(item.Cust_Name, item.Last_Date, item.InvoiceId, item.Quantity, item.Amount, item.PayMode, item.Paid);
            }

        }


        private void CustTrans_Load(object sender, EventArgs e)
        {
            LoadInitialData();
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            

            }
            catch (Exception ex)
            {
                 
            }
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void gunaDataGridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
              
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void gunaDataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ReportStd rp = new ReportStd(gunaDataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString(), textBox1.Text);
                rp.Show();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void gunaDataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                DisplayMoreRecords();
            }
            catch (Exception ex)
            {

             }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {

            this.Controls.Clear();
           Customer item = new Customer();
            item.Dock = DockStyle.Fill;
            this.Controls.Add(item);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {

                


                    List<dynamic> customers = MainEngine_.GetDataScript<dynamic>("select Cust_Name from CustomerTransactions where Cust_Name  LIKE '%" + textBox1.Text + "%'").ToList();
                    foreach (var item in customers.Take(10))
                    {
                        textBox1.Items.Add(item.Cust_Name);

                    }
                }
            
            catch (Exception ex)
            {

            }
        }

        private void textBox1_SelectedIndexChanged(object sender, EventArgs e)
        {




            List<dynamic> getdata = MainEngine_.GetDataScript<dynamic>("select Cust_Name,Last_Date,InvoiceId,Quantity,Amount,PayMode,Paid from CustomerTransactions where Cust_Name ='" +textBox1.Text+ "' ").ToList();

            gunaDataGridView2.Rows.Clear();
            foreach (var item in getdata)
            {
                gunaDataGridView2.Rows.Add(item.Cust_Name, item.Last_Date, item.InvoiceId, item.Quantity, item.Amount, item.PayMode, item.Paid);

            }

            decimal val = MainEngine_.GetDataScript<decimal>("select Balance from Customer  where cust_name  = '" + textBox1.Text + "' ").FirstOrDefault();

            totalamt.Text = val.ToString();
            if (val > 0)
            {
                totalamt.ForeColor = Color.LawnGreen;
            }
            else
            {
                totalamt.ForeColor = Color.IndianRed;

            }
            textBox1.Items.Clear();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                 
            }
        }
    }
}
