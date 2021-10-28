using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddContributor
{
    public class AddContributorUseCaseRequest : IUseCaseRequest
    {
        public AddContributorUseCaseRequest(Guid testID, Guid teacherID)
        {
            TestID = testID;
            TeacherID = teacherID;
        }

        public Guid TestID { get; private set; }

        public Guid TeacherID { get; private set; }
    }
}
