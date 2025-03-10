namespace Cool.Interpreter.ASTNodes.BinaryOperationNodes;

public abstract class BinaryOperationNode(ExpressionNode leftOperand, ExpressionNode rightOperand, ParserRuleContext? context = null) : ExpressionNode(context)
{
    protected readonly ExpressionNode _leftOperand = leftOperand;
    protected readonly ExpressionNode _rightOperand = rightOperand;

    public abstract string Symbol { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{GetIndentation()}{Symbol}");
        IncreaseIndent();
        sb.AppendLine($"{GetIndentation()}Left: {_leftOperand}");
        sb.AppendLine($"{GetIndentation()}Right: {_rightOperand}");
        DecreaseIndent();
        return sb.ToString();
    }
}