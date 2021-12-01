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

            CreateMap<QuestionOption, QuestionOptionDto>();

            CreateMap<Question, QuestionInfoDto>()
                .ForMember(dto => dto.Score, opt => opt.MapFrom(q => q.Score.Value))
                .ForMember(dto => dto.Text, opt => opt.MapFrom(q => q.Text.Value))
                .ForMember(dto => dto.RightAnswer, opt => opt.MapFrom((q, dest) =>
                {
                    if (q is QuestionWithNumericAnswer questionWithNumericAnswer)
                    {
                        return questionWithNumericAnswer.RightAnswer.ToString();
                    }
                    if (q is QuestionWithTextualAnswer questionWithTextualAnswer)
                    {
                        return questionWithTextualAnswer.RightAnswer;
                    }
                    return null;
                }))
                .ForMember(dto => dto.QuestionType, opt => opt.MapFrom(q => q.GetType().Name))
                .ForMember(dto => dto.Options, opt => opt.Condition(q => q is QuestionWithChoices))
                .ForMember(dto => dto.Options, opt => opt.MapFrom(q => (q as QuestionWithChoices).Options));
        }
    }
}
