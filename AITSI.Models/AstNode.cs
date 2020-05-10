using System.Collections.Generic;

namespace AITSI.Models
{
    public class AstNode
    {
        public AstNode()
        {
            this.Children = new List<AstNode>();
        }

        public NodeType NodeType { get; set; }
        public string Value { get; set; }
        public IList<AstNode> Children { get; set; }
        public bool IsRoot { get; set; }
        public AstNode RightSiblingNode { get; set; }
        public AstNode ParentNode { get; set; }
        public int LineNumber { get; set; }
        public int StatementNumber { get; set; }
        public bool IsAstEndScope { get; set; }
        public bool IsCfgEndScope { get; set; }
    }
}