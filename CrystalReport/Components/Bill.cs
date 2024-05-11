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
                                qty = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()),
                                rate = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString()),
                                discount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()),
                                amount = Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString()),
                                Bill = inv
                            };

                            await MainEngine_.Add(dynamic, "insertitems");


                        }
                    }


                    var smodel = new
                    {

                        BillID = inv,
                        partiname = cust_name.Text,
                        items = inv,
                        sub_total = Convert.ToDecimal(totalamt.Text),
                        perdis = Convert.ToDecimal(discount_.Text),
                        discount = Convert.ToDecimal(discount_.Text),
                        other = Convert.ToDecimal(tax.Text),
                        TotalBill = Convert.ToDecimal(totalamt.Text),
                        billdate = DateTime.Now


                    };



                    await MainEngine_.Add(smodel, "InsertBill");


                    MessageBox.Show("Bill has been saved!", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dataGridView1.Rows.Clear();
                    StoreRoom.ClearData(this.Controls);
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
                    MessageBox.Show(ex.Message);
                }



                cal.discounts = 0;
                cal.sub_amount = 0;
                cal.total = 0;
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
                // Handle the Enter key
                if (amt.Text != "")
                {
                    if (disc.Focused == true)
                    {
                        button3.PerformClick();
                        return true;
                    }

                    if (dgview.Rows.Count > 0 && dgview.Visible == true)
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

        private void Bill_Load(object sender, EventArgs e)
        {

            srs = 0;
            cust_name.Focus();
            Search();
            textBox2.Text = GenerateInvoice();
        }
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private bool isDownKeyPressed = false;
        private bool isenter = false;

        private async void desc_TextChanged(object sender, EventArgs e)
        {

            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                amt.Text = v.ToString();
                decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);
                decimal originalAmount = decimal.Parse(amt.Text);

                decimal discountedAmount = originalAmount - (discountPercentage / 100m * originalAmount);

                amt.Text = discountedAmount.ToString();
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



            string exist = MainEngine_.GetDataScript<string>("select ITEM_NAME from Product_Item where ITEM_NAME = '" + desc.Text + "'").FirstOrDefault();

            if (exist != "")
            {
                try
                {

                    var product = new
                    {
                        PR_CODE = "PR_001",
                        ITEM_NAME = desc.Text.ToString(),
                        TYPE_TAX = 1,
                        STOCK = Convert.ToInt32(q.Text),
                        UNIT = "PCS",
                        BARCODE = "",
                        SALE_PRICE = (Convert.ToDecimal(rete.Text) * 0.20m + Convert.ToDecimal(rete.Text)),
                        COST_PRICE = Convert.ToDecimal(rete.Text),
                        pr_ACCOUNT = "NO",
                        pr_DESCRIPTION = "Add from purches",
                        pr_COSTPRICE = 0        ,
                        IDATE = DateTime.Now.ToString(),
                        DESCRIPTION = "ADD from  Purche",
                        ACCOUNT = "purches",
                        USER_N = "ME",
                        pic = "NO"

                    };

                    ProductAdd prd = new ProductAdd();
                    await prd.AddProduct(product);

                }
                catch (Exception ex)
                {

                }
            }

            decimal discountPercentage = string.IsNullOrEmpty(disc.Text) ? 0m : decimal.Parse(disc.Text);
            decimal originalAmount = decimal.Parse(amt.Text);

            decimal discount = originalAmount * (discountPercentage / 100);

            senddiscount += discount;





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

        private void cust_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string add = MainEngine_.GetDataScript<string>("select address from Parties Where pname = '" + cust_name.Text + "'").FirstOrDefault();
                textBox3.Text = add;
                cust_name.Items.Clear();
            }
            catch (Exception ex)
            {

            }
        }

        private void cust_name_TextChanged(object sender, EventArgs e)
        {
            try
            {


                List<dynamic> list = MainEngine_.GetDataScript<dynamic>("select company from Parties where pname LIKE '%" + cust_name.Text + "%' ").ToList();

                foreach (var item in list)
                {
                    cust_name.Items.Add(item.company);

                }

            }
            catch (Exception ex)
            {
            }
        }

        private void dataGridView1_CellClickAsync(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
                {
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
                        srs = i + 1;
                    }
                }
            }
            catch (Exception)
            {


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
            else if (e.KeyCode == Keys.Enter)
            {
                dgview.Visible = false;


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

            }
            catch (Exception ex)
            {

            }

        }
    }
}