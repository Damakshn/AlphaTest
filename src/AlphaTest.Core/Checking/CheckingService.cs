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
            CheckingVisitor checkingVisitor = new(activeAnswersInAttempt);

            foreach(var question in questionsInTest.Where(q => q is IAutoCheckQuestion))
            {
                // MAYBE использовать ФП?
                PreliminaryResult preliminaryResult = (question as IAutoCheckQuestion).AcceptCheckingVisitor(checkingVisitor);
                PreliminaryResult adjustedResult = test.CheckingPolicy.AdjustPreliminaryResult(preliminaryResult);
                results.Add(new(adjustedResult));
            }

            return results;
        }
    }
}
