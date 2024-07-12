using System;
using System.Drawing;
using System.Drawing.Imaging;
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
 
    

          

        }


   

      
       

        public bool ismin = false;
        private bool ismax = false;
        private bool isreport = true;
        private bool isReportopen;

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
            /*
            pictureBox3.Image = ConvertToWhite(pictureBox3.Image);
            pictureBox4.Image = ConvertToWhite(pictureBox4.Image);
            pictureBox5.Image = ConvertToWhite(pictureBox5.Image);
            pictureBox6.Image = ConvertToWhite(pictureBox6.Image);
            pictureBox7.Image = ConvertToWhite(pictureBox7.Image);
            pictureBox10.Image = ConvertToWhite(pictureBox10.Image);
            pictureBox11.Image = ConvertToWhite(pictureBox11.Image);
            pictureBox13.Image = ConvertToWhite(pictureBox13.Image);
            pictureBox12.Image = ConvertToWhite(pictureBox12.Image);
            pictureBox17.Image = ConvertToWhite(pictureBox17.Image);
            pictureBox1.Image = ConvertToWhite(pictureBox1.Image);
            pictureBox9.Image = ConvertToWhite(pictureBox9.Image);
            pictureBox2.Image = ConvertToWhite(pictureBox2.Image);
            pictureBox15.Image = ConvertToWhite(pictureBox15.Image);

            */


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
        private Image ConvertToWhite(Image image)
        {
            Bitmap whiteImage = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(whiteImage))
            {
                ColorMatrix colorMatrix = new ColorMatrix(
                    new float[][]
                    {
                new float[] {1f, 0f, 0f, 0, 0},    // Red channel
                new float[] {0f, 1f, 0f, 0, 0},    // Green channel
                new float[] {0f, 0f, 1f, 0, 0},    // Blue channel
                new float[] {0, 0, 0, 1, 0},       // Alpha channel
                new float[] {1, 1, 1, 0, 1}        // Constants for each channel
                    });

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }

            return whiteImage;
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
                if (StoreRoom.GetStyle() == "Default")
                {
                    ComView cm = new ComView("AddItem");
                    cm.Show();

                }
                else
                {


                    main.Controls.Clear();
                    AddItem item = new AddItem();
                    item.Dock = DockStyle.Fill;
                    main.Controls.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            try
            {
                if (StoreRoom.GetStyle() == "Default")
                {
                    ComView cm = new ComView("Itemlist");
                    cm.Show();

                }
                else
                {


                    main.Controls.Clear();
                    Itemlist item = new Itemlist();
                    item.Dock = DockStyle.Fill;
                    main.Controls.Add(item);
                }
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

                if(StoreRoom.GetStyle()=="Default")
                {
                    ComView vs = new ComView("Parties");
                    vs.Show();
                }
                else
                {
                    main.Controls.Clear();
                    Parties item = new Parties();
                    item.Dock = DockStyle.Fill;
                    main.Controls.Add(item);

                }
         
            }
            catch (Exception exx)
            {

                throw;
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

            if (StoreRoom.GetStyle() == "Default")
            {
                ComView view = new ComView("Bill");
                view.Show();
            }
            else
            {
                main.Controls.Clear();
                Bill item = new Bill();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }


           
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
            if (StoreRoom.GetStyle() == "Default")
            {
                ComView cm = new ComView("Stock");
                cm.Show();

            }
            else
            {


                main.Controls.Clear();
                Stock item = new Stock();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void label9_Click_1(object sender, EventArgs e)
        {

            //main.Controls.Clear();
            //Customer item = new Customer();
            //item.Dock = DockStyle.Fill;
            //main.Controls.Add(item);
            if (StoreRoom.GetStyle() == "Default")
            {
                ComView cm = new ComView("CustTrans");
                cm.Show();

            }
            else
            {


                main.Controls.Clear();
                CustTrans item = new CustTrans();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (StoreRoom.GetStyle()=="Default")
                {
                    ComView view = new ComView("Invoice");
                    view.Show();
                }
                else {
                    main.Controls.Clear();
                    Invoice item = new Invoice();
                    item.Dock = DockStyle.Fill;
                    main.Controls.Add(item);
                }

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
                if (StoreRoom.GetStyle() == "Default")
                {
                    ComView view = new ComView("PartiNameList");
                    view.Show();
                }
                else
                {
                    main.Controls.Clear();
                    PartiNameList item = new PartiNameList();
                    item.Dock = DockStyle.Fill;
                    main.Controls.Add(item);
                }

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            try
            {
                if (StoreRoom.GetStyle() == "Default")
                {
                    ComView view = new ComView("Payment");
                    view.Show();
                }
                else
                {
                    main.Controls.Clear();
                    Payment item = new Payment();
                    item.Dock = DockStyle.Fill;
                    main.Controls.Add(item);
                }

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
                if (StoreRoom.GetStyle() == "Default")
                {
                    ComView view = new ComView("Barcode");
                    view.Show();
                }
                else
                {
                    main.Controls.Clear();
                    Barcode item = new Barcode();
                    item.Dock = DockStyle.Fill;
                    main.Controls.Add(item);
                }

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
                if(StoreRoom.GetStyle()=="Default")
                {
                    ComView cm = new ComView("DayBook");
                    cm.Show();
                }
                else
                {
                    main.Controls.Clear();
                    DayBook item = new DayBook();
                    item.Dock = DockStyle.Fill;
                    main.Controls.Add(item);
                }
                

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

        private void label10_Click_1(object sender, EventArgs e)
        {
            try
            {


                main.Controls.Clear();
                Settings item = new Settings();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);


            }
            catch (Exception ex)
            {
                 
            }
        }

        private void report_Tick(object sender, EventArgs e)
        {
            
        }

        private int he = 185;
        private void bill_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                timer5.Start();
               // bexpand.Stop();



            }
            catch (Exception ex)
            {
                 
            }
            
        }

        private void bill_Leave(object sender, EventArgs e)
        {




        }

        private void bill_MouseLeave(object sender, EventArgs e)
        {

         
        }

        private void reportpanel_MouseEnter(object sender, EventArgs e)
        {
            isReportopen = false;
        }

        private void reportpanel_MouseLeave(object sender, EventArgs e)
        {
            isReportopen = true;
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            
        }

        private void timer4_Tick(object sender, EventArgs e)
        {

        }

        private void timer5_Tick(object sender, EventArgs e)
        {
           
        }

        private void bexpand_Tick(object sender, EventArgs e)
        {
            
             
        }

        private void panelssssss_MouseEnter(object sender, EventArgs e)
        {

            panelssssss.BackColor = Color.Chocolate;

        }

        private void panelssssss_MouseLeave(object sender, EventArgs e)
        {
            panelssssss.BackColor = Color.FromArgb(34, 36, 67);

        }

        private void reportpa_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void bill_MouseLeave_1(object sender, EventArgs e)
        {
           
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
           //timer5.Stop();
            bexpand.Start();
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Chocolate;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Chocolate;
        }

        private void panel4_MouseLeave(object sender, EventArgs e)
        {

            panel4.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Chocolate;
        }

        private void panel5_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel7_MouseEnter(object sender, EventArgs e)
        {
            panel7.BackColor = Color.Chocolate;
        }

        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel8_MouseEnter(object sender, EventArgs e)
        {
            panel8.BackColor = Color.Chocolate;

        }

        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel9_MouseHover(object sender, EventArgs e)
        {

        }

        private void panel9_MouseEnter(object sender, EventArgs e)
        {
            panel9.BackColor = Color.Chocolate;

        }

        private void panel9_MouseLeave(object sender, EventArgs e)
        {
            panel9.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel10_MouseEnter(object sender, EventArgs e)
        {
            panel10.BackColor = Color.Chocolate;
        }

        private void panel10_MouseLeave(object sender, EventArgs e)
        {

            panel10.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel11_MouseEnter(object sender, EventArgs e)
        {
            panel11.BackColor = Color.Chocolate;
        }

        private void panel11_MouseLeave(object sender, EventArgs e)
        {

            panel11.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel12_MouseEnter(object sender, EventArgs e)
        {
            panel12.BackColor = Color.Chocolate;
        }

        private void panel12_MouseLeave(object sender, EventArgs e)
        {
            panel12.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel13_MouseEnter(object sender, EventArgs e)
        {
            panel13.BackColor = Color.Chocolate;

        }

        private void panel13_MouseLeave(object sender, EventArgs e)
        {
            panel13.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel14_MouseEnter(object sender, EventArgs e)
        {
            panel14.BackColor = Color.Chocolate;

        }

        private void panel14_MouseLeave(object sender, EventArgs e)
        {

            panel14.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel15_MouseEnter(object sender, EventArgs e)
        {
            panel15.BackColor = Color.Chocolate;
        }

        private void panel15_MouseLeave(object sender, EventArgs e)
        {
            panel15.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel16_MouseEnter(object sender, EventArgs e)
        {
            panel16.BackColor = Color.Chocolate;
        }

        private void panel16_MouseLeave(object sender, EventArgs e)
        {
            panel16.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel17_MouseEnter(object sender, EventArgs e)
        {
            panel17.BackColor = Color.Chocolate;
        }

        private void panel17_MouseLeave(object sender, EventArgs e)
        {
            panel17.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel18_MouseEnter(object sender, EventArgs e)
        {
            panel18.BackColor = Color.Chocolate;
        }

        private void panel18_MouseLeave(object sender, EventArgs e)
        {
            panel18.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panelssssss_Paint(object sender, PaintEventArgs e)
        {
            main.Controls.Clear();
            DashBoard item = new DashBoard();
            item.Dock = DockStyle.Fill;
            main.Controls.Add(item);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (StoreRoom.GetStyle() == "Default")
            {
                ComView cm = new ComView("AddItem");
                cm.Show();

            }
            else
            {


                main.Controls.Clear();
                AddItem item = new AddItem();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
           if (StoreRoom.GetStyle() == "Default")
            {
                ComView cm = new ComView("Itemlist");
                cm.Show();

            }
            else
            {


                main.Controls.Clear();
                Itemlist item = new Itemlist();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            if (StoreRoom.GetStyle() == "Default")
            {
                ComView cm = new ComView("Stock");
                cm.Show();

            }
            else
            {


                main.Controls.Clear();
                Stock item = new Stock();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            if (StoreRoom.GetStyle() == "Default")
            {
                ComView cm = new ComView("CustTrans");
                cm.Show();

            }
            else
            {


                main.Controls.Clear();
                CustTrans item = new CustTrans();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

            if (StoreRoom.GetStyle() == "Default")
            {
                ComView view = new ComView("Invoice");
                view.Show();
            }
            else
            {
                main.Controls.Clear();
                Invoice item = new Invoice();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

            if (StoreRoom.GetStyle() == "Default")
            {
                ComView view = new ComView("Barcode");
                view.Show();
            }
            else
            {
                main.Controls.Clear();
                Barcode item = new Barcode();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

            if (StoreRoom.GetStyle() == "Default")
            {
                ComView vs = new ComView("Parties");
                vs.Show();
            }
            else
            {
                main.Controls.Clear();
                Parties item = new Parties();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
               
            }
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
            if (StoreRoom.GetStyle() == "Default")
            {
                ComView view = new ComView("Bill");
                view.Show();
            }
            else
            {
                main.Controls.Clear();
                Bill item = new Bill();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {
            if (StoreRoom.GetStyle() == "Default")
            {
                ComView view = new ComView("PartiNameList");
                view.Show();
            }
            else
            {
                main.Controls.Clear();
                PartiNameList item = new PartiNameList();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {
            if (StoreRoom.GetStyle() == "Default")
            {
                ComView view = new ComView("Payment");
                view.Show();
            }
            else
            {
                main.Controls.Clear();
                Payment item = new Payment();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void label3_Click_2(object sender, EventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {
            if (StoreRoom.GetStyle() == "Default")
            {
                ComView cm = new ComView("DayBook");
                cm.Show();
            }
            else
            {
                main.Controls.Clear();
                DayBook item = new DayBook();
                item.Dock = DockStyle.Fill;
                main.Controls.Add(item);
            }
        }

        private void panelssssss_Paint(object sender, EventArgs e)
        {

        }

        private void panelssssss_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void panelssssss_Paint_2(object sender, PaintEventArgs e)
        {

        }
    }
}