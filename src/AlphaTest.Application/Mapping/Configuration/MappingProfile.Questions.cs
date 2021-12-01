using AlphaTest.Application.Models.Questions;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Application.Mapping.Configuration
{
    public partial class MappingProfile
    {
        private void CreateMappingForQuestions()
        {
            CreateMap<Question, QuestionListItemDto>()
                .ForMember(dto => dto.Score, opt => opt.MapFrom(q => q.Score.Value))
                .ForMember(dto => dto.QuestionType, opt => opt.MapFrom(q => q.GetType().Name))
                .ForMember(dto => dto.Text, opt => opt.MapFrom(q => q.Text.Value));
        }
    }
}
