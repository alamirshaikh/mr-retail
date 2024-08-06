using Back_Dr.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class GSTR1B : Form
    {
        public GSTR1B()
        {
            InitializeComponent();
        }
        public List<dynamic> GetData_OF_B2B()
        {

            List<dynamic> datas = MainEngine_.GetDataScript<dynamic>($"select *  from SInvoice where GSTIN !='' AND invdate >= '{dateTimePicker1.Text} 00:00:00' AND invdate <= '{dateTimePicker2.Text} 23:59:59' ").ToList();


            return datas;

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear the existing rows in the btob DataGridView.
                btob.Rows.Clear();

                // Get the data for B2B
                var b2bData = GetData_OF_B2B();

                // Loop through each item in the B2B data.
                foreach (var item in b2bData)
                {
                    // Ensure date formats are correctly handled.
                    string startDate = DateTime.Parse(dateTimePicker1.Text).ToString("yyyy-MM-dd 00:00:00");
                    string endDate = DateTime.Parse(dateTimePicker2.Text).ToString("yyyy-MM-dd 23:59:59");

                    // Retrieve GST data for the current item using the invoice date range.
                    string gstQuery = $"SELECT Sum(GST) FROM Sale_Items WHERE invdate >= '{startDate}' AND invdate <= '{endDate}' AND Invoice = '"+item.items+"' ";
                    var gstValue = MainEngine_.GetDataScript<string>(gstQuery).FirstOrDefault();

                    // Retrieve state data for the current customer.
                    string stateQuery = $"SELECT pstate FROM Customer WHERE cust_name = '{item.cust_Name}'";
                    var stateValue = MainEngine_.GetDataScript<string>(stateQuery).FirstOrDefault();

                    // Add the data to the btob DataGridView.
                    btob.Rows.Add(
                        item.GSTIN,
                        item.cust_Name,
                        item.items,
                        item.invdate,
                        item.TotalBill,
                        stateValue,
                        item.TAX_TYPE,
                        gstValue,
                        item.sCGST,
                        item.sSGST,
                        item.sIGST,
                        item.sub_total
                    );
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions by logging or displaying the error message.
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception occurred while releasing object: " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // Open a SaveFileDialog to ask the user where to save the Excel file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.Title = "Save Excel File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Create a new Excel application instance
                    Excel.Application excelApp = new Excel.Application();

                    // Check if Excel is installed and available
                    if (excelApp == null)
                    {
                        MessageBox.Show("Excel is not properly installed.");
                        return;
                    }

                    // Add a new workbook
                    Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                    worksheet.Name = "Sale Register Report";

                    // Export the column headers to Excel
                    for (int i = 1; i < btob.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = btob.Columns[i - 1].HeaderText;
                    }

                    // Export the rows to Excel
                    for (int i = 0; i < btob.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < btob.Columns.Count; j++)
                        {
                            if (btob.Rows[i].Cells[j].Value != null)
                            {
                                worksheet.Cells[i + 2, j + 1] = btob.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }

                    // Save the workbook
                    workbook.SaveAs(filePath);

                    // Clean up
                    workbook.Close(false);
                    excelApp.Quit();

                    // Release the COM objects
                    ReleaseObject(worksheet);
                    ReleaseObject(workbook);
                    ReleaseObject(excelApp);

                    MessageBox.Show("Data Exported Successfully to " + filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
