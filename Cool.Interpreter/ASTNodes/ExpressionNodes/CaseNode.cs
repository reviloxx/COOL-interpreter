using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class CaseNode : ExpressionNode
{
    public CaseNode(ParserRuleContext context) : base(context)
    {
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}