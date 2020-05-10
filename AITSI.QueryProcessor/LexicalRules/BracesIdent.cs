using System.Text.RegularExpressions;

namespace AITSI.QueryProcessor.LexicalRules
{
    public class BracesIdent : ILexicalRules
    {
        public bool validate(string text)
        {
            Match ident = Regex.Match(text, "^\"[A-Za-z][A-Za-z0-9#]*\"$");
            return ident.Success;
        }
    }
}
