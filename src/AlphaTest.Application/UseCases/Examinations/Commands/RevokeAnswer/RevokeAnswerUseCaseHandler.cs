using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Works;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Examinations.Commands.RevokeAnswer
{
    public class RevokeAnswerUseCaseHandler : UseCaseHandlerBase<RevokeAnswerUseCaseRequest>
    {
        public RevokeAnswerUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(RevokeAnswerUseCaseRequest request, CancellationToken cancellationToken)
        {
            Examination currentExamination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);

            /*
            ToDo есть несколько сценариев, почему ответ нельзя ОТОЗВАТЬ
                - тестирование завершено;
                - время тестирования истекло;
                    ToDo причина завершения теста не фиксируется нигде, поэтому разница между этими двумя событиями не видна
                - не найдена работа - тестирование не начиналось;
            */
            Work currentWorkOfTheStudent = await _db.Works.Aggregates().GetActiveWork(request.ExaminationID, request.StudentID);
            if (currentWorkOfTheStudent is null)
            {
                throw new AlphaTestApplicationException($"Работа учащегося с ID={request.StudentID} для экзамена ID={request.ExaminationID} не найдена.");
            }

            Test test = await _db.Tests.Aggregates().FindByID(currentExamination.TestID);
            
            Answer latestAnswer = await _db.Answers.Aggregates().GetLatestAnswerForQuestion(currentWorkOfTheStudent.ID, request.QuestionID);
            if (latestAnswer is null)
                throw new AlphaTestApplicationException("Действие невозможно - ответ на вопрос не был отправлен.");
            if (latestAnswer.IsRevoked)
                throw new AlphaTestApplicationException("Действие невозможно последний отправленный ответ уже отозван.");
            latestAnswer.Revoke(test, currentWorkOfTheStudent);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
