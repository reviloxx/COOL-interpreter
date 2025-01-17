using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class FormalNode : AstNode
{
    public FormalNode(ParserRuleContext context) : base(context)
    {
    }
}