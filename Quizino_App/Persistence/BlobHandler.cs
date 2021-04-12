using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Persistence
{
    internal class BlobHandler
    {
        public async Task SaveData(string content)
        {
            string localPath = "./data/";
            string fileName = "questions-" + Guid.NewGuid().ToString() + ".json";
            string localFilePath = Path.Combine(localPath, fileName);

            try
            {
                // Write text to the file
                await File.WriteAllTextAsync(localFilePath, content);

                var containerClient = await GetBlobContainerClient().ConfigureAwait(false);
                // Get a reference to a blob
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

                // Open the file and upload its data
                using FileStream uploadFileStream = File.OpenRead(localFilePath);
                await blobClient.UploadAsync(uploadFileStream, true);
                uploadFileStream.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to save data to blob storage:  {0}", e.Message);
            }
        }

        private async Task<BlobContainerClient> GetBlobContainerClient()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=bks197;AccountKey=Bat2Yl6pdfENHrxZ1KoHfAjUQfswdsFWGT+HiliKFlO9B4iX+EXUz95RLKnOAB74TiZjDX/NVtWNlippkuBFtw==;EndpointSuffix=core.windows.net";
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //Create a unique name for the container
            string containerName = "test-blobs-" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            return await blobServiceClient.CreateBlobContainerAsync(containerName);
        }
    }
}
