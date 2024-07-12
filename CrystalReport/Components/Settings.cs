using Back_Dr.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            printer1.Items.Clear();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printer1.Items.Add(printer);
            }

            // Optionally select the default printer
            if (printer1.Items.Count > 0)
            {
                printer1.SelectedItem = new PrinterSettings().PrinterName;
            }



        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var dynamicParams = new
                {
                    PrinterName = printer1.Text,
                    TemplateName = templates.Text
                };

                // Assuming MainEngine_.Add<T> method handles execution of stored procedure
                DialogResult result = MessageBox.Show(" You want  to Change Settings ?.", "Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Information);


                // Check the result if needed (depends on MainEngine_.Add implementation)
                if (result == DialogResult.Yes)
                {

                    await MainEngine_.Add<dynamic>(dynamicParams, "UpdatePrinterSetting");
                    MessageBox.Show("Changes saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
