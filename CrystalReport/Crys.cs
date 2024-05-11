using CrystalDecisions.CrystalReports.Engine;
using System;
using CrystalDecisions.Shared;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport
{
    public class Crys
    {
        public     string InvoiceId { get; set; }
        public    int Type { get; set; }



        public void DuplicateValue()
        {

            MessageBox.Show("Item All Ready Added!", "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

            public  bool isDuplicate(DataGridView dataGridView,string value)
            {
                HashSet<string> uniqueValues = new HashSet<string>();

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    // Assuming you want to check for duplicates based on a specific column (change the column index or name accordingly)
                    string valueToCheck = row.Cells[1].Value?.ToString(); // Replace "ColumnName" with the actual column name
                 
                if(value == valueToCheck)
                {
                    return true;
                }
                 
                }

                // No duplicates found
                return false;
            }




    }
}
