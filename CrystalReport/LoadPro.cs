using Back_Dr.Backup;
using Back_Dr.Sale;
using Dapper;
using Dr.Sale.Components;
using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport
{
    public partial class LoadPro : Form
    {
  
        private int progress;
        private string lics;
        private string getstr;
        private string send;
        private StoreRoom trs;
        private int st;

        public LoadPro( )
        {
            InitializeComponent();
             
        }


        public void CopyFile()
        {
            string sourcePath = Application.StartupPath; // Path to your database files
            string destinationFolder1 = @"C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\";
            string destinationFolder2 = @"C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\";

            string databaseFileName = @"\drsale.mdf"; // Change this to your database file name

            // Check if the first destination folder exists
            if (Directory.Exists(destinationFolder1))
            {
                // Copy files to the first destination folder
                CopyDatabaseFiles(sourcePath + databaseFileName, destinationFolder1 + databaseFileName);
                send = destinationFolder1;
            }
            else if (Directory.Exists(destinationFolder2))
            {
                // Copy files to the second destination folder
                CopyDatabaseFiles(sourcePath + databaseFileName, destinationFolder2 + databaseFileName);
                send = destinationFolder2;

            }
            else
            {
                // If neither folder exists, handle as needed
             
            
            }
             
        }   




        private void CopyDatabaseFiles(string sourceFile, string destinationFile)
        {
            if (!File.Exists(destinationFile))
            {
                File.Copy(sourceFile, destinationFile);
            }
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


        void UpdateProgress(string processName, int progress)
        {
            progressBar1.Value = progress;
            label2.Text = processName;
            // Ensure the form updates immediately
            Application.DoEvents();
        }


        private async void LoadPro_Load(object sender, EventArgs e)
        {
            
                 

                try
                {
                
                    lics = MainEngine_.GetDataScript<string>("select Licence from OwnerInformation").FirstOrDefault();
                    getstr = MainEngine_.GetDataScript<string>("select ShopName From OwnerInformation").FirstOrDefault();
                                }
            
                catch (Exception exw)
                {
                
                    try
                    {
                        st = await trs.DoesDatabaseExistAsync("drsale");

                    }
                    catch (Exception exs)
                    {


                        st = 1;

                    }
                    if (st == 1)
                    {


                        CopyFile();
                        SqlConnection sc = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=master;Integrated Security=True;");
                        sc.Open();

                        string databaseName = "drsale";
                        string query = $@"
USE master;
CREATE DATABASE drsale
ON (FILENAME = '{send + databaseName}.mdf')
FOR ATTACH;
";


                        sc.Execute(query);
                        sc.Close();
                       
                    }
                       
                }


            if (CheackKey(lics) == true)
            {

                LICTO.Text = getstr;

            }

            UpdateProgress("Loading Data from Database....", 20);
             timer1.Enabled = true;
              timer1.Interval = 1;
            UpdateProgress("Welcome to Mr.Retaile", 100);






        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            progress += 1;
            if (progress >= 100)
            {
                timer1.Enabled = false;
                timer1.Stop();

            }
            progressBar1.Value = progress;
            if (progress == 100)
            {
                timer1.Stop();

            }



            if (progressBar1.Value == progressBar1.Maximum)
            {



                string amir = ComputerInfo.GetComputerId();



                //dataGridView1.Rows[dataGridView1.CurrentRow.Index
                string i = ComputerInfo.GetComputerId();

                
                    if (lics == "" || lics == null || String.IsNullOrEmpty(lics))
                    {
                        View fs = new View("Owner","Calll over");
                        fs.Show();
                        this.Hide();
                   


                    }
                    else
                    {
                        Login fs1 = new Login();
                        fs1.Show();

                        this.Hide();

                    }

            }
        }

        private void LoadPro_DoubleClick(object sender, EventArgs e)
        {

            if (lics == "" || lics == null || String.IsNullOrEmpty(lics))
            {
                View fs = new View("Owner","Another");
                fs.Show();
                this.Hide();

                timer1.Stop();

            }
            else
            {
                Login fs1 = new Login();
                fs1.Show();
                this.Hide();
                timer1.Stop();


            }
        }
    }
}
