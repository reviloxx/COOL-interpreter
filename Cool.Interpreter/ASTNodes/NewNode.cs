namespace Cool.Interpreter.ASTNodes;

public class NewNode(string typeName, ParserRuleContext? context = null) : ExpressionNode(context)
{
    private readonly IdNode _typeIdNode = new(typeName, context);

    public override object? Execute(RuntimeEnvironment env)
    {
        var className = _typeIdNode.Name;
        // For now, just return the class node itself
        return env.LookupClass(className) ?? throw new Exception($"Cannot instantiate undefined class {className} at line {_line}, column {_column}");         
    }

    public override string ToString()
    {
        return $"{GetIndentation()}new {_typeIdNode.Name}";
    }
}