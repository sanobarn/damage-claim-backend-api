using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace damage_assessment_api.Services
{
    public class AzureBlobStorageService: IAzureBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;
        private readonly ILogger<DamageAssessmentService> _logger;
        private readonly string _containerName;
       public AzureBlobStorageService(IConfiguration configuration, ILogger<DamageAssessmentService> logger)
        {
            string connectionString = configuration["AzureBlobStorage:ConnectionString"];
            _containerName = configuration["AzureBlobStorage:ContainerName"];
            _containerClient = new BlobContainerClient(connectionString, _containerName);
            _logger = logger;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                return null;

                var blobClient = _containerClient.GetBlobClient(file.FileName);
                using (var stream = file.OpenReadStream())
                {
                    var blobInfo = await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
                }
                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"MongoDB Insert Error: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GenerateSasTokenAsync(string blobName, int expiryMinutes = 60)
        {           
            var blobClient = _containerClient.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync())
            {
                throw new Exception("Blob not found.");
            }

            // Generate SAS Token
            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = _containerName,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(expiryMinutes)
            };
           
            sasBuilder.SetPermissions(BlobSasPermissions.All);

            var sasToken = blobClient.GenerateSasUri(sasBuilder);
            return sasToken.ToString();
        }
    }
}
