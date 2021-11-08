using System;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptSingleChoiceAnswerUseCaseRequest : AcceptAnswerUseCaseRequest
    {
        public AcceptSingleChoiceAnswerUseCaseRequest(Guid examinationID, Guid studentID, Guid questionID, Guid rightOptionID) 
            : base(examinationID, studentID, questionID)
        {
            RightOptionID = rightOptionID;
        }

        public Guid RightOptionID { get; private set; }
    }
}
