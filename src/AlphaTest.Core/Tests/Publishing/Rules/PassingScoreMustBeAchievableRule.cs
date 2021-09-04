using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class PassingScoreMustBeAchievableRule : IBusinessRule
    {
        private readonly uint _passingScore;
        private readonly List<Question> _allQuestionsInTest;
        private readonly uint _totalScore;

        public PassingScoreMustBeAchievableRule(uint passingScore, List<Question> allQuestionsInTest)
        {
            _passingScore = passingScore;
            _allQuestionsInTest = allQuestionsInTest;
            _totalScore = (uint)_allQuestionsInTest.Sum(q => q.Score.Value);
        }

        public string Message => $"Проходной балл теста - {_passingScore}, максимально достижимый балл - {_totalScore}.";

        public bool IsBroken => _totalScore < _passingScore;
    }
}
