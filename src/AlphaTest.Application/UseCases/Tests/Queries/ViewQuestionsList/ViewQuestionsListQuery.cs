using System;
using System.Collections.Generic;
using AlphaTest.Application.Models.Questions;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Tests.Queries.ViewQuestionsList
{
    public class ViewQuestionsListQuery : IUseCaseRequest<List<QuestionListItemDto>>
    {
        public Guid TestID { get; private set; }

        public ViewQuestionsListQuery(Guid testID)
        {
            TestID = testID;
        }
    }
}
