using System;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Groups.Commands.AssignCurator
{
    public class AssignCuratorUseCaseRequest : IUseCaseRequest
    {
        public AssignCuratorUseCaseRequest(Guid groupID, Guid curatorID)
        {
            GroupID = groupID;
            CuratorID = curatorID;
        }

        public Guid GroupID { get; private set; }

        public Guid CuratorID { get; private set; }
    }
}
