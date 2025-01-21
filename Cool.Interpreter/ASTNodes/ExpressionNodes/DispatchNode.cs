using System.Text;
using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class DispatchNode : ExpressionNode
{
    public ExpressionNode? Object { get; set; }  // Will be null for implicit dispatch
    public TypeNode? StaticType { get; set; }    // For @TYPE syntax
    public IdNode MethodName { get; set; }
    public List<ExpressionNode> Arguments { get; set; } = new();

    public DispatchNode(ParserRuleContext context) : base(context)
    {
    }

public override object? Execute(RuntimeEnvironment env)
    {
        // 1. Evaluate the object expression (if explicit dispatch)
        object? dispatchObject;
        ClassDefineNode? classNode;
        
        if (Object != null) // Explicit dispatch: obj.method()
        {
            dispatchObject = Object.Execute(env);
            if (dispatchObject == null)
                throw new Exception($"Dispatch error: null object reference at line {Line}, column {Column}");
                
            // If there's a static type (@Type), use that class
            if (StaticType != null)
            {
                classNode = env.LookupClass(StaticType.TypeName);
                if (classNode == null)
                    throw new Exception($"Static dispatch type {StaticType.TypeName} not found at line {Line}, column {Column}");
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
                throw new Exception($"Implicit dispatch error: 'self' not found at line {Line}, column {Column}");
                
            classNode = env.GetObjectClass(dispatchObject);
        }

        if (classNode == null)
            throw new Exception($"Cannot determine class for dispatch at line {Line}, column {Column}");

        // 2. Evaluate all arguments
        var evaluatedArgs = new List<object?>();
        foreach (var arg in Arguments)
        {
            evaluatedArgs.Add(arg.Execute(env));
        }

        // 3. Find the method
        var method = classNode.GetMethod(MethodName.Name);
        if (method == null)
        {
            // If not found in current class, search in parent classes
            var currentClass = classNode;
            while (currentClass.BaseClassName != null)
            {
                var parentClass = env.LookupClass(currentClass.BaseClassName.Name);
                if (parentClass == null)
                    break;
                    
                method = parentClass.GetMethod(MethodName.Name);
                if (method != null)
                    break;
                    
                currentClass = parentClass;
            }
            
            if (method == null)
                throw new Exception($"Method {MethodName.Name} not found in class {classNode.ClassName.Name} or its parents at line {Line}, column {Column}");
        }

        // 4. Create new scope for method execution
        env.PushScope();
        try
        {
            // Set up 'self' in the new scope
            env.DefineVariable("self", dispatchObject);
            
            // Bind parameters to arguments
            if (method.Parameters.Count != evaluatedArgs.Count)
                throw new Exception($"Method {MethodName.Name} expects {method.Parameters.Count} arguments but got {evaluatedArgs.Count} at line {Line}, column {Column}");
                
            for (int i = 0; i < method.Parameters.Count; i++)
            {
                env.DefineVariable(method.Parameters[i].ParameterName.Name, evaluatedArgs[i]);
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
        
        if (Object != null)
        {
            sb.AppendLine($"{GetIndentation()}Object:");
            IncreaseIndent();
            sb.AppendLine(Object.ToString());
            DecreaseIndent();
        }
        
        if (StaticType != null)
        {
            sb.AppendLine($"{GetIndentation()}Static Type: {StaticType}");
        }
        
        sb.AppendLine($"{GetIndentation()}Method: {MethodName}");
        
        if (Arguments.Any())
        {
            sb.AppendLine($"{GetIndentation()}Arguments:");
            IncreaseIndent();
            foreach (var arg in Arguments)
            {
                sb.AppendLine(arg.ToString());
            }
            DecreaseIndent();
        }
        
        DecreaseIndent();
        return sb.ToString();
    }
}