using CrystalReport.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport
{
    public partial class ComView : Form
    {
        private string getForm;
        public ComView(string getForms)
        {
            InitializeComponent();
            getForm = getForms;
 
        }

        private void ComView_Load(object sender, EventArgs e)
        {

            panel1.Controls.Clear();




            if (getForm=="Invoice")
            {
                panel1.Controls.Clear();
                Invoice item = new Invoice();
                item.Dock = DockStyle.Fill;
                panel1.Controls.Add(item);

                WindowState = FormWindowState.Maximized;
            }
            else if (getForm == "Bill")
            {
                panel1.Controls.Clear();
                Bill item = new Bill();
                item.Dock = DockStyle.Fill;
                panel1.Controls.Add(item);

                WindowState = FormWindowState.Maximized;
            }
            else
            {



                // Get the current assembly
                var currentAssembly = Assembly.GetExecutingAssembly();

                // Find the type of the UserControl by name
                var userControlType = currentAssembly.GetTypes().FirstOrDefault(t => t.Name == getForm);

                if (userControlType != null)
                {
                    // Create an instance of the UserControl
                    UserControl item = (UserControl)Activator.CreateInstance(userControlType);
                    item.Dock = DockStyle.Fill; // Make it fill the panel
                    panel1.Controls.Add(item);


         
                }
                else
                {
                    MessageBox.Show($"UserControl '{getForm}' not found.");
                }


            }


        }
    }
}
