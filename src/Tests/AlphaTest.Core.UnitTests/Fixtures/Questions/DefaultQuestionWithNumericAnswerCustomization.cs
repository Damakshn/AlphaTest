using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.UnitTests.Fixtures.Questions
{
    internal class DefaultQuestionWithNumericAnswerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<QuestionWithNumericAnswer>(c =>
                c.FromFactory((
                    Test test,
                    QuestionText text,
                    uint number,
                    decimal rightAnswer) =>
                {
                    HelpersForTests.SetNewStatusForTest(test, TestStatus.Draft);
                    return test.AddQuestionWithNumericAnswer(text, new QuestionScore(1), rightAnswer, number);
                }   
            ));
        }
    }
}
