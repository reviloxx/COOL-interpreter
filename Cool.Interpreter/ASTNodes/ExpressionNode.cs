namespace Cool.Interpreter.ASTNodes;

public abstract class ExpressionNode(ParserRuleContext? context) : AstNode(context)
{
    private List<ExpressionNode> _children { get; set; } = [];

    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}