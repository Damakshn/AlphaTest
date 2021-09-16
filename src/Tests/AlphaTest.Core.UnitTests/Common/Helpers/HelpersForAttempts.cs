using AlphaTest.Core.Attempts;
using System;
using System.Reflection;

namespace AlphaTest.Core.UnitTests.Common.Helpers
{
    public static class HelpersForAttempts
    {
        public static void SetAttemptForcedEndDate(Attempt attempt, DateTime forcedEndAt)
        {
            if (attempt is null)
                throw new ArgumentNullException(nameof(attempt));
            
            var forcedEndAtProperty = attempt.GetType().GetProperty("ForceEndAt", BindingFlags.Public | BindingFlags.Instance);
            if (forcedEndAtProperty is null)
                throw new InvalidOperationException($"Свойство ForceEndAt не найдено в типе {attempt.GetType()}");
            
            forcedEndAtProperty.SetValue(attempt, forcedEndAt);
        }
    }
}
