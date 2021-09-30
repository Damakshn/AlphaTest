using AlphaTest.Core.Answers;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;
using AutoFixture;

namespace AlphaTest.Core.UnitTests.Fixtures.Answers
{
    internal class DefaultSingleChoiceAnswerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<SingleChoiceAnswer>(c =>
                c.FromFactory((SingleChoiceQuestion question, Attempt attempt) =>
                    new SingleChoiceAnswer(question, attempt, question.Options[0].ID)
                )
            );
        }
    }
}
