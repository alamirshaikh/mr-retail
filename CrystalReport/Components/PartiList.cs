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
    public partial class PartiList : UserControl
    {


        public static event EventHandler<string> GetIDClient;
        public static event EventHandler<bool> isOff;

        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        private int count = 0;
        public PartiList()
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
                if (e.Result is List<SupplyerModel> fetchedData)
                {
                    // Clear the existing data and add the new fetched data to the DataGridView
                    dataGridView1.BeginInvoke(new Action(() =>
                    {
                        // Clear the current data
                        dataGridView1.DataSource = null;

                        // Assign the fetched data to the DataGridView
                        dataGridView1.DataSource = new BindingList<SupplyerModel>(fetchedData);





                        // Hide the loading indicator if necessary
                        // For example, hide the loading spinner
                    })); 
                }
            }
        }



        private void DataLoader_DoWork(object sender, DoWorkEventArgs e)
        {

            List<SupplyerModel> fetchedData = MainEngine_.GetDataScript<SupplyerModel>("select * from Parties");

            // Return the fetched data as the result
            count = fetchedData.Count();
            e.Result = fetchedData;
        }




        private void PartiList_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSize = false;
            int c  = MainEngine_.GetDataScript<int>("select Count(ID) from Parties").FirstOrDefault();

            textBox1.Text = c.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            StoreRoom st = new StoreRoom();
            st.SearchRecords(textBox2.Text, dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                GetIDClient?.Invoke(this, StoreRoom.GetData=dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                isOff?.Invoke(this, true);

            }
            catch (Exception ex)
            {

            }

        }
    }
}
