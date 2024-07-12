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
    public partial class Expenses : UserControl
    {
        public Expenses()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {

                var dem = new
                {
                id = Convert.ToInt64(textBox4.Text),
                Kharcha = textBox1.Text,
                Amount = decimal.Parse(textBox2.Text),
                date = dateTimePicker3.Text,
                 Name = name.Text,
                 Note = note.Text
                };


                await MainEngine_.Add<dynamic>(dem, "MeraKharcha");

                LoadInvoice();

                textBox1.Text = "";
                textBox2.Text = "";
                note.Text = "";





                if (dataGridView1.Rows.Count > 0)
                {
                    double totalSum = dataGridView1.Rows.Cast<DataGridViewRow>()
                .Sum(row => Convert.ToDouble(row.Cells[2].Value ?? 0));
                    totalamt.Text = totalSum.ToString();

                }


                
            }
            catch (Exception ex)
            {


            }
              
        }



        private async void LoadInvoice()
        {

            dataGridView1.Rows.Clear();

            // Fetch initial set of data
            fetchedData = await Task.Run(() => MainEngine_.GetDataScript<dynamic>("select * from Kharcha where ID = '" +textBox4.Text+ "'"));

            // Show the initial set of records in the DataGridView
            DisplayRecords(0, 100); // Display the first 100 records as an example

        }






        List<dynamic> fetchedData = new List<dynamic>();
        private async void LoadInitialData()
        {
           
                dataGridView1.Rows.Clear();
           
                // Fetch initial set of data
                fetchedData = await Task.Run(() => MainEngine_.GetDataScript<dynamic>("select * from Kharcha where kharcha_date = '" + DateTime.Today.ToString("yyyy/MM/dd") + "'"));

                // Show the initial set of records in the DataGridView
                DisplayRecords(0, 100); // Display the first 100 records as an example
            
            }

        private void DisplayRecords(int startIndex, int count)
        {
            for (int i = startIndex; i < Math.Min(startIndex + count, fetchedData.Count); i++)
            {
                dataGridView1.Rows.Add(fetchedData[i].ID, fetchedData[i].Kharcha_Name, fetchedData[i].Amount, fetchedData[i].kharcha_date);
            }
        }


        private void DisplayMoreRecords()
        {
            // Example: Load the next 100 records
            int startIndex = dataGridView1.RowCount; // Index from which to start adding new records
            int recordsToLoad = 10; // Number of records to load at a time

            DisplayRecords(startIndex, recordsToLoad);
        }
        private string GenerateInvoice()
        {
            string inv = "";
            string newinv = "";
            try
            {
                var sql = "select * from Kharcha";
                var sql1 = $"select * from Exp_name";
                int custsr = MainEngine_.GetDataScript<int>(sql).Count();
                int srn = MainEngine_.GetDataScript<int>(sql).Count();
                srn = srn + 1;
                custsr = custsr + 1;

                inv = $"{DateTime.Now.ToString("yy")}{DateTime.Now.ToString("dd")}{custsr}{srn + 10}";



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return inv;


        }
        private void Expenses_Load(object sender, EventArgs e)
        {
            try
            {




                textBox1.Items.AddRange(MainEngine_.GetDataScript<string>("select Name from Exp_name").ToArray());



               // LoadInitialData();
                textBox4.Text = GenerateInvoice();




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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {



            }
            catch (Exception ex)
            {
                 
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    LoadInitialData();
                    if (dataGridView1.Rows.Count > 0)
                    {
                        double totalSum = dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Sum(row => Convert.ToDouble(row.Cells[2].Value ?? 0));
                        totalamt.Text = totalSum.ToString();

                    }

                }
                else
                {
                    LoadInvoice();
                    if (dataGridView1.Rows.Count > 0)
                    {
                        double totalSum = dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Sum(row => Convert.ToDouble(row.Cells[2].Value ?? 0));
                        totalamt.Text = totalSum.ToString();

                    }
                }
               

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void name_MouseEnter(object sender, EventArgs e)
        {
            textBox4.Text = GenerateInvoice();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
              
                    ReportStd std = new ReportStd(textBox4.Text, "Exp");
                    std.Show();


            }
            catch (Exception ex)
            {
                 
            } 
        }
    }
}
