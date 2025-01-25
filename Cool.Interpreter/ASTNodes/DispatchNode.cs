namespace Cool.Interpreter.ASTNodes;

public class DispatchNode(string methodName, IEnumerable<ExpressionNode> arguments, string? staticTypeName = null, ExpressionNode? expression = null, ParserRuleContext? context = null) : ExpressionNode(context)
{
    private readonly IdNode _methodId = new(methodName, context);
    private readonly IEnumerable<ExpressionNode> _arguments = arguments;
    private readonly TypeNode? _staticType = staticTypeName == null ? null : new(staticTypeName, context);    // For @TYPE syntax
    private readonly ExpressionNode? _expression = expression;  // Will be null for implicit dispatch      
    
    public override object? Execute(RuntimeEnvironment env)
    {
        // 1. Evaluate the object expression (if explicit dispatch)
        object? dispatchObject;
        ClassDefineNode? classNode;
        
        if (_expression != null) // Explicit dispatch: obj.method()
        {
            dispatchObject = _expression.Execute(env);
            if (dispatchObject == null)
                throw new Exception($"Dispatch error: null object reference at line {_line}, column {_column}");
                
            // If there's a static type (@Type), use that class
            if (_staticType != null)
            {
                classNode = env.LookupClass(_staticType.TypeName);
                if (classNode == null)
                    throw new Exception($"Static dispatch type {_staticType.TypeName} not found at line {_line}, column {_column}");
            }
            else
            {
                // Use the runtime type of the object
                classNode = env.GetObjectClass(dispatchObject);
            }
        }
        else // Implicit dispatch: method()
        {
            // For implicit dispatch, use 'self'
            dispatchObject = env.LookupVariable("self");
            if (dispatchObject == null)
                throw new Exception($"Implicit dispatch error: 'self' not found at line {_line}, column {_column}");
                
            classNode = env.GetObjectClass(dispatchObject);
        }

        if (classNode == null)
            throw new Exception($"Cannot determine class for dispatch at line {_line}, column {_column}");

        // 2. Evaluate all arguments
        var evaluatedArgs = new List<object?>();
        foreach (var arg in _arguments)
        {
            evaluatedArgs.Add(arg.Execute(env));
        }

        // 3. Find the method
        var method = classNode.GetMethod(_methodId.Name);
        if (method == null)
        {
            // If not found in current class, search in parent classes
            var currentClass = classNode;
            while (currentClass.BaseClassName != null)
            {
                var parentClass = env.LookupClass(currentClass.BaseClassName);

                if (parentClass == null)
                    break;
                    
                method = parentClass.GetMethod(_methodId.Name);
                if (method != null)
                    break;
                    
                currentClass = parentClass;
            }
            
            if (method == null)
                throw new Exception($"Method {_methodId.Name} not found in class {classNode.ClassName} or its parents at line {_line}, column {_column}");
        }

        // 4. Create new scope for method execution
        env.PushScope();
        try
        {
            // Set up 'self' in the new scope
            env.DefineVariable("self", dispatchObject);
            
            // Bind parameters to arguments
            if (method.Parameters.Count() != evaluatedArgs.Count)
                throw new Exception($"Method {_methodId.Name} expects {method.Parameters.Count()} arguments but got {evaluatedArgs.Count} at line {_line}, column {_column}");
                
            for (int i = 0; i < method.Parameters.Count(); i++)
            {
                env.DefineVariable(method.Parameters.ElementAt(i).ParameterName, evaluatedArgs[i]);
            }

            // 5. Execute the method body
            return method.Execute(env);
        }
        finally
        {
            env.PopScope();
        }
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}Dispatch");
        
        IncreaseIndent();
        
        if (_expression != null)
        {
            sb.AppendLine($"{GetIndentation()}Object:");
            IncreaseIndent();
            sb.AppendLine(_expression.ToString());
            DecreaseIndent();
        }
        
        if (_staticType != null)
        {
            sb.AppendLine($"{GetIndentation()}Static Type: {_staticType}");
        }
        
        sb.AppendLine($"{GetIndentation()}Method: {_methodId.Name}");
        
        if (_arguments.Any())
        {
            sb.AppendLine($"{GetIndentation()}Arguments:");
            IncreaseIndent();
            foreach (var arg in _arguments)
            {
                sb.AppendLine(arg.ToString());
            }
            DecreaseIndent();
        }
        
        DecreaseIndent();
        return sb.ToString();
    }
}