using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaTest.Core.Common.Exceptions;

namespace AlphaTest.Infrastructure.Database.Exceptions
{
    internal class DatabaseLevelException : AlphaTestException
    {
        public DatabaseLevelException() { }

        public DatabaseLevelException(string message) : base(message) { }

        public DatabaseLevelException(string message, Exception inner) : base(message, inner) { }
    }
}
