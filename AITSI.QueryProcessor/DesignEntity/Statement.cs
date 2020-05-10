namespace AITSI.QueryProcessor.DesignEntity
{
    public class Statement : AbstractDesignEntity
    {
        public int StatementNumber { get; set; }

        public Statement(string name) : base(name) { }

        public Statement(string name, int number) : base(name)
        {
            this.StatementNumber = number;
        }
    }
}