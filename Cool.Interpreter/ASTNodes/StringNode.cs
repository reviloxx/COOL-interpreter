namespace Cool.Interpreter.ASTNodes;

public class StringNode(ParserRuleContext context) : ExpressionNode(context)
{
    public string Value { get; set; }

    public override object? Execute(RuntimeEnvironment env)
    {
        return Value;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}String: \"{Value}\""); // Added quotes to make string boundaries clear
        return sb.ToString();
    }
}