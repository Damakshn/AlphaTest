using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Works;
using AlphaTest.Core.Checking;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Core.Tests.Ownership;
using AlphaTest.Core.Users;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext :
        IdentityDbContext<AlphaTestUser, AlphaTestRole, Guid, IdentityUserClaim<Guid>,
            AlphaTestUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>,
        IDbContext
    {

        #region DbSets
        public DbSet<Test> Tests { get; set; }

        public DbSet<Contribution> Contributions {get; set;}

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionOption> QuestionOptions { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Membership> Memberships { get; set; }

        public DbSet<Examination> Examinations { get; set; }

        public DbSet<Work> Works { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<CheckResult> Results { get; set; }

        public DbSet<PublishingProposal> PublishingProposals { get; set; }
        #endregion
                        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplyEntityConfigurations(modelBuilder);
        }
    }
}
