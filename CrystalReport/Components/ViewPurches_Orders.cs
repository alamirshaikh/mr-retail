using Back_Dr.Sale;
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
    public partial class ViewPurches_Orders : UserControl
    {
        public ViewPurches_Orders()
        {
            InitializeComponent();
        }



        private List<dynamic> fetchedData; // Declare as a class variable
        private int count = 0;  
        private async void LoadInitialData()
        {
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<dynamic>("select * from Purches_Order"));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example
        }

        private void DisplayRecords(int startIndex, int count)
        {
            for (int i = startIndex; i < Math.Min(startIndex + count, fetchedData.Count); i++)
            {
                dataGridView1.Rows.Add(i + 1, fetchedData[i].Status, fetchedData[i].billdate, "", fetchedData[i].BillID, fetchedData[i].contact, fetchedData[i].partiname, fetchedData[i].state, fetchedData[i].address);
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
 

        private List<dynamic> GetNextPage(string searchTerm, int pageNumber, int size)

        {


            // Get the next page of data based on the search term and page number
            if (string.IsNullOrEmpty(searchTerm))
            {
                return MainEngine_.GetDataScript<dynamic>("SELECT * FROM Purches_Order ORDER BY ID OFFSET '" + pageNumber * size + "' ROWS FETCH NEXT '" + size + "' ROWS ONLY").ToList();
            }
            else
            {
                return MainEngine_.GetDataScript<dynamic>($"SELECT * FROM Purches_Order WHERE partiname LIKE '%{searchTerm}%' ORDER BY ID OFFSET {pageNumber * size} ROWS FETCH NEXT {size} ROWS ONLY");
            }
        }

        private async Task LoadData(string searchTerm)
        {
            // Fetch data based on the search term and current page number

            List<dynamic> data = MainEngine_.GetDataScript<dynamic>($"SELECT * FROM Purches_Order WHERE partiname LIKE '%{searchTerm}%' OR BillID LIKE '%{searchTerm}%' OR Contact LIKE '%{searchTerm}%'  ");

            // Add fetched data to DataGridView
            dataGridView1.Rows.Clear();
            foreach (var item in data.Take(40))
            {
                count++;
                //dataGridView1.Rows.Add(count, item.ITEM_NAME, item.UNIT, item.SALE_PRICE, item.STOCK);
           
                
                    dataGridView1.Rows.Add(
                        count + 1,
                        item.Status,
                        item.billdate,
                        "",
                        item.BillID,
                        item.contact,
                        item.partiname,
                        item.state,
                        item.address
                    );
                   
                


            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                count = i - 1;
            }
        }




        private void ViewPurches_Orders_Load(object sender, EventArgs e)
        {
            LoadInitialData();
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadData(textBox1.Text);
            }
            catch (Exception ex)
            {
                 
            }
        }
    }
}
