using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.Questions;


namespace AlphaTest.Core.Checking
{
    public class CheckingService
    {
        public int CalculateWorkScore(Test test, List<Question> questionsInTest, List<Answer> activeAnswerInAttempt)
        {
            int result = 0; ;
            foreach (var question in questionsInTest.Where(q => q is IAutoCheckQuestion))
            {
                var answer = activeAnswerInAttempt.Where(a => a.QuestionID == question.ID).FirstOrDefault();
                if (answer is not null)
                {
                    int preliminaryResult;
                    bool answerIsRight = (question as IAutoCheckQuestion).IsRight(answer);
                    if (answerIsRight)
                    {
                        preliminaryResult = question.Score.Value;
                    }
                    else
                    {
                        if (test.CheckingPolicy == CheckingPolicy.HARD)
                            preliminaryResult = question.Score.Value * -1;
                        else
                            preliminaryResult = 0;
                    }   
                    result += preliminaryResult;
                }
            }
            return result;
        }
    }
}
