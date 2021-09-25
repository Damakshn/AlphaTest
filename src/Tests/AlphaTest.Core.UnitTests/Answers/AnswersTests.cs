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
using System.Linq;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Tests.TestSettings.TestFlow;

namespace AlphaTest.Core.UnitTests.Answers
{
    public class AnswersTests: UnitTestBase
    {
        [Theory, AnswerTestData]
        public void Single_choice_answer_cannot_be_registered_if_option_ID_is_invalid(Attempt attempt, SingleChoiceQuestion question)
        {

            var optionsData = new List<(string text, uint number, bool isRight)>
            {
                new ("Первый вариант", 1, true),
                new ("Второй вариант", 2, false),
                new ("Третий вариант", 3, false)
            };
            question.ChangeOptions(optionsData);

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
            question.ChangeOptions(new List<(string text, uint number, bool isRight)>
            {
                new ("Первый вариант", 1, true),
                new ("Второй вариант", 2, true),
                new ("Третий вариант", 3, false)
            });

            AssertBrokenRule<MultiChoiceAnswerValueMustBeValidSetOfOptionIDsRule>(() =>
                new MultiChoiceAnswer(question, attempt, new List<Guid> { Guid.NewGuid(), Guid.NewGuid() })
            );
        }

        [Theory, AnswerTestData]
        public void Multi_choice_answer_can_be_registered(Attempt attempt, MultiChoiceQuestion question)
        {
            MultiChoiceAnswer answer = new MultiChoiceAnswer(question, attempt, new List<Guid> { question.Options[0].ID });

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(attempt.ID, answer.AttemptID);
            Assert.All(answer.RightOptions, o => Assert.Equal(1, question.Options.Count(opt => opt.ID == o)));
        }

        [Theory, AnswerTestData]
        public void Exact_numeric_answer_can_be_registered(Attempt attempt, QuestionWithNumericAnswer question)
        {
            ExactNumericAnswer answer = new(question, attempt, 50);

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(attempt.ID, answer.AttemptID);
            Assert.Equal(50, answer.Value);
        }

        [Theory, AnswerTestData]
        public void Exact_textual_answer_can_be_registered(Attempt attempt, QuestionWithTextualAnswer question)
        {
            ExactTextualAnswer answer = new(question, attempt, "Правильно!");

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(attempt.ID, answer.AttemptID);
            Assert.Equal("Правильно!", answer.Value);
        }

        [Theory, AnswerTestData]
        public void Detailed_answer_can_be_registered(Attempt attempt, QuestionWithDetailedAnswer question)
        {
            DetailedAnswer answer = new(question, attempt, "Развёрнутый ответ");

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(attempt.ID, answer.AttemptID);
        }

        [Theory, AnswerTestData]
        public void None_answer_can_be_registered_for_finished_attempt(
            Attempt attempt,
            SingleChoiceQuestion singleChoiceQuestion,
            MultiChoiceQuestion multiChoiceQuestion,
            QuestionWithNumericAnswer questionWithNumericAnswer,
            QuestionWithTextualAnswer questionWithTextualAnswer,
            QuestionWithDetailedAnswer questionWithDetailedAnswer)
        {
            HelpersForAttempts.SetAttemptForcedEndDate(attempt, DateTime.Now);
            attempt.ForceEnd();

            // проверяем, что одно и то же правило нарушается для всех ответов
            AssertBrokenRule<AnswerCannotBeRegisteredIfAttemptIsFinishedRule>(() => 
                new SingleChoiceAnswer(singleChoiceQuestion, attempt, singleChoiceQuestion.Options[0].ID));
            AssertBrokenRule<AnswerCannotBeRegisteredIfAttemptIsFinishedRule>(() =>
                new MultiChoiceAnswer(multiChoiceQuestion, attempt, new List<Guid> { singleChoiceQuestion.Options[0].ID }));
            AssertBrokenRule<AnswerCannotBeRegisteredIfAttemptIsFinishedRule>(() =>
                new ExactNumericAnswer(questionWithNumericAnswer, attempt, 50));
            AssertBrokenRule<AnswerCannotBeRegisteredIfAttemptIsFinishedRule>(() =>
                new ExactTextualAnswer(questionWithTextualAnswer, attempt, "ПравильныйОтвет"));
            AssertBrokenRule<AnswerCannotBeRegisteredIfAttemptIsFinishedRule>(() =>
                new DetailedAnswer(questionWithDetailedAnswer, attempt, "Развернутый ответ"));
        }

        [Theory, AnswerTestData]
        public void None_answer_can_be_revoked_if_revoke_is_not_enabled(Attempt attempt,
            Test test,
            SingleChoiceQuestion singleChoiceQuestion,
            MultiChoiceQuestion multiChoiceQuestion,
            QuestionWithNumericAnswer questionWithNumericAnswer,
            QuestionWithTextualAnswer questionWithTextualAnswer,
            QuestionWithDetailedAnswer questionWithDetailedAnswer)
        {   
            SingleChoiceAnswer singleChoiceAnswer = new(singleChoiceQuestion, attempt, singleChoiceQuestion.Options[0].ID);
            MultiChoiceAnswer multiChoiceAnswer = new(multiChoiceQuestion, attempt, new List<Guid> { multiChoiceQuestion.Options[0].ID });
            ExactNumericAnswer exactNumericAnswer = new(questionWithNumericAnswer, attempt, 50);
            ExactTextualAnswer exactTextualAnswer = new(questionWithTextualAnswer, attempt, "Правильный ответ");
            DetailedAnswer detailedAnswer = new(questionWithDetailedAnswer, attempt, "Развёрнутый ответ");

            List<Answer> answers = new() { singleChoiceAnswer, multiChoiceAnswer, exactNumericAnswer, exactTextualAnswer, detailedAnswer };
            test.ChangeRevokePolicy(new RevokePolicy(false));

            foreach (Answer answer in answers)
            {
                AssertBrokenRule<AnswerCannotBeRevokedIfRevokeIsNotAllowedRule>(() => answer.Revoke(test, attempt, 0));
            }
        }

