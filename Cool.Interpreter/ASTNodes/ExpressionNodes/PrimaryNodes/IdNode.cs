using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class IdNode : PrimaryNode
{
    public IdNode(ParserRuleContext context) : base(context)
    {
        
    }
}