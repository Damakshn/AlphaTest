using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Core.Tests;
using AlphaTest.Infrastructure.Auth;
using AlphaTest.Application.Exceptions;

namespace AlphaTest.Application.UseCases.Tests.Commands.CreateTest
{
    public class CreateTestUseCaseHandler : IRequestHandler<CreateTestUseCaseRequest, Guid>
    {
        private AlphaTestContext _db;
        public CreateTestUseCaseHandler(AlphaTestContext db)
        {
            _db = db;
        }

        public Task<Guid> Handle(CreateTestUseCaseRequest request, CancellationToken cancellationToken)
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
