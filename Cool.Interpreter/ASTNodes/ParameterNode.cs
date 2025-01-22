namespace Cool.Interpreter.ASTNodes;

public class ParameterNode(ParserRuleContext? context) : AstNode(context)
{
    public required IdNode ParameterName { get; set; }
    public required TypeNode ParameterType { get; set; }

    public override string ToString()
    {
        return $"{ParameterName} : {ParameterType}";
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}