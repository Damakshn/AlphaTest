using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Groups.Commands.EditGroupInfo
{
    public class EditGroupInfoUseCaseRequest : IUseCaseRequest
    {
        public EditGroupInfoUseCaseRequest(Guid groupID, string name, DateTime beginDate, DateTime endDate)
        {
            GroupID = groupID;
            Name = name;
            BeginDate = beginDate;
            EndDate = endDate;
        }

        public Guid GroupID { get; private set; }

        public string Name { get; private set; }

        public DateTime BeginDate { get; private set; }

        public DateTime EndDate { get; private set; }
    }
}
