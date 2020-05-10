namespace AITSI.QueryProcessor.DesignEntity
{
    public class Assign : Statement
    {
        public Assign(string name) : base(name) { }

        public Assign(string name, int number) : base(name, number) { }
    }
}