namespace Cool.Interpreter.ASTNodes;

public abstract class UnaryNode(ExpressionNode operand, ParserRuleContext? context) : ExpressionNode(context)
{

    protected readonly ExpressionNode _operand = operand;

    public abstract string Symbol { get; }
}