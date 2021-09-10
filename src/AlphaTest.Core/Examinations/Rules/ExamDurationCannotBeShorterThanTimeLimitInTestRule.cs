using System;
using AlphaTest.Core.Common;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Examinations.Rules
{
    public class ExamDurationCannotBeShorterThanTimeLimitInTestRule : IBusinessRule
    {

        private readonly DateTime _start;
        private readonly DateTime _end;
        private readonly Test _test;
        private readonly TimeSpan _examDuration;

        public ExamDurationCannotBeShorterThanTimeLimitInTestRule(DateTime start, DateTime end, Test test)
        {
            _start = start;
            _end = end;
            _test = test;
            _examDuration = _end - _start;
        }

        // ToDo сделать более внятное сообщение с указанием сколько времени по факту указано, чтобы не проверять вручную
        public string Message => "Продолжительность экзамена не может быть меньше, чем время отведённое на тест.";

        public bool IsBroken => _test.TimeLimit is null ? false : _examDuration < _test.TimeLimit;
    }
}
