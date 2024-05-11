
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
    public partial class Parties : UserControl
    {
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        private int count = 0;

        private int sn = 0;
        public Parties()
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
            e.Result = fetchedData;
        }




        private void panel2_Paint(object sender, PaintEventArgs e)
        {
                
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Crys cr = new Crys();
                if (dataGridView1.Rows.Count == 1)
                {


                    var pa = new
                    {
                        pname = partiname.Text,
                        company = company.Text,
                        partiphone = workphone.Text,
                        partimobile = mobile.Text,
                        paninformation = pan.Text,
                        city = textBox2.Text,
                        state = state.Text,
                        address = address.Text,
                        BankName = bankname.Text,
                        ACName = acname.Text,
                        ACNumber = acnumber.Text,
                        IFCCODE = ifc.Text
                    };

                    if (pa.city != "" && pa.partimobile != "" || pa.partiphone != "")
                    {
                        await MainEngine_.Add<dynamic>(pa, "InsertIntoParties");
                        int row = 0;
                        string fone = "0000";

                        if (mobile.Text != "" && workphone.Text == "")
                        {
                            fone = mobile.Text;
                        }
                        else
                        {
                            fone = workphone.Text;
                        }

                        // Check if the specified cell is not empty
                        if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells[dataGridView1.Rows.Count - 1].Value != null &&
                            !string.IsNullOrEmpty(dataGridView1.Rows[0].Cells[dataGridView1.Rows.Count - 1].Value.ToString()))
                        {
                            // If the cell is not empty, parse its value and increment 'row' by 1
                            if (int.TryParse(dataGridView1.Rows[0].Cells[dataGridView1.Rows.Count - 1].Value.ToString(), out int parsedValue))
                            {
                                row = parsedValue + 1;
                            }
                            else
                            {
                                row = 1;
                            }
                        }
                        dataLoader.RunWorkerAsync();
                        MessageBox.Show("Parti Added!", "Parti Add:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StoreRoom.ClearData(this.Controls);
                    }
                }
                else
                {
                    if (cr.isDuplicate(dataGridView1, company.Text)==false)
                    {


                        var pa1 = new
                        {
                            pname = partiname.Text,
                            company = company.Text,
                            partiphone = workphone.Text,
                            partimobile = mobile.Text,
                            paninformation = pan.Text,
                            city = textBox2.Text,
                            state = state.Text,
                            address = address.Text,
                            BankName = bankname.Text,
                            ACName = acname.Text,
                            ACNumber = acnumber.Text,
                            IFCCODE = ifc.Text
                        };

                        if (pa1.city != "" && pa1.partimobile != "" || pa1.partiphone != "")
                        {
                            await MainEngine_.Add<dynamic>(pa1, "InsertIntoParties");
                            int row = 0;
                            string fone = "0000";

                            if (mobile.Text != "" && workphone.Text == "")
                            {
                                fone = mobile.Text;
                            }
                            else
                            {
                                fone = workphone.Text;
                            }

                            // Check if the specified cell is not empty
                            if (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].Cells[dataGridView1.Rows.Count - 1].Value != null &&
                                !string.IsNullOrEmpty(dataGridView1.Rows[0].Cells[dataGridView1.Rows.Count - 1].Value.ToString()))
                            {
                                // If the cell is not empty, parse its value and increment 'row' by 1
                                if (int.TryParse(dataGridView1.Rows[0].Cells[dataGridView1.Rows.Count - 1].Value.ToString(), out int parsedValue))
                                {
                                    row = parsedValue + 1;
                                }
                                else
                                {
                                    row = 1;
                                }
                            }
                            dataLoader.RunWorkerAsync();
                            MessageBox.Show("Parti Added!", "Parti Add:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            StoreRoom.ClearData(this.Controls);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parti Error:", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        
        
        }

        private void Parties_Load(object sender, EventArgs e)
        {
            try
            {
                dataLoader.RunWorkerAsync();
            }
            catch (Exception ex)
            {
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StoreRoom.ClearData(this.Controls);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            StoreRoom st = new StoreRoom();
            st.SearchRecords(textBox3.Text, dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
