namespace Cool.Interpreter.ASTNodes;

public class CaseNode(ParserRuleContext? context) : ExpressionNode(context)
{
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}