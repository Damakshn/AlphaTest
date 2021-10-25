using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTest.Infrastructure.Database.Exceptions
{
    internal class EntityNotFoundException : DatabaseLevelException
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
