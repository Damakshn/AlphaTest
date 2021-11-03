using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.GenerateTemporaryPassword
{
    public class GenerateTemporaryPasswordUseCaseRequest : IUseCaseRequest
    {
        public GenerateTemporaryPasswordUseCaseRequest(Guid userID)
        {
            UserID = userID;
        }

        public Guid UserID { get; private set; }
    }
}
