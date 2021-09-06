using System;
using Xunit;
using AlphaTest.Core.Common;
using AlphaTest.Core.Common.Exceptions;

namespace AlphaTest.Core.UnitTests.Common
{
    public abstract class UnitTestBase
    {
        public static void AssertBrokenRule<TRule>(Action testAction) where TRule : class, IBusinessRule
        {
            // ToDo если нет ошибки - одно сообщение, если нарушено другое правило - другое сообщение
            string message = $"Правило {typeof(TRule).Name} не было нарушено.";
            BusinessException exception = Assert.Throws<BusinessException>(testAction);
            if (exception is not null)
            {
                Assert.True(exception.BrokenRule is TRule, message);
            }
        }
    }
}
