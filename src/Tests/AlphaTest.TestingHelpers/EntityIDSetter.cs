using System;
using System.Reflection;
using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.TestingHelpers
{
    public static class EntityIDSetter
    {
        public static void SetIDTo(Entity entity, int ID)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var property = entity.GetType().GetProperty("ID", BindingFlags.Public | BindingFlags.Instance);
            if (property is null)
            {
                throw new InvalidOperationException($"Свойство ID не найдено у типа {entity.GetType()}");
            }
            if (property.PropertyType != typeof(int))
                throw new InvalidOperationException($"ID сущности типа {entity.GetType()} не int");
            property.SetValue(entity, ID, null);
        }

        public static void SetIDTo(Entity entity, Guid ID)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var property = entity.GetType().GetProperty("ID", BindingFlags.Public | BindingFlags.Instance);
            if (property is null)
            {
                throw new InvalidOperationException($"Свойство ID не найдено у типа {entity.GetType()}");
            }
            if (property.PropertyType != typeof(Guid))
                throw new InvalidOperationException($"ID сущности типа {entity.GetType()} не Guid");
            property.SetValue(entity, ID, null);
        }
    }
}
