namespace Cool.Interpreter.ASTNodes.BinaryOperationNodes;

public class MulNode(ParserRuleContext? context, ExpressionNode leftOperand, ExpressionNode rightOperand) : BinaryOperationNode(context, leftOperand, rightOperand)
{
    public override string Symbol => "*";

    public override object? Execute(RuntimeEnvironment env)
    {
        // Execute both operands
        object? leftValue = _leftOperand?.Execute(env);
        object? rightValue = _rightOperand?.Execute(env);

        // Check if both operands are integers
        if (leftValue is int leftInt && rightValue is int rightInt)
        {
            return leftInt * rightInt;
        }

        throw new Exception($"Type error: '*' operation expects two Ints but got {leftValue?.GetType().Name} and {rightValue?.GetType().Name} at line {Line}, column {Column}");
    }
}