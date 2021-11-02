using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.SuspendUser
{
    public class SuspendUserUseCaseRequest : IUseCaseRequest
    {
        public SuspendUserUseCaseRequest(Guid suspendedUserID, Guid currentUserID)
        {
            SuspendedUserID = suspendedUserID;
            CurrentUserID = currentUserID;
        }

        public Guid SuspendedUserID { get; private set; }

        public Guid CurrentUserID { get; private set; }
    }
}
