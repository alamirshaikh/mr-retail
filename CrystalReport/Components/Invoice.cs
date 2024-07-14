﻿
using Back_Dr.Models;
using Back_Dr.Sale;
using Back_Dr.Sale.Inventory;
using CrystalDecisions.ReportSource;
using CrystalReport;
using Dr.Sale.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Timer = System.Threading.Timer;
using Unit = Dr.Sale.Components.Unit;

namespace CrystalReport.Components
{
    public partial class Invoice : UserControl
    {

        public bool isState = false; 
        private string mystate = "";

        private decimal Taxes = 0;
        private string temp_cust = "CASH";
        private string temp_invoice;
        private decimal senddiscount = 0;
        private bool enterKeyPressed = false;

        public Calculation cal = new Calculation();
        private int CustCount = 0;
        public Invoice(string inv = "")
        {
            _inv = inv;

            mystate = MainEngine_.GetDataScript<string>("select State from OwnerInformation").FirstOrDefault();
         
            InitializeComponent();
            state.Text = mystate;
        }

        public string inv = "";
        public decimal r, amst;
       public decimal v = 0;
        private int srs;

        private void label1_Click(object sender, EventArgs e)
        {
               
        
        }

        private DataGridView dgview;
        private DataGridViewTextBoxColumn dgviewcol1;
        private DataGridViewTextBoxColumn dgviewcol2;
        private DataGridViewTextBoxColumn dgviewcol3;


        void Search()
        {
            dgview = new DataGridView();
            dgviewcol1 = new DataGridViewTextBoxColumn();
            dgviewcol2 = new DataGridViewTextBoxColumn();
            dgviewcol3 = new DataGridViewTextBoxColumn();

            this.dgview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
   

            this.dgview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgviewcol1, this.dgviewcol2,this.dgviewcol3 });
            this.dgview.Name = "dgview";
            dgview.Visible = false;
            this.dgviewcol1.Visible = false;
            this.dgviewcol2.Visible = false;
            this.dgviewcol3.Visible = false;

