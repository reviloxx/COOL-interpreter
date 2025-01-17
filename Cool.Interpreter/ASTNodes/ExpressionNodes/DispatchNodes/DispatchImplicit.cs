using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class DispatchImplicit : DispatchNode
{
    public DispatchImplicit(ParserRuleContext context) : base(context)
    {
    }
}