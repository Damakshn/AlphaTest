using System;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Groups.Commands.UnsetCurator
{
    public class UnsetCuratorUseCaseRequest : IUseCaseRequest
    {
        public UnsetCuratorUseCaseRequest(Guid groupID)
        {
            GroupID = groupID;
        }

        public Guid GroupID { get; private set; }
    }
}
