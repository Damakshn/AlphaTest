using AlphaTest.Core.UnitTests.Fixtures.Answers;
using AlphaTest.Core.UnitTests.Fixtures.Examinations;
using AlphaTest.Core.UnitTests.Fixtures.Questions;
using AlphaTest.Core.UnitTests.Fixtures.Tests;
using AlphaTest.Core.Users;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class AttemptTestingCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<UserRole>(c =>
                c.FromFactory(() => UserRole.TEACHER)
            );
            fixture.Customize(new DefaultTestCustomization());
            fixture.Customize(new DefaultExaminationCustomization());
        }
    }
}
