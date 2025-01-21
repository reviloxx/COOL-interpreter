using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class BuiltInMethodNode : MethodNode
{
    public required Func<RuntimeEnvironment, List<object?>, object?> ExecuteImpl { get; set; }

    public BuiltInMethodNode(ParserRuleContext? context) : base(context)
    {
    }

    public override object? Execute(RuntimeEnvironment env)
    {
        var args = Parameters.Select(p => env.LookupVariable(p.ParameterName.Name)).ToList();
        return ExecuteImpl(env, args);
    }
}