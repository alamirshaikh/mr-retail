using Back_Dr.Models;
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
    public partial class Barcode : UserControl
    {
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        private int count = 0;
        public Barcode()
        {
            InitializeComponent();
            dataLoader.WorkerReportsProgress = true;

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

            List<FBarcodeModel> fetchedData = MainEngine_.GetDataScript<FBarcodeModel>("select ITEM_NAME,BARCODE from Product_Item");

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
    }
}
