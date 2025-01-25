namespace Cool.Interpreter.ASTNodes;

public class IfNode(ExpressionNode condition, ExpressionNode thenBranch, ExpressionNode? elseBranch, ParserRuleContext? context = null) : ExpressionNode(context)
{
    private readonly ExpressionNode _condition = condition;
    private readonly ExpressionNode _thenBranch = thenBranch;
    private readonly ExpressionNode? _elseBranch = elseBranch;

    public override object? Execute(RuntimeEnvironment env)
    {
        var conditionResult = _condition.Execute(env);
        if (conditionResult is bool boolResult && boolResult)
        {
            return _thenBranch.Execute(env);
        }
        else
        {
            return _elseBranch?.Execute(env);
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}If Expression");
        IncreaseIndent();
        
        sb.AppendLine($"{GetIndentation()}Condition:");
        IncreaseIndent();
        if (_condition != null)
            sb.Append(_condition.ToString());
        DecreaseIndent();
        
        sb.AppendLine($"{GetIndentation()}Then Branch:");
        IncreaseIndent();
        if (_thenBranch != null)
            sb.Append(_thenBranch.ToString());
        DecreaseIndent();
        
        sb.AppendLine($"{GetIndentation()}Else Branch:");
        IncreaseIndent();
        if (_elseBranch != null)
            sb.Append(_elseBranch.ToString());
        DecreaseIndent();
        
        DecreaseIndent();
        return sb.ToString();
    }
}