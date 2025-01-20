using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class NewNode : ExpressionNode
{
    public IdNode TypeName { get; set; }

    public NewNode(ParserRuleContext context) : base(context)
    {
    }

    public override object? Execute(RuntimeEnvironment env)
    {
        var className = TypeName.Name;
        var classNode = env.LookupClass(className);
        if (classNode == null)
        {
            throw new Exception($"Cannot instantiate undefined class {className} at line {Line}, column {Column}");
    }
        return classNode; // For now, just return the class node itself
    }

    public override string ToString()
    {
        return $"{GetIndentation()}new {TypeName.Name}";
    }
}