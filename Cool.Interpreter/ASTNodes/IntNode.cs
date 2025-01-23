namespace Cool.Interpreter.ASTNodes;

public class IntNode(int value, ParserRuleContext? context) : ExpressionNode(context)
{
    private readonly int _value = value;

    public override object? Execute(RuntimeEnvironment env)
    {
        return _value;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Integer: {_value}");
        return sb.ToString();
    }
}