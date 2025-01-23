namespace Cool.Interpreter.ASTNodes;

public class WhileNode(ExpressionNode condition, ExpressionNode body, ParserRuleContext? context = null) : ExpressionNode(context)
{
    private readonly ExpressionNode _condition = condition;
    private readonly ExpressionNode? _body = body;

    public override object? Execute(RuntimeEnvironment env)
    {        
        object? result = null;
        while (true)
        {
            var conditionResult = _condition.Execute(env);
        
            // Handle numeric comparisons
            if (conditionResult is bool boolResult)
            {
                if (!boolResult) break;
            }
            else if (conditionResult is int intResult)
            {
                if (intResult == 0) break;  // In COOL, 0 is false
            }
            else
            {
                // Handle other types or throw appropriate exception
                throw new Exception($"Invalid condition result type: {conditionResult?.GetType()}");
            }
        
            result = _body?.Execute(env);
        }
        return result;
    }

    public override string ToString()
    {
        return $"{GetIndentation()}while {_condition} loop\n{_body}\n{GetIndentation()}pool";
    }
}