using damage_assessment_api.RequestModels;
using damage_assessment_api.ResponseModels;
using damage_assessment_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace damage_assessment_api.Controllers
{
    [ApiController]
    [Route("damage-assessments")]
    public class DamageAssessmentController : ControllerBase
    {
        private readonly ILogger<DamageAssessmentController> _logger;
        private readonly IDamageAssessmentService _damageService;
        private readonly IAzureBlobStorageService _blobStorageService;
        public DamageAssessmentController(IDamageAssessmentService damageService, IAzureBlobStorageService  blobStorageService, ILogger<DamageAssessmentController> logger)
        {
            _logger = logger;
            _damageService = damageService;
            _blobStorageService = blobStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssessments([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var assessments = await _damageService.GetAllAsync(pageNumber, pageSize);
            return Ok(assessments);
        }

        [HttpPost]
        [RequestSizeLimit(1000 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1000 * 1024 * 1024)]
        public async Task<IActionResult> CreateAssessment([FromForm] DamageAssessmentRequest assessment)
        {
            if (assessment == null)
                return BadRequest(ResponseResult<string>.Fail("Invalid request"));

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ResponseResult<List<string>>.Fail("Invalid input data", errors));
            }
                      
            var fileUrl = await _blobStorageService.UploadFileAsync(assessment.File);
            if (string.IsNullOrEmpty(fileUrl))
                return BadRequest(ResponseResult<string>.Fail("File upload failed"));

            var response = await _damageService.CreateAsync(assessment, fileUrl);

            if(!response.success || response.assessmentResponse is null)
                return BadRequest(ResponseResult<string>.Fail("Error in storing data."));

            return Ok(ResponseResult<DamageAssessmentResponse>.Success(response.assessmentResponse, "Assessment created successfully"));
        }

        [HttpGet("getblob")]
        public async Task<IActionResult> GetBlobUrl([FromQuery] string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
                return BadRequest(ResponseResult<string>.Fail("Invalid request"));

            var sasUrl = await _blobStorageService.GenerateSasTokenAsync(fileName);
            return Ok(sasUrl);
        }

    }
}
