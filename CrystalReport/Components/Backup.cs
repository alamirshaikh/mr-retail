using Back_Dr.Backup;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class Backup : UserControl
    {
        public Backup()
        {
            InitializeComponent();
        }

        private void Backup_Load(object sender, EventArgs e)
        {
            try
            {
                shop.Text = StoreRoom.ShopNames();
                owner.Text = StoreRoom.OwnerName();

            }
            catch (Exception ex)
            {
                 
            }
        }


        public static async Task Backupss()
        {
            try
            {
                bool isConnected = await BackUpServices.IsInternetAvailable();


                GenerateScript scr = new GenerateScript();

                scr.Generate();



                if (isConnected)
                {
                    await BackUpServices.UploadFileToGoogleDrive(StoreRoom.MobileNumber());


                }
                else
                {
                    MessageBox.Show("NO INternet ,It looks like you're not connected to the internet right now. Please connect to the internet to ensure your data is backed up automatically. For now, you can manually back up your data. Thank you for your patience!");


                }


            }
            catch (Exception ex)
            {

            }
        }

        void UpdateProgress(string processName, int progress)
        {
            progressBar1.Value = progress;
            label9.Text = processName;
            // Ensure the form updates immediately
            Application.DoEvents();
        }
        private async Task  button2s_Click(object sender, EventArgs e)
        {
            
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("if you want to Upload Data?Or backup","Upload Data!" ,MessageBoxButtons.YesNo);
                if(result == DialogResult.Yes)
                {
                    File.Delete("C:\\SYSTEM\\backup.txt");
                    UpdateProgress("Generating Scripts.....", 10);
                    await Task.Run(async () => await Backupss());

                    UpdateProgress("Successfully Uploaded!", 100);


                }
                else
                {

                }
             

            }
            catch (Exception ex)
            {

            }
        }
    }
}
