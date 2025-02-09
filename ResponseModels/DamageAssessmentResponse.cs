namespace damage_assessment_api.ResponseModels
{
    public class DamageAssessmentResponse
    {
        /// <summary>
        /// MongoDB Document ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// // Associated Claim ID
        /// </summary>
        public string ClaimId { get; set; }
        /// <summary>
        /// // Name of the uploaded file
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// // URL to access the uploaded file (Azure Blob Storage)
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// // Damage description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// // Upload time in UTC
        /// </summary>
        public DateTime UploadTimestamp { get; set; }

        /// <summary>
        /// Stores additional file details
        /// </summary>
        public FileMetaDataResponse FileMetaData { get; set; } = new FileMetaDataResponse();

    }
}
