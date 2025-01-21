using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class AssignmentNode : ExpressionNode
{
    public required IdNode Target { get; set; } // Zielvariable
    public ExpressionNode? Value { get; set; } // Zuweisungswert // kann auch null sein?

    public AssignmentNode(ParserRuleContext context) : base(context)
    {
    }
   
    public override object? Execute(RuntimeEnvironment env)
    {
        // Evaluate the right-hand side expression
        object? valueToAssign = Value?.Execute(env);
    
        // Store the value in the current scope
        env.DefineVariable(Target.Name, valueToAssign);
    
        // In COOL, assignments return the assigned value
        return valueToAssign;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Assignment");
        IncreaseIndent();
        sb.AppendLine($"{GetIndentation()}Target: {Target}");
        if (Value != null)
        {
            sb.AppendLine($"{GetIndentation()}Value:");
            IncreaseIndent();
            sb.AppendLine(Value.ToString());
            DecreaseIndent();
        }
        DecreaseIndent();
        return sb.ToString();
    }
}