using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Users.BulkImportReport
{
    public class BulkImportEventType : Enumeration<BulkImportEventType>
    {
        // для EF
        private BulkImportEventType(){}

        private BulkImportEventType(int id, string name) : base(id, name) { }

        public static readonly BulkImportEventType NewStudentSuccessfullyImported = 
            new(1, "Новая учётная запись для учащегося создана");

        public static readonly BulkImportEventType ExistingUserSuccessfullyAddedToStudents = 
            new(2, "Существующий пользователь успешно зарегистрирован как учащийся.");

        public static readonly BulkImportEventType StudentSuccessfullyAddedToGroup =
            new(3, "Учащийся успешно добавлен в группу.");

        public static readonly BulkImportEventType ImportErrorGroupIsDisbanded =
            new(4, "Ошибка импорта - группа была расформирована");

        public static readonly BulkImportEventType ImportErrorGroupIsOutdated =
            new(5, "Ошибка импорта - срок существования группы истёк");

        public static readonly BulkImportEventType ImportErrorGroupNotFound =
            new(6, "Ошибка импорта - группа не найдена");

        public static readonly BulkImportEventType StudentAlreadyInGroup =
            new(7, "Учащийся уже состоит в группе");

        public static readonly BulkImportEventType UnknownError =
            new(8, "Неизвестная ошибка, см. запись в отчёте");
    }
}
