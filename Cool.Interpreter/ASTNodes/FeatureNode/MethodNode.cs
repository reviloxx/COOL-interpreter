using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class MethodNode : FeatureNode
{
    public List<ParameterNode> Parameters { get; set; } = new();
    public TypeNode ReturnType { get; set; }
    public BlockSequenceNode? Body { get; set; }

    public MethodNode(ParserRuleContext context) : base(context)
    {
    }

    public override object? Execute(RuntimeEnvironment env)
    {
        env.PushScope(); // Create new scope for method execution
        try
        {
            // Execute method body
            var result = Body?.Execute(env);
            return result;
        }
        finally
        {
            env.PopScope();
        }
        
        return null;
    }
    
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{GetIndentation()}Method {FeatureName}(");
        sb.Append(string.Join(", ", Parameters.Select(p => p.ToString())));
        sb.Append($") : {ReturnType}");
        
        if (Body != null)
        {
            sb.AppendLine(" {");
            IncreaseIndent();
            sb.AppendLine(Body.ToString());
            DecreaseIndent();
            sb.Append($"{GetIndentation()}}};");
        }
        
        return sb.ToString();
    }
}
