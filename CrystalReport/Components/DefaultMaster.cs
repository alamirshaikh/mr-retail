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
    public partial class DefaultMaster : Form
    {
        public DefaultMaster()
        {
            InitializeComponent();

        }


        private string getcom = "";
        private void DefaultMaster_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (comboBox1.SelectedIndex == 0)
                {


                    getcom = "POS";

                    comboBox2.Items.Clear();

                    comboBox2.Items.AddRange(MainEngine_.GetDataScript<string>("select sub from DefaultS where Comp = 'POS'").ToArray());


                    comboBox3.Items.Clear();

                    comboBox3.Items.AddRange(MainEngine_.GetDataScript<string>("select defaults from DefaultS where Comp = 'POS'").ToArray());


                }
                else if(comboBox1.SelectedIndex == 1)
                {
                    getcom = "BILL";

                    comboBox2.Items.Clear();

                    comboBox2.Items.AddRange(MainEngine_.GetDataScript<string>("select sub from DefaultS where Comp = 'BILL'").ToArray());


                    comboBox3.Items.Clear();

                    comboBox3.Items.AddRange(MainEngine_.GetDataScript<string>("select defaults from DefaultS where Comp = 'BILL'").ToArray());


                }
                else if (comboBox1.SelectedIndex == 2)
                {

                    getcom = "PRINTVIEW";


                    comboBox2.Items.Clear();

                    comboBox2.Items.AddRange(MainEngine_.GetDataScript<string>("select sub from DefaultS where Comp = 'PRINTVIEW'").ToArray());


                    comboBox3.Items.Clear();

                    comboBox3.Items.AddRange(MainEngine_.GetDataScript<string>("select defaults from DefaultS where Comp = 'PRINTVIEW'").ToArray());


                }
           


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {


                var dyn = new
                {

                    Comp = getcom,
                    sub  = comboBox2.Text,
                    defaults = comboBox3.Text

                };

               await MainEngine_.Add<dynamic>(dyn,"MasterUpdate");
 
                MessageBox.Show("Succsfully Updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"errr",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
        }
    }
}
