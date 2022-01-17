using System;

namespace AlphaTest.Core.Common.Utils
{
    public static class TimeResolver
    {
        private static DateTime? _customizedTime;
        private static TimeZoneInfo _timeZoneInfo;

        public static DateTime CurrentTime => _customizedTime ?? DateTime.UtcNow;

        public static void SetTime(DateTime customizedTime)
        {
            _customizedTime = customizedTime;
        }

        public static void ResetTime()
        {
            _customizedTime = null;
        }

        public static void SetTimeZone(string serializedString)
        {   
            _timeZoneInfo = TimeZoneInfo.FromSerializedString(serializedString);
        }

        public static DateTime ToUtc(DateTime input)
        {   
            return input.Kind == DateTimeKind.Unspecified 
                ? TimeZoneInfo.ConvertTimeToUtc(input, _timeZoneInfo)
                : input.ToUniversalTime();
        }

        public static DateTime ToLocal(DateTime utc)
        {   
            return TimeZoneInfo.ConvertTimeFromUtc(utc, _timeZoneInfo);
        }
    }
}
