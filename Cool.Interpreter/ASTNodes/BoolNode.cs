namespace Cool.Interpreter.ASTNodes;

public class BoolNode(bool value, ParserRuleContext? context) : ExpressionNode(context)
{
    private readonly bool _value = value;

    public override object? Execute(RuntimeEnvironment env)
    {
        return _value;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Boolean: {_value}");
        return sb.ToString();
    }
}