using AlphaTest.Core.Common.Exceptions;
using System;

namespace AlphaTest.Application.Exceptions
{
    public class AlphaTestApplicationException : AlphaTestException
    {
        public AlphaTestApplicationException() { }

        public AlphaTestApplicationException(string message) : base(message) { }

        public AlphaTestApplicationException(string message, Exception inner) : base(message, inner) { }
    }
}
