namespace Cool.Interpreter.ASTNodes.FeatureNodes;

public class BuiltInMethodNode(string methodName, IEnumerable<ParameterNode> parameters, TypeNode returnType, Func<RuntimeEnvironment, List<object?>, object?> body) 
    : MethodNode(methodName, parameters, returnType, null, null)
{
    private Func<RuntimeEnvironment, List<object?>, object?> _body => body;

    public override object? Execute(RuntimeEnvironment env)
    {
        var args = Parameters.Select(p => env.LookupVariable(p.ParameterName)).ToList();
        return _body(env, args);
    }
}