using Back_Dr.Models;
using Back_Dr.Sale;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport
{
    public partial class ReportBarcode : Form
    {

        private BarcodeSingle _model;
        private List<int> _list = new List<int>();
        private string qry;

        public ReportBarcode(BarcodeSingle barcodemodel , List<int> list = null)
        {
            InitializeComponent();
            _model = barcodemodel;
            _list = list;
        }

        private void ReportBarcode_Load(object sender, EventArgs e)
        {
            string path;
            try
            {


                ReportDocument report = new ReportDocument();
               

                    path = Application.StartupPath + "/Barcodelable.rpt";

                if (_model.CODE == 2)
                {

                    path = Application.StartupPath + "/Barcodelabels.rpt";
                    report.Load(path);




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

                    DataTable table1 = new DataTable();

                        DataSet dst = new DataSet();

                    string idList = string.Join(",", _list);

                  
                    if (_list.Count >= 1)
                    {
                        qry = $"select ITEM_NAME, SALE_PRICE, BARCODE from Product_Item where ID IN({ idList})";

                    }
                    else
                    {
                        qry = $"select ITEM_NAME, SALE_PRICE, BARCODE from Product_Item";

                    }
                    table1 = StoreRoom.GetTable(qry);


                    ParameterField shopname = new ParameterField();
                    ParameterFields fields = new ParameterFields();



                    ParameterDiscreteValue shopvalue = new ParameterDiscreteValue();

                    shopname.ParameterFieldName = "shopname";
                    shopvalue.Value = _model.ShopName;
                    shopname.CurrentValues.Add(shopvalue);

                    fields.Add(shopname);
                    // Set the DataTable as the report's data source
                    report.SetDataSource(table1);



                    //report.SetParameterValue("place",Address);



                    // Verify the report's database
                    report.VerifyDatabase();
                    crystalReportViewer1.ParameterFieldInfo = fields;



                    crystalReportViewer1.ReportSource = report;

                    
                }
                else
                {



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
                    ParameterField productname = new ParameterField();
                    ParameterField barcode = new ParameterField();
                    ParameterField price = new ParameterField();

                    ParameterField shopname = new ParameterField();


                    ParameterFields pfield = new ParameterFields();




                    ParameterDiscreteValue productnamevalue = new ParameterDiscreteValue();
                    ParameterDiscreteValue barcodevalue = new ParameterDiscreteValue();

                    ParameterDiscreteValue pricevalue = new ParameterDiscreteValue();
                    ParameterDiscreteValue shopnamevalue = new ParameterDiscreteValue();


                    price.ParameterFieldName = "price";
                    pricevalue.Value = $"₹{_model.Price}";

                    price.CurrentValues.Add(pricevalue);



                    //product name
                    productname.ParameterFieldName = "productname";
                    productnamevalue.Value = $"{_model.ItemName}";
                    productname.CurrentValues.Add(productnamevalue);

                    //barocode
                    barcode.ParameterFieldName = "barcode";
                    barcodevalue.Value = $"{_model.Barcode}";
                    barcode.CurrentValues.Add(barcodevalue);

                    //shopname
                    shopname.ParameterFieldName = "shopname";
                    shopnamevalue.Value = $"{_model.ShopName}";
                    shopname.CurrentValues.Add(shopnamevalue);


                    pfield.Add(productname);
                    pfield.Add(barcode);
                    pfield.Add(shopname);
                    pfield.Add(price);



                    //report.SetParameterValue("place",Address);


                    // Verify the report's database
                   report.VerifyDatabase();

                    crystalReportViewer1.ParameterFieldInfo = pfield;
                    
                    crystalReportViewer1.ReportSource = report;

                }

            }
            catch (Exception ex)
            {
                 
            }
        }
    }
}

