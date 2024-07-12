using Back_Dr.Sale;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using CrystalDecisions.Shared;
using Dr.Sale.Components;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ConnectionInfo = CrystalDecisions.Shared.ConnectionInfo;
using ParameterField = CrystalDecisions.Shared.ParameterField;
using Table = CrystalDecisions.CrystalReports.Engine.Table;
using Tables = CrystalDecisions.CrystalReports.Engine.Tables;


namespace CrystalReport.Components
{
    public partial class GetReport : UserControl
    {
        public static string code = "";
        private DataTable table1;

        public GetReport()
        {
            InitializeComponent();

            CostomerList.InvoiceTrs += Invoic;
            PartiNameList.IDSupp += PartiNameList_IDSupp;
            CustomerList.InvoiceTrs += CustomerList_InvoiceTrs;

        }

        private void CustomerList_InvoiceTrs(object sender, string e)
        {
            textBox1.Text = StoreRoom.GetData;
        }

        private void PartiNameList_IDSupp(object sender, string e)
        {
            textBox1.Text = StoreRoom.GetData;

        }

        private void Invoic(object sender, string e)
        {
            textBox1.Text = StoreRoom.GetData;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                View c = new View(comboBox1.Text,"GetReport");
                c.Show();

            }
            catch (Exception ex)
            {
                 
            }
        }
         
        public void GetLeader(string type)
        {

            string path = "";
            ReportDocument report = new ReportDocument();

            if(type=="sup")
            {
                path = Application.StartupPath + "/Ledger.rpt";

            }
            else
            {
                path = Application.StartupPath + "/CustomerLedger.rpt";

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


            
            if (type =="sup")
            {


                string invoiceId = textBox1.Text;
                string startDate = $"{dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")} 00:00:00";
                string endDate = $"{dateTimePicker2.Value.Date.ToString("yyyy-MM-dd")} 23:59:59";

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
                dateRange.CurrentValues.Add(new ParameterDiscreteValue { Value = $"FROM {dateTimePicker1.Value.ToString("yyyy-MM-dd")} TO {dateTimePicker2.Value.ToString("yyyy-MM-dd")}" });

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
                string invoiceId = textBox1.Text;
                DateTime startDate = dateTimePicker1.Value.Date;
                DateTime endDate = dateTimePicker2.Value.Date;

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

                 table1 = GetTable("SELECT * FROM SInvoice INNER JOIN Sale_Items ON SInvoice.InvoiceID = Sale_Items.Invoice WHERE SInvoice.InvoiceID = '" + textBox1.Text + "'; ");
                string cust_s = MainEngine_.GetDataScript<string>("select cust_name from SInvoice where items='" + textBox1.Text + "'").FirstOrDefault().ToString();
                decimal current = MainEngine_.GetDataScript<decimal>("select TotalBill from SInvoice where items = '" + textBox1.Text + "'").FirstOrDefault();

                decimal paid = MainEngine_.GetDataScript<decimal>("select Paid from CustomerTransactions where InvoiceId = '" + textBox1.Text + "'").FirstOrDefault();
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
            DataTable table1 = GetTable($"exec GetSaleReport '{dateTimePicker1.Text} 00:00:00', '{dateTimePicker2.Text} 23:59:59'");
 
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
            lowval.Value = MainEngine_.GetDataScript<string>("Select description from LowSale").FirstOrDefault()??"NULL";
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
            ddateval.Value = $"{dateTimePicker1.Text} - {dateTimePicker2.Text}";
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

        private void GetReport_Load(object sender, EventArgs e)
        {
 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            
            {

                if(comboBox1.Text == "Stock Report")
                {
                    GetStock();
                }

                if(comboBox1.Text == "Customer Statment")
                {
                    GetLeader("test");

                }


                if (comboBox1.Text == "Invoice")
                {
                    GetRecipt("test");
               

                }
                else if(comboBox1.Text == "Sales Report")

                {
                    GetReports("tst");
                }
                else if(comboBox1.Text == "Supplier Ledger")
                {
                    GetLeader("sup");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            crystalReportViewer1.PrintReport();
        }
    }
}
