using AlphaTest.Core.Answers;
using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;
using AutoFixture;

namespace AlphaTest.Core.UnitTests.Fixtures.Answers
{
    internal class DefaultSingleChoiceAnswerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<SingleChoiceAnswer>(c =>
                c.FromFactory((SingleChoiceQuestion question, Work work) =>
                    new SingleChoiceAnswer(question, work, question.Options[0].ID)
                )
            );
        }
    }
}
