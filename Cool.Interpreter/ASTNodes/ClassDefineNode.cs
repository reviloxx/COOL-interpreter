using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class ClassDefineNode : AstNode, ICoolGrammarVisitor<>
{
    public IdNode Name { get; set; }
    public IdNode? BaseClassName { get; set; } // Optional

    public List<FeatureNode> FeatureNodes = new();

    public ClassDefineNode(ParserRuleContext context) : base(context)
    {
    }

    public ClassDefineNode(int line, int column) : base(line, column)
    {
    }
}