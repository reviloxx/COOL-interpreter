namespace Cool.Interpreter.ASTNodes;

public class FormalNode : AstNode
{
    public FormalNode(ParserRuleContext context) : base(context)
    {
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
    
}