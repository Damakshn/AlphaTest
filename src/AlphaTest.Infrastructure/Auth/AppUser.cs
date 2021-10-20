using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using AlphaTest.Core.Users;

namespace AlphaTest.Infrastructure.Auth
{
    public class AppUser: IdentityUser<Guid>, IAlphaTestUser
    {
        #region Поля
        private string _firstName;

        private string _lastName;

        private string _middleName;

        private string _temporaryPassword;

        private DateTime _temporaryPasswordExpirationDate;

        private DateTime _registeredAt;

        private DateTime? _lastVisitedAt;

        private bool _isPasswordChanged;

        private bool _isSuspended;

        // TBD
        // https://stackoverflow.com/questions/47767267/ef-core-2-how-to-include-roles-navigation-property-on-identityuser/47772406
        // https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x?view=aspnetcore-5.0#add-identityuser-poco-navigation-properties
        private ICollection<AppUserRole> _userRoles;
        #endregion

        public AppUser(string firstName, string lastName, string middleName, string temporaryPassword, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _middleName = middleName;
            Email = email;
            UserName = email;
            NormalizedEmail = email.ToUpper();
            NormalizedUserName = NormalizedEmail;
            _temporaryPassword = temporaryPassword;
            PasswordHash = new PasswordHasher<AppUser>().HashPassword(this, temporaryPassword);
            _isPasswordChanged = false;
            _isSuspended = false;
            _registeredAt = DateTime.Now;
            _lastVisitedAt = null;
            _userRoles = new List<AppUserRole>();
        }

        #region Свойства
        public Guid ID => this.Id;

        public string FirstName => _firstName;

        public string LastName => _lastName;

        public string MiddleName => _middleName;

        public string TemporaryPassword => _temporaryPassword;

        public DateTime TemporaryPasswordExpirationDate => _temporaryPasswordExpirationDate;

        public DateTime RegisteredAt => _registeredAt;

        public DateTime? LastVisitedAt => _lastVisitedAt;

        public bool IsPasswordChanged => _isPasswordChanged;

        public bool IsSuspended => _isSuspended;
        
        public bool IsAdmin => HasRole("Admin");

        public bool IsStudent => HasRole("Student");

        public bool IsTeacher => HasRole("Teacher");
        #endregion

        #region Методы
        public void ResetTemporaryPassword(string newPassword)
        {
            throw new NotImplementedException();
        }

        public void Suspend()
        {
            _isSuspended = true;
        }

        public void Unlock()
        {
            _isSuspended = false;
        }

        private bool HasRole(string roleName)
        {
            return _userRoles.Where(userRole => userRole.Role.Name == roleName).FirstOrDefault() is not null;
        }
        #endregion
    }
}
