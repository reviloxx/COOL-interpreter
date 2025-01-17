using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class ExpressionNode : AstNode
{
    public ExpressionNode(ParserRuleContext context) : base(context)
    {
    }
}