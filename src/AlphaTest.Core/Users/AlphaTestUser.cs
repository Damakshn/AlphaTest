using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using AlphaTest.Core.Common;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Common.Exceptions;
using AlphaTest.Core.Users.Rules;

namespace AlphaTest.Core.Users
{
    public class AlphaTestUser : IdentityUser<Guid>, IAlphaTestUser, ICanCheckRules
    {
        #region Поля
        public static readonly TimeSpan TemporaryPasswordLifetime = new(1, 0, 0, 0);


        // TBD
        // https://stackoverflow.com/questions/47767267/ef-core-2-how-to-include-roles-navigation-property-on-identityuser/47772406
        // https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x?view=aspnetcore-5.0#add-identityuser-poco-navigation-properties
        private ICollection<AppUserRole> _userRoles;
        #endregion

        public AlphaTestUser(string firstName, string lastName, string middleName, string temporaryPassword, string email)
            : base()
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
            UserName = email;
            NormalizedEmail = email.ToUpper();
            NormalizedUserName = NormalizedEmail;
            TemporaryPassword = temporaryPassword;
            PasswordHash = new PasswordHasher<AlphaTestUser>().HashPassword(this, temporaryPassword);
            IsPasswordChanged = false;
            IsSuspended = false;
            RegisteredAt = DateTime.Now;
            LastVisitedAt = null;
            _userRoles = new List<AppUserRole>();
            TemporaryPasswordExpirationDate = CalculateTemporaryPasswordExpirationDate();
            SecurityStamp = Guid.NewGuid().ToString();
        }

        #region Свойства
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string MiddleName { get; private set; }

        public string TemporaryPassword { get; private set; }

        public DateTime TemporaryPasswordExpirationDate { get; private set; }

        public DateTime RegisteredAt { get; private set; }

        public DateTime? LastVisitedAt { get; private set; }

        public bool IsPasswordChanged { get; private set; }

        public bool IsSuspended { get; private set; }

        public bool IsAdmin => HasRole("Admin");

        public bool IsStudent => HasRole("Student");

        public bool IsTeacher => HasRole("Teacher");
        #endregion

        #region Методы
        public void ResetTemporaryPassword(string newPassword)
        {
            TemporaryPassword = newPassword;
            PasswordHash = new PasswordHasher<AlphaTestUser>().HashPassword(this, newPassword);
            TemporaryPasswordExpirationDate = CalculateTemporaryPasswordExpirationDate();
        }

        public void ChangePassword(string oldPassword, string newPassword, string newPasswordRepeat)
        {
            var hasher = new PasswordHasher<AlphaTestUser>();
            CheckRule(new NewPermanentPasswordMustBeRepeatedCorrectlyRule(newPassword, newPasswordRepeat));
            // ToDo check system password options
            PasswordHash = hasher.HashPassword(this, newPassword);
            IsPasswordChanged = true;
        }

        public void Suspend()
        {
            CheckRule(new AdminUserCannotBeSuspendedRule(this));
            IsSuspended = true;
        }

        public void Unlock()
        {
            IsSuspended = false;
        }

        public void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken)
                throw new BusinessException(rule);
        }

        public bool IsTemporaryPasswordExpired()
        {
            return !IsPasswordChanged && TemporaryPasswordExpirationDate <= DateTime.Now;
        }

        private bool HasRole(string roleName)
        {
            return _userRoles.Where(userRole => userRole.Role.Name == roleName).FirstOrDefault() is not null;
        }

        private static DateTime CalculateTemporaryPasswordExpirationDate()
        {
            return DateTime.Now + TemporaryPasswordLifetime;
        }
        #endregion
    }
}
