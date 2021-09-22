using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AlphaTest.Core.Tests;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class AnswerTestingCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<UserRole>(c =>
                c.FromFactory(() => UserRole.TEACHER)
            );
            fixture.Customize<Test>(composer =>
               composer.FromFactory(
                   (int id, string title, string topic, int authorID) => new Test(id, title, topic, authorID, false))
               .Do(t => HelpersForTests.SetNewStatusForTest(t, TestStatus.Published))
            );
            fixture.Customize(new DefaultSingleChoiceQuestionCustomization());
            fixture.Customize(new DefaultMultiChoiceQuestionCustomization());
            fixture.Customize(new DefaultExaminationCustomization());
        }
    }
}
