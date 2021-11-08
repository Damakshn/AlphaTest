using System;
using System.Collections.Generic;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptMultiChoiceAnswerUseCaseRequest : AcceptAnswerUseCaseRequest
    {
        public AcceptMultiChoiceAnswerUseCaseRequest(Guid examinationID, Guid studentID, Guid questionID, List<Guid> rightOptions) 
            : base(examinationID, studentID, questionID)
        {
            RightOptions = rightOptions;
        }

        public List<Guid> RightOptions { get; private set; }
    }
}
