namespace Cool.Interpreter.ASTNodes;

public class NewNode(ParserRuleContext context) : ExpressionNode(context)
{
    public IdNode TypeName { get; set; }

    public override object? Execute(RuntimeEnvironment env)
    {
        var className = TypeName.Name;
        // For now, just return the class node itself
        return env.LookupClass(className) ?? throw new Exception($"Cannot instantiate undefined class {className} at line {Line}, column {Column}");         
    }

    public override string ToString()
    {
        return $"{GetIndentation()}new {TypeName.Name}";
    }
}