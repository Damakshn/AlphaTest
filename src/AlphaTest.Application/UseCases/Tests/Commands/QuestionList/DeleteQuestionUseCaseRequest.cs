using System;

namespace AlphaTest.Application.UseCases.Tests.Commands.QuestionList
{
    public class DeleteQuestionUseCaseRequest : EditQuestionListUseCaseRequest
    {
        public DeleteQuestionUseCaseRequest(Guid testID, Guid questionID)
        {
            TestID = testID;
            QuestionID = questionID;
        }

        public Guid QuestionID { get; private set; }
    }
}
