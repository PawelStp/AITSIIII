using System;

namespace AITSI.QueryProcessor.Preprocessor
{
    public class InvalidQueryException : Exception
    {
        public InvalidQueryException(string message) : base(message)
        {
        }
    }
}