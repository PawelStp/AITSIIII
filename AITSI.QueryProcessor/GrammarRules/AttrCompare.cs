using AITSI.QueryProcessor.Preprocessor;

namespace AITSI.QueryProcessor.GrammarRules
{
    public class AttrCompare
    {
        private string text;
        private DeclarationsArray Declarations;
        public AttrCompare(string text, DeclarationsArray Declarations)
        {
            this.text = text;
            this.Declarations = Declarations;
        }

        public bool IsGrammarCorrect()
        {
            string[] refText = text.Split('=');

            Ref ref1 = new Ref(refText[0], Declarations);
            Ref ref2 = new Ref(refText[1], Declarations);

            if (!ref1.IsGrammarCorrect() || !ref2.IsGrammarCorrect())
            {
                return false;
            }

            if (ref1.refType != ref2.refType)
            {
                // left-hand-side and right-hand-side 'ref' must be of the same type (INTRGER or character string)
                return false;
            }

            return true;
        }
    }
}