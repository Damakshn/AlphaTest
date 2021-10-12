using System;
using System.Collections.Generic;
using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Users
{   
    public class User: Entity, IAlphaTestUser
    {
        public static readonly TimeSpan TEMPORARY_PASSWORD_DURATION = new(24, 0, 0);

        private List<UserRole> _roles;

        public User(string firstName, string lastName, string middleName, string email, UserRole initialRole, string temporaryPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
            _roles = new List<UserRole> { initialRole };
            RegisteredAt = DateTime.Now;
            TemporaryPassword = temporaryPassword;
            TemporaryPasswordExpirationDate = RegisteredAt + TEMPORARY_PASSWORD_DURATION;
            IsPasswordChanged = false;
            IsSuspended = false;
            LastVisitedAt = null;
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string MiddleName { get; private set; }

        public string Email { get; private set; }

        public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

        public string TemporaryPassword { get; private set; }

        public DateTime RegisteredAt { get; private set; }

        public DateTime? LastVisitedAt { get; private set; }

        public DateTime TemporaryPasswordExpirationDate { get; private set; }

        public bool IsPasswordChanged { get; private set; }

        public bool IsSuspended { get; private set; }

        public bool IsAdmin => IsInRole(UserRole.ADMIN);

        public bool IsStudent => IsInRole(UserRole.STUDENT);

        public bool IsTeacher => IsInRole(UserRole.TEACHER);

        public bool IsInRole(UserRole role)
        {
            return _roles.Contains(role);
        }

        public void Suspend()
        {
            IsSuspended = true;
        }

        public void Unlock()
        {
            IsSuspended = false;
        }

        public void ResetTemporaryPassword(string newPassword)
        {
            TemporaryPassword = newPassword;
        }
    }
}
