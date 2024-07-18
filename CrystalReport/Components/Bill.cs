using Back_Dr.Sale;
using Back_Dr.Sale.Inventory;
using CrystalDecisions.CrystalReports.Engine;
using CrystalReport.Components;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class Bill : UserControl
    {

        public Calculation cal = new Calculation();
        private string inv;
        private decimal senddiscount = 0;
        private int srs;
        private decimal v;
        private decimal r;
        private decimal amst;

        public Bill()
        {
            InitializeComponent();
            PartiNameList.IDSupp += PartiNameList_IDSupp;
        }

        private void PartiNameList_IDSupp(object sender, string e)
        {
            supid.Text = StoreRoom.GetData;
        }

        public void PerDisc()
        {
            try
            {



            }
            catch (Exception ex)
            {

            }
        }

        private string GenerateInvoice()
        {
            string inv = "";
            try
            {
                var sql = "select * from Bill";
                var sql1 = $"select * from Parties";
                int custsr = MainEngine_.GetDataScript<int>(sql).Count();
                int srn = MainEngine_.GetDataScript<int>(sql).Count();
                srn = srn + 1;
                custsr = custsr + 1;

                inv = $"BILL{DateTime.Now.ToString("yy")}{DateTime.Now.ToString("MM")}{DateTime.Now.ToString("dd")}{custsr}{srn}";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return inv;


        }

        private void invoice1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void cust_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               

            }
            catch (Exception ex)
            {

            }
        }

        private void cust_name_TextChanged(object sender, EventArgs e)
        {
            try
            {

                /*
                                List<dynamic> list = MainEngine_.GetDataScript<dynamic>("select company from Parties where pname LIKE '%" + cust_name.Text + "%' ").ToList();

                                foreach (var item in list)
                                {
                                    cust_name.Items.Add(item.company);

                                }



                                */

                            }


            catch (Exception ex)
            {
            }
        }



        private void dataGridView1_CellClickAsync(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
            {




                if (totalamt != null && !string.IsNullOrEmpty(totalamt.Text) &&
    dataGridView1 != null &&
    e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count &&
    dataGridView1.Rows[e.RowIndex].Cells[7].Value != null)
                {
                    // Perform your calculations and assignments here
                    // Example:

                    decimal total;
                    if (decimal.TryParse(totalamt.Text, out total))
                    {
                        decimal amountCellValue;
                        if (decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString(), out amountCellValue))
                        {

                            totalamt.Text = (total - amountCellValue).ToString();
            

                            cal.sub_amount = cal.sub_amount - amountCellValue;
                          

                        }
                    }
                }
                decimal discountPercentage = string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString()) ? 0m : decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                decimal originalAmount = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());

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
                    cgsto.Text = CGSTm.ToString(); // Formatting to two decimal places
                    sgsto.Text = SGSTm.ToString(); // Formatting to two decimal places
                    igsto.Text = IGSTm.ToString(); // Formatting to two decimal places



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


        private async void button2_ClickAsync(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("IF you want to save bill ?", "Bill Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                srs = 1;

                try
                {


                    inv = GenerateInvoice();



                    if (dataGridView1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            var dynamic = new
                            {


                                sr_no = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString()),
                                description = dataGridView1.Rows[i].Cells[1].Value.ToString(),
                                qty = Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value.ToString()),
                                rate = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString()),
                          
                                Bill = inv,
                                CGST = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()),
                                SGST = Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString()),
                                IGST = Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value.ToString()),
                                discount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[7].Value.ToString()),
                                amount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[8].Value.ToString()),
                                per = dataGridView1.Rows[i].Cells[9].Value.ToString(),
                                HSN = dataGridView1.Rows[i].Cells[10].Value.ToString(),
                                Color = dataGridView1.Rows[i].Cells[11].Value.ToString(),
                                Size = dataGridView1.Rows[i].Cells[12].Value.ToString(),
                                Msg = dataGridView1.Rows[i].Cells[13].Value.ToString(),
                                Exp = dataGridView1.Rows[i].Cells[14].Value.ToString(),
                                MRP = Convert.ToDecimal(dataGridView1.Rows[i].Cells[15].Value.ToString()),
                                Sale = Convert.ToDecimal(dataGridView1.Rows[i].Cells[16].Value.ToString()),
                                ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[17].Value.ToString()),
                              


                            };

                            await MainEngine_.Add(dynamic, "insertitems");


                        }
                    }


                    var smodel = new
                    {

                        BillID = inv,
                        partiname = cust_name.Text,
                        items = inv,
                        sub_total = Convert.ToDecimal(totalamt.Text) - Convert.ToDecimal(tax.Text)-Convert.ToDecimal(discount_.Text),
                        perdis = Convert.ToDecimal(discount_.Text),
                        discount = Convert.ToDecimal(discount_.Text),
                        other = Convert.ToDecimal(tax.Text),

                        TotalBill = Convert.ToDecimal(totalamt.Text),
                        billdate = DateTime.Now,
                        GSTIN = textBox1.Text,
                        CGST = Convert.ToDecimal(cgsto.Text),
                        SGST = Convert.ToDecimal(sgsto.Text),
                        IGST = Convert.ToDecimal(igsto.Text),
                        TAX = TAX_TYPE.Text,
                        spid = Convert.ToInt64(supid.Text)
                         
                    };



                    await MainEngine_.Add(smodel, "InsertBill");


                    MessageBox.Show("Bill has been saved!", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dataGridView1.Rows.Clear();
   
                    amt.Text = "";

                    discount_.Text = "0";
                    tax.Text = "0";
                    totalamt.Text = "0";
                    sumqty.Text = "0";
                    items.Text = "0";
                    invnum.Text = GenerateInvoice();

                }
                catch (Exception ex)
                 { 
                }



                cal.discounts = 0;
                cal.sub_amount = 0;
                cal.total = 0;

                textBox2.Text = GenerateInvoice();
            }
            else
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            srs = 1;
            cal.discounts = 0;
            cal.sub_amount = 0;
            cal.total = 0;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {

                if (dgview.Rows.Count > 0 && dgview.Visible == true)
                {

                    if (checkBox1.Checked)
                    {

                        ID.Text = dgview.Rows[0].Cells[0].Value.ToString();
                        desc.Text = dgview.Rows[0].Cells[1].Value.ToString();
                        amst = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);
                        if (TAX_TYPE.SelectedIndex == 0 || TAX_TYPE.Text == "GST")
                        {
                            gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[4].Value.ToString());

                        }
                        else
                        {
                            gsttext.Text = "0";
                        }
                        barcode.Text = dgview.Rows[0].Cells[6].Value.ToString();
                        sale_price.Text = dgview.Rows[0].Cells[7].Value.ToString();


                        MRP.Text = dgview.Rows[0].Cells[5].Value.ToString();
                        v = amst;
                        q.Text = "1";
                        disc.Text = "0";

                    }
                    else
                    {

                        int rowindex = dgview.SelectedRows[0].Index;
                        ID.Text = dgview.Rows[rowindex].Cells[0].Value.ToString();
                        desc.Text = dgview.Rows[rowindex].Cells[1].Value.ToString();
                        amst = Convert.ToDecimal(dgview.Rows[rowindex].Cells[3].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[rowindex].Cells[3].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);
                        if (TAX_TYPE.SelectedIndex == 0 || TAX_TYPE.Text == "GST")
                        {
                            gsttext.Text = Convert.ToString(dgview.Rows[rowindex].Cells[4].Value.ToString());

                        }
                        else
                        {
                            gsttext.Text = "0";
                        }
                        v = amst;
                        barcode.Text = dgview.Rows[rowindex].Cells[6].Value.ToString();

                        MRP.Text = dgview.Rows[rowindex].Cells[5].Value.ToString();
                        sale_price.Text = dgview.Rows[rowindex].Cells[7].Value.ToString();
                        q.Text = "1";
                        disc.Text = "0";

                    }
                }


                if (dgview.Visible == true)
                {
                    dgview.Visible = false;
                }




                // Handle the Enter key
                if (amt.Text != "")
                {
                    if (disc.Focused == true)
                    {
                        button3.PerformClick();
                        return true;
                    }

                    


                    if (button2.Focused == true)
                    {
                        isenter = true;

                    }



                }
                SendKeys.Send("{TAB}"); // Simulate Tab key press
                return true; // Mark the key as handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
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


            this.dgview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgviewcol1, this.dgviewcol2, this.dgviewcol3 });
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

        //Two Column
        void Search(int LX, int LY, int DW, int DH, string ColName, string ColSize)
        {
            this.dgview.Location = new System.Drawing.Point(LX, LY);
            this.dgview.Size = new System.Drawing.Size(DW, DH);

            string[] ClSize = ColSize.Split(',');
            string[] ClName = ColName.Split(',');

            // Ensure the DataGridView has exactly 6 columns
            if (dgview.Columns.Count < 8)
            {
                while (dgview.Columns.Count < 8)
                {
                    dgview.Columns.Add(new DataGridViewTextBoxColumn());
                }
            }
            else if (dgview.Columns.Count > 8)
            {
                while (dgview.Columns.Count > 8)
                {
                    dgview.Columns.RemoveAt(dgview.Columns.Count - 1);
                }
            }

            // Set column sizes
            for (int i = 0; i < 8; i++)
            {
                if (i < ClSize.Length && int.Parse(ClSize[i]) != 0)
                {
                    dgview.Columns[i].Width = int.Parse(ClSize[i]);
                }
                else
                {
                    dgview.Columns[i].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            // Set column names and visibility
            for (int i = 0; i < 8; i++)
            {
                if (i < ClName.Length)
                {
                    this.dgview.Columns[i].HeaderText = ClName[i];
                }
                else
                {
                    this.dgview.Columns[i].HeaderText = $"Column {i + 1}";
                }
                this.dgview.Columns[i].Visible = true;
            }
        }

        
        private void Bill_Load(object sender, EventArgs e)
        {

            srs = 0;
            cust_name.Focus();
            Search();
            textBox2.Text = GenerateInvoice();
            List<string> units = Task.Run(() => MainEngine_.GetDataScript<string>("select Unit from Unit")).Result.ToList();


            // Update the ComboBox on the UI thread
            unitss.Items.Clear();
            unitss.Items.AddRange(units.Distinct().ToArray());
            unitss.SelectedIndex = 0;
        }
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private bool isDownKeyPressed = false;
        private bool isenter = false;
        private decimal okdiscount;
        private decimal CGSTm = 0;
        private decimal SGSTm = 0;
        private decimal IGSTm=0;
        private decimal Taxes = 0;
        private bool isState = false;

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
            Search(9, 250, 430, 200, "ID,Item Name,Stock,Price,GST,MRP,Barcode,Sale_Price", "100,100,100,100,100,100,100,100");


            using (SqlConnection con = new SqlConnection(MainEngine_.SERVER_PATH))
            {
                con.Open();
                string query = "SELECT TOP(40) ID,Item_Name, Item_price, GST FROM Mechanic WHERE Mechanic_Name = @CustName AND Item_Name LIKE @SearchTerm + '%'";
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
            Search(9, 250, 430, 200, "ID,Item Name,Stock,Price,GST,MRP,Barcode,Sale_Price", "100,100,100,100,100,100,100,100");


            using (SqlConnection con = new SqlConnection(MainEngine_.SERVER_PATH))
            {
                con.Open();
                string query = "SELECT TOP(40) ID,ITEM_NAME, STOCK, COST_PRICE, GST,MRP,BARCODE,SALE_PRICE FROM Product_Item WHERE ITEM_NAME LIKE @SearchTerm + '%'";
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

                    dgview.Rows[n].Cells[0].Value = row[0].ToString(); //item name 0 id

                    dgview.Rows[n].Cells[1].Value = row[1].ToString(); //item name 0
                    dgview.Rows[n].Cells[2].Value = row[2].ToString(); //stock 1 
                    dgview.Rows[n].Cells[3].Value = row[3].ToString();  // price 2 
                    dgview.Rows[n].Cells[4].Value = row[4].ToString(); // GST column 3

                    dgview.Rows[n].Cells[5].Value = row[5].ToString(); // GST column 3
                    dgview.Rows[n].Cells[6].Value = row[6].ToString(); // GST column 3
                    dgview.Rows[n].Cells[7].Value = row[7].ToString(); // GST column 3





                }
            }
        }





        private async void pictureBox2_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                srs = srs + 1;


                dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, disc.Text, amt.Text);


                string cals = cal.SubTotal(amt.Text).ToString();
                totalamt.Text = cals;

                if (tax.Text != "") { totalamt.Text = (decimal.Parse(totalamt.Text) + decimal.Parse(tax.Text)).ToString(); }

                //cal.total = decimal.Parse(textBox1.Text);
                StoreRoom.ClearData(panel3.Controls);
                amt.Text = "";

            }
            catch (Exception ex)
            {
            }
        }

        private void desc_SelectedIndexChanged(object sender, EventArgs e)
        {
            rete.Text = Convert.ToString(r);
            amt.Text = Convert.ToString(amst);
            v = amst;


        }

        private void perdisc_TextChanged(object sender, EventArgs e)
        {
            PerDisc();
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

            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                PerDisc();
            }
            catch (Exception ex)
            {

            }
        }

        private async void button3_ClickAsync(object sender, EventArgs e)
        {



            
            string exist = MainEngine_.GetDataScript<int>("select ID from Product_Item where ID = " + ID.Text + "").FirstOrDefault().ToString();

            if (exist == "")
            {
                try
                {
                    string price = "";
                    if (sale_price.Text == "")
                    {
                        price = (Convert.ToDecimal(rete.Text) * 0.20m + Convert.ToDecimal(rete.Text)).ToString();
                    }
                    else
                    {
                        price = sale_price.Text;
                    }

                    var product = new
                    {
                        PR_CODE = "PR_001",
                        ITEM_NAME = desc.Text.ToString(),
                        GST = gsttext.Text,
                        STOCK = 0,
                        UNIT = unitss.Text,
                        BARCODE = "",
                        SALE_PRICE = Convert.ToDecimal(price),
                        COST_PRICE = Convert.ToDecimal(rete.Text),
                        pr_ACCOUNT = "NO",
                        pr_DESCRIPTION = "Add from purches",
                        pr_COSTPRICE = 0,
                        IDATE = DateTime.Now.ToString(),
                        DESCRIPTION = "ADD from  Purche",
                        ACCOUNT = "purches",
                        USER_N = "ME",
                        pic = "NO",
                        MRP = MRP.Text,
                        CGST = cgst.Text,
                        SGST = sgst.Text,
                        IGST = igst.Text,
                        HSN = hsn.Text,
                        Color = Colors.Text,
                        Size = size.Text,
                        TAX_TYPE = TAX_TYPE.Text


                    };

                    ProductAdd prd = new ProductAdd();
                    await prd.AddProduct(product);

                }
                catch (Exception ex)
                {

                }
            }

            decimal originalAmount = decimal.Parse(rete.Text) * decimal.Parse(q.Text);
            if (MainEngine_.GetDataScript<dynamic>("select ID from Product_Item where  ID = " + ID.Text + "").Count > 0)
            {

              
                decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);


                decimal discount = originalAmount * (discountPercentage / 100);




                senddiscount = senddiscount + discount;
                if (Convert.ToDecimal(q.Text) < MainEngine_.GetDataScript<int>("select Stock from Product_Item where ID = " + ID.Text + " ").FirstOrDefault())
                { 


                    try
                    {
                        Crys cr = new Crys();
                        if (dataGridView1.Rows.Count == 1)
                        {

                            srs = srs + 1;

                            decimal finalAmount = (originalAmount + (originalAmount * Convert.ToDecimal(gsttext.Text) / 100)) - discount;
                            amt.Text = finalAmount.ToString();

                            dataGridView1.Rows.Add(srs,desc.Text, q.Text, rete.Text,cgst.Text, sgst.Text, igst.Text,disc.Text, amt.Text,unitss.Text,hsn.Text,Colors.Text,size.Text,msgdate.Text,expdate.Text,MRP.Text,sale_price.Text,ID.Text);

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
                                sgsto.Text = SGSTm.ToString(); // Formatting to two decimal places
                                igsto.Text = IGSTm.ToString(); // Formatting to two decimal places



                                tax.Text = Taxes.ToString();



                            }


                            string cals = cal.SubTotal(amt.Text).ToString();

                            totalamt.Text = (decimal.Parse(cals)).ToString();


               

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
                            dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, cgst.Text, sgst.Text, igst.Text, disc.Text, amt.Text, unitss.Text, hsn.Text, Colors.Text, size.Text, msgdate.Text, expdate.Text, MRP.Text, sale_price.Text, ID.Text);
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
                                   sgsto.Text = SGSTm.ToString(); // Formatting to two decimal places
                                    igsto.Text = IGSTm.ToString(); // Formatting to two decimal places


                                    tax.Text = Taxes.ToString();


                                }

                                string cals = cal.SubTotal(amt.Text).ToString();
                                totalamt.Text = (decimal.Parse(cals)).ToString();
                     



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
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = i + 1;
                        srs = i + 1;
                    }

                }

                else
                {
                   


                    try
                    {
                        Crys cr = new Crys();
                        if (dataGridView1.Rows.Count == 1)
                        {

                            srs = srs + 1;

                            dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, cgst.Text, sgst.Text, igst.Text, disc.Text, amt.Text, unitss.Text, hsn.Text, Colors.Text, size.Text, msgdate.Text, expdate.Text, MRP.Text, sale_price.Text, ID.Text); TotalQty();
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
                                sgsto.Text = SGSTm.ToString(); // Formatting to two decimal places
                                igsto.Text = IGSTm.ToString(); // Formatting to two decimal places


                                tax.Text = Taxes.ToString();

                            }
                            string cals = cal.SubTotal(amt.Text).ToString();
                            totalamt.Text = (decimal.Parse(cals)).ToString();

 

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
                           
                                srs = srs + 1;

                            dataGridView1.Rows.Add(srs, desc.Text, q.Text, rete.Text, cgst.Text, sgst.Text, igst.Text, disc.Text, amt.Text, unitss.Text, hsn.Text, Colors.Text, size.Text, msgdate.Text, expdate.Text, MRP.Text, sale_price.Text, ID.Text); TotalQty();
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
                                    sgsto.Text = SGSTm.ToString(); // Formatting to two decimal places
                                    igsto.Text = IGSTm.ToString(); // Formatting to two decimal places


                                    tax.Text = Taxes.ToString();


                                }

                                string cals = cal.SubTotal(amt.Text).ToString();
                                totalamt.Text = (decimal.Parse(cals)).ToString();
                              



                                //cal.total = decimal.Parse(totalamt.Text);
                                StoreRoom.ClearData(panel3.Controls);
                                amt.Text = "";

                                this.BeginInvoke(new Action(() =>
                                {
                                    desc.Focus();
                                }));
                          
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
                    ID.Text = "";
                }
            }

        }

            private void ItemsCount()
        {

            int count = dataGridView1.Rows.Count - 1;

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



        private void dgview_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Retrieve the clicked row
                DataGridViewRow selectedRow = dgview.Rows[e.RowIndex];

                // Access the row's data, for example:
                string ids = selectedRow.Cells[0].Value.ToString(); // Assuming column 1 contains strings
                string cellValue1 = selectedRow.Cells[1].Value.ToString(); // Assuming column 1 contains strings

                string cellValue2 = selectedRow.Cells[3].Value.ToString(); // Assuming column 3 contains strings
                string barcode = selectedRow.Cells[6].Value.ToString(); // Assuming column scontains strings
                string mrp = selectedRow.Cells[5].Value.ToString(); // Assuming column scontains strings
                string sale_prices= Convert.ToString(selectedRow.Cells[7].Value.ToString());


                // Do something with the values or the selected row
                // Example: Display the values in a message box

                ID.Text = ids;
                desc.Text = cellValue1;
                amst = Convert.ToDecimal(cellValue2);
                r = Convert.ToDecimal(cellValue2);
                rete.Text = Convert.ToString(r);
                amt.Text = Convert.ToString(amst);
                sale_price.Text = sale_prices;
                v = amst;
                this.barcode.Text = barcode;
                MRP.Text = mrp;
                if (TAX_TYPE.SelectedIndex == 0 || TAX_TYPE.Text == "GST")
                {
                    gsttext.Text = selectedRow.Cells[4].Value.ToString();

                }
                else
                {
                    gsttext.Text = "0";
                }
                q.Text = "1";
                disc.Text = "0";
                desc.Focus();
                dgview.Visible = false;

            }
        }
        private void discountsu()
        {

            int total = 0;





            discount_.Text = senddiscount.ToString();

        }


        private void dataGridView1_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            Parties li = new Parties();
            li.Show();

        }

        private void button2_Leave(object sender, EventArgs e)
        {
            try
            {
                button2.PerformClick();
            }
            catch (Exception ex)
            {

            }
        }

        private void desc_TextUpdate(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void desc_TextChanged_1(object sender, EventArgs e)
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
                            desc.Text = dgview.Rows[nextIndex].Cells[1].Value.ToString();
                            ID.Text = dgview.Rows[nextIndex].Cells[0].Value.ToString();
                            amst = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[3].Value.ToString());
                            r = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[3].Value.ToString());
                            rete.Text = Convert.ToString(r);
                            amt.Text = Convert.ToString(amst);
                            if(TAX_TYPE.SelectedIndex == 0 || TAX_TYPE.Text == "GST")
                            {
                                gsttext.Text = Convert.ToString(dgview.Rows[nextIndex].Cells[4].Value.ToString());

                            }
                            else
                            {
                                gsttext.Text = "0";
                            }
                            sale_price.Text = dgview.Rows[nextIndex].Cells[7].Value.ToString();

                            v = amst;
                            q.Text = "1";
                            disc.Text = "0";
                        }
                    }
                    else
                    {
                        desc.Text = dgview.Rows[0].Cells[1].Value.ToString();
                        ID.Text = dgview.Rows[0].Cells[0].Value.ToString();
                        amst = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);

                        if (TAX_TYPE.SelectedIndex == 0 || TAX_TYPE.Text == "GST")
                        {
                            gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[4].Value.ToString());

                        }
                        else
                        {
                            gsttext.Text = "0";
                        }
                        v = amst;
                        sale_price.Text = dgview.Rows[0].Cells[7].Value.ToString();

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
                            desc.Text = dgview.Rows[nextIndex].Cells[1].Value.ToString();
                            ID.Text = dgview.Rows[nextIndex].Cells[0].Value.ToString();
                            amst = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[3].Value.ToString());
                            r = Convert.ToDecimal(dgview.Rows[nextIndex].Cells[3].Value.ToString());
                            rete.Text = Convert.ToString(r);
                            if (TAX_TYPE.SelectedIndex == 0 || TAX_TYPE.Text == "GST")
                            {
                                gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[4].Value.ToString());

                            }
                            else
                            {
                                gsttext.Text = "0";
                            }
                            amt.Text = Convert.ToString(amst);
                            v = amst;
                            sale_price.Text = dgview.Rows[nextIndex].Cells[7].Value.ToString();
                            q.Text = "1";
                            disc.Text = "0";
                        }
                    }
                    else
                    {
                        desc.Text = dgview.Rows[0].Cells[1].Value.ToString();
                        ID.Text = dgview.Rows[0].Cells[0].Value.ToString();
                        amst = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());
                        r = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());
                        rete.Text = Convert.ToString(r);
                        amt.Text = Convert.ToString(amst);
                        v = amst;
                        if (TAX_TYPE.SelectedIndex == 0 || TAX_TYPE.Text == "GST")
                        {
                            gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[4].Value.ToString());

                        }
                        else
                        {
                            gsttext.Text = "0";
                        }
                        sale_price.Text = dgview.Rows[0].Cells[7].Value.ToString();
                        q.Text = "1";
                        disc.Text = "0";
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (checkBox1.Checked && dgview.Rows.Count > 0)
                {
                    desc.Text = dgview.Rows[0].Cells[1].Value.ToString();
                    amst = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());
                    ID.Text = dgview.Rows[0].Cells[0].Value.ToString();
                    r = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());

                    rete.Text = Convert.ToString(r);
                    if (TAX_TYPE.SelectedIndex == 0 || TAX_TYPE.Text == "GST")
                    {
                        gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[4].Value.ToString());

                    }
                    else
                    {
                        gsttext.Text = "0";
                    }
                    sale_price.Text = Convert.ToString(dgview.Rows[0].Cells[7].Value.ToString());
                    amt.Text = Convert.ToString(amst);
                    v = amst;

                    q.Text = "1";
                    disc.Text = "0";
                    dgview.Visible = false;

                }
                else
                {
                    desc.Text = dgview.Rows[0].Cells[1].Value.ToString();
                    ID.Text = dgview.Rows[0].Cells[0].Value.ToString();
                    amst = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());
                    r = Convert.ToDecimal(dgview.Rows[0].Cells[3].Value.ToString());
                    rete.Text = Convert.ToString(r);
                    if (TAX_TYPE.SelectedIndex == 0 || TAX_TYPE.Text == "GST")
                    {
                        gsttext.Text = Convert.ToString(dgview.Rows[0].Cells[4].Value.ToString());

                    }
                    else
                    {
                        gsttext.Text = "0";
                    }
                    amt.Text = Convert.ToString(amst);
                    sale_price.Text = dgview.Rows[0].Cells[7].Value.ToString();
                    v = amst;

                    q.Text = "1";
                    disc.Text = "0";
                    dgview.Visible = false;

                }


            }
        }

        private void button2_Leave_1(object sender, EventArgs e)
        {
            if (isenter == true) // Check if the active control is not the button itself
            {
                button2.PerformClick();
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
                decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);
                decimal originalAmount = (decimal.Parse(rete.Text) * decimal.Parse(q.Text));
                originalAmount = originalAmount + (originalAmount * decimal.Parse(gsttext.Text) / 100);

                decimal discountedAmount = originalAmount - (discountPercentage / 100m * originalAmount);

                okdiscount = (discountPercentage / 100m * originalAmount);

                amt.Text = discountedAmount.ToString();


            }
            catch (Exception ex)
            {

            }

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gsttext_TextChanged(object sender, EventArgs e)
        {
            try
            {


                try
                {

                    decimal a = Convert.ToDecimal(gsttext.Text) / 2;

                    if (isState == true)
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
                amt.Text = (StoreRoom.GST(total, Convert.ToInt32(gsttext.Text)) - okdiscount).ToString();








            }
            catch (Exception ex)
            {

            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
           if(checkBox3.Checked)
            {
                panel25.Visible = true;
                panel26.Visible = true;


            }
            else
            {
                panel25.Visible = false;
                panel26.Visible = false;

            }
        }

        private void parties_Click(object sender, EventArgs e)
        {
            try
            {
                View c = new View("Supplier Ledger", "GetReport");
                c.Show();


                //
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void supid_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void supid_TextChanged(object sender, EventArgs e)
        {

            try
            {

            string add = MainEngine_.GetDataScript<string>("select address from Parties Where ID = " + supid.Text + "").FirstOrDefault();
            
            string name = MainEngine_.GetDataScript<string>("select company from Parties Where ID = " + supid.Text + "").FirstOrDefault();
            string mob = MainEngine_.GetDataScript<string>("select partimobile from Parties Where ID = " + supid.Text + "").FirstOrDefault();

                cust_name.Text = name;
            textBox3.Text = add;
            cust_name.Items.Clear();

                textBox5.Text = mob;
            StoreRoom sr = new StoreRoom();

                // Retrieve the customer name from the textbox
                string customerName = cust_name.Text;

                // Get the balance for the customer
                decimal? balance = sr.Balance(customerName);

                // Ensure balance is not null and format it properly
                decimal balanceValue = balance ?? 0m;
                string formattedBalance = balanceValue.ToString("C2", new CultureInfo("hi-IN"));

                blc.Text = $"{formattedBalance} CR";


                state.Text = MainEngine_.GetDataScript<string>("select state from Parties Where ID = " + supid.Text + "").FirstOrDefault();

            textBox1.Text = MainEngine_.GetDataScript<string>("select GSTIN from Parties Where ID = " + supid.Text + "").FirstOrDefault();

            if (StoreRoom.GetState() == state.Text)
            {
                isState = true;
            }
            else
            {
                isState = false;

            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }
    }
}