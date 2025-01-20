using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class SmallerNode : BinaryOperationNode
{
    public override string Symbol => "<"; 
    
    public SmallerNode(ParserRuleContext context):base(context){}
    
    public override void Execute()
    {
        throw new NotImplementedException();
    }
}