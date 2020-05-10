namespace AITSI.QueryProcessor.Preprocessor
{
    public class DictionaryNode
    {
        public int Value { get; set; }
        public string KeyInRelatedNode { get; set; }
        public int PositionInRelatedNodeList { get; set; }

        public DictionaryNode(string keyInRelatedNode, int value, int positionInRelatedNodeList)
        {
            Value = value;
            KeyInRelatedNode = keyInRelatedNode;
            PositionInRelatedNodeList = positionInRelatedNodeList;
        }
    }
}