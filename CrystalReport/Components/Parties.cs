
using Back_Dr.Models;
using Back_Dr.Sale;
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
    public partial class Parties : UserControl
    {
        private BackgroundWorker dataLoader = new BackgroundWorker();
        private DataTable dataTable = new DataTable();
        private int count = 0;

        private int sn = 0;
        public Parties()
        {
            InitializeComponent();
 

        
        }


 



        private void panel2_Paint(object sender, PaintEventArgs e)
        {
                
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Crys cr = new Crys();
                


                    var pa = new
                    {
                        pname = partiname.Text,
                        company = company.Text,
                        partiphone = workphone.Text,
                        partimobile = mobile.Text,
                        paninformation = pan.Text,
                        city = textBox2.Text,
                        state = state.Text,
                        address = address.Text,
                        BankName = bankname.Text,
                        ACName = acname.Text,
                        ACNumber = acnumber.Text,
                        IFCCODE = ifc.Text,
                        GSTN = gstin.Text
                    };

                    if (pa.city != "" && pa.partimobile != "" || pa.partiphone != "")
                    {
                        await MainEngine_.Add<dynamic>(pa, "InsertIntoParties");
                        int row = 0;
                        string fone = "0000";

                        if (mobile.Text != "" && workphone.Text == "")
                        {
                            fone = mobile.Text;
                        }
                        else
                        {
                            fone = workphone.Text;
                        }

                       
                        MessageBox.Show("Parti Added!", "Parti Add:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StoreRoom.ClearData(this.Controls);
                    }
         
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parti Error:", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        
        
        }

        private void Parties_Load(object sender, EventArgs e)
        {
            try
            {
                dataLoader.RunWorkerAsync();
            }
            catch (Exception ex)
            {
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StoreRoom.ClearData(this.Controls);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private async void gunaButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Crys cr = new Crys();



                var pa = new
                {
                    pname = partiname.Text,
                    company = company.Text,
                    partiphone = workphone.Text,
                    partimobile = mobile.Text,
                    paninformation = pan.Text,
                    city = textBox2.Text,
                    state = state.Text,
                    address = address.Text,
                    BankName = bankname.Text,
                    ACName = acname.Text,
                    ACNumber = acnumber.Text,
                    IFCCODE = ifc.Text,
                    GSTN = gstin.Text
                };

                if (pa.city != "" && pa.partimobile != "" || pa.partiphone != "")
                {
                    await MainEngine_.Add<dynamic>(pa, "InsertIntoParties");
                    int row = 0;
                    string fone = "0000";

                    if (mobile.Text != "" && workphone.Text == "")
                    {
                        fone = mobile.Text;
                    }
                    else
                    {
                        fone = workphone.Text;
                    }


                    MessageBox.Show("Parti Added!", "Parti Add:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StoreRoom.ClearData(this.Controls);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parti Error:", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
