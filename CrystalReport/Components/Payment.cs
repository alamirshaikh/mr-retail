using Back_Dr.Models;
using Back_Dr.Sale;
using Dr.Sale.Components;
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
    public partial class Payment : UserControl
    {
        public Payment()
        {
            InitializeComponent();
            PartiList.GetIDClient += PartiList_GetIDClient;
        }

        private void PartiList_GetIDClient(object sender, string e)
        {
            textBox1.Text = StoreRoom.GetData;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                View VIEW = new View("Payment","Nothing");
                VIEW.Show();

                dataGridView1.Rows.Clear();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                List<SupplyerModel> fetchedData = MainEngine_.GetDataScript<SupplyerModel>("select * from Parties where ID = '"+textBox1.Text+"'");

                foreach (var item in fetchedData)
                {
                    comboBox1.Text = item.company;
                    richTextBox2.Text = item.address;
                    textBox2.Text = item.partimobile;

                }

                StoreRoom sr = new StoreRoom();
                pay.Text = sr.Balance(comboBox1.Text).ToString();
                LoadInitialData(comboBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private string GetTrans()
        {
            string inv = "";
            try
            {
                var sql = "select * from Ledger_Supplier";
                var sql1 = $"select * from Payment";
                int custsr = MainEngine_.GetDataScript<int>(sql).Count();
                int srn = MainEngine_.GetDataScript<int>(sql).Count();
                srn = srn + 1;
                custsr = custsr + 1;

                inv = $"SP-{DateTime.Now.ToString("yy")}{DateTime.Now.ToString("MM")}/{DateTime.Now.ToString("dd")}{custsr}{srn}";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return inv;


        }






        private List<PaymentModel> fetchedData; // Declare as a class variable

        private async void LoadInitialData(string name)
        {
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<PaymentModel>("select * from payment where name_off = '"+name+"' "));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example
        }

        private void DisplayRecords(int startIndex, int count)
        {
            for (int i = startIndex; i < Math.Min(startIndex + count, fetchedData.Count); i++)
            {
                dataGridView1.Rows.Add(fetchedData[i].TransactionNo, fetchedData[i].name_off, fetchedData[i].TranDate, fetchedData[i].PaymentMode,fetchedData[i].Amount);
            }
        }

        private void DisplayMoreRecords()
        {
            // Example: Load the next 100 records
            int startIndex = dataGridView1.RowCount; // Index from which to start adding new records
            int recordsToLoad = 10; // Number of records to load at a time

            DisplayRecords(startIndex, recordsToLoad);
        }


        private int currentPage = 0; // Track current page number for pagination
        private string currentSearchTerm = ""; // Track current search term
        private string PageSize = MainEngine_.GetData<int>("TotalItems").FirstOrDefault().ToString();
        private object[] count;

        private List<PaymentModel> GetNextPage(string searchTerm, int pageNumber, int size)

        {


            // Get the next page of data based on the search term and page number
            if (string.IsNullOrEmpty(searchTerm))
            {
                return MainEngine_.GetDataScript<PaymentModel>("SELECT * FROM payment ORDER BY ID OFFSET '" + pageNumber * size + "' ROWS FETCH NEXT '" + size + "' ROWS ONLY").ToList();
            }
            else
            {
                return MainEngine_.GetDataScript<PaymentModel>($"SELECT * FROM payment WHERE name_off LIKE '%{searchTerm}%' ORDER BY ID OFFSET {pageNumber * size} ROWS FETCH NEXT {size} ROWS ONLY");
            }
        }

        private async Task LoadData(string searchTerm)
        {
            // Fetch data based on the search term and current page number
            int size = Convert.ToInt32(PageSize);
            List<PaymentModel> data = GetNextPage(searchTerm, currentPage, size);

            // Add fetched data to DataGridView
            dataGridView1.Rows.Clear();
            foreach (var item in data)
            { 
                dataGridView1.Rows.Add(item.TransactionNo, item.name_off, item.TranDate, item.PaymentMode,item.Amount);
            }
           
        }


        private async void SearchAndLoad(string searchTerm)
        {
            // Clear existing data before performing a new search
            dataGridView1.Rows.Clear();

            // Reset page number for new search
            currentPage = 0;

            currentSearchTerm = searchTerm;

            // Load data based on the search term
            await LoadData(searchTerm);
        }







        private void Payment_Load(object sender, EventArgs e)
        {
            
            textBox3.Text = GetTrans();
           
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (StoreRoom.Dialog("If you want to pay ?", "Supplier Amount") == DialogResult.Yes)
            {

                try
                {
                    var dynamic = new
                    {

                        NameOf = comboBox1.Text,
                        TransactionNo = GetTrans(),
                        TranDate = dateTimePicker1.Value,
                        PaymentMode = comboBox2.Text,
                        Amount = Convert.ToDecimal(amt.Text)

                    };

                    await MainEngine_.Add<dynamic>(dynamic, "InsertPayment");
                    MessageBox.Show("Succefully Paid amount");
                    //StoreRoom.ClearData(this.Controls);
                    textBox3.Text = GetTrans();
                    pay.Text = (decimal.Parse(pay.Text) - decimal.Parse(amt.Text)).ToString();
                  

                    dataGridView1.Rows.Add(textBox3.Text, comboBox1.Text, dateTimePicker1.Value, comboBox2.Text, amt.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else
            {
            }
        }

        private void amt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(Convert.ToDecimal(pay.Text) < Convert.ToDecimal(amt.Text))
                {
                    amtind.Text = "Amount More than balance";
                }
                else
                {
                    amtind.Text = "";
                }
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (dataGridView1.DisplayedRowCount(false) + dataGridView1.FirstDisplayedScrollingRowIndex >= dataGridView1.RowCount)
            {
                // Fetch more data and display when scrolled to the bottom
                DisplayMoreRecords();
            }
        }

        private   void textBox4_TextChanged(object sender, EventArgs e)
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
