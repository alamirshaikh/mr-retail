using Back_Dr.Models;
using Back_Dr.Sale;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using CrystalReport.Components;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using ConnectionInfo = CrystalDecisions.Shared.ConnectionInfo;
 
using ParameterField = CrystalDecisions.Shared.ParameterField;
using Table = CrystalDecisions.CrystalReports.Engine.Table;
using Tables = CrystalDecisions.CrystalReports.Engine.Tables;

namespace CrystalReport
{
    public partial class ReportStd : Form
    {
        public   string inv;
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
        private string _isInvoie = "no";
        private DataTable table1;
        private string _i;
        private string _p;
        private string tss;


       

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

        public void OpenUrlInBrowser(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening URL: {ex.Message}");
            }
        }


        public void GetLeader(string type)
        {

            string path = "";
            ReportDocument report = new ReportDocument();

            if (type == "sup")
            {
                path = Application.StartupPath + "/Ledger.rpt";
                _isInvoie = "no";


               
            }
            else
            {
                path = Application.StartupPath + "/CustomerLedger.rpt";
                _isInvoie = "no";

            
            
            }




            report.Load(path);

            // Set database login information for the report
            ConnectionInfo connectionInfo = new ConnectionInfo
            {
                ServerName = @"localhost\sqlexpress", // Replace with your server name
                DatabaseName = "drsale",
                UserID = "mrsales", // Replace with your database username
                Password = "mrsale@123" // Replace with your database password
            };

            Tables tables = report.Database.Tables;
            foreach (Table table in tables)
            {
                TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                tableLogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogOnInfo);
            }



