using System.Collections.Generic;

namespace AITSI.Models.Models
{
    public class Node
    {
        public Node()
        {
            LineNumbers = new List<int>();
            NextNodes = new List<Node>();
        }
        public List<int> LineNumbers { get; set; }
        public List<Node> NextNodes { get; set; }
        public bool IsEndScope { get; set; }
    }
}