using System;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.Models.Questions;


namespace AlphaTest.Application.UseCases.Tests.Queries.ViewQuestionInfo
{
    public class ViewQuestionInfoQuery : IUseCaseRequest<QuestionInfoDto>
    {
        public Guid TestID { get; private set; }

        public Guid QuestionID { get; private set; }

        public ViewQuestionInfoQuery(Guid testID, Guid questionID)
        {
            TestID = testID;
            QuestionID = questionID;
        }
    }
}
