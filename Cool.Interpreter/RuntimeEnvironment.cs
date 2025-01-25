using Cool.Interpreter.ASTNodes;

namespace Cool.Interpreter;

public class RuntimeEnvironment
{
    // Track all defined classes
    private Dictionary<string, ClassDefineNode> Classes { get; } = new();
    
    // Track current scope for variable lookup
    private Stack<Dictionary<string, object?>> ScopeStack { get; } = new();


    public RuntimeEnvironment()
    {
        // Register built-in classes
        RegisterClass(new IOClassNode());
    }
    
    public void RegisterClass(ClassDefineNode classNode)
    {
        Classes[classNode.ClassName.ToUpperInvariant()] = classNode;
    }
    
    public object? LookupVariable(string name)
    {
        foreach (var scope in ScopeStack)
        {
            if (scope.TryGetValue(name, out var value))
                return value;
        }
        return null;
    }
    
    public void PushScope()
    {
        ScopeStack.Push([]);
    }
    
    public void PopScope()
    {
        if (ScopeStack.Count > 0)
            ScopeStack.Pop();
    }
    
    public ClassDefineNode? LookupClass(string name)
    {
        return Classes.TryGetValue(name.ToUpperInvariant(), out var classNode) ? classNode : null;
    }

    public ClassDefineNode GetObjectClass(object obj)
    {
        // For now, just handle ClassDefineNode objects
        if (obj is ClassDefineNode classNode)
        {
            return classNode;
        }
    
        throw new Exception($"Cannot determine class for object of type {obj.GetType()}");
    }

    public void DefineVariable(string name, object? value)
    {
        if (ScopeStack.Count == 0)
            throw new Exception("No active scope");
            
        ScopeStack.Peek()[name] = value;
    }
}