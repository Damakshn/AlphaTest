using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Answers.Rules;
using AlphaTest.Core.Works;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Core.UnitTests.Fixtures;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;

namespace AlphaTest.Core.UnitTests.Answers
{
    public class AnswersTests: UnitTestBase
    {
        [Theory, AnswerTestData]
        public void Single_choice_answer_cannot_be_registered_if_option_ID_is_invalid(Work work, SingleChoiceQuestion question, Test test)
        {

            var optionsData = new List<(string text, uint number, bool isRight)>
            {
                new ("Первый вариант", 1, true),
                new ("Второй вариант", 2, false),
                new ("Третий вариант", 3, false)
            };
            question.ChangeOptions(optionsData);

            AssertBrokenRule<SingleChoiceAnswerValueMustBeValidOptionIDRule>(() =>
                new SingleChoiceAnswer(question, work, test, 0, Guid.NewGuid())
            );
        }

        [Theory, AnswerTestData]
        public void Single_choice_answer_can_be_registered(Work work, SingleChoiceQuestion question, Test test)
        {
            var answer = new SingleChoiceAnswer(question, work, test, 0, question.Options[0].ID);

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(work.ID, answer.WorkID);
            Assert.Equal(question.Options[0].ID, answer.RightOptionID);
        }

        [Theory, AnswerTestData]
        public void Multi_choice_answer_cannot_be_registered_if_any_option_ID_is_invalid(Work work, MultiChoiceQuestion question, Test test)
        {
            question.ChangeOptions(new List<(string text, uint number, bool isRight)>
            {
                new ("Первый вариант", 1, true),
                new ("Второй вариант", 2, true),
                new ("Третий вариант", 3, false)
            });

            AssertBrokenRule<MultiChoiceAnswerValueMustBeValidSetOfOptionIDsRule>(() =>
                new MultiChoiceAnswer(question, work, test, 0, new List<Guid> { Guid.NewGuid(), Guid.NewGuid() })
            );
        }

        [Theory, AnswerTestData]
        public void Multi_choice_answer_can_be_registered(Work work, MultiChoiceQuestion question, Test test)
        {
            MultiChoiceAnswer answer = new MultiChoiceAnswer(question, work, test, 0, new List<Guid> { question.Options[0].ID });

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(work.ID, answer.WorkID);
            Assert.All(answer.RightOptions, o => Assert.Equal(1, question.Options.Count(opt => opt.ID == o)));
        }

        [Theory, AnswerTestData]
        public void Exact_numeric_answer_can_be_registered(Work work, QuestionWithNumericAnswer question, Test test)
        {
            ExactNumericAnswer answer = new(question, work, test, 0, 50);

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(work.ID, answer.WorkID);
            Assert.Equal(50, answer.Value);
        }

        [Theory, AnswerTestData]
        public void Exact_textual_answer_can_be_registered(Work work, QuestionWithTextualAnswer question, Test test)
        {
            ExactTextualAnswer answer = new(question, work, test, 0, "Правильно!");

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(work.ID, answer.WorkID);
            Assert.Equal("Правильно!", answer.Value);
        }

        [Theory, AnswerTestData]
        public void Detailed_answer_can_be_registered(Work work, QuestionWithDetailedAnswer question, Test test)
        {
            DetailedAnswer answer = new(question, work, test, 0, "Развёрнутый ответ");

            Assert.Equal(question.ID, answer.QuestionID);
            Assert.Equal(work.ID, answer.WorkID);
        }

        [Theory, AnswerTestData]
        public void None_answer_can_be_registered_for_finished_work(
            Work work,
            Test test,
            SingleChoiceQuestion singleChoiceQuestion,
            MultiChoiceQuestion multiChoiceQuestion,
            QuestionWithNumericAnswer questionWithNumericAnswer,
            QuestionWithTextualAnswer questionWithTextualAnswer,
            QuestionWithDetailedAnswer questionWithDetailedAnswer)
        {
            HelpersForWorks.SetWorkForcedEndDate(work, DateTime.Now);
            work.ForceEnd();

            // проверяем, что одно и то же правило нарушается для всех ответов
            AssertBrokenRule<AnswerCannotBeRegisteredIfWorkIsFinishedRule>(() => 
                new SingleChoiceAnswer(singleChoiceQuestion, work, test, 0, singleChoiceQuestion.Options[0].ID));
            AssertBrokenRule<AnswerCannotBeRegisteredIfWorkIsFinishedRule>(() =>
                new MultiChoiceAnswer(multiChoiceQuestion, work, test, 0, new List<Guid> { singleChoiceQuestion.Options[0].ID }));
            AssertBrokenRule<AnswerCannotBeRegisteredIfWorkIsFinishedRule>(() =>
                new ExactNumericAnswer(questionWithNumericAnswer, work, test, 0, 50));
            AssertBrokenRule<AnswerCannotBeRegisteredIfWorkIsFinishedRule>(() =>
                new ExactTextualAnswer(questionWithTextualAnswer, work, test, 0, "ПравильныйОтвет"));
            AssertBrokenRule<AnswerCannotBeRegisteredIfWorkIsFinishedRule>(() =>
                new DetailedAnswer(questionWithDetailedAnswer, work, test, 0, "Развернутый ответ"));
        }

