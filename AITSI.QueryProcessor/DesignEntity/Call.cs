namespace AITSI.QueryProcessor.DesignEntity
{
    public class Call : Statement
    {
        public Call(string name) : base(name) { }

        public Call(string name, int number) : base(name, number) { }
    }
}