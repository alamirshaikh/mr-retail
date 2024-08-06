using System;
using System.Data;
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
        private void StartPanelExpansion(ref bool isExpanding, Timer timer)
        {
            isExpanding = true;
            timer.Start();
        }

        private void StartPanelCollapse(ref bool isExpanding, Timer timer)
        {
            isExpanding = false;
            timer.Start();
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

           // if (StoreRoom.GetStyle() == "Default")
            //{
                ComView view = new ComView("Bill");
                view.Show();
            //}
            //else
            //{
             //   main.Controls.Clear();
              //  Bill item = new Bill();
               // item.Dock = DockStyle.Fill;
                //main.Controls.Add(item);
            //}


           
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



            customer_t.Start();
            //main.Controls.Clear();
            //Customer item = new Customer();
            //item.Dock = DockStyle.Fill;
            //main.Controls.Add(item);



            /*
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

            */
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




        private void OffOpen()
        {
            isExpanding = false;
            customer_t.Start();
            isExpandingP = false;
            purches_timer.Start();
            isExpandingS = false;
            supplier_timer.Start();
            isExpandingM = false;
            master_timer.Start();
            isExpandingR = false;
            report_timer.Start();
            isExpandingSe = false;
            isExpandingI = false;
            import_timer.Start();
            isExpandingRe = false;
            isExpandingTax = false;
            timer_tax.Start();
            record_timer.Start();

            setting_timer.Start();
            isExpandingT = false;
            tran_timer.Start();
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
            panel15.BackColor = Color.Chocolate;
            Reports_StartExpanding();

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
            OffOpen();

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
            StartCollaps_Tran();
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Chocolate;
            StartExp_Record();
        }

        private void panel4_MouseLeave(object sender, EventArgs e)
        {

            panel4.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Chocolate;
            OffOpen();
        }

        private void panel5_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel7_MouseEnter(object sender, EventArgs e)
        {
            panel7.BackColor = Color.Chocolate;
            Customer_StartExpanding();
        }

        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel8_MouseEnter(object sender, EventArgs e)
        {
            panel8.BackColor = Color.Chocolate;
            OffOpen();

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
            OffOpen();
        }

        private void panel9_MouseLeave(object sender, EventArgs e)
        {
            panel9.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel10_MouseEnter(object sender, EventArgs e)
        {
            panel10.BackColor = Color.Chocolate;
            StartExp_Purches();
        }

        private void panel10_MouseLeave(object sender, EventArgs e)
        {

            panel10.BackColor = Color.FromArgb(34, 36, 67);
          
            StartExp_Purches();
        }

        private void panel11_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void panel11_MouseLeave(object sender, EventArgs e)
        {

     
        }

        private void panel12_MouseEnter(object sender, EventArgs e)
        {
            Supploer_StartExpanding();
            panel12.BackColor = Color.Chocolate;
        }

        private void panel12_MouseLeave(object sender, EventArgs e)
        {
            panel12.BackColor = Color.FromArgb(34, 36, 67);
            Supploer_StartExpanding();
            
        }

        private void panel13_MouseEnter(object sender, EventArgs e)
        {
            panel13.BackColor = Color.Chocolate;
            StartExp_Tax();
      
            

        }

        private void panel13_MouseLeave(object sender, EventArgs e)
        {
            panel13.BackColor = Color.FromArgb(34, 36, 67);
     
        }

        private void panel14_MouseEnter(object sender, EventArgs e)
        {
            panel14.BackColor = Color.Chocolate;
            Import_StartExpanding();
            

        }

        private void panel14_MouseLeave(object sender, EventArgs e)
        {

            panel14.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel15_MouseEnter(object sender, EventArgs e)
        {
            panel15.BackColor = Color.Chocolate;
            Reports_StartExpanding();
            
        }

        private void panel15_MouseLeave(object sender, EventArgs e)
        {
            panel15.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel16_MouseEnter(object sender, EventArgs e)
        {
            panel16.BackColor = Color.Chocolate;
   

            OffOpen();
        }

        private void panel16_MouseLeave(object sender, EventArgs e)
        {
            panel16.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel17_MouseEnter(object sender, EventArgs e)
        {
            panel17.BackColor = Color.Chocolate;
            OffOpen();
        }

        private void panel17_MouseLeave(object sender, EventArgs e)
        {
            panel17.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void panel18_MouseEnter(object sender, EventArgs e)
        {
            panel18.BackColor = Color.Chocolate;
            OffOpen();
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
            import_timer.Start();
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
            OffOpen();
        }

        private void panelssssss_Paint_2(object sender, PaintEventArgs e)
        {

        }

 






        private bool isExpanding = true; // Flag to indicate if the panel is expanding
        private bool isExpandingP = true; // Flag to indicate if the panel is expanding
        private bool isExpandingS = true; // Flag to indicate if the panel is expanding
        private bool isExpandingM = true; // Flag to indicate if the panel is expanding
        private bool isExpandingR = true; // Flag to indicate if the panel is expanding
        private bool isExpandingSe = true; // Flag to indicate if the panel is expanding
        private bool isExpandingT = true;
        private bool isExpandingRe = true;
        private bool isExpandingI = true;
        private bool isExpandingTax =true;

        private void customer_t_Tick(object sender, EventArgs e)
        {
            try
            {
                int targetHeight = 85; // Target height for expanding the panel
                int step = 50;
                // Amount to increase or decrease the height by each tick

                if (isExpanding)
                {
                    isExpandingRe = false;
                    record_timer.Start();
                    isExpandingTax = false;
                    timer_tax.Start();
                    isExpandingP = false;
                    purches_timer.Start();
                    isExpandingS = false;
                    supplier_timer.Start();
                    isExpandingM = false;
                    master_timer.Start();
                    isExpandingR = false;
                    report_timer.Start();
                    isExpandingSe = false;
                    isExpandingI = false;
                    import_timer.Start();
                    setting_timer.Start();
                    isExpandingT = false;
                    tran_timer.Start();


                    if (panelcustomer.Height < targetHeight)
                    {
                        // Expand the panel
                        panelcustomer.Height += step;

                        // Ensure the panel does not exceed the target height
                        if (panelcustomer.Height >= targetHeight)
                        {
                            panelcustomer.Height = targetHeight; // Set exact target height
                            customer_t.Stop(); // Stop the timer once the target height is reached
                        }
                    }
                }
                else
                {
                    if (panelcustomer.Height > 0)
                    {
                        // Collapse the panel
                        panelcustomer.Height -= step;

                        // Ensure the panel does not go below zero
                        if (panelcustomer.Height <= 0)
                        {
                            panelcustomer.Height = 0; // Set exact zero height
                            customer_t.Stop(); // Stop the timer once the panel is fully collapsed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message or handle it accordingly
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Call this method to start expanding the panel
        private void Customer_StartExpanding()
        {
            isExpanding= true;
            customer_t.Start();
        }

        // Call this method to start collapsing the panel
        private void Customer_StartCollapsing()
        {
            isExpanding = false;
            customer_t.Start();
        }




        private void Supploer_StartExpanding()
        {
            isExpandingS = true;
            supplier_timer.Start();
        }




        private void Import_StartExpanding()
        {
            isExpandingI = true;
            import_timer.Start();
        }


        private void Master_StartExpanding()
        {
            isExpandingM = true;
            master_timer.Start();
        }





        private void Reports_StartCollapsing()
        {
            isExpandingR = false;
           report_timer.Start();
        }




        private void Reports_StartExpanding()
        {
            isExpandingR = true;
            report_timer.Start();
        }









        // Call this method to start collapsing the panel
        private void Supplier_StartCollapsing()
        {
            isExpandingS = false;
            supplier_timer.Start();
        }








        private void label9_MouseEnter(object sender, EventArgs e)
        {
             
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {

            panel5.BackColor = Color.Chocolate;
            OffOpen();
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            panel8.BackColor = Color.Chocolate;
            OffOpen();
        }

        private void label17_MouseEnter(object sender, EventArgs e)
        {

            panel9.BackColor = Color.Chocolate;
            OffOpen();
        }

        private void label13_MouseEnter(object sender, EventArgs e)
        {
            panel10.BackColor = Color.Chocolate;
            StartExp_Purches();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            Customer_StartCollapsing();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            panel14.BackColor = Color.Chocolate;
            Import_StartExpanding();
         
        }

        private void label17_MouseHover(object sender, EventArgs e)
        {
            Customer_StartCollapsing();
        }

        private void label9_MouseEnter_1(object sender, EventArgs e)
        {

            panel7.BackColor = Color.Chocolate;
            Customer_StartExpanding();
        }

        private void purches_timer_Tick(object sender, EventArgs e)
        {
          

            try
            {
                int targetHeight = 120; // Target height for expanding the panel
                int step = 50;         // Amount to increase or decrease the height by each tick

                if (isExpandingP)
                {
                    isExpandingRe = false;
                    record_timer.Start();
                    isExpanding = false;
                    customer_t.Start();
                    isExpandingS = false;
                    isExpandingTax = false;
                    timer_tax.Start();
                    supplier_timer.Start();
                    isExpandingM = false;
                    isExpandingT = false;
                    tran_timer.Start();
                    master_timer.Start();
                    isExpandingR = false;
                    report_timer.Start();
                    isExpandingSe = false;
                    isExpandingI = false;
                    import_timer.Start();
                    setting_timer.Start();
                    if (purches_panel.Height < targetHeight)
                    {
                        // Expand the panel
                        purches_panel.Height += step;

                        // Ensure the panel does not exceed the target height
                        if (purches_panel.Height >= targetHeight)
                        {
                            purches_panel.Height = targetHeight; // Set exact target height
                            purches_timer.Stop(); // Stop the timer once the target height is reached
                        }
                    }
                }
                else
                {
                    if (purches_panel.Height > 0)
                    {
                        // Collapse the panel
                        purches_panel.Height -= step;

                        // Ensure the panel does not go below zero
                        if (purches_panel.Height <= 0)
                        {
                            purches_panel.Height = 0; // Set exact zero height
                            purches_timer.Stop(); // Stop the timer once the panel is fully collapsed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message or handle it accordingly
                //MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

        // Call this method to start expanding the panel
       

    }
        private void StartExp_Purches()
        {
            isExpandingP = true;
           purches_timer.Start();
        }

        private void StartExp_Tax()
        {
            isExpandingTax = true;
            timer_tax.Start();
        }




        private void StartExp_Record()
        {
            isExpandingRe = true;
            record_timer.Start();
        }


        private void StartCollaps_Tran()
        {
            isExpandingT = true;
            tran_timer.Start();
        }




        // Call this method to start collapsing the panel
        private void StartCollaps_Purches()
        {
            isExpandingP = false;
            purches_timer.Start();
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            Supploer_StartExpanding();
            panel12.BackColor = Color.Chocolate;
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            panel13.BackColor = Color.Chocolate;
            OffOpen();
        }

        private void supplier_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                int targetHeight = 81; // Target height for expanding the panel
                int step = 50;         // Amount to increase or decrease the height by each tick

                if (isExpandingS)
                {
                    isExpanding = false;
                    customer_t.Start();
                    isExpandingP = false;
                    purches_timer.Start();
                    isExpandingM = false;
                    isExpandingT = false;
                    tran_timer.Start();
                    isExpandingRe = false;
                    record_timer.Start();
                    master_timer.Start();
                    isExpandingR = false;
                    isExpandingTax = false;
                    timer_tax.Start();
                    isExpandingI = false;
                    import_timer.Start();
                    report_timer.Start();
                    isExpandingSe = false;
                    setting_timer.Start();
                    if (supplier_p.Height < targetHeight)
                    {
                        // Expand the panel
                        supplier_p.Height += step;

                        // Ensure the panel does not exceed the target height
                        if (supplier_p.Height >= targetHeight)
                        {
                            supplier_p.Height = targetHeight; // Set exact target height
                            supplier_timer.Stop(); // Stop the timer once the target height is reached
                        }
                    }
                }
                else
                {
                    if (supplier_p.Height >= 0)
                    {
                        // Collapse the panel
                        supplier_p.Height -= step;

                        // Ensure the panel does not go below zero
                        if (supplier_p.Height <= 0)
                        {
                            supplier_p.Height = 0; // Set exact zero height
                            supplier_timer.Stop(); // Stop the timer once the panel is fully collapsed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message or handle it accordingly
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel25_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox41_Click(object sender, EventArgs e)
        {

        }

        private void report_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                int targetHeight = 215; // Target height for expanding the panel
                int step = 50;         // Amount to increase or decrease the height by each tick

                if (isExpandingR)
                {
                    isExpanding = false;
                    customer_t.Start();
                    isExpandingP = false;
                    isExpandingT = false;
                     isExpandingTax = false;
                    timer_tax.Start();
                    tran_timer.Start();
                    purches_timer.Start();
                    isExpandingM = false;
                    master_timer.Start();
                    isExpandingS = false;
                    supplier_timer.Start();
                    isExpandingSe = false;
                    setting_timer.Start();
                    isExpandingI = false;
                    import_timer.Start();
                    if (bill_panel.Height < targetHeight)
                    {
                        // Expand the panel
                        bill_panel.Height += step;

                        // Ensure the panel does not exceed the target height
                        if (bill_panel.Height >= targetHeight)
                        {
                            bill_panel.Height = targetHeight; // Set exact target height
                            report_timer.Stop(); // Stop the timer once the target height is reached
                        }
                    }
                }
                else
                {
                    if (bill_panel.Height >= 0)
                    {
                        // Collapse the panel
                        bill_panel.Height -= step;

                        // Ensure the panel does not go below zero
                        if (bill_panel.Height <= 0)
                        {
                            bill_panel.Height = 0; // Set exact zero height
                            report_timer.Stop(); // Stop the timer once the panel is fully collapsed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message or handle it accordingly
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label51_Click(object sender, EventArgs e)
        {
            GST_MASTER ms = new GST_MASTER();
            ms.Show();
            OffOpen();
        }

        private void master_timer_Tick(object sender, EventArgs e)
        {



            try
            {
                int targetHeight = 300; // Target height for expanding the panel
                int step = 50;         // Amount to increase or decrease the height by each tick

                if (isExpandingM)
                {
                    isExpanding = false;
                    customer_t.Start();
                    isExpandingRe = false;
                    record_timer.Start();
                    isExpandingTax = false;
                    timer_tax.Start();
                    isExpandingT = false;
                    tran_timer.Start();

                    isExpandingP = false;
                    purches_timer.Start();
                    isExpandingR = false;
                    report_timer.Start();
                    isExpandingS = false;
                    isExpandingI = false;
                    import_timer.Start();
                    supplier_timer.Start();
                    isExpandingSe = false;
                    setting_timer.Start();
                    if (master_panel.Height < targetHeight)
                    {
                        // Expand the panel
                        master_panel.Height += step;

                        // Ensure the panel does not exceed the target height
                        if (master_panel.Height >= targetHeight)
                        {
                            master_panel.Height = targetHeight; // Set exact target height
                            master_timer.Stop(); // Stop the timer once the target height is reached
                        }
                    }
                }
                else
                {
                    if (master_panel.Height >= 0)
                    {
                        // Collapse the panel
                        master_panel.Height -= step;

                        // Ensure the panel does not go below zero
                        if (master_panel.Height <= 0)
                        {
                            master_panel.Height = 0; // Set exact zero height
                            master_timer.Stop(); // Stop the timer once the panel is fully collapsed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message or handle it accordingly
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void panel25_MouseEnter(object sender, EventArgs e)
        {

            
            Master_StartExpanding();

            panel25.BackColor = Color.Chocolate;
        }

        private void tran_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                int targetHeight = 240; // Target height for expanding the panel
                int step = 50;         // Amount to increase or decrease the height by each tick

                if (isExpandingT)
                {
                    isExpanding = false;
                    customer_t.Start();

                    isExpandingM = false;
                    master_timer.Start();
                    isExpandingRe = false;
                    isExpandingTax = false;
                    timer_tax.Start();
                    record_timer.Start();
                    isExpandingP = false;
                    purches_timer.Start();
                    isExpandingR = false;
                    isExpandingI = false;
                    import_timer.Start();
                    report_timer.Start();
                    isExpandingS = false;
                    supplier_timer.Start();
                    isExpandingSe = false;
                    setting_timer.Start();
                    if (tran_panel.Height < targetHeight)
                    {
                        // Expand the panel
                        tran_panel.Height += step;

                        // Ensure the panel does not exceed the target height
                        if (tran_panel.Height >= targetHeight)
                        {
                            tran_panel.Height = targetHeight; // Set exact target height
                            tran_timer.Stop(); // Stop the timer once the target height is reached
                        }
                    }
                }
                else
                {
                    if (tran_panel.Height >= 0)
                    {
                        // Collapse the panel
                        tran_panel.Height -= step;

                        // Ensure the panel does not go below zero
                        if (tran_panel.Height <= 0)
                        {
                            tran_panel.Height = 0; // Set exact zero height
                            tran_timer.Stop(); // Stop the timer once the panel is fully collapsed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message or handle it accordingly
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void label15_MouseEnter(object sender, EventArgs e)
        {
            StartCollaps_Tran();
        }

        private void label50_MouseEnter(object sender, EventArgs e)
        {
            Master_StartExpanding();
            panel25.BackColor = Color.Chocolate;
        }

        private void label14_MouseEnter(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Chocolate;
            StartExp_Record();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            panel16.BackColor = Color.Chocolate;


            OffOpen();
        }

        private void label10_MouseEnter(object sender, EventArgs e)
        {
               

            OffOpen();
        }

        private void label10_MouseEnter_1(object sender, EventArgs e)
        {
            panel17.BackColor = Color.Chocolate;
            OffOpen();
        }

        private void label18_MouseEnter(object sender, EventArgs e)
        {
            panel18.BackColor = Color.Chocolate;
            OffOpen();
        }

        private void label45_Click(object sender, EventArgs e)
        {
            try
            {
                ComView cm = new ComView("AddItem");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label52_Click(object sender, EventArgs e)
        {
            try
            {
                Unit cm = new Unit();
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }
        }

        private void label44_Click(object sender, EventArgs e)
        {
            DefaultMaster mst = new DefaultMaster();
            mst.Show();
            OffOpen();
        }

        private void panel25_MouseLeave(object sender, EventArgs e)
        {

            panel25.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void label50_MouseLeave(object sender, EventArgs e)
        {

            panel25.BackColor = Color.FromArgb(34, 36, 67);
        }

        private void label58_Click(object sender, EventArgs e)
        {
            try
            {


                OffOpen();
                ComView cm = new ComView("LoadRotray");
                cm.Show();


            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label54_Click(object sender, EventArgs e)
        {
            try
            {


                OffOpen();
                ComView cm = new ComView("Customer_pay");
                cm.Show();


            }
            catch (Exception ex)
            {

            }
        }

        private void label53_Click(object sender, EventArgs e)
        {

            try
            {


                OffOpen();
                ComView cm = new ComView("Payment");
                cm.Show();


            }
            catch (Exception ex)
            {

            }
        }

        private void label59_Click(object sender, EventArgs e)
        {

            try
            {


                OffOpen();
                ComView cm = new ComView("Expenses");
                cm.Show();


            }
            catch (Exception ex)
            {

            }
        }

        private void label60_Click(object sender, EventArgs e)
        {
            try
            {


                OffOpen();
                ComView cm = new ComView("Purches_View");
                cm.Show();


            }
            catch (Exception ex)
            {

            }

        }

        private void label67_Click(object sender, EventArgs e)
        {


            try
            {


                OffOpen();
                ComView cm = new ComView("ViewPurches_Orders");
                cm.Show();


            }
            catch (Exception ex)
            {

            }
        }

        private void pictureBox69_Click(object sender, EventArgs e)
        {

        }

        private void record_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                int targetHeight = 240; // Target height for expanding the panel
                int step = 50;         // Amount to increase or decrease the height by each tick

                if (isExpandingRe)
                {
                    isExpanding = false;
                    customer_t.Start();
                    isExpandingT = false;
                    tran_timer.Start();
                    isExpandingI = false;
                    import_timer.Start();

                    isExpandingM = false;
                    master_timer.Start();

                    isExpandingP = false;
                    purches_timer.Start();
                    isExpandingTax = false;
                    timer_tax.Start();
                    isExpandingR = false;
                    report_timer.Start();
                    isExpandingS = false;
                    supplier_timer.Start();
                    isExpandingSe = false;
                    setting_timer.Start();
                    if (tran_panel.Height < targetHeight)
                    {
                        // Expand the panel
                        record_panel.Height += step;

                        // Ensure the panel does not exceed the target height
                        if (record_panel.Height >= targetHeight)
                        {
                            record_panel.Height = targetHeight; // Set exact target height
                            record_timer.Stop(); // Stop the timer once the target height is reached
                        }
                    }
                }
                else
                {
                    if (record_panel.Height >= 0)
                    {
                        // Collapse the panel
                        record_panel.Height -= step;

                        // Ensure the panel does not go below zero
                        if (record_panel.Height <= 0)
                        {
                            record_panel.Height = 0; // Set exact zero height
                            record_timer.Stop(); // Stop the timer once the panel is fully collapsed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message or handle it accordingly
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void setting_timer_Tick(object sender, EventArgs e)
        {

        }

        private void label64_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("CustomerList");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label63_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("PartiNameList");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }

        }

        private void label65_Click(object sender, EventArgs e)
        {

            try
            {

                ComView cm = new ComView("Itemlist");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }

        }

        private void label68_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Itemlist");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }

        }

        private void label11_Click_1(object sender, EventArgs e)
        {



            try
            {

                ComView cm = new ComView("Customer");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }

        }

        private void label16_Click_1(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("CustTrans");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }
        }

        private void label33_Click(object sender, EventArgs e)
        {

            try
            {

                ComView cm = new ComView("Bill");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }

        }

        private void label32_Click(object sender, EventArgs e)
        {

            try
            {

                ComView cm = new ComView("Purches_Order");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }
        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Purches_Retern");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }

        }

        private void label36_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("PartiNameList");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }
        }

        private void label29_Click(object sender, EventArgs e)
        {

            try
            {

                ComView cm = new ComView("Parties");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }
        }

        private void label28_Click(object sender, EventArgs e)
        {

            try
            {

                ComView cm = new ComView("PartiNameList");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {

            }
        }

        private void label40_Click(object sender, EventArgs e)
        {
            try
            {

                Report_Bill cm = new Report_Bill("Supplier Ledger");
                cm.Show();
                OffOpen();




            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label46_Click(object sender, EventArgs e)
        {
            try
            {

                Report_Bill cm = new Report_Bill("Customer Statment");
                cm.Show();
                OffOpen();




            }
            catch (Exception ex)
            {

            }
        }

        private void label48_Click(object sender, EventArgs e)
        {
            try
            {

                Report_Bill cm = new Report_Bill("Stock Report");
                cm.Show();
                OffOpen();




            }
            catch (Exception ex)
            {

            }
        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Profit");
                cm.Show();
                OffOpen();




            }
            catch (Exception ex)
            {

            }
        }

        private void label24_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Bill");
                cm.Show();
                OffOpen();




            }
            catch (Exception ex)
            {

            }

        }

        private void label23_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Purches_Order");
                cm.Show();
                OffOpen();




            }
            catch (Exception ex)
            {

            }
        }

        private void label25_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Purches_Retern");
                cm.Show();
                OffOpen();




            }
            catch (Exception ex)
            {

            }
        }

        private void import_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                int targetHeight = 97; // Target height for expanding the panel
                int step = 50;         // Amount to increase or decrease the height by each tick

                if (isExpandingI)
                {
                    isExpanding = false;
                    customer_t.Start();
                    isExpandingT = false;
                    tran_timer.Start();
                    isExpandingTax = false;
                    timer_tax.Start();

                    isExpandingRe = false;
                    record_timer.Start();
                    isExpandingM = false;
                    master_timer.Start();

                    isExpandingP = false;
                    purches_timer.Start();
                    isExpandingR = false;
                    report_timer.Start();
                    isExpandingS = false;
                    supplier_timer.Start();
                    isExpandingSe = false;
                    setting_timer.Start();
                    if (tran_panel.Height < targetHeight)
                    {
                        // Expand the panel
                        import_panel.Height += step;

                        // Ensure the panel does not exceed the target height
                        if (import_panel.Height >= targetHeight)
                        {
                            import_panel.Height = targetHeight; // Set exact target height
                            import_timer.Stop(); // Stop the timer once the target height is reached
                        }
                    }
                }
                else
                {
                    if (import_panel.Height >= 0)
                    {
                        // Collapse the panel
                        import_panel.Height -= step;

                        // Ensure the panel does not go below zero
                        if (import_panel.Height <= 0)
                        {
                            import_panel.Height = 0; // Set exact zero height
                            import_timer.Stop(); // Stop the timer once the panel is fully collapsed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message or handle it accordingly
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label36_Click_1(object sender, EventArgs e)
        {
            try
            {
               
                Import_Records records = new Import_Records(null);
                records.Show();
                OffOpen();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label47_Click(object sender, EventArgs e)
        {
            try
            {

                Report_Bill cm = new Report_Bill("Sales Report");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void timer_tax_Tick(object sender, EventArgs e)
        {
            


                     try
            {
                int targetHeight = 260; // Target height for expanding the panel
                int step = 50;         // Amount to increase or decrease the height by each tick

                if (isExpandingTax)
                {
                    isExpanding = false;
                    customer_t.Start();
                    isExpandingT = false;
                    tran_timer.Start();

                    isExpandingI = false;
                    timer_tax.Start();

                    isExpandingRe = false;
                    record_timer.Start();
                    isExpandingM = false;
                    master_timer.Start();

                    isExpandingP = false;
                    purches_timer.Start();
                    isExpandingR = false;
                    report_timer.Start();
                    isExpandingS = false;
                    supplier_timer.Start();
                    isExpandingSe = false;
                    setting_timer.Start();
                    if (tran_panel.Height < targetHeight)
                    {
                        // Expand the panel
                        panel_tax.Height += step;

                        // Ensure the panel does not exceed the target height
                        if (panel_tax.Height >= targetHeight)
                        {
                            panel_tax.Height = targetHeight; // Set exact target height
                            timer_tax.Stop(); // Stop the timer once the target height is reached
                        }
                    }
                }
                else
                {
                    if (panel_tax.Height >= 0)
                    {
                        // Collapse the panel
                        panel_tax.Height -= step;

                        // Ensure the panel does not go below zero
                        if (panel_tax.Height <= 0)
                        {
                            panel_tax.Height = 0; // Set exact zero height
                            timer_tax.Stop(); // Stop the timer once the panel is fully collapsed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message or handle it accordingly
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

        }

        private void label33_Click_1(object sender, EventArgs e)
        {
            try
            {
                Report_Bill cm = new Report_Bill("Sale_GST");
                cm.Show();
                OffOpen();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label70_Click(object sender, EventArgs e)
        {
            try
            {
                GSTR1B cm = new GSTR1B();
                cm.Show();
                OffOpen();
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label32_Click_1(object sender, EventArgs e)
        {
            try
            {
                SALE_REGISTER cm = new SALE_REGISTER();
                cm.Show();
                OffOpen();
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label72_Click(object sender, EventArgs e)
        {
            try
            {
                Purches_Register cm = new Purches_Register();
                cm.Show();
                OffOpen();
            }
            catch (Exception ex)
            {

            }

        }
    }
}