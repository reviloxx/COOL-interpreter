using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class StringNode : PrimaryNode
{
    public StringNode(ParserRuleContext context) : base(context)
    {
    }
}