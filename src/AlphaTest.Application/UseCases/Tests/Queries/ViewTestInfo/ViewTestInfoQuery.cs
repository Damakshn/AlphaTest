using System;
using AlphaTest.Application.Models.Tests;
using MediatR;

namespace AlphaTest.Application.UseCases.Tests.Queries.ViewTestInfo
{
    public class ViewTestInfoQuery : IRequest<TestInfo>
    {
        public Guid TestID { get; set; }
    }
}
