using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class UnaryNode : ExpressionNode
{
    public UnaryNode(ParserRuleContext context) : base(context)
    {
    }
}