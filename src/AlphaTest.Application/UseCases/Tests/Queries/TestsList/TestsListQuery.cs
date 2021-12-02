using AlphaTest.Application.Models.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Tests;
using System;
using System.Collections.Generic;


namespace AlphaTest.Application.UseCases.Tests.Queries.TestsList
{
    public class TestsListQuery : IUseCaseRequest<List<TestsListItemDto>>
    {
        // ToDo сортировка
        public Guid UserID { get; private set; }

        public string Title { get; private set; }

        public string Topic { get; private set; }

        public Guid? Author { get; private set; }

        public Guid? AuthorOrContributor { get; private set; }

        public List<TestStatus> Statuses { get; private set; }

        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        /// <summary>
        /// Инициализирует запрос для поиска рабочих тестов, к которым текущий
        /// пользователь имеет доступ как автор или составитель.
        /// </summary>
        /// <param name="currentUserID"></param>
        public TestsListQuery(Guid currentUserID, int pageSize, int pageNumber)
        {
            UserID = currentUserID;
            AuthorOrContributor = currentUserID;
            Statuses = new List<TestStatus>() 
            { 
                TestStatus.Draft,
                TestStatus.Published,
                TestStatus.WaitingForPublishing 
            };
            Title = null;
            Topic = null;
            Author = null;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        /// <summary>
        /// Инициализирует запрос для поиска по всему списку тестов.
        /// </summary>
        public TestsListQuery(
            string title,
            string topic,
            Guid? author,
            Guid? authorOrContributor,
            List<int> statuses,
            int pageSize,
            int pageNumber)
        {   
            Title = title;
            Topic = topic;
            Author = author;
            AuthorOrContributor = authorOrContributor;
            if (statuses is not null)
            {
                Statuses = new List<TestStatus>();
                foreach (int statusID in statuses)
                {
                    Statuses.Add(TestStatus.ParseFromID(statusID));
                }
            }
            else
            {
                Statuses = null;
            }
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
