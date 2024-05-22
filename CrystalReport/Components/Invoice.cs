 
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

namespace CrystalReport.Components
{
    public partial class Invoice : UserControl
    {


        private decimal senddiscount = 0;
        public Calculation cal = new Calculation();
        private int CustCount = 0;
        public Invoice(string inv = "")
        {
            _inv = inv;
            InitializeComponent();
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
                    string cellValue2 = selectedRow.Cells[1].Value.ToString(); // Assuming column 3 contains strings

                    // Do something with the values or the selected row
                    // Example: Display the values in a message box


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

                    // Do something with the values or the selected row
                    // Example: Display the values in a message box


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
            }
        }


        //Two Column
        void Search(int LX, int LY, int DW, int DH, string ColName, String ColSize)
        {
            this.dgview.Location = new System.Drawing.Point(LX, LY);
            this.dgview.Size = new System.Drawing.Size(DW, DH);

            string[] ClSize = ColSize.Split(',');
            //Size
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
            //Name 
            string[] ClName = ColName.Split(',');

            for (int i = 0; i < ClName.Length; i++)
            {
                this.dgview.Columns[i].HeaderText = ClName[i];
                this.dgview.Columns[i].Visible = true;
            }
        }
    

    private string GenerateInvoice()
        {
            string inv = "";
            try
            {
                var sql = "select * from SInvoice";
                var sql1 = $"select * from Customer";
                int custsr = MainEngine_.GetDataScript<int>(sql).Count();
                int srn = MainEngine_.GetDataScript<int>(sql).Count();
                srn = srn + 1;
                custsr = custsr + 1;

                inv =  $"INV{DateTime.Now.ToString("yy")}{DateTime.Now.ToString("MM")}{DateTime.Now.ToString("dd")}{custsr}{srn}";

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
                    tr.NewData(cust_name.Text, textBox1.Text, mobile_num.Text,city.Text);


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
                        amst = Convert.ToDecimal(dgview.Rows[0].Cells[1].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[0].Cells[1].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);
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

                city.Text = ciyu.ToString();

                prc = MainEngine_.GetDataScript<decimal>("Select Balance from Customer where cust_name = '" + cust_name.Text + "'").FirstOrDefault();


                prs.Text = (prc.ToString() ?? "0").ToString();
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
        private   void desc_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (checkBox1.Checked)
                {


                    if (desc.Text.Length <= 0)
                    {
                        dgview.Visible = false;
                    }

                    if (isDownKeyPressed == false)
                    {


                        if (desc.Text.Length > 0)
                        {

                            var searchTerm = desc.Text.Trim();

                            if (!string.IsNullOrWhiteSpace(searchTerm))
                            {
                                this.dgview.Visible = true;
                                dgview.BringToFront();
                                Search(9, 250, 430, 200, "Item Name,Price", "100,0");

                                // Assuming MainEngine_.SERVER_PATH contains the connection string
                                using (SqlConnection con = new SqlConnection(MainEngine_.SERVER_PATH))
                                {
                                    con.Open();
                                    string query = "SELECT TOP(40) Item_Name,Item_price FROM Mechanic WHERE Mechanic_Name = '"+cust_name.Text+ "' AND Item_Name LIKE @SearchTerm + '%'";
                                    using (SqlCommand cmd = new SqlCommand(query, con))
                                    {
                                        cmd.Parameters.AddWithValue("@SearchTerm", desc.Text);

                                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                                        {
                                            DataTable dt = new DataTable();
                                            sda.Fill(dt);
                                            dgview.Rows.Clear();

                                            if (dt.Rows.Count > 0)
                                            {


                                                foreach (DataRow row in dt.Rows)
                                                {
                                                    int n = dgview.Rows.Add();
                                                    dgview.Rows[n].Cells[0].Value = row["Item_Name"].ToString();

                                                    dgview.Rows[n].Cells[1].Value = row["Item_price"].ToString();

                                                }
                                            }
                                            else
                                            {
                                                con.Close();
                                                checkBox1.Checked = false;

                                                con.Open();
                                                string query1 = "SELECT TOP(40) ITEM_NAME,sSALE_PRICE FROM Product_Item WHERE ITEM_NAME LIKE @SearchTerm + '%'";
                                                using (SqlCommand cmd1 = new SqlCommand(query1, con))
                                                {
                                                    cmd.Parameters.AddWithValue("@SearchTerm", desc.Text);

                                                    using (SqlDataAdapter sda1 = new SqlDataAdapter(cmd))
                                                    {
                                                        DataTable dt1 = new DataTable();
                                                        sda1.Fill(dt);
                                                        dgview.Rows.Clear();

                                                        foreach (DataRow row in dt.Rows)
                                                        {
                                                            int n = dgview.Rows.Add();
                                                            dgview.Rows[n].Cells[0].Value = row["ITEM_NAME"].ToString();
                                                         
                                                            dgview.Rows[n].Cells[2].Value = row["SALE_PRICE"].ToString();

                                                        }


                                                    }
                                                }
                                                con.Close();
                                            }

                                        }
                                    }
                                }

                            }
                            else
                            {


                            }
                        }

                    }
                    else
                    {
                        isDownKeyPressed = false;

                    }
                }
                else
                {

                    

                    
                    if (desc.Text.Length <= 0)
                    {
                        dgview.Visible = false;
                    }

                    if (isDownKeyPressed == false)
                    {


                        if (desc.Text.Length > 0)
                        {

                            var searchTerm = desc.Text.Trim();

                            if (!string.IsNullOrWhiteSpace(searchTerm))
                            {
                                this.dgview.Visible = true;
                                dgview.BringToFront();
                                Search(9, 250, 430, 200, "Item Name,Stock,Price", "100,0");

                                // Assuming MainEngine_.SERVER_PATH contains the connection string
                                using (SqlConnection con = new SqlConnection(MainEngine_.SERVER_PATH))
                                {
                                    con.Open();
                                    string query = "SELECT TOP(40) ITEM_NAME,STOCK,SALE_PRICE FROM Product_Item WHERE ITEM_NAME LIKE @SearchTerm + '%'";
                                    using (SqlCommand cmd = new SqlCommand(query, con))
                                    {
                                        cmd.Parameters.AddWithValue("@SearchTerm", desc.Text);

                                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                                        {
                                            DataTable dt = new DataTable();
                                            sda.Fill(dt);
                                            dgview.Rows.Clear();

                                            foreach (DataRow row in dt.Rows)
                                            {
                                                int n = dgview.Rows.Add();
                                                dgview.Rows[n].Cells[0].Value = row["ITEM_NAME"].ToString();
                                                dgview.Rows[n].Cells[1].Value = row["STOCK"].ToString();
                                                dgview.Rows[n].Cells[2].Value = row["SALE_PRICE"].ToString();

                                            }


                                        }
                                    }
                                    con.Close();
                                }
                                
                            }
                            else
                            {


                            }
                        }

                    }
                    else
                    {
                        isDownKeyPressed = false;

                    }
                }
            }
            catch (Exception ex)
            {
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
                    decimal originalAmount = decimal.Parse(amt.Text);

                    decimal discountedAmount = originalAmount - (discountPercentage / 100m * originalAmount);


                    amt.Text = discountedAmount.ToString();
                }
                else
                {

                    amt.Text = v.ToString();
                    decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);
                    decimal originalAmount = decimal.Parse(amt.Text);

                    decimal discountedAmount = originalAmount - discountPercentage;


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

        private async void button1_Click(object sender, EventArgs e)
        {
            srs = 0;
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

                                Invoice = invnum.Text
                            };


                            await MainEngine_.Add(dynamic, "sp_addsaleitem");

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
                                discount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()),
                                amount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString()),

                                Invoice = invnum.Text
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


            if (MainEngine_.GetDataScript<dynamic>("select ITEM_NAME from Product_Item where  ITEM_NAME = '" + desc.Text + "'").Count > 0)
            {
                 
                    MainEngine_.GetDataScript<dynamic>("UPDATE Product_Item SET SALE_PRICE = " + rete.Text + " where ITEM_NAME = '" + desc.Text + "' ");
             

                decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);
                decimal originalAmount = decimal.Parse(rete.Text) * decimal.Parse(q.Text);

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

                            dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, disc.Text, amt.Text);

                            TotalQty();
                            ItemsCount();
                            discountsu();


                            string cals = cal.SubTotal(amt.Text).ToString();

                            totalamt.Text = (decimal.Parse(cals)).ToString();


                            blc.Text = (decimal.Parse(cals)).ToString();
                            prs.Text = (prc - decimal.Parse(cals)).ToString();
                            if (tax.Text != "") { totalamt.Text = (decimal.Parse(totalamt.Text) + decimal.Parse(tax.Text)).ToString(); }

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

                                dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, disc.Text, amt.Text);

                                TotalQty();
                                ItemsCount();
                                discountsu();


                                string cals = cal.SubTotal(amt.Text).ToString();
                                totalamt.Text = (decimal.Parse(cals)).ToString();
                                blc.Text = (decimal.Parse(cals)).ToString();
                                prs.Text = (prc - decimal.Parse(cals)).ToString();

                                if (tax.Text != "") { totalamt.Text = (decimal.Parse(totalamt.Text) + decimal.Parse(tax.Text)).ToString(); }

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

                            dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, disc.Text, amt.Text);

                            TotalQty();
                            ItemsCount();
                            discountsu();


                            string cals = cal.SubTotal(amt.Text).ToString();
                            totalamt.Text = (decimal.Parse(cals)).ToString();


                            blc.Text = (decimal.Parse(cals)).ToString();
                            prs.Text = (prc - decimal.Parse(cals)).ToString();
                            if (tax.Text != "") { totalamt.Text = (decimal.Parse(totalamt.Text) + decimal.Parse(tax.Text)).ToString(); }

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

                                dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, disc.Text, amt.Text);

                                TotalQty();
                                ItemsCount();
                                discountsu();


                                string cals = cal.SubTotal(amt.Text).ToString();
                                totalamt.Text = (decimal.Parse(cals)).ToString();
                                blc.Text = (decimal.Parse(cals)).ToString();
                                prs.Text = (prc - decimal.Parse(cals)).ToString();

                                if (tax.Text != "") { totalamt.Text = (decimal.Parse(totalamt.Text) + decimal.Parse(tax.Text)).ToString(); }

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
                    UNIT = "pcs",
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
                    pic = "SSSSSSDDDSDSDSDS"

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

                        dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, disc.Text, amt.Text);

                        TotalQty();
                        ItemsCount();
                        discountsu();


                        string cals = cal.SubTotal(amt.Text).ToString();
                        totalamt.Text = (decimal.Parse(cals)).ToString();


                        blc.Text = (decimal.Parse(cals)).ToString();
                        prs.Text = (prc - decimal.Parse(cals)).ToString();
                        if (tax.Text != "") { totalamt.Text = (decimal.Parse(totalamt.Text) + decimal.Parse(tax.Text)).ToString(); }

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

                            dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, disc.Text, amt.Text);

                            TotalQty();
                            ItemsCount();
                            discountsu();


                            string cals = cal.SubTotal(amt.Text).ToString();
                            totalamt.Text = (decimal.Parse(cals)).ToString();
                            blc.Text = (decimal.Parse(cals)).ToString();
                            prs.Text = (prc - decimal.Parse(cals)).ToString();

                            if (tax.Text != "") { totalamt.Text = (decimal.Parse(totalamt.Text) + decimal.Parse(tax.Text)).ToString(); }

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
                        if (decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString(), out amountCellValue))
                        {

                            totalamt.Text = (total - amountCellValue).ToString();
                            blc.Text = (total - amountCellValue).ToString();

                            cal.sub_amount = cal.sub_amount - amountCellValue;
                            prs.Text = (prc- (total - amountCellValue)).ToString();

                        }
                    }
                }
                decimal discountPercentage = string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) ? 0m : decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                decimal originalAmount = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());

                decimal discount = originalAmount * (discountPercentage / 100);
                senddiscount -= discount;

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
        {
            try
            {





                if (isenter == true) // Check if the active control is not the button itself
                {
                    DialogResult result = MessageBox.Show("Print and Save Bill?", "Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        button1.PerformClick();
                    }
                    else
                    {
                        // Handle the case where "No" is clicked in the MessageBox
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                            amst = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[1].Value.ToString());
                            r = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[1].Value.ToString());
                            rete.Text = Convert.ToString(r);
                            amt.Text = Convert.ToString(amst);
                            v = amst;
                            q.Text = "1";
                            disc.Text = "0";
                        }
                    }
                    else
                    {
                        desc.Text = dgview.Rows[0].Cells[0].Value.ToString();
                        amst = Convert.ToDecimal(dgview.Rows[0].Cells[1].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[0].Cells[1].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);
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

                        q.Text = "1";
                        disc.Text = "0";
                    }
                }
            }else if(e.KeyCode == Keys.Enter)
            {
                if(checkBox1.Checked && dgview.Rows.Count> 0)
                {
                    desc.Text = dgview.Rows[0].Cells[0].Value.ToString();
                    amst = Convert.ToDecimal(dgview.Rows[0].Cells[1].Value.ToString());
                    r = Convert.ToDecimal(dgview.Rows[0].Cells[1].Value.ToString());
                    
                    rete.Text = Convert.ToString(r);
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
                MessageBox.Show("An error occurred: " + ex.Message);
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
                List<dynamic> fet=   MainEngine_.GetDataScript<dynamic>("select  sr_no,description,qty,rate,discount,amount from Sale_Items where Invoice = '" + invnum.Text+"' ").ToList();
                // Suspend the layout to optimize the update process
                dataGridView1.SuspendLayout();


                // Add rows to DataGridView in bulk
                foreach (var item in fet)
                {
                    dataGridView1.Rows.Add(item.sr_no, item.description, item.qty, item.rate, item.discount, item.amount);
                }

                // Resume the layout to reflect changes
                dataGridView1.ResumeLayout();
                string customername = MainEngine_.GetDataScript<string>("select cust_name from SInvoice where  items = '"+invnum.Text+"'").FirstOrDefault();
                cust_name.Text = customername;

                
                items.Text = $"{dataGridView1.Rows.Count-1}";
                TotalQty();


                var dynamic = new
                {
                    id = _inv,
                    name = "paid"

                };
                var dynamic1 = new
                {
                    id = _inv,
                    name = "online"

                };


               var p =  MainEngine_.GetData<dynamic>("Get_Amount_PAID", dynamic).FirstOrDefault();
                var online = MainEngine_.GetData<dynamic>("Get_Amount_PAID", dynamic1).FirstOrDefault();
                paid.Text = $"{p.PAID}";
  
                onlinep.Text = $"{online.PAID}";
                comboBox3.SelectedIndex = 3;

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
                decimal discountPercentage = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                decimal actualamount = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                // Calculate the total price before discount
                decimal totalPrice = quantity * unitPrice;

                // Calculate the discount amount
                decimal discountAmount = totalPrice * (discountPercentage / 100);

                // Calculate the final price after applying the discount
                decimal finalPrice = totalPrice - discountAmount;
                // Assign the final calculated value to the corresponding cell


                decimal act = (-1) * actualamount;

                cal.SubTotal(act.ToString());

                dataGridView1.Rows[e.RowIndex].Cells[5].Value = finalPrice;


string cals = cal.SubTotal(finalPrice.ToString()).ToString();
                totalamt.Text = (decimal.Parse(cals)).ToString();
                blc.Text = (decimal.Parse(cals)).ToString();
                prs.Text = (prc - decimal.Parse(cals)).ToString();

                if (tax.Text != "") { totalamt.Text = (decimal.Parse(totalamt.Text) + decimal.Parse(tax.Text)).ToString(); }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the calculation
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

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
