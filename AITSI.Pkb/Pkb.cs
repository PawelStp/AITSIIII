using AITSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AITSI.Pkb
{
    public class Pkb : IPkb
    {
        #region SingletonStuff

        private static readonly Pkb instance = new Pkb();

        public static Pkb Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        public Pkb()
        {
            AbstractSyntaxTree = new AbstractSyntaxTree();
            ControlFlowGraph = new ControlFlowGraph();
            VarTable = new VarTable();
        }

        public void PrintAst()
        {
            AbstractSyntaxTree.PrintAst();
        }

        public void CreateControlFlowGraph()
        {
            ControlFlowGraph.Create();
        }

        public AbstractSyntaxTree AbstractSyntaxTree { get; set; }
        public ControlFlowGraph ControlFlowGraph { get; set; }
        public VarTable VarTable { get; set; }
        public List<ProcTableCell> ProcTable { get; set; }
        public List<StatementColumn> StatementModifiesTable { get; set; }
        public Dictionary<string, List<string>> ProceduresModifiesTable { get; set; }
        public List<StatementColumn> StatementUsesTable { get; set; }
        public Dictionary<string, List<string>> ProceduresUsesTable { get; set; }
        public List<string> UsedVariables { get; set; }
        public List<ParrentTableCell> ParentTable { get; set; }
        public Dictionary<int, int> FollowsTable { get; set; }
        public List<string> UsedProcedures { get; set; }
        public List<UsedStatementElement> UsedStatements { get; set; }

        public bool Follows(AstNode s1, AstNode s2)
        {
            return s1.RightSiblingNode == s2;
        }

        public bool Calls(string s1, string s2)
        {
            var s1Ref = ProcTable.FirstOrDefault(p => p.ProcedureName.Equals(s1, StringComparison.InvariantCultureIgnoreCase));
            if (s1Ref == null) return false;
            if (s1Ref.CalledProcedures.Contains(s2, StringComparer.InvariantCultureIgnoreCase))
                return true;
            else
                return false;
        }

        public List<string> CalledByProcedure(string s1)
        {
            var result = new List<string>();
            var s1Ref = ProcTable.FirstOrDefault(p => p.ProcedureName.Equals(s1, StringComparison.InvariantCultureIgnoreCase));
            if (s1Ref == null) return result;
            return s1Ref.CalledProcedures;
        }

        public List<string> ProceduresThatCall(string s1)
        {
            var result = new List<string>();

            foreach (var procTableCell in ProcTable)
            {
                if (procTableCell.CalledProcedures.Contains(s1, StringComparer.InvariantCultureIgnoreCase))
                    result.Add(procTableCell.ProcedureName);
            }

            return result;
        }

        public bool ModifiesStatement(int statementNumber, string variable)
        {
            if (String.IsNullOrEmpty(variable))
                return false;
            if (StatementModifiesTable.Where(x => x.StatementNumber == statementNumber && x.Variables.Any(y => y.Equals(variable, StringComparison.InvariantCultureIgnoreCase))).Any())
                return true;
            return false;
        }

        public List<string> ModifiesVarModInStmt(int statementNumber, NodeType statementType = NodeType.Statement)
        {
            if (statementType == NodeType.Statement)
                return StatementModifiesTable.Where(x => x.StatementNumber == statementNumber).Select(x => x.Variables).FirstOrDefault();
            else
                return StatementModifiesTable.Where(x => x.StatementNumber == statementNumber && x.StatementType == statementType).Select(x => x.Variables).FirstOrDefault();
        }

        public List<int> ModifiesStmtThatModVar(string variable, NodeType statementType = NodeType.Statement)
        {
            if (statementType == NodeType.Statement)
                return StatementModifiesTable.Where(x => x.Variables.Any(y => y.Equals(variable, StringComparison.InvariantCultureIgnoreCase))).Select(x => x.StatementNumber).ToList();
            else
                return StatementModifiesTable.Where(x => x.Variables.Any(y => y.Equals(variable, StringComparison.InvariantCultureIgnoreCase)) && x.StatementType == statementType).Select(x => x.StatementNumber).ToList();
        }

        public bool ModifiesProcedure(string procedureName, string variable)
        {
            return ProceduresModifiesTable.Where(x => x.Key.Equals(procedureName, StringComparison.InvariantCultureIgnoreCase) && x.Value.Contains(variable, StringComparer.InvariantCultureIgnoreCase)).Any();
        }

        public List<string> ModifiesAllProcModVar(string procedureName)
        {
            return ProceduresModifiesTable.Where(x => x.Key.Equals(procedureName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value).FirstOrDefault();
        }

        public List<string> ModifiesAllVarModProc(string variable)
        {
            return ProceduresModifiesTable.Where(x => x.Value.Any(y => y.Equals(variable, StringComparison.InvariantCultureIgnoreCase))).Select(x => x.Key).ToList();
        }

        public bool UsesStatement(int statementNumber, string variable)
        {
            if (String.IsNullOrEmpty(variable))
                return false;
            if (StatementUsesTable.Where(x => x.StatementNumber == statementNumber && x.Variables.Contains(variable, StringComparer.InvariantCultureIgnoreCase)).Any())
                return true;
            return false;
        }

        public List<string> UsesVarModInStmt(int statementNumber, NodeType statementType = NodeType.Statement)
        {
            if (statementType == NodeType.Statement)
                return StatementUsesTable.Where(x => x.StatementNumber == statementNumber).Select(x => x.Variables).FirstOrDefault();
            else
                return StatementUsesTable.Where(x => x.StatementNumber == statementNumber && x.StatementType == statementType).Select(x => x.Variables).FirstOrDefault();
        }

        public List<int> UsesStmtThatModVar(string variable, NodeType statementType = NodeType.Statement)
        {
            if (statementType == NodeType.Statement)
                return StatementUsesTable.Where(x => x.Variables.Contains(variable, StringComparer.InvariantCultureIgnoreCase)).Select(x => x.StatementNumber).ToList();
            else
                return StatementUsesTable.Where(x => x.Variables.Contains(variable, StringComparer.InvariantCultureIgnoreCase) && x.StatementType == statementType).Select(x => x.StatementNumber).ToList();
        }

        public bool UsesProcedure(string procedureName, string variable)
        {
            return ProceduresUsesTable.Where(x => x.Key.Equals(procedureName, StringComparison.InvariantCultureIgnoreCase) && x.Value.Contains(variable, StringComparer.InvariantCultureIgnoreCase)).Any();
        }

        public List<string> UsesAllProcModVar(string procedureName)
        {
            return ProceduresUsesTable.Where(x => x.Key.Equals(procedureName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value).FirstOrDefault();
        }

        public List<string> UsesAllVarModProc(string variable)
        {
            return ProceduresUsesTable.Where(x => x.Value.Any(y => y.Equals(variable, StringComparison.InvariantCultureIgnoreCase))).Select(x => x.Key).ToList();
        }

        public List<string> GetAllUsedVariables()
        {
            return UsedVariables;
        }

        public List<string> GetAllUsedProcedures()
        {
            return UsedProcedures;
        }

        public List<UsedStatementElement> GetAllUsedStatements()
        {
            return UsedStatements;
        }

        public bool Parent(int statementNumber1, int statementNumber2)
        {
            return ParentTable.Where(x => x.ContainerStatementNumber == statementNumber1 && x.ChildsStatementNumber.Contains(statementNumber2)).Any();
        }

        public bool ParentStar(int statementNumber1, int statementNumber2)
        {
            if (ParentTable.Where(x => x.ContainerStatementNumber == statementNumber1 && x.ChildsStatementNumber.Contains(statementNumber2)).Any())
                return true;
            ParrentTableCell cell = ParentTable.Where(x => x.ChildsStatementNumber.Contains(statementNumber2)).FirstOrDefault();
            if (cell == null)
                return false;
            return ParentStar(statementNumber1, cell.ContainerStatementNumber);
        }

        public bool Follows(int statementNumber1, int statementNumber2)
        {
            return FollowsTable.Where(x => x.Key == statementNumber1 && x.Value == statementNumber2).Any();
        }

        public bool FollowsStar(int statementNumber1, int statementNumber2)
        {
            if (FollowsTable.Where(x => x.Key == statementNumber1 && x.Value == statementNumber2).Any())
                return true;
            KeyValuePair<int, int> cell = FollowsTable.Where(x => x.Value == statementNumber2).FirstOrDefault();
            if (cell.Key == 0)
                return false;
            return FollowsStar(statementNumber1, cell.Key);
        }

    }
    public class UsedStatementElement
    {
        public int statementNumber { get; set; }
        public NodeType stementType { get; set; }
    }
}