using System;
using System.Reflection;
using AlphaTest.Core.Groups;

namespace AlphaTest.Core.UnitTests.Common.Helpers
{
    public static class HelpersForGroups
    {
        public static Group CreateGroup(GroupTestData data)
        {
            return new Group(data.Name, data.BeginDate, data.EndDate, data.GroupAlreadyExists);
        }

        public static void SetGroupDates(Group group, DateTime beginDate, DateTime endDate)
        {
            if (group is null) 
                throw new ArgumentNullException(nameof(group));
            var beginDateProperty = group.GetType().GetProperty("BeginDate", BindingFlags.Public | BindingFlags.Instance);
            var endDateProperty = group.GetType().GetProperty("EndDate", BindingFlags.Public | BindingFlags.Instance);
            if (beginDateProperty is null)
                throw new InvalidOperationException($"Свойство BeginDate не найдено в тип {group.GetType()}.");
            if (endDateProperty is null)
                throw new InvalidOperationException($"Свойство EndDate не найдено в тип {group.GetType()}.");
            beginDateProperty.SetValue(group, beginDate);
            endDateProperty.SetValue(group, endDate);
        }
    }
}
