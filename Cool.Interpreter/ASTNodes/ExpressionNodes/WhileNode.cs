using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class WhileNode : ExpressionNode
{
    public WhileNode(ParserRuleContext context) : base(context)
    {
    }
}