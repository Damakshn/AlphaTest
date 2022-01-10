using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Groups.Commands.AddStudent
{
    public class AddStudentUseCaseRequest : IUseCaseRequest
    {
        public AddStudentUseCaseRequest(Guid groupID, Guid studentID)
        {
            GroupID = groupID;
            StudentID = studentID;
        }

        public Guid GroupID { get; private set; }

        public Guid StudentID { get; private set; }
    }
}
