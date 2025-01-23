namespace Cool.Interpreter.ASTNodes.FeatureNodes;

public class MethodNode(string methodName, IEnumerable<ParameterNode> parameters, TypeNode returnType, BlockSequenceNode? body, ParserRuleContext? context = null) : FeatureNode(methodName, context)
{
    public IEnumerable<ParameterNode> Parameters { get; private set; } = parameters;
    private readonly TypeNode _returnType = returnType;
    private readonly BlockSequenceNode? _body = body;

    public override object? Execute(RuntimeEnvironment env)
    {
        env.PushScope(); // Create new scope for method execution
        var result = _body?.Execute(env);
        env.PopScope();
        return result;            
    }     
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{GetIndentation()}Method {_featureIdNode}(");
        sb.Append(string.Join(", ", Parameters.Select(p => p.ToString())));
        sb.Append($") : {_returnType}");
        
        if (_body != null)
        {
            sb.AppendLine(" {");
            IncreaseIndent();
            sb.AppendLine(_body.ToString());
            DecreaseIndent();
            sb.Append($"{GetIndentation()}}};");
        }
        
        return sb.ToString();
    }
}
