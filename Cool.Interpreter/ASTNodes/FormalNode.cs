using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class FormalNode : AstNode
{
    public FormalNode(ParserRuleContext context) : base(context)
    {
    }
    
    public override void Execute()
    {
        throw new NotImplementedException();
    }
    
}