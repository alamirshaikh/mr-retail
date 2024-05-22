using Back_Dr.Backup;
using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MRRetailSystemB
{
    class Program
    {

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // Constants for the ShowWindow function
        private const int SW_MINIMIZE = 6;

        // Import the GetConsoleWindow function to get the handle of the console window
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();


        public static async Task Backup()
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
                    Console.WriteLine("NO INternet ,It looks like you're not connected to the internet right now. Please connect to the internet to ensure your data is backed up automatically. For now, you can manually back up your data. Thank you for your patience!");  


                }


            }
            catch (Exception ex)
            {

            }
        }

        static async  Task Main(string[] args)
        {
            IntPtr consoleWindow = GetConsoleWindow();
            ShowWindow(consoleWindow, SW_MINIMIZE);
            await Backup();

            File.Delete("C:\\SYSTEM\\backup.txt");

            // Exit the application
            Environment.Exit(0);

        }
    }
}
