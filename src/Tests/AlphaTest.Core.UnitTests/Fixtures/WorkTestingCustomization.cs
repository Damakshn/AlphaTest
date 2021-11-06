using AlphaTest.Core.UnitTests.Fixtures.Examinations;
using AlphaTest.Core.UnitTests.Fixtures.Tests;
using AutoFixture;
using AutoFixture.Kernel;
using System.Reflection;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class WorkTestingCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize(new DefaultTestCustomization());
            fixture.Customize(new DefaultExaminationCustomization());
            fixture.Customizations.Add(new AttemptsSpentSpecimenBuilder());
        }
    }

    internal class AttemptsSpentSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var p = (request as ParameterInfo);
            uint attemptsSpent = 0;
            if (p is not null &&
                p.Name == "attemptsSpent" &&
                p.ParameterType == typeof(uint))
                return attemptsSpent;
            return new NoSpecimen();
        }
    }
}
