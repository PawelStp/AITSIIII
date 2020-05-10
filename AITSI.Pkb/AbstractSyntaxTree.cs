using AITSI.Models;
using AITSI.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AITSI.Pkb
{
    public class AbstractSyntaxTree : ITreeTraversal, INextNode
    {
        public AstNode AstRoot { get; set; }

        private List<AstNode> FindedNodes { get; set; }

        readonly List<NodeType> SimpleType = new List<NodeType>
        {
            NodeType.Assign,
            NodeType.Call,
        };
        readonly List<NodeType> ComplicatedType = new List<NodeType>
        {
            NodeType.If,
            NodeType.While,
        };

        internal AstNode AddNode(NodeType nodeType, int lineNumber, string tokenValue = "", bool isRoot = false, bool isEndedScope = false)
        {
            AstNode lastNodeContainer = GetLastNodeContainer(AstRoot);
            if (lastNodeContainer != null)
            {
                AstNode currentNode = new AstNode
                {
                    NodeType = nodeType,
                    ParentNode = lastNodeContainer,
                    Value = tokenValue,
                    IsAstEndScope = isEndedScope,
                    LineNumber = lineNumber,
                };
                if (lastNodeContainer.Children.Any())
                {
                    lastNodeContainer.Children.Last().RightSiblingNode = currentNode;
                }
                lastNodeContainer.Children.Add(currentNode);
                return currentNode;
            }
            else
            {
                AstRoot = new AstNode
                {
                    IsRoot = isRoot,
                    NodeType = nodeType,
                    Value = tokenValue,
                    IsAstEndScope = isEndedScope
                };
                return AstRoot;
            }
        }

        internal AstNode AddNode(AstNode node)
        {
            AstNode lastNodeContainer = GetLastNodeContainer(AstRoot);
            if (lastNodeContainer != null)
            {
                AstNode currentNode = node;
                node.ParentNode = lastNodeContainer;
                if (lastNodeContainer.Children.Any())
                {
                    lastNodeContainer.Children.Last().RightSiblingNode = currentNode;
                }
                lastNodeContainer.Children.Add(currentNode);
                return currentNode;
            }
            else
            {
                node.IsRoot = true;
                AstRoot = node;
                return AstRoot;
            }
        }

        public AstNode GetRoot()
        {
            return AstRoot;
        }

        public AstNode GetFirstChild(AstNode node)
        {
            return node.Children.FirstOrDefault();
        }

        public AstNode GetRightSibling(AstNode node)
        {
            return node.RightSiblingNode;
        }

        public IList<AstNode> GetAllChildren(AstNode node)
        {
            return node.Children;
        }

        private AstNode GetLastNodeContainer(AstNode currentNode)
        {
            if (currentNode != null)
            {
                if (currentNode.IsAstEndScope)
                {
                    while (currentNode.RightSiblingNode != null || currentNode.IsAstEndScope)
                    {
                        currentNode = currentNode.RightSiblingNode;
                    }
                }
                else
                {
                    while (currentNode.Children.Any(x => !x.IsAstEndScope))
                    {
                        currentNode = GetLastNodeContainer(currentNode.Children.FirstOrDefault());
                    }
                }
                return currentNode;
            }
            return null;
        }

        internal void PrintAst()
        {
            int tab = 0;
            PrintAstInternal(AstRoot, ref tab);
        }

        private void PrintAstInternal(AstNode node, ref int tab)
        {
            if (!node.Children.Any())
            {
                PrintTabs(tab, node);
            }
            else
            {
                PrintTabs(tab, node);
                tab++;
                foreach (var item in node.Children)
                {
                    PrintTabs(tab, item);
                    if (item.Children.Any())
                    {
                        foreach (var child in item.Children)
                        {
                            tab++;
                            PrintAstInternal(child, ref tab);
                            tab--;
                        }
                    }
                }
                tab--;
            }
        }

        private void PrintTabs(int tab, AstNode node)
        {
            //if (node.LineNumber != 0)
            //{
            //    Console.Write(node.LineNumber + ".");
            //}
            //for (int i = 0; i < tab; i++)
            //{
            //    Console.Write("    ");
            //}
            //Console.WriteLine(node.NodeType + " " + node.Value);
        }

        //public List<AstNode> GetNext(AstNode node, int procNumber)
        //{
        //    FindedNodes = new List<AstNode>();
        //    if (node.LineNumber != 0)
        //    {
        //        var nextNodesLines = VarTable.NextTable.FirstOrDefault(x => x.Key == procNumber).Value.Where(x => x.FirstLine == node.LineNumber).Select(x => x.SecondLine).ToList();
        //        TraverseThroughTree(AstRoot.Children[procNumber], nextNodesLines);
        //    }
        //    return FindedNodes;

        //}

        public List<AstNode> GetNext(int nodeLine, int procNumber)
        {
            FindedNodes = new List<AstNode>();
            if (nodeLine != 0)
            {
                var nextNodesLines = VarTable.NextTable.FirstOrDefault(x => x.Key == procNumber).Value.Where(x => x.FirstLine == nodeLine).Select(x => x.SecondLine).ToList();
                TraverseThroughTree(AstRoot.Children[procNumber - 1], nextNodesLines);
            }
            return FindedNodes;

        }

        public List<AstNode> GetAllNextLines(AstNode node, int procNumber)
        {
            List<AstNode> allNodeLines = new List<AstNode>();
            allNodeLines.Add(node);
            while (true)
            {
                node.IsCfgEndScope = true;
                var nextNodes = GetNext(node.LineNumber, procNumber);
                if (!nextNodes.Any())
                {
                    break;
                }
                else if (nextNodes.Count == 1 && !ComplicatedType.Contains(nextNodes.FirstOrDefault().NodeType))
                {
                    allNodeLines.Add(nextNodes.FirstOrDefault());
                    node = nextNodes.FirstOrDefault();
                }
                else
                {
                    break;
                }

            }
            return allNodeLines;
        }

        private void TraverseThroughTree(AstNode node, List<int> linesToFind)
        {
            if (FindedNodes.Count > 1)
            {
                return;
            }
            if (SimpleType.Contains(node.NodeType) || ComplicatedType.Contains(node.NodeType))
            {
                if (linesToFind.Contains(node.LineNumber))
                {
                    FindedNodes.Add(node);
                }
                foreach (var item in node.Children.Where(x => x.NodeType == NodeType.StatementList).Select((value, i) => (value, i)))
                {
                    TraverseThroughTree(item.value, linesToFind);
                }
            }
            else
            {
                foreach (var item in node.Children)
                {
                    TraverseThroughTree(item, linesToFind);
                }
            }
        }
    }
}