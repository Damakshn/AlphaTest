using System;
using System.Collections.Generic;
using System.Text;


namespace AlphaTest.Application.Notifications.Messages.Examinations
{
    class ExaminationAccessNotification : IBroadcastNotification
    {
        private readonly string _testTitle;
        private readonly string _testTopic;
        private readonly string _examinationUrl;
        private readonly DateTime _examStart;
        private readonly DateTime _examEnd;
        private readonly List<string> _audience;

        public ExaminationAccessNotification(List<string> audience, string testTitle, string testTopic, string examinationUrl, DateTime examStart, DateTime examEnd)
        {
            _testTitle = testTitle;
            _testTopic = testTopic;
            _examinationUrl = examinationUrl;
            _examStart = examStart;
            _examEnd = examEnd;
            _audience = audience;
        }

        public string Message
        {
            get
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append("Уважаемые студенты!");
                stringBuilder.Append($"Вам предоставлен доступ к выполнению тестового задания {_testTitle}: {_testTopic}.");
                stringBuilder.Append($"Сроки сдачи теста - с {_examStart} по {_examEnd}");
                stringBuilder.Append($"Чтобы приступить к сдаче теста, пройдите по ссылке - {_examinationUrl}");
                stringBuilder.Append(MailSignature.AlphaTestSignature);
                return stringBuilder.ToString();
            }
        }

        public List<string> Audience  => _audience;

        
    }
}
