using AITSI.Models;
using AITSI.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace AITSI.Pkb
{
    public class ControlFlowGraph
    {
        private IPkb PkbReference;
        public List<Node> Roots { get; set; }
        private Node CurrentRoot { get; set; }
        private AstNode AstRoot { get; set; }
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
        private int currentProcedureNumber = 0;

        public ControlFlowGraph()
        {
            this.Roots = new List<Node>();
            PkbReference = Initialization.PkbReference;
        }

        internal void Create()
        {
            this.AstRoot = PkbReference.AbstractSyntaxTree.AstRoot;
            foreach (var item in AstRoot.Children.Select((procedure, i) => (procedure, i)))
            {
                this.currentProcedureNumber = item.i + 1;
                GetAstNodeInOrder(item.procedure);
            }
        }

        private void GetAstNodeInOrder(AstNode node)
        {
            if ((SimpleType.Contains(node.NodeType) || ComplicatedType.Contains(node.NodeType)) && !node.IsCfgEndScope)
            {
                var nodes = PkbReference.AbstractSyntaxTree.GetAllNextLines(node, this.currentProcedureNumber);
                AddNode(nodes);

                foreach (var item in node.Children.Where(x => x.NodeType == NodeType.StatementList))
                {
                    GetAstNodeInOrder(item);
                }
            }
            else
            {
                foreach (var item in node.Children)
                {
                    GetAstNodeInOrder(item);
                }
            }
        }

        private Node AddNode(List<AstNode> nextNodes)
        {
            Node node = new Node();
            foreach (var lines in nextNodes)
            {
                node.LineNumbers.Add(lines.LineNumber);
            }
            return node;
        }
        internal void Print()
        {
            //foreach (var item in CfgRoots)
            //{
            //    PrintInternal(item);
            //}
        }
        private void PrintInternal(Node node)
        {
            //foreach (var lines in node.LineNumbers)
            //{
            //    Console.WriteLine(lines + ",");
            //}
            //foreach (var child in node.NextNodes)
            //{
            //    foreach (var lines in child.LineNumbers)
            //    {
            //        Console.WriteLine(lines + ",");
            //    }
            //}
            //PrintInternal(node.NextNodes.Last());
        }
    }
}