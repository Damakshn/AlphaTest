using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;
using AlphaTest.Application.Notifications;
using AlphaTest.Application.Notifications.Helpers;
using AlphaTest.Application.Notifications.Messages.Examinations;


namespace AlphaTest.Application.UseCases.Schedule.Commands.CreateExamination
{
    public class CreateExaminationUseCaseHandler : UseCaseHandlerBase<CreateExaminationUseCaseRequest, Guid>
    {
        private readonly ChannelWriter<INotification> _notificationQueue;

        public CreateExaminationUseCaseHandler(IDbContext db, ChannelWriter<INotification> notificationQueue) : base(db)
        {
            _notificationQueue = notificationQueue;
        }

        public override async Task<Guid> Handle(CreateExaminationUseCaseRequest request, CancellationToken cancellationToken)
        {
            #region Создание экзамена
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            AlphaTestUser examiner = await _db.Users.Aggregates().FindByID(request.UserID);

            List<Group> groups =
                request.Groups.Any()
                ? await _db.Groups.Aggregates().FindManyByIds(request.Groups)
                : new List<Group>();

            Examination examination = new(test, request.StartsAt, request.EndsAt, examiner, groups);
            _db.Examinations.Add(examination);
            await _db.SaveChangesAsync(cancellationToken);
            #endregion

            #region Отправка уведомления
            var audience = _db.Users
                .FilterByGroups(groups.Select(g => g.ID).ToList(), _db)
                .ToList()
                .ToMailingListDictionary();

            ExaminationAccessNotification notification = 
                new(audience, test.Title, test.Topic, "exam url", examination.StartsAt, examination.EndsAt);
            await _notificationQueue.WriteAsync(notification, cancellationToken);
            #endregion

            return examination.ID;
        }
    }
}
