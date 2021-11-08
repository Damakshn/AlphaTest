using AlphaTest.Core.Answers;
using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;
using AutoFixture;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.UnitTests.Fixtures.Answers
{
    internal class DefaultSingleChoiceAnswerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<SingleChoiceAnswer>(c =>
                c.FromFactory((SingleChoiceQuestion question, Work work, Test test) =>
                    new SingleChoiceAnswer(question, work, test, 0, question.Options[0].ID)
                )
            );
        }
    }
}
