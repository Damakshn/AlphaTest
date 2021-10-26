using System.Linq;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;
using System.Collections.Generic;
using AlphaTest.Application.Exceptions;

namespace AlphaTest.Application.UseCases.Tests.Commands.QuestionList
{
    public class DeleteQuestionUseCaseHandler : EditQuestionListUseCaseHandler<DeleteQuestionUseCaseRequest>
    {
        public DeleteQuestionUseCaseHandler(AlphaTestContext db) : base(db) { }

        public override void ExecuteAction(List<Question> questions, DeleteQuestionUseCaseRequest request)
        {   
            Question questionToDelete = questions.FirstOrDefault(q => q.ID == request.QuestionID);
            if (questionToDelete is null)
                throw new AlphaTestApplicationException($"Операция невозможна - вопрос с ID={request.QuestionID} не найден");
            int startIndex = questions.IndexOf(questionToDelete);
            questions.Remove(questionToDelete);
            int endIndex = questions.Count - 1;
            _db.Questions.Remove(questionToDelete);

            // не делаем перенумерацию, если был удалён последний вопрос
            if (startIndex <= endIndex)
                ReorderQuestions(questions, startIndex, endIndex);

        }
    }
}
