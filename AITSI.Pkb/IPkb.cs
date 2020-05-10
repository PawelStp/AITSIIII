using AITSI.Models;
using System.Collections.Generic;

namespace AITSI.Pkb
{
    public interface IPkb
    {
        void PrintAst();
        void CreateControlFlowGraph();

        AbstractSyntaxTree AbstractSyntaxTree { get; set; }

        ControlFlowGraph ControlFlowGraph { get; set; }
        VarTable VarTable { get; set; }
        List<ProcTableCell> ProcTable { get; set; }
        List<StatementColumn> StatementModifiesTable { get; set; }
        Dictionary<string, List<string>> ProceduresModifiesTable { get; set; }
        List<StatementColumn> StatementUsesTable { get; set; }
        Dictionary<string, List<string>> ProceduresUsesTable { get; set; }
        List<string> UsedVariables { get; set; }
        List<ParrentTableCell> ParentTable { get; set; }
        Dictionary<int, int> FollowsTable { get; set; }
        List<string> UsedProcedures { get; set; }
        List<UsedStatementElement> UsedStatements { get; set; }
        bool Follows(AstNode s1, AstNode s2);

        bool Calls(string s1, string s2);

        List<string> CalledByProcedure(string s1);

        List<string> ProceduresThatCall(string s1);

        bool ModifiesStatement(int statementNumber, string variable);

        List<string> ModifiesVarModInStmt(int statementNumber, NodeType statementType = NodeType.Statement);

        List<int> ModifiesStmtThatModVar(string variable, NodeType statementType = NodeType.Statement);

        bool ModifiesProcedure(string procedureName, string variable);

        List<string> ModifiesAllProcModVar(string procedureName);

        List<string> ModifiesAllVarModProc(string variable);

        bool UsesStatement(int statementNumber, string variable);

        List<string> UsesVarModInStmt(int statementNumber, NodeType statementType = NodeType.Statement);

        List<int> UsesStmtThatModVar(string variable, NodeType statementType = NodeType.Statement);

        bool UsesProcedure(string procedureName, string variable);

        List<string> UsesAllProcModVar(string procedureName);

        List<string> UsesAllVarModProc(string variable);

        List<string> GetAllUsedVariables();

        bool Parent(int statementNumber1, int statementNumber2);

        bool ParentStar(int statementNumber1, int statementNumber2);

        List<string> GetAllUsedProcedures();


        List<UsedStatementElement> GetAllUsedStatements();

        bool Follows(int statementNumber1, int statementNumber2);

        bool FollowsStar(int statementNumber1, int statementNumber2);
    }
}