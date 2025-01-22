namespace Cool.Interpreter.ASTNodes.FeatureNodes;

public class BuiltInMethodNode() : MethodNode(null)
{
    public required Func<RuntimeEnvironment, List<object?>, object?> ExecuteImpl { get; set; }

    public override object? Execute(RuntimeEnvironment env)
    {
        var args = Parameters.Select(p => env.LookupVariable(p.ParameterName.Name)).ToList();
        return ExecuteImpl(env, args);
    }
}