using AITSI.QueryProcessor.DesignEntity;
using AITSI.QueryProcessor.LexicalRules;

namespace AITSI.QueryProcessor.AuxiliaryGrammar
{
    public class StmtRef : AbstractAuxiliaryGrammar
    {
        public StmtRef(string text) : base(text) { }
        public Statement Type { get; set; }

        public override void InitLexicalRules()
        {
            lexicalRules.Add(new Ident());
            lexicalRules.Add(new Placeholder());
            lexicalRules.Add(new IntegerRule());
        }
    }
}