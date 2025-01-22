namespace Cool.Interpreter.ASTNodes;

public class IfNode(ParserRuleContext context, ExpressionNode condition, ExpressionNode thenBranch, ExpressionNode? elseBranch) : ExpressionNode(context)
{
    private ExpressionNode _condition = condition;
    private ExpressionNode _thenBranch = thenBranch;
    private ExpressionNode? _elseBranch = elseBranch;

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