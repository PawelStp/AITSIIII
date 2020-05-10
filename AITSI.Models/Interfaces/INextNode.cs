using System.Collections.Generic;

namespace AITSI.Models.Interfaces
{
    public interface INextNode
    {
        List<AstNode> GetNext(int nodeLine, int procNumber);
    }
}