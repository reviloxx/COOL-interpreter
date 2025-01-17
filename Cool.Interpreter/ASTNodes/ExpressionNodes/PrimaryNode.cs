using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class PrimaryNode : ExpressionNode
{
    public PrimaryNode(ParserRuleContext context) : base(context)
    {
    }
}