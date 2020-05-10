using AITSI.QueryProcessor.LexicalRules;

namespace AITSI.QueryProcessor.AuxiliaryGrammar
{
    public class Synonym : AbstractAuxiliaryGrammar, ILexicalRules
    {
        public Synonym(string text) : base(text) { }

        public override void InitLexicalRules()
        {
            lexicalRules.Add(new Ident());
        }

        public bool validate(string text)
        {
            return new Ident().validate(text);
        }
    }
}