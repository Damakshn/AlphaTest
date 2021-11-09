using System.Reflection;
using AutoFixture.Kernel;


namespace AlphaTest.Core.UnitTests.Fixtures.Answers
{
    internal class AnswersAcceptedSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {   
            var pi = (request as ParameterInfo);
            uint answersAccepted = 0;
            if (pi is not null &&
                pi.Name == "answersAccepted" &&
                pi.ParameterType == typeof(uint))
                return answersAccepted;
            return new NoSpecimen();
        }
    }
}
