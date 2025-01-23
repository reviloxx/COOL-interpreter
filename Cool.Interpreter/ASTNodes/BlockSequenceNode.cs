namespace Cool.Interpreter.ASTNodes;

public class BlockSequenceNode(IEnumerable<ExpressionNode> expressions, ParserRuleContext? context) : ExpressionNode(context)
{
    private readonly IEnumerable<ExpressionNode> _expressions = expressions;

    public override object? Execute(RuntimeEnvironment env)
    {
        object? lastResult = null;
        foreach (var expr in _expressions)
        {
            lastResult = expr.Execute(env);
        }
        
        return lastResult;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Block {{");
        
        IncreaseIndent();
        foreach (var expr in _expressions)
        {
            if (expr != null)
            {
                sb.Append(expr.ToString());
                sb.AppendLine(";");
            }
        }
        DecreaseIndent();
        
        sb.Append($"{GetIndentation()}}}");
        return sb.ToString();
    }
}