using Back_Dr.Sale;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class Customer_pay : UserControl
    {
        private bool issave;

        public Customer_pay()
        {
            InitializeComponent();
            CustomerList.InvoiceTrs += CustomerList_InvoiceTrs;
        }

        private void CustomerList_InvoiceTrs(object sender, string e)
        {
            id.Text = StoreRoom.GetData;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void getid_Click(object sender, EventArgs e)
        {
            try
            {


                View c = new View("Customer Statment", "GetReport");
                c.Show();
                GetInvoice();


            }
            catch (Exception ex)
            {
                 
            }
        }


        
        public void GetInvoice()
        {
            int s = MainEngine_.GetDataScript<decimal>("select * from CustomerTransactions").Count();


            string idge = $"REC/{s + 1}/{id.Text}";


            trno.Text = idge;
        }




        private void id_TextChanged(object sender, EventArgs e)
        {
            try
            {

                var dym = MainEngine_.GetDataScript<dynamic>("select * from Customer Where Id = "+id.Text+"").ToList();


                foreach (var item in dym)
                {
                    name.Text = item.cust_name;
                    contact.Text = item.cust_phone;
                    address.Text = item.Place;

                    decimal balanceValue = item.Balance ?? 0m;
                    string formattedBalance = balanceValue.ToString("C2", new CultureInfo("hi-IN"));

                    blc.Text = $"{formattedBalance} CR";


                }

                

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void Customer_pay_Load(object sender, EventArgs e)
        {
            GetInvoice();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            issave = false;
            button2.Enabled = true;
        }

        private void trno_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (issave == false)
            {


                 
                GetInvoice();
                issave = true;
                button2.Enabled = false;

            }
            else
            {
                button2.Enabled = false;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {




            }
            catch (Exception ex)
            {
                 
            }
        }
    }
}
