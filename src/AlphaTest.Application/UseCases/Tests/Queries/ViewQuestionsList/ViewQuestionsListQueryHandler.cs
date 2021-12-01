using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.Models.Questions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AutoMapper;

namespace AlphaTest.Application.UseCases.Tests.Queries.ViewQuestionsList
{
    public class ViewQuestionsListQueryHandler : UseCaseReportingHandlerBase<ViewQuestionsListQuery, List<QuestionListItemDto>>
    {
        public ViewQuestionsListQueryHandler(AlphaTestContext db, IMapper mapper) : base(db, mapper) { }

        public override async Task<List<QuestionListItemDto>> Handle(ViewQuestionsListQuery request, CancellationToken cancellationToken)
        {
            List<Question> questions = await _db.Questions.Aggregates().FilterByTest(request.TestID).ToListAsync();
            return _mapper.Map<List<Question>, List<QuestionListItemDto>>(questions);

        }
    }
}
