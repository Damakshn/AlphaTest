using System.Collections.Generic;
using System;
using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.Answers;
using AutoFixture;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests;
using AlphaTest.Core.UnitTests.Fixtures;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Answers.Rules;

namespace AlphaTest.Core.UnitTests.Answers
{
    public class AnswersTests: UnitTestBase
    {
        [Theory, AnswerTestData]
        public void Single_choice_answer_cannot_be_registered_if_option_ID_is_invalid(Attempt attempt, SingleChoiceQuestion question)
        {
            question.ChangeOptions(new List<QuestionOption>
            {
                new QuestionOption("Первый вариант", 1, true),
                new QuestionOption("Второй вариант", 2, false),
                new QuestionOption("Третий вариант", 3, false)
            });

            AssertBrokenRule<SingleChoiceAnswerValueMustBeValidOptionIDRule>(() =>
                new SingleChoiceAnswer(question, attempt, Guid.NewGuid())
            );
        }

        [Theory, AnswerTestData]
        public void Single_choice_answer_can_be_registered(Attempt attempt, SingleChoiceQuestion question)
        {
            var answer = new SingleChoiceAnswer(question, attempt, question.Options[0].ID);

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(attempt.ID, answer.AttemptID);
            Assert.Equal(question.Options[0].ID, answer.RightOptionID);
        }

        [Theory, AnswerTestData]
        public void Multi_choice_answer_cannot_be_registered_if_any_option_ID_is_invalid(Attempt attempt, MultiChoiceQuestion question)
        {
            question.ChangeOptions(new()
            {
                new QuestionOption("Первый вариант", 1, true),
                new QuestionOption("Второй вариант", 2, true),
                new QuestionOption("Третий вариант", 3, false)
            });

            AssertBrokenRule<MultiChoiceAnswerValueMustBeValidSetOfOptionIDsRule>(() =>
                new MultiChoiceAnswer(question, attempt, new List<Guid> { Guid.NewGuid(), Guid.NewGuid() })
            );
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