        [Theory, AnswerTestData]
        public void None_answer_can_be_registered_if_retries_limit_is_exhausted(
            Work work,
            Test test,
            SingleChoiceQuestion singleChoiceQuestion,
            MultiChoiceQuestion multiChoiceQuestion,
            QuestionWithNumericAnswer questionWithNumericAnswer,
            QuestionWithTextualAnswer questionWithTextualAnswer,
            QuestionWithDetailedAnswer questionWithDetailedAnswer)
        {
            uint answersAccepted = test.RevokePolicy.RetriesLimit + 1;
            // проверяем, что одно и то же правило нарушается для всех ответов
            AssertBrokenRule<AnswerCannotBeRegisteredIfNumberOfRetriesIsExhaustedRule>(() =>
                new SingleChoiceAnswer(singleChoiceQuestion, work, test, answersAccepted, singleChoiceQuestion.Options[0].ID));
            AssertBrokenRule<AnswerCannotBeRegisteredIfNumberOfRetriesIsExhaustedRule>(() =>
                new MultiChoiceAnswer(multiChoiceQuestion, work, test, answersAccepted, new List<Guid> { singleChoiceQuestion.Options[0].ID }));
            AssertBrokenRule<AnswerCannotBeRegisteredIfNumberOfRetriesIsExhaustedRule>(() =>
                new ExactNumericAnswer(questionWithNumericAnswer, work, test, answersAccepted, 50));
            AssertBrokenRule<AnswerCannotBeRegisteredIfNumberOfRetriesIsExhaustedRule>(() =>
                new ExactTextualAnswer(questionWithTextualAnswer, work, test, answersAccepted, "ПравильныйОтвет"));
            AssertBrokenRule<AnswerCannotBeRegisteredIfNumberOfRetriesIsExhaustedRule>(() =>
                new DetailedAnswer(questionWithDetailedAnswer, work, test, answersAccepted, "Развернутый ответ"));
        }

        [Theory, AnswerTestData]
        public void Any_number_of_answers_can_be_accepted_if_test_has_no_retries_limit(
            Work work,
            Test test,
            SingleChoiceQuestion singleChoiceQuestion,
            MultiChoiceQuestion multiChoiceQuestion,
            QuestionWithNumericAnswer questionWithNumericAnswer,
            QuestionWithTextualAnswer questionWithTextualAnswer,
            QuestionWithDetailedAnswer questionWithDetailedAnswer)
        {
            uint answersAccepted = 1000;
            RevokePolicy revokePolicy = new(true, infiniteRetriesEnabled: true);
            test.ChangeRevokePolicy(revokePolicy);

            SingleChoiceAnswer singlieChoiceAnswer = new(singleChoiceQuestion, work, test, answersAccepted, singleChoiceQuestion.Options[0].ID);
            MultiChoiceAnswer multiChoiceAnswer = new(multiChoiceQuestion, work, test, answersAccepted, new List<Guid> { multiChoiceQuestion.Options[0].ID });
            ExactNumericAnswer exactNumericAnswer =  new(questionWithNumericAnswer, work, test, answersAccepted, 50);
            ExactTextualAnswer exactTextualAnswer = new(questionWithTextualAnswer, work, test, answersAccepted, "ПравильныйОтвет");
            DetailedAnswer detailedAnswer = new(questionWithDetailedAnswer, work, test, answersAccepted, "Развернутый ответ");

            List<Answer> answers = new(){ singlieChoiceAnswer, multiChoiceAnswer, exactNumericAnswer, exactTextualAnswer, detailedAnswer };
            foreach (var answer in answers)
            {
                Assert.Equal(work.ID, answer.WorkID);
            }
        }

        [Theory, AnswerTestData]
        public void None_answer_can_be_revoked_if_revoke_is_not_enabled(Work work,
            Test test,
            SingleChoiceAnswer singleChoiceAnswer,
            MultiChoiceAnswer multiChoiceAnswer,
            ExactNumericAnswer exactNumericAnswer,
            ExactTextualAnswer exactTextualAnswer,
            DetailedAnswer detailedAnswer)
        {
            List<Answer> answers = new() { singleChoiceAnswer, multiChoiceAnswer, exactNumericAnswer, exactTextualAnswer, detailedAnswer };
            test.ChangeRevokePolicy(new RevokePolicy(false));

            foreach (Answer answer in answers)
            {
                AssertBrokenRule<AnswerCannotBeRevokedIfRevokeIsNotAllowedRule>(() => answer.Revoke(test, work));
            }
        }

        [Theory, AnswerTestData]
        public void None_answer_can_be_revoked_if_work_is_already_finished(Work work,
            Test test,
            SingleChoiceAnswer singleChoiceAnswer,
            MultiChoiceAnswer multiChoiceAnswer,
            ExactNumericAnswer exactNumericAnswer,
            ExactTextualAnswer exactTextualAnswer,
            DetailedAnswer detailedAnswer)
        {
            List<Answer> answers = new() { singleChoiceAnswer, multiChoiceAnswer, exactNumericAnswer, exactTextualAnswer, detailedAnswer };
            test.ChangeRevokePolicy(new RevokePolicy(true, 2));
            HelpersForWorks.SetWorkForcedEndDate(work, DateTime.Now);
            work.ForceEnd();

            foreach (Answer answer in answers)
            {
                AssertBrokenRule<AnswerCannotBeRevokedIfWorkIsFinishedRule>(() => answer.Revoke(test, work));
            }
        }

        [Theory, AnswerTestData]
        public void Answer_can_be_revoked(
            Test test,
            Work work,
            SingleChoiceAnswer singleChoiceAnswer,
            MultiChoiceAnswer multiChoiceAnswer,
            ExactTextualAnswer exactTextualAnswer,
            ExactNumericAnswer exactNumericAnswer,
            DetailedAnswer detailedAnswer)
        {
            List<Answer> answers = new() { singleChoiceAnswer, multiChoiceAnswer, exactTextualAnswer, exactNumericAnswer, detailedAnswer };
            test.ChangeRevokePolicy(new RevokePolicy(true, 2));

            foreach (Answer answer in answers)
            {
                answer.Revoke(test, work);
            }

            Assert.All(answers, answer => Assert.True(answer.IsRevoked));
        }

    }
}
