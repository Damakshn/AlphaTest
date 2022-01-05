using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

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
            
            Examination currentExamination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);

            /*
            ToDo есть несколько сценариев, почему ответ нельзя принять
                - тестирование завершено;
                - время тестирования истекло;
                    ToDo причина завершения теста не фиксируется нигде, поэтому разница между этими двумя событиями не видна
                - не найдена работа - тестирование не начиналось;
                * вариант с несуществующим пользователем не рассматриваем, потому что ID пользователя приходит через механизм аутентификации;
                * Если экзамен не существует, в самом начале вылетет исключение
            */
            Work currentWorkOfTheStudent = await _db.Works.Aggregates().GetActiveWork(request.ExaminationID, request.StudentID);
            if (currentWorkOfTheStudent is null)
            {
                throw new AlphaTestApplicationException($"Работа учащегося с ID={request.StudentID} для экзамена ID={request.ExaminationID} не найдена.");
            }

            Test test = await _db.Tests.Aggregates().FindByID(currentExamination.TestID);
            Question questionBeingAnswered = await _db.Questions.Aggregates().FindByID(request.QuestionID);

            // отзываем предыдущий ответ на этот вопрос, если он есть
            Answer latestAnswer = await _db.Answers.Aggregates().GetLatestActiveAnswerForQuestion(currentWorkOfTheStudent.ID, request.QuestionID);
            if (latestAnswer is not null)
            {   
                latestAnswer.Revoke(test, currentWorkOfTheStudent);
            }

            // проверяем вопрос на соответствие типу
            if (questionBeingAnswered is not TQuestion)
            {
                throw new AlphaTestApplicationException("Тип вопроса не соответствует форме ответа.");
            }

            uint numberOfAnswersAlreadyAccepted = await _db.Answers.GetNumberOfAcceptedAnswers(currentWorkOfTheStudent.ID, request.QuestionID);
            TAnswer registeredAnswer = MakeAnswer(
                request,
                currentWorkOfTheStudent,
                test,
                questionBeingAnswered as TQuestion,
                numberOfAnswersAlreadyAccepted);

            _db.Answers.Add(registeredAnswer);
            _db.SaveChanges();
            return Unit.Value;
        }

        /* ToDo занести в документацию: при наличии принятого ответа на вопрос 
         * можно отправить новый ответ, и, если кол-во попыток не исчерпано, 
         * то старый ответ будет автоматически отозван а новый принят как активный.
         */
        protected abstract TAnswer MakeAnswer(TAcceptAnswerUseCaseRequest request, Work work, Test test, TQuestion question, uint answersAccepted);
    }
}
