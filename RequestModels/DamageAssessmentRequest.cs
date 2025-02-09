using damage_assessment_api.Shared;
using System.ComponentModel.DataAnnotations;

namespace damage_assessment_api.RequestModels
{
    public class DamageAssessmentRequest
    {
        [Required]
        public string ClaimId { get; set; }  // Unique Claim ID

        [MaxLength(1000)]
        public string Description { get; set; }

        [ExtensionValidator(new string[] { ".jpg", ".jpeg", ".png", ".pdf" })]
        [Required]
        public IFormFile File { get; set; } 
    }
}
