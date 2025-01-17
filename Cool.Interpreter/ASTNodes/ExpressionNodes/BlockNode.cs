using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class BlockNode : ExpressionNode
{
    public BlockNode(ParserRuleContext context) : base(context)
    {
    }
}