        [Theory, AnswerTestData]
        public void None_answer_can_be_revoked_if_attempt_is_already_finished(Attempt attempt,
            Test test,
            SingleChoiceQuestion singleChoiceQuestion,
            MultiChoiceQuestion multiChoiceQuestion,
            QuestionWithNumericAnswer questionWithNumericAnswer,
            QuestionWithTextualAnswer questionWithTextualAnswer,
            QuestionWithDetailedAnswer questionWithDetailedAnswer)
        {   
            SingleChoiceAnswer singleChoiceAnswer = new(singleChoiceQuestion, attempt, singleChoiceQuestion.Options[0].ID);
            MultiChoiceAnswer multiChoiceAnswer = new(multiChoiceQuestion, attempt, new List<Guid> { multiChoiceQuestion.Options[0].ID });
            ExactNumericAnswer exactNumericAnswer = new(questionWithNumericAnswer, attempt, 50);
            ExactTextualAnswer exactTextualAnswer = new(questionWithTextualAnswer, attempt, "Правильный ответ");
            DetailedAnswer detailedAnswer = new(questionWithDetailedAnswer, attempt, "Развёрнутый ответ");

            List<Answer> answers = new() { singleChoiceAnswer, multiChoiceAnswer, exactNumericAnswer, exactTextualAnswer, detailedAnswer };
            test.ChangeRevokePolicy(new RevokePolicy(true, 2));
            HelpersForAttempts.SetAttemptForcedEndDate(attempt, DateTime.Now);
            attempt.ForceEnd();

            foreach (Answer answer in answers)
            {
                AssertBrokenRule<AnswerCannotBeRevokedIfAttemptIsFinishedRule>(() => answer.Revoke(test, attempt, 0));
            }
        }

        [Theory, AnswerTestData]
        public void None_answer_can_be_revoked_if_limit_of_retries_is_exhausted(Attempt attempt,
            Test test,
            SingleChoiceQuestion singleChoiceQuestion,
            MultiChoiceQuestion multiChoiceQuestion,
            QuestionWithNumericAnswer questionWithNumericAnswer,
            QuestionWithTextualAnswer questionWithTextualAnswer,
            QuestionWithDetailedAnswer questionWithDetailedAnswer)
        {
            SingleChoiceAnswer singleChoiceAnswer = new(singleChoiceQuestion, attempt, singleChoiceQuestion.Options[0].ID);
            MultiChoiceAnswer multiChoiceAnswer = new(multiChoiceQuestion, attempt, new List<Guid> { multiChoiceQuestion.Options[0].ID });
            ExactNumericAnswer exactNumericAnswer = new(questionWithNumericAnswer, attempt, 50);
            ExactTextualAnswer exactTextualAnswer = new(questionWithTextualAnswer, attempt, "Правильный ответ");
            DetailedAnswer detailedAnswer = new(questionWithDetailedAnswer, attempt, "Развёрнутый ответ");

            List<Answer> answers = new() { singleChoiceAnswer, multiChoiceAnswer, exactNumericAnswer, exactTextualAnswer, detailedAnswer };
            uint maxRetries = 2;
            test.ChangeRevokePolicy(new RevokePolicy(true, maxRetries));

            foreach (Answer answer in answers)
            {
                AssertBrokenRule<AnswerCannotBeRevokedIfNumberOfRetriesIsExhaustedRule>(() => answer.Revoke(test, attempt, maxRetries));
            }
        }

        [Theory, AnswerTestData]
        public void Answer_can_be_revoked(Attempt attempt,
            Test test,
            SingleChoiceQuestion singleChoiceQuestion,
            MultiChoiceQuestion multiChoiceQuestion,
            QuestionWithNumericAnswer questionWithNumericAnswer,
            QuestionWithTextualAnswer questionWithTextualAnswer,
            QuestionWithDetailedAnswer questionWithDetailedAnswer)
        {
            SingleChoiceAnswer singleChoiceAnswer = new(singleChoiceQuestion, attempt, singleChoiceQuestion.Options[0].ID);
            MultiChoiceAnswer multiChoiceAnswer = new(multiChoiceQuestion, attempt, new List<Guid> { multiChoiceQuestion.Options[0].ID });
            ExactNumericAnswer exactNumericAnswer = new(questionWithNumericAnswer, attempt, 50);
            ExactTextualAnswer exactTextualAnswer = new(questionWithTextualAnswer, attempt, "Правильный ответ");
            DetailedAnswer detailedAnswer = new(questionWithDetailedAnswer, attempt, "Развёрнутый ответ");

            List<Answer> answers = new() { singleChoiceAnswer, multiChoiceAnswer, exactNumericAnswer, exactTextualAnswer, detailedAnswer };
            test.ChangeRevokePolicy(new RevokePolicy(true, 2));

            foreach (Answer answer in answers)
            {
                answer.Revoke(test, attempt, 0);
            }

            Assert.All(answers, answer => Assert.True(answer.IsRevoked));
        }

    }
}
