using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Users
{
    public class UserRole: Enumeration<UserRole>
    {
        private UserRole(int id, string nameRussian, string nameEnglish): base(id, nameRussian) 
        {
            NameEnglish = nameEnglish;
        }

        public string NameEnglish { get; private set; }

        public static readonly UserRole STUDENT = new(1, "Учащийся", "Student");

        public static readonly UserRole TEACHER = new(2, "Преподаватель", "Teacher");

        public static readonly UserRole ADMIN = new(3, "Администратор", "Admin");
    }
}
