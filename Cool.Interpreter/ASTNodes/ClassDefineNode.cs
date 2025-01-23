namespace Cool.Interpreter.ASTNodes;


public class ClassDefineNode(string className, string? baseClassName = null, ParserRuleContext? context = null) : AstNode(context)
{
    public string ClassName => _classIdNode.Name;
    public string? BaseClassName => _baseClassIdNode?.Name;

    private readonly IdNode _classIdNode = new(className, context);
    private readonly IdNode? _baseClassIdNode = baseClassName == null ? null : new(baseClassName, context);
    private Dictionary<string, MethodNode> _methods = [];    
    private Dictionary<string, PropertyNode> _properties { get; } = [];

    public void AddFeatures(IEnumerable<FeatureNode> features)
    {
        foreach (var feature in features)
        {
            if (feature is MethodNode method)
            {
                _methods[method.FeatureName.ToUpperInvariant()] = method;
            }
            else if (feature is PropertyNode property)
            {
                _properties[property.FeatureName.ToUpperInvariant()] = property;
            }
        }        
    }
        
    public MethodNode? GetMethod(string name)
    {
        return _methods.TryGetValue(name.ToUpperInvariant(), out var method) ? method : null;
    }        
        
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{GetIndentation()}Class {ClassName}");
        
        if (BaseClassName != null)
        {
            sb.Append($" inherits {BaseClassName}");
        }
        sb.AppendLine();
        
        IncreaseIndent();
        foreach (var method in _methods)
        {
            sb.AppendLine(method.ToString());
        }
        foreach (var property in _properties)
        {
            sb.AppendLine(property.ToString());
        }
                
        DecreaseIndent();
        
        return sb.ToString();
    }
        
    public override object? Execute(RuntimeEnvironment env)
    {
        // Find and execute the main method if this is the Main class
        if (ClassName.Equals("Main", StringComparison.OrdinalIgnoreCase))
        {
            var mainMethod = GetMethod("main") ?? throw new Exception("No main method found in Main class");
            env.PushScope(); // Create scope for main method
            env.DefineVariable("self", this);
            mainMethod.Execute(env);
            env.PopScope();
        }

        return null;
    }
}