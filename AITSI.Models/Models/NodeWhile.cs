namespace AITSI.Models.Models
{
    public class NodeWhile : NodeBase
    {
        public NodeBase NextNode { get; set; }
        public NodeBase Children { get; set; }
    }
}