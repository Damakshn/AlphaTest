using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Application.UseCases.Tests.Commands.CreateTest
{
    public class CreateTestUseCaseHandler : UseCaseHandlerBase<CreateTestUseCaseRequest, Guid>
    {
        public CreateTestUseCaseHandler(AlphaTestContext db) : base(db) { }

        public override async Task<Guid> Handle(CreateTestUseCaseRequest request, CancellationToken cancellationToken)
        {
            AlphaTestUser author = await _db.Users.Aggregates().FindByID(request.AuthorID);
            if (author is null)
            {
                throw new AlphaTestApplicationException($"Пользователь {request.AuthorID} не зарегистрирован в системе.");
            }
            bool testAlreadyExists = _db.Tests
                .Aggregates()
                .Any(test => test.Title == request.Title && test.Topic == request.Topic);
            Test test = new(request.Title, request.Topic, author.Id, testAlreadyExists);
            _db.Tests.Add(test);
            _db.SaveChanges();
            return test.ID;
        }
    }
}
