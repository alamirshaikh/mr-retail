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
                

                // Apply SQL command to the report using a parameterized query
                string invoiceId = textBox1.Text; // Retrieve the invoice ID from the textbox
                DataTable table1 = GetTable($"SELECT * FROM Ledger_Supplier where SupplierName =  '{textBox1.Text}'  AND Date >= '{dateTimePicker1.Text} 00:00:00' AND  date <= '{dateTimePicker2.Text} 23:59:59' ");

                report.SetDataSource(table1);
                ParameterFields pfield = new ParameterFields();
                ParameterField balance = new ParameterField();
                ParameterDiscreteValue balanceval = new ParameterDiscreteValue();
                balance.ParameterFieldName = "balance";
                balanceval.Value = $"{MainEngine_.GetDataScript<decimal>($"SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) - SUM(CAST(Debit AS DECIMAL(18, 2))) AS CreditMinusDebit FROM Ledger_Supplier where SupplierName  ='{textBox1.Text}' and  Date >= '{dateTimePicker1.Text} 00:00:00' AND  date <= '{dateTimePicker2.Text} 23:59:59' ; ").FirstOrDefault().ToString()}";
                balance.CurrentValues.Add(balanceval);

                ParameterField tcredit = new ParameterField();
                ParameterDiscreteValue tcreditval = new ParameterDiscreteValue();
                tcredit.ParameterFieldName = "tcredit";
                tcreditval.Value = $"{MainEngine_.GetDataScript<decimal>($"SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) AS CreditMinusDebit FROM Ledger_Supplier where SupplierName  ='{textBox1.Text}' and Date >= '{dateTimePicker1.Text} 00:00:00' AND  date <= '{dateTimePicker2.Text} 23:59:59' ; ").FirstOrDefault().ToString()}";
                tcredit.CurrentValues.Add(tcreditval);


                ParameterField tdebit = new ParameterField();
                ParameterDiscreteValue tdebitval = new ParameterDiscreteValue();
                tdebit.ParameterFieldName = "tdebit";
                tdebitval.Value = $"{MainEngine_.GetDataScript<decimal>($"SELECT SUM(CAST(Debit AS DECIMAL(18, 2))) AS CreditMinusDebit FROM Ledger_Supplier  where SupplierName  ='{textBox1.Text}' and  Date >= '{dateTimePicker1.Text} 00:00:00' AND  date <= '{dateTimePicker2.Text} 23:59:59' ; ").FirstOrDefault().ToString()}";
                tdebit.CurrentValues.Add(tdebitval);



                ParameterField date = new ParameterField();
                ParameterDiscreteValue dateval = new ParameterDiscreteValue();
                date.ParameterFieldName = "ddate";
                dateval.Value = $"(FROM {dateTimePicker1.Text}  TO - {dateTimePicker2.Text})";
                date.CurrentValues.Add(dateval);

                pfield.Add(date);
                pfield.Add(balance);
                pfield.Add(tcredit);
                pfield.Add(tdebit);

                report.VerifyDatabase();

                // Set the report source for the CrystalReportViewer
                crystalReportViewer1.ParameterFieldInfo = pfield;
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.Zoom(2);


            }
            else
            {
                ParameterField date = new ParameterField();
                ParameterDiscreteValue dateval = new ParameterDiscreteValue();


                // Apply SQL command to the report using a parameterized query
                string invoiceId = textBox1.Text; // Retrieve the invoice ID from the textbox
                DataTable table1 = GetTable($"SELECT * FROM Ledger_Customer where Cust_name =  '{textBox1.Text}'  AND Date >= '{dateTimePicker1.Text} 00:00:00' AND  date <= '{dateTimePicker2.Text} 23:59:59' ");

                report.SetDataSource(table1);
                ParameterFields pfield = new ParameterFields();
                ParameterField balance = new ParameterField();
                ParameterDiscreteValue balanceval = new ParameterDiscreteValue();
                balance.ParameterFieldName = "balance";
                balanceval.Value = $"{MainEngine_.GetDataScript<decimal>($"SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) - SUM(CAST(Debit AS DECIMAL(18, 2))) AS CreditMinusDebit FROM Ledger_Customer where Cust_name  ='{textBox1.Text}' and  Date >= '{dateTimePicker1.Text} 00:00:00' AND  date <= '{dateTimePicker2.Text} 23:59:59' ; ").FirstOrDefault().ToString()}";
                balance.CurrentValues.Add(balanceval);



                ParameterField tcredit = new ParameterField();
                ParameterDiscreteValue tcreditval = new ParameterDiscreteValue();
                tcredit.ParameterFieldName = "tcredit";
                tcreditval.Value = $"{MainEngine_.GetDataScript<decimal>($"SELECT SUM(CAST(Credit AS DECIMAL(18, 2))) AS CreditMinusDebit FROM Ledger_Customer where Cust_name  ='{textBox1.Text}' and Date >= '{dateTimePicker1.Text} 00:00:00' AND  date <= '{dateTimePicker2.Text} 23:59:59' ; ").FirstOrDefault().ToString()}";
                tcredit.CurrentValues.Add(tcreditval);


                ParameterField tdebit = new ParameterField();
                ParameterDiscreteValue tdebitval = new ParameterDiscreteValue();
                tdebit.ParameterFieldName = "tdebit";
                tdebitval.Value = $"{MainEngine_.GetDataScript<decimal>($"SELECT SUM(CAST(Debit AS DECIMAL(18, 2))) AS CreditMinusDebit FROM Ledger_Customer  where Cust_name  ='{textBox1.Text}' and  Date >= '{dateTimePicker1.Text} 00:00:00' AND  date <= '{dateTimePicker2.Text} 23:59:59' ; ").FirstOrDefault().ToString()}";
                tdebit.CurrentValues.Add(tdebitval);



                date.ParameterFieldName = "ddate";
                dateval.Value = $"(FROM {dateTimePicker1.Text}  TO - {dateTimePicker2.Text})";
                date.CurrentValues.Add(dateval);

                pfield.Add(date);
                pfield.Add(balance);
                pfield.Add(tcredit);
                pfield.Add(tdebit);


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

                tdebitval.Value = $"₹{(bal + current) - paid}";

            }
            else
            {
                tdebitval.Value = $"₹{(current) - paid}";

            }


            tdebit.CurrentValues.Add(tdebitval);

                ParameterDiscreteValue oldval = new ParameterDiscreteValue();
                old.ParameterFieldName = "prc";
                oldval.Value = $"₹{bal}";
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
