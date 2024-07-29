using Dr.Sale.Components;
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
    public partial class Report_Bill : Form
    {
        private string _sp;

        public Report_Bill(string sp)
        {
            InitializeComponent();

            _sp = sp;
            CostomerList.InvoiceTrs += Invoic;
            PartiNameList.IDSupp += PartiNameList_IDSupp;
            CustomerList.InvoiceTrs += CustomerList_InvoiceTrs;
        }

        private void CustomerList_InvoiceTrs(object sender, string e)
        {

            textBox1.Text = StoreRoom.GetData;
        }

        private void PartiNameList_IDSupp(object sender, string e)
        {


            textBox1.Text = StoreRoom.GetData;

        }

        private void Invoic(object sender, string e)
        {
            textBox1.Text = StoreRoom.GetData;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                View c = new View(_sp, "GetReport");
                c.Show();

            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                StoreRoom.dateTimePicker1 = dateTimePicker1.Value;
                StoreRoom.dateTimePicker2 = dateTimePicker2.Value;


                ReportStd std = new ReportStd(textBox1.Text,_sp);
                std.Show();


            }
            catch (Exception ex)
            {
                 
            }
        }

        private void Report_Bill_Load(object sender, EventArgs e)
        {
            label1.Text = "Report - "+ _sp;
        }
    }
}
