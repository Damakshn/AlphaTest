using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Groups.Commands.DisbandGroup
{
    public class DisbandGroupUseCaseRequest : IUseCaseRequest
    {
        public DisbandGroupUseCaseRequest(Guid groupID)
        {
            GroupID = groupID;
        }

        public Guid GroupID { get; private set; }
    }
}
