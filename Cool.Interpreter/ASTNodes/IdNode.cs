namespace Cool.Interpreter.ASTNodes;

public class IdNode(string name, ParserRuleContext? context) : ExpressionNode(context) 
{
    public string Name { get; private set; } = name;
        
    public override object? Execute(RuntimeEnvironment env)
    {
        // Look up the variable in the current environment
        var value = env.LookupVariable(Name);

        return value ?? throw new Exception($"Variable '{Name}' not found at line {Line}, column {Column}");
    }
}