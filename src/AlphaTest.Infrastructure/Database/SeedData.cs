using System;
using System.Linq;
using AlphaTest.Infrastructure.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace AlphaTest.Infrastructure.Database
{
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider serviceProvider)
        {
            using(var scope = serviceProvider.CreateScope())
            {
                using(var context = scope.ServiceProvider.GetRequiredService<AlphaTestContext>())
                {
                    AppRole admin = context.Roles.Where(role => role.Name == "Admin").FirstOrDefault();
                    if (admin is null)
                    {
                        admin = new() { Name = "admin", NameInRussian = "Администратор" };
                        context.Add(admin);
                    }

                    AppRole teacher = context.Roles.Where(role => role.Name == "Teacher").FirstOrDefault();
                    if (teacher is null)
                    {
                        teacher = new() { Name = "teacher", NameInRussian = "Преподаватель" };
                        context.Add(teacher);
                    }

                    AppRole student = context.Roles.Where(role => role.Name == "Student").FirstOrDefault();
                    if (student is null)
                    {
                        student = new() { Name = "student", NameInRussian = "Обучающийся" };
                        context.Add(student);
                    }

                    // ToDo придумать что-то с паролем и email
                    if (context.Users.Any() == false)
                    {
                        AppUser firstUser = new(
                            "Виктор",
                            "Сорокин",
                            "Николаевич",
                            "tmpPassw",
                            "admin@mail.ru");
                        AppUserRole firstUserIsAdmin = new() { Role = admin, User = firstUser };
                        AppUserRole firstUserIsTeacher = new() { Role = teacher, User = firstUser };
                        context.Users.Add(firstUser);
                        context.UserRoles.Add(firstUserIsAdmin);
                        context.UserRoles.Add(firstUserIsTeacher);
                    }

                    if (context.ChangeTracker.HasChanges())
                    {
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
