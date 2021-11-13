using AlphaTest.Core.Common.Abstractions;
using System.Collections.Generic;

namespace AlphaTest.Core.Works
{
    public class WorkFinishReason : Enumeration<WorkFinishReason>
    {
        // для EF
        public WorkFinishReason() :base() { }

        public WorkFinishReason(int id, string name): base(id, name) { }

        public static readonly WorkFinishReason TestTimeLimitExpired = new(1, "Истекло время, отведённое для тестирования.");

        public static readonly WorkFinishReason ExaminationTimeLimitExpired = new(2, "Истекло время экзамена");

        public static readonly WorkFinishReason AutoFinish = new(3, "Автоматическое завершение");

        public static readonly WorkFinishReason ManualFinish = new(4, "Завершено вручную");

        public static IReadOnlyCollection<WorkFinishReason> ForceEndReasons => 
            new List<WorkFinishReason>() { TestTimeLimitExpired, ExaminationTimeLimitExpired }.AsReadOnly();
    }
}
