
using Back_Dr.Sale;
using Back_Dr.Sale.Models;
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
    public partial class Customer : UserControl
    {
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        private int count = 0;
        private string gradd;
        private int iterindex;

        public Customer()
        {
            InitializeComponent();
             
        }


        private List<CustomerModel> fetchedData; // Declare as a class variable

        private async void LoadInitialData()
        {
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<CustomerModel>("select * from Customer"));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example
        }

        private void DisplayRecords(int startIndex, int count)
        {
            for (int i = startIndex; i < Math.Min(startIndex + count, fetchedData.Count); i++)
            {
                dataGridView1.Rows.Add(fetchedData[i].id,fetchedData[i].cust_name, fetchedData[i].cust_phone, fetchedData[i].pcity,fetchedData[i].pstate);
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
        private string PageSize = MainEngine_.GetDataScript<int>("select Count(ID) from Customer").FirstOrDefault().ToString();

        private List<CustomerModel> GetNextPage(string searchTerm, int pageNumber, int size)

        {


            // Get the next page of data based on the search term and page number
            if (string.IsNullOrEmpty(searchTerm))
            {
                return MainEngine_.GetDataScript<CustomerModel>("SELECT * FROM Customer ORDER BY ID OFFSET '" + pageNumber * size + "' ROWS FETCH NEXT '" + size + "' ROWS ONLY").ToList();
            }
            else
            {
                return MainEngine_.GetDataScript<CustomerModel>($"SELECT * FROM Customer WHERE cust_name LIKE '%{searchTerm}%' ORDER BY ID OFFSET {pageNumber * size} ROWS FETCH NEXT {size} ROWS ONLY");
            }
        }

        private async Task LoadData(string searchTerm)
        {
            // Fetch data based on the search term and current page number
            int size = Convert.ToInt32(PageSize);
            List<CustomerModel> data = GetNextPage(searchTerm, currentPage, size);

            // Add fetched data to DataGridView
            dataGridView1.Rows.Clear();
            foreach (var item in data)
            { 
                dataGridView1.Rows.Add(item.id, item.cust_name, item.cust_phone, item.pcity,item.pstate);
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





        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
              SendKeys.Send("{TAB}"); // Simulate Tab key press
                return true; // Mark the key as handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }



        private void Customer_Load(object sender, EventArgs e)
        {
            LoadInitialData();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Crys cr = new Crys();
            try
            {
                var customer = new
                {
                    cust_name = cust_name.Text,
                    cust_address = cust_address.Text,
                    cust_service = cust_service.Text,
                    cust_phone = cust_phone.Text,
                    cust_date = DateTime.Now,
                    pcity = pcity.Text,
                    pstate = pstate.Text,
                    Place = pcity.Text

                };

                if (dataGridView1.Rows.Count == 1)
                {


                    await MainEngine_.Add(customer, "sp_Customer");
                     MessageBox.Show("New Customer Added!", "New Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StoreRoom.ClearData(this.Controls);
                    dataGridView1.Rows.Clear();
                    LoadInitialData();
                }
                else
                {

                    if (cr.isDuplicate(dataGridView1, cust_name.Text) == false)
                    {


                      
                        await MainEngine_.Add(customer, "sp_Customer");
                        count++;
                        dataGridView1.Rows.Clear();

                        LoadInitialData();
                        MessageBox.Show("New Customer Added!", "New Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StoreRoom.ClearData(this.Controls);
                    }
                    else
                    {
                        cr.DuplicateValue();
                    }
                }
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StoreRoom.ClearData(this.Controls);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                gradd = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                iterindex = e.RowIndex;

                string rw = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                int id = MainEngine_.GetDataScript<int>("select id from Customer where cust_name = '" + rw + "'").FirstOrDefault();

                List<dynamic> gethist = MainEngine_.GetDataScript<dynamic>("select * from Transactions where REFRANCE_ID = " + id + "");
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    LoadInitialData();
                }
            }
            catch (Exception ex)
            {

            }
        }

            private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("Do you want to Remove Customer?","Customer",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (res ==DialogResult.Yes)
                {


                    MainEngine_.GetDataScript<string>("delete from Customer where ID = " + gradd + "");

                    dataGridView1.Rows.RemoveAt(iterindex);
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

        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    await LoadData(textBox1.Text);
                    //StoreRoom st = new StoreRoom();
                    //st.SearchRecords(textBox1.Text, dataGridView1);
                }
                catch (Exception ex)
                {
                    // Handle the exception appropriately
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
