using Back_Dr.Models;
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
    public partial class Itemlist : UserControl
    {
        private int srn;
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        private int count = 0;
        public Itemlist()
        {
            InitializeComponent();
               }


        private List<ItemModels> fetchedData; // Declare as a class variable

        private async void LoadInitialData()
        {
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetData<ItemModels>("spGetProducts"));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example
        }

        private void DisplayRecords(int startIndex, int count)
        {
            for (int i = startIndex; i < Math.Min(startIndex + count, fetchedData.Count); i++)
            {
                dataGridView1.Rows.Add(i + 1, fetchedData[i].ITEM_NAME, fetchedData[i].UNIT,fetchedData[i].MRP, fetchedData[i].SALE_PRICE, fetchedData[i].CGST, fetchedData[i].SGST, fetchedData[i].IGST, fetchedData[i].STOCK,fetchedData[i].Color,fetchedData[i].Style);
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

        private    List<ItemModels> GetNextPage(string searchTerm, int pageNumber,int size)

        {

           
            // Get the next page of data based on the search term and page number
            if (string.IsNullOrEmpty(searchTerm))
            {
                return   MainEngine_.GetDataScript<ItemModels>("SELECT * FROM Product_Item ORDER BY ID OFFSET '"+pageNumber * size+"' ROWS FETCH NEXT '"+size+"' ROWS ONLY").ToList();
            }
            else
            {
                return   MainEngine_.GetDataScript<ItemModels>($"SELECT * FROM Product_Item WHERE ITEM_NAME LIKE '%{searchTerm}%' ORDER BY ID OFFSET {pageNumber * size} ROWS FETCH NEXT {size} ROWS ONLY");
            }
        }

        private async Task LoadData(string searchTerm)
        {
            // Fetch data based on the search term and current page number

            List<ItemModels> data = MainEngine_.GetDataScript<ItemModels>($"SELECT * FROM Product_Item WHERE ITEM_NAME LIKE '%{searchTerm}%'");

            // Add fetched data to DataGridView
            dataGridView1.Rows.Clear();
            foreach (var item in data.Take(40))
            {
                count++;
                //dataGridView1.Rows.Add(count, item.ITEM_NAME, item.UNIT, item.SALE_PRICE, item.STOCK);
                dataGridView1.Rows.Add(count, item.ITEM_NAME, item.UNIT, item.MRP, item.SALE_PRICE, item.CGST, item.SGST, item.IGST, item.STOCK,item.Color,item.Style);

            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                count = i - 1;
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





        private void Itemlist_Load(object sender, EventArgs e)
        {
            try
            {

                // Add "Sr.No" column
                //DataGridViewTextBoxColumn srNoColumn = new DataGridViewTextBoxColumn();
                //srNoColumn.HeaderText = "Sr.No";
                //srNoColumn.Name = "colSrNo";
                //dataGridView1.Columns.Add(srNoColumn);

                //// Add "Item Name" column
                //DataGridViewTextBoxColumn itemNameColumn = new DataGridViewTextBoxColumn();
                //itemNameColumn.HeaderText = "Item Name";
                //itemNameColumn.Name = "colItemName";
                //dataGridView1.Columns.Add(itemNameColumn);

                //// Add "Sell Price" column
                //DataGridViewTextBoxColumn sellPriceColumn = new DataGridViewTextBoxColumn();
                //sellPriceColumn.HeaderText = "Sell Price";
                //sellPriceColumn.Name = "colSellPrice";
                //dataGridView1.Columns.Add(sellPriceColumn);

                //// Add "Stock" column
                //DataGridViewTextBoxColumn stockColumn = new DataGridViewTextBoxColumn();
                //stockColumn.HeaderText = "Stock";
                //stockColumn.Name = "colStock";
                //dataGridView1.Columns.Add(stockColumn);

                //// Add "Date" column
                //DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
                //dateColumn.HeaderText = "Date";
                //dateColumn.Name = "colDate";
                //dataGridView1.Columns.Add(dateColumn);

            


                LoadInitialData();
                label4.Text = PageSize;


            }
            catch (Exception ex)
            { 
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                //int rowCount = dataGridView1.Rows.Count;

                //if (dataGridView1.Rows[0].Cells[0].Value==null)
                //{
                //    dataGridView1.Rows[0].Cells[0].Value = 1;

                //}
                //else
                //{
                //    DataGridViewCell cell = dataGridView1.Rows[e.RowIndex - 1].Cells[0];
                //    if (cell.Value != null && int.TryParse(cell.Value.ToString(), out int lastValue))
                //    {
                //        dataGridView1.Rows[e.RowIndex].Cells[0].Value = (lastValue + 1).ToString();
                //    }
                //}

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchRecords(string searchTerm)
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



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         }

        private async void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    LoadInitialData();
                }
                else
                {
                    await LoadData(textBox1.Text);
                }
            }
            catch (Exception ex)
            {
                 
            }
            
        }

        private void dataGridView1_CellClick1(object sender, DataGridViewCellEventArgs e)
        {

            try
            {

                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Check if the clicked cell is in the "Delete" column
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        DialogResult result = MessageBox.Show("If you want to delete record?", "Record Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {


                            // Remove the row based on the clicked cell's row index
                            MainEngine_.GetDataScript<string>("delete from Product_Item where ITEM_NAME = '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");

                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                            LoadInitialData();
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void dataGridView1_CellContentClick1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Check if the clicked cell is in the "Delete" column
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        DialogResult result = MessageBox.Show("If you want to delete record?", "Record Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {


                            // Remove the row based on the clicked cell's row index
                            MainEngine_.GetDataScript<string>("delete from Product_Item where ITEM_NAME = '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");

                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                        else
                        {

                        }
                    }
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

        private void dataGridView1_CellClick(object sender, EventArgs e)
        {




        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

