using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Back_Dr.Sale.Models;
using System.Text;
using System.Threading.Tasks;
using Back_Dr.Sale.Inventory;
using System.Windows.Forms;
using Back_Dr.Sale;
using ZXing;
using Dr.Sale.Components;
using System.IO;

namespace CrystalReport.Components
{
    public partial class AddItem : UserControl
    {

           public int tax;
        public decimal saleprice = 0;
        public decimal cost_price = 0;
        public decimal pr_cost_price = 0;
        public int pcout = 0;
       private byte[] bpic;
        public string pics = "";
        private byte[] barcodeset;
        public string barcodeget = "";
        public AddItem()
        {
            InitializeComponent();
          
        }
 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
            if (keyData == Keys.Enter)
            {
                // Handle the Enter key
                if(button1.Focused == true)
                {
                    isenter = true;

                }
                    SendKeys.Send("{TAB}"); // Simulate Tab key press
                return true; // Mark the key as handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private int ste = MainEngine_.GetData<int>("GetBarcode").First();
        private bool isenter = false;

        private async void AddItem_Load(object sender, EventArgs e)
        {
            
            name_item.Focus();
            try
            {

                List<dynamic> model = new List<dynamic>();
                unit.Items.Clear();
                await Task.Run(()=>  model = MainEngine_.GetData<dynamic>("spGetProducts").ToList());

                unit.Items.AddRange(MainEngine_.GetData<string>("spUnitSelect").ToArray());
                pcout = model.Count();
                textBox1.Text = pcout.ToString();
              

                
                BarcodeWriter write = new BarcodeWriter()
                {
                    Format = BarcodeFormat.CODE_128

                };
                pictureBox2.Image = write.Write(ste.ToString());


                barcodeget = ste.ToString();
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message)  
                    ;
            }
             

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        { 
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you Want to Add New Item ?", "Item Add", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);


                if (result == DialogResult.OK)

                {

                    try
                    {
                        if (good.Checked)
                        {
                            tax = 0;

                        }
                        else if (service.Checked)
                        {
                            tax = 1;

                        }
                        else
                        {
                            tax = -1;
                            //MessageBox.Show("Please Cheack your Service type","Work Type",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                        }

                        if (sprice.Text != "")
                        {
                            saleprice = decimal.Parse(sprice.Text);

                        }
                        else
                        {
                            saleprice = 0;

                        }
                        if (cprice.Text != "")
                        {
                            cost_price = decimal.Parse(cprice.Text);

                        }
                        else
                        {
                            cost_price = 0;
                        }

                  
                

                    if (checkBox3.Checked)
                    {
                        barcodeget = textBox2.Text;
                    }
                    else
                    {
                        barcodeget = ste.ToString();
                    }
                    string gstText = gst.Text.Trim('%'); // Remove the percentage symbol


                    var product = new
                    {
                        PR_CODE = "PR_001",
                        ITEM_NAME = name_item.Text.ToString(),
                        TYPE_TAX = Convert.ToInt32(gstText),
                        STOCK = Convert.ToInt32(stock.Text),
                        UNIT = unit.Text,
                        BARCODE = barcodeget,
                        SALE_PRICE = saleprice,
                        COST_PRICE = cost_price,
                        pr_ACCOUNT = acc.Text,
                        pr_DESCRIPTION = dss.Text,
                        pr_COSTPRICE = pr_cost_price,
                        IDATE = DateTime.Now.ToString(),
                        DESCRIPTION = ds.Text,
                        ACCOUNT = ac.Text,
                        USER_N = "Amir Feroz",
                        pic = pics,
                        MRP = decimal.Parse(MRP.Text),
                        CGST = decimal.Parse(cgst.Text),
                        SGST = decimal.Parse(sgst.Text),
                        IGST = decimal.Parse(igst.Text),
                        HSN = hsn.Text,
                        Msg = msgdate.Value.ToString("yyyy/MM/dd"),
                        Exp = expdate.Value.ToString("yyyy/MM/dd"),
                        Color = Colors.Text,
                        Size = size.Text,
                    };


                        ProductAdd prd = new ProductAdd();
                        await prd.AddProduct(product);

                        MessageBox.Show("Succefully Add product!", "Add Product", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    StoreRoom.ClearData(this.Controls);
                    BarcodeWriter write = new BarcodeWriter()
                    {
                        Format = BarcodeFormat.CODE_128

                    };
                    pictureBox2.Image = write.Write(ste.ToString());

                    pcout += 1;
                    textBox1.Text = pcout.ToString();
                   

                     
                 }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
        
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

   
        private void AddItem_KeyPress(object sender, KeyPressEventArgs e)
        {

          

        }

        private void name_item_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void AddItem_Enter(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           StoreRoom.ClearData(this.Controls);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.Controls.Clear();

            Itemlist list = new Itemlist();
            list.Dock = DockStyle.Fill;
            this.Controls.Add(list);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
        public byte[] saveimage(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
             
        }

        private void button4_Click(object sender, EventArgs e)
        {


         
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked)
            {
                textBox2.Enabled = true;


            }
            else
            {
                textBox2.Enabled = false;
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog1.Title = "Select an Excel File";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;

                    // Display the filename in a label
                    file_name.Text = System.IO.Path.GetFileName(filePath);

                    // Start importing the file data
                    ImportExcelData(filePath);
                    var model = MainEngine_.GetData<dynamic>("spGetProducts").ToList();
                    textBox1.Text = model.Count.ToString();
                   
                }

            }
            catch (Exception ex)
            {
                 
            }
        }

        private async void ImportExcelData(string filePath)
        {
            var dynamic = new
            {
                path = filePath
            };
            file_name.Text = "Wait...";

           DataTable tb =  StoreRoom.AddExcel(filePath);

         MainEngine_.BulkInsertDataTable(tb);

     
            file_name.Text = "Record has been saved";

            MessageBox.Show("Succefully Products Add!","Product Add?",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            try
            {

                if (isenter == true) // Check if the active control is not the button itself
                {

                    button1.PerformClick();
                }
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                MessageBox.Show("Call key");
                button1.PerformClick();

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
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

        private void gst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string gstText = gst.Text.Trim('%'); // Remove the percentage symbol
                decimal ts = Convert.ToDecimal(gstText); // Convert to decimal


                ts = ts / 2;

                cgst.Text = ts.ToString();
                sgst.Text = ts.ToString();

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                if(checkBox1.Checked)
                {
                    panel10.Visible = true;

                }
                else
                {
                    panel10.Visible = false;

                }

            }
            catch (Exception ex)
            { 
            }
        }
    }
}