            this.dgview.AllowUserToAddRows = false;
            this.dgview.RowHeadersVisible = false;
            this.dgview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dgview.CellClick += new DataGridViewCellEventHandler(dgview_Click);
            this.Controls.Add(dgview);
            this.dgview.ReadOnly = true;
            dgview.BringToFront();
        }

        private void dgview_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (checkBox1.Checked)
                {
                    DataGridViewRow selectedRow = dgview.Rows[e.RowIndex];

                    // Access the row's data, for example:
                    string cellValue1 = selectedRow.Cells[0].Value.ToString(); // Assuming column 1 contains strings
                    string cellValue2 = selectedRow.Cells[2].Value.ToString(); // Assuming column 3 contains strings
                    

                    // Do something with the values or the selected row
                    // Example: Display the values in a message box
                    if (comboBox1.SelectedIndex == 0 || comboBox1.Text == "GST")
                    {
                        gsttext.Text = selectedRow.Cells[3].Value.ToString();

                    }
                    else
                    {
                        gsttext.Text = "0";
                    }

                    desc.Text = cellValue1;
                    amst = Convert.ToDecimal(cellValue2);
                    r = Convert.ToDecimal(cellValue2);
                    rete.Text = Convert.ToString(r);
                    amt.Text = Convert.ToString(amst);
                
                    v = amst;
                    q.Text = "1";
                    disc.Text = "0";
                    desc.Focus();
                    dgview.Visible = false;
                }
                else
                {
                    // Retrieve the clicked row
                    DataGridViewRow selectedRow = dgview.Rows[e.RowIndex];

                    // Access the row's data, for example:
                    string cellValue1 = selectedRow.Cells[0].Value.ToString(); // Assuming column 1 contains strings
                    string cellValue2 = selectedRow.Cells[2].Value.ToString(); // Assuming column 3 contains strings
                    string gst = selectedRow.Cells[3].Value.ToString(); // Assuming column 3 contains strings


                    // Do something with the values or the selected row
                    // Example: Display the values in a message box


                    desc.Text = cellValue1;
                    amst = Convert.ToDecimal(cellValue2);
                    r = Convert.ToDecimal(cellValue2);
                    rete.Text = Convert.ToString(r);
                    amt.Text = Convert.ToString(amst);
                    v = amst;
                    q.Text = "1";
                    gsttext.Text = gst;
                    disc.Text = "0";
                    desc.Focus();
                    dgview.Visible = false;
                }
            }
        }


        //Two Column
        void Search(int LX, int LY, int DW, int DH, string ColName, string ColSize)
        {
            this.dgview.Location = new System.Drawing.Point(LX, LY);
            this.dgview.Size = new System.Drawing.Size(DW, DH);

            string[] ClSize = ColSize.Split(',');
            string[] ClName = ColName.Split(',');

            // Ensure the DataGridView has the right number of columns
            if (dgview.Columns.Count < ClName.Length)
            {
                while (dgview.Columns.Count < ClName.Length)
                {
                    dgview.Columns.Add(new DataGridViewTextBoxColumn());
                }
            }
            else if (dgview.Columns.Count > ClName.Length)
            {
                while (dgview.Columns.Count > ClName.Length)
                {
                    dgview.Columns.RemoveAt(dgview.Columns.Count - 1);
                }
            }

            // Set column sizes
            for (int i = 0; i < ClSize.Length; i++)
            {
                if (int.Parse(ClSize[i]) != 0)
                {
                    dgview.Columns[i].Width = int.Parse(ClSize[i]);
                }
                else
                {
                    dgview.Columns[i].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            // Set column names
            for (int i = 0; i < ClName.Length; i++)
            {
                this.dgview.Columns[i].HeaderText = ClName[i];
                this.dgview.Columns[i].Visible = true;
            }
        }



        private string GenerateInvoice()
        {
            string inv = "";
            string newinv = "";
            try
            {
                var sql = "select * from SInvoice";
                var sql1 = $"select * from Customer";
                int custsr = MainEngine_.GetDataScript<int>(sql).Count();
                int srn = MainEngine_.GetDataScript<int>(sql).Count();
                srn = srn + 1;
                custsr = custsr + 1;

                inv =  $"INV{DateTime.Now.ToString("yy")}{DateTime.Now.ToString("MM")}{DateTime.Now.ToString("dd")}{custsr}{srn+10}";


              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return inv;


        }


        private async void button2_Click(object sender, EventArgs e)
        {
            srs = 0;
            
            try
            {
                
                inv =  GenerateInvoice();

                if (dataGridView1.Rows.Count > 0)
                {
                    StoreRoom tr = new StoreRoom();
                   await tr.NewData(cust_name.Text, textBox1.Text, mobile_num.Text,city.Text);


                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {

                        var dynamic = new
                        {

                            sr_no = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                            description = dataGridView1.Rows[i].Cells[1].Value.ToString(),
                            qty = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()),
                            rate = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString()),
                            discount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()),
                            amount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString()),

                            Invoice = inv
                        };

                         
                        await MainEngine_.Add(dynamic, "sp_addsaleitem");

                    }
                }
              







                var smodel = new
                {

                    invoiceID = inv,
                    cust_Name = cust_name.Text, 
                    items = inv,
                    sub_total = Convert.ToDecimal(totalamt.Text)-Convert.ToDecimal(discount_.Text),
                    perdis = Convert.ToInt32(discount_.Text),       
                    discount = Convert.ToDecimal(discount_.Text),
                    other = Convert.ToDecimal(tax.Text),
                    TotalBill = Convert.ToDecimal(totalamt.Text),
                    invdate = DateTime.Now,
                    place = textBox1.Text
                   

                };
               
             await MainEngine_.Add(smodel, "spSaleInsert");
                 MessageBox.Show("Bill has been saved!", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Rows.Clear();
                StoreRoom.ClearData(this.Controls);
                invnum.Text = GenerateInvoice();
                amt.Text = "";
                //sub_t.Text = "0";
                //perdisc.Text = "0";
                //discount_.Text = "0";
                //other.Text = "0";

            
                totalamt.Text = "0";
                cal.discounts = 0;
                cal.sub_amount = 0;
                cal.total = 0;
                CustCount = CustCount + 1;
                textBox2.Text = CustCount.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private   void cust_name_TextChanged(object sender, EventArgs e)
        {
            try
            {

                label31.Text = "Rs.0";
                var test = new { cust_name = cust_name.Text };

                List<dynamic> list = MainEngine_.GetData<dynamic>("sp_getCustomer", test).ToList();
              
                foreach (var item in list)
                {
                    cust_name.Items.Add(item.cust_name);

                }

                prs.Text = "0";
                blc.Text = "0";



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        
            {



            if (keyData == Keys.F1)
            {
                temp_cust = textBox1.Text;
                temp_invoice = invnum.Text;
                SaveAndPrint("F1");
            }



            if (keyData == Keys.F2)
            {
                temp_cust = textBox1.Text;
                temp_invoice = invnum.Text;
                SaveAndPrint("F2");
            }


            if(keyData == Keys.F5)
            {
                button1.PerformClick();

            }



            if (keyData == Keys.Enter)
            {
                // Handle the Enter key
                if (amt.Text != "")
                {
                    if (disc.Focused == true)
                    {

                        button3.PerformClick();
                        return true;
                    }
                }

                if(dgview.Rows.Count > 0 && dgview.Visible == true)
                {

                    if (checkBox1.Checked)
                    {


                        desc.Text = dgview.Rows[0].Cells[0].Value.ToString();
                        amst = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);
                        if (comboBox1.SelectedIndex == 0 || comboBox1.Text == "GST")
                        {
                            gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[3].Value.ToString());

                        }
                        else
                        {
                            gsttext.Text = "0";
                        }
                        v = amst;

                        q.Text = "1";
                        disc.Text = "0";
                    
                    }
                    else
                    {

                        int rowindex = dgview.SelectedRows[0].Index;

                        desc.Text = dgview.Rows[rowindex].Cells[0].Value.ToString();
                        amst = Convert.ToDecimal(dgview.Rows[rowindex].Cells[2].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[rowindex].Cells[2].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);
                        if (comboBox1.SelectedIndex == 0 || comboBox1.Text == "GST")
                        {
                            gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[3].Value.ToString());

                        }
                        else
                        {
                            gsttext.Text = "0";
                        }
                        v = amst;

                        q.Text = "1";
                        disc.Text = "0";

                    }
                }


                    if (dgview.Visible == true)
                {
                    dgview.Visible = false;
                }

                if(button1.Focused == true)
                {
                    isenter = true;
                }
                
                    SendKeys.Send("{TAB}"); // Simulate Tab key press
                 
                    return true; // Mark the key as handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private  void Invoice_Load(object sender, EventArgs e)
        {
         


            srs = 0;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            List<string> units = Task.Run(() => MainEngine_.GetDataScript<string>("select Unit from Unit")).Result.ToList();


            // Update the ComboBox on the UI thread
            comboBox5.Items.Clear();
            comboBox5.Items.AddRange(units.Distinct().ToArray());
            comboBox5.SelectedIndex = 0;


            try
            {
                Search();
               string ctd = MainEngine_.GetDataScript<string>("select Count(ID) from Customer").FirstOrDefault().ToString();
              if(ctd==null)
                {
                    textBox2.Text = "0";
                    CustCount = 0;
                }
                else
                {
                    textBox2.Text = ctd;
                    CustCount = Convert.ToInt32(ctd);
                }
            }
            catch (Exception ex)
            {
                 
            }

    

          

           invnum.Text =  GenerateInvoice();

            if(_inv !="")
            {
                invnum.Text =_inv;
 
            }
            else
            {
                invnum.Text = GenerateInvoice();
            }
 

        }

        private void cust_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string phone = MainEngine_.GetDataScript<string>("select cust_phone from Customer Where cust_name = '" + cust_name.Text + "'").FirstOrDefault();
                mobile_num.Text = phone;
                string sec = MainEngine_.GetDataScript<string>("select place from Customer Where cust_name = '" + cust_name.Text + "'").FirstOrDefault();
                string ciyu = MainEngine_.GetDataScript<string>("select pcity from Customer Where cust_name = '" + cust_name.Text + "'").FirstOrDefault();

                string states = MainEngine_.GetDataScript<string>("select pstate from Customer Where cust_name = '" + cust_name.Text + "'").FirstOrDefault();

                state.Text = states;

                city.Text = ciyu.ToString();

                if(StoreRoom.GetState() == state.Text)
                {
                    isState = true;
                }
                else
                {
                    isState = false;

                }

                if (invnum.Text == MainEngine_.GetDataScript<string>("select InvoiceID from SInvoice where InvoiceId = '" + invnum.Text + "'").FirstOrDefault()) 
                {


                    decimal pss = StoreRoom.CustomerBalace(cust_name.Text);


                    decimal totalBill = Convert.ToDecimal(StoreRoom.TotalBill(invnum.Text));
                    decimal totalPaid = Convert.ToDecimal(StoreRoom.TotalPaid(invnum.Text)); // Ensure TotalPaid returns a nullable decimal or handle null appropriately

                    decimal total = totalPaid - totalPaid;
                    prs.Text = (total+pss).ToString();

                    string cals = cal.SubTotal(totalBill.ToString()).ToString();

                 
                    blc.Text = StoreRoom.TotalBill(invnum.Text);


                }
                else
                {
                    prc = StoreRoom.CustomerBalace(cust_name.Text);
                    prs.Text = (prc.ToString() ?? "0").ToString();


                }




                label31.Text = (prc.ToString() ?? "0").ToString();



                if (sec =="" || sec==null)
                {
                    string add = MainEngine_.GetDataScript<string>("select pcity from Customer Where cust_name = '" + cust_name.Text + "'").FirstOrDefault();
                   
                    textBox1.Text = add;
                   
                }
                else
                {
                    textBox1.Text = sec;

                }

                cust_name.Items.Clear();
            }
            catch (Exception dx)
            {
                MessageBox.Show(dx.Message);
            }
          
        }

        private bool isDownKeyPressed = false;

        private CancellationTokenSource _cancellationTokenSource;
        private void desc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (desc.Text.Length <= 0)
                {
                    dgview.Visible = false;
                    return;
                }

                if (isDownKeyPressed)
                {
                    isDownKeyPressed = false;
                    return;
                }

                if (checkBox1.Checked)
                {
                    HandleMechanicSearch();
                }
                else
                {
                    HandleProductSearch();
                }
            }
            catch (Exception ex)
            {
                // Log the exception and display a user-friendly message
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void HandleMechanicSearch()
        {
            var searchTerm = desc.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return;
            }

            dgview.Visible = true;
            dgview.BringToFront();
            Search(9, 250, 430, 200, "Item Name,Stock,Price,GST", "100,100,100,100");


            using (SqlConnection con = new SqlConnection(MainEngine_.SERVER_PATH))
            {
                con.Open();
                string query = "SELECT TOP(40) Item_Name, Item_price, GST FROM Mechanic WHERE Mechanic_Name = @CustName AND Item_Name LIKE @SearchTerm + '%'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CustName", cust_name.Text);
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    FillDataGridView(cmd);
                }
            }
        }

        private void HandleProductSearch()
        {
            var searchTerm = desc.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return;
            }

            dgview.Visible = true;
            dgview.BringToFront();
            Search(9, 250, 430, 200, "Item Name,Stock,Price,GST", "100,100,100,100");


            using (SqlConnection con = new SqlConnection(MainEngine_.SERVER_PATH))
            {
                con.Open();
                string query = "SELECT TOP(40) ITEM_NAME, STOCK, SALE_PRICE, GST FROM Product_Item WHERE ITEM_NAME LIKE @SearchTerm + '%'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    FillDataGridView(cmd);
                }
            }
        }

        private void FillDataGridView(SqlCommand cmd)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgview.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int n = dgview.Rows.Add();
                    dgview.Rows[n].Cells[0].Value = row[0].ToString(); //item name 0
                    dgview.Rows[n].Cells[1].Value = row[1].ToString(); //stock 1 
                    dgview.Rows[n].Cells[2].Value = row[2].ToString();  // price 2 
                    dgview.Rows[n].Cells[3].Value = row[3].ToString(); // GST column 3
                   
                }
            }
        }


        private void proCode_MouseDoubleClick(object sender, MouseEventArgs e) 
        {
            throw new NotImplementedException();
        }

        private HashSet<string> addedItems = new HashSet<string>();
        private bool isenter = false;
        private decimal prc;
        private decimal cals;
        private decimal same;
        private string _inv;
        private decimal okdiscount;
        private bool issave;

        public decimal CGSTm = 0m;
        public decimal SGSTm  = 0m;
        public decimal IGSTm  = 0m;

        private void q_TextChanged(object sender, EventArgs e)
        {
            try
            {



                if (q.Text == "")
                {
                    q.Text = "1";
                }




                  v = decimal.Parse(q.Text);
                v = v * r;
                amt.Text = v.ToString();
                decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);
                decimal originalAmount = (decimal.Parse(rete.Text) * decimal.Parse(q.Text));
                originalAmount = originalAmount + (originalAmount * decimal.Parse(gsttext.Text) / 100);

                decimal discountedAmount = originalAmount - (discountPercentage / 100m * originalAmount);

                okdiscount = (discountPercentage / 100m * originalAmount);

                amt.Text = discountedAmount.ToString();





            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void disc_TextChanged(object sender, EventArgs e)
        { 
            try
            {
                if (checkBox2.Checked)
                {


                    amt.Text = v.ToString();
                    decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);
                    decimal originalAmount = (decimal.Parse(rete.Text) * decimal.Parse(q.Text));
                    originalAmount = originalAmount + (originalAmount * decimal.Parse(gsttext.Text) / 100);

                    decimal discountedAmount = originalAmount - (discountPercentage / 100m * originalAmount);

                    okdiscount = (discountPercentage / 100m * originalAmount);

                    amt.Text = discountedAmount.ToString();
                }
                else
                {

                    amt.Text = v.ToString();
                    decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);
                    decimal originalAmount = (decimal.Parse(rete.Text) * decimal.Parse(q.Text));
                    originalAmount = originalAmount + (originalAmount * decimal.Parse(gsttext.Text) / 100);
                    decimal discountedAmount = originalAmount - discountPercentage;
                    okdiscount = discountPercentage;

                    amt.Text = discountedAmount.ToString();
                }
                
                }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or display an error message
            }

        }

        private void disc_Leave(object sender, EventArgs e)
        {
        }

        private async void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void perdisc_TextChanged(object sender, EventArgs e)
        {
            try
            {

                //if(perdisc.Text == "")
                //{
                //    perdisc.Text = "0";
                //}

                //cal.afterdiscount(decimal.Parse(perdisc.Text),comboBox3.SelectedItem.ToString());
                //discount_.Text = "-" + cal.discounts.ToString();

                //totalamt.Text = cal.total.ToString();
                

            }
            catch (Exception ex)
            {
                                                        
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //cal.afterdiscount(decimal.Parse(perdisc.Text), comboBox3.SelectedItem.ToString());
                //discount_.Text = "-" + cal.discounts.ToString();

                //totalamt.Text = cal.total.ToString();

            }
            catch (Exception ex)
            {

            }
        }


        private void Save()
        {

            srs = 0;
            try
            {

                if (dataGridView1.Rows.Count > 0)
                {

                    var result = MessageBox.Show("Do you want to Save your Bill?", "Save Bill?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (MainEngine_.GetDataScript<string>("select items from SInvoice where items = '" + _inv + "'").FirstOrDefault() == _inv)
                        {
                            SaveData();
                        }
                        else
                        {
                            SaveData();
                            SaveSaleItems();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("No item. Please add an item!", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                issave = true;

               
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }



        }









        private  void SaveAndPrint(string f="")
        {
            srs = 0;
            try
            {

                if (dataGridView1.Rows.Count > 0)
                {

                    var result = MessageBox.Show("Do you want to Save and Print Bill?", "Print?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (MainEngine_.GetDataScript<string>("select items from SInvoice where items = '" + _inv + "'").FirstOrDefault() == _inv)
                    {
                        SaveData(f);
                    }
                    else
                    {
                        SaveData(f);
                        SaveSaleItems();
                    }

                        /*ReportStd rp = new ReportStd(temp_invoice, temp_cust);
                       rp.Print(temp_invoice);

                          */


                        PrintInvoice();
                    }
                }
                else
                {
                    MessageBox.Show("No item. Please add an item!", "No Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

               
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            Taxes = 0; // Reset taxes
            tax.Text = Taxes.ToString();
        }

        private void RefreshInvoice()
        {
            Invoice ins = this;
            ins.Refresh();
            srs = 0;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void PrintInvoice()
        {
            ReportStd rp = new ReportStd(temp_invoice, temp_cust);
            rp.Show();
        }

        private async void SaveData(string f="")
        {
            try
            {
                StoreRoom tr = new StoreRoom();
              await  tr.NewData(cust_name.Text, textBox1.Text, mobile_num.Text, city.Text);
            }
            catch (Exception ex12)
            {
                // Handle exception if necessary
            }

            string cashred;
            decimal to = Convert.ToDecimal(totalamt.Text);

            if (comboBox1.Text == "Cash")
            {
                cashred = "Cash";
            }
            else
            {
                cashred = "Credit";
            }

            string callid = MainEngine_.GetDataScript<string>("select id from Customer Where cust_name = '" + cust_name.Text + "'").FirstOrDefault();


            if (paid.Text == "0" && onlinep.Text == "0" && f == "F1")
            {
                await ProcessTransaction(callid, "Cash", decimal.Parse(totalamt.Text));
            }

            // Check if paid amount is zero, online payment is zero, and f is "F2"
            if (paid.Text == "0" && onlinep.Text == "0" && f == "F2")
            {
                await ProcessTransaction(callid, "Credit", 0);
            }

            // Check if online payment is greater than zero
            if (decimal.Parse(onlinep.Text) > 0)
            {
                await ProcessTransaction(callid, comboBox3.Text, decimal.Parse(onlinep.Text));
            }

            // Check if paid amount is greater than zero
            if (decimal.Parse(paid.Text) > 0)
            {
                await ProcessTransaction(callid, comboBox2.Text, decimal.Parse(paid.Text));
            }



            decimal td = 0;
            inv = GenerateInvoice();
            decimal sub = Convert.ToDecimal(totalamt.Text) - Convert.ToDecimal(tax.Text);

            var smodel = new
            {
                invoiceID = invnum.Text,
                cust_Name = cust_name.Text,
                items = invnum.Text,
                sub_total = sub,
                perdis = td,
                discount = Convert.ToDecimal(senddiscount),
                other = Convert.ToDecimal(tax.Text),
                TotalBill = Convert.ToDecimal(totalamt.Text),
                invdate = DateTime.Now,
                place = textBox1.Text,
                sCGST = Convert.ToDecimal(cgsto.Text),
                sSGST = Convert.ToDecimal(sgsti.Text),
                sIGST = Convert.ToDecimal(igsto.Text),
                TAX = comboBox1.Text

            };

            await MainEngine_.Add(smodel, "spSaleInsert");

            ClearForm();
            cust_name.Focus();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private async void SaveSaleItems()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                var dynamic = new
                {
                    sr_no = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                    description = dataGridView1.Rows[i].Cells[1].Value.ToString(),
                    qty = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()),
                    rate = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString()),
                    discount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString()),
                    amount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value.ToString()),
                    Invoice = invnum.Text,
                    GST = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString()),
                    GSTV = ((Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()) * Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString())) * Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 100),
                    CGST = Convert.ToDecimal(dataGridView1.Rows[i].Cells[7].Value.ToString()),
                    SGST = Convert.ToDecimal(dataGridView1.Rows[i].Cells[8].Value.ToString()),
                    IGST = Convert.ToDecimal(dataGridView1.Rows[i].Cells[9].Value.ToString())

                };

                await MainEngine_.Add(dynamic, "sp_addsaleitem");
            }
        }

        private async Task ProcessTransaction(string callid, string payMode, decimal amount)
        {
            var TransCustomer = new
            {
                Cust_ID = int.Parse(callid),
                Cust_Name = cust_name.Text,
                InvoiceId = invnum.Text,
                PayMode = payMode,
                Amount = Convert.ToDecimal(totalamt.Text),
                Paid = amount,
                Quantity = decimal.Parse(sumqty.Text)
            };

            await MainEngine_.Add(TransCustomer, "InsertCustomerTransaction");
            MainEngine_.GetDataScript<dynamic>($"INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID,Particular,voucher_t) values(1,{amount},'Saving','{invdate.Text}','Sale/Recovery','{invnum.Text}','{cust_name.Text}','{payMode}')");
        }

        private void ClearForm()
        {
            dataGridView1.Rows.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            try
            {
                StoreRoom.ClearData(this.Controls);
            }
            catch (Exception ex)
            {
                 
            }
          
            amt.Text = "";
            totalamt.Text = "0";
            cal.discounts = 0;
            cal.sub_amount = 0;
            cal.total = 0;
            tax.Text = "0";
            sumqty.Text = "0";
            items.Text = "0";
            discount_.Text = "0";
            CustCount += 1;
            textBox2.Text = CustCount.ToString();
            invnum.Text = GenerateInvoice();

            onlinep.Text = "0";
            paid.Text = "0";
        }




















        private async void button1_Click(object sender, EventArgs e)
        {
            /*srs = 0;
            
            try
            {


                if (dataGridView1.Rows.Count > 0)
                {


                     if (MainEngine_.GetDataScript<string>("select items from SInvoice where items = '" + _inv + "'").FirstOrDefault() == _inv)
                    {







                        try

                        {

                            StoreRoom tr = new StoreRoom();
                            tr.NewData(cust_name.Text, textBox1.Text, mobile_num.Text, city.Text);

                        }
                        catch (Exception ex12)
                        {

                        }
                        string cashred;

                        decimal to = 0;



                        if (comboBox1.Text == "Cash")
                        {
                            cashred = "cash";
                            to = Convert.ToDecimal(totalamt.Text);

                        }
                        else
                        {
                            cashred = "credit";
                            to = Convert.ToDecimal(totalamt.Text);
                        }

                        string callid = MainEngine_.GetDataScript<string>("select id from Customer Where cust_name = '" + cust_name.Text + "'").FirstOrDefault();


                        if (decimal.Parse(onlinep.Text) > 0)
                        {
                            var TransCustomer = new
                            {
                                Cust_ID = int.Parse(callid),
                                Cust_Name = cust_name.Text,
                                InvoiceId = invnum.Text,
                                PayMode = comboBox3.Text,
                                Amount = to,
                                Paid = decimal.Parse(onlinep.Text),
                                Quantity = decimal.Parse(sumqty.Text)

                            };

                            await MainEngine_.Add(TransCustomer, "UpdateCustomerTransaction");
                            MainEngine_.GetDataScript<dynamic>($"UPDATE Transactions SET AccountID = 1, Amount = {decimal.Parse(onlinep.Text)}, Type = 'Saving', Date_ = '{invdate.Text}', Description = 'Sale/Recovery', Particular = '{cust_name.Text}', voucher_t = '{comboBox3.Text}' WHERE REFRANCE_ID = '"+invnum.Text+"'");


                        }
                        if (decimal.Parse(paid.Text) > 0 && decimal.Parse(onlinep.Text) > 0 || decimal.Parse(paid.Text) > 0)
                        {
                            var TransCustomer = new
                            {
                                Cust_ID = int.Parse(callid),
                                Cust_Name = cust_name.Text,
                                InvoiceId = invnum.Text,
                                PayMode = comboBox2.Text,
                                Amount = to,
                                Paid = decimal.Parse(paid.Text),
                                Quantity = decimal.Parse(sumqty.Text)

                            };


                            await MainEngine_.Add(TransCustomer, "UpdateCustomerTransaction");
                            MainEngine_.GetDataScript<dynamic>($"UPDATE Transactions SET AccountID = 1, Amount = {decimal.Parse(paid.Text)}, Type = 'Saving', Date_ = '{invdate.Text}', Description = 'Sale/Recovery', Particular = '{cust_name.Text}', voucher_t = '{comboBox3.Text}' WHERE REFRANCE_ID = '" + invnum.Text + "'");


                        }
                        if (paid.Text == "0" && onlinep.Text == "0")
                        {
                            var TransCustomer = new
                            {
                                Cust_ID = int.Parse(callid),
                                Cust_Name = cust_name.Text,
                                InvoiceId = invnum.Text,
                                PayMode = "Credit",
                                Amount = to,
                                Paid = decimal.Parse(paid.Text),
                                Quantity = decimal.Parse(sumqty.Text)

                            };


                            await MainEngine_.Add(TransCustomer, "UpdateCustomerTransaction");
                            MainEngine_.GetDataScript<dynamic>($"UPDATE Transactions SET AccountID = 1, Amount = {decimal.Parse(blc.Text)}, Type = 'Saving', Date_ = '{invdate.Text}', Description = 'Sale/Recovery', Particular = '{cust_name.Text}', voucher_t = 'Credit' WHERE REFRANCE_ID = '" + invnum.Text + "'");

                        }



                            

                        decimal td = 0;
                        inv = GenerateInvoice();
                        decimal sub = Convert.ToDecimal(totalamt.Text) - Convert.ToDecimal(discount_.Text);
  

                        var smodel = new
                        {
                            invoiceID = invnum.Text,
                            cust_Name = cust_name.Text,
                            items = invnum.Text,
                            sub_total = sub,
                            perdis = td,
                            discount = Convert.ToDecimal(senddiscount),
                            other = Convert.ToDecimal(tax.Text),
                            TotalBill = Convert.ToDecimal(totalamt.Text),
                            invdate = DateTime.Now,
                            place = textBox1.Text
 



                        };

                        await MainEngine_.Add(smodel, "spSaleInsert");





                        dataGridView1.Rows.Clear();


                        StoreRoom.ClearData(this.Controls);
                        amt.Text = "";
                        //sub_t.Text = "0";
                        //perdisc.Text = "0";
                        //discount_.Text = "0";
                        //other.Text = "0";
                        totalamt.Text = "0";
                        cal.discounts = 0;
                        cal.sub_amount = 0;
                        cal.total = 0;
                        tax.Text = "0";
                        sumqty.Text = "0";
                        items.Text = "0";
                        discount_.Text = "0";
                        CustCount = CustCount + 1;

                        textBox2.Text = CustCount.ToString();
                     
                        DialogResult result = MessageBox.Show("If you wan to Print ?", "Print ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        comboBox1.SelectedIndex = 0;
                        comboBox2.SelectedIndex = 0;
                        onlinep.Text = "0";
                        paid.Text = "0";
                        if (result == DialogResult.Yes)
                        {



                            ReportStd rp = new ReportStd(smodel.invoiceID, textBox1.Text);
                            rp.Show();
                        }
                        else
                        {
                            //pass
                        }


           

            }
                    else
                    {








                  
                            StoreRoom tr = new StoreRoom();
                              tr.NewData(cust_name.Text, textBox1.Text, mobile_num.Text, city.Text);

                       
                        string cashred;

                        decimal to = 0;



                        if (comboBox1.Text == "Cash")
                        {
                            cashred = "cash";
                            to = Convert.ToDecimal(totalamt.Text);



                        }
                        else
                        {
                            cashred = "credit";
                            to = Convert.ToDecimal(totalamt.Text);
                        }

                        string callid = MainEngine_.GetDataScript<string>("select id from Customer Where cust_name = '" + cust_name.Text + "'").FirstOrDefault();


                        if (decimal.Parse(onlinep.Text) > 0)
                        {
                            var TransCustomer = new
                            {
                                Cust_ID = int.Parse(callid),
                                Cust_Name = cust_name.Text,
                                InvoiceId = invnum.Text,
                                PayMode = comboBox3.Text,
                                Amount = to,
                                Paid = decimal.Parse(onlinep.Text),
                                Quantity = decimal.Parse(sumqty.Text)

                            };

                            await MainEngine_.Add(TransCustomer, "InsertCustomerTransaction");
                            MainEngine_.GetDataScript<dynamic>($"INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID,Particular,voucher_t) values(1,{decimal.Parse(onlinep.Text)},'Saving','{invdate.Text}','Sale/Recovery','{invnum.Text}','{cust_name.Text}','{comboBox3.Text}')");


                        }
                        if (decimal.Parse(paid.Text) > 0 && decimal.Parse(onlinep.Text) > 0 || decimal.Parse(paid.Text) > 0)
                        {
                            var TransCustomer = new
                            {
                                Cust_ID = int.Parse(callid),
                                Cust_Name = cust_name.Text,
                                InvoiceId = invnum.Text,
                                PayMode = comboBox2.Text,
                                Amount = to,
                                Paid = decimal.Parse(paid.Text),
                                Quantity = decimal.Parse(sumqty.Text)

                            };


                            await MainEngine_.Add(TransCustomer, "InsertCustomerTransaction");
                            MainEngine_.GetDataScript<dynamic>($"INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID,Particular,voucher_t) values(1,{decimal.Parse(paid.Text)},'Saving','{invdate.Text}','Sale/Recovery','{invnum.Text}','{cust_name.Text}','{comboBox2.Text}')");


                        }
                        if (paid.Text == "0" && onlinep.Text == "0")
                        {
                            var TransCustomer = new
                            {
                                Cust_ID = int.Parse(callid),
                                Cust_Name = cust_name.Text,
                                InvoiceId = invnum.Text,
                                PayMode = "Credit",
                                Amount = to,
                                Paid = decimal.Parse(paid.Text),
                                Quantity = decimal.Parse(sumqty.Text)

                            };


                            await MainEngine_.Add(TransCustomer, "InsertCustomerTransaction");
                            MainEngine_.GetDataScript<dynamic>($"INSERT INTO Transactions (AccountID, Amount, Type, Date_, Description, REFRANCE_ID,Particular,voucher_t) values(1,{decimal.Parse(blc.Text)},'Saving','{invdate.Text}','Sale/Recovery','{invnum.Text}','{cust_name.Text}','Credit')");

                        }




                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {

                            var dynamic = new
                            {

                                sr_no = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                                description = dataGridView1.Rows[i].Cells[1].Value.ToString(),
                                qty = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()),
                                rate = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString()),
                                discount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString()),
                                amount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value.ToString()),

                                Invoice = invnum.Text,
                                GST = Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString()),
                                GSTV = (Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()) * Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString()) * Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString())/100)

                            };


                            await MainEngine_.Add(dynamic, "sp_addsaleitem");

                        }




                        decimal td = 0;
                        inv = GenerateInvoice();
                        decimal sub = Convert.ToDecimal(totalamt.Text) - Convert.ToDecimal(discount_.Text);
                        var smodel = new
                        {
                            invoiceID = inv,
                            cust_Name = cust_name.Text,
                            items = inv,
                            sub_total = sub,
                            perdis = td,
                            discount = Convert.ToDecimal(senddiscount),
                            other = Convert.ToDecimal(tax.Text),
                            TotalBill = Convert.ToDecimal(totalamt.Text),
                            invdate = DateTime.Now,
                            place = textBox1.Text



                        };

                        await MainEngine_.Add(smodel, "spSaleInsert");





                        dataGridView1.Rows.Clear();


                        StoreRoom.ClearData(this.Controls);
                        amt.Text = "";
                        //sub_t.Text = "0";
                        //perdisc.Text = "0";
                        //discount_.Text = "0";
                        //other.Text = "0";
                        totalamt.Text = "0";
                        cal.discounts = 0;
                        cal.sub_amount = 0;
                        cal.total = 0;
                        tax.Text = "0";
                        sumqty.Text = "0";
                        items.Text = "0";
                        discount_.Text = "0";
                        CustCount = CustCount + 1;

                        textBox2.Text = CustCount.ToString();
                        invnum.Text = GenerateInvoice();

                        DialogResult result = MessageBox.Show("If you wan to Print ?", "Print ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        comboBox1.SelectedIndex = 0;
                        comboBox2.SelectedIndex = 0;
                        onlinep.Text = "0";
                        paid.Text = "0";
                        if (result == DialogResult.Yes)
                        {



                            ReportStd rp = new ReportStd(smodel.invoiceID, textBox1.Text);
                            rp.Show();
                        }
                        else
                        {
                            //pass
                        }


                    }
                }
                else
                {
                MessageBox.Show("No item Please Add item!","No Item",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                }

                Invoice ins = this;
                ins.Refresh();

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

            */

            if(comboBox4.SelectedIndex == 1)
            {

                List<WithoutSaveModels> models = new List<WithoutSaveModels>();

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    WithoutSaveModels model = new WithoutSaveModels();

                    model.Sr = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    model.ITEM_NAME = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    model.Qty = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    model.SALE_PRICE = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    model.discount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    model.Amount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value.ToString());
                    model.invoiceID = invnum.Text;
                    model.invdate = invdate.Value;
                    model.TotalBill = Convert.ToDecimal(totalamt.Text);
                    model.cust_Name = cust_name.Text;
                   
                    models.Add(model);
                }



                ReportStd std = new ReportStd("","wsave","","","", "", "", "",models);
                std.Show();
 

            }
            else
            {
                if(issave==true)
                {

                    PrintInvoice();
                    issave = false;
                }
                else
                {
                    MessageBox.Show("Please save your bill","Save ? ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

            }
             
            Taxes = 0; //reset taxs
            tax.Text = Taxes.ToString();

        }


        private void ItemsCount()
        {

            int count=  dataGridView1.Rows.Count-1;

            items.Text = count.ToString();

        }


        private void TotalQty()
        {
            int total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "")
                {
                    int quantity;
                    if (int.TryParse(row.Cells[2].Value.ToString(), out quantity))
                    {
                        total += quantity;
                    }
                    // If the cell value is not a valid integer, it will be ignored in the sum.
                }
            }
            sumqty.Text = total.ToString();
        }




        private void discountsu()
        {

            int total = 0;

 

            

            discount_.Text = senddiscount.ToString();

        }


        private async void button3_Click(object sender, EventArgs e)
        {



            decimal originalAmount = decimal.Parse(rete.Text) * decimal.Parse(q.Text);
            if (MainEngine_.GetDataScript<dynamic>("select ITEM_NAME from Product_Item where  ITEM_NAME = '" + desc.Text + "'").Count > 0)
            {
                 
                    MainEngine_.GetDataScript<dynamic>("UPDATE Product_Item SET SALE_PRICE = " + rete.Text + " where ITEM_NAME = '" + desc.Text + "' ");
             

                decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);
              

                decimal discount = originalAmount * (discountPercentage / 100);

              
               

                senddiscount = senddiscount + discount;
                if (Convert.ToInt32(q.Text) < MainEngine_.GetDataScript<int>("select Stock from Product_Item where ITEM_NAME = '" + desc.Text + "' ").FirstOrDefault())
                {
                    label24.Text = "No Message";

                    if (desc.Text == "")
                    {

                        paid.Focus();
                    }


                    try
                    {
                        Crys cr = new Crys();
                        if (dataGridView1.Rows.Count == 1)
                        {

                            srs = srs + 1;

                            decimal finalAmount = (originalAmount + (originalAmount * Convert.ToDecimal(gsttext.Text)/100))- discount;
                            amt.Text = finalAmount.ToString();

                            dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text,gsttext.Text, disc.Text, amt.Text,cgst.Text,sgst.Text,igst.Text);

                            TotalQty();
                            ItemsCount();
                            discountsu();
                            decimal gstPercentage;
                            if (decimal.TryParse(gsttext.Text, out gstPercentage))
                            {
                                // Assuming originalAmount and tax are already defined and are decimals
                                Taxes += originalAmount * gstPercentage / 100;
                                decimal percentageCGST = Convert.ToDecimal(cgst.Text);
                                decimal percentageSGST = Convert.ToDecimal(sgst.Text);
                                decimal percentageIGST = Convert.ToDecimal(igst.Text);

                                // Calculate the monetary values based on the percentages
                                decimal cgstAmount = (percentageCGST / 100) * originalAmount;
                                decimal sgstAmount = (percentageSGST / 100) * originalAmount;
                                decimal igstAmount = (percentageIGST / 100) * originalAmount;

                                // Update the values
                                CGSTm += cgstAmount;
                                SGSTm += sgstAmount;
                                IGSTm += igstAmount;

                                // Update the text properties
                                cgsto.Text = CGSTm.ToString(); // Formatting to two decimal places
                                sgsti.Text = SGSTm.ToString(); // Formatting to two decimal places
                                igsto.Text = IGSTm.ToString(); // Formatting to two decimal places



                                tax.Text = Taxes.ToString();

                          
                               
                            }


                            string cals = cal.SubTotal(amt.Text).ToString();

                            totalamt.Text = (decimal.Parse(cals)).ToString();


                            blc.Text = (decimal.Parse(cals)).ToString();
                            prs.Text = (prc - decimal.Parse(cals)).ToString();
                            
                            //cal.total = decimal.Parse(totalamt.Text);
                            StoreRoom.ClearData(panel3.Controls);
                            amt.Text = "";
                            cgst.Text = "0";
                            sgst.Text = "0";
                            igst.Text = "0";



                            this.BeginInvoke(new Action(() =>
                            {
                                desc.Focus();
                            }));


                        }
                        else
                        {
                             
                                srs = srs + 1;
                                decimal finalAmount = (originalAmount + (originalAmount * Convert.ToDecimal(gsttext.Text) / 100)) - discount;
                                amt.Text = finalAmount.ToString();
                                dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, gsttext.Text, disc.Text, amt.Text, cgst.Text, sgst.Text, igst.Text);

                                TotalQty();
                                ItemsCount();
                                discountsu();
                                decimal gstPercentage;
                                if (decimal.TryParse(gsttext.Text, out gstPercentage))
                                {
                                    // Assuming originalAmount and tax are already defined and are decimals
                                    Taxes += originalAmount * gstPercentage / 100;
                                    decimal percentageCGST = Convert.ToDecimal(cgst.Text);
                                    decimal percentageSGST = Convert.ToDecimal(sgst.Text);
                                    decimal percentageIGST = Convert.ToDecimal(igst.Text);

                                    // Calculate the monetary values based on the percentages
                                    decimal cgstAmount = (percentageCGST / 100) * originalAmount;
                                    decimal sgstAmount = (percentageSGST / 100) * originalAmount;
                                    decimal igstAmount = (percentageIGST / 100) * originalAmount;

                                    // Update the values
                                    CGSTm += cgstAmount;
                                    SGSTm += sgstAmount;
                                    IGSTm += igstAmount;

                                    // Update the text properties
                                    cgsto.Text = CGSTm.ToString(); // Formatting to two decimal places
                                    sgsti.Text = SGSTm.ToString(); // Formatting to two decimal places
                                    igsto.Text = IGSTm.ToString(); // Formatting to two decimal places


                                    tax.Text = Taxes.ToString();


                                }

                                string cals = cal.SubTotal(amt.Text).ToString();
                                totalamt.Text = (decimal.Parse(cals)).ToString();
                                blc.Text = (decimal.Parse(cals)).ToString();
                                prs.Text = (prc - decimal.Parse(cals)).ToString();

                               

                                //cal.total = decimal.Parse(totalamt.Text);
                                StoreRoom.ClearData(panel3.Controls);
                               

                                this.BeginInvoke(new Action(() =>
                                {
                                    desc.Focus();
                                }));
                                amt.Text = "";
                                cgst.Text = "0";
                                sgst.Text = "0";
                                igst.Text = "0";
                            

                             
                        }




                        
                    }



                    catch (Exception ex)
                    {
                    }
                    for (int i = 0;     i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = i + 1;
                        srs = i + 1;
                    }

                }

                else
                {
                     label24.Text = "Stock is Not Availale Please update Stock.....";

                    if (desc.Text == "")
                    {

                        paid.Focus();
                    }


                    try
                    {
                        Crys cr = new Crys();
                        if (dataGridView1.Rows.Count == 1)
                        {

                            srs = srs + 1;

                            dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, gsttext.Text, disc.Text, amt.Text, cgst.Text, sgst.Text, igst.Text);

                            TotalQty();
                            ItemsCount();
                            discountsu();

                            decimal gstPercentage;
                            if (decimal.TryParse(gsttext.Text, out gstPercentage))
                            {
                                // Assuming originalAmount and tax are already defined and are decimals
                                Taxes += originalAmount * gstPercentage / 100;
                                decimal percentageCGST = Convert.ToDecimal(cgst.Text);
                                decimal percentageSGST = Convert.ToDecimal(sgst.Text);
                                decimal percentageIGST = Convert.ToDecimal(igst.Text);

                                // Calculate the monetary values based on the percentages
                                decimal cgstAmount = (percentageCGST / 100) * originalAmount;
                                decimal sgstAmount = (percentageSGST / 100) * originalAmount;
                                decimal igstAmount = (percentageIGST / 100) * originalAmount;

                                // Update the values
                                CGSTm += cgstAmount;
                                SGSTm += sgstAmount;
                                IGSTm += igstAmount;

                                // Update the text properties
                                cgsto.Text = CGSTm.ToString(); // Formatting to two decimal places
                                sgsti.Text = SGSTm.ToString(); // Formatting to two decimal places
                                igsto.Text = IGSTm.ToString(); // Formatting to two decimal places


                                tax.Text = Taxes.ToString();

                            }
                            string cals = cal.SubTotal(amt.Text).ToString();
                            totalamt.Text = (decimal.Parse(cals)).ToString();


                            blc.Text = (decimal.Parse(cals)).ToString();
                            prs.Text = (prc - decimal.Parse(cals)).ToString();
                           

                            //cal.total = decimal.Parse(totalamt.Text);
                            StoreRoom.ClearData(panel3.Controls);
                            amt.Text = "";


                            this.BeginInvoke(new Action(() =>
                            {
                                desc.Focus();
                            }));


                        }
                        else
                        {
                            if (cr.isDuplicate(dataGridView1, desc.Text) == false)
                            {
                                srs = srs + 1;

                                dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, gsttext.Text, disc.Text, amt.Text, cgst.Text, sgst.Text, igst.Text);

                                TotalQty();
                                ItemsCount();
                                discountsu();
                                decimal gstPercentage;
                                if (decimal.TryParse(gsttext.Text, out gstPercentage))
                                {
                                    // Assuming originalAmount and tax are already defined and are decimals
                                    Taxes += originalAmount * gstPercentage / 100;
                                    decimal percentageCGST = Convert.ToDecimal(cgst.Text);
                                    decimal percentageSGST = Convert.ToDecimal(sgst.Text);
                                    decimal percentageIGST = Convert.ToDecimal(igst.Text);

                                    // Calculate the monetary values based on the percentages
                                    decimal cgstAmount = (percentageCGST / 100) * originalAmount;
                                    decimal sgstAmount = (percentageSGST / 100) * originalAmount;
                                    decimal igstAmount = (percentageIGST / 100) * originalAmount;

                                    // Update the values
                                    CGSTm += cgstAmount;
                                    SGSTm += sgstAmount;
                                    IGSTm += igstAmount;

                                    // Update the text properties
                                    cgsto.Text = CGSTm.ToString(); // Formatting to two decimal places
                                    sgsti.Text = SGSTm.ToString(); // Formatting to two decimal places
                                    igsto.Text = IGSTm.ToString(); // Formatting to two decimal places


                                    tax.Text = Taxes.ToString();


                                }

                                string cals = cal.SubTotal(amt.Text).ToString();
                                totalamt.Text = (decimal.Parse(cals)).ToString();
                                blc.Text = (decimal.Parse(cals)).ToString();
                                prs.Text = (prc - decimal.Parse(cals)).ToString();

                               

                                //cal.total = decimal.Parse(totalamt.Text);
                                StoreRoom.ClearData(panel3.Controls);
                                amt.Text = "";

                                this.BeginInvoke(new Action(() =>
                                {
                                    desc.Focus();
                                }));
                            }

                            else
                            {
                                cr.DuplicateValue();
                            }
                        }





                    }



                    catch (Exception ex)
                    {
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = i + 1;
                        srs = i + 1;
                    }


                    desc.Focus();
                }
            }
            else
            {

                var product = new
                {
                    PR_CODE = "PR_001",
                    ITEM_NAME = desc.Text.ToString(),
                    TYPE_TAX = 1,
                    STOCK = Convert.ToInt32(q.Text),
                    UNIT = comboBox5.Text,
                    BARCODE = "0",
                    SALE_PRICE = Convert.ToDecimal(rete.Text),
                    COST_PRICE = Convert.ToDecimal(rete.Text),
                    pr_ACCOUNT = "NO",
                    pr_DESCRIPTION = "No any Accout",
                    pr_COSTPRICE = 110,
                    IDATE = DateTime.Now.ToString(),
                    DESCRIPTION = "No Account",
                    ACCOUNT = "S",
                    USER_N = "Amir Feroz",
                    pic = "SSSSSSDDDSDSDSDS",
                    MRP = 0,
                    CGST = decimal.Parse(cgst.Text),
                    SGST = decimal.Parse(sgst.Text),
                    IGST = decimal.Parse(igst.Text),
                    HSN = "0",
                    Msg = msgdate.Value.ToString("yyyy/MM/dd"),
                    Exp = expdate.Value.ToString("yyyy/MM/dd"),
                    Color = "",
                    Size = ""

                };


                ProductAdd prd = new ProductAdd();
                await prd.AddProduct(product);



                if (desc.Text == "")
                {

                    paid.Focus();
                }


                try
                {
                    Crys cr = new Crys();
                    if (dataGridView1.Rows.Count == 1)
                    {

                        srs = srs + 1;

                        dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, gsttext.Text, disc.Text, amt.Text, cgst.Text, sgst.Text, igst.Text);

                        TotalQty();
                        ItemsCount();
                        discountsu();
                        decimal gstPercentage;
                        if (decimal.TryParse(gsttext.Text, out gstPercentage))
                        {
                            // Assuming originalAmount and tax are already defined and are decimals
                            Taxes += originalAmount * gstPercentage / 100;
                            decimal percentageCGST = Convert.ToDecimal(cgst.Text);
                            decimal percentageSGST = Convert.ToDecimal(sgst.Text);
                            decimal percentageIGST = Convert.ToDecimal(igst.Text);

                            // Calculate the monetary values based on the percentages
                            decimal cgstAmount = (percentageCGST / 100) * originalAmount;
                            decimal sgstAmount = (percentageSGST / 100) * originalAmount;
                            decimal igstAmount = (percentageIGST / 100) * originalAmount;

                            // Update the values
                            CGSTm += cgstAmount;
                            SGSTm += sgstAmount;
                            IGSTm += igstAmount;

                            // Update the text properties
                            cgsto.Text = CGSTm.ToString(); // Formatting to two decimal places
                            sgsti.Text = SGSTm.ToString(); // Formatting to two decimal places
                            igsto.Text = IGSTm.ToString(); // Formatting to two decimal places


                            tax.Text = Taxes.ToString();


                        }

                        string cals = cal.SubTotal(amt.Text).ToString();
                        totalamt.Text = (decimal.Parse(cals)).ToString();


                        blc.Text = (decimal.Parse(cals)).ToString();
                        prs.Text = (prc - decimal.Parse(cals)).ToString();
                       

                        //cal.total = decimal.Parse(totalamt.Text);
                        StoreRoom.ClearData(panel3.Controls);
                        amt.Text = "";


                        this.BeginInvoke(new Action(() =>
                        {
                            desc.Focus();
                        }));


                    }
                    else
                    {
                        if (cr.isDuplicate(dataGridView1, desc.Text) == false)
                        {
                            srs = srs + 1;

                            dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, gsttext.Text, disc.Text, amt.Text, cgst.Text, sgst.Text, igst.Text);

                            TotalQty();
                            ItemsCount();
                            discountsu();

                            decimal gstPercentage;
                            if (decimal.TryParse(gsttext.Text, out gstPercentage))
                            {
                                // Assuming originalAmount and tax are already defined and are decimals
                                Taxes += originalAmount * gstPercentage / 100;
                                decimal percentageCGST = Convert.ToDecimal(cgst.Text);
                                decimal percentageSGST = Convert.ToDecimal(sgst.Text);
                                decimal percentageIGST = Convert.ToDecimal(igst.Text);

                                // Calculate the monetary values based on the percentages
                                decimal cgstAmount = (percentageCGST / 100) * originalAmount;
                                decimal sgstAmount = (percentageSGST / 100) * originalAmount;
                                decimal igstAmount = (percentageIGST / 100) * originalAmount;

                                // Update the values
                                CGSTm += cgstAmount;
                                SGSTm += sgstAmount;
                                IGSTm += igstAmount;

                                // Update the text properties
                                cgsto.Text = CGSTm.ToString(); // Formatting to two decimal places
                                sgsti.Text = SGSTm.ToString(); // Formatting to two decimal places
                                igsto.Text = IGSTm.ToString(); // Formatting to two decimal places


                                tax.Text = Taxes.ToString();


                            }
                            string cals = cal.SubTotal(amt.Text).ToString();
                            totalamt.Text = (decimal.Parse(cals)).ToString();
                            blc.Text = (decimal.Parse(cals)).ToString();
                            prs.Text = (prc - decimal.Parse(cals)).ToString();

                           

                            //cal.total = decimal.Parse(totalamt.Text);
                            StoreRoom.ClearData(panel3.Controls);
                            amt.Text = "";

                            this.BeginInvoke(new Action(() =>
                            {
                                desc.Focus();
                            }));
                        }

                        else
                        {
                            cr.DuplicateValue();
                        }
                    }
                }



                catch (Exception ex)
                {
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = i + 1;
                    srs = i + 1;
                }









            }
        }

        private void q_KeyDown(object sender, KeyEventArgs e)
        {



        }

        private async void disc_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
            {


          

                if (totalamt != null && !string.IsNullOrEmpty(totalamt.Text) &&
    dataGridView1 != null &&
    e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count &&
    dataGridView1.Rows[e.RowIndex].Cells[5].Value != null)
                {
                    // Perform your calculations and assignments here
                    // Example:

                    decimal total;
                    if (decimal.TryParse(totalamt.Text, out total))
                    {
                        decimal amountCellValue;
                        if (decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), out amountCellValue))
                        {

                            totalamt.Text = (total - amountCellValue).ToString();
                            blc.Text = (total - amountCellValue).ToString();

                            cal.sub_amount = cal.sub_amount - amountCellValue;
                            prs.Text = (prc- (total - amountCellValue)).ToString();

                        }
                    }
                }
                decimal discountPercentage = string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) ? 0m : decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                decimal originalAmount = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());

                decimal originalAmounts = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) * decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());


                decimal discount = originalAmount * (discountPercentage / 100);
                senddiscount -= discount;

                decimal gstPercentage;
                if (decimal.TryParse(gsttext.Text, out gstPercentage))
                {
                    // Assuming originalAmount and tax are already defined and are decimals
                    Taxes -= originalAmounts * gstPercentage / 100;
                    tax.Text = Taxes.ToString();


                    // Ensure the cell values are not null and can be converted to decimal
                    decimal percentageCGST = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                    decimal percentageSGST = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                    decimal percentageIGST = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[9].Value);

                    // Calculate the monetary values based on the percentages
                    decimal cgstAmount = (percentageCGST / 100) * originalAmounts;
                    decimal sgstAmount = (percentageSGST / 100) * originalAmounts;
                    decimal igstAmount = (percentageIGST / 100) * originalAmounts;

                    // Update the values
                    CGSTm -= cgstAmount;
                    SGSTm -= sgstAmount;
                    IGSTm -= igstAmount;

                    // Update the text properties
                    cgsto.Text = CGSTm.ToString("0.##"); // Formatting to two decimal places
                    sgsti.Text = SGSTm.ToString("0.##"); // Formatting to two decimal places
                    igsto.Text = IGSTm.ToString("0.##"); // Formatting to two decimal places



                }


                dataGridView1.Rows.RemoveAt(e.RowIndex);

                TotalQty();
                ItemsCount();
                discountsu();

              




                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = i + 1;
                    srs = i - 1;
                }
            }
        }

        private void q_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the entered character is not a digit or a control character, ignore it
                e.Handled = true;
            }
        }

        private void button1_TabStopChanged(object sender, EventArgs e)
        {
        
        }

        private void button1_TabIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void button1_Leave(object sender, EventArgs e)
        {/*
            try
            {





                if (isenter == true) // Check if the active control is not the button itself
                {
                   
                     
                        button1.PerformClick();
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {

                if (checkBox1.Checked)
                {
                    isDownKeyPressed = true;

                    // Navigate and select next row in DataGridView when down arrow is pressed
                    int currentIndex = dgview.SelectedRows.Count > 0 ? dgview.SelectedRows[0].Index : -1;
                    int nextIndex = currentIndex + 1;

                    if (nextIndex < dgview.Rows.Count)
                    {
                        //   dgview.ClearSelection(); // Clear previous selection
                        dgview.Rows[nextIndex].Selected = true;
                        dgview.FirstDisplayedScrollingRowIndex = nextIndex;
                        if (dgview.Rows[nextIndex].Cells[0].Value != null)
                        {
                            desc.Text = dgview.Rows[nextIndex].Cells[0].Value.ToString();
                            amst = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[2].Value.ToString());
                            r = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[2].Value.ToString());
                            rete.Text = Convert.ToString(r);
                            amt.Text = Convert.ToString(amst);
                            if (comboBox1.SelectedIndex == 0 || comboBox1.Text == "GST")
                            {
                                gsttext.Text = Convert.ToString(dgview.Rows[nextIndex].Cells[3].Value.ToString());

                            }
                            else
                            {
                                gsttext.Text = "0";
                            }
                            v = amst;
                            q.Text = "1";
                            disc.Text = "0";
                        }
                    }
                    else
                    {
                        desc.Text = dgview.Rows[0].Cells[0].Value.ToString();
                        amst = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);

                        if (comboBox1.SelectedIndex == 0 || comboBox1.Text == "GST")
                        {
                            gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[3].Value.ToString());

                        }
                        else
                        {
                            gsttext.Text = "0";
                        }
                        v = amst;

                        q.Text = "1";
                        disc.Text = "0";
                    
                    
                    }
                
            }
                else
                {


                    isDownKeyPressed = true;

                    // Navigate and select next row in DataGridView when down arrow is pressed
                    int currentIndex = dgview.SelectedRows.Count > 0 ? dgview.SelectedRows[0].Index : -1;
                    int nextIndex = currentIndex + 1;

                    if (nextIndex < dgview.Rows.Count)
                    {
                        //   dgview.ClearSelection(); // Clear previous selection
                        dgview.Rows[nextIndex].Selected = true;
                        dgview.FirstDisplayedScrollingRowIndex = nextIndex;
                        if (dgview.Rows[nextIndex].Cells[0].Value != null)
                        {
                            desc.Text = dgview.Rows[nextIndex].Cells[0].Value.ToString();
                            amst = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[2].Value.ToString());
                            r = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[2].Value.ToString());
                            rete.Text = Convert.ToString(r);
                            if (comboBox1.SelectedIndex == 0 || comboBox1.Text == "GST")
                            {
                                gsttext.Text = Convert.ToString(dgview.Rows[nextIndex].Cells[3].Value.ToString());

                            }
                            else
                            {
                                gsttext.Text = "0";
                            }
                            amt.Text = Convert.ToString(amst);
                            v = amst;
                            q.Text = "1";
                            disc.Text = "0";
                        }
                    }
                    else
                    {
                        desc.Text = dgview.Rows[0].Cells[0].Value.ToString();
                        amst = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);
                        v = amst;
                        if (comboBox1.SelectedIndex == 0 || comboBox1.Text == "GST")
                        {
                            gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[3].Value.ToString());

                        }
                        else
                        {
                            gsttext.Text = "0";
                        }

                        q.Text = "1";
                        disc.Text = "0";
                    }
                }
            }else if(e.KeyCode == Keys.Enter)
            {
                if(checkBox1.Checked && dgview.Rows.Count> 0)
                {
                    desc.Text = dgview.Rows[0].Cells[0].Value.ToString();
                    amst = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                    r = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                    
                    rete.Text = Convert.ToString(r);
                    if (comboBox1.SelectedIndex == 0 || comboBox1.Text == "GST")
                    {
                        gsttext.Text = Convert.ToString(dgview.Rows[3].Cells[2].Value.ToString());

                    }
                    else
                    {
                        gsttext.Text = "0";
                    }
                    amt.Text = Convert.ToString(amst);
                    v = amst;

                    q.Text = "1";
                    disc.Text = "0";
                    dgview.Visible = false;

                }
                else
                {
                    desc.Text = dgview.Rows[0].Cells[0].Value.ToString();
                    amst = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                    r = Convert.ToDecimal(dgview.Rows[0].Cells[2].Value.ToString());
                    rete.Text = Convert.ToString(r);
                    if (comboBox1.SelectedIndex == 0 || comboBox1.Text == "GST")
                    {
                        gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[3].Value.ToString());

                    }
                    else
                    {
                        gsttext.Text = "0";
                    }
                    amt.Text = Convert.ToString(amst);
                    v = amst;

                    q.Text = "1";
                    disc.Text = "0";
                dgview.Visible = false;

                }


            }
        }

        private void rete_TextChanged(object sender, EventArgs e)
        {
            try
            {
                r = decimal.Parse(rete.Text);
                v = decimal.Parse(q.Text);
                v = v * r;
                amt.Text = v.ToString();

            }
            catch (Exception ex)
            {
                 
            }
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void paid_TextChanged(object sender, EventArgs e)
        {
            try
            
            {

 

                string cals = (prc - decimal.Parse(blc.Text)).ToString();

                if (decimal.TryParse(paid.Text == "" ? "0" : paid.Text, out decimal paidAmount) && decimal.TryParse(cals, out decimal currentBalanceAmount))
                {

                    if (paid.Text == "" || paid.Text == "0")
                    {
                        if (onlinep.Text!="" || onlinep.Text !="0")
                        {
                            prs.Text = ((paidAmount + decimal.Parse(onlinep.Text)) + currentBalanceAmount).ToString();        
                        }
                        else {
                            prs.Text = (prc - decimal.Parse(blc.Text)).ToString();
                        }
                            label28.Text = $"Rs.0";
                    }
                    else
                    {

                        decimal updatedValue;



                        // If you want to ensure that 'updatedValue' is always positive, use Math.Abs
                        // updatedValue = Math.Abs(updatedValue);

                        // Calculate the absolute value of the updatedValue


                        if (decimal.Parse(paid.Text) >= (prc - decimal.Parse(blc.Text)))
                        {
                            updatedValue = (paidAmount + decimal.Parse(onlinep.Text)) + currentBalanceAmount;
                            prs.ForeColor = Color.DarkOliveGreen;   
                            same =updatedValue;
                            prs.Text = same.ToString("0.00"); // Format the decimal value with two decimal places

                        }
                        else
                        {

                            updatedValue = currentBalanceAmount - (paidAmount + decimal.Parse(onlinep.Text));

                            same = -updatedValue;
                            prs.Text = same.ToString("0.00"); // Format the decimal value with two decimal places

                            prs.ForeColor = Color.MediumVioletRed;





                        }




                    }
                }

                label28.Text = $"Rs.{paid.Text.ToString()}";

            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                
            }
        }

        private void paid_Validated(object sender, EventArgs e)
        {

        }

        private void onlinep_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string cals = (prc - decimal.Parse(blc.Text)).ToString();

                if (decimal.TryParse(onlinep.Text == "" ? "0" : onlinep.Text, out decimal paidAmount) && decimal.TryParse(cals, out decimal currentBalanceAmount))
                {

                    if (onlinep.Text == "" || onlinep.Text == "0")
                    {
                        if (paid.Text != "" || paid.Text != "0")
                        {
                            prs.Text = ((paidAmount + decimal.Parse(paid.Text)) + currentBalanceAmount).ToString();
                        }
                        else
                        {
                            prs.Text = (prc - decimal.Parse(blc.Text)).ToString();
                        }
                        label28.Text = $"Rs.0";
                    }
                    else
                    {

                        decimal updatedValue;



                        // If you want to ensure that 'updatedValue' is always positive, use Math.Abs
                        // updatedValue = Math.Abs(updatedValue);

                        // Calculate the absolute value of the updatedValue


                        if (decimal.Parse(paid.Text) >= (prc - decimal.Parse(blc.Text)))
                        {
                            updatedValue = (paidAmount + decimal.Parse(paid.Text)) + currentBalanceAmount;
                            prs.ForeColor = Color.DarkOliveGreen;
                            same = updatedValue;
                            prs.Text = same.ToString("0.00"); // Format the decimal value with two decimal places

                        }
                        else
                        {

                            updatedValue = currentBalanceAmount - (paidAmount+decimal.Parse(paid.Text));

                            same = -updatedValue;
                            prs.Text = same.ToString("0.00"); // Format the decimal value with two decimal places

                            prs.ForeColor = Color.MediumVioletRed;





                        }




                    }
                }

                label29.Text = $"Rs.{onlinep.Text.ToString()}";
            }catch(Exception ex)
            {

            }
            }

        private void invnum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                List<dynamic> fet=   MainEngine_.GetDataScript<dynamic>("select  sr_no,description,qty,rate,GST,discount,amount from Sale_Items where Invoice = '" + invnum.Text+"' ").ToList();
                // Suspend the layout to optimize the update process
                dataGridView1.SuspendLayout();


                // Add rows to DataGridView in bulk
                foreach (var item in fet)
                {
                    dataGridView1.Rows.Add(item.sr_no, item.description, item.qty, item.rate,item.GST, item.discount, item.amount);
                }

                // Resume the layout to reflect changes
                dataGridView1.ResumeLayout();
                string customername = MainEngine_.GetDataScript<string>("select cust_name from SInvoice where  items = '"+invnum.Text+"'").FirstOrDefault();
                cust_name.Text = customername;

                
                items.Text = $"{dataGridView1.Rows.Count-1}";
                TotalQty();


                var dynamic = new
                {
                    id = invnum.Text,
                    name = "paid"

                };
                var dynamic1 = new
                {
                    id = invnum.Text,
                    name = "online"

                };



               var p =  MainEngine_.GetData<dynamic>("Get_Amount_PAID", dynamic).FirstOrDefault();
                var online = MainEngine_.GetData<dynamic>("Get_Amount_PAID", dynamic1).FirstOrDefault();
                paid.Text = $"{p.PAID}";
  
                onlinep.Text = $"{online.PAID}";
                comboBox3.SelectedIndex = 3;


                totalamt.Text = StoreRoom.TotalBill(invnum.Text);
                tax.Text = StoreRoom.TotalTax(invnum.Text);
                Taxes = Convert.ToDecimal(StoreRoom.TotalTax(invnum.Text));



            }
            catch (Exception ex)
            {
                 
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            

            try
            {
                // Extract values from DataGridView cells
                decimal quantity = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                decimal unitPrice = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                decimal gst = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value);

                decimal discountPercentage = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                decimal actualamount = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                // Calculate the total price before discount
                decimal totalPrice = quantity * unitPrice;

                // Calculate the discount amount
                decimal discountAmount = totalPrice * (discountPercentage / 100);

                decimal finalPrice = (StoreRoom.GST(totalPrice, Convert.ToInt32(gst)) - discountAmount);

                // Assign the final calculated value to the corresponding cell


                decimal act = (-1) * actualamount;

                cal.SubTotal(act.ToString());

                dataGridView1.Rows[e.RowIndex].Cells[6].Value = finalPrice;


                string cals = cal.SubTotal(finalPrice.ToString()).ToString();
                totalamt.Text = (decimal.Parse(cals)).ToString();
                blc.Text = (decimal.Parse(cals)).ToString();
                prs.Text = (prc - decimal.Parse(cals)).ToString();

                decimal gstPercentage;
    
                
                    // Assuming originalAmount and tax are already defined and are decimals
                    Taxes -= totalPrice * gst / 100;

                    Taxes += totalPrice * gst / 100;


  

                tax.Text = Taxes.ToString();





            }
            catch (Exception ex)
            {

            }

        
   }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Temporarily remove the event handler to prevent re-entrancy
            textBox3.TextChanged -= textBox3_TextChanged;

            try
            {
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    return; // If the text box is empty, do nothing
                }

                // Fetch item details in one query to avoid redundant calls and improve performance.
                string query = $"SELECT ITEM_NAME, SALE_PRICE,GST FROM Product_Item WHERE BARCODE = '{textBox3.Text.Replace("'", "''")}'";

                var itemDetails = MainEngine_.GetDataScript<dynamic>(query).FirstOrDefault();
                int gstvalueS = itemDetails.GST;

                if (itemDetails != null && !string.IsNullOrWhiteSpace(itemDetails.ITEM_NAME))
                {
                    string itemName = itemDetails.ITEM_NAME;
                    decimal salePrice = itemDetails.SALE_PRICE;
                    int gstvalue = itemDetails.GST;


                    // Check for duplicate items
                    Crys cr = new Crys();
                    if (cr.isDuplicate(dataGridView1, itemName))
                    {
                        // If the item is a duplicate, update the quantity in the existing row
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["dsf"].Value != null && row.Cells["dsf"].Value.ToString() == itemName)
                            {
                                int currentQty = int.Parse(row.Cells["Qtyssss"].Value.ToString());
                                currentQty++;
                                row.Cells["Qtyssss"].Value = currentQty;
                                row.Cells["GST"].Value = gstvalue;
                                row.Cells["Amountsss"].Value = (currentQty * salePrice * (gstvalue /100)).ToString();


                                // Update totals and counts
                                TotalQty();
                                ItemsCount();
                                discountsu();
                                UpdateTotalAmount();
                                break;
                            }
                        }
                    }
                    else
                    {
                        // Increment the serial number for the new item
                        srs++;

                        // Add a new row to the DataGridView
                        dataGridView1.Rows.Add(srs, itemName, 1, salePrice.ToString(), gstvalue,0, salePrice + (salePrice * (gstvalueS / 100)));

                        // Update totals and counts
                        TotalQty();
                        ItemsCount();
                        discountsu();
                        UpdateTotalAmount();
                    }

                    // Clear the controls in the panel
                    StoreRoom.ClearData(panel3.Controls);
                }
            }
            catch (Exception ex)
            {
                // Log the error or inform the user
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Reattach the event handler
                textBox3.TextChanged += textBox3_TextChanged;
            }
        }

        private void UpdateTotalAmount()
        {
            decimal subtotal = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Amountsss"].Value != null)
                {
                    subtotal += decimal.Parse(row.Cells["Amountsss"].Value.ToString());
                }
            }

            totalamt.Text = subtotal.ToString();
            blc.Text = subtotal.ToString();
            prs.Text = (prc - subtotal).ToString();

            if (!string.IsNullOrEmpty(tax.Text))
            {
                totalamt.Text = (subtotal + decimal.Parse(tax.Text)).ToString();
            }
        }

        private void gsttext_TextChanged(object sender, EventArgs e)
        {
            try
            {


                try
                {

                    decimal a = Convert.ToDecimal(gsttext.Text) / 2;

                    if (isState==true)
                    {
                        cgst.Text = a.ToString();
                        sgst.Text = a.ToString();
                        igst.Text = "0";

                    }
                    else
                    {
                        igst.Text = gsttext.Text;
                    }
                    
                }
                catch (Exception ex)
                {

                }

                decimal total = Convert.ToDecimal(rete.Text) * Convert.ToDecimal(q.Text);
                    amt.Text = (StoreRoom.GST(total, Convert.ToInt32(gsttext.Text))- okdiscount).ToString();



              




            }
            catch (Exception ex)
            {
                 
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enterKeyPressed = true;
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            temp_cust = textBox1.Text;
            temp_invoice = invnum.Text;
            SaveAndPrint("F1");
        }

        private void Invoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                MessageBox.Show("You pressed the 'A' key!");
            }
            else if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("You pressed the 'Enter' key!");
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            temp_cust = textBox1.Text;
            temp_invoice = invnum.Text;
            SaveAndPrint("F2");
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            temp_cust = textBox1.Text;
            temp_invoice = invnum.Text;

            Save();
            issave = true;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            try
            {

                Unit s = new Unit();
                s.Show();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void comboBox5_Click(object sender, EventArgs e)
        {
     

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if(checkBox3.Checked)
                {
                    cust_name.Text = "CASH";
                    cust_name.SelectedIndex = 0;
                }
                else
                {
                    cust_name.Text = "";
         

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

                if(comboBox1.SelectedIndex == 1)
                {
                    gsties_p.Visible = true;
                }
                else
                {
                    gsties_p.Visible = false;

                }


            }
            catch (Exception ex)
            {
                 
            }
        }

        private void info_CheckedChanged(object sender, EventArgs e)
        {
            if (info.Checked)
            {
                lmg.Visible = true;
                lmg2.Visible = true;
                msgdate.Visible = true;
                expdate.Visible = true;



            }
            else
            {
                lmg.Visible = false;
                lmg2.Visible = false;
                msgdate.Visible = false;
                expdate.Visible = false;

            }
        }

        private void desc_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                if (checkBox1.Checked)
                {
              
                    var test = new
                    {
                        name = desc.Text
                    };
                    List<dynamic> list = new List<dynamic>();
                    try
                    {
                         list = MainEngine_.GetData<dynamic>("GetItemMac", test);

                    }
                    catch (Exception ex)
                    {
                         
                    }

                    if (list.Count < 0)
                    {
                        list = MainEngine_.GetData<dynamic>("spselectitem",test);
                    }

                    q.Text = "1";
                    disc.Text = "0";



                    foreach (var item in list)
                    {
                        r = (item.Item_price);
                        amst = item.Item_Cost;


                    }

                    rete.Text = Convert.ToString(r);
                    amt.Text = Convert.ToString(amst);
                    v = amst;
                }
                else
                {

                  
                    var test = new
                    {
                        ITEM_NAME = desc.Text
                    };
                    List<dynamic> list = MainEngine_.GetData<dynamic>("spselectitem", test);

                    q.Text = "1";
                    disc.Text = "0";



                    foreach (var item in list)
                    {
                        r = (item.SALE_PRICE);
                        amst = item.SALE_PRICE;


                    }

                    rete.Text = Convert.ToString(r);
                    amt.Text = Convert.ToString(amst);
                    v = amst;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
