namespace Cool.Interpreter.ASTNodes;

public class NegativeNode(ExpressionNode? expression, ParserRuleContext? context) : ExpressionNode(context)
{
    private readonly ExpressionNode? _expression = expression;

    public override object? Execute(RuntimeEnvironment env)
    {
        var result = _expression?.Execute(env);
        if (result is int intValue)
        {
            return -intValue;
        }
        throw new Exception($"Cannot negate non-integer value at line {_line}, column {_column}");
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Negative Expression");
        if (_expression != null)
        {
            IncreaseIndent();
            sb.Append(_expression.ToString());
            DecreaseIndent();
        }
        return sb.ToString();
    }
}