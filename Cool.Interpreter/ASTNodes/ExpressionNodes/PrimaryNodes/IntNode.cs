using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class IntNode : PrimaryNode
{
    public IntNode(ParserRuleContext context) : base(context)
    {
    }
}
