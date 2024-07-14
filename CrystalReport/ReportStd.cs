using Back_Dr.Models;
using Back_Dr.Sale;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using CrystalDecisions.Shared;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
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
        private List<WithoutSaveModels> _models;
        private string path;
        private DataTable table1;

        public ReportStd(string invoice, string place, string areas = "", string citys = "", string credit = "", string from = "", string to = "", string total = "", List<WithoutSaveModels> models = null)
        {
            inv = invoice;
            pl = place;
            _ar = areas;
            _city = citys;
            _credit = credit;
            _from = from;
            _to = to;
            _total = total;
            _models = models;
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




        public DataTable ConvertToDataTable(List<WithoutSaveModels> models)
        {
            DataTable table = new DataTable();

            // Define columns
            table.Columns.Add("Sr", typeof(int));
            table.Columns.Add("ITEM_NAME", typeof(string));
            table.Columns.Add("Qty", typeof(int));
            table.Columns.Add("SALE_PRICE", typeof(decimal));
            table.Columns.Add("discount", typeof(decimal));
            table.Columns.Add("Amount", typeof(decimal));
            table.Columns.Add("invoiceID", typeof(string));
            table.Columns.Add("invdate", typeof(DateTime));
            table.Columns.Add("TotalBill", typeof(decimal));
            table.Columns.Add("cust_Name", typeof(string));

            // Populate rows
            foreach (var model in models)
            {
                table.Rows.Add(model.Sr, model.ITEM_NAME, model.Qty, model.SALE_PRICE, model.discount, model.Amount, model.invoiceID, model.invdate, model.TotalBill, model.cust_Name);
            }

            return table;
        }

        private void WithoutSave()
        {

            ReportDocument report = new ReportDocument();

            path = Application.StartupPath + "/WithoutSaveBil.rpt";


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



            DataTable dataTable = ConvertToDataTable(_models);


            report.Database.Tables["DataTable1"].SetDataSource(dataTable);
            //report.SetParameterValue("place",Address);


            // Verify the report's database
            report.VerifyDatabase();



            crystalReportViewer1.ReportSource = report;

        }













        public void Print(string inv)
        {
            ReportDocument report = new ReportDocument();
            try
            {
                string path = Application.StartupPath + $"/{StoreRoom.Template()}.rpt";

                if (File.Exists(path))
                {
                    report.Load(path);

                    // Set database connection information
                    ConnectionInfo connectionInfo = new ConnectionInfo();
                    connectionInfo.ServerName = @"mrsales";
                    connectionInfo.DatabaseName = "drsale";
                    connectionInfo.UserID = "mrsales";
                    connectionInfo.Password = "mrsale@123";

                    Tables tables = report.Database.Tables;
                    foreach (Table table in tables)
                    {
                        TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                        tableLogOnInfo.ConnectionInfo = connectionInfo;
                        table.ApplyLogOnInfo(tableLogOnInfo);
                    }

                    // Set up parameter fields
                    ParameterFields pfield = new ParameterFields();
                    string cust_s = MainEngine_.GetDataScript<string>("select cust_name from SInvoice where items='" + inv + "'").FirstOrDefault().ToString();

                    // Example for parameter values
                    decimal current = MainEngine_.GetDataScript<decimal>("select TotalBill from SInvoice where items = '" + inv + "'").FirstOrDefault();
                    IEnumerable<decimal> paids = MainEngine_.GetDataScript<decimal>("select Paid from CustomerTransactions where InvoiceId = '" + inv + "'").ToList();
                    decimal paid = paids.Sum();
                    decimal bal = MainEngine_.GetDataScript<decimal>($"SELECT Balance FROM Customer WHERE cust_name = '" + cust_s + "'").FirstOrDefault();
                    bal = Math.Abs(bal);
                    decimal pss = (bal + paid) - current;

                    // Create parameter fields and set values
                    ParameterField tdebit = new ParameterField();
                    ParameterField rcd = new ParameterField();
                    ParameterField old = new ParameterField();

                    ParameterDiscreteValue tdebitval = new ParameterDiscreteValue();
                    tdebit.ParameterFieldName = "blc";
                    tdebitval.Value = $"₹{bal}";
                    tdebit.CurrentValues.Add(tdebitval);

                    ParameterDiscreteValue rcdval = new ParameterDiscreteValue();
                    rcd.ParameterFieldName = "rcd";
                    rcdval.Value = $"₹{paid}";
                    rcd.CurrentValues.Add(rcdval);

                    ParameterDiscreteValue oldval = new ParameterDiscreteValue();
                    old.ParameterFieldName = "prc";
                    oldval.Value = $"₹{pss}";
                    old.CurrentValues.Add(oldval);

                    pfield.Add(tdebit);
                    pfield.Add(rcd);
                    pfield.Add(old);

                    // Set report data source and parameters
                    DataTable table1 = GetTable("SELECT * FROM SInvoice INNER JOIN Sale_Items ON SInvoice.InvoiceID = Sale_Items.Invoice WHERE SInvoice.InvoiceID = '" + inv + "'; ");
                    report.SetDataSource(table1);

                    // Directly print the report without previewing

                    // Set up printer options (optional)
                    PrinterSettings printerSettings = new PrinterSettings();
                    printerSettings.PrinterName = StoreRoom.Printer_Name(); // Set your printer name here

                    // Set paper orientation and other settings if needed
                    PageSettings pageSettings = new PageSettings();
                    pageSettings.Landscape = true; // Example for landscape orientation

                    // Apply printer and page settings to the report
                    report.PrintOptions.PrinterName = printerSettings.PrinterName;

                    report.PrintToPrinter(1, false, 1, 1); // Adjust parameters as needed

                }
                else
                {
                    Console.WriteLine("Report file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error printing report: " + ex.Message);
            }
            finally
            {
                // Clean up resources
                report.Close();
                report.Dispose();
            }
        }









        public void GetReport(string type)
        {

            ReportDocument report = new ReportDocument();
            if (type == "test")
            {

                //   path = Application.StartupPath + "/NewTirupat.rpt";

                path = Application.StartupPath + $"/{StoreRoom.Template()}.rpt";

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

            if (type == "test")
            {

                table1 = GetTable("SELECT * FROM SInvoice INNER JOIN Sale_Items ON SInvoice.InvoiceID = Sale_Items.Invoice WHERE SInvoice.InvoiceID = '" + inv + "'; ");
                string cust_s = MainEngine_.GetDataScript<string>("select cust_name from SInvoice where items='" + inv + "'").FirstOrDefault().ToString();
                decimal current = MainEngine_.GetDataScript<decimal>("select TotalBill from SInvoice where items = '" + inv + "'").FirstOrDefault();

                IEnumerable<decimal> paids = MainEngine_.GetDataScript<decimal>("select Paid from CustomerTransactions where InvoiceId = '" + inv + "'").ToList();
                decimal bal = MainEngine_.GetDataScript<decimal>($"SELECT Balance FROM Customer WHERE cust_name = '" + cust_s + "'").FirstOrDefault();
                decimal paid = paids.Sum();

                bal = Math.Abs(bal);

                ParameterDiscreteValue tdebitval = new ParameterDiscreteValue();
                ParameterDiscreteValue rcdval = new ParameterDiscreteValue();
                rcd.ParameterFieldName = "rcd";
                rcdval.Value = $"₹{paid}";
                rcd.CurrentValues.Add(rcdval);


                tdebit.ParameterFieldName = "blc";// Assuming tdebitval is a Crystal Report field

                if (MainEngine_.GetDataScript<int>("select COUNT(id) from CustomerTransactions where Cust_Name = '" + cust_s + "'").FirstOrDefault() > 0)
                {

                    tdebitval.Value = $"₹{bal}";


                }
                else
                {
                    tdebitval.Value = $"₹{(current) - paid}";

                }
                tdebit.CurrentValues.Add(tdebitval);

                ParameterDiscreteValue oldval = new ParameterDiscreteValue();
                old.ParameterFieldName = "prc";


                decimal pss = (bal + paid) - current;


                oldval.Value = $"₹{pss}";
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

                List<dynamic> getbal = MainEngine_.GetDataScript<dynamic>(inv).ToList();



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








        

  private void GetPurches_Return()
        {
            try
            {


                ReportDocument report = new ReportDocument();

                //   path = Application.StartupPath + "/NewTirupat.rpt";

                path = Application.StartupPath + $"/Purches_Return_Report.rpt";


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






                table1 = GetTable("SELECT * FROM Purches_Return o INNER JOIN purches_Items_return p ON p.Bill = o.BillID where p.Bill = '" + inv + "' ");

                report.SetDataSource(table1);
                //report.SetParameterValue("place",Address);


                // Verify the report's database
                report.VerifyDatabase();

                crystalReportViewer1.ReportSource = report;

            }
            catch (Exception ex)
            {



            }

        }



        private void GetPurches_Order()
        {
            try
            {


                ReportDocument report = new ReportDocument();

                //   path = Application.StartupPath + "/NewTirupat.rpt";

                path = Application.StartupPath + $"/Purches_Order_Report.rpt";


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






                table1 = GetTable("SELECT * FROM Purches_Order o INNER JOIN purches_Items_orders p ON p.Bill = o.BillID where p.Bill = '" + inv + "' ");

                report.SetDataSource(table1);
                //report.SetParameterValue("place",Address);


                // Verify the report's database
                report.VerifyDatabase();

                crystalReportViewer1.ReportSource = report;

            }
            catch (Exception ex)
            {



            }

        }
    



        private void Exp()
        {

            try
            {


                ReportDocument report = new ReportDocument();

                //   path = Application.StartupPath + "/NewTirupat.rpt";

                path = Application.StartupPath + $"/Exp_v.rpt";


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






                table1 = GetTable("SELECT * FROM Kharcha where ID =" + inv + " ");

                report.SetDataSource(table1);
                //report.SetParameterValue("place",Address);


                // Verify the report's database
                report.VerifyDatabase();

                crystalReportViewer1.ReportSource = report;

            }
            catch (Exception ex)
            {



            }
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
               else if(pl == "wsave")
                {

                    WithoutSave();

                }

              else  if(pl== "Purches_Order")
                {


                    GetPurches_Order();

                }


                else if(pl == "Purches_Return")
                {
                    GetPurches_Return();
                }

                else if (pl == "AMIRSHAKH1234")
                {
                    DayBook();
                }

                else if (pl == "Exp")
                {
                    Exp();
                }



               else
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
