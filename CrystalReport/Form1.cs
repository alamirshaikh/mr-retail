using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalReport.Components;
using Dr.Sale.Components;
 
namespace CrystalReport
{
    public partial class Form1 : Form
    {

         
        private const int AnimationDuration = 350;
        public Form1()
        {
            InitializeComponent();
 
            this.FormClosing += Form1_FormClosing;
          
        }


        private async void AnimateMinimize()
        {
            double opacityDecrement = 1.0 / (AnimationDuration / 10.0); // Adjust the interval as needed

            while (this.Opacity > 0)
            {
                await Task.Delay(10); // Adjust the delay as needed
                this.Opacity -= opacityDecrement;
            }

            // Minimize the form after the animation completes
            this.WindowState = FormWindowState.Minimized;
        }

        private async Task AnimateFadeOut()
        {
            double opacityDecrement = 1.0 / (AnimationDuration / 10.0); // Adjust the interval as needed

            while (this.Opacity > 0)
            {
                await Task.Delay(10); // Adjust the delay as needed
                this.Opacity -= opacityDecrement;
            }
        }
        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            // Start the custom fade-out animation
            await AnimateFadeOut();

            // Close the form
            Environment.Exit(0);
        }

        public bool ismin = false;
        private bool ismax = false;
     

        private void label2_Click(object sender, EventArgs e)
        {

         
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

    


        }






  



        private void Form1_Load(object sender, EventArgs e)
        {
            main.Controls.Clear();
            DashBoard item = new DashBoard();
            item.Dock = DockStyle.Fill;
            main.Controls.Add(item);


        }

        private void label11_Click(object sender, EventArgs e)
        {
            try
            {

                main.Controls.Clear();
                Invoice item = new Invoice();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
             

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (ismax == false)
            {

                this.WindowState = FormWindowState.Maximized;
                ismax = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;

                ismax = false;

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (ismin == true)
            {

                this.WindowState = FormWindowState.Minimized;
                ismin = false;
                timer2.Stop();

            }






        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

            main.Controls.Clear();
            Customer item = new Customer();
            item.Dock = DockStyle.Fill;
            main.Controls.Add(item);


        }



        private void main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
             

        }

        private void label15_Click(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
               AddItem item = new AddItem();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
            catch (Exception ex)
            {

            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
                Itemlist item = new Itemlist();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);

            }

            catch (Exception ex)
            {

                throw;
            }
        }

        private void prch_Click(object sender, EventArgs e)
        {
           
        }



        private void label5_Click(object sender, EventArgs e)
        {
            try
            {
               main.Controls.Clear();
                Parties item = new Parties();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
            catch (Exception exx)
            {

                throw;
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            main.Controls.Clear();
            Bill item = new Bill();
            item.Dock = DockStyle.Fill;
            main.Controls.Add(item);
        }

        private void label18_Click(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
                Company item = new Company();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
        }

        private void bill_Click(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
                GetReport item = new GetReport();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
            catch (Exception ex)
            {

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

            main.Controls.Clear();
            Stock item = new Stock();
            item.Dock = DockStyle.Fill;
            main.Controls.Add(item);
        }

        private void label9_Click_1(object sender, EventArgs e)
        {

            //main.Controls.Clear();
            //Customer item = new Customer();
            //item.Dock = DockStyle.Fill;
            //main.Controls.Add(item);


            main.Controls.Clear();
            CustTrans item = new CustTrans();
            item.Dock = DockStyle.Fill;
            main.Controls.Add(item);
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            try
            {

                main.Controls.Clear();
                Invoice item = new Invoice();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
                PartiList item = new PartiList();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
                Payment item = new Payment();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);

            }
            catch (Exception ex)
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            try
            {
 

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
                Barcode item = new Barcode();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);

            }
            catch (Exception ex)
            {

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
                DashBoard item = new DashBoard();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
            catch (Exception ex)
            {  
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
                Mechanic item = new Mechanic();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
            catch (Exception ex)
            {
                 
            }
        }

    



        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaPictureBox2_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Maximized)
            {

                this.WindowState = FormWindowState.Normal;

            }
            else
            {
                this.WindowState = FormWindowState.Maximized;

            }
        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            try
            {

                main.Controls.Clear();
                Backup item = new Backup();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            try
            {

                View c = new View("Invoice","SaleReport");
                c.Show();
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            main.Controls.Clear();
            Transactions item = new Transactions();
            item.Dock = DockStyle.Fill;
            main.Controls.Add(item);
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            try
            {

                main.Controls.Clear();
                RecoveryAmount item = new RecoveryAmount();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            try
            {

                main.Controls.Clear();
                Expenses item = new Expenses();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            try
            {
                main.Controls.Clear();
                DayBook item = new DayBook();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);

            }
            catch (Exception ex)
            {

            }
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                // Check if the control that triggered the event is a label
                if (sender is Label)
                {
                    // Change the mouse cursor to a hand pointer
                    Cursor = Cursors.Hand;
                }
                else
                {
                    // Change the mouse cursor to the default pointer
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}