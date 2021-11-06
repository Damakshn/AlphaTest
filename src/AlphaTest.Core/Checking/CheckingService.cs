using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;


namespace AlphaTest.Core.Checking
{
    public class CheckingService
    {
        public List<CheckResult> CalculateCheckResults(Test test, List<Question> questionsInTest, List<Answer> activeAnswersInWork)
        {
            List<CheckResult> results = new();
            CheckingVisitor checkingVisitor = new(activeAnswersInWork);

            foreach(var question in questionsInTest.Where(q => q is IAutoCheckQuestion))
            {   
                AdjustedResult adjustedResult = (question as IAutoCheckQuestion)
                    .AcceptCheckingVisitor(checkingVisitor)
                    .AdjustWithCheckingPolicy(test.CheckingPolicy);
                    
                results.Add(new(adjustedResult));
            }

            return results;
        }
    }
}
