using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class ExpressionNode : AstNode
{
    List<ExpressionNode> Children { get; set; } = new();

    public ExpressionNode(ParserRuleContext context) : base(context)
    {
    }
}