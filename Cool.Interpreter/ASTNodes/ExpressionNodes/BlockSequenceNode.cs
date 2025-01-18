using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class BlockSequenceNode : ExpressionNode
{
    public List<ExpressionNode?> Expressions { get; set; } = new();

    public BlockSequenceNode(ParserRuleContext context) : base(context)
    {
    }
}