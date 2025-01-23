namespace Cool.Interpreter.ASTNodes.FeatureNodes;

public class PropertyNode(string propertyName, TypeNode propertyType, ExpressionNode? initialValue = null, ParserRuleContext? context = null) : FeatureNode(propertyName, context)
{
    private readonly TypeNode _propertyType = propertyType;
    public ExpressionNode? InitialValue { get; private set; } = initialValue;

    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{GetIndentation()}Property {_featureIdNode} : {_propertyType}");
        
        if (InitialValue != null)
        {
            sb.AppendLine(" =");
            IncreaseIndent();
            sb.Append(InitialValue.ToString());
            DecreaseIndent();
        }
        
        return sb.ToString();
    }
}
