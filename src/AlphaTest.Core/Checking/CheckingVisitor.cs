using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests.Questions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaTest.Core.Checking
{
    public class CheckingVisitor
    {
        private readonly IEnumerable<Answer> _activeAnswersFromWork;
        
        internal CheckingVisitor(IEnumerable<Answer> activeAnswersFromWork)
        {
            _activeAnswersFromWork = activeAnswersFromWork;
        }

        internal PreliminaryResult CheckSingleChoiceQuestion(SingleChoiceQuestion question)
        {
            var answer = _activeAnswersFromWork.Where(a => a.QuestionID == question.ID).FirstOrDefault();
            if (answer is null)
                return null;
            if (answer is not SingleChoiceAnswer convertedAnswer)
                throw new InvalidOperationException($"{nameof(CheckSingleChoiceQuestion)} - Типы вопроса и ответа не соответствуют друг другу ({question.GetType()} и {answer.GetType()})");

            bool isRight = 
                question.Options.Where(o => o.IsRight).Select(o => o.ID).First() == convertedAnswer.RightOptionID;
            if (isRight)
                return new PreliminaryResult(question, answer, question.Score.Value, CheckResultType.Credited);
            else
                return new PreliminaryResult(question, answer, 0, CheckResultType.NotCredited);
        }

        internal PreliminaryResult CheckMultiChoiceQuestion(MultiChoiceQuestion question)
        {
            var answer = _activeAnswersFromWork.Where(a => a.QuestionID == question.ID).FirstOrDefault();
            if (answer is null)
                return null;
            if (answer is not MultiChoiceAnswer convertedAnswer)
                throw new InvalidOperationException($"{nameof(CheckMultiChoiceQuestion)} - Типы вопроса и ответа не соответствуют друг другу ({question.GetType()} и {answer.GetType()})");

            decimal preliminaryScore = 0;
            CheckResultType preliminaryVerdict = CheckResultType.NotCredited;

            bool allRightOptionsSelected =
                question.Options.Where(o => o.IsRight).All(o => convertedAnswer.RightOptions.Contains(o.ID));

            if (allRightOptionsSelected)
            {
                preliminaryScore = question.Score.Value;
                // если помимо верных ответов выбрано что-то лишнее, то ответ засчитывается как частично верный
                if (convertedAnswer.RightOptions.Count > question.Options.Where(o => o.IsRight).Count())
                    preliminaryVerdict = CheckResultType.PartiallyCredited;
                else
                    preliminaryVerdict = CheckResultType.Credited;
            }
            else
            {
                foreach (var rightOption in question.Options.Where(o => o.IsRight))
                {
                    if (convertedAnswer.RightOptions.Contains(rightOption.ID))
                        preliminaryScore += question.Score.Value / question.Options.Count;
                }
                preliminaryVerdict = preliminaryScore == 0 ? CheckResultType.NotCredited : CheckResultType.PartiallyCredited;
            }
            return new PreliminaryResult(question, answer, preliminaryScore, preliminaryVerdict);
        }

        internal PreliminaryResult CheckQuestionWithTextualAnswer(QuestionWithTextualAnswer question)
        {
            var answer = _activeAnswersFromWork.Where(a => a.QuestionID == question.ID).FirstOrDefault();
            if (answer is null)
                return null;
            if (answer is not ExactTextualAnswer convertedAnswer)
                throw new InvalidOperationException($"{nameof(CheckQuestionWithTextualAnswer)} - Типы вопроса и ответа не соответствуют друг другу ({question.GetType()} и {answer.GetType()})");

            if (question.RightAnswer == convertedAnswer.Value)
                return new PreliminaryResult(question, answer, question.Score.Value, CheckResultType.Credited);
            else
                return new PreliminaryResult(question, answer, 0, CheckResultType.NotCredited);
        }

        internal PreliminaryResult CheckQuestionWithNumericAnswer(QuestionWithNumericAnswer question)
        {
            var answer = _activeAnswersFromWork.Where(a => a.QuestionID == question.ID).FirstOrDefault();
            if (answer is null)
                return null;
            if (answer is not ExactNumericAnswer convertedAnswer)
                throw new InvalidOperationException($"{nameof(CheckQuestionWithNumericAnswer)} - Типы вопроса и ответа не соответствуют друг другу ({question.GetType()} и {answer.GetType()})");

            if (question.RightAnswer == convertedAnswer.Value)
                return new PreliminaryResult(question, answer, question.Score.Value, CheckResultType.Credited);
            else
                return new PreliminaryResult(question, answer, 0, CheckResultType.NotCredited);
        }
    }
}
