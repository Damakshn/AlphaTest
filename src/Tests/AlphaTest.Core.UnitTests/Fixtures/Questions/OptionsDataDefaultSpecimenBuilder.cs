using System;
using AutoFixture.Kernel;
using System.Collections.Generic;
using System.Reflection;

namespace AlphaTest.Core.UnitTests.Fixtures.Questions
{
    internal class OptionsDataDefaultSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            
            var t = (request as Type);
            if (t is not null && t == typeof(IEnumerable<(string text, uint number, bool isRight)>))
                return new List<(string text, uint number, bool isRight)>
                {
                    new("Первый вариант", 1, true),
                    new("Второй вариант", 2, false),
                    new("Третий вариант", 3, false)
                };

            // TBD - написано по документации, но не работает, как будто параметра optionsData нигде нет
            /*
            var pi = (request as ParameterInfo); 
            if (pi is not null &&
                pi.ParameterType == typeof(IEnumerable<(string text, uint number, bool isRight)>) &&
                pi.Name == "optionsData")
            {   
                return new List<(string text, uint number, bool isRight)>
                {
                    new("Первый вариант", 1, true),
                    new("Второй вариант", 2, false),
                    new("Третий вариант", 3, false)
                };
            }*/
            return new NoSpecimen();
        }
    }
}
