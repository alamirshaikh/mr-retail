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

namespace CrystalReport
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Password_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $"update UserAccount set password = '{newpassword.Text}' where password='{oldpassword.Text}'";
                MainEngine_.GetDataScript<string>(sql).FirstOrDefault();

                MessageBox.Show("Successfully Update Password!", "User Password!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Cheack your old password","Password ?",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                 
            }
        }
    }
}
