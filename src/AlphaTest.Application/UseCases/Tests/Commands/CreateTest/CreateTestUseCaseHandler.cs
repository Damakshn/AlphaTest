using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Core.Tests;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Auth.UserManagement;

namespace AlphaTest.Application.UseCases.Tests.Commands.CreateTest
{
    public class CreateTestUseCaseHandler : UseCaseHandlerBase<CreateTestUseCaseRequest, Guid>
    {
        public CreateTestUseCaseHandler(AlphaTestContext db) : base(db) { }

        public override Task<Guid> Handle(CreateTestUseCaseRequest request, CancellationToken cancellationToken)
        {
            AppUser author = _db.Users.Aggregates().Where(user => user.UserName == request.Username).FirstOrDefault();
            if (author is null)
            {
                throw new AlphaTestApplicationException($"Пользователь {request.Username} не зарегистрирован в системе.");
            }
            bool testAlreadyExists = _db.Tests
                .Aggregates()
                .Any(test => test.Title == request.Title && test.Topic == request.Topic);
            Test test = new(request.Title, request.Topic, author.ID, testAlreadyExists);
            _db.Tests.Add(test);
            _db.SaveChanges();
            return Task.FromResult(test.ID);
        }
    }
}
