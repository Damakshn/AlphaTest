using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;
using AlphaTest.Application.UtilityServices.Security;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.GenerateTemporaryPassword
{
    public class GenerateTemporaryPasswordUseCaseHandler : UseCaseHandlerBase<GenerateTemporaryPasswordUseCaseRequest>
    {
        private readonly IPasswordGenerator _passwordGenerator;

        public GenerateTemporaryPasswordUseCaseHandler(IDbContext db, IPasswordGenerator passwordGenerator) : base(db)
        {
            _passwordGenerator = passwordGenerator;
        }

        public override async Task<Unit> Handle(GenerateTemporaryPasswordUseCaseRequest request, CancellationToken cancellationToken)
        {
            AlphaTestUser user = await _db.Users.Aggregates().FindByID(request.UserID);
            string newPassword = _passwordGenerator.GeneratePassword();
            user.ResetTemporaryPassword(newPassword);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
