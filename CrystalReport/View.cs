using CrystalReport.Components;
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
    public partial class View : Form
    {
        private readonly string getForm;
        private readonly string _terms;
        public View(string formanme,string term)
        {
            getForm = formanme;
            _terms = term;
            InitializeComponent();
            CostomerList.HideOR += CostomerList_HideOR;
            PartiNameList.HideOR += PartiNameList_HideOR;
            PartiList.isOff += PartiList_isOff;
            Purches_View.isOff += Purches_View_isOff;
           
        }

        private void Purches_View_isOff(object sender, bool e)
        {

            if (e == true)
            {
                this.Hide();
            }
            else
            {
            }
        }

        private void PartiList_isOff(object sender, bool e)
        {
            if (e == true)
            {
                this.Hide();
            }
            else
            {
            }
        }

        private void PartiNameList_HideOR(object sender, bool e)
        {
            if (e == true)
            {
                this.Hide();
            }
            else
            {
            }
        }

        private void CostomerList_HideOR(object sender, bool e)
        {
            if(e == true)
            {
                this.Hide();
            }
            else
            {
            }
        }

        private void View_Load(object sender, EventArgs e)
        {
            try
            {
                if(getForm == "Invoice")
                {
                     
                        panel1.Controls.Clear();
                        CostomerList item = new CostomerList(_terms);
                        item.Dock = DockStyle.Fill;
                    panel1.Controls.Add(item);
                   
                }


                else if (getForm == "View_Purches")
                {

                    panel1.Controls.Clear();
                    Purches_View item = new Purches_View();
                    item.Dock = DockStyle.Fill;
                    panel1.Controls.Add(item);


                }


                else if(getForm == "Customer Statment")
                {
                    panel1.Controls.Clear();
                    CustomerList item = new CustomerList();
                    item.Dock = DockStyle.Fill;
                    panel1.Controls.Add(item);


                }
                else if (getForm == "Supplier Ledger")
                {
                    panel1.Controls.Clear();
                    PartiNameList item = new PartiNameList();
                    item.Dock = DockStyle.Fill;
                    panel1.Controls.Add(item);
                }
                else if(getForm == "Payment")
                {
                    panel1.Controls.Clear();
                    PartiList item = new PartiList();
                    item.Dock = DockStyle.Fill;
                    panel1.Controls.Add(item);

                }

                else if(getForm == "Owner")
                {
                    panel1.Controls.Clear();
                    Company item = new Company();
                    item.Dock = DockStyle.Fill;
                    panel1.Controls.Add(item);

                }

 
            }
            catch (Exception ex)
            {
                 
            }
        }
    }
}
