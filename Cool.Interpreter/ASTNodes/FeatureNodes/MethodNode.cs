namespace Cool.Interpreter.ASTNodes.FeatureNodes;

public class MethodNode(ParserRuleContext? context) : FeatureNode(context)
{
    public List<ParameterNode> Parameters { get; set; } = new();
    public TypeNode ReturnType { get; set; }
    public BlockSequenceNode? Body { get; set; }

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
