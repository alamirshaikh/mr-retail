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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                 if(textBox1.Text == "bill" && textBox2.Text == "bill123")
                {

                    Form1 f = new Form1();
                    f.Show();

                    this.Hide();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_KeyUp(object sender, KeyEventArgs e)
        {
             
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        { 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // Handle the Enter key
                SendKeys.Send("{TAB}"); // Simulate Tab key press
                return true; // Mark the key as handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Leave(object sender, EventArgs e)
        {
           
                button1.PerformClick();
 

        }

        private void Login_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
