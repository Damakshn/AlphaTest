using System;
using System.Collections.Generic;
using AlphaTest.Application.Models.Users;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Groups.Queries.StudentsInGroup
{
    public class StudentsListQuery : IUseCaseRequest<List<StudentListItemDto>>
    {
        public StudentsListQuery(Guid groupID)
        {
            GroupID = groupID;
        }

        public Guid GroupID { get; private set; }
    }
}
