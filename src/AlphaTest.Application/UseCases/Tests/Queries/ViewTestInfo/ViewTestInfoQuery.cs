using System;
using AlphaTest.Application.Models.Tests;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Queries.ViewTestInfo
{
    public class ViewTestInfoQuery : IUseCaseRequest<TestInfo>
    {
        public Guid TestID { get; set; }
    }
}
