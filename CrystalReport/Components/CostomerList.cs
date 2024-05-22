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
    public partial class CostomerList : UserControl
    {

            private readonly string _ter;
            public static event EventHandler<string> InvoiceTrs;
            public static event EventHandler<bool> HideOR;





        private int srn;
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        public CostomerList(string _terms)
        {
            _ter = _terms;
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
            try {
                if (e.Error != null)
                {
                    // Handle any errors that occurred during data fetching
                    // For example, display an error message to the user
                }
                else
                {
                    // Data fetching completed successfully
                    if (e.Result is List<InvoiceModels> fetchedData)
                    {
                        // Clear the existing data and add the new fetched data to the DataGridView
                        data.BeginInvoke(new Action(() =>
                        {
                            // Clear the current data
                            data.DataSource = null;

                            // Assign the fetched data to the DataGridView
                            data.DataSource = new BindingList<InvoiceModels>(fetchedData);



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
                }catch(Exception ex)
            {

            }
                
            }

        private void DataLoader_DoWork(object sender, DoWorkEventArgs e)
        {

            List<InvoiceModels> fetchedData = MainEngine_.GetDataScript<InvoiceModels>("select  Top(100) * from SInvoice  order by id DESC");

            // Return the fetched data as the result
            e.Result = fetchedData;
        }



        private void CostomerList_Load(object sender, EventArgs e)
        {
            
        }


        protected virtual void OnDataTransferred(string data)
        {
             
            InvoiceTrs?.Invoke(this, StoreRoom.GetData= data);
        }
        private int rowindex;

        private void data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowindex = e.RowIndex;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {


                

            List<InvoiceModels> fetchedData = MainEngine_.GetDataScript<InvoiceModels>("exec FindInvoice '"+textBox1.Text+"'");

                data.BeginInvoke(new Action(() =>
                {
                    // Clear the current data
                    data.DataSource = null;

                    // Assign the fetched data to the DataGridView
                    data.DataSource = new BindingList<InvoiceModels>(fetchedData);
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        data.Rows[i].Cells[0].Value = i + 1;
                        srn = i + 1;
                    }


                    // Hide the loading indicator if necessary
                    // For example, hide the loading spinner
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void data_DoubleClick(object sender, EventArgs e)
        {
            try
            {

               

                    if (_ter == "SaleReport")
                    {
                        ReportStd rp = new ReportStd(data.Rows[rowindex].Cells[1].Value.ToString(), textBox1.Text);
                        rp.Show();
                    }

                    else if (_ter == "ledger")
                    {
                        OnDataTransferred(data.Rows[rowindex].Cells[1].Value.ToString());
                        HideOR?.Invoke(this, true);
                    }
                    else
                    {
                        OnDataTransferred(data.Rows[rowindex].Cells[1].Value.ToString());
                        HideOR?.Invoke(this, true);
                    }
              
                
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (data.SelectedRows.Count > 0)
            {

                int selectedIndex = data.SelectedRows[0].Index;
                string cellValue = data.Rows[selectedIndex].Cells[1].Value.ToString();
                this.Controls.Clear();

                Invoice item = new Invoice(cellValue);
                item.Dock = DockStyle.Fill;
                this.Controls.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedIndex = data.SelectedRows[0].Index;

            MainEngine_.GetDataScript<dynamic>("delete from SInvoice where items='"+data.Rows[selectedIndex].Cells[1].Value.ToString()+"'");
            data.Rows.RemoveAt(selectedIndex);

        }
    }
}
