﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.Models.Tests;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Application.UseCases.Tests.Queries.TestsList
{
    public class TestsListQueryHandler : UseCaseReportingHandlerBase<TestsListQuery, List<TestsListItemDto>>
    {
        public TestsListQueryHandler(AlphaTestContext db, IMapper mapper) : base(db, mapper) { }

        public override async Task<List<TestsListItemDto>> Handle(TestsListQuery request, CancellationToken cancellationToken)
        {
            var query = from test in _db.Tests
                .FilterByTitle(request.Title)
                .FilterByTopic(request.Topic)
                .FilterByAuthor(request.Author)
                .FilterByStatusList(request.Statuses)
                .FilterByAuthorOrContributor(request.AuthorOrContributor)
                .Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize)
            join author in _db.Users on test.AuthorID equals author.Id
            select new TestsListItemDto()
            {
                ID = test.ID,
                Title = test.Title,
                Topic = test.Topic,
                Version = test.Version,
                Author = $"{author.LastName} {author.FirstName} {author.MiddleName}",
                AuthorEmail = author.Email,
                Status = test.Status.Name
            };
            return await query.ToListAsync();
        }
    }
}
