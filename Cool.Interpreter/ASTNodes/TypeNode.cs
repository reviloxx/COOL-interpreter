namespace Cool.Interpreter.ASTNodes;

public class TypeNode(string typeName, ParserRuleContext? context = null) : AstNode(context)
{
    public string TypeName { get; private set; } = typeName;

    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
    
    public override string ToString()
    {
        return TypeName;
    }
}