namespace AITSI.Pkb
{
    public class NextNode
    {
        public NextNode(int lastLineNumber, int currentLine)
        {
            FirstLine = lastLineNumber;
            SecondLine = currentLine;
        }

        public int FirstLine { get; set; }
        public int SecondLine { get; set; }
    }
}