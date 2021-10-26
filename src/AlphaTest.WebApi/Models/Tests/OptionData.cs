namespace AlphaTest.WebApi.Models.Tests
{
    // костыльный класс, добавлен из-за того, что кортежи почему-то не десериализуются
    // ToDo разобраться с десериализацией и удалить
    public class OptionData
    {
        public string Text { get; set; }

        public uint Number { get; set; }

        public bool IsRight { get; set; }
    }
}
