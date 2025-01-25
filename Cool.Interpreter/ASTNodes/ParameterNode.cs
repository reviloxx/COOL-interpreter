namespace Cool.Interpreter.ASTNodes;

public class ParameterNode(string parameterName, TypeNode parameterType, ParserRuleContext? context = null) : AstNode(context)
{
    public string ParameterName => _parameterIdNode.Name;
    public string TypeName => _parameterType.TypeName;
    private readonly IdNode _parameterIdNode = new(parameterName, context);
    private readonly TypeNode _parameterType = parameterType;

    public override string ToString()
    {
        return $"{_parameterIdNode} : {_parameterType}";
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}