using Back_Dr.Sale;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport
{
    public partial class Import_Records : Form
    {
        private DataTable _table;

        public Import_Records(DataTable table)
        {
            InitializeComponent();

            _table = table;
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            // Ensure the DataGridView allows both horizontal and vertical scrolling
            dataGridView1.ScrollBars = ScrollBars.Both;

            // Set the AutoSize mode to None to enable scrolling
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Optionally set column and row sizes
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 100; // Adjust width as needed
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 22; // Adjust height as needed
            }
        }





        private void Import_Records_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = _table;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void gunaButton2_Click(object sender, EventArgs e)
        {
            
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                    Title = "Select an Excel File"
                };

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;

                    // Display the filename in a label
                    string path = System.IO.Path.GetFileName(filePath);
                    lblFileName.Text = $"Loading: {path}";

                    // Show the progress bar
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.Visible = true;

            
             
                    // Load the Excel file asynchronously
                    DataTable tb = await Task.Run(() => StoreRoom.AddExcel(filePath));
  
               
                    // Hide the progress bar
                    progressBar1.Visible = false;

                    // Update the DataGridView
                    dataGridView1.DataSource = tb;

                    lblFileName.Text = $"Loaded: {path}";
                }
            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
              // Convert DataGridView to DataTable
                DataTable tb = ConvertDataGridViewToDataTable(dataGridView1);

                // Perform bulk insert operation
                MainEngine_.BulkInsertDataTable(tb);

                // Show success message
                MessageBox.Show("Successfully Products Added!", "Product Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Method to convert DataGridView to DataTable
            private DataTable ConvertDataGridViewToDataTable(DataGridView dgv)
            {
                DataTable dt = new DataTable();

                // Add columns to DataTable
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    dt.Columns.Add(column.Name, column.ValueType);
                }

                // Add rows to DataTable
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dr[cell.ColumnIndex] = cell.Value ?? DBNull.Value;
                    }
                    dt.Rows.Add(dr);
                }

                return dt;
            }

        }
    }
