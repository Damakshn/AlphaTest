using AlphaTest.Core.Tests.Questions;
using System;
using System.Collections.Generic;
using static AlphaTest.Core.UnitTests.Testmaking.Questions.QuestionTestsBase;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionTestSamples
    {
        #region Создание вопросов со стандартными данными
        protected static Func<QuestionTestData, Question> CreateSingleChoiceQuestion =
            data => data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

        protected static Func<QuestionTestData, Question> CreateMultiChoiceQuestion =
            data => data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

        protected static Func<QuestionTestData, Question> CreateQuestionWithTextualAnswer =
            data => data.Test.AddQuestionWithTextualAnswer(data.Text, data.Score, data.TextualAnswer, data.CounterMock.Object);

        protected static Func<QuestionTestData, Question> CreateQuestionWithNumericAnswer =
            data => data.Test.AddQuestionWithNumericAnswer(data.Text, data.Score, data.NumericAnswer, data.CounterMock.Object);

        protected static Func<QuestionTestData, Question> CreateQuestionWithDetailedAnswer =
            data => data.Test.AddQuestionWithDetailedAnswer(data.Text, data.Score, data.CounterMock.Object);

        public static IEnumerable<object[]> InstanceAllTypesOfQuestions =>
            new List<object[]>
            {
                new object[] { CreateSingleChoiceQuestion },
                new object[] { CreateMultiChoiceQuestion },
                new object[] { CreateQuestionWithTextualAnswer },
                new object[] { CreateQuestionWithNumericAnswer },
                new object[] { CreateQuestionWithDetailedAnswer }
            };
        #endregion
    }
}
