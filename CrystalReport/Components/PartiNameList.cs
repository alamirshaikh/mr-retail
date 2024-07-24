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
    public partial class PartiNameList : UserControl
    {


        public static event EventHandler<string> IDSupp;
        public static event EventHandler<bool> HideOR;

        private int srn;
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        public PartiNameList()
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
            try
            {
                if (e.Error != null)
                {
                    // Handle any errors that occurred during data fetching
                    // For example, display an error message to the user
                }
                else
                {
                    // Data fetching completed successfully
                    if (e.Result is List<PartiNameLIst> fetchedData)
                    {
                        // Clear the existing data and add the new fetched data to the DataGridView
                        data.BeginInvoke(new Action(() =>
                        {
                            // Clear the current data
                            data.DataSource = null;

                            // Assign the fetched data to the DataGridView
                            data.DataSource = new BindingList<PartiNameLIst>(fetchedData);




                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                data.Rows[i].Cells[0].Value = i + 1;
                                srn = i + 1;
                            }


                            // Hide the loading indicator if necessary
                            // For example, hide the loading spinner
                        }));
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void DataLoader_DoWork(object sender, DoWorkEventArgs e)
        {

            List<PartiNameLIst> fetchedData = MainEngine_.GetDataScript<PartiNameLIst>("select * from Parties");

            // Return the fetched data as the result
            e.Result = fetchedData;
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                /*
                /*StoreRoom st = new StoreRoom();
                st.SearchRecords(textBox1.Text, data);
                
                if (textBox1.Text == "")
                {
                    data.Rows.Clear();
                    LoadInitialData();
                }
                else
                {
                    LoadSearch(textBox1.Text);
                }

                */

            }
            catch (Exception ex)
            {


            }
        }

        private void PartiNameList_Load(object sender, EventArgs e)
        {

        }
        private async void LoadSearch(string search)
        {

           /* data.Rows.Clear();
            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<PartiNameLIst>("select * from Customer where cust_name LIKE '%" + search + "%' OR cust_phone LIKE '%" + search + "%' "));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example
        */
            }





        private void data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void data_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                IDSupp?.Invoke(this, StoreRoom.GetData = data.Rows[e.RowIndex].Cells[1].Value.ToString());
                HideOR?.Invoke(this, true);

            }
            catch (Exception ex)
            {

            }

        }
    }
}
