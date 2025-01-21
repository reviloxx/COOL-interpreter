using Antlr4.Runtime;

namespace Cool.Interpreter.ASTNodes;

public abstract class UnaryNode : ExpressionNode
{
    
    public ExpressionNode Operand { get; set; }

    public abstract string Symbol { get; }
    
    public UnaryNode(ParserRuleContext context) : base(context)
    {
    }
}