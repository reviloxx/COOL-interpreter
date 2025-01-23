namespace Cool.Interpreter.ASTNodes;

public class LetInNode(IEnumerable<PropertyNode> declarations, ExpressionNode? body, ParserRuleContext? context) : ExpressionNode(context)
{
    private readonly IEnumerable<PropertyNode> _declarations = declarations;
    private readonly ExpressionNode? _body = body;

    public override object? Execute(RuntimeEnvironment env)
    {
        // Create a new scope for the let-in expression
        env.PushScope();
        
        try
        {
            // Define variables in the current scope
            foreach (var declaration in _declarations)
            {
                object? initialValue = null;
        
                // If the property has an initializer, execute it
                if (declaration.InitialValue != null)
                {
                    initialValue = declaration.InitialValue.Execute(env);
                }
        
                // Define the variable in the current scope
                env.DefineVariable(declaration.FeatureName, initialValue);
            }
        
            // Execute the body expression
            return _body?.Execute(env);
        }
        finally
        {
            // Always pop the scope, even if an exception occurs
            env.PopScope();
        }        
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Let {{");
        
        IncreaseIndent();
        foreach (var declaration in _declarations)
        {
            sb.AppendLine(declaration.ToString());
        }
        
        if (_body != null)
        {
            sb.AppendLine("In:");
            sb.Append(_body.ToString());
        }
        DecreaseIndent();
        
        sb.Append($"{GetIndentation()}}}");
        return sb.ToString();
    }
}