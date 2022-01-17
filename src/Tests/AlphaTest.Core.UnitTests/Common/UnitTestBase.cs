using System;
using Xunit;
using AlphaTest.Core.Common;
using AlphaTest.Core.Common.Utils;
using AlphaTest.Core.Common.Exceptions;

namespace AlphaTest.Core.UnitTests.Common
{
    public abstract class UnitTestBase
    {
        
        public UnitTestBase()
        {   
            string timeZoneStr = Environment.GetEnvironmentVariable("ALPHATEST__TIMEZONE");
            TimeResolver.SetTimeZone(timeZoneStr);
        }
        
        public static void AssertBrokenRule<TRule>(Action testAction) where TRule : class, IBusinessRule
        {
            // ToDo если нет ошибки - одно сообщение, если нарушено другое правило - другое сообщение
            BusinessException exception = Assert.Throws<BusinessException>(testAction);
            if (exception is not null)
            {
                string message = $"Правило {typeof(TRule).Name} не было нарушено, ошибка - {exception.Message}.";
                Assert.True(exception.BrokenRule is TRule, message);
            }
        }
    }
}
