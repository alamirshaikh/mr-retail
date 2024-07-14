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
    public partial class Purches_View : UserControl
    {
        public Purches_View()
        {
            InitializeComponent();
            LoadInitialData();
        }

        private int count = 0;
        public static event EventHandler<bool> isOff;
        public static event EventHandler<string> GetIDClient;








        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private List<dynamic> fetchedData; // Declare as a class variable

        private async void LoadInitialData()
        {
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<dynamic>("select * from Bill order by ID desc"));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example
        }

        private void DisplayRecords(int startIndex, int count)
        {
            for (int i = startIndex; i < Math.Min(startIndex + count, fetchedData.Count); i++)
            {
                gunaDataGridView1.Rows.Add(i + 1, fetchedData[i].TAX, fetchedData[i].billdate, fetchedData[i].items, fetchedData[i].partiname, fetchedData[i].TotalBill);
            }
        }

        private void DisplayMoreRecords()
        {
            // Example: Load the next 100 records
            int startIndex = gunaDataGridView1.RowCount; // Index from which to start adding new records
            int recordsToLoad = 10; // Number of records to load at a time

            DisplayRecords(startIndex, recordsToLoad);
        }


        private int currentPage = 0; // Track current page number for pagination
        private string currentSearchTerm = ""; // Track current search term
   

        private List<dynamic> GetNextPage(string searchTerm, int pageNumber, int size)

        {


            // Get the next page of data based on the search term and page number
            if (string.IsNullOrEmpty(searchTerm))
            {
                return MainEngine_.GetDataScript<dynamic>("SELECT * FROM Bill ORDER BY ID OFFSET '" + pageNumber * size + "' ROWS FETCH NEXT '" + size + "' ROWS ONLY").ToList();
            }
            else
            {
                return MainEngine_.GetDataScript<dynamic>($"SELECT * FROM Bill WHERE items LIKE '%{searchTerm}%' OR partiname LIKE '%{searchTerm}%'  OR billdate LIKE '%{searchTerm}%' ORDER BY ID OFFSET {pageNumber * size} ROWS FETCH NEXT {size} ROWS ONLY");
            }
        }

        private async Task LoadData(string searchTerm)
        {
            // Fetch data based on the search term and current page number

            List<dynamic> data = MainEngine_.GetDataScript<dynamic>($"SELECT * FROM Bill WHERE items LIKE '%{searchTerm}%' OR partiname LIKE '%{searchTerm}%'  OR billdate LIKE '%{searchTerm}%'");

            // Add fetched data to DataGridView
            gunaDataGridView1.Rows.Clear();
            foreach (var item in data.Take(40))
            {
                count++;
                //dataGridView1.Rows.Add(count, item.ITEM_NAME, item.UNIT, item.SALE_PRICE, item.STOCK);
                gunaDataGridView1.Rows.Add(count, item.TAX, item.billdate, item.items, item.partiname, item.TotalBill);

            }
            for (int i = 0; i < gunaDataGridView1.Rows.Count; i++)
            {
                gunaDataGridView1.Rows[i].Cells[0].Value = i + 1;
                count = i - 1;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Purches_View_Load(object sender, EventArgs e)
        {

        }

        private async void textBox2_TextChanged(object sender, EventArgs e)
        {
            await LoadData(textBox2.Text);
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                GetIDClient?.Invoke(this, StoreRoom.GetData = gunaDataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                isOff?.Invoke(this, true);

                if ((dataGridView2.Rows.Count) - 1 > 0)
                {
                    dataGridView2.Rows.Clear();
                    
                    List<dynamic> list = MainEngine_.GetDataScript<dynamic>("SELECT * FROM Bill JOIN purches_Items ON Bill.BillID = purches_Items.Bill where items = '" + gunaDataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()+ "'");
                    foreach (var item in list)
                    {
                        dataGridView2.Rows.Add(item.billdate, item.partiname, item.description, item.rate, item.qty, item.discount, item.amount);

                    }
                    double totalSum = dataGridView2.Rows.Cast<DataGridViewRow>()
            .Sum(row => Convert.ToDouble(row.Cells[6].Value ?? 0));


                    textBox1.Text = $"Rs.{totalSum}";
                }
                else
                {
                    List<dynamic> list = MainEngine_.GetDataScript<dynamic>("SELECT * FROM Bill JOIN purches_Items ON Bill.BillID = purches_Items.Bill where items = '" + gunaDataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + "'");
                    foreach (var item in list)
                    {
                        dataGridView2.Rows.Add(item.billdate, item.partiname, item.description, item.rate, item.qty, item.discount, item.amount);

                    }

                    double totalSum = dataGridView2.Rows.Cast<DataGridViewRow>()
            .Sum(row => Convert.ToDouble(row.Cells[6].Value ?? 0));


                    textBox1.Text = $"Rs.{totalSum}";

                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
