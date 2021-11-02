using System;

namespace AlphaTest.Core.Groups
{
    public interface IGroupUniquenessChecker
    {
        // ToDo добавить пояснение, для чего служит каждый из методов
        // для добавления
        bool CheckIfGroupExists(string name, DateTime beginDate, DateTime endDate);

        // для редактирования
        bool CheckIfOneMoreGroupExists(string name, DateTime beginDate, DateTime endDate, Guid id);
    }
}
