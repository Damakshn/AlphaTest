using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;


namespace AlphaTest.Core.Checking
{
    public class CheckingService
    {
        public List<CheckResult> CalculateCheckResults(Test test, List<Question> questionsInTest, List<Answer> activeAnswersInAttempt)
        {
            List<CheckResult> results = new();

            foreach(var question in questionsInTest.Where(q => q is IAutoCheckQuestion))
            {
                var answer = activeAnswersInAttempt.Where(a => a.QuestionID == question.ID).FirstOrDefault();
                if (answer is not null)
                {
                    PreliminaryResult preliminaryResult = (question as IAutoCheckQuestion).CheckAnswer(answer);
                    PreliminaryResult adjustedResult = test.CheckingPolicy.AdjustPreliminaryResult(preliminaryResult);
                    // проверка выполнена автоматически, поэтому teacherID == null
                    results.Add(new(answer, null, adjustedResult));
                }
            }

            return results;
        }
    }
}
