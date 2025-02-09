using AutoMapper;
using damage_assessment_api.Collections;
using damage_assessment_api.RequestModels;
using damage_assessment_api.ResponseModels;
using MongoDB.Driver;

namespace damage_assessment_api.Services
{
    public class DamageAssessmentService : IDamageAssessmentService
    {
        private readonly IMongoCollection<DamageAssessment> _assessments;
        private readonly ILogger<DamageAssessmentService> _logger;
        private readonly IMapper _mapper;
        public DamageAssessmentService(IConfiguration configuration, ILogger<DamageAssessmentService> logger, IMapper mapper)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
            _assessments = database.GetCollection<DamageAssessment>(configuration["MongoDB:CollectionName"]);
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<DamageAssessmentResponse>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var documents =  await _assessments
                .Find(_ => true) 
                .Skip((pageNumber - 1) * pageSize) 
                .Limit(pageSize) 
                .ToListAsync();

            return _mapper.Map<List<DamageAssessmentResponse>>(documents);
        }


        public async Task<(bool success, DamageAssessmentResponse? assessmentResponse)> CreateAsync(DamageAssessmentRequest assessment, string fileUrl)
        {
            try
            {
                var document = new DamageAssessment
                {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    ClaimId = assessment.ClaimId,
                    FileName = assessment.File.FileName,
                    FileUrl = fileUrl,
                    Description = assessment.Description,
                    UploadTimestamp = DateTime.UtcNow,
                    FileMetadata=new Entity.FileMetadata() { Size=assessment.File.Length, Type=assessment.File.ContentType }
                };
                await _assessments.InsertOneAsync(document);

                var response = _mapper.Map<DamageAssessmentResponse>(document);

                return (true, response);
            }
            catch (MongoException ex)
            {
                _logger.LogError($"MongoDB Insert Error: {ex.Message}");
                return (false, default);
            }
        }
    }
}
