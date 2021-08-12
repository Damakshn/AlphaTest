namespace AlphaTest.Core.Common.Abstractions
{
    public abstract class Enumeration
    {
        public int ID { get; }

        public string Name { get; private set; }

        protected Enumeration(){}

        public Enumeration(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString() => Name;


        
    }
}
