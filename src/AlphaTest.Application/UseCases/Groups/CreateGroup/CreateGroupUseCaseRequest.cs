using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Groups.CreateGroup
{
    public class CreateGroupUseCaseRequest : IUseCaseRequest<Guid>
    {
        public CreateGroupUseCaseRequest(string name, DateTime beginDate, DateTime endDate, Guid? curatorID)
        {
            Name = name;
            BeginDate = beginDate;
            EndDate = endDate;
            CuratorID = curatorID;
        }

        public string Name { get; private set; }

        public DateTime BeginDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public Guid? CuratorID { get; private set; }
    }
}
