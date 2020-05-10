using AITSI.QueryProcessor.LexicalRules;

namespace AITSI.QueryProcessor.AuxiliaryGrammar
{
    public class LineRef : AbstractAuxiliaryGrammar
    {
        public LineRef(string text) : base(text) { }

        public override void InitLexicalRules()
        {
            lexicalRules.Add(new Ident());
            lexicalRules.Add(new Placeholder());
            lexicalRules.Add(new IntegerRule());
        }
    }
}