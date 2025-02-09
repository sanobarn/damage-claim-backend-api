namespace damage_assessment_api.ResponseModels
{
    public class FileMetaDataResponse
    {
        /// <summary>
        /// MIME type of the file (e.g., "image/png", "application/pdf")
        /// </summary>
        public string Type { get; set; } = String.Empty;
        /// <summary>
        /// Size of the file
        /// </summary>
        public int Size { get; set; }
    }
}
