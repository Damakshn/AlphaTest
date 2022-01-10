using System;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Groups.Commands.ExcluedStudent
{
    public class ExcludeStudentUseCaseRequest : IUseCaseRequest
    {
        public ExcludeStudentUseCaseRequest(Guid groupID, Guid studentID)
        {
            GroupID = groupID;
            StudentID = studentID;
        }

        public Guid GroupID { get; private set; }

        public Guid StudentID { get; private set; }
    }
}
