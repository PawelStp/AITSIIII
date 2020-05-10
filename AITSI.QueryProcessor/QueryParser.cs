using AITSI.QueryProcessor.Preprocessor;
using System;

namespace AITSI.QueryProcessor
{
    public class QueryParser
    {
        public static string ProcessQuery(string query)
        {
            try
            {
                QueryValidator queryValidator = new QueryValidator();
                bool isQueryValid = queryValidator.ValidateQuery(query);
            }
            catch (Exception e)
            {
               Console.WriteLine("none");
            }

            return "OK";
        }
    }
}