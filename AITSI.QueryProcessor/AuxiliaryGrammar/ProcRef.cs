using AITSI.QueryProcessor.LexicalRules;

namespace AITSI.QueryProcessor.AuxiliaryGrammar
{
    public class ProcRef : AbstractAuxiliaryGrammar
    {
        public ProcRef(string text) : base(text) { }

        public override void InitLexicalRules()
        {
            lexicalRules.Add(new Ident());
            lexicalRules.Add(new Placeholder());
            lexicalRules.Add(new BracesIdent());
        }
    }
}