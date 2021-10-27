using AlphaTest.Application.Exceptions;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaTest.Application.UseCases.Tests.Commands.QuestionList
{
    public class MoveQuestionUseCaseHandler : EditQuestionListUseCaseHandler<MoveQuestionsUseCaseRequest>
    {
        public MoveQuestionUseCaseHandler(AlphaTestContext db) : base(db) { }

        public override void ExecuteAction(List<Question> questions, MoveQuestionsUseCaseRequest request)
        {
            #region Подготовка
            List<(Question Question, int StartIndex, int DestIndex)> permutations = BuildPermutationsList(questions, request);
            #endregion

            #region Проверки
            var brokenIds = request.QuestionsOrder.Where(x => questions.Count(q => q.ID == x.QuestionID) == 0).Select(x => x.QuestionID).ToList();
            if (brokenIds.Any())
                throw new AlphaTestApplicationException(BuildErrorMessageForAbsentQuestions(brokenIds));

            if (permutations.Any(p => 
                p.StartIndex < 0 || 
                p.StartIndex > questions.Count - 1 || 
                p.DestIndex < 0 ||
                p.DestIndex > questions.Count - 1))
                throw new AlphaTestApplicationException("Операция невозможна - недопустимая перестановка вопросов.");
            #endregion

            #region Перестановка
            foreach (var p in permutations)
            {
                questions.Remove(p.Question);
                questions.Insert(p.DestIndex, p.Question);
            }
            #endregion
        }

        private static List<(Question Question, int StartIndex, int DestIndex)> BuildPermutationsList(List<Question> questions, MoveQuestionsUseCaseRequest request)
        {
            return questions
                .Where(q => request.QuestionsOrder.Any(x => x.QuestionID == q.ID))
                .Select(q =>
                    (q,
                    questions.IndexOf(q),
                    (int)request.QuestionsOrder.Where(x => x.QuestionID == q.ID).First().Position - 1)
                )
                .ToList();
        }

        private static string BuildErrorMessageForAbsentQuestions(List<Guid> absentQuestionsIds)
        {
            StringBuilder builder = new();
            if (absentQuestionsIds.Count > 1)
            {
                builder.Append("Операция невозможна, следующие вопросы не найдены в системе:\n");
                foreach(var id in absentQuestionsIds)
                {
                    builder.Append($"ID = {id};");
                }
            }
            else
            {
                builder.Append($"Операция невозможна - вопрос с ID={absentQuestionsIds[0]} не найден");
            }
            return builder.ToString();
        }
    }
}
