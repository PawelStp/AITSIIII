namespace AITSI.Models.Models
{
    public class NodeIf : NodeBase
    {
        public NodeBase NextNodeForTrue { get; set; }
        public NodeBase NextNodeForFalse { get; set; }
    }
}