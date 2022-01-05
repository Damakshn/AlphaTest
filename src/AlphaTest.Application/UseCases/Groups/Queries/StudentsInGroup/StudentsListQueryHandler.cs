﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.Models.Users;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Application.UseCases.Groups.Queries.StudentsInGroup
{
    public class StudentsListQueryHandler : UseCaseReportingHandlerBase<StudentsListQuery, List<StudentListItemDto>>
    {
        public StudentsListQueryHandler(AlphaTestContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public override async Task<List<StudentListItemDto>> Handle(StudentsListQuery request, CancellationToken cancellationToken)
        {
            var students = await _db.Users.StudiesInGroup(request.GroupID, _db).ToListAsync();
            return _mapper.Map<List<AlphaTestUser>, List<StudentListItemDto>>(students);
        }
    }
}
