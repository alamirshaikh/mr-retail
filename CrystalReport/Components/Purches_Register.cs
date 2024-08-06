using Back_Dr.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class Purches_Register : Form
    {
        public Purches_Register()
        {
            InitializeComponent();
        }




        public List<dynamic> GetData()
        {

            List<dynamic> datas = MainEngine_.GetDataScript<dynamic>($"select * from Bill s inner join purches_Items i  ON i.Bill = s.items where TAX = 'GST' AND  s.billdate >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND s.billdate <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}' ").ToList();


            return datas;

        }





        private void gunaButton1_Click(object sender, EventArgs e)
        {
            try
            {
                main_datagrid.Rows.Clear();
                int count = 0;
                foreach (var item in GetData())
                {
                    count++;

                    main_datagrid.Rows.Add(count, item.BillID, item.partiname, item.HSN, item.description, item.qty, item.per, item.rate, item.discount, item.billdate, item.CGST, item.SGST, item.IGST, item.CGST, item.SGST, item.IGST, item.sub_total, item.other, item.discount, item.TotalBill, item.Color, item.Size, item.msg, item.exp,item.MRP,item.Sale);

                }


            }
            catch (Exception ex)
            {

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



        private void gunaButton3_Click(object sender, EventArgs e)
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
                    worksheet.Name = "Purches Register Report";

                    // Export the column headers to Excel
                    for (int i = 1; i < main_datagrid.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = main_datagrid.Columns[i - 1].HeaderText;
                    }

                    // Export the rows to Excel
                    for (int i = 0; i < main_datagrid.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < main_datagrid.Columns.Count; j++)
                        {
                            if (main_datagrid.Rows[i].Cells[j].Value != null)
                            {
                                worksheet.Cells[i + 2, j + 1] = main_datagrid.Rows[i].Cells[j].Value.ToString();
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


            // Method to release COM objects

        }
    }
}
