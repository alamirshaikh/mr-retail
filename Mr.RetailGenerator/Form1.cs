using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mr.RetailGenerator
{
    public partial class Form1 : Form
    {
        private string getmac;

        public byte ProductCode { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }




        public string GetKEy()
        {
            getmac = ComputerInfo.GetComputerId();

            FoxLearn.License.KeyManager km = new FoxLearn.License.KeyManager(getmac);
            KeyValuesClass kv = new KeyValuesClass();
            string produckey = string.Empty;




            kv = new KeyValuesClass()
            {
                Type = LicenseType.FULL,
                Header = Convert.ToByte(9),
                Footer = Convert.ToByte(6),
                ProductCode = (byte)ProductCode,
                Edition = Edition.ENTERPRISE,
                Version = 1
            };
            if (!km.GenerateKey(kv, ref produckey))
                return "ERROR";


            return produckey;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                GetKEy();
                textBox1.Text = getmac;
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                getmac = textBox1.Text;
                textBox2.Text = GetKEy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
