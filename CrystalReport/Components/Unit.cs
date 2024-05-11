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

namespace Dr.Sale.Components
{
    public partial class Unit : Form
    {
        public Unit()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Unit_Load(object sender, EventArgs e)
        {
            try

            {
                
               
                listBox1.Items.AddRange(MainEngine_.GetData<string>("spUnitSelect").ToArray());

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var dynami = new
                {
                    Command = 0,
                    Unit = textBox1.Text

                };

                await MainEngine_.Add(dynami,"spUnit");
                MessageBox.Show("Succefully Add Unit!", "Add Unit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Items.Add(textBox1.Text);
                StoreRoom.ClearData(this.Controls);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var dynami = new
                {
                    Command = 1,
                    Unit = listBox1.SelectedItem.ToString()

                };

                await MainEngine_.Add(dynami,"spUnit");
                MessageBox.Show("Delete Unit!", "Delete Unit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
