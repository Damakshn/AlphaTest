using System.Linq;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.DataAccess.EF.Abstractions;
using System.Collections.Generic;
using AlphaTest.Application.Exceptions;

namespace AlphaTest.Application.UseCases.Tests.Commands.QuestionList
{
    public class DeleteQuestionUseCaseHandler : EditQuestionListUseCaseHandler<DeleteQuestionUseCaseRequest>
    {
        public DeleteQuestionUseCaseHandler(IDbContext db) : base(db) { }

        public override void ExecuteAction(List<Question> questions, DeleteQuestionUseCaseRequest request)
        {   
            Question questionToDelete = questions.FirstOrDefault(q => q.ID == request.QuestionID);
            if (questionToDelete is null)
                throw new AlphaTestApplicationException($"Операция невозможна - вопрос с ID={request.QuestionID} не найден");
            questions.Remove(questionToDelete);
            _db.Questions.Remove(questionToDelete);
        }
    }
}
