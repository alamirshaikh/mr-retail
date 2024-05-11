using Back_Dr.Sale;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ConnectionInfo = CrystalDecisions.Shared.ConnectionInfo;
using ParameterField = CrystalDecisions.Shared.ParameterField;
using Table = CrystalDecisions.CrystalReports.Engine.Table;
using Tables = CrystalDecisions.CrystalReports.Engine.Tables;

namespace CrystalReport
{
    public partial class ReportStd : Form
    {
        private readonly string inv;
        public string Address { get; set; }
        public string pl = "";
        private string _ar;
        private string _city;
        private string _credit;
        private string _from;
        private string _to;
        private string _total;
        private string path;
        private DataTable table1;

        public ReportStd(string invoice, string place, string areas = "", string citys = "", string credit = "", string from = "", string to = "", string total = "")
        {
            inv = invoice;
            pl = place;
            _ar = areas;
            _city = citys;
            _credit = credit;
            _from = from;
            _to = to;
            _total = total;
            InitializeComponent();
        }


        public DataTable GetTable(string sql)
        {
            DataTable table = new DataTable();

            // Assuming you have a valid connection string


            using (SqlConnection conn = new SqlConnection(MainEngine_.SERVER_PATH))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }




        public void DayBook()
        {

            ReportDocument report = new ReportDocument();

            path = Application.StartupPath + "/DailyReport.rpt";


            report.Load(path);
            // Set database login information for the report
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ServerName = @"mrsales"; // Replace with your server name
            connectionInfo.DatabaseName = "drsale";
            connectionInfo.UserID = "mrsales"; // Replace with your database username
            connectionInfo.Password = "mrsale@123"; // Replace with your database password


            Tables tables = report.Database.Tables;
            foreach (Table table in tables)
            {
                TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                tableLogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogOnInfo);
            }

            // Apply SQL command to the report
            // Replace with the desired invoice ID
            ParameterField fromdate = new ParameterField();
            ParameterField todate = new ParameterField();
            ParameterField credit = new ParameterField();
            ParameterField online = new ParameterField();
            ParameterField cash = new ParameterField();

            ParameterField total = new ParameterField();








            ParameterFields pfield = new ParameterFields();



            table1 = GetTable(inv);
            ParameterDiscreteValue fromdateval = new ParameterDiscreteValue();
            ParameterDiscreteValue todateval = new ParameterDiscreteValue();
            ParameterDiscreteValue creditval = new ParameterDiscreteValue();
            ParameterDiscreteValue onlineval = new ParameterDiscreteValue();
            ParameterDiscreteValue cashval = new ParameterDiscreteValue();
            ParameterDiscreteValue totalval = new ParameterDiscreteValue();




            fromdate.ParameterFieldName = "fromdate";
            todate.ParameterFieldName = "todate";
            credit.ParameterFieldName = "credit";
            online.ParameterFieldName = "online";
            cash.ParameterFieldName = "cash";
            total.ParameterFieldName = "total";




            fromdateval.Value = _from;
            fromdate.CurrentValues.Add(fromdateval);


            todateval.Value = _to;
            todate.CurrentValues.Add(todateval);

            creditval.Value = $"₹{_credit}";
            credit.CurrentValues.Add(creditval);

            onlineval.Value = $"₹{_city}";

            online.CurrentValues.Add(onlineval);

            cashval.Value = $"₹{_ar}";

            cash.CurrentValues.Add(cashval);

            totalval.Value = $"₹{_total}";

            total.CurrentValues.Add(totalval);



            pfield.Add(fromdate);
            pfield.Add(todate);

            pfield.Add(total);
            pfield.Add(cash);
            pfield.Add(credit);
            pfield.Add(online);
            pfield.Add(total);






            report.SetDataSource(table1);
            //report.SetParameterValue("place",Address);


            // Verify the report's database
            report.VerifyDatabase();

            crystalReportViewer1.ParameterFieldInfo = pfield;

            crystalReportViewer1.ReportSource = report;


        }





    
 























        public void GetReport(string type)
        {
             
            ReportDocument report = new ReportDocument();
            if (type == "test")
            {

                  path = Application.StartupPath + "/NewTirupat.rpt";
            }
            else
            {
                  path = Application.StartupPath + "/CustomerCreditBill.rpt";

            }
            report.Load(path);
            // Set database login information for the report
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ServerName = @"mrsales"; // Replace with your server name
            connectionInfo.DatabaseName = "drsale";
            connectionInfo.UserID = "mrsales"; // Replace with your database username
            connectionInfo.Password = "mrsale@123"; // Replace with your database password


            Tables tables = report.Database.Tables;
            foreach (Table table in tables)
            {
                TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                tableLogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogOnInfo);
            }

