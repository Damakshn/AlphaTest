using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlphaTest.Core.Common.Abstractions
{
    public abstract class Enumeration<TEnum> :
        IEquatable<Enumeration<TEnum>>,
        IComparable<Enumeration<TEnum>>
        where TEnum : Enumeration<TEnum>
    {
        public int ID { get; private set; }

        public string Name { get; private set; }

        #region Конструкторы
        protected Enumeration() { }

        public Enumeration(int id, string name)
        {
            ID = id;
            Name = name;
        }
        #endregion

        #region Получение списка опций
        protected static List<TEnum> GetAll()
        {
            return typeof(TEnum)
                .GetFields(BindingFlags.Public |
                           BindingFlags.Static |
                           BindingFlags.DeclaredOnly)
                .Where(f => f.FieldType.IsAssignableFrom(typeof(TEnum)))
                .Select(f => f.GetValue(null))
                .Cast<TEnum>()
                .OrderBy(t => t.ID)
                .ToList();
        }

        public static readonly IReadOnlyCollection<TEnum> All = GetAll().AsReadOnly();
        #endregion

        #region Парсинг

        #region Обычный парсинг
        public static TEnum ParseFromID(int id)
        {
            if (id == 0) return null;
            TEnum match = All.FirstOrDefault(e => e.ID == id);
            if (match is null)
                throw new ArgumentException($"{id} is not valid ID for {typeof(TEnum).Name}", nameof(id));
            return match;
        }

        public static TEnum ParseFromName(string name)
        {
            TEnum match = All.FirstOrDefault(e => e.Name == name);
            if (match is null)
                throw new ArgumentException($"{name} is not valid name for {typeof(TEnum).Name}", nameof(name));
            return match;
        }
        #endregion

        #region Парсинг через Try-паттерн
        public static bool TryParseFromID(int id, out TEnum match)
        {
            if (id == 0)
            {
                match = null;
                return true;
            }
            match = All.FirstOrDefault(e => e.ID == id);
            if (match is null)
            {
                return false;
            }
            return true;
        }

        public static bool TryParseFromName(string name, out TEnum match)
        {
            match = All.FirstOrDefault(e => e.Name.ToLower() == name.ToLower());
            if (match is null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #endregion

        #region ToString, GetHashCode, IEquatable, IComparable, операторы, и т.д.
        public override string ToString() => Name;

        public override int GetHashCode() => ID.GetHashCode();

        public override bool Equals(object obj) => (obj is TEnum other) && Equals(other);


        public bool Equals(Enumeration<TEnum> other)
        {
            if (Object.ReferenceEquals(this, other))
                return true;

            if (other is null)
                return false;

            return ID == other.ID;
        }

        public static bool operator ==(Enumeration<TEnum> left, Enumeration<TEnum> right)
        {
            if (left is null)
                return (right is null);
            return left.Equals(right);
        }

        public static bool operator !=(Enumeration<TEnum> left, Enumeration<TEnum> right)
            => !(left == right);

        public int CompareTo(Enumeration<TEnum> other) => ID.CompareTo(other.ID);
        #endregion
    }
}
