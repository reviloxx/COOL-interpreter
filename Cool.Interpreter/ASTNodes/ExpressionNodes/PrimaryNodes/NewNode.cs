using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class NewNode : PrimaryNode
{
    public NewNode(ParserRuleContext context) : base(context)
    {
    }
}