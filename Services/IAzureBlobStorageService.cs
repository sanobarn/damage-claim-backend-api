namespace damage_assessment_api.Services
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<string> GenerateSasTokenAsync(string blobName, int expiryMinutes = 60);
    }
}
