using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.Models.Questions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Queries.ViewQuestionInfo
{
    public class ViewQuestionInfoQueryHandler : UseCaseReportingHandlerBase<ViewQuestionInfoQuery, QuestionInfoDto>
    {
        public ViewQuestionInfoQueryHandler(AlphaTestContext db, IMapper mapper) : base(db, mapper) { }

        public override async Task<QuestionInfoDto> Handle(ViewQuestionInfoQuery request, CancellationToken cancellationToken)
        {
            Question question = await _db.Questions.Aggregates().FilterByTest(request.TestID).FindByID(request.QuestionID);
            return _mapper.Map<QuestionInfoDto>(question);
        }
    }
}
