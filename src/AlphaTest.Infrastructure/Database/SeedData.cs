using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using AlphaTest.Core.Users;

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
                    AlphaTestRole admin = context.Roles.Where(role => role.Name == "Admin").FirstOrDefault();
                    if (admin is null)
                    {
                        admin = new("Admin", "Администратор");
                        context.Add(admin);
                    }

                    AlphaTestRole teacher = context.Roles.Where(role => role.Name == "Teacher").FirstOrDefault();
                    if (teacher is null)
                    {
                        teacher = new("Teacher", "Преподаватель");
                        context.Add(teacher);
                    }

                    AlphaTestRole student = context.Roles.Where(role => role.Name == "Student").FirstOrDefault();
                    if (student is null)
                    {
                        student = new("Student", "Обучающийся");
                        context.Add(student);
                    }

                    // ToDo придумать что-то с паролем и email
                    if (context.Users.Any() == false)
                    {
                        AlphaTestUser firstUser = new(
                            "Виктор",
                            "Сорокин",
                            "Николаевич",
                            "tmpPassw",
                            "admin@mail.ru");
                        AlphaTestUserRole firstUserIsAdmin = new() { Role = admin, User = firstUser };
                        AlphaTestUserRole firstUserIsTeacher = new() { Role = teacher, User = firstUser };
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
