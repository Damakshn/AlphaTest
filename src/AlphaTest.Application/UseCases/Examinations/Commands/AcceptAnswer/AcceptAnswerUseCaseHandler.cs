using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Infrastructure.Auth;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public abstract class AcceptAnswerUseCaseHandler<TAnswer, TQuestion, TAcceptAnswerUseCaseRequest> 
        : UseCaseHandlerBase<TAcceptAnswerUseCaseRequest> 
        where TAnswer : Answer
        where TAcceptAnswerUseCaseRequest : AcceptAnswerUseCaseRequest
        where TQuestion : Question
    {
        public AcceptAnswerUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(TAcceptAnswerUseCaseRequest request, CancellationToken cancellationToken)
        {
            Examination examination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);

            // ToDo auth
            #region костыль
            AppUser dummyStudent = await _db.Users.Aggregates().FindByUsername("dummystudent@mail.ru");
            Guid studentID = dummyStudent.Id;
            #endregion

            /*
            ToDo есть несколько сценариев, почему ответ нельзя принять
                - тестирование завершено;
                - время тестирования истекло;
                    ToDo причина завершения теста не фиксируется нигде, поэтому разница между этими двумя событиями не видна
                - не найдена работа - тестирование не начиналось;
                * вариант с несуществующим пользователем не рассматриваем, потому что ID пользователя приходит через механизм аутентификации;
                * Если экзамен не существует, в самом начале вылетет исключение
            */
            Work activeWork = await _db.Works.Aggregates().GetActiveWork(request.ExaminationID, studentID);
            if (activeWork is null)
            {
                throw new AlphaTestApplicationException($"Работа учащегося с ID={studentID} для экзамена ID={request.ExaminationID} не найдена.");
            }

            Test test = await _db.Tests.Aggregates().FindByID(examination.TestID);

            uint retriesUsed = await _db.Answers.GetNumberOfRetriesUsed(activeWork.ID, request.QuestionID);

            Answer lastAnswer = await _db.Answers.Aggregates().GetLastActiveAnswerForQuestion(activeWork.ID, request.QuestionID);
            if (lastAnswer is not null)
            {   
                lastAnswer.Revoke(test, activeWork);
            }


            Question question = await _db.Questions.Aggregates().FindByID(request.QuestionID);
            if (question is not TQuestion)
            {
                throw new AlphaTestApplicationException("Тип вопроса не соответствует форме ответа.");
            }

            _db.Answers.Add(MakeAnswer(request, activeWork, test, question as TQuestion));
            _db.SaveChanges();
            return Unit.Value;
        }

        /* ToDo занести в документацию: при наличии принятого ответа на вопрос 
         * можно отправить новый ответ, и, если кол-во попыток не исчерпано, 
         * то старый ответ будет автоматически отозван а новый принят как активный.
         */
        protected abstract TAnswer MakeAnswer(TAcceptAnswerUseCaseRequest request, Work work, Test test, TQuestion question);
    }
}
