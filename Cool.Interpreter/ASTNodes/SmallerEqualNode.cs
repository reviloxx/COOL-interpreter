using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public class SmallerEqualNode : BinaryOperationNode
{
    public override string Symbol => "<="; 
    
    public SmallerEqualNode(ParserRuleContext context):base(context){}
    
    public override object? Execute(RuntimeEnvironment env)
    {
        throw new NotImplementedException();
    }
}