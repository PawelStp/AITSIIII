using AITSI.Pkb.Exceptions;
using AITSI.Pkb.Properties;
using System.IO;

namespace AITSI.Pkb.Helpers
{
    public static class SourceRepository
    {
        public static string SourceCode { get; private set; }

        public static void StoreSourceCode(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                SourceCode = Resources.sample_program;
            }
            else
            {
                if (!File.Exists(filePath))
                    throw new PkbException("Specified file does not exist");

                using (var file = new StreamReader(filePath))
                {
                    SourceCode = file.ReadToEnd();
                    file.Close();
                }
            }
        }

        public static void StoreSourceCodeFromString(string content)
        {
            SourceCode = content;
        }
    }
}