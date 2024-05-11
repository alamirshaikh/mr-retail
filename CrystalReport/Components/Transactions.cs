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
    public partial class Transactions : UserControl
    {
        public Transactions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                this.Controls.Clear();
                LoadRotray item = new LoadRotray();
                item.Dock = DockStyle.Fill;
                this.Controls.Add(item);

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Controls.Clear();
            Purches_View item = new Purches_View();
            item.Dock = DockStyle.Fill;
            this.Controls.Add(item);
        }
    }
}
