namespace Cool.Interpreter.ASTNodes;

public class ParenthesesNode(ParserRuleContext context) : ExpressionNode(context)
{
    public ExpressionNode? Expression { get; set; }

    public override object? Execute(RuntimeEnvironment env)
    {
        return Expression?.Execute(env);
    }

    public override string ToString()
    {
        return $"{GetIndentation()}({Expression})";
    }
}