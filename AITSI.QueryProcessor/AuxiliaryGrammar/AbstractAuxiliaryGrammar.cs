using AITSI.QueryProcessor.LexicalRules;
using System.Collections.Generic;

namespace AITSI.QueryProcessor.AuxiliaryGrammar
{
    public abstract class AbstractAuxiliaryGrammar
    {
        public List<ILexicalRules> lexicalRules = new List<ILexicalRules>();
        public string entry { get; protected set; }
        public ILexicalRules EntryTypeReference { get; protected set; }

        public AbstractAuxiliaryGrammar(string text)
        {
            entry = text;
        }

        public abstract void InitLexicalRules();

        public virtual bool IsGrammarCorrect()
        {
            InitLexicalRules();

            foreach (ILexicalRules lexicalRule in lexicalRules)
            {
                bool isCorrect = lexicalRule.validate(entry);
                if (isCorrect)
                {
                    EntryTypeReference = lexicalRule;
                    return true;
                }
            }
            return false;
        }
    }
}