using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class LetInNode : ExpressionNode
{
    public LetInNode(ParserRuleContext context) : base(context)
    {
    }
}