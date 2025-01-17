using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class DispatchExplicit : DispatchNode
{
    public DispatchExplicit(ParserRuleContext context) : base(context)
    {
    }
}