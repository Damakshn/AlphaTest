using System;

namespace AlphaTest.Core.Common.Exceptions
{
    public abstract class AlphaTestException: Exception
    {
        public AlphaTestException() { }

        public AlphaTestException(string message) : base(message) { }

        public AlphaTestException(string message, Exception inner) : base(message, inner) { }
    }
}
