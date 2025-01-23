namespace Cool.Interpreter.ASTNodes;

public class FormalNode(ParserRuleContext context) : AstNode(context)
{
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
    
}