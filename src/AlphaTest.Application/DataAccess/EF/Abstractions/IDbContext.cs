using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Checking;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Tests.Ownership;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Core.Users;


namespace AlphaTest.Application.DataAccess.EF.Abstractions
{
    public interface IDbContext
    {
        DbSet<Test> Tests { get; }

        DbSet<Contribution> Contributions { get; }

        DbSet<Question> Questions { get; }

        DbSet<QuestionOption> QuestionOptions { get; }

        DbSet<Group> Groups { get; }

        DbSet<Membership> Memberships { get; }

        DbSet<Examination> Examinations { get; }

        DbSet<Work> Works { get; }

        DbSet<Answer> Answers { get; }

        DbSet<CheckResult> Results { get; }

        DbSet<PublishingProposal> PublishingProposals { get; }

        DbSet<AlphaTestUser> Users { get; }

        DbSet<AlphaTestRole> Roles { get; }

        DbSet<AlphaTestUserRole> UserRoles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
