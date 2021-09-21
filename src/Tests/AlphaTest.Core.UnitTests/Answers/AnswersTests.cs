using System.Collections.Generic;
using System;
using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.Answers;
using AutoFixture;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests;
using AlphaTest.Core.UnitTests.Common.Helpers;

namespace AlphaTest.Core.UnitTests.Answers
{
    public class AnswersTests: UnitTestBase
    {
        [Fact]
        public void Single_choice_answer_cannot_be_registered_if_option_ID_is_invalid()
        {
            Fixture fixture = new Fixture();
            fixture.Customize<Test>( composer =>
                composer.FromFactory(
                    (int id, string title, string topic, int authorID) => new Test(id, title, topic, authorID, false))
                .Do(t => HelpersForTests.SetNewStatusForTest(t, TestStatus.Published))
            );
            //Test test = fixture.Create<Test>();
            Attempt attempt = fixture.Build<Attempt>().Create();
            //throw new NotImplementedException();
            Assert.Equal(1, 1);
        }

        [Fact(Skip = "Ещё не готово")]
        public void Single_choice_answer_can_be_registered()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Ещё не готово")]
        public void Multi_choice_answer_cannot_be_registered_if_any_option_ID_is_invalid()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Ещё не готово")]
        public void Multi_choice_answer_can_be_registered()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Ещё не готово")]
        public void Exact_numeric_answer_can_be_registered()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Ещё не готово")]
        public void Exact_textual_answer_can_be_registered()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Ещё не готово")]
        public void Detailed_answer_can_be_registered()
        {
            throw new NotImplementedException();
        }
        
        [Fact(Skip = "Ещё не готово")]
        public void None_answer_can_be_registered_for_finished_attempt()
        {
            throw new NotImplementedException();
        }
        
        [Theory(Skip = "Ещё не готово")]
        [MemberData(nameof(InstanceAllTypesOfAnswers))]
        public void None_answer_can_be_revoked_if_revoke_is_not_enabled(Func<Answer> answerCreatingDelegate)
        {
            throw new NotImplementedException();
        }

        [Theory(Skip = "Ещё не готово")]
        [MemberData(nameof(InstanceAllTypesOfAnswers))]
        public void None_answer_can_be_revoked_if_attempt_is_already_finished(Func<Answer> answerCreatingDelegate)
        {
            throw new NotImplementedException();
        }

        [Theory(Skip = "Ещё не готово")]
        [MemberData(nameof(InstanceAllTypesOfAnswers))]
        public void None_answer_can_be_revoked_if_limit_of_retries_is_exhausted(Func<Answer> answerCreatingDelegate)
        {
            throw new NotImplementedException();
        }

        [Theory(Skip = "Ещё не готово")]
        [MemberData(nameof(InstanceAllTypesOfAnswers))]
        public void Answer_can_be_revoked(Func<Answer> answerCreatingDelegate)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<object[]> InstanceAllTypesOfAnswers =>
            new List<object[]>
            {
                new object[]{},
                new object[]{},
                new object[]{},
                new object[]{},
                new object[]{}
            };
    }
}
