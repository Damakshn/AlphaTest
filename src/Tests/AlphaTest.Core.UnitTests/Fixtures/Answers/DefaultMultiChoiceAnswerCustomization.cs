using AlphaTest.Core.Answers;
using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;
using AutoFixture;
using System;
using System.Collections.Generic;

namespace AlphaTest.Core.UnitTests.Fixtures.Answers
{
    internal class DefaultMultiChoiceAnswerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<MultiChoiceAnswer>(c =>
                c.FromFactory((MultiChoiceQuestion question, Work work) => 
                    new MultiChoiceAnswer(question, work, new List<Guid>() { question.Options[0].ID})
                )
            );
        }
    }
}
