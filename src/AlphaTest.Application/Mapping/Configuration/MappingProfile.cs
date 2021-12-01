using AutoMapper;

namespace AlphaTest.Application.Mapping.Configuration
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMappingForTestAndTestSettings();
            CreateMappingForQuestions();
        }
    }
}
