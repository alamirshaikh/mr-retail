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
    public partial class CustomerList : UserControl
    {
        public CustomerList()
        {
            InitializeComponent();
        }
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        private int count = 0;
        private string gradd;
        private int iterindex;
        private readonly string _ter;
        public static event EventHandler<string> InvoiceTrs;
        public static event EventHandler<bool> HideOR;




        private List<CustomerModel> fetchedData; // Declare as a class variable

        private async void LoadInitialData()
        {
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<CustomerModel>("select * from Customer"));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example
        }

        private async void LoadSearch(string search)
        {

            dataGridView1.Rows.Clear();
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<CustomerModel>("select * from Customer where cust_name = '"+search+"' OR cust_phone='"+search+"' "));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example
        }




        private void DisplayRecords(int startIndex, int count)
        {
            for (int i = startIndex; i < Math.Min(startIndex + count, fetchedData.Count); i++)
            {
                dataGridView1.Rows.Add(fetchedData[i].id, fetchedData[i].cust_name, fetchedData[i].cust_phone, fetchedData[i].pcity);
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
                dataGridView1.Rows.Add(item.id, item.cust_name, item.cust_phone, item.pcity);
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

        protected virtual void OnDataTransferred(string data)
        {

            InvoiceTrs?.Invoke(this, StoreRoom.GetData = data);
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            LoadInitialData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    dataGridView1.Rows.Clear();
                    LoadInitialData();
                }
                else
                {
                    LoadSearch(textBox1.Text);
                }
            }
            catch (Exception ec)
            {
                 
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Do you want to Remove Customer?", "Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {


                MainEngine_.GetDataScript<string>("delete from Customer where ID = " + gradd + "");

                dataGridView1.Rows.RemoveAt(iterindex);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the RowIndex is valid
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
                {
                    var cellValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                    if (cellValue != null)
                    {
                        OnDataTransferred(cellValue);
                        HideOR?.Invoke(this, true);
             
                        this.FindForm()?.Hide();

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
