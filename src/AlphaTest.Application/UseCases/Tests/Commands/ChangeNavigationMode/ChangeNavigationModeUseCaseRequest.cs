using System;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeNavigationMode
{
    public class ChangeNavigationModeUseCaseRequest : IUseCaseRequest
    {
        public ChangeNavigationModeUseCaseRequest(Guid testID, int modeID)
        {
            TestID = testID;
            NavigationMode = NavigationMode.ParseFromID(modeID);
        }

        public Guid TestID { get; private set; }

        public NavigationMode NavigationMode { get; private set; }
    }
}
