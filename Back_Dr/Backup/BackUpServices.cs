using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;

namespace Back_Dr.Backup
{
   public class BackUpServices
    {
        public static async Task<bool> IsInternetAvailable()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Setting a timeout to ensure it doesn't hang indefinitely
                    client.Timeout = TimeSpan.FromSeconds(5);

                    // Sending a request to a reliable website
                    HttpResponseMessage response = await client.GetAsync("http://www.google.com");

                    // Checking if the response status code indicates success
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                // If any exception occurs, assume the internet is not available
                return false;
            }
        }
        public static async Task UploadFileToGoogleDrive(string userName)
        {
            // Path to the Service Account's JSON key file
            string serviceAccountKeyFilePath =@"D:\New folder (2)\mr-retail\Back_Dr\Backup\sercret.json";
 

            // Define the scopes required by the API.
            string[] scopes = { DriveService.Scope.DriveFile };

            // Authenticate using the service account
            GoogleCredential credential;
            using (var stream = new FileStream(serviceAccountKeyFilePath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(scopes);
            }

            // Create the Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API .NET Quickstart",

            });

            // ID of the folder where the file will be uploaded
            string folderId = "1HzK9nc2G9aThEB5aifcTNICDLMA0al6e";

            // Metadata for the file
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = $"{userName}-{DateTime.Now}.txt",
                Parents = new[] { folderId } // Add the folder ID here
            };

            string filePath = @"C:\\SYSTEM\\backup.txt";

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "text/plain");
                request.Fields = "id";
                var progress = await request.UploadAsync();

                if (progress.Status == UploadStatus.Failed)
                {
                    Console.WriteLine($"Error uploading file: {progress.Exception}");
                }
            }

            var file = request.ResponseBody;
            Console.WriteLine($"File ID: {file.Id}");
        }





    }
}
