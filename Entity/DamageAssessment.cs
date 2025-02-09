using damage_assessment_api.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace damage_assessment_api.Collections
{
    public class DamageAssessment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("ClaimId")]
        public string ClaimId { get; set; } = null!;

        [BsonElement("FileName")]
        public string FileName { get; set; } = null!;

        [BsonElement("FileUrl")]
        public string FileUrl { get; set; } = null!;

        [BsonElement("Description")]
        public string? Description { get; set; }

        [BsonElement("UploadTimestamp")]
        public DateTime UploadTimestamp { get; set; } = DateTime.UtcNow;

        [BsonElement("FileMetadata")]
        public FileMetadata FileMetadata { get; set; }
    }
}
