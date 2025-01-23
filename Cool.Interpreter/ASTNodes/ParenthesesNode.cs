namespace Cool.Interpreter.ASTNodes;

public class ParenthesesNode(ExpressionNode? expression, ParserRuleContext? context = null) : ExpressionNode(context)
{
    private readonly ExpressionNode? _expression = expression;

    public override object? Execute(RuntimeEnvironment env)
    {
        return _expression?.Execute(env);
    }

    public override string ToString()
    {
        return $"{GetIndentation()}({_expression})";
    }
}