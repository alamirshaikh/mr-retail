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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class Stock : UserControl
    {
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        private int srn;
        private int index;
        public Stock()
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
                dataGridView1.Rows.Add(fetchedData[i].ID, fetchedData[i].ITEM_NAME, fetchedData[i].UNIT, fetchedData[i].SALE_PRICE, fetchedData[i].COST_PRICE, fetchedData[i].BARCODE , fetchedData[i].STOCK, fetchedData[i].CGST, fetchedData[i].SGST, fetchedData[i].IGST);
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
        private int count;

        private List<ItemModels> GetNextPage(string searchTerm, int pageNumber, int size)

        {


            // Get the next page of data based on the search term and page number
            if (string.IsNullOrEmpty(searchTerm))
            {
                return MainEngine_.GetDataScript<ItemModels>("SELECT * FROM Product_Item ORDER BY ID OFFSET '" + pageNumber * size + "' ROWS FETCH NEXT '" + size + "' ROWS ONLY").ToList();
            }
            else
            {
                return MainEngine_.GetDataScript<ItemModels>($"SELECT * FROM Product_Item WHERE ITEM_NAME LIKE '%{searchTerm}%' ORDER BY ID OFFSET {pageNumber * size} ROWS FETCH NEXT {size} ROWS ONLY");
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
                dataGridView1.Rows.Add(count, item.ITEM_NAME, item.UNIT, item.SALE_PRICE,item.COST_PRICE,item.BARCODE, item.STOCK,item.CGST,item.SGST,item.IGST);
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





        private void label1_Click(object sender, EventArgs e)
        {

        }

        private   void Stock_Load(object sender, EventArgs e)
        {
            try
            {

                List<string> unit = MainEngine_.GetDataScript<string>("select unit from Unit");
                units.Items.AddRange(unit.ToArray());
                LoadInitialData();
            }
            catch (Exception ex)
            {
                 
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {


                var dy = new
                {
                    item =INAME.Text,
                    SALE = SALE.Text,
                    COST = COST.Text,
              
                    UNIT = units.Text,
                    BARCODE = barcode.Text,
                    STOCK = stt.Text,
                    ID = id.Text

                };

                await MainEngine_.Add<dynamic>(dy, "UpdateStock");
                MessageBox.Show("Successfully Stock Add!","Stock",MessageBoxButtons.OK,MessageBoxIcon.Information);


            
                dataGridView1.Rows[index].Cells[5].Value = barcode.Text;
                dataGridView1.Rows[index].Cells[6].Value = stt.Text;
                dataGridView1.Rows[index].Cells[1].Value = INAME.Text;
                dataGridView1.Rows[index].Cells[3].Value = SALE.Text;
                dataGridView1.Rows[index].Cells[4].Value = COST.Text;

                barcode.Text = "";
                units.Text = "";
                stt.Text = "";
                id.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Stock",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                barcode.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch (Exception ex)
            {
                 
            }
            try
            {
                id.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                INAME.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                units.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                SALE.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                COST.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                stt.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                
            
              
               


                index = e.RowIndex;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the entered character is not a digit or a control character, ignore it
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                StoreRoom st = new StoreRoom();
                st.SearchRecords(textBox1.Text, dataGridView1);
            }
            catch (Exception ex)
            {
                 
            }
        }

        private async void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text== "")
                {
                    await LoadData(textBox1.Text);

                }
               
            }
            catch (Exception ex)
            {


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

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {

            if (dataGridView1.DisplayedRowCount(false) + dataGridView1.FirstDisplayedScrollingRowIndex >= dataGridView1.RowCount)
            {
                // Fetch more data and display when scrolled to the bottom
                DisplayMoreRecords();
            }

        }

        private async void textBox1_TextChanged_2(object sender, EventArgs e)
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
    }
}
