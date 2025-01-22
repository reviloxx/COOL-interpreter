namespace Cool.Interpreter.ASTNodes;

public class IntNode(ParserRuleContext context) : ExpressionNode(context)
{
    public int Value { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Integer: {Value}");
        return sb.ToString();
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        return Value;
    }
}