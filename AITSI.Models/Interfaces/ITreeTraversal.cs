using System.Collections.Generic;

namespace AITSI.Models.Interfaces
{
    public interface ITreeTraversal
    {
        AstNode GetRoot();
        AstNode GetFirstChild(AstNode node);
        AstNode GetRightSibling(AstNode node);
        IList<AstNode> GetAllChildren(AstNode node);
    }
}