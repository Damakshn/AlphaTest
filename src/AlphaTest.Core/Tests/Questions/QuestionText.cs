using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionText : ValueObject, IEquatable<QuestionText>
    {
        public string Value { get; }

        public QuestionText(string value)
        {
            CheckRule(new QuestionTextLengthMustBeInRangeRule(value));
            Value = value;
        }

        public override bool Equals(object obj) => (obj is QuestionText other) && Equals(other);

        public bool Equals(QuestionText other)
        {
            if (other is null) return false;
            return this.Value == other.Value;
        }

        public static bool operator ==(QuestionText left, QuestionText right)
        {
            if (left is null)
            {
                return right is null;
            }
            return left.Equals(right);
        }

        public static bool operator !=(QuestionText left, QuestionText right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
