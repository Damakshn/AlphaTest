using System;

namespace AlphaTest.Application.Models.Tests
{
    public class TestsListItemDto
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Topic { get; set; }

        public int Version { get; set; }

        public string Status { get; set; }

        public string Author { get; set; }

        public string AuthorEmail { get; set; }
    }
}
