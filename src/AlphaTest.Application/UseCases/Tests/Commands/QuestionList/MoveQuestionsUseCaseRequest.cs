using System;
using System.Collections.Generic;

namespace AlphaTest.Application.UseCases.Tests.Commands.QuestionList
{
    public class MoveQuestionsUseCaseRequest : EditQuestionListUseCaseRequest
    {

        public MoveQuestionsUseCaseRequest(Guid testID, List<(Guid QuestionID, uint Position)> questionsOrder)
        {
            TestID = testID;
            QuestionsOrder = questionsOrder;
        }
        public List<(Guid QuestionID, uint Position)> QuestionsOrder { get; private set; }
    }
}
