namespace Cool.Interpreter.ASTNodes;

public class StringNode(string value, ParserRuleContext? context) : ExpressionNode(context)
{
    private readonly string _value = value;

    public override object? Execute(RuntimeEnvironment env)
    {
        return _value;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}String: \"{_value}\""); // Added quotes to make string boundaries clear
        return sb.ToString();
    }
}