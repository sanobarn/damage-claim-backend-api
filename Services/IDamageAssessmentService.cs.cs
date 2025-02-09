using damage_assessment_api.RequestModels;
using damage_assessment_api.ResponseModels;

namespace damage_assessment_api.Services
{
    public interface IDamageAssessmentService
    {
        Task<List<DamageAssessmentResponse>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<(bool success, DamageAssessmentResponse? assessmentResponse)> CreateAsync(DamageAssessmentRequest assessment, string fileUrl);
    }
}
