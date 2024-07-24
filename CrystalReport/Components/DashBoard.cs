using Back_Dr.Sale;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CrystalReport.Components
{
    public partial class DashBoard : UserControl
    {
        private Bitmap image;

        public DashBoard()
        {
            InitializeComponent();
        }


        private void GetCurrent()
        {
            /*
            int currentYear = DateTime.Now.Year;

            // Set chart properties
            chart1.Titles.Add($"Monthly Data for {currentYear}");
            chart1.ChartAreas.Clear();
            chart1.ForeColor = System.Drawing.Color.White;
         
            var chartArea = new ChartArea("MainArea")
            {
                BackColor = System.Drawing.Color.WhiteSmoke,
                BorderWidth = 1,
                ShadowColor = System.Drawing.Color.Gray,
                ShadowOffset = 2
            };
            chart1.ChartAreas.Add(chartArea);

            // Create a new series and set its chart type
            Series series = new Series("Data")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = System.Drawing.Color.DodgerBlue,
                ShadowColor = System.Drawing.Color.Gray,
                ShadowOffset = 2
            };







            // Example monthly data
            var monthlyData = new Dictionary<string, double>
            {
                { "Jan", Convert.ToDouble(StoreRoom.GetValueMonth(1)) },
                { "Feb", Convert.ToDouble(StoreRoom.GetValueMonth(2)) },
                { "Mar", Convert.ToDouble(StoreRoom.GetValueMonth(3)) },
                { "Apr", Convert.ToDouble(StoreRoom.GetValueMonth(4)) },
                { "May", Convert.ToDouble(StoreRoom.GetValueMonth(5)) },
                { "Jun", Convert.ToDouble(StoreRoom.GetValueMonth(6)) },
                { "Jul", Convert.ToDouble(StoreRoom.GetValueMonth(7)) },
                { "Aug", Convert.ToDouble(StoreRoom.GetValueMonth(8)) },
                { "Sep", Convert.ToDouble(StoreRoom.GetValueMonth(9)) },
                { "Oct", Convert.ToDouble(StoreRoom.GetValueMonth(10)) },
                { "Nov", Convert.ToDouble(StoreRoom.GetValueMonth(11)) },
                { "Dec", Convert.ToDouble(StoreRoom.GetValueMonth(12)) }
            };

            // Add data points to the series
            foreach (var point in monthlyData)
            {
                series.Points.AddXY(point.Key, point.Value);
            }

            // Add the series to the chart
          //  chart1.Series.Clear();
            //chart1.Series.Add(series);

            // Customize the chart appearance
         //   chart1.ChartAreas[0].AxisX.Title = "Month";
            //chart1.ChartAreas[0].AxisY.Title = "Value";
           // chart1.ChartAreas[0].AxisX.Interval = 1;
           // chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
           // chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
           // chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
           // chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // Enable tooltips
            chart1.Series[0].ToolTip = "Value: #VALY";

            // Ensure chart resizes with the form
            //  chart1.Dock = DockStyle.Fill;
           
            // Additional customizations
            chart1.Legends.Clear();
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            chart1.Series[0].MarkerSize = 10;
            chart1.Series[0].MarkerColor = System.Drawing.Color.Red;

            */

        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            textBox1.Text = string.Format("₹{0:N2}", StoreRoom.TodaysSales());
            textBox2.Text = string.Format("₹{0:N2}", StoreRoom.MonthSales());
            textBox3.Text = StoreRoom.CustomerCount().ToString(); // Assuming CustomerCount is a count and not an amount
            textBox4.Text = string.Format("₹{0:N2}", StoreRoom.Balance());



            state.Text = MainEngine_.GetDataScript<string>("Select State from OwnerInformation").FirstOrDefault();
            num.Text = MainEngine_.GetDataScript<string>("Select ShopMobile from OwnerInformation").FirstOrDefault();
            address.Text = MainEngine_.GetDataScript<string>("Select BusinessAddress from OwnerInformation").FirstOrDefault();
            gst.Text = MainEngine_.GetDataScript<string>("Select GSTIN from Tax").FirstOrDefault();
             

            //  GetCurrent();


            string colorpath = Application.StartupPath + "/company.png";

            string path = Application.StartupPath;
            string[] files = Directory.GetFiles(path, "company.png");

            image = new Bitmap(colorpath);
            // Identify the background color
            //Color backgroundColor = StoreRoom.IdentifyBackgroundColor(image);
            //panel5.BackColor = backgroundColor;
             
            if (files.Length > 0)
            {
                try
                {
                    pictureBox5.Image = Image.FromFile(files[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
            else
            {

            }





            try
            {
                label6.Text = StoreRoom.ShopNames();

            }
            catch (Exception ex)
            {
                 
            }

        }

        private void panelssssss_MouseEnter(object sender, EventArgs e)
        {
            panelssssss.BackColor = Color.Chocolate;
        }

        private void panelssssss_MouseLeave(object sender, EventArgs e)
        {
    
            panelssssss.BackColor = Color.FromArgb(34, 36, 50);

        }

        private void panelssssss_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Invoice");
                cm.Show();
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            ComView cm = new ComView("Invoice");
            cm.Show();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Expenses");
                cm.Show();


            }
            catch (Exception ex)
            {
                 
            }
        }

        private void panelssssss_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

            ComView cm = new ComView("Payment");
            cm.Show();
        }

        private void panel12_Click(object sender, EventArgs e)
        {

            ComView cm = new ComView("Payment");
            cm.Show();
        }

        private void label19_Click(object sender, EventArgs e)
        {

            ComView cm = new ComView("Expenses");
            cm.Show();
        }

        private void panel16_Click(object sender, EventArgs e)
        {
            ComView cm = new ComView("Purches_Order");
            cm.Show();
        }

        private void label15_Click(object sender, EventArgs e)
        {

            ComView cm = new ComView("Purches_Order");
            cm.Show();
        }

        private void panel15_Click(object sender, EventArgs e)
        {
            try
            {
                ComView cm = new ComView("ViewPurches_Orders");
                cm.Show();
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
     
            ComView cm = new ComView("Purches_Retern");

            cm.Show();

 

           
        }

        private void label24_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Customer_pay");

                cm.Show();


            }
            catch (Exception ex)
            {
                 
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            try
            {

                ComView cm = new ComView("Customer_pay");

                cm.Show();


            }
            catch (Exception ex)
            {

            }
        }
    }
    }

