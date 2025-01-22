namespace Cool.Interpreter.ASTNodes;

public abstract class UnaryNode(ParserRuleContext context) : ExpressionNode(context)
{
    
    public ExpressionNode Operand { get; set; }

    public abstract string Symbol { get; }
}