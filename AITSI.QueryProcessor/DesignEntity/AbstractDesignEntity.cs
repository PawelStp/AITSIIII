namespace AITSI.QueryProcessor.DesignEntity
{
    public abstract class AbstractDesignEntity
    {
        public string name { get; protected set; }

        public AbstractDesignEntity(string name)
        {
            this.name = name;
        }
    }
}