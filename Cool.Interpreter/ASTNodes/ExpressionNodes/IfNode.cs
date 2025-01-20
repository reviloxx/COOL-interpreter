using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class IfNode : ExpressionNode
{
    public IfNode(ParserRuleContext context) : base(context)
    {
    }
    
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}