using System.Text.RegularExpressions;

namespace AITSI.QueryProcessor.LexicalRules
{
    public class Placeholder : ILexicalRules
    {
        public bool validate(string text)
        {
            Match placeholder = Regex.Match(text, "^_$");
            return placeholder.Success;
        }
    }
}