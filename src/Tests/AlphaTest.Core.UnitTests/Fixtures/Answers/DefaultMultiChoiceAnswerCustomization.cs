using AlphaTest.Core.Answers;
using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;
using AutoFixture;
using System;
using System.Collections.Generic;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.UnitTests.Fixtures.Answers
{
    internal class DefaultMultiChoiceAnswerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<MultiChoiceAnswer>(c =>
                c.FromFactory((MultiChoiceQuestion question, Work work, Test test) => 
                    new MultiChoiceAnswer(question, work, test, 0, new List<Guid>() { question.Options[0].ID})
                )
            );
        }
    }
}
