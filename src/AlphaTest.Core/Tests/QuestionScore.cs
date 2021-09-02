using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Rules;

namespace AlphaTest.Core.Tests
{
    public class QuestionScore: ValueObject, IEquatable<QuestionScore>
    {
        public int Value { get; }

        public QuestionScore(int value)
        {
            CheckRule(new QuestionScoreMustBeInRangeRule((uint)value));
            Value = value;
        }

        public override bool Equals(object obj) => (obj is QuestionScore other) && Equals(other);

        public bool Equals(QuestionScore other)
        {
            if (other is null) return false;
            return this.Value == other.Value;
        }

        public static bool operator == (QuestionScore left, QuestionScore right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(QuestionScore left, QuestionScore right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