            // Apply SQL command to the report
            // Replace with the desired invoice ID
            ParameterField tdebit = new ParameterField();
            ParameterField rcd = new ParameterField();
            ParameterField old = new ParameterField();

            ParameterField city = new ParameterField();
            ParameterField area = new ParameterField();
            ParameterField totalblc = new ParameterField();



            ParameterFields pfield = new ParameterFields();

            if (type=="test")
            {

                  table1 = GetTable("SELECT * FROM SInvoice INNER JOIN Sale_Items ON SInvoice.InvoiceID = Sale_Items.Invoice WHERE SInvoice.InvoiceID = '" + inv + "'; ");
                string cust_s = MainEngine_.GetDataScript<string>("select cust_name from SInvoice where items='" + inv + "'").FirstOrDefault().ToString();
                decimal current = MainEngine_.GetDataScript<decimal>("select TotalBill from SInvoice where items = '"+inv+"'").FirstOrDefault();

                IEnumerable<decimal> paids = MainEngine_.GetDataScript<decimal>("select Paid from CustomerTransactions where InvoiceId = '" + inv+"'").ToList();
                decimal bal = MainEngine_.GetDataScript<decimal>($"SELECT Balance FROM Customer WHERE cust_name = '" + cust_s + "'").FirstOrDefault();
                decimal paid = paids.Sum();

                bal = Math.Abs(bal);

                ParameterDiscreteValue tdebitval = new ParameterDiscreteValue();
                ParameterDiscreteValue rcdval = new ParameterDiscreteValue();
                rcd.ParameterFieldName = "rcd";
                rcdval.Value = $"₹{paid}";
                rcd.CurrentValues.Add(rcdval);


                tdebit.ParameterFieldName = "blc";// Assuming tdebitval is a Crystal Report field

                if (MainEngine_.GetDataScript<int>("select COUNT(id) from CustomerTransactions where Cust_Name = '"+cust_s+"'").FirstOrDefault() > 0)
                {

                    tdebitval.Value = $"₹{(bal) - paid}";

                }
                else
                {
                    tdebitval.Value = $"₹{(current) - paid}";

                }
                tdebit.CurrentValues.Add(tdebitval);

                ParameterDiscreteValue oldval = new ParameterDiscreteValue();
                old.ParameterFieldName = "prc";
                oldval.Value = $"₹{bal-current}";
                old.CurrentValues.Add(oldval);

                pfield.Add(tdebit);
                pfield.Add(old);
                pfield.Add(rcd);



            }
            else
            {

                table1 = GetTable(inv);
                ParameterDiscreteValue areaval = new ParameterDiscreteValue();
                ParameterDiscreteValue cityval = new ParameterDiscreteValue();
                ParameterDiscreteValue totalblcval = new ParameterDiscreteValue();

                List<dynamic> getbal = MainEngine_.GetDataScript<dynamic>(inv).ToList() ;

                 

                decimal s = 0;
                foreach (var item in getbal)
                {
                    s += Math.Abs(item.Balance);
                }

                area.ParameterFieldName = "area";
                city.ParameterFieldName = "city";
                totalblc.ParameterFieldName = "balance";

                areaval.Value = _ar;
                area.CurrentValues.Add(areaval);
                cityval.Value = _city;
                city.CurrentValues.Add(cityval);
                totalblcval.Value = $"₹{s}";
                totalblc.CurrentValues.Add(totalblcval);



                pfield.Add(area);
                pfield.Add(city);
                pfield.Add(totalblc);

            }



            report.SetDataSource(table1);
            //report.SetParameterValue("place",Address);

 
            // Verify the report's database
            report.VerifyDatabase();

            crystalReportViewer1.ParameterFieldInfo = pfield;
            
            crystalReportViewer1.ReportSource = report;


        }










 


















        private void button1_Click(object sender, EventArgs e)
        { 
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {


                if (pl == "AMIRSHAKH123")
                {
                    GetReport("nion");
                }

                if (pl == "AMIRSHAKH1234")
                {
                    DayBook();
                }
                if (pl != "AMIRSHAKH1234" || pl!= "AMIRSHAKH123")
                {
                    Crys crs = new Crys();

                    GetReport("test");

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.PrintReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
