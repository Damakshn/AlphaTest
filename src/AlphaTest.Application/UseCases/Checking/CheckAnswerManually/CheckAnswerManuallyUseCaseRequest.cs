using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Checking;
using System;

namespace AlphaTest.Application.UseCases.Checking.CheckAnswerManually
{
    public class CheckAnswerManuallyUseCaseRequest : IUseCaseRequest
    {
        public CheckAnswerManuallyUseCaseRequest(Guid answerID, Guid teacherID, decimal score, int checkResultTypeID)
        {
            AnswerID = answerID;
            TeacherID = teacherID;
            Score = score;
            CheckResultType = CheckResultType.ParseFromID(checkResultTypeID);
        }

        public Guid AnswerID { get; private set; }

        public Guid TeacherID { get; private set; }

        public decimal Score { get; private set; }

        public CheckResultType CheckResultType { get; private set; }
    }
}