            if (type == "sup")
            {


                string invoiceId = inv;
                string startDate = $"{StoreRoom.dateTimePicker1.Date.ToString("yyyy-MM-dd")} 00:00:00";
                string endDate = $"{StoreRoom.dateTimePicker2.Date.ToString("yyyy-MM-dd")} 23:59:59";

                // Calculate balance
                decimal? balanceValue = MainEngine_.GetDataScript<decimal?>($@"
    SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) - SUM(CAST(Debit AS DECIMAL(18, 2))) AS CreditMinusDebit 
    FROM Ledger_Supplier 
    WHERE SupplierID = {invoiceId} AND Date >= '{startDate}' AND Date <= '{endDate}'
").FirstOrDefault();

                // Ensure balance is non-negative or default to 0 if null
                decimal blcvlc = balanceValue.HasValue ? Math.Max(balanceValue.Value, 0) : 0;

                // Calculate total credit
                decimal? totalCredit = MainEngine_.GetDataScript<decimal?>($@"
    SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) AS TotalCredit 
    FROM Ledger_Supplier 
    WHERE SupplierID = {invoiceId} AND Date >= '{startDate}' AND Date <= '{endDate}'
").FirstOrDefault();

                // Ensure total credit is non-negative or default to 0 if null
                decimal totalcreditvlc = totalCredit.HasValue ? Math.Max(totalCredit.Value, 0) : 0;

                // Calculate total debit
                decimal? totalDebit = MainEngine_.GetDataScript<decimal?>($@"
    SELECT SUM(CAST(Debit AS DECIMAL(18, 2))) AS TotalDebit 
    FROM Ledger_Supplier 
    WHERE SupplierID = {invoiceId} AND Date >= '{startDate}' AND Date <= '{endDate}'
").FirstOrDefault();

                // Ensure total debit is non-negative or default to 0 if null
                decimal totaldevitvlc = totalDebit.HasValue ? Math.Max(totalDebit.Value, 0) : 0;

                // Set up parameters for the report
                ParameterFields pfield = new ParameterFields();

                ParameterField balance = new ParameterField();
                balance.ParameterFieldName = "balance";
                balance.CurrentValues.Add(new ParameterDiscreteValue { Value = blcvlc });

                ParameterField tcredit = new ParameterField();
                tcredit.ParameterFieldName = "tcredit";
                tcredit.CurrentValues.Add(new ParameterDiscreteValue { Value = totalcreditvlc });

                ParameterField tdebit = new ParameterField();
                tdebit.ParameterFieldName = "tdebit";
                tdebit.CurrentValues.Add(new ParameterDiscreteValue { Value = totaldevitvlc });

                ParameterField dateRange = new ParameterField();
                dateRange.ParameterFieldName = "ddate";
                dateRange.CurrentValues.Add(new ParameterDiscreteValue { Value = $"FROM {StoreRoom.dateTimePicker1.ToString("yyyy-MM-dd")} TO {StoreRoom.dateTimePicker2.ToString("yyyy-MM-dd")}" });

                pfield.Add(dateRange);
                pfield.Add(balance);
                pfield.Add(tcredit);
                pfield.Add(tdebit);

                // Set parameters to Crystal Report Viewer
                crystalReportViewer1.ParameterFieldInfo = pfield;

                // Retrieve data table
                DataTable table1 = GetTable($"SELECT * FROM Ledger_Supplier WHERE SupplierID = {invoiceId} AND Date >= '{startDate}' AND Date <= '{endDate}'");
                report.SetDataSource(table1);

                // Set the report source for the CrystalReportViewer
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.Zoom(2);


            }
            else
            {
                // Retrieve inputs
                string invoiceId = inv;
                DateTime startDate = StoreRoom.dateTimePicker1.Date;
                DateTime endDate = StoreRoom.dateTimePicker2.Date;

                // Retrieve data table
                DataTable table1 = GetTable($@"
    SELECT * 
    FROM Ledger_Customer 
    WHERE CustomerID = '{invoiceId}' 
    AND Date >= '{startDate:yyyy-MM-dd} 00:00:00' 
    AND Date <= '{endDate:yyyy-MM-dd} 23:59:59'
");

                // Set the retrieved data table as the report data source
                report.SetDataSource(table1);

                // Define parameter fields for additional data (balance, total credit, total debit, date range)
                ParameterFields pfield = new ParameterFields();

                // Balance parameter field
                ParameterField balance = new ParameterField();
                ParameterDiscreteValue balanceVal = new ParameterDiscreteValue();
                balance.ParameterFieldName = "balance";

                // Calculate balance
                decimal? balanceResult = MainEngine_.GetDataScript<decimal?>($@"
    SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) - SUM(CAST(Debit AS DECIMAL(18, 2))) AS CreditMinusDebit 
    FROM Ledger_Customer 
    WHERE CustomerID = {invoiceId} 
    AND Date >= '{startDate:yyyy-MM-dd} 00:00:00' 
    AND Date <= '{endDate:yyyy-MM-dd} 23:59:59'
").FirstOrDefault();

                balanceVal.Value = balanceResult ?? 0.00m;
                balance.CurrentValues.Add(balanceVal);
                pfield.Add(balance);

                // Total credit parameter field
                ParameterField tcredit = new ParameterField();
                ParameterDiscreteValue tcreditVal = new ParameterDiscreteValue();
                tcredit.ParameterFieldName = "tcredit";

                // Calculate total credit
                decimal? totalCreditResult = MainEngine_.GetDataScript<decimal?>($@"
    SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) AS TotalCredit 
    FROM Ledger_Customer 
    WHERE CustomerID = {invoiceId} 
    AND Date >= '{startDate:yyyy-MM-dd} 00:00:00' 
    AND Date <= '{endDate:yyyy-MM-dd} 23:59:59'
").FirstOrDefault();

                tcreditVal.Value = totalCreditResult ?? 0.00m;
                tcredit.CurrentValues.Add(tcreditVal);
                pfield.Add(tcredit);

                // Total debit parameter field
                ParameterField tdebit = new ParameterField();
                ParameterDiscreteValue tdebitVal = new ParameterDiscreteValue();
                tdebit.ParameterFieldName = "tdebit";

                // Calculate total debit
                decimal? totalDebitResult = MainEngine_.GetDataScript<decimal?>($@"
    SELECT SUM(CAST(Debit AS DECIMAL(18, 2))) AS TotalDebit 
    FROM Ledger_Customer 
    WHERE CustomerID = {invoiceId} 
    AND Date >= '{startDate:yyyy-MM-dd} 00:00:00' 
    AND Date <= '{endDate:yyyy-MM-dd} 23:59:59'
").FirstOrDefault();

                tdebitVal.Value = totalDebitResult ?? 0.00m;
                tdebit.CurrentValues.Add(tdebitVal);
                pfield.Add(tdebit);

                // Date parameter field
                ParameterField date = new ParameterField();
                ParameterDiscreteValue dateVal = new ParameterDiscreteValue();
                date.ParameterFieldName = "ddate";
                dateVal.Value = $"(FROM {startDate:yyyy-MM-dd} TO {endDate:yyyy-MM-dd})";
                date.CurrentValues.Add(dateVal);
                pfield.Add(date);

                // Verify and set report parameters
                report.VerifyDatabase();

                // Set the report source for the CrystalReportViewer
                crystalReportViewer1.ParameterFieldInfo = pfield;
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.Zoom(2);

            }


            // Set report parameters if needed
            // report.SetParameterValue("place", Address);

            // Verify the report's database

        }



        public void GetRecipt(string type)
        {


            ReportDocument report = new ReportDocument();
            string path = Application.StartupPath + "/NewTirupat.rpt";
            report.Load(path);

            // Set database login information for the report
            ConnectionInfo connectionInfo = new ConnectionInfo
            {
                ServerName = @"mrsales", // Replace with your server name
                DatabaseName = "drsale",
                UserID = "mrsales", // Replace with your database username
                Password = "mrsale@123" // Replace with your database password
            };

            Tables tables = report.Database.Tables;
            foreach (Table table in tables)
            {
                TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                tableLogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogOnInfo);
            }

            ParameterField tdebit = new ParameterField();
            ParameterField rcd = new ParameterField();
            ParameterField old = new ParameterField();

            table1 = GetTable("SELECT * FROM SInvoice INNER JOIN Sale_Items ON SInvoice.InvoiceID = Sale_Items.Invoice WHERE SInvoice.InvoiceID = '" + inv + "'; ");
            string cust_s = MainEngine_.GetDataScript<string>("select cust_name from SInvoice where items='" +inv + "'").FirstOrDefault().ToString();
            decimal current = MainEngine_.GetDataScript<decimal>("select TotalBill from SInvoice where items = '" + inv + "'").FirstOrDefault();

            decimal paid = MainEngine_.GetDataScript<decimal>("select Paid from CustomerTransactions where InvoiceId = '" + inv + "'").FirstOrDefault();
            decimal bal = MainEngine_.GetDataScript<decimal>($"SELECT Balance FROM Customer WHERE cust_name = '" + cust_s + "'").FirstOrDefault();

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



            ParameterFields pfield = new ParameterFields();

            pfield.Add(tdebit);
            pfield.Add(old);
            pfield.Add(rcd);



            report.SetDataSource(table1);
            //report.SetParameterValue("place",Address);


            // Verify the report's database
            report.VerifyDatabase();

            crystalReportViewer1.ParameterFieldInfo = pfield;

            crystalReportViewer1.ReportSource = report;

        }





        public void GetReports(string type)
        {


            ReportDocument report = new ReportDocument();
            string path = Application.StartupPath + "/SalesReport.rpt";

            report.Load(path);
            // Set database login information for the report
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ServerName = @"localhost\sqlexpress"; // Replace with your server name
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
            DataTable table1 = GetTable($"exec GetSaleReport '{StoreRoom.dateTimePicker1}', '{StoreRoom.dateTimePicker2}'");

            report.SetDataSource(table1);
            ParameterFields pfield = new ParameterFields();
            ParameterField ptitle = new ParameterField();
            ParameterDiscreteValue pvalue = new ParameterDiscreteValue();
            ptitle.ParameterFieldName = "higy";
            pvalue.Value = MainEngine_.GetDataScript<string>("Select description from HighSale").FirstOrDefault() ?? "NULL";
            ptitle.CurrentValues.Add(pvalue);


            ParameterField low = new ParameterField();
            ParameterDiscreteValue lowval = new ParameterDiscreteValue();
            low.ParameterFieldName = "low";
            lowval.Value = MainEngine_.GetDataScript<string>("Select description from LowSale").FirstOrDefault() ?? "NULL";
            low.CurrentValues.Add(lowval);




            ParameterField nosales = new ParameterField();
            ParameterDiscreteValue nosalesval = new ParameterDiscreteValue();
            nosales.ParameterFieldName = "nosales";
            nosalesval.Value = MainEngine_.GetDataScript<string>("Select TotalSales from TotalSale").FirstOrDefault() ?? "NULL";
            nosales.CurrentValues.Add(nosalesval);

            ParameterField tsales = new ParameterField();
            ParameterDiscreteValue tsalesval = new ParameterDiscreteValue();
            tsales.ParameterFieldName = "tsales";
            tsalesval.Value = MainEngine_.GetDataScript<string>("Select TotalAmount from TotalSale").FirstOrDefault() ?? "NULL";
            tsales.CurrentValues.Add(tsalesval);


            ParameterField ddate = new ParameterField();
            ParameterDiscreteValue ddateval = new ParameterDiscreteValue();
            ddate.ParameterFieldName = "ddate";
            ddateval.Value = $"{StoreRoom.dateTimePicker1} - {StoreRoom.dateTimePicker2}";
            ddate.CurrentValues.Add(ddateval);




            pfield.Add(ptitle);
            pfield.Add(low);
            pfield.Add(nosales);
            pfield.Add(tsales);
            pfield.Add(ddate);






            // Verify the report's database
            report.VerifyDatabase();
            crystalReportViewer1.ParameterFieldInfo = pfield;

            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();

            crystalReportViewer1.Zoom(2);
        }





        public void GetStock()
        {


            ReportDocument report = new ReportDocument();
            string path = Application.StartupPath + "/StockReport.rpt";
            report.Load(path);
            // Set database login information for the report
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ServerName = @"localhost\sqlexpress"; // Replace with your server name
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
            DataTable table1 = GetTable($"select * from Product_Item");

            report.SetDataSource(table1);

            // Verify the report's database
            report.VerifyDatabase();
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();

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

            _isInvoie = "yes";
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







        public void GetSaleReport(string type, string ins = "")
        {

            ReportDocument report = new ReportDocument();
          

                //   path = Application.StartupPath + "/NewTirupat.rpt";

                path = Application.StartupPath + $"/Sale_GST_REPORT.rpt";
       
            
            
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
            ParameterField CGST = new ParameterField();
            ParameterField SGST = new ParameterField();
            ParameterField IGST = new ParameterField();




            ParameterFields pfield = new ParameterFields();

            if (type == "test")
            {



                string startDate = $"{StoreRoom.dateTimePicker1.Date.ToString("yyyy-MM-dd")}";
                string endDate = $"{StoreRoom.dateTimePicker2.Date.ToString("yyyy-MM-dd")}";


                table1 = GetTable("SELECT * FROM SInvoice WHERE invdate>='"+ startDate + "' AND invdate<='"+ endDate + "' AND TAX = 'GST' ");





                IEnumerable<decimal> cgst = MainEngine_.GetDataScript<decimal>($"SELECT sCGST FROM SInvoice WHERE invdate>='" + startDate + "' AND invdate<='" + endDate + "' AND TAX ='GST'  ").ToList();
                IEnumerable<decimal> sgst = MainEngine_.GetDataScript<decimal>($"SELECT sSGST FROM SInvoice WHERE invdate>='" + startDate + "' AND invdate<='" + endDate + "' AND TAX ='GST' ").ToList();
                IEnumerable<decimal> igst = MainEngine_.GetDataScript<decimal>($"SELECT sIGST FROM SInvoice WHERE invdate>='" + startDate + "' AND invdate<='" + endDate + "' AND TAX ='GST' ").ToList();

                decimal paidcgst = cgst.Sum();
                decimal paidsgst = sgst.Sum();
                decimal paidigst = igst.Sum();

                 

                    ParameterDiscreteValue cgstvalue = new ParameterDiscreteValue();
                    ParameterDiscreteValue sgstvalue = new ParameterDiscreteValue();
                ParameterDiscreteValue igstvalue = new ParameterDiscreteValue();

                CGST.ParameterFieldName = "CGST";
                    cgstvalue.Value = $"{paidcgst}";
                
                CGST.CurrentValues.Add(cgstvalue);



                SGST.ParameterFieldName = "SGST";
                sgstvalue.Value = $"{paidsgst}";

                SGST.CurrentValues.Add(sgstvalue);

                IGST.ParameterFieldName = "IGST";
                igstvalue.Value = $"{paidigst}";

                IGST.CurrentValues.Add(igstvalue);





                    pfield.Add(CGST);
                    pfield.Add(SGST);
                    pfield.Add(IGST);
                }


            
         

            report.SetDataSource(table1);
            //report.SetParameterValue("place",Address);


            // Verify the report's database
            report.VerifyDatabase();


          
            
                crystalReportViewer1.ParameterFieldInfo = pfield;

                crystalReportViewer1.ReportSource = report;

         




        }


        public ReportDocument GetReport(string type,string ins="")
        {

            ReportDocument report = new ReportDocument();
            if (type == "test")
            {

                //   path = Application.StartupPath + "/NewTirupat.rpt";

                path = Application.StartupPath + $"/{StoreRoom.Template()}.rpt";
                _isInvoie = "yes";
            }
            else
            {
                path = Application.StartupPath + "/CustomerCreditBill.rpt";
                _isInvoie = "no";

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


                if (ins!="")
                {
                    inv = ins;



                    table1 = GetTable("SELECT * FROM SInvoice INNER JOIN Sale_Items ON SInvoice.InvoiceID = Sale_Items.Invoice WHERE SInvoice.InvoiceID = '" + inv + "'; ");
                    string cust_s = MainEngine_.GetDataScript<string>("select cust_name from SInvoice where items='" + inv + "'").FirstOrDefault().ToString();
                    decimal current = MainEngine_.GetDataScript<decimal>("select TotalBill from SInvoice where items = '" + inv + "'").FirstOrDefault();

                    IEnumerable<decimal> paids = MainEngine_.GetDataScript<decimal>("select Paid from CustomerTransactions where InvoiceId = '" + inv + "'").ToList();


                    num.Text = MainEngine_.GetDataScript<string>("select cust_phone from Customer where cust_name = '"+cust_s+ "'").FirstOrDefault();

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

                    pfield.Add(CreateParameterField("blc", $"₹{bal}"));
                    pfield.Add(CreateParameterField("rcd", $"₹{paid}"));
                    pfield.Add(CreateParameterField("prc", $"₹{(bal + paid) - current}"));
                }
                else
                {


                    table1 = GetTable("SELECT * FROM SInvoice INNER JOIN Sale_Items ON SInvoice.InvoiceID = Sale_Items.Invoice WHERE SInvoice.InvoiceID = '" + inv + "'; ");
                    string cust_s = MainEngine_.GetDataScript<string>("select cust_name from SInvoice where items='" + inv + "'").FirstOrDefault().ToString();
                    decimal current = MainEngine_.GetDataScript<decimal>("select TotalBill from SInvoice where items = '" + inv + "'").FirstOrDefault();

                    IEnumerable<decimal> paids = MainEngine_.GetDataScript<decimal>("select Paid from CustomerTransactions where InvoiceId = '" + inv + "'").ToList();


                    num.Text = MainEngine_.GetDataScript<string>("select cust_phone from Customer where cust_name = '" + cust_s + "'").FirstOrDefault();


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


            if (!string.IsNullOrEmpty(ins))
            {
                CrystalReportViewer crs = new CrystalReportViewer
                {
                    ParameterFieldInfo = pfield,
                    ReportSource = report
                };
                // Add the viewer to the form if necessary
                this.Controls.Add(crs);
            }
            else
            {
                crystalReportViewer1.ParameterFieldInfo = pfield;

                crystalReportViewer1.ReportSource = report;

            }
      


            return report;

        }



        private ParameterField CreateParameterField(string name, object value)
        {
            ParameterField parameterField = new ParameterField
            {
                ParameterFieldName = name
            };
            ParameterDiscreteValue discreteValue = new ParameterDiscreteValue
            {
                Value = value
            };
            parameterField.CurrentValues.Add(discreteValue);
            return parameterField;
        }






        private void GetPurches_Return()
        {
            try
            {


                ReportDocument report = new ReportDocument();

                //   path = Application.StartupPath + "/NewTirupat.rpt";

                path = Application.StartupPath + $"/Purches_Return_Report.rpt";

                _isInvoie = "no";
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






                table1 = GetTable("SELECT * FROM Purches_Return o INNER JOIN purches_Items_return p ON p.Bill = o.BillID where p.prb_bill = '" + inv + "' ");

                


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














        private void GetSale_Return()
        {
            try
            {


                ReportDocument report = new ReportDocument();

                //   path = Application.StartupPath + "/NewTirupat.rpt";

                path = Application.StartupPath + $"/Sale_Retern_Report.rpt";

                _isInvoie = "no";
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






                table1 = GetTable("SELECT * FROM Sale_Return o INNER JOIN Sale_Items_return p ON p.Bill = o.BillID where p.prb_bill = '" + inv + "' ");

                string cust_s = MainEngine_.GetDataScript<string>("select partiname from Sale_Return where items='" + inv + "'").FirstOrDefault();
 

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

                _isInvoie = "no";
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

                string cust_s = MainEngine_.GetDataScript<string>("select partiname from Purches_Order where items='" + inv + "'").FirstOrDefault();

                num.Text = MainEngine_.GetDataScript<string>("select company from Parties where company = '" + cust_s + "'").FirstOrDefault();


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



        private void Cust_Payment()
        {
             
                try
                {


                    ReportDocument report = new ReportDocument();

                    //   path = Application.StartupPath + "/NewTirupat.rpt";

                    path = Application.StartupPath + $"/CUST_RECIPT.rpt";

                _isInvoie = "no";
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






                    table1 = GetTable("select * from CustomerTransactions where InvoiceId ='" + inv + "' ");

                
                
                
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
                _isInvoie = "no";

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





        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {

                CrystalDecisions.Shared.PaperSize MapPaperSize(string paperSize)
                {
                    switch (paperSize)
                    {
                        case "PaperA4":
                            return CrystalDecisions.Shared.PaperSize.PaperA4;
                        case "PaperLetter":
                            return CrystalDecisions.Shared.PaperSize.PaperLetter;
                        case "PaperA5":
                            return CrystalDecisions.Shared.PaperSize.PaperA5;
                        case "PaperA3":
                            return CrystalDecisions.Shared.PaperSize.PaperA3;
                        case "PaperB5":
                            return CrystalDecisions.Shared.PaperSize.PaperB5;
                        case "PaperB4":
                            return CrystalDecisions.Shared.PaperSize.PaperB4;
                        // Add other cases as needed
                        default:
                            return CrystalDecisions.Shared.PaperSize.DefaultPaperSize; // Default case
                    }
                }
                try
                {
                    System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
                    settings.PrinterName = StoreRoom.Printer_Name();

                    // Set the output file path for the PDF
                    settings.PrintToFile = true;

                    if (StoreRoom.Printer_Name() == "Microsoft Print to PDF")
                    {


                        settings.PrintFileName = @"C:\Output\Invoice.pdf"; // Replace with your desired path
                    }




                    if (_isInvoie == "yes")
                    {
                        System.Drawing.Printing.PageSettings pagesettings = new System.Drawing.Printing.PageSettings();
                        GetReport("test").PrintOptions.PrinterName = settings.PrinterName;
                        GetReport("test").PrintOptions.PaperSize = MapPaperSize($"{StoreRoom.Printer_Size()}");

                        // Print the report

                        GetReport("test").PrintToPrinter(settings, pagesettings, false);

                    }
                    else
                    {
                        ReportDocument anotherReportDocument = (ReportDocument)crystalReportViewer1.ReportSource;

                        anotherReportDocument.PrintOptions.PrinterName = settings.PrinterName;
                        anotherReportDocument.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;

                        // Print the report directly from the CrystalReportViewer
                        crystalReportViewer1.PrintReport();
                    }

                    // Create and configure page settings






                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                return true; // Indicate that the key press has been handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
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

                    _isInvoie = "no";
                    GetReport("nion");
                }
               else if(pl == "wsave")
                {
                    _isInvoie = "yes";

                    WithoutSave();

                }


                else if(pl== "SaleReport")
                {
                    _isInvoie = "yes";
                    GetReport("test");
                }
                else if(pl== "Sale_GST")
                {
                    _isInvoie = "no";

                    GetSaleReport("test");


                }


                else if(pl == "Cust_Pay")
                {
                    _isInvoie = "no";
                    Cust_Payment();

                }

              else  if(pl== "Purches_Order")
                {

                    _isInvoie = "no";
                    GetPurches_Order();

                }


                else if(pl == "Purches_Return")
                {
                    _isInvoie = "no";
                    GetPurches_Return();
                }

                else if (pl == "AMIRSHAKH1234")
                {
                    _isInvoie = "no";
                    DayBook();
                }

                else if(pl == "Sale_Return")
                {
                    _isInvoie = "no";
                    GetSale_Return();

                }

                else if (pl == "Exp")
                {
                    _isInvoie = "no";
                    Exp();
                }


                if (pl == "Stock Report")
                {
                    _isInvoie = "no";
                    GetStock();
                }

                if (pl == "Customer Statment")
                {
                    _isInvoie = "no";
                    GetLeader("test");

                }


                else if (pl == "Invoice")
                {

                    GetReport("test");


                }
                else if (pl == "Sales Report")

                {
                    _isInvoie = "no";
                    GetReports("tst");
                }
                else if (pl == "Supplier Ledger")
                {
                    _isInvoie = "no";
                    GetLeader("sup");
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }





        public  void GlobleReport(string invs)
        {



            




            CrystalDecisions.Shared.PaperSize MapPaperSize(string paperSize)
            {
                switch (paperSize)
                {
                    case "PaperA4":
                        return CrystalDecisions.Shared.PaperSize.PaperA4;
                    case "PaperLetter":
                        return CrystalDecisions.Shared.PaperSize.PaperLetter;
                    case "PaperA5":
                        return CrystalDecisions.Shared.PaperSize.PaperA5;
                    case "PaperA3":
                        return CrystalDecisions.Shared.PaperSize.PaperA3;
                    case "PaperB5":
                        return CrystalDecisions.Shared.PaperSize.PaperB5;
                    case "PaperB4":
                        return CrystalDecisions.Shared.PaperSize.PaperB4;
                    // Add other cases as needed
                    default:
                        return CrystalDecisions.Shared.PaperSize.DefaultPaperSize; // Default case
                }
            }
           
                System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
                settings.PrinterName = StoreRoom.Printer_Name();

                // Set the output file path for the PDF
                settings.PrintToFile = true;

                if (StoreRoom.Printer_Name() == "Microsoft Print to PDF")
                {


                    settings.PrintFileName = @"C:\Output\Invoice.pdf"; // Replace with your desired path
                }




                    System.Drawing.Printing.PageSettings pagesettings = new System.Drawing.Printing.PageSettings();
                    GetReport("test", invs).PrintOptions.PrinterName = settings.PrinterName;
                    GetReport("test", invs).PrintOptions.PaperSize = MapPaperSize($"{StoreRoom.Printer_Size()}");

                    // Print the report

                    GetReport("test", invs).PrintToPrinter(settings, pagesettings, false);

             


            }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            CrystalDecisions.Shared.PaperSize MapPaperSize(string paperSize)
            {
                switch (paperSize)
                {
                    case "PaperA4":
                        return CrystalDecisions.Shared.PaperSize.PaperA4;
                    case "PaperLetter":
                        return CrystalDecisions.Shared.PaperSize.PaperLetter;
                    case "PaperA5":
                        return CrystalDecisions.Shared.PaperSize.PaperA5;
                    case "PaperA3":
                        return CrystalDecisions.Shared.PaperSize.PaperA3;
                    case "PaperB5":
                        return CrystalDecisions.Shared.PaperSize.PaperB5;
                    case "PaperB4":
                        return CrystalDecisions.Shared.PaperSize.PaperB4;
                    // Add other cases as needed
                    default:
                        return CrystalDecisions.Shared.PaperSize.DefaultPaperSize; // Default case
                }
            }
            try
            {
                System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
                settings.PrinterName = StoreRoom.Printer_Name();

                // Set the output file path for the PDF
                settings.PrintToFile = true;


                    if(StoreRoom.Printer_Name() == "Microsoft Print to PDF")
                {
                    settings.PrintFileName = @"C:\Output\" + inv + ".pdf"; // Replace with your desired path

                }




                if (_isInvoie == "yes")
                {
                    System.Drawing.Printing.PageSettings pagesettings = new System.Drawing.Printing.PageSettings();
                    GetReport("test").PrintOptions.PrinterName = settings.PrinterName;
                    GetReport("test").PrintOptions.PaperSize = MapPaperSize($"{StoreRoom.Printer_Size()}");

                    // Print the report

                    GetReport("test").PrintToPrinter(settings, pagesettings, false);

                }
                else
                {
                    ReportDocument anotherReportDocument = (ReportDocument)crystalReportViewer1.ReportSource;

                    anotherReportDocument.PrintOptions.PrinterName = settings.PrinterName;
                    anotherReportDocument.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.DefaultPaperSize;

                    // Print the report directly from the CrystalReportViewer
                    crystalReportViewer1.PrintReport();
                }

                // Create and configure page settings
                

                //redierct whatsapp 



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReportStd_KeyPress(object sender, KeyPressEventArgs e)
        {
             
        }

        private void ReportStd_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            CrystalDecisions.Shared.PaperSize MapPaperSize(string paperSize)
            {
                switch (paperSize)
                {
                    case "PaperA4":
                        return CrystalDecisions.Shared.PaperSize.PaperA4;
                    case "PaperLetter":
                        return CrystalDecisions.Shared.PaperSize.PaperLetter;
                    case "PaperA5":
                        return CrystalDecisions.Shared.PaperSize.PaperA5;
                    case "PaperA3":
                        return CrystalDecisions.Shared.PaperSize.PaperA3;
                    case "PaperB5":
                        return CrystalDecisions.Shared.PaperSize.PaperB5;
                    case "PaperB4":
                        return CrystalDecisions.Shared.PaperSize.PaperB4;
                    // Add other cases as needed
                    default:
                        return CrystalDecisions.Shared.PaperSize.DefaultPaperSize; // Default case
                }
            }
            try
            {
                System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
                settings.PrinterName = StoreRoom.Printer_Name();

                // Set the output file path for the PDF
                settings.PrintToFile = true;



                settings.PrintFileName = @"C:\Output\" + inv + ".pdf"; // Replace with your desired path




                if (_isInvoie == "yes")
                {
                    System.Drawing.Printing.PageSettings pagesettings = new System.Drawing.Printing.PageSettings();
                    GetReport("test").PrintOptions.PrinterName = settings.PrinterName;
                    GetReport("test").PrintOptions.PaperSize = MapPaperSize($"{StoreRoom.Printer_Size()}");

                    // Print the report

                    GetReport("test").PrintToPrinter(settings, pagesettings, false);

                }
                else
                {
                    ReportDocument anotherReportDocument = (ReportDocument)crystalReportViewer1.ReportSource;

                    anotherReportDocument.PrintOptions.PrinterName = settings.PrinterName;
                    anotherReportDocument.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;

                    // Print the report directly from the CrystalReportViewer
                    crystalReportViewer1.PrintReport();
                }

                // Create and configure page settings

                string whatsappUrl = $"https://wa.me/{num.Text}?text={HttpUtility.UrlEncode(message.Text)}";

                OpenUrlInBrowser(whatsappUrl);
                MessageBox.Show("Succefully Send");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
