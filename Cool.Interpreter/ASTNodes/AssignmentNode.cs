namespace Cool.Interpreter.ASTNodes;

public class AssignmentNode(ParserRuleContext? context, string targetName, ExpressionNode? value) : ExpressionNode(context)
{
    private readonly IdNode _target = new(targetName, context);
    private readonly ExpressionNode? _value = value;

    public override object? Execute(RuntimeEnvironment env)
    {
        // Evaluate the right-hand side expression
        object? valueToAssign = _value?.Execute(env);
    
        // Store the value in the current scope
        env.DefineVariable(_target.Name, valueToAssign);
    
        // In COOL, assignments return the assigned value
        return valueToAssign;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Assignment");
        IncreaseIndent();
        sb.AppendLine($"{GetIndentation()}Target: {_target}");
        if (_value != null)
        {
            sb.AppendLine($"{GetIndentation()}Value:");
            IncreaseIndent();
            sb.AppendLine(_value.ToString());
            DecreaseIndent();
        }
        DecreaseIndent();
        return sb.ToString();
    }
}