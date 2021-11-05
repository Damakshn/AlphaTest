using AlphaTest.Core.Works;
using System;
using System.Reflection;

namespace AlphaTest.Core.UnitTests.Common.Helpers
{
    public static class HelpersForWorks
    {
        public static void SetWorkForcedEndDate(Work work, DateTime forcedEndAt)
        {
            if (work is null)
                throw new ArgumentNullException(nameof(work));
            
            var forcedEndAtProperty = work.GetType().GetProperty("ForceEndAt", BindingFlags.Public | BindingFlags.Instance);
            if (forcedEndAtProperty is null)
                throw new InvalidOperationException($"Свойство ForceEndAt не найдено в типе {work.GetType()}");
            
            forcedEndAtProperty.SetValue(work, forcedEndAt);
        }
    }
}
