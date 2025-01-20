using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public abstract class BinaryNode : ExpressionNode
{
    public BinaryNode(ParserRuleContext context) : base(context)
    {
    }
}