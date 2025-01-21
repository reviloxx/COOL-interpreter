using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class LetInNode : ExpressionNode
{
    public List<PropertyNode> Declarations { get; set; } = new();
    public ExpressionNode? Body { get; set; }

    public LetInNode(ParserRuleContext context) : base(context)
    {
    }

    public override object? Execute(RuntimeEnvironment env)
    {
        // Create a new scope for the let-in expression
        env.PushScope();
        
        try
        {
            // Define variables in the current scope
            foreach (var declaration in Declarations)
            {
                object? initialValue = null;
        
                // If the property has an initializer, execute it
                if (declaration.InitialValue != null)
                {
                    initialValue = declaration.InitialValue.Execute(env);
                }
        
                // Define the variable in the current scope
                env.DefineVariable(declaration.FeatureName.Name, initialValue);
            }
        
            // Execute the body expression
            return Body?.Execute(env);
        }
        finally
        {
            // Always pop the scope, even if an exception occurs
            env.PopScope();
        }
        return null;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Let {{");
        
        IncreaseIndent();
        foreach (var declaration in Declarations)
        {
            sb.AppendLine(declaration.ToString());
        }
        
        if (Body != null)
        {
            sb.AppendLine("In:");
            sb.Append(Body.ToString());
        }
        DecreaseIndent();
        
        sb.Append($"{GetIndentation()}}}");
        return sb.ToString();
    }
}