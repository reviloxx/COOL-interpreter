namespace Cool.Interpreter.ASTNodes;


public class BoolNotNode(ExpressionNode operand, ParserRuleContext? context) : UnaryNode(operand, context) 
{
    public override string Symbol => "!";

    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute the operand
        object? operandValue = _operand?.Execute(env);

        // Check if operand is a boolean
        if (operandValue is bool boolValue)
        {
            return !boolValue;  // Return the logical NOT
        }

        throw new Exception($"Type error: NOT operation expects a Bool but got {operandValue?.GetType().Name} at line {_line}, column {_column}");
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}NOT");
        
        IncreaseIndent();
        sb.AppendLine($"{GetIndentation()}Operand:");
        IncreaseIndent();
        if (_operand != null)
        {
            sb.AppendLine(_operand.ToString());
        }
        DecreaseIndent();
        DecreaseIndent();
        
        return sb.ToString();
    }  
}