using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;


public class BoolNotNode(ParserRuleContext? context) : UnaryNode(context) 
{
    public override string Symbol => "!";

    // public override void Accept(IVisitor visitor)
    // {
    //     visitor.Visit(this);
    // }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}NOT");
        
        IncreaseIndent();
        sb.AppendLine($"{GetIndentation()}Operand:");
        IncreaseIndent();
        if (Operand != null)
        {
            sb.AppendLine(Operand.ToString());
        }
        DecreaseIndent();
        DecreaseIndent();
        
        return sb.ToString();
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute the operand
        object? operandValue = Operand?.Execute(env);
    
        // Check if operand is a boolean
        if (operandValue is bool boolValue)
        {
            return !boolValue;  // Return the logical NOT
        }
    
        throw new Exception($"Type error: NOT operation expects a Bool but got {operandValue?.GetType().Name} at line {Line}, column {Column}");
    }
}