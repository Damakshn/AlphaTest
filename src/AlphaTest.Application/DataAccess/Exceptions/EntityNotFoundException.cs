using System;

namespace AlphaTest.Application.DataAccess.Exceptions
{
    internal class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
