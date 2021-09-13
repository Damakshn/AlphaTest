using System;
using System.Reflection;
using AlphaTest.Core.Examinations;

namespace AlphaTest.Core.UnitTests.Common.Helpers
{
    public static class HelpersForExaminations
    {
        public static void SetExaminationDates(Examination exam, DateTime startsAt, DateTime endsAt)
        {
            if (exam is null)
                throw new ArgumentNullException(nameof(exam));
            var startsAtProperty = exam.GetType().GetProperty("StartsAt", BindingFlags.Public | BindingFlags.Instance);
            var endsAtProperty = exam.GetType().GetProperty("EndsAt", BindingFlags.Public | BindingFlags.Instance);
            if (startsAtProperty is null)
                throw new InvalidOperationException($"Поле StartsAt не найдено в типе {exam.GetType()}");
            if (endsAtProperty is null)
                throw new InvalidOperationException($"Поле EndsAt не найдено в типе {exam.GetType()}");
            startsAtProperty.SetValue(exam, startsAt);
            endsAtProperty.SetValue(exam, endsAt);
        }
    }
}
