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
    public partial class Mechanic : UserControl
    {
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        private int count = 0;

        private int sn = 0;
        public Mechanic()
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
                if (e.Result is List<MechanicModel> fetchedData)
                {
                    // Clear the existing data and add the new fetched data to the DataGridView
                    dataGridView1.BeginInvoke(new Action(() =>
                    {
                        // Clear the current data
                        dataGridView1.DataSource = null;

                        // Assign the fetched data to the DataGridView
                        dataGridView1.DataSource = new BindingList<MechanicModel>(fetchedData);





                        // Hide the loading indicator if necessary
                        // For example, hide the loading spinner
                    }));
                }
            }
        }

        private void DataLoader_DoWork(object sender, DoWorkEventArgs e)
        {

            List<MechanicModel> fetchedData = MainEngine_.GetData<MechanicModel>("GetMechanic");

            // Return the fetched data as the result
            e.Result = fetchedData;
        }



        private void Mechanic_Load(object sender, EventArgs e)
        {
            try
            {
                panel1.BackColor = Color.FromArgb(45, 63, 85);
                panel1.ForeColor = Color.White;
                panel2.BackColor = Color.FromArgb(64, 64, 64);
                panel2.ForeColor = Color.White;
                panel3.BackColor = Color.Wheat;
                panel4.BackColor = Color.FromArgb(64, 64, 64);
                panel4.ForeColor = Color.White;

                // Set label font and styles
                label1.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
                label1.ForeColor = Color.White;
                label2.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
                label2.ForeColor = Color.White;
                label7.Font = new Font("Yu Gothic UI", 12, FontStyle.Bold);
                label7.ForeColor = Color.White;

                // Set DataGridView styles
                dataGridView1.BackgroundColor = Color.LightGray;
                dataGridView1.GridColor = Color.Black;
         

                // Customize button appearance
                button1.BackColor = Color.Goldenrod;
                button1.ForeColor = Color.Black;
                button1.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

                // Customize ComboBox and TextBox styles
                mac_name.BackColor = Color.White;
                mac_name.Font = new Font("Verdana", 10);
                item_name.BackColor = Color.White;
                item_name.Font = new Font("Verdana", 10);
                cost.BackColor = Color.White;
                cost.Font = new Font("Verdana", 10);
                sale.BackColor = Color.White;
                sale.Font = new Font("Verdana", 10);
            }
            catch (Exception EX)
            {

                throw;
            }
        }

        private void item_name_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var searchTerm = item_name.Text.Trim(); // Trim to remove leading/trailing spaces

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var test = new { ITEM_NAME = searchTerm };
                    // Assuming MainEngine_.GetData supports asynchronous execution
                    List<dynamic> list = MainEngine_.GetData<dynamic>("dbo.spGetitem", test);

                    // Limit the number of results displayed for responsiveness
                    const int maxResults = 10; // Adjust this value based on your needs
                    foreach (var item in list.Take(maxResults))
                    {
                        item_name.Items.Add(item.ITEM_NAME);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void item_name_SelectedIndexChanged(object sender, EventArgs e)
        {
           try
            {
                item_name.Items.Clear();
                var test = new
                {
                    ITEM_NAME = item_name.Text
                };
                List<dynamic> list = MainEngine_.GetData<dynamic>("spselectitem", test);
                foreach (var item in list)
                {
                    sale.Text = Convert.ToString(item.SALE_PRICE);
                    cost.Text = Convert.ToString(item.COST_PRICE);


                }

            }catch(Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
    }

        private async void button1_Click(object sender, EventArgs e)
        {
            try

            {

                if (MainEngine_.GetDataScript<dynamic>("select * from Mechanic where Mechanic_Name = '" + mac_name.Text + "' AND Item_Name = '" + item_name.Text + "'").Count > 0)
                {


                    MainEngine_.GetDataScript<dynamic>("update Mechanic set Item_price = "+Convert.ToDecimal(sale.Text)+ " where Mechanic_Name ='"+mac_name.Text+"' ");

                    dataLoader.RunWorkerAsync();
                    StoreRoom.ClearData(this.Controls);
                }
                else
                {



                    if (StoreRoom.Dialog("If you want to add Mechanic", "Mechanic Items") == DialogResult.Yes)
                    {
                        var dynamo = new
                        {
                            Mechanic_Name = mac_name.Text,
                            Item_Name = item_name.Text,
                            Item_Cost = Convert.ToDecimal(cost.Text),
                            Item_price = Convert.ToDecimal(sale.Text),
                            Date = DateTime.Now
                        };

                        await MainEngine_.Add(dynamo, "AddMechanic");

                        dataLoader.RunWorkerAsync();
                        StoreRoom.ClearData(this.Controls);
                    }
                    else
                    {

                    }

                }


                mac_name.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
             }
        
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            try
            {
                    button1.PerformClick();
             
                }
            catch (Exception ex)
            {
                 
            }
        }

        private void mac_name_TextChanged(object sender, EventArgs e)
        {
            try
            {

                var test = new { cust_name = mac_name.Text };

                List<dynamic> list = MainEngine_.GetData<dynamic>("sp_getCustomer", test).ToList();

                foreach (var item in list)
                {
                    mac_name.Items.Add(item.cust_name);

                }





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void item_name_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }


        private async Task LoadData(string searchTerm)
        {
            // Fetch data based on the search term and current page number

            List<dynamic> data = MainEngine_.GetDataScript<dynamic>($"SELECT * FROM Mechanic WHERE Mechanic_Name LIKE '%{searchTerm}%'");

            // Add fetched data to DataGridView
            dataGridView1.Rows.Clear();
            foreach (var item in data.Take(40))
            {
                count++;
                dataGridView1.Rows.Add(item.Mechanic_Name, item.Data);
            }
            
        }

        private async void textBox1_TextChangedAsync(object sender, EventArgs e)
        {
            await LoadData(textBox1.Text);
        }
    }
            
    
}
