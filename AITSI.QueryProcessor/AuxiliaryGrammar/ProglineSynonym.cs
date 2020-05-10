using AITSI.QueryProcessor.DesignEntity;
using AITSI.QueryProcessor.LexicalRules;
using AITSI.QueryProcessor.Preprocessor;

namespace AITSI.QueryProcessor.AuxiliaryGrammar
{
    public class ProglineSynonym : AbstractAuxiliaryGrammar
    {
        private DeclarationsArray declarations;
        public ProglineSynonym(string text, DeclarationsArray declarations) : base(text)
        {
            this.declarations = declarations;
        }

        public override void InitLexicalRules()
        {
            // IT OVERRIDES IS GRAMMAR CORRECT SO LEXICAL RULES INIT IS NOT NEEDED
        }

        public override bool IsGrammarCorrect()
        {
            var declaration = declarations.GetDeclarationByName(entry);

            if (declaration == null)
            {
                return false;
            }

            if (!(declaration is ProgramLine))
            {
                return false;
            }

            return new Ident().validate(entry);
        }
    }
}