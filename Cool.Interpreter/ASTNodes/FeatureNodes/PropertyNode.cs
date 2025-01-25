namespace Cool.Interpreter.ASTNodes.FeatureNodes;

public class PropertyNode(string propertyName, TypeNode propertyType, ExpressionNode? initialValue = null, ParserRuleContext? context = null) : FeatureNode(propertyName, context)
{
    private readonly TypeNode _propertyType = propertyType;
    private readonly ExpressionNode? _initialValue = initialValue;

    public override object? Execute(RuntimeEnvironment env)
    {
        return _initialValue?.Execute(env);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{GetIndentation()}Property {_featureIdNode} : {_propertyType}");
        
        if (_initialValue != null)
        {
            sb.AppendLine(" =");
            IncreaseIndent();
            sb.Append(_initialValue.ToString());
            DecreaseIndent();
        }
        
        return sb.ToString();
    }
}
