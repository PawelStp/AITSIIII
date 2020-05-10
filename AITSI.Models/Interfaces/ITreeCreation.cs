namespace AITSI.Models.Interfaces
{
    public interface ITreeCreation
    {
        AstNode CreateNode(NodeType nodeType, bool isRoot = false);
        void SetSibling(AstNode sibling);
        void SetChild(AstNode child);
        void SetParent(AstNode parent);
    }
}