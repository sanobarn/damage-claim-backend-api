using AutoMapper;
using damage_assessment_api.Collections;
using damage_assessment_api.Entity;
using damage_assessment_api.ResponseModels;

namespace damage_assessment_api.Profiles
{
    
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DamageAssessment, DamageAssessmentResponse>();
            CreateMap<FileMetadata, FileMetaDataResponse>();
        }
    }
}
