using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class DispatchNode : ExpressionNode
{
    public DispatchNode(ParserRuleContext context) : base(context)
    {
    }
}