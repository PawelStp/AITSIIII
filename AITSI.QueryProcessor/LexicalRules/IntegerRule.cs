using System.Text.RegularExpressions;

namespace AITSI.QueryProcessor.LexicalRules
{
    public class IntegerRule : ILexicalRules
    {
        public bool validate(string text)
        {
            Match placeholder = Regex.Match(text, "^[0-9]+$");
            return placeholder.Success;
        }
    }
}