using System;

namespace AlphaTest.Core.Users
{
    public interface IAlphaTestUser
    {
        #region Свойства

        #region Основные
        public Guid ID { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string MiddleName { get; }

        public string Email { get; }

        public string TemporaryPassword { get; }

        public DateTime RegisteredAt { get;  }

        public DateTime? LastVisitedAt { get; }

        public DateTime TemporaryPasswordExpirationDate { get; }

        public bool IsPasswordChanged { get; }

        public bool IsSuspended { get; }
        #endregion

        #region Флаги ролей
        public bool IsAdmin { get; }

        public bool IsStudent { get; }

        public bool IsTeacher { get; }
        #endregion

        #endregion

        #region Методы
        public void Suspend();

        public void Unlock();

        public void ResetTemporaryPassword(string newPassword);
        #endregion
    }
}
