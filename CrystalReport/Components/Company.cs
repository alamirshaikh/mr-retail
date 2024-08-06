 
using Back_Dr.Sale;
using Dr.Sale.Components;
using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class Company : UserControl
    {
        private string getshopimage;
        private string imagePath;
        private string savePath;

        public byte ProductCode { get; private set; }

        public Company()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // Handle the Enter key
                SendKeys.Send("{TAB}"); // Simulate Tab key press
                return true; // Mark the key as handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool CheackKey(string macid)
        {
            KeyManager km = new KeyManager(macid);
            string productkey = macid;
            if (km.ValidKey(ref macid))
            {
                KeyValuesClass kv = new KeyValuesClass();
                LicenseInfo lics = new LicenseInfo();

                if (km.DisassembleKey(productkey, ref kv))
                {
                    lics.ProductKey = productkey;
                    lics.FullName = "Mr.Sale";

                }
                km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lics);
                return true;
            }
            else
            {
                return false;
            }
        }
        private static Image ResizeImage(Image image, int newWidth, int newHeight)
        {
            Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return resizedImage;
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (CheackKey(lic.Text) == true)
            {
                try
                {
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "company.png");

                    if (File.Exists(savePath))
                    {
                        try
                        {
                            File.Delete(savePath);
                        }
                        catch (IOException ioEx)
                        {
                            MessageBox.Show("File is in use: " + ioEx.Message);
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred while deleting the file: " + ex.Message);
                            return;
                        }
                    }

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        string extension = Path.GetExtension(imagePath).ToLower();

                        if (extension != ".png")
                        {
                            using (Image image = Image.FromFile(imagePath))
                            {
                                int originalWidth = image.Width;
                                int originalHeight = image.Height;
                                int newWidth = 220; // Example: New width
                                int newHeight = 192; // Example: New height

                                using (Image resizedImage = ResizeImage(image, newWidth, newHeight))
                                {
                                    resizedImage.Save(savePath, ImageFormat.Png);
                                }
                            }
                        }
                        else
                        {
                            int newWidth = 224; // Example: New width
                            int newHeight = 192; // Example: New height
                            using (Image image = Image.FromFile(imagePath))
                            {
                                using (Image resizedImage = ResizeImage(image, newWidth, newHeight))
                                {
                                    resizedImage.Save(savePath, ImageFormat.Png);
                                }
                            }

                            try
                            {
                                File.Copy(imagePath, savePath, true);
                            }
                            catch (IOException ioEx)
                            {
                                MessageBox.Show("File is in use: " + ioEx.Message);
                                return;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                                return;
                            }
                        }
                    }

                    var para = new
                    {
                        ownerName = ownername.Text,
                        shopName = shopname.Text,
                        shopPhone = workphone.Text,
                        shopMobile = mobile.Text,
                        city = city.Text,
                        state = state.Text,
                        businessAddress = address.Text,
                        gstin = gstin.Text,
                        typeB = typeb.Text,
                        gst = decimal.Parse(gst.Text),
                        sgst = decimal.Parse(sgst.Text),
                        cgst = decimal.Parse(cgst.Text),
                        hsncode = "0012",
                        panInfo = pan.Text,
                        email = email.Text,
                        bankName = bankname.Text,
                        accountName = acname.Text,
                        accountNumber = acnumber.Text,
                        ifsc = ifc.Text,
                        licence = lic.Text,
                        logo = savePath
                    };

                    int c = MainEngine_.GetDataScript<int>("select Count(OwnerID) from OwnerInformation").FirstOrDefault();

                    await MainEngine_.Add(para, "InsOwner");

                    MessageBox.Show("Your Information Has Been Saved!", "Owner Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (c < 1 || c == null)
                    {
                        // Additional actions if needed
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                MessageBox.Show("Invalid Licence.", "Licence", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            StoreRoom.ClearData(this.Controls);
        }
        private  async void Company_Load(object sender, EventArgs e)
        {
          await Task.Run(()=>  macid.Text = ComputerInfo.GetComputerId());
        }




        public string GetKEy()
        {

            KeyManager km = new KeyManager(macid.Text);
            KeyValuesClass kv = new KeyValuesClass();
            string produckey = string.Empty;




            kv = new KeyValuesClass()
            {
                Type = LicenseType.FULL,
                Header = Convert.ToByte(9),
                Footer = Convert.ToByte(6),
                ProductCode = (byte)ProductCode,
                Edition = Edition.ENTERPRISE,
                Version = 1
            };
            if (!km.GenerateKey(kv, ref produckey))
                return "ERROR";


            return produckey;
        }
    
        private async void button3_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                using (SmtpClient client = new SmtpClient("127.0.0.1", 1025))
                {
                    client.EnableSsl = false; // ProtonMail Bridge might not use SSL/TLS

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("mrretail@protonmail.com"); // Your ProtonMail address
                    message.To.Add("amirshaikhsystem@gmail.com"); // Recipient's email
                    message.Subject = "MR.SALE Licence key";
                    message.Body = GetKEy();

                    client.Send(message);
                }
            }
            catch (SmtpException smtpEx)
            {
                MessageBox.Show(smtpEx.Message, "Error sending email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void button1_Leave(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Save?", "Company Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                button1.PerformClick();

            }
            else
            {

            }
        }

        private void gst_TextChanged(object sender, EventArgs e)
        {
            try
            {

                decimal dem = Convert.ToDecimal(gst.Text);

                sgst.Text = (dem / 2).ToString();
                cgst.Text = (dem / 2).ToString();


            }
            catch (Exception ex)
            {
                 
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    
                    Task<List<dynamic>> ownerTask = Task.Run(() => MainEngine_.GetDataScript<dynamic>("SELECT * FROM OwnerInformation"));
                    Task<List<dynamic>> taxTask = Task.Run(() => MainEngine_.GetDataScript<dynamic>("SELECT * FROM Tax"));
                    Task<List<dynamic>> bankTask = Task.Run(() => MainEngine_.GetDataScript<dynamic>("SELECT * FROM Bank"));

                    await Task.WhenAll(ownerTask, taxTask, bankTask);

                    var owner = await ownerTask;
                    var tax = await taxTask;
                    var bank = await bankTask;

                    if (owner.Count > 0)
                    {
                        var ownerData = owner[0];
                        ownername.Text = ownerData.OwnerName;
                        shopname.Text = ownerData.ShopName;
                        workphone.Text = ownerData.ShopPhone;
                        mobile.Text = ownerData.ShopMobile;
                        city.Text = ownerData.City;
                        state.Text = ownerData.State;
                        address.Text = ownerData.BusinessAddress;
                        lic.Text = ownerData.Licence;
                        email.Text = ownerData.email;
                   

                        var bankData = bank[0];
                        pan.Text = bankData.PANInfo;
                        bankname.Text = bankData.BankName;
                        acname.Text = bankData.AccountName;
                        acnumber.Text = bankData.AccountNumber;
                        ifc.Text = bankData.IFSC;

                        var taxData = tax[0];
                        gstin.Text = taxData.GSTIN;
                        typeb.Text = taxData.TypeB;
                        gst.Text = taxData.GST.ToString(); // Convert decimal to string
                        sgst.Text = taxData.SGST.ToString(); // Convert decimal to string
                        cgst.Text = taxData.CGST.ToString(); // Convert decimal to string

                        
                        // Convert byte array to Image
                        string path = Application.StartupPath;
                        string[] files = Directory.GetFiles(path, "company.png");

                        if (files.Length > 0)
                        {
                            try
                            {
                                pictureBox1.Image = Image.FromFile(files[0]);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error loading image: " + ex.Message);
                            }
                        }
                        else
                        {
                           
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                gst.Text = "0";
                cgst.Text = "0";
                sgst.Text = "0";


            }
            else
            {
                gst.Text = "";
                cgst.Text = "";
                sgst.Text = "";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                // image filters


                

                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {



                    string path = Application.StartupPath;

                    pictureBox1.Image = new Bitmap(open.FileName);

                    // Image file path
                    label25.Text = open.FileName;
                    // display image in picture box
                    byte[] imageBytes = File.ReadAllBytes(open.FileName);

                    // Convert byte array to base64 string
                    string base64Image = Convert.ToBase64String(imageBytes);

                    // Store the base64 string in getshopimage

                    imagePath = open.FileName;
                    getshopimage = base64Image;
                }

            }
            catch (Exception ex)
            {
                 
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Password pas = new Password();
                pas.Show();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
