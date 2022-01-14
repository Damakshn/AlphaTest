using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTest.Core.Common.Utils
{
    public static class TimeResolver
    {
        private static DateTime? _customizedTime;

        public static void SetTime(DateTime customizedTime)
        {
            _customizedTime = customizedTime;
        }

        public static void ResetTime()
        {
            _customizedTime = null;
        }

        public static DateTime CurrentTime => _customizedTime.HasValue ? DateTime.UtcNow : _customizedTime.Value;
    }
}
