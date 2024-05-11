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
                Kharcha = textBox1.Text,
                Amount = decimal.Parse(textBox2.Text),
                date = dateTimePicker3.Text
                };


                await MainEngine_.Add<dynamic>(dem, "MeraKharcha");

                LoadInitialData();

            }
            catch (Exception ex)
            {


            }
              
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
        private void Expenses_Load(object sender, EventArgs e)
        {
            try
            {
                LoadInitialData();

                if(dataGridView1.Rows.Count > 0 )
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
    }
}
