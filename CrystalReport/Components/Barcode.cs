using Back_Dr.Models;
using Back_Dr.Sale;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace CrystalReport.Components
{
    public partial class Barcode : UserControl
    {
        private BackgroundWorker dataLoader = new BackgroundWorker();

        private DataTable dataTable = new DataTable();
        private int count = 0;
        private List<int> selectedRowIds = new List<int>();

        public Barcode()
        {
            InitializeComponent();
            dataLoader.WorkerReportsProgress = true;
            LoadInstalledPrinters();
            dataLoader.DoWork += DataLoader_DoWork;
            dataLoader.RunWorkerCompleted += DataLoader_RunWorkerCompleted;

            if (!dataLoader.IsBusy)
            {
                // Show a loading indicator if necessary
                // For example, display a loading spinner

                // Start the BackgroundWorker
                dataLoader.RunWorkerAsync();
            }
        }





        private void LoadInstalledPrinters()
        {
            printer1.Items.Clear();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printer1.Items.Add(printer);
            }

            // Optionally select the default printer
            if (printer1.Items.Count > 0)
            {
                printer1.SelectedItem = new PrinterSettings().PrinterName;
            }
        }





        private void DataLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Handle any errors that occurred during data fetching
                // For example, display an error message to the user
            }
            else
            {
                // Data fetching completed successfully
                if (e.Result is List<FBarcodeModel> fetchedData)
                {
                    // Clear the existing data and add the new fetched data to the DataGridView
                    dataGridView1.BeginInvoke(new Action(() =>
                    {
                        // Clear the current data
                        dataGridView1.DataSource = null;

                        // Assign the fetched data to the DataGridView
                        dataGridView1.DataSource = new BindingList<FBarcodeModel>(fetchedData);





                        // Hide the loading indicator if necessary
                        // For example, hide the loading spinner
                    }));
                }
            }
        }

        private void DataLoader_DoWork(object sender, DoWorkEventArgs e)
        {

            List<FBarcodeModel> fetchedData = MainEngine_.GetDataScript<FBarcodeModel>("select ID,ITEM_NAME,BARCODE,SALE_PRICE,IDATE from Product_Item");

            // Return the fetched data as the result
            e.Result = fetchedData;
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                 
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void Barcode_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {




            }
            catch (Exception ex)
            {
                 
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                itemname.Text = $"{dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()}";
                bar.Text = $"{dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()}";

                price.Text = $"{dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()}";
                shopname.Text = $"{StoreRoom.ShopNames()}";


              


            }
            catch (Exception ex)
            {
                 
            }
        }

        private void itemname_TextChanged(object sender, EventArgs e)
        {
            try
            {


                prname.Text = itemname.Text;



            }
            catch (Exception ex)
            {
                 
            }
        }

        private void bar_TextChanged(object sender, EventArgs e)
        {
            try
            {

                BarcodeWriter write = new BarcodeWriter()
                {
                    Format = BarcodeFormat.CODE_39

                };
                prbarcode.Image = write.Write(bar.Text);


            }
            catch (Exception ex)
            {

            }

        }

        private void price_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                prprice.Text = price.Text;

            }
            catch (Exception ex)
            {

            }

        }

        private void unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        private void shopname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                prshop.Text = shopname.Text;
            }
            catch (Exception ex)
            {

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                width.Text = numericUpDown1.Value.ToString();
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            height.Text = numericUpDown2.Value.ToString();

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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string Barcodes = "*" + bar.Text + "*";


                BarcodeSingle newItem = new BarcodeSingle
                {
                    Width = Convert.ToDouble(numericUpDown1.Value.ToString()),
                    Height = Convert.ToDouble(numericUpDown2.Value.ToString()),
                    Quantity = Convert.ToInt32(numericUpDown3.Value.ToString()),
                    ItemName = prname.Text,
                    Barcode = Barcodes,
                    Price = Convert.ToDecimal(price.Text),
                    ShopName = shopname.Text,
                    CODE = 1
                };
                 

                ReportBarcode barcode = new ReportBarcode(newItem);
                barcode.Show();



            }
            catch (Exception ex)
            {



             }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
                  // Get the selected rows
            var selectedRows = dataGridView1.SelectedRows;

            // Loop through the rows and update the selectedRowIds list
            selectedRowIds.Clear(); // Clear the list to start fresh
            foreach (DataGridViewRow row in selectedRows)
            {
                int id = Convert.ToInt32(row.Cells["Id"].Value);
                if (!selectedRowIds.Contains(id))
                {
                    selectedRowIds.Add(id);
                }
            }

            // Remove unselected rows from the list
            var currentlySelectedIds = selectedRows.Cast<DataGridViewRow>().Select(row => Convert.ToInt32(row.Cells["Id"].Value)).ToList();
            selectedRowIds = selectedRowIds.Intersect(currentlySelectedIds).ToList();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {




            BarcodeSingle newItem = new BarcodeSingle
            {
                Width = Convert.ToDouble(numericUpDown1.Value.ToString()),
                Height = Convert.ToDouble(numericUpDown2.Value.ToString()),
                Quantity = Convert.ToInt32(numericUpDown3.Value.ToString()),
                ShopName = shopname.Text,
                CODE = 2
            };


            ReportBarcode barcode = new ReportBarcode(newItem, selectedRowIds);
            barcode.Show();

        }
    }
}
