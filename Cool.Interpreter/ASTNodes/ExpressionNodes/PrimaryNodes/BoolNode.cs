using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class BoolNode : PrimaryNode
{
    public BoolNode(ParserRuleContext context) : base(context)
    {
    }
}