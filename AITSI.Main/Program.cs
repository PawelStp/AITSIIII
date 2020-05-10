using AITSI.Pkb;
using AITSI.Pkb.Exceptions;
using AITSI.Pkb.Helpers;
using AITSI.QueryProcessor;
using System;

namespace AITSI.Main
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
            if (args.Length > 0)
            {
                SourceRepository.StoreSourceCode(args[0]);
            }
            else
            {
            }

            Initialization.PkbReference = new Pkb.Pkb();

            try
            {
                Parser parser = new Parser();
                parser.StartParsing();
                DesignExtractor designExtractor = new DesignExtractor();
                designExtractor.CreateProcTable();

                //QueryParser.ProcessQuery("PROCEDURE p; VARIABLE v, v2; STMT s1, s2, s3, s4; WHILE w;" +
                //    "select p such that moDIFIES (p, v) and MODIFIES (s1, v) and USES (s2, v) and USES (s1, v) and PARENT (w, s1)");
                //QueryParser.ProcessQuery("stmt s; SELECT BOOLEAN such that follows (54, 51)");
                //QueryParser.ProcessQuery("stmt s; Select BOOLEAN such that Follows(5, 6)");
                //QueryParser.ProcessQuery("PROCEDURE p; VARIABLE v, v2; STMT s1, s2, s3, s4; WHILE w;" +
                //    "SELECT p SUCH THAT MODIFIES (p, v) and MODIFIES (s1, v) and USES (s2, v) and USES (s1, v) and PARENT (w, s1)");
                //                procedure p1, p2;
                //                Select p1 such that Modifies(p1, “x”) and
                //Uses(p1, “y”) and Calls(p2, p1)
                while (true)
                {
                    string s1 = Console.ReadLine();
                    string s2 = Console.ReadLine();
                    QueryParser.ProcessQuery($"{s1}{s2}");
                }
            }
            catch (ParsingException e)
            {
                Console.WriteLine(e);
                //Console.ReadLine();
                Environment.Exit(1);
            }
            //string command = Console.ReadLine();
            //while(!(command.Equals("exit")))
            //{
            //    if(command.Equals("print-ast"))
            //        Pkb.Pkb.PrintAst();
            //    command = Console.ReadLine();
            //}
        }
    }
}